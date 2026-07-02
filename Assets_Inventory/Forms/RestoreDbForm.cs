using Assets_Inventory.Helper;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class RestoreDbForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public RestoreDbForm()
        {
            InitializeComponent();
        }

        private void RestoreDbForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Restore");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtPath.Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "SQL Backup Files|*.sql";
                ofd.Title = "Pilih File Backup Database";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = ofd.FileName;
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text) || !File.Exists(txtPath.Text))
            {
                MessageBox.Show("Silakan pilih file SQL yang valid terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("PERINGATAN: Memulihkan database akan menimpa/menghapus data saat ini dengan data dari file backup. Apakah Anda yakin ingin melanjutkan?", "Konfirmasi Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                try
                {
                    PerformRestore(txtPath.Text);
                    MessageBox.Show("Proses Restore Database berhasil diselesaikan! Silakan login ulang jika diperlukan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal melakukan restore. Pastikan XAMPP/MySQL Anda menyala dan 'mysql.exe' tersedia.\n\nDetail Error: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PerformRestore(string filePath)
        {
            var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
                throw new Exception("Konfigurasi database tidak ditemukan.");

            var builder = new DbConnectionStringBuilder();
            builder.ConnectionString = settings.ConnectionString;

            builder.TryGetValue("Server", out object host);
            builder.TryGetValue("Database", out object dbName);
            builder.TryGetValue("Uid", out object user);
            builder.TryGetValue("Pwd", out object pass);
            builder.TryGetValue("Port", out object port);

            string dbHost = host?.ToString() ?? "localhost";
            string dbUser = user?.ToString() ?? "root";
            string dbPass = pass?.ToString() ?? "";
            string dbDatabase = dbName?.ToString() ?? "";
            string dbPort = port?.ToString() ?? "3306";

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string mysqlPath = Path.Combine(appFolder, "Resources", "mysql.exe");

            if (!File.Exists(mysqlPath))
            {
                throw new Exception($"File mysql.exe tidak ditemukan di: {mysqlPath}");
            }

            string restoreCommand = $"\"{mysqlPath}\" -h {dbHost} -P {dbPort} -u {dbUser} ";

            if (!string.IsNullOrEmpty(dbPass))
            {
                restoreCommand += $"-p\"{dbPass}\" ";
            }

            restoreCommand += $"{dbDatabase} < \"{filePath}\"";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {restoreCommand}",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                if (process.ExitCode != 0) throw new Exception("Gagal menjalankan perintah restore.");
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}