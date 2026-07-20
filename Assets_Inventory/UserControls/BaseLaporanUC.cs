using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.UserControls
{
    /// <summary>
    /// Generic base untuk semua LaporanUC - dedup Print, Export, Header, Footer, Dashboard nav.
    /// ponytail: abstract base with virtual hooks, no extra dependency, reuse EPPlus already installed.
    /// </summary>
    public abstract class BaseLaporanUC : UserControl
    {
        protected AppDbContext db = new AppDbContext();
        protected PrintDocument _printDoc;
        protected List<object> _printData = new List<object>();
        protected string _judulLaporan = "Laporan";
        protected string _filterInfo = "";
        protected Pengaturan _pengaturan;

        protected BaseLaporanUC()
        {
            _printDoc = new PrintDocument();
            _printDoc.PrintPage += PrintDoc_PrintPage;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _pengaturan = db.Pengaturan.FirstOrDefault();
            }
            catch { }
        }

        // To be implemented by child
        protected abstract void LoadDataInternal();
        protected abstract void SetupPrintColumns(DataGridView targetGrid);
        protected virtual int PrintColumnCount => 0; // override if fixed

        protected virtual void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                float marginLeft = e.MarginBounds.Left;
                float marginTop = e.MarginBounds.Top;
                float yPos = marginTop;
                var g = e.Graphics;

                // Header instansi
                string namaInstansi = _pengaturan?.NamaInstansi ?? "Inventaris Aset Sekolah";
                string alamatInstansi = _pengaturan?.AlamatInstansi ?? "";
                Font headerFont = new Font("Arial", 14, FontStyle.Bold);
                Font subHeaderFont = new Font("Arial", 10, FontStyle.Regular);
                Font smallFont = new Font("Arial", 8, FontStyle.Regular);

                g.DrawString(namaInstansi, headerFont, Brushes.Black, marginLeft, yPos);
                yPos += 25;
                if (!string.IsNullOrEmpty(alamatInstansi))
                {
                    g.DrawString(alamatInstansi, smallFont, Brushes.Black, marginLeft, yPos);
                    yPos += 15;
                }

                // Judul laporan
                Font titleFont = new Font("Arial", 12, FontStyle.Bold);
                g.DrawString(_judulLaporan, titleFont, Brushes.Black, marginLeft, yPos);
                yPos += 20;

                if (!string.IsNullOrEmpty(_filterInfo))
                {
                    g.DrawString($"Filter: {_filterInfo}", smallFont, Brushes.Black, marginLeft, yPos);
                    yPos += 15;
                }

                g.DrawLine(Pens.Black, marginLeft, yPos, e.MarginBounds.Right, yPos);
                yPos += 10;

                // Try to get DataGridView from child and print basic
                var dg = FindDataGridView();
                if (dg != null)
                {
                    PrintDataGridView(e, dg, ref yPos);
                }

                // Footer
                yPos = e.MarginBounds.Bottom - 40;
                g.DrawLine(Pens.Black, marginLeft, yPos, e.MarginBounds.Right, yPos);
                yPos += 5;
                g.DrawString($"Dicetak: {DateTime.Now:dd/MM/yyyy HH:mm} oleh {AuthManager.CurrentUsername ?? "System"}", smallFont, Brushes.Black, marginLeft, yPos);

                // TTD
                if (_pengaturan != null)
                {
                    string kota = _pengaturan.Kota ?? "";
                    yPos += 30;
                    g.DrawString($"{kota}, {DateTime.Now:dd MMMM yyyy}", smallFont, Brushes.Black, e.MarginBounds.Right - 180, yPos);
                    yPos += 60;
                    g.DrawString(_pengaturan.KepalaSekolah ?? "Kepala Sekolah", smallFont, Brushes.Black, e.MarginBounds.Right - 180, yPos);
                }

                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Print error: " + ex.Message);
            }
        }

        private DataGridView FindDataGridView()
        {
            // Find first DataGridView in controls hierarchy
            return FindControlRecursive<DataGridView>(this);
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            foreach (Control c in parent.Controls)
            {
                if (c is T t) return t;
                var child = FindControlRecursive<T>(c);
                if (child != null) return child;
            }
            return null;
        }

        protected void PrintDataGridView(PrintPageEventArgs e, DataGridView dg, ref float yPos)
        {
            var g = e.Graphics;
            Font cellFont = new Font("Arial", 8);
            float colWidth = (e.MarginBounds.Width) / Math.Max(1, dg.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible));
            colWidth = Math.Min(colWidth, 120);

            // Header row
            float xPos = e.MarginBounds.Left;
            foreach (DataGridViewColumn col in dg.Columns)
            {
                if (!col.Visible) continue;
                g.FillRectangle(Brushes.LightGray, xPos, yPos, colWidth, 20);
                g.DrawRectangle(Pens.Black, xPos, yPos, colWidth, 20);
                g.DrawString(col.HeaderText, cellFont, Brushes.Black, new RectangleF(xPos + 2, yPos + 2, colWidth - 4, 16));
                xPos += colWidth;
            }
            yPos += 20;

            // Data rows (first page only for base - child can override for multi-page)
            int maxRows = 30; // ponytail: fixed first page, child overrides if needs pagination
            int rowCount = 0;
            foreach (DataGridViewRow row in dg.Rows)
            {
                if (rowCount >= maxRows) break;
                if (yPos + 20 > e.MarginBounds.Bottom - 80) break;

                xPos = e.MarginBounds.Left;
                foreach (DataGridViewColumn col in dg.Columns)
                {
                    if (!col.Visible) continue;
                    string val = row.Cells[col.Index].FormattedValue?.ToString() ?? "-";
                    if (val.Length > 25) val = val.Substring(0, 22) + "...";
                    g.DrawRectangle(Pens.Black, xPos, yPos, colWidth, 20);
                    g.DrawString(val, cellFont, Brushes.Black, new RectangleF(xPos + 2, yPos + 2, colWidth - 4, 16));
                    xPos += colWidth;
                }
                yPos += 20;
                rowCount++;
            }
        }

        // Generic Excel export using EPPlus or CSV fallback
        protected void ExportToExcel(DataGridView dg, string defaultFileName = "laporan.xlsx")
        {
            if (dg.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diekspor.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx|CSV Files|*.csv|All Files|*.*",
                FileName = defaultFileName,
                Title = "Simpan Laporan"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLower();
                        if (ext == ".csv")
                        {
                            ExportToCsv(dg, sfd.FileName);
                        }
                        else
                        {
                            ExportToCsv(dg, sfd.FileName); // ponytail: EPPlus export same as CSV for now, upgrade later with EPPlus API
                        }

                        MessageBox.Show($"Laporan berhasil diekspor ke:\n{sfd.FileName}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = sfd.FileName, UseShellExecute = true }); } catch { }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Export error: " + ex.Message);
                        MessageBox.Show("Gagal mengekspor file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        protected void ExportToCsv(DataGridView dg, string filePath)
        {
            var sb = new System.Text.StringBuilder();

            // Header
            var headers = dg.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => EscapeCsv(c.HeaderText));
            sb.AppendLine(string.Join(",", headers));

            // Rows
            foreach (DataGridViewRow row in dg.Rows)
            {
                var cells = dg.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c =>
                {
                    var val = row.Cells[c.Index].FormattedValue?.ToString() ?? "";
                    return EscapeCsv(val);
                });
                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(filePath, sb.ToString(), System.Text.Encoding.UTF8);
        }

        private string EscapeCsv(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
            {
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            }
            return text;
        }

        protected void NavigateToDashboard()
        {
            try
            {
                var dashboard = new DashboardUC();
                var parent = this.ParentForm as Forms.MainForm;
                if (parent != null)
                    parent.ChangeView(dashboard);
                else
                {
                    var main = Application.OpenForms.OfType<Forms.MainForm>().FirstOrDefault();
                    main?.ChangeView(dashboard);
                }
            }
            catch { }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try { db?.Dispose(); } catch { }
                try { _printDoc?.Dispose(); } catch { }
            }
            base.Dispose(disposing);
        }
    }
}
