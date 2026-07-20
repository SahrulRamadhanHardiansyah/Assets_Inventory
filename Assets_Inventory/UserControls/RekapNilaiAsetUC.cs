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
    public partial class RekapNilaiAsetUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<RekapNilaiAsetViewModel> printData = new List<RekapNilaiAsetViewModel>();
        int currentPrintIndex = 0;

        public class RekapNilaiAsetViewModel
        {
            public int No { get; set; }
            public string JenisAset { get; set; }
            public int JumlahUnit { get; set; }
            public decimal TotalNilai { get; set; }
        }

        public RekapNilaiAsetUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += RekapNilaiAsetUC_Load;

            if (this.Controls.Find("btnTampilkan", true).FirstOrDefault() is Button btnTampil)
                btnTampil.Click += BtnTampilkanData_Click;
            if (this.Controls.Find("btnPreview", true).FirstOrDefault() is Button btnPrev)
                btnPrev.Click += BtnPreview_Click;
            if (this.Controls.Find("btnExport", true).FirstOrDefault() is Button btnExport)
                btnExport.Click += BtnExportToExcel_Click;
            if (this.Controls.Find("btnTutup", true).FirstOrDefault() is Button btnTutup)
                btnTutup.Click += BtnTutup_Click;

            printDoc.BeginPrint += PrintDoc_BeginPrint;
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void RekapNilaiAsetUC_Load(object sender, EventArgs e)
        {
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        }
        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            // ponytail: reuse context, don't Dispose-recreate
            if (db == null) db = new AppDbContext();
            printData = new List<RekapNilaiAsetViewModel>();

            // DB-level aggregation: Count + Sum without loading entities
            var qAsetAktif = db.Aset.AsNoTracking().Where(a => a.Status != "Nonaktif");
            var qAsetNonaktif = db.Aset.AsNoTracking().Where(a => a.Status == "Nonaktif");
            var qBangunanAktif = db.AsetBangunan.AsNoTracking().Where(b => b.Status != "Nonaktif");
            var qBangunanNonaktif = db.AsetBangunan.AsNoTracking().Where(b => b.Status == "Nonaktif");
            var qTanahAktif = db.AsetTanah.AsNoTracking().Where(t => t.Status != "Nonaktif");
            var qTanahNonaktif = db.AsetTanah.AsNoTracking().Where(t => t.Status == "Nonaktif");

            int jmlBarang = qAsetAktif.Count();
            decimal nilaiBarang = qAsetAktif.Sum(a => (decimal?)(a.HargaSatuan ?? 0m)) ?? 0m;
            printData.Add(new RekapNilaiAsetViewModel { No = 1, JenisAset = "Barang Inventaris (Aktif)", JumlahUnit = jmlBarang, TotalNilai = nilaiBarang });

            int jmlBangunan = qBangunanAktif.Count();
            decimal nilaiBangunan = qBangunanAktif.Sum(b => (decimal?)(b.NilaiAset ?? 0m)) ?? 0m;
            printData.Add(new RekapNilaiAsetViewModel { No = 2, JenisAset = "Bangunan / Gedung (Aktif)", JumlahUnit = jmlBangunan, TotalNilai = nilaiBangunan });

            int jmlTanah = qTanahAktif.Count();
            decimal nilaiTanah = qTanahAktif.Sum(t => (decimal?)(t.NilaiAset ?? 0m)) ?? 0m;
            printData.Add(new RekapNilaiAsetViewModel { No = 3, JenisAset = "Tanah (Aktif)", JumlahUnit = jmlTanah, TotalNilai = nilaiTanah });

            int jmlBarangNA = qAsetNonaktif.Count();
            decimal nilaiBarangNA = qAsetNonaktif.Sum(a => (decimal?)(a.HargaSatuan ?? 0m)) ?? 0m;
            printData.Add(new RekapNilaiAsetViewModel { No = 4, JenisAset = "Barang Inventaris (Non Aktif)", JumlahUnit = jmlBarangNA, TotalNilai = nilaiBarangNA });

            int jmlBangunanNA = qBangunanNonaktif.Count();
            decimal nilaiBangunanNA = qBangunanNonaktif.Sum(b => (decimal?)(b.NilaiAset ?? 0m)) ?? 0m;
            printData.Add(new RekapNilaiAsetViewModel { No = 5, JenisAset = "Bangunan / Gedung (Non Aktif)", JumlahUnit = jmlBangunanNA, TotalNilai = nilaiBangunanNA });

            int jmlTanahNA = qTanahNonaktif.Count();
            decimal nilaiTanahNA = qTanahNonaktif.Sum(t => (decimal?)(t.NilaiAset ?? 0m)) ?? 0m;
            printData.Add(new RekapNilaiAsetViewModel { No = 6, JenisAset = "Tanah (Non Aktif)", JumlahUnit = jmlTanahNA, TotalNilai = nilaiTanahNA });

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblRec = this.Controls.Find("lblRecord", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<RekapNilaiAsetViewModel>(printData);
                if (dgv.Columns["TotalNilai"] != null)
                {
                    dgv.Columns["TotalNilai"].DefaultCellStyle.Format = "C2";
                    dgv.Columns["TotalNilai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            if (lblRec != null) lblRec.Text = $"Total Nilai Aset Keseluruhan: {printData.Sum(x => x.TotalNilai).ToString("C2")}";
        }


        #region CETAK DOKUMEN (PRINT PREVIEW)

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (printData.Count == 0) { MessageBox.Show("Tidak ada data untuk dicetak. Silakan tampilkan data terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
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

            g.DrawString("REKAP NILAI ASET", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString($"TAHUN {DateTime.Now.Year}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos + 20, pageWidth, 20), formatCenter);
            yPos += 50;

            string[] headers = { "No", "Jenis Aset", "Jumlah Unit", "Total Nilai Aset" };
            int[] colWidths = { 40, 350, 100, 200 };
            int tableWidth = colWidths.Sum(); int startX = margin + ((pageWidth - tableWidth) / 2); int currentX = startX;

            Font fontTableHead = new Font("Arial", 10, FontStyle.Bold); Font fontTableData = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush headerBrush = new SolidBrush(Color.FromArgb(143, 188, 143));
            g.FillRectangle(headerBrush, startX, yPos, tableWidth, 35); g.DrawRectangle(Pens.Black, startX, yPos, tableWidth, 35);
            StringFormat formatMidCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat formatMidLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            StringFormat formatMidRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

            for (int i = 0; i < headers.Length; i++)
            {
                RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], 35);
                g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], 35);
                g.DrawString(headers[i], fontTableHead, Brushes.Black, cellRect, formatMidCenter);
                currentX += colWidths[i];
            }
            yPos += 35; int rowHeight = 35;

            while (currentPrintIndex < printData.Count)
            {
                var item = printData[currentPrintIndex]; currentX = startX;
                string[] rowData = { item.No.ToString(), item.JenisAset, item.JumlahUnit.ToString("N0"), item.TotalNilai.ToString("N0") };
                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);
                    StringFormat fmt = formatMidLeft;
                    if (i == 0 || i == 2) fmt = formatMidCenter;
                    else if (i == 3) fmt = formatMidRight;
                    if (fmt == formatMidLeft) cellRect.X += 5;
                    if (fmt == formatMidRight) cellRect.Width -= 5;
                    g.DrawString(rowData[i], fontTableData, Brushes.Black, cellRect, fmt);
                    currentX += colWidths[i];
                }
                yPos += rowHeight; currentPrintIndex++;
            }

            // Total row
            currentX = startX;
            int sumWidth = colWidths.Take(3).Sum();
            g.FillRectangle(headerBrush, startX, yPos, tableWidth, rowHeight);
            g.DrawRectangle(Pens.Black, currentX, yPos, sumWidth, rowHeight);
            g.DrawRectangle(Pens.Black, currentX + sumWidth, yPos, colWidths[3], rowHeight);
            g.DrawString("TOTAL KESELURUHAN", fontTableHead, Brushes.Black, new RectangleF(currentX, yPos, sumWidth, rowHeight), formatMidCenter);
            RectangleF totValRect = new RectangleF(currentX + sumWidth, yPos, colWidths[3] - 5, rowHeight);
            g.DrawString(printData.Sum(x => x.TotalNilai).ToString("N0"), fontTableHead, Brushes.Black, totValRect, formatMidRight);
            yPos += rowHeight;

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
            if (printData.Count == 0) { MessageBox.Show("Tidak ada data untuk diexport.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView ?? this.Controls.Find("dgvLaporan", true).FirstOrDefault() as DataGridView;
                if (dgv == null) { MessageBox.Show("Grid tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (ExportHelper.ShowSaveDialog(out string path, "laporan"))
                {
                    ExportHelper.ExportDataGridView(dgv, path);
                    try { AuditHelper.Log("laporan", System.IO.Path.GetFileName(path), "EXPORT", (object)null, (object)($"Exported {printData.Count} rows"), "Laporan"); } catch {}
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
