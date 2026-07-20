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
    public partial class LaporanBangunanInventarisUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        PrintDocument printDoc = new PrintDocument();
        List<LaporanBangunanViewModel> printData = new List<LaporanBangunanViewModel>();
        int currentPrintIndex = 0;

        public class LaporanBangunanViewModel
        {
            public int KodeBangunan { get; set; }
            public string NamaBangunan { get; set; }
            public int LuasBangunan { get; set; }
            public string Ukuran { get; set; }
            public string Konstruksi { get; set; }
            public string StatusBangunan { get; set; }
            public decimal NilaiAset { get; set; }
            public string TanggalBangunan { get; set; }
            public string Kondisi { get; set; }
            public string Status { get; set; }
            public string Keterangan { get; set; }
        }

        public LaporanBangunanInventarisUC()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += LaporanBangunanInventarisUC_Load;

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

        private void LaporanBangunanInventarisUC_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        }

        private void LoadComboBoxes()
        {
            var cnbKondisi = this.Controls.Find("cnbKondisi", true).FirstOrDefault() as ComboBox;
            var cmbStatus = this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox;
            var rbSemua = this.Controls.Find("rbSemua", true).FirstOrDefault() as RadioButton;

            if (cnbKondisi != null)
            {
                var list = db.Kondisi.ToList();
                list.Insert(0, new Kondisi { IdKondisi = 0, NamaKondisi = "-- Semua Kondisi --" });
                cnbKondisi.DataSource = list;
                cnbKondisi.DisplayMember = "NamaKondisi";
                cnbKondisi.ValueMember = "IdKondisi";
            }

            if (cmbStatus != null)
            {
                cmbStatus.Items.Clear();
                cmbStatus.Items.AddRange(new object[] { "Aktif", "Nonaktif" });
                cmbStatus.SelectedIndex = 0;
            }

            if (rbSemua != null) rbSemua.Checked = true;
        }

        private void BtnTampilkanData_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var rbKondisi = this.Controls.Find("rbKondisi", true).FirstOrDefault() as RadioButton;
            var cnbKondisi = this.Controls.Find("cnbKondisi", true).FirstOrDefault() as ComboBox;
            var rbStatus = this.Controls.Find("rbStatus", true).FirstOrDefault() as RadioButton;
            var cmbStatus = this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox;

            var query = db.VAsetBangunan.AsNoTracking().AsQueryable();

            if (rbKondisi != null && rbKondisi.Checked && cnbKondisi != null && cnbKondisi.SelectedValue != null)
            {
                int idKondisi = (int)cnbKondisi.SelectedValue;
                if (idKondisi != 0)
                {
                    string nmKondisi = db.Kondisi.Find(idKondisi)?.NamaKondisi;
                    query = query.Where(v => v.NamaKondisi == nmKondisi);
                }
            }
            else if (rbStatus != null && rbStatus.Checked && cmbStatus != null && cmbStatus.SelectedItem != null)
            {
                string statusFilter = cmbStatus.SelectedItem.ToString();
                query = query.Where(v => v.Status == statusFilter);
            }

            printData = query.ToList().Select(v => new LaporanBangunanViewModel
            {
                KodeBangunan = v.KodeBangunan,
                NamaBangunan = v.NamaBangunan ?? "-",
                LuasBangunan = v.LuasBangunan,
                Ukuran = $"{v.UkuranP?.ToString("N1") ?? "-"} x {v.UkuranL?.ToString("N1") ?? "-"}",
                Konstruksi = v.Konstruksi ?? "-",
                StatusBangunan = v.StatusBangunan ?? "-",
                NilaiAset = v.NilaiAset ?? 0m,
                TanggalBangunan = v.TanggalBangunan?.ToString("dd-MM-yyyy") ?? "-",
                Kondisi = v.NamaKondisi ?? "-",
                Status = v.Status ?? "-",
                Keterangan = v.Keterangan ?? "-"
            }).OrderBy(x => x.KodeBangunan).ToList();

            var dgv = this.Controls.Find("dg", true).FirstOrDefault() as DataGridView;
            var lblRec = this.Controls.Find("lblRecord", true).FirstOrDefault() as Label;

            if (dgv != null)
            {
                dgv.DataSource = new SortableBindingList<LaporanBangunanViewModel>(printData);

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

            g.DrawString("LAPORAN BANGUNAN / GEDUNG INVENTARIS", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos, pageWidth, 20), formatCenter);
            g.DrawString($"TAHUN {DateTime.Now.Year}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new RectangleF(margin, yPos + 20, pageWidth, 20), formatCenter);
            yPos += 50;

            string[] headers = { "No", "Kode", "Nama Bangunan", "Luas", "Ukuran (PxL)", "Konstruksi", "Status Bgn", "Nilai Aset", "Tgl Bangunan", "Kondisi", "Status" };
            int[] colWidths = { 25, 45, 150, 50, 80, 90, 80, 110, 85, 70, 60 };

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
                    item.KodeBangunan.ToString(),
                    item.NamaBangunan,
                    item.LuasBangunan.ToString("N0"),
                    item.Ukuran,
                    item.Konstruksi,
                    item.StatusBangunan,
                    item.NilaiAset.ToString("N0"),
                    item.TanggalBangunan,
                    item.Kondisi,
                    item.Status
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(currentX, yPos, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, currentX, yPos, colWidths[i], rowHeight);

                    StringFormat fmt = formatMidLeft;
                    if (i == 0 || i == 1 || i == 3 || i == 4 || i == 8 || i == 9 || i == 10) fmt = formatMidCenter;
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
                sfd.FileName = "Laporan_Bangunan_Inventaris_" + DateTime.Now.ToString("yyyyMMdd");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("Bangunan Inventaris");

                            ws.Cells["A1:K1"].Merge = true;
                            ws.Cells["A1"].Value = "LAPORAN BANGUNAN / GEDUNG INVENTARIS";
                            ws.Cells["A1"].Style.Font.Bold = true;
                            ws.Cells["A1"].Style.Font.Size = 14;
                            ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            string[] headers = { "No", "Kode Bangunan", "Nama Bangunan", "Luas Bangunan", "Ukuran (PxL)", "Konstruksi", "Status Bangunan", "Nilai Aset", "Tgl Bangunan", "Kondisi", "Status", "Keterangan" };
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
                                ws.Cells[row, 2].Value = item.KodeBangunan;
                                ws.Cells[row, 3].Value = item.NamaBangunan;
                                ws.Cells[row, 4].Value = item.LuasBangunan;
                                ws.Cells[row, 5].Value = item.Ukuran;
                                ws.Cells[row, 6].Value = item.Konstruksi;
                                ws.Cells[row, 7].Value = item.StatusBangunan;

                                ws.Cells[row, 8].Value = item.NilaiAset;
                                ws.Cells[row, 8].Style.Numberformat.Format = "Rp #,##0.00";

                                ws.Cells[row, 9].Value = item.TanggalBangunan;
                                ws.Cells[row, 10].Value = item.Kondisi;
                                ws.Cells[row, 11].Value = item.Status;
                                ws.Cells[row, 12].Value = item.Keterangan;

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
