using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assets_Inventory.Helper
{
    /// <summary>
    /// Unified export: EPPlus xlsx if possible, else CSV fallback.
    /// </summary>
    public static class ExportHelper
    {
        public static void ExportDataGridView(DataGridView dg, string filePath)
        {
            if (dg == null || dg.Rows.Count == 0) throw new InvalidOperationException("Tidak ada data untuk diekspor.");
            var ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".xlsx") { if (!TryExportEpplus(dg, filePath)) ExportCsv(dg, filePath); }
            else ExportCsv(dg, filePath);
        }

        public static bool TryExportEpplus(DataGridView dg, string filePath)
        {
            try
            {
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var pkg = new OfficeOpenXml.ExcelPackage())
                {
                    var ws = pkg.Workbook.Worksheets.Add("Sheet1");
                    int colIdx = 1;
                    var visibleCols = dg.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
                    foreach (var col in visibleCols) { ws.Cells[1, colIdx].Value = col.HeaderText; colIdx++; }
                    using (var rng = ws.Cells[1, 1, 1, visibleCols.Count])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSteelBlue);
                    }
                    int r = 2;
                    foreach (DataGridViewRow row in dg.Rows)
                    {
                        if (row.IsNewRow) continue;
                        int c = 1;
                        foreach (var col in visibleCols)
                        {
                            ws.Cells[r, c].Value = row.Cells[col.Index].Value ?? row.Cells[col.Index].FormattedValue;
                            c++;
                        }
                        r++;
                    }
                    if (ws.Dimension != null) ws.Cells[ws.Dimension.Address].AutoFitColumns();
                    pkg.SaveAs(new FileInfo(filePath));
                }
                return true;
            }
            catch { return false; }
        }

        public static void ExportCsv(DataGridView dg, string filePath)
        {
            var sb = new StringBuilder();
            var visibleCols = dg.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
            sb.AppendLine(string.Join(",", visibleCols.Select(c => Escape(c.HeaderText))));
            foreach (DataGridViewRow row in dg.Rows)
            {
                if (row.IsNewRow) continue;
                sb.AppendLine(string.Join(",", visibleCols.Select(c => Escape(row.Cells[c.Index].FormattedValue?.ToString() ?? row.Cells[c.Index].Value?.ToString() ?? ""))));
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        public static void ExportList<T>(IEnumerable<T> data, string filePath, Dictionary<string, Func<T, object>> columns = null)
        {
            var sb = new StringBuilder();
            var props = typeof(T).GetProperties().Where(p => p.CanRead).ToList();
            if (columns != null) sb.AppendLine(string.Join(",", columns.Keys.Select(Escape)));
            else sb.AppendLine(string.Join(",", props.Select(p => Escape(p.Name))));
            foreach (var item in data)
            {
                if (columns != null)
                {
                    sb.AppendLine(string.Join(",", columns.Values.Select(fn => { try { return Escape(fn(item)?.ToString() ?? ""); } catch { return ""; } })));
                }
                else
                {
                    sb.AppendLine(string.Join(",", props.Select(p => { try { return Escape(p.GetValue(item)?.ToString() ?? ""); } catch { return ""; } })));
                }
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string Escape(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            return text;
        }

        public static bool ShowSaveDialog(out string filePath, string defaultName = "laporan")
        {
            filePath = null;
            using (var sfd = new SaveFileDialog { Filter = "Excel Files|*.xlsx|CSV Files|*.csv", FileName = defaultName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx", Title = "Simpan Laporan" })
            {
                if (sfd.ShowDialog() == DialogResult.OK) { filePath = sfd.FileName; return true; }
            }
            return false;
        }
    }
}
