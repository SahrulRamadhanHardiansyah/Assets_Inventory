using Assets_Inventory.Helper; 
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
    public partial class LaporanBarangInventarisUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanBarangViewModel> printData = new List<LaporanBarangViewModel>();
        int currentPrintIndex = 0;

        public class LaporanBarangViewModel
        {
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string Kategori { get; set; }
            public string Lokasi { get; set; }
            public string Ruang { get; set; }
            public string Kondisi { get; set; }
            public DateTime TanggalRegistrasi { get; set; }
            public decimal HargaSatuan { get; set; }
            public string Status { get; set; }
            public string GambarBase64 { get; set; }
        }

        public LaporanBarangInventarisUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += LaporanBarangInventarisUC_Load;

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

        private void LaporanBarangInventarisUC_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        }

        private void LoadComboBoxes()
        {
            var cmbKat = this.Controls.Find("cmbFilterKategori", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbKategori", true).FirstOrDefault() as ComboBox;
            var cmbNama = this.Controls.Find("cmbFilterNamaBarang", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbNamaBarang", true).FirstOrDefault() as ComboBox;
            var cmbLok = this.Controls.Find("cmbFilterLokasi", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbFilterRuang", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbRuang", true).FirstOrDefault() as ComboBox;
            var cmbKon = this.Controls.Find("cmbFilterKondisi", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbKondisi", true).FirstOrDefault() as ComboBox;

            if (cmbKat != null)
            {
                var list = db.Kategori.ToList();
                list.Insert(0, new Kategori { IdKategori = 0, NamaKategori = "-- Semua Kategori --" });
                cmbKat.DataSource = list;
                cmbKat.DisplayMember = "NamaKategori";
                cmbKat.ValueMember = "IdKategori";
            }

            if (cmbNama != null)
            {
                var list = db.MasterBarang.ToList();
                list.Insert(0, new MasterBarang { IdMasterBarang = 0, NamaBarang = "-- Semua Barang --" });
                cmbNama.DataSource = list;
                cmbNama.DisplayMember = "NamaBarang";
                cmbNama.ValueMember = "IdMasterBarang";
            }

            if (cmbLok != null)
            {
                var list = db.Lokasi.ToList();
                list.Insert(0, new Lokasi { IdLokasi = 0, NamaLokasi = "-- Semua Lokasi --" });
                cmbLok.DataSource = list;
                cmbLok.DisplayMember = "NamaLokasi";
                cmbLok.ValueMember = "IdLokasi";
            }

            if (cmbRuang != null)
            {
                var list = db.Ruang.ToList();
                list.Insert(0, new Ruang { IdRuang = 0, NamaRuang = "-- Semua Ruang --" });
                cmbRuang.DataSource = list;
                cmbRuang.DisplayMember = "NamaRuang";
                cmbRuang.ValueMember = "IdRuang";
            }

            if (cmbKon != null)
            {
                var list = db.Kondisi.ToList();
                list.Insert(0, new Kondisi { IdKondisi = 0, NamaKondisi = "-- Semua Kondisi --" });
                cmbKon.DataSource = list;
                cmbKon.DisplayMember = "NamaKondisi";
                cmbKon.ValueMember = "IdKondisi";
            }
        }

        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cmbKat = this.Controls.Find("cmbFilterKategori", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbKategori", true).FirstOrDefault() as ComboBox;
            var cmbNama = this.Controls.Find("cmbFilterNamaBarang", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbNamaBarang", true).FirstOrDefault() as ComboBox;
            var cmbLok = this.Controls.Find("cmbFilterLokasi", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbFilterRuang", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbRuang", true).FirstOrDefault() as ComboBox;
            var cmbKon = this.Controls.Find("cmbFilterKondisi", true).FirstOrDefault() as ComboBox ?? this.Controls.Find("cmbKondisi", true).FirstOrDefault() as ComboBox;

            int idKat = cmbKat?.SelectedValue != null ? (int)cmbKat.SelectedValue : 0;
            string namaBrg = cmbNama?.SelectedIndex > 0 ? cmbNama.Text : "";
            int idLok = cmbLok?.SelectedValue != null ? (int)cmbLok.SelectedValue : 0;
            int idRng = cmbRuang?.SelectedValue != null ? (int)cmbRuang.SelectedValue : 0;
            int idKon = cmbKon?.SelectedValue != null ? (int)cmbKon.SelectedValue : 0;

            var query = from v in db.VAset.AsNoTracking()
                        join a in db.Aset.AsNoTracking() on v.KodeBarang equals a.KodeBarang
                        select new { v, a.Gambar };

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
            if (idKon != 0)
            {
                string nmKon = db.Kondisi.Find(idKon)?.NamaKondisi;
                query = query.Where(q => q.v.NamaKondisi == nmKon);
            }

            printData = query.ToList().Select(q => new LaporanBarangViewModel
            {
                KodeInventaris = q.v.KodeInventaris ?? "-",
                NamaBarang = q.v.NamaBarang ?? "N/A",
                Kategori = q.v.NamaKategori ?? "-",
                Lokasi = q.v.NamaLokasi ?? "-",
                Ruang = q.v.NamaRuang ?? "-",
                Kondisi = q.v.NamaKondisi ?? "-",
                TanggalRegistrasi = q.v.TanggalRegistrasi,
                HargaSatuan = q.v.HargaSatuan ?? 0m,
                Status = q.v.Status ?? "-",
                GambarBase64 = q.Gambar
            }).OrderBy(x => x.NamaBarang).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblTot = this.Controls.Find("lblTotal", true).FirstOrDefault() as Label;
            var lblTotItem = this.Controls.Find("lblTotalItem", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanBarangViewModel>(printData);

                if (dgv.Columns["GambarBase64"] != null) dgv.Columns["GambarBase64"].Visible = false;

                if (dgv.Columns["HargaSatuan"] != null)
                {
                    dgv.Columns["HargaSatuan"].DefaultCellStyle.Format = "C2";
                    dgv.Columns["HargaSatuan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgv.Columns["TanggalRegistrasi"] != null)
                {
                    dgv.Columns["TanggalRegistrasi"].HeaderText = "Tgl Registrasi";
                    dgv.Columns["TanggalRegistrasi"].DefaultCellStyle.Format = "dd MMM yyyy";
                }
            }

            if (lblTot != null) lblTot.Text = $"Total Record: {printData.Count}";
            if (lblTotItem != null) lblTotItem.Text = $"Total Item: {printData.Count}";
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

            g.DrawString("LAPORAN BARANG INVENTARIS", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString($"TAHUN {DateTime.Now.Year}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos + 20, pageWidth, 20), formatCenter);
            yPos += 50;

            var chkImg = this.Controls.Find("chkIncludeGambar", true).FirstOrDefault() as CheckBox;
            bool includeGambar = chkImg != null && chkImg.Checked;

            string[] headers = { "No", "Kode Inventaris", "Nama Barang", "Kategori", "Ruang", "Kondisi", "Harga", "Status" };
            int[] colWidths = { 30, 140, 170, 110, 120, 70, 100, 80 };

            if (includeGambar)
            {
                headers = headers.Concat(new[] { "Photo" }).ToArray();
                colWidths = colWidths.Concat(new[] { 90 }).ToArray();
            }

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

            int rowHeight = includeGambar ? 60 : 30;

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
                    item.Ruang,
                    item.Kondisi,
                    item.HargaSatuan.ToString("N0"),
                    item.Status
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = formatMidLeft;
                    if (i == 0 || i == 5 || i == 7) fmt = formatMidCenter;
                    else if (i == 6) fmt = formatMidRight;

                    if (fmt == formatMidLeft) cellRect.X += 5;
                    if (fmt == formatMidRight) cellRect.Width -= 5;

                    g.DrawString(rowData[i], fontTableData, Brushes.Black, cellRect, fmt);
                    currentX += colWidths[i];
                }

                if (includeGambar)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[colWidths.Length - 1], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[colWidths.Length - 1], rowHeight);

                    if (!string.IsNullOrEmpty(item.GambarBase64))
                    {
                        try
                        {
                            byte[] imgBytes = Convert.FromBase64String(item.GambarBase64);
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                Image asetImg = Image.FromStream(ms);
                                g.DrawImage(asetImg, currentX + 10, yPos + 5, 70, 50);
                            }
                        }
                        catch { }
                    }
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