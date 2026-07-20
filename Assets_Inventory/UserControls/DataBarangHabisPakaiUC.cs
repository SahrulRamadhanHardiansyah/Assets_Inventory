using Assets_Inventory.Forms;
using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace Assets_Inventory
{
    public partial class DataBarangHabisPakaiUC : UserControl
    {
        private int _currentPage = 0;
        private const int _pageSize = 100;
        private int _totalRecords = 0;
        private Button _btnPrev;
        private Button _btnNext;

        public class BarangHabisPakaiViewModel
        {
            public string KodeBarang { get; set; }
            public string NamaBarang { get; set; }
            public int StokAwal { get; set; }
            public int StokAktual { get; set; }
            public string Ruang { get; set; }
            public string Status { get; set; }
            public string DapatDipinjam { get; set; }
            public DateTime TanggalMasuk { get; set; }
            public AsetHabisPakai ObjekAsli { get; set; }
        }

        public DataBarangHabisPakaiUC()
        {
            InitializeComponent();
        }

        private void DataBarangHabisPakaiUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Data Barang Habis Pakai");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainForm parentForm = this.ParentForm as MainForm;
                DashboardUC dashboardUC = new DashboardUC();
                if (parentForm != null) parentForm.ChangeView(dashboardUC);
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnUbah.Enabled = hakAkses.HakUbah;
            btnHapus.Enabled = hakAkses.HakHapus;
            btnImport.Enabled = hakAkses.HakBuat;

            cmbJumlah.Items.Clear();
            cmbJumlah.Items.Add("1");
            cmbJumlah.Items.Add("Semua");
            cmbJumlah.SelectedIndex = 0;

            cmbJenisBarcode.Items.Clear();
            cmbJenisBarcode.Items.Add("Code 128");
            cmbJenisBarcode.Items.Add("Code 39");
            cmbJenisBarcode.Items.Add("QR Code");
            cmbJenisBarcode.SelectedIndex = 0;

            EnsurePagingControls();
            loadData();
        }
        private void loadData(bool resetPage = true)
        {
            if (resetPage) _currentPage = 0;
            var cari = txtCari.Text.Trim();

            List<BarangHabisPakaiViewModel> dataTampil;
            using (var db = new AppDbContext())
            {
                IQueryable<AsetHabisPakai> q = db.AsetHabisPakai
                    .Include(a => a.IdMasterBarangNavigation)
                    .Include(a => a.IdRuangNavigation)
                    .AsNoTracking();

                if (!string.IsNullOrEmpty(cari))
                    q = q.Where(a => a.KodeBarang.Contains(cari) || a.IdMasterBarangNavigation.NamaBarang.Contains(cari));

                _totalRecords = q.Count();

                var page = q.OrderByDescending(a => a.KodeBarang)
                            .Skip(_currentPage * _pageSize)
                            .Take(_pageSize)
                            .ToList();

                dataTampil = page.Select(a => new BarangHabisPakaiViewModel
                {
                    KodeBarang = a.KodeBarang,
                    NamaBarang = a.IdMasterBarangNavigation != null ? a.IdMasterBarangNavigation.NamaBarang : "N/A",
                    StokAwal = a.Stok,
                    StokAktual = a.StokAktual,
                    Ruang = a.IdRuangNavigation != null ? a.IdRuangNavigation.NamaRuang : "-",
                    Status = a.Status ?? "Tersedia",
                    DapatDipinjam = (a.IsReturnable.HasValue && a.IsReturnable.Value) ? "Ya" : "Tidak",
                    TanggalMasuk = a.TanggalRegistrasi,
                    ObjekAsli = a
                }).ToList();
            }

            dg.DataSource = new SortableBindingList<BarangHabisPakaiViewModel>(dataTampil);
            int totalPages = System.Math.Max(1, (int)System.Math.Ceiling((double)_totalRecords / _pageSize));
            lblTotal.Text = $"Total Record : {_totalRecords}  |  Page {_currentPage + 1}/{totalPages} (showing {dataTampil.Count})";

            if (_btnPrev != null) _btnPrev.Enabled = _currentPage > 0;
            if (_btnNext != null) _btnNext.Enabled = (_currentPage + 1) * _pageSize < _totalRecords;

            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;

            if (dg.Columns["KodeBarang"] != null) dg.Columns["KodeBarang"].HeaderText = "Kode Barang";
            if (dg.Columns["NamaBarang"] != null) dg.Columns["NamaBarang"].HeaderText = "Nama Barang";

            if (dg.Columns["StokAwal"] != null)
            {
                dg.Columns["StokAwal"].HeaderText = "Stok Awal";
                dg.Columns["StokAwal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dg.Columns["StokAktual"] != null)
            {
                dg.Columns["StokAktual"].HeaderText = "Stok Aktual";
                dg.Columns["StokAktual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dg.Columns["Ruang"] != null) dg.Columns["Ruang"].HeaderText = "Ruang / Lokasi";
            if (dg.Columns["Status"] != null) dg.Columns["Status"].HeaderText = "Status";
            if (dg.Columns["DapatDipinjam"] != null) dg.Columns["DapatDipinjam"].HeaderText = "Bisa Dipinjam?";

            if (dg.Columns["TanggalMasuk"] != null)
            {
                dg.Columns["TanggalMasuk"].HeaderText = "Tgl. Registrasi";
                dg.Columns["TanggalMasuk"].DefaultCellStyle.Format = "dd MMM yyyy";
            }
        }

        private void EnsurePagingControls()
        {
            if (_btnPrev != null) return;
            _btnPrev = new Button { Text = "< Prev", Size = new Size(65, 23) };
            _btnNext = new Button { Text = "Next >", Size = new Size(65, 23) };
            _btnPrev.Click += (s, e) => { if (_currentPage > 0) { _currentPage--; loadData(false); } };
            _btnNext.Click += (s, e) => { if ((_currentPage + 1) * _pageSize < _totalRecords) { _currentPage++; loadData(false); } };
            var gp = lblTotal?.Parent ?? this;
            var pos = lblTotal != null ? lblTotal.Location : new System.Drawing.Point(10, 10);
            _btnPrev.Location = new System.Drawing.Point(pos.X + 220, pos.Y - 2);
            _btnNext.Location = new System.Drawing.Point(pos.X + 290, pos.Y - 2);
            gp.Controls.Add(_btnPrev);
            gp.Controls.Add(_btnNext);
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtCari_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCari_Click(sender, e);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            InputBarangHabisPakaiForm form = new InputBarangHabisPakaiForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is BarangHabisPakaiViewModel vm)
            {
                AsetHabisPakai aset;
                using (var db = new AppDbContext())
                {
                    aset = db.AsetHabisPakai.Find(vm.KodeBarang);
                }
                InputBarangHabisPakaiForm form = new InputBarangHabisPakaiForm();
                form.selectedAset = aset;
                if (form.ShowDialog() == DialogResult.OK) loadData();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah terlebih dahulu.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is BarangHabisPakaiViewModel vm)
            {
                if (MessageBox.Show($"Yakin ingin menghapus data stok untuk barang '{vm.NamaBarang}'?\n\nPeringatan: Tindakan ini akan menghapus catatan barang habis pakai secara permanen.", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        using (var db = new AppDbContext())
                        {
                            var aset = db.AsetHabisPakai.Find(vm.KodeBarang);
                            if (aset == null) return;
                            db.AsetHabisPakai.Remove(aset);
                            db.SaveChanges();
                        }
                        try { AuditHelper.Log("Data Barang Habis Pakai", vm.KodeBarang?.ToString(), "DELETE", AuditHelper.SerializeObject(vm), null, "Data Barang Habis Pakai"); } catch {}
                        MessageBox.Show("Data berhasil dihapus.");
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus data. Kemungkinan data stok ini masih terikat pada riwayat pengeluaran atau peminjaman.\n\nDetail: " + ex.Message);
                    }
                }
            }
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (cmbJenisBarcode.SelectedIndex == -1 || cmbJumlah.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih Jenis Barcode dan Jumlah terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> daftarKode = new List<string>();

            if (cmbJumlah.SelectedItem.ToString() == "1")
            {
                if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is BarangHabisPakaiViewModel vm)
                {
                    daftarKode.Add(vm.KodeBarang);
                }
                else
                {
                    MessageBox.Show("Silakan pilih satu baris data dari tabel terlebih dahulu."); return;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if (row.DataBoundItem is BarangHabisPakaiViewModel vm && !string.IsNullOrEmpty(vm.KodeBarang))
                    {
                        daftarKode.Add(vm.KodeBarang);
                    }
                }
            }

            if (daftarKode.Count == 0) return;

            try
            {
                BarcodeFormat formatBarcode = BarcodeFormat.CODE_128;
                int lebar = 300, tinggi = 100;
                string jenisPilihan = cmbJenisBarcode.SelectedItem.ToString();

                if (jenisPilihan == "Code 39") formatBarcode = BarcodeFormat.CODE_39;
                else if (jenisPilihan == "QR Code") { formatBarcode = BarcodeFormat.QR_CODE; lebar = 200; tinggi = 200; }

                Image MakeBarcode(string kode)
                {
                    if (jenisPilihan == "QR Code") return QrCodeHelper.GenerateQrCode(kode, lebar, tinggi);
                    return QrCodeHelper.GenerateBarcode128(kode, lebar, tinggi);
                }
                var writer = new BarcodeWriter { Format = formatBarcode, Options = new EncodingOptions { Height = tinggi, Width = lebar, Margin = 1 } };
                PrintDocument pd = new PrintDocument();
                int currentItemIndex = 0;

                pd.BeginPrint += (s, ev) => { currentItemIndex = 0; };
                pd.PrintPage += (s, ev) =>
                {
                    float x = ev.MarginBounds.Left, y = ev.MarginBounds.Top;
                    float itemWidth = lebar + 20, itemHeight = tinggi + 40;

                    while (currentItemIndex < daftarKode.Count)
                    {
                        string kode = daftarKode[currentItemIndex];
                        Image img = MakeBarcode(kode) ?? writer.Write(kode);
                        ev.Graphics.DrawImage(img, x, y, lebar, tinggi);

                        Font fontTeks = new Font("Arial", 10, FontStyle.Bold);
                        SizeF textSize = ev.Graphics.MeasureString(kode, fontTeks);
                        ev.Graphics.DrawString(kode, fontTeks, Brushes.Black, x + ((lebar - textSize.Width) / 2), y + tinggi + 5);

                        currentItemIndex++;
                        x += itemWidth;

                        if (x + itemWidth > ev.MarginBounds.Right) { x = ev.MarginBounds.Left; y += itemHeight; }
                        if (y + itemHeight > ev.MarginBounds.Bottom && currentItemIndex < daftarKode.Count)
                        {
                            ev.HasMorePages = true; return;
                        }
                    }
                    ev.HasMorePages = false;
                };

                PrintPreviewDialog preview = new PrintPreviewDialog { Document = pd, Width = 850, Height = 600, ShowIcon = false };
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem saat memproses cetak:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel & CSV Files|*.xlsx;*.xls;*.csv|Semua File (*.*)|*.*";
                ofd.Title = "Pilih File Data Barang Habis Pakai";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int berhasil = 0;

                        using (var db = new AppDbContext())
                        {
                            using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                            {
                                using (var reader = ExcelReaderFactory.CreateReader(stream))
                                {
                                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                    {
                                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                    });

                                    DataTable dt = result.Tables[0];

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        string idMasterStr = row[0]?.ToString().Trim();
                                        string stokStr = row[1]?.ToString().Trim();
                                        string idRuangStr = row[2]?.ToString().Trim();
                                        string returnableStr = row[3]?.ToString().Trim().ToLower();
                                        string ket = row[4]?.ToString().Trim();

                                        if (int.TryParse(idMasterStr, out int idMaster))
                                        {
                                            int stok = int.TryParse(stokStr, out int st) ? st : 0;
                                            var baru = new AsetHabisPakai
                                            {
                                                KodeBarang = "KB-HP-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4),
                                                IdMasterBarang = idMaster,
                                                Stok = stok,
                                                StokAktual = stok,
                                                IdRuang = int.TryParse(idRuangStr, out int idR) ? idR : (int?)null,
                                                IsReturnable = (returnableStr == "ya" || returnableStr == "true" || returnableStr == "1"),
                                                Keterangan = ket,
                                                Status = stok > 0 ? "Tersedia" : "Habis",
                                                TanggalRegistrasi = DateTime.Now
                                            };

                                            db.AsetHabisPakai.Add(baru);
                                            berhasil++;
                                        }
                                    }
                                }
                            }

                            db.SaveChanges();
                        }

                        MessageBox.Show($"Import selesai! {berhasil} data stok habis pakai berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka atau digunakan oleh program lain (seperti Microsoft Excel). Harap tutup file tersebut dan coba lagi.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file. Pastikan format kolom sesuai dengan template baru (IdMasterBarang, Stok, IdRuang, DapatDipinjam, Keterangan).\n\nDetail Error: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dg.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diexport.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                sfd.FileName = "Laporan_AsetHabisPakai_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
                sfd.Title = "Export Data Aset Habis Pakai ke Excel";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;

                        Dictionary<int, string> dictBarang;
                        Dictionary<int, string> dictRuang;
                        List<AsetHabisPakai> dataList;

                        using (var db = new AppDbContext())
                        {
                            dictBarang = db.MasterBarang.ToDictionary(b => b.IdMasterBarang, b => b.NamaBarang);
                            dictRuang = db.Ruang.ToDictionary(r => r.IdRuang, r => r.NamaRuang);
                            dataList = db.AsetHabisPakai.AsNoTracking().ToList();
                        }

                        using (var package = new OfficeOpenXml.ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Data Aset Habis Pakai");

                            string[] headers = { "Kode Barang", "Nama Barang", "Stok Awal", "Stok Aktual", "Ruang Tujuan", "Status", "Dapat Dipinjam", "Tanggal Registrasi", "Keterangan" };
                            for (int i = 0; i < headers.Length; i++)
                            {
                                worksheet.Cells[1, i + 1].Value = headers[i];
                            }

                            using (var range = worksheet.Cells[1, 1, 1, headers.Length])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            }

                            int baris = 2;
                            foreach (var item in dataList)
                            {
                                string namaBrg = dictBarang.ContainsKey(item.IdMasterBarang) ? dictBarang[item.IdMasterBarang] : "N/A";
                                string namaRuang = (item.IdRuang.HasValue && dictRuang.ContainsKey(item.IdRuang.Value)) ? dictRuang[item.IdRuang.Value] : "-";
                                string dptDipinjam = (item.IsReturnable.HasValue && item.IsReturnable.Value) ? "Ya" : "Tidak";

                                worksheet.Cells[baris, 1].Value = item.KodeBarang;
                                worksheet.Cells[baris, 2].Value = namaBrg;
                                worksheet.Cells[baris, 3].Value = item.Stok;
                                worksheet.Cells[baris, 4].Value = item.StokAktual;
                                worksheet.Cells[baris, 5].Value = namaRuang;
                                worksheet.Cells[baris, 6].Value = item.Status ?? "-";
                                worksheet.Cells[baris, 7].Value = dptDipinjam;

                                worksheet.Cells[baris, 8].Value = item.TanggalRegistrasi;
                                worksheet.Cells[baris, 8].Style.Numberformat.Format = "dd-mm-yyyy hh:mm";

                                worksheet.Cells[baris, 9].Value = item.Keterangan ?? "-";

                                baris++;
                            }

                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                            FileInfo fi = new FileInfo(sfd.FileName);
                            package.SaveAs(fi);
                        }

                        MessageBox.Show("Data berhasil diekspor ke Excel!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Gagal menyimpan. Pastikan file Excel tujuan tidak sedang dibuka oleh program lain.", "Error Akses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat mengekspor data: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Enabled = true;
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
    }
}
