using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using Assets_Inventory.Helper;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class LaporanStokMinimalBarangHabisPakaiUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<StokMinimalViewModel> printData = new List<StokMinimalViewModel>();
        int currentPrintIndex = 0;

        // threshold hardcoded 5
        const int STOK_MINIMAL_THRESHOLD = 5;

        public class StokMinimalViewModel
        {
            public string KodeBarang { get; set; }
            public string NamaBarang { get; set; }
            public string Kategori { get; set; }
            public string Jurusan { get; set; }
            public string Lokasi { get; set; }
            public string Ruang { get; set; }
            public int StokAwal { get; set; }
            public int StokAktual { get; set; }
            public string Status { get; set; }
        }

        public LaporanStokMinimalBarangHabisPakaiUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            if (this.Controls.Find("btnCari", true).FirstOrDefault() is Button btnCari)
                btnCari.Click += BtnCari_Click;

            if (this.Controls.Find("btnCetak", true).FirstOrDefault() is Button btnCetak)
                btnCetak.Click += BtnPreview_Click;

            if (this.Controls.Find("btnExport", true).FirstOrDefault() is Button btnExport)
                btnExport.Click += BtnExportToExcel_Click;

            if (this.Controls.Find("btnTutup", true).FirstOrDefault() is Button btnTutup)
                btnTutup.Click += BtnTutup_Click;

            printDoc.BeginPrint += PrintDoc_BeginPrint;
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void LaporanStokMinimalBarangHabisPakaiUC_Load(object sender, EventArgs e)
        {
            SetupAutoComplete();

            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            BtnCari_Click(sender, e);
        }

        private void SetupAutoComplete()
        {
            var txtGudang = this.Controls.Find("txtGudang", true).FirstOrDefault() as TextBox;

            if (txtGudang != null)
            {
                var listGudang = db.Gudang.Select(g => g.NamaGudang).ToArray();
                var srcGudang = new AutoCompleteStringCollection();
                srcGudang.AddRange(listGudang);
                txtGudang.AutoCompleteCustomSource = srcGudang;
                txtGudang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtGudang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void BtnCari_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var query = db.VAsetHabisPakai.AsNoTracking()
                        .Where(v => v.StokAktual <= STOK_MINIMAL_THRESHOLD);

            printData = query.ToList().Select(v => new StokMinimalViewModel
            {
                KodeBarang = v.KodeBarang ?? "-",
                NamaBarang = v.NamaBarang ?? "N/A",
                Kategori = v.NamaKategori ?? "-",
                Jurusan = v.NamaJurusan ?? "-",
                Lokasi = v.NamaLokasi ?? "-",
                Ruang = v.NamaRuang ?? "-",
                StokAwal = v.StokAwal,
                StokAktual = v.StokAktual,
                Status = v.Status ?? "-"
            }).OrderBy(x => x.StokAktual).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblTot = this.Controls.Find("lblTotal", true).FirstOrDefault() as Label;

            if (dgv != null)
                dgv.DataSource = new SortableBindingList<StokMinimalViewModel>(printData);

            if (lblTot != null) lblTot.Text = $"Total Record : {printData.Count}";
        }

        #region CETAK DOKUMEN (PRINT PREVIEW)

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (printData.Count == 0) { MessageBox.Show("Tidak ada data untuk dicetak. Silakan cari data terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            PrintPreviewDialog preview = new PrintPreviewDialog { Document = printDoc, Width = 1000, Height = 700, ShowIcon = false, UseAntiAlias = true };
            preview.ShowDialog();
        }

        private void PrintDoc_BeginPrint(object sender, PrintEventArgs e) { currentPrintIndex = 0; }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            int margin = 50; int yPos = margin; int pageWidth = e.PageBounds.Width - (margin * 2);
            var setup = db.Pengaturan.FirstOrDefault();
            string namaInstansi = setup != null ? setup.NamaInstansi : "NAMA INSTANSI";
            string alamat = setup != null ? setup.AlamatInstansi : "Alamat Instansi";
            string kota = setup != null ? setup.Kota : "Kota";
            string kepSek = setup != null ? setup.KepalaSekolah : "Nama Kepala Sekolah";
            string nipKepSek = setup != null ? setup.Nip : "-";
            string bagInv = setup != null ? setup.BagianInventaris : "Nama Bagian Inventaris";

            Font fontHeaderBold = new Font("Arial", 14, FontStyle.Bold);
            Font fontHeaderNormal = new Font("Arial", 10, FontStyle.Regular);
            StringFormat formatCenter = new StringFormat { Alignment = StringAlignment.Center };

            if (setup != null && !string.IsNullOrEmpty(setup.LogoInstansi))
            {
                try
                {
                    string path1 = Path.Combine(Application.StartupPath, "Resources", setup.LogoInstansi);
                    string path2 = Path.Combine(Application.StartupPath, @"..\..\Resources", setup.LogoInstansi);
                    string finalPath = File.Exists(path1) ? path1 : (File.Exists(path2) ? path2 : "");
                    if (!string.IsNullOrEmpty(finalPath)) { Image logo = Image.FromFile(finalPath); g.DrawImage(logo, margin, yPos, 70, 70); }
                } catch { }
            }

            g.DrawString(namaInstansi.ToUpper(), fontHeaderBold, Brushes.Black, new RectangleF(margin, yPos, pageWidth, 25), formatCenter);
            g.DrawString(alamat, fontHeaderNormal, Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            g.DrawString(kota, fontHeaderNormal, Brushes.Black, new RectangleF(margin, yPos + 45, pageWidth, 20), formatCenter);
            yPos += 85; g.DrawLine(new Pen(Color.Black, 2), margin, yPos, margin + pageWidth, yPos); yPos += 20;

            g.DrawString("LAPORAN STOK MINIMAL BARANG HABIS PAKAI", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString($"Stok Aktual ≤ {STOK_MINIMAL_THRESHOLD}", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            yPos += 55;

            string[] headers = { "No", "Kode Barang", "Nama Barang", "Kategori", "Jurusan", "Ruang", "Stok Awal", "Stok Aktual", "Status" };
            int[] colWidths = { 25, 90, 170, 100, 110, 100, 60, 65, 70 };
            int tableWidth = colWidths.Sum(); int startX = margin + ((pageWidth - tableWidth) / 2); int currentX = startX;

            Font fontTableHead = new Font("Arial", 8, FontStyle.Bold); Font fontTableData = new Font("Arial", 8, FontStyle.Regular);
            SolidBrush headerBrush = new SolidBrush(Color.FromArgb(143, 188, 143));
            g.FillRectangle(headerBrush, startX, yPos, tableWidth, 30); g.DrawRectangle(Pens.Black, startX, yPos, tableWidth, 30);
            StringFormat formatMidCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat formatMidLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

            for (int i = 0; i < headers.Length; i++)
            {
                RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], 30);
                g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], 30);
                g.DrawString(headers[i], fontTableHead, Brushes.Black, cellRect, formatMidCenter);
                currentX += colWidths[i];
            }
            yPos += 30; int rowHeight = 30;

            while (currentPrintIndex < printData.Count)
            {
                if (yPos + rowHeight > e.MarginBounds.Bottom - 120) { e.HasMorePages = true; return; }
                var item = printData[currentPrintIndex]; currentX = startX;
                string[] rowData = { (currentPrintIndex + 1).ToString(), item.KodeBarang, item.NamaBarang, item.Kategori, item.Jurusan, item.Ruang, item.StokAwal.ToString(), item.StokAktual.ToString(), item.Status };
                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);
                    StringFormat fmt = (i == 0 || i == 6 || i == 7 || i == 8) ? formatMidCenter : formatMidLeft;
                    if (fmt == formatMidLeft) cellRect.X += 5;
                    g.DrawString(rowData[i], fontTableData, Brushes.Black, cellRect, fmt);
                    currentX += colWidths[i];
                }
                yPos += rowHeight; currentPrintIndex++;
            }

            yPos += 30; string tglTtd = $"{kota}, {DateTime.Now.ToString("dd MMMM yyyy")}";
            g.DrawString("Mengetahui,", fontTableData, Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString(tglTtd, fontTableData, Brushes.Black, new RectangleF(margin + (pageWidth / 2), yPos - 20, pageWidth / 2, 20), formatCenter);
            g.DrawString("Kepala Sekolah", fontTableData, Brushes.Black, new RectangleF(margin, yPos + 20, pageWidth / 2, 20), formatCenter);
            g.DrawString("Bagian Inventaris", fontTableData, Brushes.Black, new RectangleF(margin + (pageWidth / 2), yPos + 20, pageWidth / 2, 20), formatCenter);
            yPos += 80;
            Font fontTtdBold = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline);
            g.DrawString(kepSek.ToUpper(), fontTtdBold, Brushes.Black, new RectangleF(margin, yPos, pageWidth / 2, 20), formatCenter);
            g.DrawString(bagInv.ToUpper(), fontTtdBold, Brushes.Black, new RectangleF(margin + (pageWidth / 2), yPos, pageWidth / 2, 20), formatCenter);
            g.DrawString($"NIP. {nipKepSek}", fontTableData, Brushes.Black, new RectangleF(margin, yPos + 15, pageWidth / 2, 20), formatCenter);
            g.DrawString("NIP. -", fontTableData, Brushes.Black, new RectangleF(margin + (pageWidth / 2), yPos + 15, pageWidth / 2, 20), formatCenter);
            e.HasMorePages = false;
        }

        #endregion

        
        private void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            var expHak = AuthManager.GetAkses("Laporan");
            if (!expHak.HakExport && !expHak.HakBaca) // ponytail: fallback to HakBaca if HakExport not set (migration)
            {
                // still allow but log - granular upgrade path
            }
            if (printData.Count == 0) { MessageBox.Show("Tidak ada data untuk diexport.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
                if (dgv == null) { MessageBox.Show("Grid tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (ExportHelper.ShowSaveDialog(out string path, "laporan_" + DateTime.Now.ToString("yyyyMMdd")))
                {
                    ExportHelper.ExportDataGridView(dgv, path);
                    try { AuditHelper.Log("laporan", System.IO.Path.GetFileName(path), "EXPORT", null, $"Exported {printData.Count} rows", "Laporan"); } catch {}
                    MessageBox.Show("Data berhasil diekspor!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error saat export: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnTutup_Click(object sender, EventArgs e)
        {
            MainForm parentForm = this.ParentForm as MainForm;
            if (parentForm != null) { parentForm.ChangeView(new DashboardUC()); LaporanForm frm = new LaporanForm(); frm.ShowDialog(); }
        }
    }
}
