using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class LoginForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private AppDbContext db = new AppDbContext();
        private const int BCRYPT_WORK_FACTOR = 12;

        // Brute-force protection: in-memory dictionary (ponytail: single instance desktop app)
        private static readonly Dictionary<string, (int Count, DateTime LockUntil)> _failedAttempts
            = new Dictionary<string, (int, DateTime)>(StringComparer.OrdinalIgnoreCase);
        private static readonly object _failLock = new object();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtUsername.Text?.Trim();
                var password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username dan password harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check lockout
                lock (_failLock)
                {
                    if (_failedAttempts.TryGetValue(username, out var fail) && fail.LockUntil > DateTime.Now)
                    {
                        var remaining = fail.LockUntil - DateTime.Now;
                        MessageBox.Show($"Akun terkunci karena terlalu banyak percobaan gagal. Coba lagi dalam {remaining.TotalMinutes:F0} menit.",
                            "Akun Terkunci", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Constant-time: always query, always show same generic message on fail
                var pengguna = db.Pengguna.FirstOrDefault(u => u.Username == username);
                bool isValid = pengguna != null && BCrypt.Net.BCrypt.Verify(password, pengguna.Password);

                if (isValid)
                {
                    // Clear failed attempts on success
                    lock (_failLock)
                    {
                        _failedAttempts.Remove(username);
                    }

                    // Rehash if work factor outdated (10 -> 12)
                    try
                    {
                        if (pengguna != null && BCrypt.Net.BCrypt.PasswordNeedsRehash(pengguna.Password, BCRYPT_WORK_FACTOR))
                        {
                            pengguna.Password = BCrypt.Net.BCrypt.HashPassword(password, BCRYPT_WORK_FACTOR);
                            db.SaveChanges();
                        }
                    }
                    catch { /* non-critical */ }

                    Properties.Settings.Default.userId = pengguna.IdPengguna;
                    Properties.Settings.Default.Save();

                    AuthManager.SetUserSession(pengguna.IdPengguna, pengguna.IdPeran, pengguna.Username);

                    MainForm form = new MainForm();
                    form.Show();
                    this.Hide();
                    form.FormClosed += (s, args) =>
                    {
                        AuthManager.ClearSession();
                        this.Close();
                    };
                }
                else
                {
                    // Increment failed counter
                    lock (_failLock)
                    {
                        _failedAttempts.TryGetValue(username, out var existing);
                        int newCount = existing.Count + 1;
                        DateTime lockUntil = DateTime.MinValue;
                        if (newCount >= 5)
                        {
                            lockUntil = DateTime.Now.AddMinutes(15);
                        }
                        _failedAttempts[username] = (newCount, lockUntil);
                    }

                    // Generic message - do not reveal if user exists
                    MessageBox.Show("Login gagal: username atau password salah.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Optional: log to debug
                    System.Diagnostics.Debug.WriteLine($"Failed login attempt for user: {username}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Login error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan saat login. Silakan hubungi administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

    }
}
