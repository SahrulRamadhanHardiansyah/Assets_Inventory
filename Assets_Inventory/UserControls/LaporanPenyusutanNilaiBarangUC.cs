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
    public partial class LaporanPenyusutanNilaiBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanPenyusutanViewModel> printData = new List<LaporanPenyusutanViewModel>();
        int currentPrintIndex = 0;

        public class LaporanPenyusutanViewModel
        {
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string Kategori { get; set; }
            public DateTime TanggalRegistrasi { get; set; }
            public decimal HargaPerolehan { get; set; }
            public int UmurEkonomi { get; set; }
            public decimal NilaiResidu { get; set; }
            public int UsiaAset { get; set; }
            public decimal PenyusutanPerTahun { get; set; }
            public decimal AkumulasiPenyusutan { get; set; }
            public decimal NilaiBuku { get; set; }
        }

        public LaporanPenyusutanNilaiBarangUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += LaporanPenyusutanNilaiBarangUC_Load;

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

        private void LaporanPenyusutanNilaiBarangUC_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            var dtTgl = this.Controls.Find("dtTgl", true).FirstOrDefault() as DateTimePicker;
            if (dtTgl != null) dtTgl.Value = DateTime.Now;

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

            var cmbKategori = this.Controls.Find("cmbKategori", true).FirstOrDefault() as ComboBox;
            var cmbBarang = this.Controls.Find("cmbBarang", true).FirstOrDefault() as ComboBox;
            var cmbLokasi = this.Controls.Find("cmbLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbRuang", true).FirstOrDefault() as ComboBox;
            var dtTgl = this.Controls.Find("dtTgl", true).FirstOrDefault() as DateTimePicker;

            int idKat = cmbKategori?.SelectedValue != null ? (int)cmbKategori.SelectedValue : 0;
            string namaBrg = cmbBarang?.SelectedIndex > 0 ? cmbBarang.Text : "";
            int idLok = cmbLokasi?.SelectedValue != null ? (int)cmbLokasi.SelectedValue : 0;
            int idRng = cmbRuang?.SelectedValue != null ? (int)cmbRuang.SelectedValue : 0;
            DateTime tglHitung = dtTgl?.Value.Date ?? DateTime.Now.Date;

            var query = from v in db.VAset.AsNoTracking()
                        join a in db.Aset.AsNoTracking() on v.KodeBarang equals a.KodeBarang
                        where a.UmurEkonomi != null && a.UmurEkonomi > 0
                        select new { v, a };

            if (idKat != 0)
            {
                string nmKat = db.Kategori.Find(idKat)?.NamaKategori;
                query = query.Where(q => q.v.NamaKategori == nmKat);
            }
            if (!string.IsNullOrEmpty(namaBrg))
            {
                query = query.Where(q => q.v.NamaBarang == namaBrg);
            }
            if (idLok != 0)
            {
                string nmLok = db.Lokasi.Find(idLok)?.NamaLokasi;
                query = query.Where(q => q.v.NamaLokasi == nmLok);
            }
            if (idRng != 0)
            {
                string nmRng = db.Ruang.Find(idRng)?.NamaRuang;
                query = query.Where(q => q.v.NamaRuang == nmRng);
            }

            printData = query.ToList().Select(q =>
            {
                decimal hargaPerolehan = q.a.HargaSatuan ?? 0m;
                int umurEkonomi = q.a.UmurEkonomi ?? 1;
                decimal nilaiResidu = q.a.NilaiResidu ?? 0m;
                DateTime tglRegistrasi = q.v.TanggalRegistrasi;

                int usiaAset = (int)Math.Floor((tglHitung - tglRegistrasi).TotalDays / 365.25);
                if (usiaAset < 0) usiaAset = 0;

                decimal penyusutanPerTahun = umurEkonomi > 0 ? (hargaPerolehan - nilaiResidu) / umurEkonomi : 0;
                if (penyusutanPerTahun < 0) penyusutanPerTahun = 0;

                int tahunPenyusutan = Math.Min(usiaAset, umurEkonomi);
                decimal akumulasiPenyusutan = penyusutanPerTahun * tahunPenyusutan;

                decimal nilaiBuku = hargaPerolehan - akumulasiPenyusutan;
                if (nilaiBuku < nilaiResidu) nilaiBuku = nilaiResidu;

                return new LaporanPenyusutanViewModel
                {
                    KodeInventaris = q.v.KodeInventaris ?? "-",
                    NamaBarang = q.v.NamaBarang ?? "N/A",
                    Kategori = q.v.NamaKategori ?? "-",
                    TanggalRegistrasi = tglRegistrasi,
                    HargaPerolehan = hargaPerolehan,
                    UmurEkonomi = umurEkonomi,
                    NilaiResidu = nilaiResidu,
                    UsiaAset = usiaAset,
                    PenyusutanPerTahun = penyusutanPerTahun,
                    AkumulasiPenyusutan = akumulasiPenyusutan,
                    NilaiBuku = nilaiBuku
                };
            }).OrderBy(x => x.NamaBarang).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblRec = this.Controls.Find("lblRecord", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanPenyusutanViewModel>(printData);

                foreach (string col in new[] { "HargaPerolehan", "NilaiResidu", "PenyusutanPerTahun", "AkumulasiPenyusutan", "NilaiBuku" })
                {
                    if (dgv.Columns[col] != null)
                    {
                        dgv.Columns[col].DefaultCellStyle.Format = "C2";
                        dgv.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                if (dgv.Columns["TanggalRegistrasi"] != null)
                {
                    dgv.Columns["TanggalRegistrasi"].HeaderText = "Tgl Registrasi";
                    dgv.Columns["TanggalRegistrasi"].DefaultCellStyle.Format = "dd MMM yyyy";
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

            g.DrawString("LAPORAN PENYUSUTAN NILAI BARANG", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);

            var dtTgl = this.Controls.Find("dtTgl", true).FirstOrDefault() as DateTimePicker;
            string tglStr = $"Per Tanggal: {dtTgl?.Value.ToString("dd MMMM yyyy")}";
            g.DrawString(tglStr, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            yPos += 55;

            string[] headers = { "No", "Kode Inventaris", "Nama Barang", "Kategori", "Hrg Perolehan", "Umur Eko", "N. Residu", "Usia", "Peny/Thn", "Akm Peny", "Nilai Buku" };
            int[] colWidths = { 25, 110, 160, 90, 100, 50, 90, 40, 95, 100, 100 };

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
                    item.KodeInventaris,
                    item.NamaBarang,
                    item.Kategori,
                    item.HargaPerolehan.ToString("N0"),
                    item.UmurEkonomi.ToString(),
                    item.NilaiResidu.ToString("N0"),
                    item.UsiaAset.ToString(),
                    item.PenyusutanPerTahun.ToString("N0"),
                    item.AkumulasiPenyusutan.ToString("N0"),
                    item.NilaiBuku.ToString("N0")
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = formatMidLeft;
                    if (i == 0 || i == 5 || i == 7) fmt = formatMidCenter;
                    else if (i >= 4 && i != 5 && i != 7) fmt = formatMidRight;

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
                sfd.FileName = "Laporan_Penyusutan_Nilai_Barang_" + DateTime.Now.ToString("yyyyMMdd");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("Penyusutan");

                            ws.Cells["A1:K1"].Merge = true;
                            ws.Cells["A1"].Value = "LAPORAN PENYUSUTAN NILAI BARANG";
                            ws.Cells["A1"].Style.Font.Bold = true;
                            ws.Cells["A1"].Style.Font.Size = 14;
                            ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            string[] headers = { "No", "Kode Inventaris", "Nama Barang", "Kategori", "Tgl Registrasi", "Harga Perolehan", "Umur Ekonomi", "Nilai Residu", "Usia Aset", "Penyusutan/Thn", "Akumulasi Penyusutan", "Nilai Buku" };
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
                                ws.Cells[row, 2].Value = item.KodeInventaris;
                                ws.Cells[row, 3].Value = item.NamaBarang;
                                ws.Cells[row, 4].Value = item.Kategori;

                                ws.Cells[row, 5].Value = item.TanggalRegistrasi;
                                ws.Cells[row, 5].Style.Numberformat.Format = "dd-MM-yyyy";

                                ws.Cells[row, 6].Value = item.HargaPerolehan;
                                ws.Cells[row, 6].Style.Numberformat.Format = "Rp #,##0.00";

                                ws.Cells[row, 7].Value = item.UmurEkonomi;

                                ws.Cells[row, 8].Value = item.NilaiResidu;
                                ws.Cells[row, 8].Style.Numberformat.Format = "Rp #,##0.00";

                                ws.Cells[row, 9].Value = item.UsiaAset;

                                ws.Cells[row, 10].Value = item.PenyusutanPerTahun;
                                ws.Cells[row, 10].Style.Numberformat.Format = "Rp #,##0.00";

                                ws.Cells[row, 11].Value = item.AkumulasiPenyusutan;
                                ws.Cells[row, 11].Style.Numberformat.Format = "Rp #,##0.00";

                                ws.Cells[row, 12].Value = item.NilaiBuku;
                                ws.Cells[row, 12].Style.Numberformat.Format = "Rp #,##0.00";

                                for (int col = 1; col <= 12; col++)
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
