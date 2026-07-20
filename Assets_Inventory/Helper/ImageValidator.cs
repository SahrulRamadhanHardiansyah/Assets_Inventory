using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Assets_Inventory.Helper
{
    /// <summary>
    /// Validasi file gambar untuk security hardening wallpaper & attachment.
    /// </summary>
    public static class ImageValidator
    {
        private static readonly string[] AllowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
        private static readonly byte[][] MagicBytes = new[]
        {
            new byte[] { 0xFF, 0xD8, 0xFF }, // JPEG
            new byte[] { 0x89, 0x50, 0x4E, 0x47 }, // PNG
            new byte[] { 0x42, 0x4D }, // BMP
        };

        public static bool IsValidExtension(string filePath)
        {
            string ext = Path.GetExtension(filePath)?.ToLower() ?? "";
            return AllowedExtensions.Contains(ext);
        }

        public static bool IsValidImageFile(string filePath, long maxSizeBytes = 10 * 1024 * 1024)
        {
            try
            {
                if (!File.Exists(filePath)) return false;

                var fi = new FileInfo(filePath);
                if (fi.Length == 0 || fi.Length > maxSizeBytes) return false;

                if (!IsValidExtension(filePath)) return false;

                // Check magic bytes
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] header = new byte[8];
                    int read = fs.Read(header, 0, 8);
                    if (read < 2) return false;

                    bool magicMatch = MagicBytes.Any(magic =>
                        header.Take(magic.Length).SequenceEqual(magic)
                    );
                    if (!magicMatch) return false;
                }

                // Try load as Bitmap (validates structure)
                using (var bmp = new Bitmap(filePath)) { }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GenerateSafeFileName(string originalPath)
        {
            string ext = Path.GetExtension(originalPath)?.ToLower() ?? ".jpg";
            if (!AllowedExtensions.Contains(ext)) ext = ".jpg";
            return Guid.NewGuid().ToString("N") + ext;
        }
    }
}
