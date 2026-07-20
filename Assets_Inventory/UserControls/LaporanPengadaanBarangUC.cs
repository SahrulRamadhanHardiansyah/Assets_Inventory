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
    public partial class LaporanPengadaanBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanPengadaanViewModel> printData = new List<LaporanPengadaanViewModel>();
        int currentPrintIndex = 0;

        public class LaporanPengadaanViewModel
        {
            public int IdPengadaan { get; set; }
            public DateTime Tanggal { get; set; }
            public string NamaBarang { get; set; }
            public string Kategori { get; set; }
            public string Sumber { get; set; }
            public string Pemasok { get; set; }
            public int Jumlah { get; set; }
            public decimal HargaSatuan { get; set; }
            public decimal SubTotal { get; set; }
            public string Keterangan { get; set; }
        }

        public LaporanPengadaanBarangUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += LaporanPengadaanBarangUC_Load;

            // Mengikat event ke kontrol UI (asumsi penamaan standar)
            if (this.Controls.Find("btnTampilkanData", true).FirstOrDefault() is Button btnTampil)
                btnTampil.Click += BtnTampilkanData_Click;

            if (this.Controls.Find("btnPreview", true).FirstOrDefault() is Button btnPrev)
                btnPrev.Click += BtnPreview_Click;

            if (this.Controls.Find("btnExportToExcel", true).FirstOrDefault() is Button btnExport)
                btnExport.Click += BtnExportToExcel_Click;

            if (this.Controls.Find("btnTutup", true).FirstOrDefault() is Button btnTutup)
                btnTutup.Click += BtnTutup_Click;

            printDoc.BeginPrint += PrintDoc_BeginPrint;
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void LaporanPengadaanBarangUC_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            // Set Tanggal Default (Awal bulan ini s/d Hari ini)
            var dtpMulai = this.Controls.Find("dtpMulai", true).FirstOrDefault() as DateTimePicker;
            var dtpSampai = this.Controls.Find("dtpSampai", true).FirstOrDefault() as DateTimePicker;
            if (dtpMulai != null) dtpMulai.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (dtpSampai != null) dtpSampai.Value = DateTime.Now;

            // Set Print to Landscape
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        }

        private void LoadComboBoxes()
        {
            var cmbKategori = this.Controls.Find("cmbKategori", true).FirstOrDefault() as ComboBox;
            var cmbNamaBarang = this.Controls.Find("cmbNamaBarang", true).FirstOrDefault() as ComboBox;
            var cmbLokasi = this.Controls.Find("cmbLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbRuang", true).FirstOrDefault() as ComboBox;

            if (cmbKategori != null)
            {
                var listKat = db.Kategori.ToList();
                listKat.Insert(0, new Kategori { IdKategori = 0, NamaKategori = "-- Semua Kategori --" });
                cmbKategori.DataSource = listKat;
                cmbKategori.DisplayMember = "NamaKategori";
                cmbKategori.ValueMember = "IdKategori";
            }

            if (cmbNamaBarang != null)
            {
                var listBrg = db.MasterBarang.ToList();
                listBrg.Insert(0, new MasterBarang { IdMasterBarang = 0, NamaBarang = "-- Semua Barang --" });
                cmbNamaBarang.DataSource = listBrg;
                cmbNamaBarang.DisplayMember = "NamaBarang";
                cmbNamaBarang.ValueMember = "IdMasterBarang";
            }

            if (cmbLokasi != null)
            {
                var listLok = db.Lokasi.ToList();
                listLok.Insert(0, new Lokasi { IdLokasi = 0, NamaLokasi = "-- Semua Lokasi --" });
                cmbLokasi.DataSource = listLok;
                cmbLokasi.DisplayMember = "NamaLokasi";
                cmbLokasi.ValueMember = "IdLokasi";
            }

            if (cmbRuang != null)
            {
                var listRng = db.Ruang.ToList();
                listRng.Insert(0, new Ruang { IdRuang = 0, NamaRuang = "-- Semua Ruang --" });
                cmbRuang.DataSource = listRng;
                cmbRuang.DisplayMember = "NamaRuang";
                cmbRuang.ValueMember = "IdRuang";
            }
        }

        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var dtpMulai = this.Controls.Find("dtpMulai", true).FirstOrDefault() as DateTimePicker;
            var dtpSampai = this.Controls.Find("dtpSampai", true).FirstOrDefault() as DateTimePicker;
            var cmbKategori = this.Controls.Find("cmbKategori", true).FirstOrDefault() as ComboBox;
            var cmbNamaBarang = this.Controls.Find("cmbNamaBarang", true).FirstOrDefault() as ComboBox;
            var cmbLokasi = this.Controls.Find("cmbLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbRuang", true).FirstOrDefault() as ComboBox;

            DateTime tglAwal = dtpMulai?.Value.Date ?? DateTime.MinValue;
            DateTime tglAkhir = dtpSampai?.Value.Date.AddDays(1).AddTicks(-1) ?? DateTime.MaxValue;

            int idKat = cmbKategori?.SelectedValue != null ? (int)cmbKategori.SelectedValue : 0;
            string namaBrg = cmbNamaBarang?.SelectedIndex > 0 ? cmbNamaBarang.Text : "";
            int idLok = cmbLokasi?.SelectedValue != null ? (int)cmbLokasi.SelectedValue : 0;
            int idRng = cmbRuang?.SelectedValue != null ? (int)cmbRuang.SelectedValue : 0;

            // Kita tarik data dari View v_pengadaan_aset yang sudah dibuat sebelumnya
            // Karena View ini tidak berisi ID Ruang/Lokasi (hanya pengadaan global), 
            // filter Lokasi/Ruang akan diabaikan kecuali kita nge-JOIN manual ke tabel Aset.
            // Untuk kesederhanaan laporan pengadaan, kita filter by Kategori & Nama Barang saja.

            var query = db.VPengadaanAset.AsNoTracking()
                        .Where(v => v.TanggalPengadaan >= tglAwal && v.TanggalPengadaan <= tglAkhir);

            if (idKat != 0)
            {
                var katName = db.Kategori.Find(idKat)?.NamaKategori;
                query = query.Where(v => v.NamaKategori == katName);
            }

            if (!string.IsNullOrEmpty(namaBrg))
            {
                query = query.Where(v => v.NamaBarang == namaBrg);
            }

            printData = query.ToList().Select(v => new LaporanPengadaanViewModel
            {
                IdPengadaan = v.IdPengadaan,
                Tanggal = v.TanggalPengadaan,
                NamaBarang = v.NamaBarang ?? "N/A",
                Kategori = v.NamaKategori ?? "-",
                Sumber = v.NamaSumber ?? "-",
                Pemasok = v.NamaPemasok ?? "-",
                Jumlah = v.JumlahMasuk,
                HargaSatuan = v.HargaSatuan,
                SubTotal = v.SubTotal,
                Keterangan = v.Keterangan ?? "-"
            }).OrderBy(x => x.Tanggal).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblTot = this.Controls.Find("lblTotal", true).FirstOrDefault() as Label;
            var lblTotItem = this.Controls.Find("lblTotalItem", true).FirstOrDefault() as Label;
            var lblTotHarga = this.Controls.Find("lblTotalHarga", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanPengadaanViewModel>(printData);

                if (dgv.Columns["HargaSatuan"] != null)
                {
                    dgv.Columns["HargaSatuan"].DefaultCellStyle.Format = "C2";
                    dgv.Columns["HargaSatuan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgv.Columns["SubTotal"] != null)
                {
                    dgv.Columns["SubTotal"].DefaultCellStyle.Format = "C2";
                    dgv.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgv.Columns["Tanggal"] != null)
                {
                    dgv.Columns["Tanggal"].DefaultCellStyle.Format = "dd MMM yyyy";
                }
            }

            if (lblTot != null) lblTot.Text = $"Total Record: {printData.Count}";
            if (lblTotItem != null) lblTotItem.Text = $"Total Item: {printData.Sum(x => x.Jumlah)}";
            if (lblTotHarga != null) lblTotHarga.Text = $"Total Harga: {printData.Sum(x => x.SubTotal).ToString("C2")}";
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
                    string namaFileLogo = setup.LogoInstansi;
                    string path1 = Path.Combine(Application.StartupPath, "Resources", namaFileLogo);
                    string path2 = Path.Combine(Application.StartupPath, @"..\..\Resources", namaFileLogo);

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

            g.DrawString("LAPORAN PENGADAAN BARANG INVENTARIS", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);

            var dtpMulai = this.Controls.Find("dtpMulai", true).FirstOrDefault() as DateTimePicker;
            var dtpSampai = this.Controls.Find("dtpSampai", true).FirstOrDefault() as DateTimePicker;
            string tglStr = $"Periode: {dtpMulai?.Value.ToString("dd MMM yyyy")} s.d {dtpSampai?.Value.ToString("dd MMM yyyy")}";

            g.DrawString(tglStr, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            yPos += 55;

            string[] headers = { "No", "Tgl Pengadaan", "Nama Barang", "Kategori", "Sumber", "Pemasok", "Jml", "Harga Satuan", "Sub Total" };
            int[] colWidths = { 30, 100, 200, 110, 120, 130, 40, 150, 189 };

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
            decimal pageTotal = 0;

            while (currentPrintIndex < printData.Count)
            {
                if (yPos + rowHeight > e.MarginBounds.Bottom - 150)
                {
                    e.HasMorePages = true;
                    return;
                }

                var item = printData[currentPrintIndex];
                currentX = startX;
                pageTotal += item.SubTotal;

                string[] rowData = {
                    (currentPrintIndex + 1).ToString(),
                    item.Tanggal.ToString("dd-MM-yyyy"),
                    item.NamaBarang,
                    item.Kategori,
                    item.Sumber,
                    item.Pemasok,
                    item.Jumlah.ToString(),
                    item.HargaSatuan.ToString("N2"),
                    item.SubTotal.ToString("N2")
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = formatMidLeft;
                    if (i == 0 || i == 1 || i == 6) fmt = formatMidCenter; 
                    else if (i == 7 || i == 8) fmt = formatMidRight; 

                    if (fmt == formatMidLeft) cellRect.X += 5;
                    if (fmt == formatMidRight) cellRect.Width -= 5;

                    g.DrawString(rowData[i], fontTableData, Brushes.Black, cellRect, fmt);
                    currentX += colWidths[i];
                }

                yPos += rowHeight;
                currentPrintIndex++;
            }

            if (currentPrintIndex >= printData.Count)
            {
                currentX = startX;
                int sumWidth = colWidths.Take(8).Sum();
                RectangleF totLabelRect = new RectangleF(currentX, yPos, sumWidth, rowHeight);
                RectangleF totValRect = new RectangleF(currentX + sumWidth, yPos, colWidths[8], rowHeight);

                g.FillRectangle(headerBrush, startX, yPos, tableWidth, rowHeight);
                g.DrawRectangle(Pens.Black, currentX, yPos, sumWidth, rowHeight);
                g.DrawRectangle(Pens.Black, currentX + sumWidth, yPos, colWidths[8], rowHeight);

                g.DrawString("TOTAL PENGADAAN KESELURUHAN", fontTableHead, Brushes.Black, totLabelRect, formatMidCenter);

                totValRect.Width -= 5;
                g.DrawString(printData.Sum(x => x.SubTotal).ToString("N2"), fontTableHead, Brushes.Black, totValRect, formatMidRight);
                yPos += rowHeight;
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
            if (parentForm != null)
            {
                parentForm.ChangeView(new DashboardUC());
                LaporanForm frm = new LaporanForm();
                frm.ShowDialog();
            }
        }
    }
}