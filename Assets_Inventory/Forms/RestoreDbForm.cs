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
                    // Validate file path safety and extension
                    string selected = ofd.FileName;
                    if (!ConnectionStringProtector.IsSafePath(selected) || Path.GetExtension(selected).ToLower() != ".sql")
                    {
                        MessageBox.Show("File tidak valid. Hanya file .sql yang diperbolehkan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    txtPath.Text = selected;
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

            // Validate extension
            if (Path.GetExtension(txtPath.Text).ToLower() != ".sql")
            {
                MessageBox.Show("Hanya file .sql yang diperbolehkan untuk restore.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Gagal melakukan restore. Pastikan MySQL aktif dan file backup valid.", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine("Restore error: " + ex.Message);
                }
            }
        }

        // ponytail: no shell operator <, use stdin redirection in C#
        private void PerformRestore(string filePath)
        {
            string connStr = ConnectionStringProtector.GetDecryptedConnectionString();
            if (string.IsNullOrEmpty(connStr))
                throw new Exception("Konfigurasi database tidak ditemukan.");

            var builder = new DbConnectionStringBuilder();
            builder.ConnectionString = connStr;

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

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string mysqlPath = Path.Combine(appFolder, "Resources", "mysql.exe");

            bool useFallback = !File.Exists(mysqlPath);
            string mysqlExe = useFallback ? "mysql" : mysqlPath;

            // Validate filePath still exists and is .sql
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File backup tidak ditemukan.");
            if (Path.GetExtension(filePath).ToLower() != ".sql")
                throw new ArgumentException("Hanya file .sql yang diperbolehkan.");

            string args = string.Format("-h {0} -P {1} -u {2} {3}",
                EscapeArg(dbHost), EscapeArg(dbPort), EscapeArg(dbUser), EscapeArg(dbDatabase));

            var psi = new ProcessStartInfo
            {
                FileName = mysqlExe,
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardError = true
            };
            if (!string.IsNullOrEmpty(dbPass))
                psi.EnvironmentVariables["MYSQL_PWD"] = dbPass;

            try
            {
                using (Process process = Process.Start(psi))
                {
                    // Stream file content to mysql stdin instead of shell < operator
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        fileStream.CopyTo(process.StandardInput.BaseStream);
                    }
                    process.StandardInput.Close();

                    string err = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                        throw new Exception($"Restore gagal (exit {process.ExitCode}).");
                }
            }
            catch (Exception ex) when (!(ex.Message.Contains("Restore gagal")))
            {
                throw new Exception("Proses restore tidak dapat dijalankan: periksa instalasi MySQL.");
            }
        }

        private static string EscapeArg(string arg)
        {
            if (string.IsNullOrEmpty(arg)) return "\"\"";
            if (arg.IndexOfAny(new[] { ' ', '"', '\'', '&', '|', '<', '>', '^', ';', '`', '$', '(', ')', '*', '?' }) >= 0)
                return "\"" + arg.Replace("\"", "\\\"") + "\"";
            return arg;
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
