using System;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Assets_Inventory.Helper
{
    /// <summary>
    /// DPAPI-based protector for connection strings + validation helpers.
    /// Enkripsi transparan: saat simpan -> Protect, saat baca -> Unprotect.
    /// Preservasi fungsionalitas KoneksiDbForm tetap jalan.
    /// </summary>
    public static class ConnectionStringProtector
    {
        private const string Entropy = "Assets_Inventory_2026_Sahrul_SecureKey_v1";
        private const string Prefix = "DPAPI:";

        // Valid identifier: huruf, angka, underscore, dash, dot
        private static readonly Regex SafeHostRegex = new Regex(@"^[a-zA-Z0-9_.-]+$", RegexOptions.Compiled);
        private static readonly Regex SafeIdentifierRegex = new Regex(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled);
        private static readonly Regex SafeTableRegex = new Regex(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled);

        public static bool IsSafeHost(string host) => !string.IsNullOrWhiteSpace(host) && SafeHostRegex.IsMatch(host);
        public static bool IsSafeIdentifier(string name) => !string.IsNullOrWhiteSpace(name) && SafeIdentifierRegex.IsMatch(name);
        public static bool IsSafeTableName(string table) => !string.IsNullOrWhiteSpace(table) && SafeTableRegex.IsMatch(table);

        public static bool IsSafePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;
            try
            {
                string full = Path.GetFullPath(path);
                // Reject paths with shell metacharacters that could break if ever logged
                char[] forbidden = new[] { '<', '>', '|', '&', '^' };
                return path.IndexOfAny(forbidden) < 0 && full.Length > 0;
            }
            catch { return false; }
        }

        public static bool IsValidPort(string portStr, out int port)
        {
            port = 0;
            if (!int.TryParse(portStr, out port)) return false;
            return port >= 1 && port <= 65535;
        }

        /// <summary>
        /// Encrypt plaintext with DPAPI CurrentUser scope.
        /// </summary>
        public static string Protect(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] entropyBytes = Encoding.UTF8.GetBytes(Entropy);
                byte[] encrypted = ProtectedData.Protect(plainBytes, entropyBytes, DataProtectionScope.CurrentUser);
                return Prefix + Convert.ToBase64String(encrypted);
            }
            catch
            {
                // Fallback: return as-is but caller should handle
                return plainText;
            }
        }

        /// <summary>
        /// Decrypt DPAPI string. If not encrypted (no prefix), return as-is for backward compat.
        /// </summary>
        public static string Unprotect(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;
            if (!cipherText.StartsWith(Prefix)) return cipherText; // plaintext legacy
            try
            {
                string b64 = cipherText.Substring(Prefix.Length);
                byte[] encrypted = Convert.FromBase64String(b64);
                byte[] entropyBytes = Encoding.UTF8.GetBytes(Entropy);
                byte[] plain = ProtectedData.Unprotect(encrypted, entropyBytes, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(plain);
            }
            catch
            {
                // If decryption fails (e.g. different user), treat as plaintext legacy
                return cipherText.StartsWith(Prefix) ? null : cipherText;
            }
        }

        /// <summary>
        /// Get decrypted connection string from config, with fallback to plaintext legacy.
        /// </summary>
        public static string GetDecryptedConnectionString()
        {
            try
            {
                var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
                if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
                    return null;
                string raw = settings.ConnectionString;
                string maybeDecrypted = Unprotect(raw);
                // Unprotect returns null only if DPAPI-prefixed but failed to decrypt
                if (maybeDecrypted == null) return raw; // will fail open but caller handles
                return maybeDecrypted;
            }
            catch
            {
                var settings = ConfigurationManager.ConnectionStrings["KoneksiServer"];
                return settings?.ConnectionString;
            }
        }

        /// <summary>
        /// Build validated MySql connection string using DbConnectionStringBuilder (no manual concat).
        /// Returns null if validation fails.
        /// </summary>
        public static string BuildValidatedMySqlConnectionString(string host, string port, string database, string uid, string pwd, out string error)
        {
            error = null;
            if (!IsSafeHost(host)) { error = "Host tidak valid (hanya huruf, angka, dot, dash, underscore)."; return null; }
            if (!IsValidPort(port, out int portNum)) { error = "Port harus angka 1-65535."; return null; }
            if (!IsSafeIdentifier(database)) { error = "Database name tidak valid."; return null; }
            if (!IsSafeIdentifier(uid)) { error = "Uid tidak valid."; return null; }
            if (string.IsNullOrWhiteSpace(pwd)) { /* allow empty */ }

            // Use builder to avoid injection via ; =
            var builder = new DbConnectionStringBuilder();
            builder["Server"] = host.Trim();
            builder["Port"] = portNum.ToString();
            builder["Database"] = database.Trim();
            builder["Uid"] = uid.Trim();
            builder["Pwd"] = pwd ?? "";
            // Enforce safe defaults
            builder["AllowLoadLocalInfile"] = "false";
            builder["AllowUserVariables"] = "false";
            return builder.ConnectionString;
        }

        /// <summary>
        /// Validate backup folder/file paths for shell safety.
        /// </summary>
        public static void ValidateBackupFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder backup tidak boleh kosong.");
            if (!IsSafePath(folderPath))
                throw new ArgumentException("Path folder mengandung karakter tidak aman.");
            string full = Path.GetFullPath(folderPath);
            if (!Directory.Exists(full))
                Directory.CreateDirectory(full);
        }

        /// <summary>
        /// Validate list table names whitelisted.
        /// </summary>
        public static void ValidateTableNames(System.Collections.Generic.IEnumerable<string> tables)
        {
            foreach (var t in tables)
            {
                if (!IsSafeTableName(t))
                    throw new ArgumentException($"Nama tabel tidak valid: {t}");
            }
        }
    }
}
