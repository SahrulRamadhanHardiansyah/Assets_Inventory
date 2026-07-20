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
    public partial class LaporanProsesOpnameUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanOpnameViewModel> printData = new List<LaporanOpnameViewModel>();
        int currentPrintIndex = 0;

        public class LaporanOpnameViewModel
        {
            public int IdOpnameAset { get; set; }
            public DateTime TanggalOpname { get; set; }
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string NamaRuang { get; set; }
            public string KondisiTerkini { get; set; }
            public string Keterangan { get; set; }
        }

        public LaporanProsesOpnameUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += LaporanProsesOpnameUC_Load;

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

        private void LaporanProsesOpnameUC_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            var dtAwal = this.Controls.Find("dtAwal", true).FirstOrDefault() as DateTimePicker;
            var dtAkhir = this.Controls.Find("dtAkhir", true).FirstOrDefault() as DateTimePicker;
            if (dtAwal != null) dtAwal.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (dtAkhir != null) dtAkhir.Value = DateTime.Now;

            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        }

        private void LoadComboBoxes()
        {
            var cmbKategori = this.Controls.Find("cmbKategori", true).FirstOrDefault() as ComboBox;
            var cmbBarang = this.Controls.Find("cmbBarang", true).FirstOrDefault() as ComboBox;
            var cmbLokasi = this.Controls.Find("cmbLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbRuang", true).FirstOrDefault() as ComboBox;

            if (cmbKategori != null)
            {
                var list = db.Kategori.ToList();
                list.Insert(0, new Kategori { IdKategori = 0, NamaKategori = "-- Semua Kategori --" });
                cmbKategori.DataSource = list;
                cmbKategori.DisplayMember = "NamaKategori";
                cmbKategori.ValueMember = "IdKategori";
            }

            if (cmbBarang != null)
            {
                var list = db.MasterBarang.ToList();
                list.Insert(0, new MasterBarang { IdMasterBarang = 0, NamaBarang = "-- Semua Barang --" });
                cmbBarang.DataSource = list;
                cmbBarang.DisplayMember = "NamaBarang";
                cmbBarang.ValueMember = "IdMasterBarang";
            }

            if (cmbLokasi != null)
            {
                var list = db.Lokasi.ToList();
                list.Insert(0, new Lokasi { IdLokasi = 0, NamaLokasi = "-- Semua Lokasi --" });
                cmbLokasi.DataSource = list;
                cmbLokasi.DisplayMember = "NamaLokasi";
                cmbLokasi.ValueMember = "IdLokasi";
            }

            if (cmbRuang != null)
            {
                var list = db.Ruang.ToList();
                list.Insert(0, new Ruang { IdRuang = 0, NamaRuang = "-- Semua Ruang --" });
                cmbRuang.DataSource = list;
                cmbRuang.DisplayMember = "NamaRuang";
                cmbRuang.ValueMember = "IdRuang";
            }
        }

        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var dtAwal = this.Controls.Find("dtAwal", true).FirstOrDefault() as DateTimePicker;
            var dtAkhir = this.Controls.Find("dtAkhir", true).FirstOrDefault() as DateTimePicker;
            var cmbKategori = this.Controls.Find("cmbKategori", true).FirstOrDefault() as ComboBox;
            var cmbBarang = this.Controls.Find("cmbBarang", true).FirstOrDefault() as ComboBox;
            var cmbLokasi = this.Controls.Find("cmbLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbRuang", true).FirstOrDefault() as ComboBox;

            DateTime tglAwal = dtAwal?.Value.Date ?? DateTime.MinValue;
            DateTime tglAkhir = dtAkhir?.Value.Date.AddDays(1).AddTicks(-1) ?? DateTime.MaxValue;

            int idKat = cmbKategori?.SelectedValue != null ? (int)cmbKategori.SelectedValue : 0;
            string namaBrg = cmbBarang?.SelectedIndex > 0 ? cmbBarang.Text : "";
            int idLok = cmbLokasi?.SelectedValue != null ? (int)cmbLokasi.SelectedValue : 0;
            int idRng = cmbRuang?.SelectedValue != null ? (int)cmbRuang.SelectedValue : 0;

            var query = db.VOpnameAset.AsNoTracking()
                        .Where(v => v.TanggalOpname >= tglAwal && v.TanggalOpname <= tglAkhir);

            if (!string.IsNullOrEmpty(namaBrg))
            {
                query = query.Where(v => v.NamaBarang == namaBrg);
            }

            if (idRng != 0)
            {
                string nmRng = db.Ruang.Find(idRng)?.NamaRuang;
                query = query.Where(v => v.NamaRuang == nmRng);
            }

            printData = query.ToList().Select(v => new LaporanOpnameViewModel
            {
                IdOpnameAset = v.IdOpnameAset,
                TanggalOpname = v.TanggalOpname,
                KodeInventaris = v.KodeInventaris ?? "-",
                NamaBarang = v.NamaBarang ?? "N/A",
                NamaRuang = v.NamaRuang ?? "-",
                KondisiTerkini = v.KondisiTerkini ?? "-",
                Keterangan = v.Keterangan ?? "-"
            }).OrderBy(x => x.TanggalOpname).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblRec = this.Controls.Find("lblRecord", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanOpnameViewModel>(printData);

                if (dgv.Columns["TanggalOpname"] != null)
                {
                    dgv.Columns["TanggalOpname"].DefaultCellStyle.Format = "dd MMM yyyy";
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

            g.DrawString("LAPORAN PROSES OPNAME BARANG", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);

            var dtpAwal = this.Controls.Find("dtAwal", true).FirstOrDefault() as DateTimePicker;
            var dtpAkhir = this.Controls.Find("dtAkhir", true).FirstOrDefault() as DateTimePicker;
            string tglStr = $"Periode: {dtpAwal?.Value.ToString("dd MMM yyyy")} s.d {dtpAkhir?.Value.ToString("dd MMM yyyy")}";
            g.DrawString(tglStr, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            yPos += 55;

            string[] headers = { "No", "Tgl Opname", "Kode Inventaris", "Nama Barang", "Ruang", "Kondisi Terkini", "Keterangan" };
            int[] colWidths = { 30, 100, 140, 200, 150, 130, 220 };

            int tableWidth = colWidths.Sum();
            int startX = margin + ((pageWidth - tableWidth) / 2);
            int currentX = startX;

            Font fontTableHead = new Font("Arial", 9, FontStyle.Bold);
            Font fontTableData = new Font("Arial", 9, FontStyle.Regular);
            SolidBrush headerBrush = new SolidBrush(Color.FromArgb(143, 188, 143));

            g.FillRectangle(headerBrush, startX, yPos, tableWidth, 30);
            g.DrawRectangle(Pens.Black, startX, yPos, tableWidth, 30);

            StringFormat formatMidCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat formatMidLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

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
                    item.TanggalOpname.ToString("dd-MM-yyyy"),
                    item.KodeInventaris,
                    item.NamaBarang,
                    item.NamaRuang,
                    item.KondisiTerkini,
                    item.Keterangan
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = (i == 0 || i == 1 || i == 5) ? formatMidCenter : formatMidLeft;
                    if (fmt == formatMidLeft) cellRect.X += 5;

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
                sfd.FileName = "Laporan_Proses_Opname_" + DateTime.Now.ToString("yyyyMMdd");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("Opname");

                            ws.Cells["A1:G1"].Merge = true;
                            ws.Cells["A1"].Value = "LAPORAN PROSES OPNAME BARANG";
                            ws.Cells["A1"].Style.Font.Bold = true;
                            ws.Cells["A1"].Style.Font.Size = 14;
                            ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            string[] headers = { "No", "Tanggal Opname", "Kode Inventaris", "Nama Barang", "Ruang", "Kondisi Terkini", "Keterangan" };
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
                                ws.Cells[row, 2].Value = item.TanggalOpname.ToString("yyyy-MM-dd");
                                ws.Cells[row, 3].Value = item.KodeInventaris;
                                ws.Cells[row, 4].Value = item.NamaBarang;
                                ws.Cells[row, 5].Value = item.NamaRuang;
                                ws.Cells[row, 6].Value = item.KondisiTerkini;
                                ws.Cells[row, 7].Value = item.Keterangan;

                                for (int col = 1; col <= 7; col++)
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
