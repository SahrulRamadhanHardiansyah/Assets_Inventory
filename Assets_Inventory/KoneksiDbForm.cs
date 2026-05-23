using ComponentFactory.Krypton.Toolkit;
using System;
using System.Configuration;
using System.Windows.Forms;
using Assets_Inventory.Models;
using System.Data.Common;
using MySql.Data.MySqlClient; // Wajib ditambahkan untuk menangkap MySqlException

namespace Assets_Inventory
{
    public partial class KoneksiDbForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db;

        public KoneksiDbForm()
        {
            InitializeComponent();
        }

        private void KoneksiDbForm_Load(object sender, EventArgs e)
        {
            try
            {
                var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];

                if (settings != null && !string.IsNullOrEmpty(settings.ConnectionString))
                {
                    var builder = new DbConnectionStringBuilder();
                    builder.ConnectionString = settings.ConnectionString;

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
                Console.WriteLine("Gagal memuat konfigurasi awal: " + ex.Message);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHost.Text))
            {
                MessageBox.Show("Host harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtPort.Text))
            {
                MessageBox.Show("Port harus diisi (misal: 3306).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtDatabase.Text))
            {
                MessageBox.Show("Database harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("User harus diisi (misal: root).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connString = $"Server={txtHost.Text};Port={txtPort.Text};Database={txtDatabase.Text};Uid={txtUser.Text};Pwd={txtPassword.Text};";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open(); 
                }

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = config.ConnectionStrings.ConnectionStrings["KoneksiServer"];

                if (settings == null)
                {
                    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("KoneksiServer", connString, "MySql.Data.MySqlClient"));
                }
                else
                {
                    settings.ConnectionString = connString;
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("Koneksi berhasil diubah dan terhubung ke Database!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (MySqlException ex) 
            {
                string pesanError = "";

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
                        pesanError = $"Terjadi error MySQL yang tidak terduga (Kode {ex.Number}):\n{ex.Message}";
                        break;
                }

                MessageBox.Show(pesanError, "Koneksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem saat mencoba koneksi:\n" + (ex.InnerException?.Message ?? ex.Message), "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}