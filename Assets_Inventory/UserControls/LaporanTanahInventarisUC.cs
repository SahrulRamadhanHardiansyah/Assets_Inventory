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
    public partial class LaporanTanahInventarisUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanTanahViewModel> printData = new List<LaporanTanahViewModel>();
        int currentPrintIndex = 0;

        public class LaporanTanahViewModel
        {
            public int KodeTanah { get; set; }
            public string NamaPemilik { get; set; }
            public string NomorSertifikat { get; set; }
            public int LuasTanah { get; set; }
            public string LetakTanah { get; set; }
            public string Lokasi { get; set; }
            public string StatusHak { get; set; }
            public decimal NilaiAset { get; set; }
            public string Penggunaan { get; set; }
            public string TanggalPerolehan { get; set; }
            public string SumberPerolehan { get; set; }
            public string Status { get; set; }
        }

        public LaporanTanahInventarisUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += LaporanTanahInventarisUC_Load;

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

        private void LaporanTanahInventarisUC_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        }

        private void LoadComboBoxes()
        {
            var mcbStatus = this.Controls.Find("mcbStatus", true).FirstOrDefault() as ComboBox;
            var rbSemua = this.Controls.Find("rbSemua", true).FirstOrDefault() as RadioButton;

            if (mcbStatus != null)
            {
                mcbStatus.Items.Clear();
                mcbStatus.Items.AddRange(new object[] { "Aktif", "Nonaktif" });
                mcbStatus.SelectedIndex = 0;
            }

            if (rbSemua != null) rbSemua.Checked = true;
        }

        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var rbStatus = this.Controls.Find("rbStatus", true).FirstOrDefault() as RadioButton;
            var mcbStatus = this.Controls.Find("mcbStatus", true).FirstOrDefault() as ComboBox;

            var query = db.VAsetTanah.AsNoTracking().AsQueryable();

            if (rbStatus != null && rbStatus.Checked && mcbStatus != null && mcbStatus.SelectedItem != null)
            {
                string statusFilter = mcbStatus.SelectedItem.ToString();
                query = query.Where(v => v.Status == statusFilter);
            }

            printData = query.ToList().Select(v => new LaporanTanahViewModel
            {
                KodeTanah = v.KodeTanah,
                NamaPemilik = v.NamaPemilik ?? "-",
                NomorSertifikat = v.NomorSertifikat ?? "-",
                LuasTanah = v.LuasTanah,
                LetakTanah = v.LetakTanah ?? "-",
                Lokasi = v.NamaLokasi ?? "-",
                StatusHak = v.StatusHak ?? "-",
                NilaiAset = v.NilaiAset ?? 0m,
                Penggunaan = v.Penggunaan ?? "-",
                TanggalPerolehan = v.TanggalPerolehan?.ToString("dd-MM-yyyy") ?? "-",
                SumberPerolehan = v.SumberPerolehan ?? "-",
                Status = v.Status ?? "-"
            }).OrderBy(x => x.KodeTanah).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblRec = this.Controls.Find("lblRecord", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanTanahViewModel>(printData);

                if (dgv.Columns["NilaiAset"] != null)
                {
                    dgv.Columns["NilaiAset"].DefaultCellStyle.Format = "C2";
                    dgv.Columns["NilaiAset"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (lblRec != null) lblRec.Text = $"Total Record : {printData.Count}";
        }

        #region CETAK DOKUMEN (PRINT PREVIEW)

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (printData.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak. Silakan tampilkan data terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 1000,
                Height = 700,
                ShowIcon = false,
                UseAntiAlias = true
            };
            preview.ShowDialog();
        }

        private void PrintDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            currentPrintIndex = 0;
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            int margin = 50;
            int yPos = margin;
            int pageWidth = e.PageBounds.Width - (margin * 2);

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

                    if (!string.IsNullOrEmpty(finalPath))
                    {
                        Image logo = Image.FromFile(finalPath);
                        g.DrawImage(logo, margin, yPos, 70, 70);
                    }
                }
                catch { }
            }

            g.DrawString(namaInstansi.ToUpper(), fontHeaderBold, Brushes.Black, new RectangleF(margin, yPos, pageWidth, 25), formatCenter);
            g.DrawString(alamat, fontHeaderNormal, Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            g.DrawString(kota, fontHeaderNormal, Brushes.Black, new RectangleF(margin, yPos + 45, pageWidth, 20), formatCenter);

            yPos += 85;
            g.DrawLine(new Pen(Color.Black, 2), margin, yPos, margin + pageWidth, yPos);
            yPos += 20;

            g.DrawString("LAPORAN TANAH INVENTARIS", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString($"TAHUN {DateTime.Now.Year}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos + 20, pageWidth, 20), formatCenter);
            yPos += 50;

            string[] headers = { "No", "Kode", "Pemilik", "No Sertifikat", "Luas (m²)", "Letak", "Status Hak", "Nilai Aset", "Penggunaan", "Status" };
            int[] colWidths = { 25, 50, 120, 100, 60, 150, 90, 110, 120, 60 };

            int tableWidth = colWidths.Sum();
            int startX = margin + ((pageWidth - tableWidth) / 2);
            int currentX = startX;

            Font fontTableHead = new Font("Arial", 8, FontStyle.Bold);
            Font fontTableData = new Font("Arial", 8, FontStyle.Regular);
            SolidBrush headerBrush = new SolidBrush(Color.FromArgb(143, 188, 143));

            g.FillRectangle(headerBrush, startX, yPos, tableWidth, 30);
            g.DrawRectangle(Pens.Black, startX, yPos, tableWidth, 30);

            StringFormat formatMidCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat formatMidLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            StringFormat formatMidRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

            for (int i = 0; i < headers.Length; i++)
            {
                RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], 30);
                g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], 30);
                g.DrawString(headers[i], fontTableHead, Brushes.Black, cellRect, formatMidCenter);
                currentX += colWidths[i];
            }
            yPos += 30;

            int rowHeight = 30;

            while (currentPrintIndex < printData.Count)
            {
                if (yPos + rowHeight > e.MarginBounds.Bottom - 120)
                {
                    e.HasMorePages = true;
                    return;
                }

                var item = printData[currentPrintIndex];
                currentX = startX;

                string[] rowData = {
                    (currentPrintIndex + 1).ToString(),
                    item.KodeTanah.ToString(),
                    item.NamaPemilik,
                    item.NomorSertifikat,
                    item.LuasTanah.ToString("N0"),
                    item.LetakTanah,
                    item.StatusHak,
                    item.NilaiAset.ToString("N0"),
                    item.Penggunaan,
                    item.Status
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = formatMidLeft;
                    if (i == 0 || i == 1 || i == 4 || i == 9) fmt = formatMidCenter;
                    else if (i == 7) fmt = formatMidRight;

                    if (fmt == formatMidLeft) cellRect.X += 5;
                    if (fmt == formatMidRight) cellRect.Width -= 5;

                    g.DrawString(rowData[i], fontTableData, Brushes.Black, cellRect, fmt);
                    currentX += colWidths[i];
                }

                yPos += rowHeight;
                currentPrintIndex++;
            }

            yPos += 30;
            string tglTtd = $"{kota}, {DateTime.Now.ToString("dd MMMM yyyy")}";

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
            if (printData.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diexport.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                sfd.FileName = "Laporan_Tanah_Inventaris_" + DateTime.Now.ToString("yyyyMMdd");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("Tanah Inventaris");

                            ws.Cells["A1:L1"].Merge = true;
                            ws.Cells["A1"].Value = "LAPORAN TANAH INVENTARIS";
                            ws.Cells["A1"].Style.Font.Bold = true;
                            ws.Cells["A1"].Style.Font.Size = 14;
                            ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            string[] headers = { "No", "Kode Tanah", "Nama Pemilik", "No Sertifikat", "Luas Tanah (m²)", "Letak Tanah", "Lokasi", "Status Hak", "Nilai Aset", "Penggunaan", "Tgl Perolehan", "Sumber Perolehan", "Status" };
                            for (int i = 0; i < headers.Length; i++)
                            {
                                ws.Cells[3, i + 1].Value = headers[i];
                                ws.Cells[3, i + 1].Style.Font.Bold = true;
                                ws.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                ws.Cells[3, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }

                            int row = 4;
                            for (int i = 0; i < printData.Count; i++)
                            {
                                var item = printData[i];
                                ws.Cells[row, 1].Value = i + 1;
                                ws.Cells[row, 2].Value = item.KodeTanah;
                                ws.Cells[row, 3].Value = item.NamaPemilik;
                                ws.Cells[row, 4].Value = item.NomorSertifikat;
                                ws.Cells[row, 5].Value = item.LuasTanah;
                                ws.Cells[row, 6].Value = item.LetakTanah;
                                ws.Cells[row, 7].Value = item.Lokasi;
                                ws.Cells[row, 8].Value = item.StatusHak;

                                ws.Cells[row, 9].Value = item.NilaiAset;
                                ws.Cells[row, 9].Style.Numberformat.Format = "Rp #,##0.00";

                                ws.Cells[row, 10].Value = item.Penggunaan;
                                ws.Cells[row, 11].Value = item.TanggalPerolehan;
                                ws.Cells[row, 12].Value = item.SumberPerolehan;
                                ws.Cells[row, 13].Value = item.Status;

                                for (int col = 1; col <= 13; col++)
                                    ws.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                row++;
                            }

                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            package.SaveAs(new FileInfo(sfd.FileName));
                        }
                        MessageBox.Show("Data berhasil diekspor ke Excel!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saat export: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void BtnTutup_Click(object sender, EventArgs e)
        {
            MainForm parentForm = this.ParentForm as MainForm;
            if (parentForm != null)
            {
                parentForm.ChangeView(new DashboardUC());
                LaporanForm frm = new LaporanForm();
                frm.ShowDialog();
            }
        }
    }
}
