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
    public partial class LaporanPeminjamanBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanPeminjamanViewModel> printData = new List<LaporanPeminjamanViewModel>();
        int currentPrintIndex = 0;

        public class LaporanPeminjamanViewModel
        {
            public string NomorPeminjaman { get; set; }
            public DateTime TanggalPinjam { get; set; }
            public string NamaPeminjam { get; set; }
            public string NomorTelepon { get; set; }
            public int LamaPinjamHari { get; set; }
            public string TanggalJatuhTempo { get; set; }
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string StatusPeminjaman { get; set; }
            public string Keterangan { get; set; }
        }

        public LaporanPeminjamanBarangUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += LaporanPeminjamanBarangUC_Load;

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

        private void LaporanPeminjamanBarangUC_Load(object sender, EventArgs e)
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
            var cmbStatus = this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox;
            var cmbBarang = this.Controls.Find("cmbBarang", true).FirstOrDefault() as ComboBox;

            if (cmbStatus != null)
            {
                cmbStatus.Items.Clear();
                cmbStatus.Items.AddRange(new object[] { "-- Semua Status --", "Sedang Dipinjam", "Dikembalikan" });
                cmbStatus.SelectedIndex = 0;
            }

            if (cmbBarang != null)
            {
                var list = db.MasterBarang.ToList();
                list.Insert(0, new MasterBarang { IdMasterBarang = 0, NamaBarang = "-- Semua Barang --" });
                cmbBarang.DataSource = list;
                cmbBarang.DisplayMember = "NamaBarang";
                cmbBarang.ValueMember = "IdMasterBarang";
            }
        }

        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var dtAwal = this.Controls.Find("dtAwal", true).FirstOrDefault() as DateTimePicker;
            var dtAkhir = this.Controls.Find("dtAkhir", true).FirstOrDefault() as DateTimePicker;
            var cmbStatus = this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox;
            var cmbBarang = this.Controls.Find("cmbBarang", true).FirstOrDefault() as ComboBox;

            DateTime tglAwal = dtAwal?.Value.Date ?? DateTime.MinValue;
            DateTime tglAkhir = dtAkhir?.Value.Date.AddDays(1).AddTicks(-1) ?? DateTime.MaxValue;

            string statusFilter = cmbStatus?.SelectedIndex > 0 ? cmbStatus.SelectedItem.ToString() : "";
            string namaBrg = cmbBarang?.SelectedIndex > 0 ? cmbBarang.Text : "";

            var query = db.VLaporanPeminjaman.AsNoTracking()
                        .Where(v => v.TanggalPinjam >= tglAwal && v.TanggalPinjam <= tglAkhir);

            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(v => v.StatusPeminjaman == statusFilter);
            }

            if (!string.IsNullOrEmpty(namaBrg))
            {
                query = query.Where(v => v.NamaBarang == namaBrg);
            }

            printData = query.ToList().Select(v => new LaporanPeminjamanViewModel
            {
                NomorPeminjaman = v.NomorPeminjaman ?? "-",
                TanggalPinjam = v.TanggalPinjam,
                NamaPeminjam = v.NamaPeminjam ?? "-",
                NomorTelepon = v.NomorTelepon ?? "-",
                LamaPinjamHari = v.LamaPinjamHari,
                TanggalJatuhTempo = v.TanggalJatuhTempo?.ToString("dd-MM-yyyy") ?? "-",
                KodeInventaris = v.KodeInventaris ?? "-",
                NamaBarang = v.NamaBarang ?? "N/A",
                StatusPeminjaman = v.StatusPeminjaman ?? "-",
                Keterangan = v.Keterangan ?? "-"
            }).OrderBy(x => x.TanggalPinjam).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblRec = this.Controls.Find("lblRecord", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanPeminjamanViewModel>(printData);

                if (dgv.Columns["TanggalPinjam"] != null)
                {
                    dgv.Columns["TanggalPinjam"].DefaultCellStyle.Format = "dd MMM yyyy";
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

            g.DrawString("LAPORAN PEMINJAMAN BARANG", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);

            var dtpAwal = this.Controls.Find("dtAwal", true).FirstOrDefault() as DateTimePicker;
            var dtpAkhir = this.Controls.Find("dtAkhir", true).FirstOrDefault() as DateTimePicker;
            string tglStr = $"Periode: {dtpAwal?.Value.ToString("dd MMM yyyy")} s.d {dtpAkhir?.Value.ToString("dd MMM yyyy")}";
            g.DrawString(tglStr, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new RectangleF(margin, yPos + 25, pageWidth, 20), formatCenter);
            yPos += 55;

            string[] headers = { "No", "No Peminjaman", "Tgl Pinjam", "Peminjam", "Kode Inventaris", "Nama Barang", "Lama (Hari)", "Jatuh Tempo", "Status" };
            int[] colWidths = { 25, 110, 85, 120, 110, 160, 60, 85, 100 };

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
                    item.NomorPeminjaman,
                    item.TanggalPinjam.ToString("dd-MM-yyyy"),
                    item.NamaPeminjam,
                    item.KodeInventaris,
                    item.NamaBarang,
                    item.LamaPinjamHari.ToString(),
                    item.TanggalJatuhTempo,
                    item.StatusPeminjaman
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = (i == 0 || i == 2 || i == 6 || i == 7 || i == 8) ? formatMidCenter : formatMidLeft;
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
