using Assets_Inventory.Helper;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient; 
using System;
using System.Collections.Generic;
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
            var hakAkses = AuthManager.GetAkses("Backup");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtPath.Text = Properties.Settings.Default.BackupPath;
            int interval = Properties.Settings.Default.BackupInterval;
            txtMenit.Text = interval > 0 ? interval.ToString() : "";
            LoadDaftarTabel();
        }

        private void LoadDaftarTabel()
        {
            try
            {
                pnlTabel.Controls.Clear();
                var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
                if (settings == null || string.IsNullOrEmpty(settings.ConnectionString)) return;

                using (MySqlConnection conn = new MySqlConnection(settings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tableName = reader.GetString(0);
                                DbTableControl tableCtrl = new DbTableControl
                                {
                                    NamaModul = tableName,
                                    IsChecked = false,
                                    Dock = DockStyle.Top
                                };
                                pnlTabel.Controls.Add(tableCtrl);
                                tableCtrl.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar tabel: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Gagal melakukan backup.\n\nDetail Error: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformBackup(string folderPath)
        {
            var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new Exception("Konfigurasi database tidak ditemukan di App.config.");
            }

            var builder = new DbConnectionStringBuilder { ConnectionString = settings.ConnectionString };

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
            if (!string.IsNullOrEmpty(dbPass)) dumpCommand += $"-p\"{dbPass}\" ";
            dumpCommand += $"{dbDatabase} > \"{fullPath}\"";

            ExecuteDumpProcess(dumpCommand);
        }

        private void btnBrowseTabel_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Pilih folder untuk menyimpan file backup spesifik tabel";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPathTabel.Text = fbd.SelectedPath;
                    txtPathTabel.Enabled = true;
                    pnlTabel.Enabled = true;
                }
            }
        }

        private void btnBackupTabel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPathTabel.Text))
            {
                MessageBox.Show("Silakan pilih lokasi penyimpanan backup untuk tabel melalui tombol Browse.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> selectedTables = new List<string>();
            foreach (Control ctrl in pnlTabel.Controls)
            {
                if (ctrl is DbTableControl tableCtrl && tableCtrl.IsChecked)
                {
                    selectedTables.Add(tableCtrl.NamaModul);
                }
            }

            if (selectedTables.Count == 0)
            {
                MessageBox.Show("Silakan centang minimal satu tabel untuk di-backup.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PerformBackupTables(txtPathTabel.Text, selectedTables);
                MessageBox.Show($"Proses Backup untuk {selectedTables.Count} tabel berhasil diselesaikan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal melakukan backup parsial.\n\nDetail Error: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformBackupTables(string folderPath, List<string> tables)
        {
            var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new Exception("Konfigurasi database tidak ditemukan di App.config.");
            }

            var builder = new DbConnectionStringBuilder { ConnectionString = settings.ConnectionString };

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

            string tablesString = string.Join(" ", tables);

            string fileName = $"Backup_Tables_{dbDatabase}_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
            string fullPath = Path.Combine(folderPath, fileName);

            string dumpCommand = $"mysqldump -h {dbHost} -P {dbPort} -u {dbUser} ";
            if (!string.IsNullOrEmpty(dbPass)) dumpCommand += $"-p\"{dbPass}\" ";

            dumpCommand += $"{dbDatabase} {tablesString} > \"{fullPath}\"";

            ExecuteDumpProcess(dumpCommand);
        }

        private void ExecuteDumpProcess(string dumpCommand)
        {
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
                    throw new Exception("Proses 'mysqldump' ditolak atau tidak ditemukan di Environment Variables.");
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

            MessageBox.Show("Pengaturan auto-backup berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}