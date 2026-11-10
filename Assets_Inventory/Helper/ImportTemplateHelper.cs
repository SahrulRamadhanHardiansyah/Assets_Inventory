using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.Helper
{
    // Gerbang umum tombol Import: konfirmasi "sudah punya template?", dan jika belum,
    // buat file template .xlsx dengan header + contoh baris sesuai urutan kolom kode import.
    public static class ImportTemplateHelper
    {
        // Kolom = (header, contoh nilai). Urutan HARUS sama dengan row[0], row[1], ... di fungsi import.
        // Return true jika user lanjut memilih file (sudah punya template ATAU setelah menyimpan template).
        public static bool ConfirmOrGenerate(IWin32Window owner, string title, IEnumerable<(string Header, string Example)> columns)
        {
            var cols = columns.ToList();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Apakah Anda SUDAH memiliki file Excel/CSV template impor?");
            sb.AppendLine();
            sb.AppendLine("Urutan kolom data per baris (baris 1 = judul):");
            for (int i = 0; i < cols.Count; i++)
                sb.AppendLine($"  Kolom {i + 1} ({ColumnLetter(i)}): {cols[i].Header}  — contoh: {cols[i].Example}");
            sb.AppendLine();
            sb.AppendLine("Klik [Ya]  : saya sudah punya, langsung pilih file.");
            sb.AppendLine("Klik [Tidak]: buatkan template Excel dulu, lalu pilih file.");

            var jawab = MessageBox.Show(owner, sb.ToString(), title,
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (jawab == DialogResult.Cancel) return false;
            if (jawab == DialogResult.Yes) return true;

            using (var sfd = new SaveFileDialog
            {
                Title = "Simpan Template Excel",
                Filter = "Excel Files|*.xlsx",
                FileName = "template_" + new string(title.Where(char.IsLetterOrDigit).ToArray()).ToLower() + ".xlsx"
            })
            {
                if (sfd.ShowDialog(owner) != DialogResult.OK) return false;

                try
                {
                    Generate(sfd.FileName, cols);
                    if (MessageBox.Show(owner, $"Template berhasil dibuat:\n{sfd.FileName}\n\nBuka file tersebut, isi datanya mulai baris ke-2, lalu klik [Yes] untuk melanjutkan impor.",
                        "Template Dibuat", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                        return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(owner, "Gagal membuat template: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private static void Generate(string path, List<(string Header, string Example)> cols)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pkg = new ExcelPackage())
            {
                var ws = pkg.Workbook.Worksheets.Add("Template");
                for (int i = 0; i < cols.Count; i++)
                {
                    int c = i + 1;
                    ws.Cells[1, c].Value = cols[i].Header;
                    ws.Cells[2, c].Value = cols[i].Example;
                }
                using (var hdr = ws.Cells[1, 1, 1, cols.Count])
                {
                    hdr.Style.Font.Bold = true;
                    hdr.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    hdr.Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);
                }
                ws.Cells[2, 1, 2, cols.Count].Style.Font.Italic = true;
                ws.Row(1).Height = 20;
                ws.Cells.AutoFitColumns(8, 45);
                File.WriteAllBytes(path, pkg.GetAsByteArray());
            }
        }

        // 0 -> A, 1 -> B, ... 26 -> AA
        private static string ColumnLetter(int index)
        {
            string s = "";
            index++;
            while (index > 0)
            {
                index--;
                s = (char)('A' + index % 26) + s;
                index /= 26;
            }
            return s;
        }
    }
}
