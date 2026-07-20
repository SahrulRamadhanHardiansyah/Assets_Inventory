using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Data.Entity;
using Assets_Inventory.Helper;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Assets_Inventory.UserControls;

namespace Assets_Inventory
{
    public partial class KartuInventarisRuanganUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanKIRViewModel> printData = new List<LaporanKIRViewModel>();
        int currentPrintIndex = 0;

        public class LaporanKIRViewModel
        {
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string Kategori { get; set; }
            public string Kondisi { get; set; }
            public string TahunPengadaan { get; set; }
            public string SumberPerolehan { get; set; }
            public int Jumlah { get; set; }
            public string Satuan { get; set; }
            public string Ruang { get; set; }
            public string Lokasi { get; set; }
            public string GambarBase64 { get; set; }
        }

        public KartuInventarisRuanganUC()
        {
            InitializeComponent();
            InitializeEvents(); 
        }

        private void InitializeEvents()
        {
            this.Load += KartuInventarisRuanganUC_Load;

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

        private void KartuInventarisRuanganUC_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        }

        private void LoadComboBoxes()
        {
            var cmbLokasi = this.Controls.Find("cmbFilterLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbFilterRuang", true).FirstOrDefault() as ComboBox; 

            if (cmbLokasi != null)
            {
                var listLokasi = db.Lokasi.ToList();
                listLokasi.Insert(0, new Lokasi { IdLokasi = 0, NamaLokasi = "-- Semua Lokasi --" });
                cmbLokasi.DataSource = listLokasi;
                cmbLokasi.DisplayMember = "NamaLokasi";
                cmbLokasi.ValueMember = "IdLokasi";
            }

            if (cmbRuang != null)
            {
                var listRuang = db.Ruang.ToList();
                listRuang.Insert(0, new Ruang { IdRuang = 0, NamaRuang = "-- Semua Ruang --" });
                cmbRuang.DataSource = listRuang;
                cmbRuang.DisplayMember = "NamaRuang";
                cmbRuang.ValueMember = "IdRuang";
            }
        }

        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cmbLokasi = this.Controls.Find("cmbFilterLokasi", true).FirstOrDefault() as ComboBox;
            var cmbRuang = this.Controls.Find("cmbFilterRuang", true).FirstOrDefault() as ComboBox;

            int idLokasi = cmbLokasi?.SelectedValue != null ? (int)cmbLokasi.SelectedValue : 0;
            int idRuang = cmbRuang?.SelectedValue != null ? (int)cmbRuang.SelectedValue : 0;

            var query = from a in db.Aset
                        join mb in db.MasterBarang on a.IdMasterBarang equals mb.IdMasterBarang
                        join k in db.Kategori on mb.IdKategori equals k.IdKategori into kGroup
                        from k in kGroup.DefaultIfEmpty()
                        join s in db.Satuan on mb.IdSatuan equals s.IdSatuan into sGroup
                        from s in sGroup.DefaultIfEmpty()
                        join ko in db.Kondisi on a.IdKondisi equals ko.IdKondisi into koGroup
                        from ko in koGroup.DefaultIfEmpty()
                        join r in db.Ruang on a.IdRuang equals r.IdRuang into rGroup
                        from r in rGroup.DefaultIfEmpty()
                        join l in db.Lokasi on a.IdLokasi equals l.IdLokasi into lGroup
                        from l in lGroup.DefaultIfEmpty()
                        join dp in db.DetailPengadaan on a.IdDetailPengadaan equals dp.IdDetailPengadaan into dpGroup
                        from dp in dpGroup.DefaultIfEmpty()
                        join p in db.Pengadaan on dp.IdPengadaan equals p.IdPengadaan into pGroup
                        from p in pGroup.DefaultIfEmpty()
                        join sp in db.SumberPerolehan on p.IdSumberPerolehan equals sp.IdSumberPerolehan into spGroup
                        from sp in spGroup.DefaultIfEmpty()
                        where a.Status != "Nonaktif"
                        select new LaporanKIRViewModel
                        {
                            KodeInventaris = a.KodeInventaris,
                            NamaBarang = mb.NamaBarang,
                            Kategori = k != null ? k.NamaKategori : "-",
                            Kondisi = ko != null ? ko.NamaKondisi : "-",
                            TahunPengadaan = a.TanggalRegistrasi.Year.ToString(),
                            SumberPerolehan = sp != null ? sp.NamaSumber : "-",
                            Jumlah = 1,
                            Satuan = s != null ? s.NamaSatuan : "-",
                            Ruang = r != null ? r.NamaRuang : "-",
                            Lokasi = l != null ? l.NamaLokasi : "-",
                            GambarBase64 = a.Gambar
                        };

            if (idLokasi != 0)
            {
                var locName = db.Lokasi.Find(idLokasi)?.NamaLokasi;
                query = query.Where(x => x.Lokasi == locName);
            }
            if (idRuang != 0)
            {
                var roomName = db.Ruang.Find(idRuang)?.NamaRuang;
                query = query.Where(x => x.Ruang == roomName);
            }

            printData = query.ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblTot = this.Controls.Find("lblTotal", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanKIRViewModel>(printData);
                if (dgv.Columns["GambarBase64"] != null) dgv.Columns["GambarBase64"].Visible = false;
                if (dgv.Columns["Ruang"] != null) dgv.Columns["Ruang"].Visible = false;
                if (dgv.Columns["Lokasi"] != null) dgv.Columns["Lokasi"].Visible = false;
            }

            if (lblTot != null) lblTot.Text = $"Total Record : {printData.Count}";
        }

        #region LOGIKA CETAK DOKUMEN (PRINT PREVIEW)

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

            if (setup != null && !string.IsNullOrEmpty(setup.LogoInstansi))
            {
                try
                {
                    string namaFileLogo = setup.LogoInstansi;
                    string path1 = Path.Combine(Application.StartupPath, "Resources", namaFileLogo);
                    string path2 = Path.Combine(Application.StartupPath, @"..\..\Resources", namaFileLogo);

                    string finalPath = "";
                    if (File.Exists(path1)) finalPath = path1;
                    else if (File.Exists(path2)) finalPath = path2;

                    if (!string.IsNullOrEmpty(finalPath))
                    {
                        Image logo = Image.FromFile(finalPath);
                        g.DrawImage(logo, margin, yPos, 70, 70);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            StringFormat formatCenter = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString(namaInstansi.ToUpper(), fontHeaderBold, Brushes.Black, new RectangleF(margin, yPos, pageWidth, 25), formatCenter);
            g.DrawString(alamat, fontHeaderNormal, Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            g.DrawString(kota, fontHeaderNormal, Brushes.Black, new RectangleF(margin, yPos + 45, pageWidth, 20), formatCenter);

            yPos += 85;
            g.DrawLine(new Pen(Color.Black, 2), margin, yPos, margin + pageWidth, yPos);
            yPos += 20;

            g.DrawString("KARTU INVENTARIS RUANGAN", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString($"TAHUN {DateTime.Now.Year}", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos + 20, pageWidth, 20), formatCenter);
            yPos += 50;

            var chkImg = this.Controls.Find("chkIncludeGambar", true).FirstOrDefault() as CheckBox;
            bool includeGambar = chkImg != null && chkImg.Checked;

            string[] headers = { "No", "Kode Inventaris", "Nama Barang", "Kategori", "Kondisi", "Thn Pengadaan", "Sumber", "Jml", "Satuan" };
            int[] colWidths = { 30, 140, 170, 130, 80, 100, 130, 40, 70 };
            if (includeGambar)
            {
                headers = headers.Concat(new[] { "Photo" }).ToArray();
                colWidths = colWidths.Concat(new[] { 100 }).ToArray();
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
                    item.Kondisi,
                    item.TahunPengadaan,
                    item.SumberPerolehan,
                    item.Jumlah.ToString(),
                    item.Satuan
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = (i == 0 || i == 4 || i == 5 || i == 7 || i == 8) ? formatMidCenter : formatMidLeft;

                    if (fmt == formatMidLeft) cellRect.X += 5;

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
                                g.DrawImage(asetImg, currentX + 10, yPos + 5, 80, 50);
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
            g.DrawString(tglTtd, fontTableData, Brushes.Black, new RectangleF(margin + (pageWidth / 2), yPos - 20, pageWidth / 2, 20), formatCenter); // Tanggal di kanan atas TTD

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
            if (parentForm != null)
            {
                parentForm.ChangeView(new DashboardUC());
                LaporanForm frm = new LaporanForm();
                frm.ShowDialog();
            }
        }
    }
}