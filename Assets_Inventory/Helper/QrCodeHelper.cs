using Assets_Inventory.Models;
using System.Drawing;
using ZXing;
using ZXing.Common;

namespace Assets_Inventory.Helper
{
    public static class QrCodeHelper
    {
        public static Image GenerateQrCode(string content, int width = 200, int height = 200)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions { Width = width, Height = height, Margin = 1 }
            };
            return writer.Write(content ?? "");
        }

        public static Image GenerateBarcode128(string content, int width = 300, int height = 100)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions { Width = width, Height = height, Margin = 2 }
            };
            return writer.Write(content ?? "");
        }

        public static Image GenerateLabelImage(Aset aset, string kodeInventaris)
        {
            const int w = 400;
            const int h = 350;
            var label = new Bitmap(w, h);
            string nama = kodeInventaris;
            try
            {
                if (aset?.IdMasterBarangNavigation != null)
                    nama = aset.IdMasterBarangNavigation.NamaBarang ?? kodeInventaris;
                else if (!string.IsNullOrEmpty(aset?.KodeInventaris))
                    nama = aset.KodeInventaris;
            }
            catch { }

            string qrContent = kodeInventaris ?? "";
            Image qr = GenerateQrCode(qrContent, 200, 200);

            using (var g = Graphics.FromImage(label))
            {
                g.Clear(Color.White);
                // QR centered
                g.DrawImage(qr, (w - 200) / 2, 10, 200, 200);

                using (var f1 = new Font("Arial", 10, FontStyle.Bold))
                using (var f2 = new Font("Arial", 8))
                using (var br = new SolidBrush(Color.Black))
                {
                    var sf = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
                    g.DrawString(kodeInventaris, f1, br, new RectangleF(0, 220, w, 25), sf);
                    g.DrawString(nama, f2, br, new RectangleF(5, 250, w - 10, 90), sf);
                }
            }
            return label;
        }
    }
}
