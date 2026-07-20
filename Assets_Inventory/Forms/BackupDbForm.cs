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
using System.Linq;
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
                string connStr = ConnectionStringProtector.GetDecryptedConnectionString();
                if (string.IsNullOrEmpty(connStr)) return;

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tableName = reader.GetString(0);
                                // Validate table name matches safe pattern
                                if (!ConnectionStringProtector.IsSafeTableName(tableName)) continue;

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
                // Generic message to user, log details privately
                System.Diagnostics.Debug.WriteLine("Gagal memuat daftar tabel: " + ex.Message);
                MessageBox.Show("Gagal memuat daftar tabel database.", "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ConnectionStringProtector.ValidateBackupFolder(txtPath.Text);
                PerformBackup(txtPath.Text);
                MessageBox.Show("Proses Backup Database berhasil diselesaikan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal melakukan backup. Periksa pengaturan koneksi dan lokasi penyimpanan.", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("Backup error: " + ex.Message);
            }
        }

        private void PerformBackup(string folderPath)
        {
            string connStr = ConnectionStringProtector.GetDecryptedConnectionString();
            if (string.IsNullOrEmpty(connStr))
                throw new Exception("Konfigurasi database tidak ditemukan.");

            var builder = new DbConnectionStringBuilder { ConnectionString = connStr };

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

            // Validate
            if (!ConnectionStringProtector.IsSafeHost(dbHost))
                throw new ArgumentException("Host tidak valid.");
            if (!ConnectionStringProtector.IsSafeIdentifier(dbDatabase))
                throw new ArgumentException("Database name tidak valid.");

            ConnectionStringProtector.ValidateBackupFolder(folderPath);

            string fileName = $"Backup_{SanitizeFileName(dbDatabase)}_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
            string fullPath = Path.Combine(folderPath, fileName);
            string fullResolved = Path.GetFullPath(fullPath);
            string folderResolved = Path.GetFullPath(folderPath);
            if (!fullResolved.StartsWith(folderResolved, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Path backup tidak valid (path traversal).");

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string mysqldumpFromResources = Path.Combine(appFolder, "Resources", "mysqldump.exe");
            string dumpExe = File.Exists(mysqldumpFromResources) ? mysqldumpFromResources : "mysqldump";

            ExecuteDumpProcess(dumpExe, dbHost, dbPort, dbUser, dbPass, dbDatabase, null, fullResolved);
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
                    // Validate each table name against whitelist regex (ponytail: identifier cannot be parameterized)
                    if (!ConnectionStringProtector.IsSafeTableName(tableCtrl.NamaModul))
                    {
                        MessageBox.Show($"Nama tabel tidak valid: {tableCtrl.NamaModul}", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
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
                ConnectionStringProtector.ValidateBackupFolder(txtPathTabel.Text);
                ConnectionStringProtector.ValidateTableNames(selectedTables);
                PerformBackupTables(txtPathTabel.Text, selectedTables);
                MessageBox.Show($"Proses Backup untuk {selectedTables.Count} tabel berhasil diselesaikan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal melakukan backup parsial. Periksa pengaturan koneksi.", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("Backup tables error: " + ex.Message);
            }
        }

        private void PerformBackupTables(string folderPath, List<string> tables)
        {
            string connStr = ConnectionStringProtector.GetDecryptedConnectionString();
            if (string.IsNullOrEmpty(connStr))
                throw new Exception("Konfigurasi database tidak ditemukan.");

            var builder = new DbConnectionStringBuilder { ConnectionString = connStr };

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

            if (!ConnectionStringProtector.IsSafeHost(dbHost))
                throw new ArgumentException("Host tidak valid.");
            if (!ConnectionStringProtector.IsSafeIdentifier(dbDatabase))
                throw new ArgumentException("Database name tidak valid.");

            ConnectionStringProtector.ValidateBackupFolder(folderPath);
            ConnectionStringProtector.ValidateTableNames(tables);

            string fileName = $"Backup_Tables_{SanitizeFileName(dbDatabase)}_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
            string fullPath = Path.Combine(folderPath, fileName);
            string fullResolved = Path.GetFullPath(fullPath);
            string folderResolved = Path.GetFullPath(folderPath);
            if (!fullResolved.StartsWith(folderResolved, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Path backup tidak valid (path traversal).");

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string mysqldumpFromResources = Path.Combine(appFolder, "Resources", "mysqldump.exe");
            string dumpExe = File.Exists(mysqldumpFromResources) ? mysqldumpFromResources : "mysqldump";

            ExecuteDumpProcess(dumpExe, dbHost, dbPort, dbUser, dbPass, dbDatabase, tables, fullResolved);
        }

        // ponytail: no shell, direct process execution with MYSQL_PWD env var + streaming output
        private void ExecuteDumpProcess(string dumpExe, string host, string port, string user, string pass, string database, List<string> tables, string outputFile)
        {
            // Escape args safely (no shell)
            string tablePart = (tables != null && tables.Count > 0) ? " " + string.Join(" ", tables.Select(t => EscapeArg(t))) : "";
            string args = string.Format("-h {0} -P {1} -u {2} {3}{4}",
                EscapeArg(host), EscapeArg(port), EscapeArg(user), EscapeArg(database), tablePart);

            var psi = new ProcessStartInfo
            {
                FileName = dumpExe,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            // Avoid password in command line; use env var
            if (!string.IsNullOrEmpty(pass))
                psi.EnvironmentVariables["MYSQL_PWD"] = pass;

            try
            {
                using (Process process = Process.Start(psi))
                {
                    using (var fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        process.StandardOutput.BaseStream.CopyTo(fileStream);
                    }
                    string err = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        // Clean up partial file
                        try { if (File.Exists(outputFile)) File.Delete(outputFile); } catch { }
                        throw new Exception($"mysqldump gagal (exit {process.ExitCode}).");
                    }
                }

                // Validate file was created and non-empty
                if (!File.Exists(outputFile) || new FileInfo(outputFile).Length == 0)
                    throw new Exception("File backup tidak terbentuk atau kosong.");
            }
            catch (Exception ex) when (!(ex.Message.Contains("mysqldump gagal")))
            {
                throw new Exception("Proses backup tidak dapat dijalankan: periksa instalasi MySQL.");
            }
        }

        private static string EscapeArg(string arg)
        {
            if (string.IsNullOrEmpty(arg)) return "\"\"";
            // Escape if contains special chars - wrap in quotes
            if (arg.IndexOfAny(new[] { ' ', '"', '\'', '&', '|', '<', '>', '^', ';', '`', '$', '(', ')', '*', '?' }) >= 0)
                return "\"" + arg.Replace("\"", "\\\"") + "\"";
            return arg;
        }

        private static string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
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
