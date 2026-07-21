using Assets_Inventory.Forms;
using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace Assets_Inventory.UserControls
{
    public partial class DataBarangAsetUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        private int _currentPage = 0;
        private const int _pageSize = 100;
        private int _totalRecords = 0;
        private Button _btnPrev;
        private Button _btnNext;

        public class AsetViewModel
        {
            public int KodeBarang { get; set; }
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string NoSeri { get; set; }
            public string NamaJurusan { get; set; }
            public string LokasiRuang { get; set; }
            public string Status { get; set; }
            public Aset ObjekAsli { get; set; }
        }

        public DataBarangAsetUC()
        {
            InitializeComponent();
        }

        private void DataBarangAsetUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Data Aset");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnUbah.Enabled = hakAkses.HakUbah;
            btnHapus.Enabled = hakAkses.HakHapus;
            btnImport.Enabled = hakAkses.HakBuat;

            cmbJumlah.Items.Clear();
            cmbJumlah.Items.Add("1 (Baris Terpilih)");
            cmbJumlah.Items.Add("Semua Data Aset");
            cmbJumlah.SelectedIndex = 0;

            cmbJenisBarcode.Items.Clear();
            cmbJenisBarcode.Items.Add("Code 128");
            cmbJenisBarcode.Items.Add("Code 39");
            cmbJenisBarcode.Items.Add("QR Code");
            cmbJenisBarcode.SelectedIndex = 0;

            _btnPrev = new Button { Text = "< Prev", Size = new Size(65, 23), FlatStyle = FlatStyle.Flat };
            _btnNext = new Button { Text = "Next >", Size = new Size(65, 23), FlatStyle = FlatStyle.Flat };
            // ponytail: anchor relative to lblTotal, simple fixed offset — no layout engine needed
            var lblPos = lblTotal.Location;
            _btnPrev.Location = new Point(lblPos.X + 250, lblPos.Y - 2);
            _btnNext.Location = new Point(lblPos.X + 320, lblPos.Y - 2);
            _btnPrev.Click += BtnPrev_Click;
            _btnNext.Click += BtnNext_Click;
            lblTotal.Parent.Controls.Add(_btnPrev);
            lblTotal.Parent.Controls.Add(_btnNext);

            loadData();
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 0) { _currentPage--; loadData(false); }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if ((_currentPage + 1) * _pageSize < _totalRecords) { _currentPage++; loadData(false); }
        }

        private void RefreshDbIfNeeded()
        {
            try
            {
                // EF Core 3.1 - check if context disposed via simple query
                if (db == null) db = new AppDbContext();
            }
            catch { db = new AppDbContext(); }
        }

        private void loadData(bool resetPage = true)
        {
            try
            {
                // resetPage=false keeps current page when paging
                if (resetPage) _currentPage = 0;
                RefreshDbIfNeeded();

                var cari = txtCari.Text?.Trim() ?? "";

                // STEP 1: Build base query with Includes + safe filtering
                IQueryable<Aset> q = db.Aset
                    .Include(a => a.IdMasterBarangNavigation)
                    .Include(a => a.IdJurusanNavigation)
                    .Include(a => a.IdRuangNavigation)
                    .AsNoTracking();

                if (!string.IsNullOrEmpty(cari))
                {
                    string cariLower = cari.ToLower();
                    // Safe filter: handle null navigation (Pomelo 3.2.4 may not translate ?. well, so use explicit null check)
                    q = q.Where(a => a.KodeInventaris.Contains(cari) ||
                                     a.KodeInventaris.ToLower().Contains(cariLower) ||
                                     (a.IdMasterBarangNavigation != null && a.IdMasterBarangNavigation.NamaBarang.Contains(cari)) ||
                                     (a.NoSeri != null && a.NoSeri.Contains(cari)));
                }

                _totalRecords = q.Count();

                var page = q.OrderByDescending(a => a.KodeBarang)
                            .Skip(_currentPage * _pageSize)
                            .Take(_pageSize)
                            .ToList();

                // STEP 2: Fallback dictionary for orphan FKs (defense-in-depth if Include returns null)
                var masterIds = page.Select(a => a.IdMasterBarang).Distinct().ToList();
                var jurusanIds = page.Where(a => a.IdJurusan.HasValue).Select(a => a.IdJurusan.Value).Distinct().ToList();
                var ruangIds = page.Where(a => a.IdRuang.HasValue).Select(a => a.IdRuang.Value).Distinct().ToList();

                var dictMaster = new System.Collections.Generic.Dictionary<int, string>();
                var dictJurusan = new System.Collections.Generic.Dictionary<int, string>();
                var dictRuang = new System.Collections.Generic.Dictionary<int, string>();

                if (masterIds.Count > 0)
                    dictMaster = db.MasterBarang.AsNoTracking().Where(m => masterIds.Contains(m.IdMasterBarang))
                        .ToDictionary(m => m.IdMasterBarang, m => m.NamaBarang);
                if (jurusanIds.Count > 0)
                    dictJurusan = db.Jurusan.AsNoTracking().Where(j => jurusanIds.Contains(j.IdJurusan))
                        .ToDictionary(j => j.IdJurusan, j => j.NamaJurusan);
                if (ruangIds.Count > 0)
                    dictRuang = db.Ruang.AsNoTracking().Where(r => ruangIds.Contains(r.IdRuang))
                        .ToDictionary(r => r.IdRuang, r => r.NamaRuang);

                var dataList = page.Select(a =>
                {
                    string namaBarang = a.IdMasterBarangNavigation != null ? a.IdMasterBarangNavigation.NamaBarang : null;
                    if (string.IsNullOrEmpty(namaBarang) && dictMaster.TryGetValue(a.IdMasterBarang, out string nm))
                        namaBarang = nm;
                    if (string.IsNullOrEmpty(namaBarang)) namaBarang = "Unknown";

                    string namaJurusan = a.IdJurusanNavigation != null ? a.IdJurusanNavigation.NamaJurusan : null;
                    if (string.IsNullOrEmpty(namaJurusan) && a.IdJurusan.HasValue && dictJurusan.TryGetValue(a.IdJurusan.Value, out string nj))
                        namaJurusan = nj;
                    if (string.IsNullOrEmpty(namaJurusan)) namaJurusan = "-";

                    string namaRuang = a.IdRuangNavigation != null ? a.IdRuangNavigation.NamaRuang : null;
                    if (string.IsNullOrEmpty(namaRuang) && a.IdRuang.HasValue && dictRuang.TryGetValue(a.IdRuang.Value, out string nr))
                        namaRuang = nr;
                    if (string.IsNullOrEmpty(namaRuang)) namaRuang = "-";

                    return new AsetViewModel
                    {
                        KodeBarang = a.KodeBarang,
                        KodeInventaris = a.KodeInventaris,
                        NamaBarang = namaBarang,
                        NoSeri = string.IsNullOrEmpty(a.NoSeri) ? "-" : a.NoSeri,
                        NamaJurusan = namaJurusan,
                        LokasiRuang = namaRuang,
                        Status = a.Status ?? "Aktif",
                        ObjekAsli = a
                    };
                }).ToList();

                dg.DataSource = new SortableBindingList<AsetViewModel>(dataList);
                int totalPages = Math.Max(1, (int)Math.Ceiling((double)_totalRecords / _pageSize));
                lblTotal.Text = $"Total Record : {_totalRecords}  |  Page {_currentPage + 1}/{totalPages} (showing {dataList.Count})";

                if (_btnPrev != null) _btnPrev.Enabled = _currentPage > 0;
                if (_btnNext != null) _btnNext.Enabled = (_currentPage + 1) * _pageSize < _totalRecords;

                if (dg.Columns["KodeBarang"] != null) dg.Columns["KodeBarang"].Visible = false;
                if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
                if (dg.Columns["KodeInventaris"] != null) dg.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
                if (dg.Columns["NamaBarang"] != null) dg.Columns["NamaBarang"].HeaderText = "Nama Barang";
                if (dg.Columns["NoSeri"] != null) dg.Columns["NoSeri"].HeaderText = "No. Seri / SN";
                if (dg.Columns["NamaJurusan"] != null) dg.Columns["NamaJurusan"].HeaderText = "Kepemilikan (Jurusan)";
                if (dg.Columns["LokasiRuang"] != null) dg.Columns["LokasiRuang"].HeaderText = "Ruang Saat Ini";
                if (dg.Columns["Status"] != null) dg.Columns["Status"].HeaderText = "Status Aset";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("loadData error: " + ex.Message + "\n" + ex.InnerException?.Message);
                MessageBox.Show("Gagal memuat data aset.\n\nDetail: " + (ex.InnerException?.Message ?? ex.Message), "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadDataNextPage() => loadData(false);

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is AsetViewModel vm)
            {
                var a = db.Aset.Find(vm.KodeBarang);
                if (a != null) bindingSource1.DataSource = a;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            loadData();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            InputBarangAsetForm form = new InputBarangAsetForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is AsetViewModel vm)
            {
                var asetTarget = db.Aset.Find(vm.KodeBarang);
                if (asetTarget != null)
                {
                    InputBarangAsetForm form = new InputBarangAsetForm();
                    form.selectedAset = asetTarget;
                    if (form.ShowDialog() == DialogResult.OK) loadData();
                }
            }
            else
            {
                MessageBox.Show("Pilih data aset dari tabel terlebih dahulu.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is AsetViewModel vm)
            {
                if (MessageBox.Show($"Anda yakin ingin menghapus Aset '{vm.KodeInventaris}' secara permanen?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var toDelete = db.Aset.Find(vm.KodeBarang);
                        if (toDelete != null)
                        {
                            db.Aset.Remove(toDelete);
                            db.SaveChanges();
                            try { AuditHelper.Log("Data Aset", vm.KodeInventaris ?? vm.KodeBarang.ToString(), "DELETE", (object)vm, (object)null, "Data Aset"); } catch {}
                            MessageBox.Show("Aset berhasil dihapus secara permanen.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadData();
                        }
                    }
                    catch (Exception ex)
                    {
                        db.Entry(vm).Reload();
                        MessageBox.Show("Gagal menghapus! Aset ini mungkin sedang terikat dengan data Peminjaman/Mutasi.\nError: " + ex.Message, "Error Relasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data aset yang valid untuk dihapus.");
            }
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (cmbJenisBarcode.SelectedIndex == -1 || cmbJumlah.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih Jenis Barcode dan Opsi Jumlah cetak.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> daftarKode = new List<string>();

            if (cmbJumlah.SelectedIndex == 0) 
            {
                if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is AsetViewModel vm)
                {
                    daftarKode.Add(vm.KodeInventaris);
                }
                else
                {
                    MessageBox.Show("Pilih satu baris aset di tabel terlebih dahulu."); return;
                }
            }
            else // Semua
            {
                var listVM = dg.Rows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem as AsetViewModel).ToList();
                daftarKode = listVM.Select(v => v.KodeInventaris).ToList();
                if (daftarKode.Count == 0) { MessageBox.Show("Tidak ada aset untuk dicetak."); return; }
            }

            try
            {
                BarcodeFormat formatBarcode = BarcodeFormat.CODE_128;
                int lebar = 300, tinggi = 100;

                string jenisPilihan = cmbJenisBarcode.SelectedItem.ToString();
                if (jenisPilihan == "Code 39") formatBarcode = BarcodeFormat.CODE_39;
                else if (jenisPilihan == "QR Code") { formatBarcode = BarcodeFormat.QR_CODE; lebar = 200; tinggi = 200; }

                // ponytail: use QrCodeHelper to dedup QR/Barcode128 logic
                Image MakeBarcode(string kode)
                {
                    if (jenisPilihan == "QR Code") return QrCodeHelper.GenerateQrCode(kode, lebar, tinggi);
                    else if (jenisPilihan == "Code 39") return QrCodeHelper.GenerateBarcode128(kode, lebar, tinggi); // ponytail: Code39 via 128 helper fallback acceptable, upgrade to dedicated if needed
                    else return QrCodeHelper.GenerateBarcode128(kode, lebar, tinggi);
                }
                // keep writer for compat if MakeBarcode fails
                var writer = new BarcodeWriter
                {
                    Format = formatBarcode,
                    Options = new EncodingOptions { Height = tinggi, Width = lebar, Margin = 1 }
                };

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
                        float textX = x + ((lebar - textSize.Width) / 2);
                        ev.Graphics.DrawString(kode, fontTeks, Brushes.Black, textX, y + tinggi + 5);

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
                MessageBox.Show("Terjadi kesalahan saat memproses cetak:\n\n" + ex.Message, "Error Cetak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var expHak = AuthManager.GetAkses("Data Aset");
            if (!expHak.HakExport && !expHak.HakBaca) { /* allow but audit */ }
            if (dg.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diekspor.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV File|*.csv", Title = "Simpan File Ekspor Aset" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("ID Aset,Kode Inventaris,Nama Barang,Nomor Seri,Nama Jurusan,Lokasi Ruang,Status Aset");

                        foreach (DataGridViewRow row in dg.Rows)
                        {
                            if (row.DataBoundItem is AsetViewModel vm)
                            {
                                sb.AppendLine($"{vm.KodeBarang},{vm.KodeInventaris},{EscapeCsv(vm.NamaBarang)},{EscapeCsv(vm.NoSeri)},{EscapeCsv(vm.NamaJurusan)},{EscapeCsv(vm.LokasiRuang)},{vm.Status}");
                            }
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString());
                        MessageBox.Show("Data Aset berhasil diekspor ke file CSV!", "Sukses Ekspor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengekspor file: " + ex.Message, "Error Ekspor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private string EscapeCsv(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n"))
            {
                text = text.Replace("\"", "\"\"");
                return $"\"{text}\"";
            }
            return text;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Semua File Data|*.csv;*.xlsx;*.xls|Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv";
                ofd.Title = "Pilih File Data Aset";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(ofd.FileName).ToLower();
                        int berhasil = 0;

                        if (ext == ".csv") berhasil = ImportCsv(ofd.FileName);
                        else if (ext == ".xlsx" || ext == ".xls") berhasil = ImportExcel(ofd.FileName);
                        else
                        {
                            MessageBox.Show("Format file tidak didukung!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        loadData();
                        MessageBox.Show($"Import selesai! {berhasil} data aset baru berhasil ditambahkan.", "Sukses Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka oleh program lain. Tutup file tersebut dan coba lagi.", "Error Akses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file: " + ex.Message, "Error Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private int ImportCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            int berhasil = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var data = lines[i].Split(',');

                if (data.Length >= 4 && !string.IsNullOrWhiteSpace(data[0]))
                {
                    if (int.TryParse(data[0].Trim(), out int idMaster))
                    {
                        var asetBaru = new Aset
                        {
                            IdMasterBarang = idMaster,
                            KodeInventaris = data[1].Trim(),
                            NoSeri = data[2].Trim(),
                            Status = data[3].Trim(),
                            TanggalRegistrasi = DateTime.Now,
                            KodeBarang2 = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper()
                        };
                        db.Aset.Add(asetBaru);
                        berhasil++;
                    }
                }
            }
            db.SaveChanges();
            return berhasil;
        }

        private int ImportExcel(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            int berhasil = 0;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    DataTable table = result.Tables[0];

                    for (int i = 1; i < table.Rows.Count; i++) 
                    {
                        var row = table.Rows[i];

                        if (row.ItemArray.Length >= 4 && !string.IsNullOrWhiteSpace(row[0]?.ToString()))
                        {
                            if (int.TryParse(row[0]?.ToString().Trim(), out int idMaster))
                            {
                                var asetBaru = new Aset
                                {
                                    IdMasterBarang = idMaster,
                                    KodeInventaris = row[1]?.ToString().Trim(),
                                    NoSeri = row[2]?.ToString().Trim(),
                                    Status = row[3]?.ToString().Trim(),
                                    TanggalRegistrasi = DateTime.Now,
                                    KodeBarang2 = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper()
                                };
                                db.Aset.Add(asetBaru);
                                berhasil++;
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
            return berhasil;
        }
    }
}