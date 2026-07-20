using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.Common;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class KoneksiDbForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public KoneksiDbForm()
        {
            InitializeComponent();
        }

        private void KoneksiDbForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Koneksi");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            try
            {
                // Use protector to get decrypted string (supports legacy plaintext)
                string connString = ConnectionStringProtector.GetDecryptedConnectionString();
                if (!string.IsNullOrEmpty(connString))
                {
                    var builder = new DbConnectionStringBuilder();
                    builder.ConnectionString = connString;

                    if (builder.TryGetValue("Server", out object host)) txtHost.Text = host.ToString();
                    if (builder.TryGetValue("Port", out object port)) txtPort.Text = port.ToString();
                    else txtPort.Text = "3306";
                    if (builder.TryGetValue("Database", out object dbName)) txtDatabase.Text = dbName.ToString();
                    if (builder.TryGetValue("Uid", out object user)) txtUser.Text = user.ToString();
                    if (builder.TryGetValue("Pwd", out object pass)) txtPassword.Text = pass.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Gagal memuat konfigurasi awal: " + ex.Message);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTes_Click(object sender, EventArgs e)
        {
            // Basic required validation
            if (string.IsNullOrWhiteSpace(txtHost.Text))
            {
                MessageBox.Show("Host harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPort.Text))
            {
                MessageBox.Show("Port harus diisi (misal: 3306).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                MessageBox.Show("Database harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("User harus diisi (misal: root).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate against safe patterns using protector
            if (!ConnectionStringProtector.IsSafeHost(txtHost.Text.Trim()))
            {
                MessageBox.Show("Host mengandung karakter tidak aman.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ConnectionStringProtector.IsValidPort(txtPort.Text.Trim(), out _))
            {
                MessageBox.Show("Port harus angka 1-65535.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ConnectionStringProtector.IsSafeIdentifier(txtDatabase.Text.Trim()))
            {
                MessageBox.Show("Database name tidak valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ConnectionStringProtector.IsSafeIdentifier(txtUser.Text.Trim()))
            {
                MessageBox.Show("User tidak valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build validated conn string (no string interpolation injection)
            string error;
            string connString = ConnectionStringProtector.BuildValidatedMySqlConnectionString(
                txtHost.Text.Trim(), txtPort.Text.Trim(), txtDatabase.Text.Trim(),
                txtUser.Text.Trim(), txtPassword.Text ?? "", out error);

            if (connString == null)
            {
                MessageBox.Show(error ?? "Koneksi string tidak valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                }

                // Encrypt before save - preservasi fungsi ubah koneksi tetap jalan
                string encrypted = ConnectionStringProtector.Protect(connString);

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = config.ConnectionStrings.ConnectionStrings["KoneksiServer"];

                if (settings == null)
                {
                    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("KoneksiServer", encrypted, "MySql.Data.MySqlClient"));
                }
                else
                {
                    settings.ConnectionString = encrypted;
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("Koneksi berhasil diubah dan terhubung ke Database!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (MySqlException ex)
            {
                string pesanError;
                switch (ex.Number)
                {
                    case 1045:
                        pesanError = "Akses ditolak! Username atau Password database salah.";
                        break;
                    case 1049:
                        pesanError = $"Database '{txtDatabase.Text}' tidak ditemukan di server MySQL Anda.";
                        break;
                    case 2002:
                    case 2003:
                        pesanError = "Tidak dapat terhubung ke server. Pastikan server aktif (XAMPP/MySQL berjalan) dan Port yang digunakan benar.";
                        break;
                    case 2005:
                        pesanError = "Alamat server (Host) tidak dikenal. Periksa kembali penulisan nama server atau IP Anda.";
                        break;
                    default:
                        pesanError = $"Terjadi error MySQL (Kode {ex.Number}).";
                        break;
                }
                MessageBox.Show(pesanError, "Koneksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.WriteLine("MySQL error: " + ex.Number + " - " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem saat mencoba koneksi.", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("Koneksi error: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
