using Assets_Inventory.Helper;
using System;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.IO;

namespace Assets_Inventory
{
    public static class DatabaseHelper
    {
        public static void PerformBackup(string folderPath)
        {
            // Validate folder safety
            ConnectionStringProtector.ValidateBackupFolder(folderPath);

            string connString = ConnectionStringProtector.GetDecryptedConnectionString();
            if (string.IsNullOrEmpty(connString)) return;

            var builder = new DbConnectionStringBuilder();
            builder.ConnectionString = connString;

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

            // Validate identifiers
            if (!ConnectionStringProtector.IsSafeHost(dbHost))
                throw new ArgumentException($"Host tidak valid: {dbHost}");
            if (!ConnectionStringProtector.IsSafeIdentifier(dbDatabase))
                throw new ArgumentException($"Database name tidak valid: {dbDatabase}");

            string fileName = $"AutoBackup_{dbDatabase}_{DateTime.Now:yyyyMMdd_HHmm}.sql";
            string fullPath = Path.Combine(folderPath, fileName);
            // Ensure fullPath does not escape folder
            string fullResolved = Path.GetFullPath(fullPath);
            string folderResolved = Path.GetFullPath(folderPath);
            if (!fullResolved.StartsWith(folderResolved, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Path backup tidak valid (path traversal).");

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string mysqlDumpPath = Path.Combine(appFolder, "Resources", "mysqldump.exe");

            if (!File.Exists(mysqlDumpPath))
            {
                // Fallback: try PATH
                mysqlDumpPath = "mysqldump";
            }

            // Execute mysqldump DIRECTLY without cmd.exe shell to avoid injection
            // Use env var MYSQL_PWD to avoid password in args
            var psi = new ProcessStartInfo
            {
                FileName = mysqlDumpPath,
                Arguments = string.Format("-h {0} -P {1} -u {2} {3}", EscapeArg(dbHost), EscapeArg(dbPort), EscapeArg(dbUser), EscapeArg(dbDatabase)),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            if (!string.IsNullOrEmpty(dbPass))
                psi.EnvironmentVariables["MYSQL_PWD"] = dbPass;

            try
            {
                using (Process p = Process.Start(psi))
                {
                    using (var fileStream = new FileStream(fullResolved, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        p.StandardOutput.BaseStream.CopyTo(fileStream);
                    }
                    string err = p.StandardError.ReadToEnd();
                    p.WaitForExit();
                    if (p.ExitCode != 0)
                        throw new Exception($"Gagal menjalankan backup (exit {p.ExitCode}): {err}");
                }
            }
            finally
            {
                // Clear env var from current process handled by PSI copy, not needed, but ensure file exists
                if (!File.Exists(fullResolved) || new FileInfo(fullResolved).Length == 0)
                    throw new Exception("File backup tidak terbentuk atau kosong.");
            }
        }

        private static string EscapeArg(string arg)
        {
            if (string.IsNullOrEmpty(arg)) return "\"\"";
            // Simple escaping: wrap in quotes if needed, escape inner quotes
            if (arg.IndexOfAny(new[] { ' ', '"', '\'', '&', '|', '<', '>', '^' }) >= 0)
                return "\"" + arg.Replace("\"", "\\\"") + "\"";
            return arg;
        }
    }
}
