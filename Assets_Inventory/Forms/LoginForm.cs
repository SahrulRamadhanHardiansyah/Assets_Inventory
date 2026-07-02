using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using Krypton.Toolkit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;


namespace Assets_Inventory
{
    public partial class LoginForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "admin2";
            txtPassword.Text = "password";
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

                var pengguna = db.Pengguna.FirstOrDefault(u => u.Username == username);

                if (pengguna != null && BCrypt.Net.BCrypt.Verify(password, pengguna.Password))
                {
                    Properties.Settings.Default.userId = pengguna.IdPengguna;
                    Properties.Settings.Default.Save();

                    AuthManager.SetUserSession(pengguna.IdPengguna, pengguna.IdPeran, pengguna.Username);

                    MessageBox.Show("Login Berhasil.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm form = new MainForm();
                    form.Show();
                    this.Hide();
                    form.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    MessageBox.Show("Login gagal: username atau password salah.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
