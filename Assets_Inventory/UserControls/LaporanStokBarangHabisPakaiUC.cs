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
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class LaporanStokBarangHabisPakaiUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<StokHabisPakaiViewModel> printData = new List<StokHabisPakaiViewModel>();
        int currentPrintIndex = 0;

        public class StokHabisPakaiViewModel
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

        public LaporanStokBarangHabisPakaiUC()
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

            printDoc.BeginPrint += PrintDoc_BeginPrint;
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void LaporanStokBarangHabisPakaiUC_Load(object sender, EventArgs e)
        {
            SetupAutoComplete();

            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            BtnCari_Click(sender, e);
        }

        private void SetupAutoComplete()
        {
            var txtGudang = this.Controls.Find("txtGudang", true).FirstOrDefault() as TextBox;
            var txtBarang = this.Controls.Find("txtBarang", true).FirstOrDefault() as TextBox;

            if (txtGudang != null)
            {
                var listGudang = db.Gudang.Select(g => g.NamaGudang).ToArray();
                var srcGudang = new AutoCompleteStringCollection();
                srcGudang.AddRange(listGudang);
                txtGudang.AutoCompleteCustomSource = srcGudang;
                txtGudang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtGudang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

            if (txtBarang != null)
            {
                var listBarang = db.MasterBarang
                    .Where(m => m.JenisBarang == "Habis Pakai")
                    .Select(b => b.NamaBarang).ToArray();
                var srcBarang = new AutoCompleteStringCollection();
                srcBarang.AddRange(listBarang);
                txtBarang.AutoCompleteCustomSource = srcBarang;
                txtBarang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtBarang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void BtnCari_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var txtBarang = this.Controls.Find("txtBarang", true).FirstOrDefault() as TextBox;
            var txtGudang = this.Controls.Find("txtGudang", true).FirstOrDefault() as TextBox;

            string namaBrg = txtBarang?.Text?.Trim() ?? "";
            string namaGudang = txtGudang?.Text?.Trim() ?? "";

            var query = db.VAsetHabisPakai.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(namaBrg))
                query = query.Where(v => v.NamaBarang.Contains(namaBrg));


            printData = query.ToList().Select(v => new StokHabisPakaiViewModel
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
            }).OrderBy(x => x.NamaBarang).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblTot = this.Controls.Find("lblTotal", true).FirstOrDefault() as Label;

            if (dgv != null)
                dgv.DataSource = new SortableBindingList<StokHabisPakaiViewModel>(printData);

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

            g.DrawString("LAPORAN STOK BARANG HABIS PAKAI", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString($"TAHUN {DateTime.Now.Year}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos + 20, pageWidth, 20), formatCenter);
            yPos += 50;

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
            if (printData.Count == 0) { MessageBox.Show("Tidak ada data untuk diexport.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"; sfd.FileName = "Laporan_Stok_Barang_HabisPakai_" + DateTime.Now.ToString("yyyyMMdd");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("Stok Habis Pakai");
                            ws.Cells["A1:I1"].Merge = true; ws.Cells["A1"].Value = "LAPORAN STOK BARANG HABIS PAKAI";
                            ws.Cells["A1"].Style.Font.Bold = true; ws.Cells["A1"].Style.Font.Size = 14;
                            ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            string[] headers = { "No", "Kode Barang", "Nama Barang", "Kategori", "Jurusan", "Lokasi", "Ruang", "Stok Awal", "Stok Aktual", "Status" };
                            for (int i = 0; i < headers.Length; i++)
                            {
                                ws.Cells[3, i + 1].Value = headers[i]; ws.Cells[3, i + 1].Style.Font.Bold = true;
                                ws.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                ws.Cells[3, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                            int row = 4;
                            for (int i = 0; i < printData.Count; i++)
                            {
                                var item = printData[i];
                                ws.Cells[row, 1].Value = i + 1; ws.Cells[row, 2].Value = item.KodeBarang;
                                ws.Cells[row, 3].Value = item.NamaBarang; ws.Cells[row, 4].Value = item.Kategori;
                                ws.Cells[row, 5].Value = item.Jurusan; ws.Cells[row, 6].Value = item.Lokasi;
                                ws.Cells[row, 7].Value = item.Ruang; ws.Cells[row, 8].Value = item.StokAwal;
                                ws.Cells[row, 9].Value = item.StokAktual; ws.Cells[row, 10].Value = item.Status;
                                for (int col = 1; col <= 10; col++) ws.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                row++;
                            }
                            ws.Cells[ws.Dimension.Address].AutoFitColumns(); package.SaveAs(new FileInfo(sfd.FileName));
                        }
                        MessageBox.Show("Data berhasil diekspor ke Excel!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show("Error saat export: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }
    }
}
