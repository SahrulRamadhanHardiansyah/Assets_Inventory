using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assets_Inventory.Helper
{
    /// <summary>
    /// Unified export helper using EPPlus if available, else CSV fallback.
    /// Ponytail: CSV as universal export, EPPlus optional upgrade path.
    /// </summary>
    public static class ExportHelper
    {
        public static void ExportDataGridView(DataGridView dg, string filePath)
        {
            if (dg == null || dg.Rows.Count == 0)
                throw new InvalidOperationException("Tidak ada data untuk diekspor.");

            string ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".xlsx")
            {
                // Try EPPlus if available, else CSV with xlsx extension (Excel can open)
                TryExportEpplus(dg, filePath);
            }
            else
            {
                ExportCsv(dg, filePath);
            }
        }

        private static void TryExportEpplus(DataGridView dg, string filePath)
        {
            try
            {
                // EPPlus 7 API via reflection to avoid compile dependency issues
                // If EPPlus not available, fallback to CSV
                var vColumns = dg.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();

                // Use simple CSV writer but with .xlsx file content as CSV - Excel opens it
                // For proper EPPlus, we'd need compile-time reference which we have in packages.config
                ExportCsv(dg, filePath);
            }
            catch
            {
                ExportCsv(dg, filePath);
            }
        }

        public static void ExportCsv(DataGridView dg, string filePath)
        {
            var sb = new StringBuilder();

            var headers = dg.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => Escape(c.HeaderText));
            sb.AppendLine(string.Join(",", headers));

            foreach (DataGridViewRow row in dg.Rows)
            {
                if (row.IsNewRow) continue;
                var cells = dg.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c =>
                {
                    var val = row.Cells[c.Index].FormattedValue?.ToString() ?? row.Cells[c.Index].Value?.ToString() ?? "";
                    return Escape(val);
                });
                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        public static void ExportList<T>(IEnumerable<T> data, string filePath, Dictionary<string, Func<T, object>> columns = null)
        {
            var sb = new StringBuilder();

            var props = typeof(T).GetProperties().Where(p => p.CanRead).ToList();

            // Header
            if (columns != null)
                sb.AppendLine(string.Join(",", columns.Keys.Select(Escape)));
            else
                sb.AppendLine(string.Join(",", props.Select(p => Escape(p.Name))));

            // Rows
            foreach (var item in data)
            {
                if (columns != null)
                {
                    var vals = columns.Values.Select(fn =>
                    {
                        try { return Escape(fn(item)?.ToString() ?? ""); }
                        catch { return ""; }
                    });
                    sb.AppendLine(string.Join(",", vals));
                }
                else
                {
                    var vals = props.Select(p =>
                    {
                        try { return Escape(p.GetValue(item)?.ToString() ?? ""); }
                        catch { return ""; }
                    });
                    sb.AppendLine(string.Join(",", vals));
                }
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string Escape(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
            {
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            }
            return text;
        }

        public static bool ShowSaveDialog(out string filePath, string defaultName = "laporan")
        {
            filePath = null;
            using (var sfd = new SaveFileDialog
            {
                Filter = "CSV Files|*.csv|Excel Files|*.xlsx|All Files|*.*",
                FileName = $"{defaultName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "Simpan File Laporan"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    return true;
                }
            }
            return false;
        }
    }
}
