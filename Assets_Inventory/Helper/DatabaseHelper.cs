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
            var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
            if (settings == null) return;

            var builder = new DbConnectionStringBuilder();
            builder.ConnectionString = settings.ConnectionString;

            builder.TryGetValue("Server", out object host);
            builder.TryGetValue("Database", out object dbName);
            builder.TryGetValue("Uid", out object user);
            builder.TryGetValue("Pwd", out object pass);
            builder.TryGetValue("Port", out object port);

            string fileName = $"AutoBackup_{dbName}_{DateTime.Now:yyyyMMdd_HHmm}.sql";
            string fullPath = Path.Combine(folderPath, fileName);

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string mysqlDumpPath = Path.Combine(appFolder, "Resources", "mysqldump.exe");

            if (!File.Exists(mysqlDumpPath))
            {
                throw new Exception($"File mysqldump.exe tidak ditemukan di: {mysqlDumpPath}");
            }

            string dumpCommand = $"\"{mysqlDumpPath}\" -h {host} -P {port} -u {user} ";
            if (!string.IsNullOrEmpty(pass?.ToString())) dumpCommand += $"-p\"{pass}\" ";
            dumpCommand += $"{dbName} > \"{fullPath}\"";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {dumpCommand}",
                CreateNoWindow = true,
                UseShellExecute = false
            };

            using (Process p = Process.Start(psi))
            {
                p.WaitForExit();
                if (p.ExitCode != 0) throw new Exception("Gagal menjalankan perintah backup.");
            }
        }
    }
}