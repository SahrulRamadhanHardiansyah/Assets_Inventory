using ComponentFactory.Krypton.Toolkit;
using System;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class BackupDbForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public BackupDbForm()
        {
            InitializeComponent();
        }

        private void BackupDbForm_Load(object sender, EventArgs e)
        {
            txtPath.Text = Properties.Settings.Default.BackupPath;
            int interval = Properties.Settings.Default.BackupInterval;
            txtMenit.Text = interval > 0 ? interval.ToString() : "";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Pilih folder untuk menyimpan file backup database";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("Silakan pilih lokasi penyimpanan backup terlebih dahulu melalui tombol Browse.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PerformBackup(txtPath.Text);
                MessageBox.Show("Proses Backup Database berhasil diselesaikan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal melakukan backup. Pastikan XAMPP/MySQL Anda menyala dan 'mysqldump' tersedia di Environment Variables.\n\nDetail Error: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformBackup(string folderPath)
        {
            var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new Exception("Konfigurasi database tidak ditemukan di App.config. Silakan atur Koneksi Database terlebih dahulu.");
            }

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

            string fileName = $"Backup_{dbDatabase}_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
            string fullPath = Path.Combine(folderPath, fileName);

            string dumpCommand = $"mysqldump -h {dbHost} -P {dbPort} -u {dbUser} ";

            if (!string.IsNullOrEmpty(dbPass))
            {
                dumpCommand += $"-p\"{dbPass}\" ";
            }

            dumpCommand += $"{dbDatabase} > \"{fullPath}\"";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {dumpCommand}",
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception("Proses 'mysqldump' ditolak atau tidak ditemukan.");
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("Lokasi direktori penyimpanan hasil backup harus diisi.");
                return;
            }

            if (!int.TryParse(txtMenit.Text, out int menit) || menit <= 0)
            {
                MessageBox.Show("Interval menit otomatis harus berupa angka bulat dan lebih dari 0.");
                return;
            }

            Properties.Settings.Default.BackupPath = txtPath.Text;
            Properties.Settings.Default.BackupInterval = menit;
            Properties.Settings.Default.Save();

            MessageBox.Show("Pengaturan auto-backup berhasil disimpan!\n\nUntuk mengaktifkan fitur otomatis, pastikan Anda menambahkan kontrol Timer di MainForm yang membaca pengaturan ini.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}