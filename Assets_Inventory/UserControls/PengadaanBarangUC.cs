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
using Assets_Inventory.Helper;

namespace Assets_Inventory
{
    public partial class PengadaanBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        public string DefaultStatusFilter = "Semua";

        public class PengadaanViewModel
        {
            public int IdPengadaan { get; set; }
            public DateTime TanggalPengadaan { get; set; }
            public DateTime? WaktuBelanja { get; set; }
            public string TahunAjaran { get; set; }
            public string NamaSumber { get; set; }
            public string NamaGudang { get; set; }
            public string NamaPemasok { get; set; }
            public decimal TotalHarga { get; set; }
            public string Status { get; set; }
            public Pengadaan ObjekAsli { get; set; }
        }

        public PengadaanBarangUC()
        {
            InitializeComponent();
        }

        private void PengadaanBarangUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Pengadaan Barang");

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

            cmbJenis.Items.Clear();
            cmbJenis.Items.Add("Code 128");
            cmbJenis.Items.Add("Code 39");
            cmbJenis.Items.Add("QR Code");
            cmbJenis.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Semua", "Menunggu Proses", "Sedang Dibelanjakan", "Selesai Dibelanjakan" });
            cmbStatus.SelectedItem = DefaultStatusFilter;

            loadData();
        }

        private void loadData()
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cari = txtCari.Text.ToLower().Trim();
            string statusFilter = cmbStatus.SelectedItem?.ToString() ?? "Semua";

            var query = db.Pengadaan.AsNoTracking()
                                  .Where(x => x.IdPengadaan.ToString().Contains(cari));

            if (statusFilter != "Semua")
            {
                query = query.Where(x => x.Status == statusFilter);
            }

            var listPengadaan = query.OrderByDescending(p => p.IdPengadaan).ToList();
            var dictSumber = db.SumberPerolehan.ToDictionary(s => s.IdSumberPerolehan, s => s.NamaSumber);
            var dictGudang = db.Gudang.ToDictionary(g => g.KodeGudang, g => g.NamaGudang);
            var dictTA = db.TahunAjaran.ToDictionary(ta => ta.IdTahunAjaran, ta => $"{ta.TahunAjaran1} - Smt {ta.Semester}");
            var dictRekanan = db.Pemasok.ToDictionary(p => p.IdPemasok, p => p.NamaPemasok);

            var data = listPengadaan.Select(p => new PengadaanViewModel
            {
                IdPengadaan = p.IdPengadaan,
                TanggalPengadaan = p.TanggalPengadaan,
                WaktuBelanja = p.WaktuBelanja,
                TahunAjaran = (p.IdTahunAjaran.HasValue && dictTA.ContainsKey(p.IdTahunAjaran.Value)) ? dictTA[p.IdTahunAjaran.Value] : "-",
                NamaSumber = (p.IdSumberPerolehan.HasValue && dictSumber.ContainsKey(p.IdSumberPerolehan.Value)) ? dictSumber[p.IdSumberPerolehan.Value] : "N/A",
                NamaGudang = (!string.IsNullOrEmpty(p.KodeGudang) && dictGudang.ContainsKey(p.KodeGudang)) ? dictGudang[p.KodeGudang] : "N/A",
                NamaPemasok = (p.IdPemasok.HasValue && dictRekanan.ContainsKey(p.IdPemasok.Value)) ? dictRekanan[p.IdPemasok.Value] : "-",
                TotalHarga = p.TotalHarga ?? 0,
                Status = p.Status ?? "Menunggu Proses",
                ObjekAsli = p
            }).ToList();

            dg.DataSource = new SortableBindingList<PengadaanViewModel>(data);
            lblTotal.Text = $"Total Record: {data.Count}";

            if (dg.Columns["IdPengadaan"] != null) dg.Columns["IdPengadaan"].HeaderText = "ID Pengadaan";
            if (dg.Columns["TanggalPengadaan"] != null) dg.Columns["TanggalPengadaan"].HeaderText = "Tanggal";
            if (dg.Columns["WaktuBelanja"] != null)
            {
                dg.Columns["WaktuBelanja"].HeaderText = "Waktu Belanja (Rencana Pengadaan)";
                dg.Columns["WaktuBelanja"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dg.Columns["TahunAjaran"] != null) dg.Columns["TahunAjaran"].HeaderText = "Tahun Ajaran";
            if (dg.Columns["NamaSumber"] != null) dg.Columns["NamaSumber"].HeaderText = "Sumber Perolehan";
            if (dg.Columns["NamaGudang"] != null) dg.Columns["NamaGudang"].HeaderText = "Gudang Tujuan";
            if (dg.Columns["NamaPemasok"] != null) dg.Columns["NamaPemasok"].HeaderText = "Rekanan";
            // Kolom Status disembunyikan dari grid: posisinya digantikan Waktu Belanja,
            // tapi nilainya tetap dipakai filter cmbStatus + guard tombol.
            if (dg.Columns["Status"] != null) dg.Columns["Status"].Visible = false;
            if (dg.Columns["TotalHarga"] != null)
            {
                dg.Columns["TotalHarga"].HeaderText = "Total Harga";
                dg.Columns["TotalHarga"].DefaultCellStyle.Format = "C2";
                dg.Columns["TotalHarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dg.Columns["DetailColumn"] != null) dg.Columns["DetailColumn"].DisplayIndex = dg.Columns.Count - 1;
            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is PengadaanViewModel vm)
            {
                Pengadaan p = vm.ObjekAsli;
                var pengadaan = db.Pengadaan.Find(p.IdPengadaan);
                if (pengadaan != null)
                {
                    bindingSource1.DataSource = pengadaan;
                }
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            cmbStatus.SelectedIndex = 0;
            loadData();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            InputPengadaanBarangForm form = new InputPengadaanBarangForm();
            if (DialogResult.OK == form.ShowDialog()) loadData();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pengadaan p)
            {
                if (p.Status == "Selesai Dibelanjakan" || p.Status == "Sedang Dibelanjakan")
                {
                    MessageBox.Show("Pengadaan yang sedang atau selesai dibelanjakan tidak dapat diubah susunan keranjangnya lagi. Anda hanya bisa melihat detailnya.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pengadaan = db.Pengadaan.Find(p.IdPengadaan);
                InputPengadaanBarangForm form = new InputPengadaanBarangForm();
                form.selectedPengadaan = pengadaan;
                if (form.ShowDialog() == DialogResult.OK) loadData();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pengadaan k)
            {
                if (k.Status != "Menunggu Proses")
                {
                    MessageBox.Show("Hanya pengadaan dengan status 'Menunggu Proses' yang dapat dihapus. Jika barang sudah masuk gudang, Anda harus membatalkannya melalui menu lain.", "Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data Pengadaan ID: {k.IdPengadaan}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var toDelete = db.Pengadaan.Find(k.IdPengadaan);
                        db.Pengadaan.Remove(toDelete);
                        db.SaveChanges();
                        try { AuditHelper.Log("Pengadaan", k?.IdPengadaan.ToString() ?? "", "DELETE", AuditHelper.SerializeObject(k), null, "Pengadaan"); } catch {}

                        MessageBox.Show("Berhasil dihapus!");
                        loadData();
                        bindingSource1.AddNew();
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        db.Entry(k).Reload();
                        MessageBox.Show("Tidak dapat menghapus data ini karena data masih terikat dengan detail barang.", "Peringatan Relasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        db.Entry(k).Reload();
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem saat menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang valid untuk dihapus.");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            if (!ImportTemplateHelper.ConfirmOrGenerate(this, "Import Pengadaan Barang",
                new[] { ("Tanggal Pengadaan", "2026-11-09"),("Rekanan (ID atau Nama)", "Pemasok Umum"),("Total Harga", "250000"),("Kode Gudang", "GDG-001"),("Status", "Menunggu Proses"),("Keterangan", "") }))
                return;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Semua File Data|*.csv;*.xlsx;*.xls|Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv";
                ofd.Title = "Pilih File Data Pengadaan";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(ofd.FileName).ToLower();
                        int berhasil = 0;

                        if (ext == ".csv")
                        {
                            berhasil = ImportCsv(ofd.FileName);
                        }
                        else if (ext == ".xlsx" || ext == ".xls")
                        {
                            berhasil = ImportExcel(ofd.FileName);
                        }
                        else
                        {
                            MessageBox.Show("Format file tidak didukung!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        loadData();
                        MessageBox.Show($"Import selesai! {berhasil} data baru berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka oleh program lain. Tutup file tersebut dan coba lagi.", "Error Akses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private int ImportCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var importer = new PengadaanImportContext(db);
            int berhasil = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var data = lines[i].Split(',');

                if (data.Length < 5) continue;

                if (DateTime.TryParse(data[0].Trim(), out DateTime tgl) &&
                    decimal.TryParse(data[2].Trim(), out decimal totalHarga))
                {
                    if (importer.Tambah(tgl, data[1], totalHarga, data[3], data[4], data.Length > 5 ? data[5] : null, i + 1))
                        berhasil++;
                }
            }

            db.SaveChanges();
            importer.LaporkanGagal();
            return berhasil;
        }

        private int ImportExcel(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var importer = new PengadaanImportContext(db);
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

                        if (row.ItemArray.Length < 5 || string.IsNullOrWhiteSpace(row[0]?.ToString())) continue;

                        if (DateTime.TryParse(row[0]?.ToString().Trim(), out DateTime tgl) &&
                            decimal.TryParse(row[2]?.ToString().Trim(), out decimal totalHarga))
                        {
                            if (importer.Tambah(tgl, row[1]?.ToString(), totalHarga, row[3]?.ToString(),
                                    row[4]?.ToString(), row.ItemArray.Length > 5 ? row[5]?.ToString() : null, i + 1))
                                berhasil++;
                        }
                    }
                }
            }

            db.SaveChanges();
            importer.LaporkanGagal();
            return berhasil;
        }

        // Menyimpan baris import dan memetakan kolom Rekanan ke pengadaan.id_pemasok.
        // Nilai kolom boleh berupa ID numerik atau nama rekanan; divalidasi ke database
        // agar baris dengan rekanan tak dikenal tidak membuat import gagal seluruhnya.
        private class PengadaanImportContext
        {
            private readonly AppDbContext _db;
            private readonly Dictionary<int, bool> _idRekanan;
            private readonly Dictionary<string, int> _namaRekanan;
            private readonly HashSet<string> _kodeGudang;
            private readonly List<string> _gagal = new List<string>();

            public PengadaanImportContext(AppDbContext db)
            {
                _db = db;
                var listRekanan = db.Pemasok.AsNoTracking().Select(p => new { p.IdPemasok, p.NamaPemasok }).ToList();
                _idRekanan = listRekanan.ToDictionary(p => p.IdPemasok, p => true);
                _namaRekanan = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var p in listRekanan)
                {
                    var nama = (p.NamaPemasok ?? string.Empty).Trim();
                    if (nama.Length > 0 && !_namaRekanan.ContainsKey(nama)) _namaRekanan[nama] = p.IdPemasok;
                }
                _kodeGudang = new HashSet<string>(db.Gudang.AsNoTracking().Select(g => g.KodeGudang), StringComparer.OrdinalIgnoreCase);
            }

            public bool Tambah(DateTime tgl, string rekananRaw, decimal totalHarga, string gudang, string status, string keterangan, int nomorBaris)
            {
                string rekananTeks = (rekananRaw ?? string.Empty).Trim();
                int? idRekanan = null;

                if (rekananTeks.Length > 0)
                {
                    if (int.TryParse(rekananTeks, out int idDariAngka))
                    {
                        if (_idRekanan.ContainsKey(idDariAngka)) idRekanan = idDariAngka;
                    }
                    else if (_namaRekanan.TryGetValue(rekananTeks, out int idDariNama))
                    {
                        idRekanan = idDariNama;
                    }
                }

                if (!idRekanan.HasValue)
                {
                    _gagal.Add($"Baris {nomorBaris}: rekanan '{rekananTeks}' tidak ditemukan (isi ID atau nama rekanan yang terdaftar).");
                    return false;
                }

                string kodeGudang = (gudang ?? string.Empty).Trim();
                if (kodeGudang.Length > 0 && !_kodeGudang.Contains(kodeGudang))
                {
                    _gagal.Add($"Baris {nomorBaris}: kode gudang '{kodeGudang}' tidak terdaftar.");
                    return false;
                }

                _db.Pengadaan.Add(new Pengadaan
                {
                    TanggalPengadaan = tgl,
                    TotalHarga = totalHarga,
                    KodeGudang = kodeGudang.Length > 0 ? kodeGudang : null,
                    Status = (status ?? string.Empty).Trim(),
                    Keterangan = keterangan,
                    IdPemasok = idRekanan.Value
                });
                return true;
            }

            public void LaporkanGagal()
            {
                if (_gagal.Count == 0) return;
                string daftar = string.Join(Environment.NewLine + " - ", _gagal);
                MessageBox.Show(
                    $"{_gagal.Count} baris dilewati karena data tidak valid:" + Environment.NewLine + Environment.NewLine + " - " + daftar,
                    "Sebagian Baris Dilewati", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (cmbJenis.SelectedIndex == -1 || cmbJumlah.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih Jenis Barcode dan Jumlah terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> daftarKode = new List<string>();

            if (cmbJumlah.SelectedItem.ToString() == "1")
            {
                if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PengadaanViewModel vm)
                {
                    var p = vm.ObjekAsli;
                    if (p.Status != "Selesai Dibelanjakan")
                    {
                        MessageBox.Show("Pengadaan ini belum selesai dibelanjakan.\n\nBarcode hanya dapat dicetak setelah barang didistribusikan dan memperoleh kode inventaris di halaman Distribusi Barang.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var asetList = db.Aset
                        .Where(a => db.DetailPengadaan.Any(d => d.IdDetailPengadaan == a.IdDetailPengadaan && d.IdPengadaan == p.IdPengadaan))
                        .Select(a => a.KodeInventaris)
                        .ToList();

                    if (asetList.Count == 0)
                    {
                        MessageBox.Show("Belum ada aset / kode inventaris untuk pengadaan ini.\n\nData aset baru dibuat setelah barang diverifikasi pada halaman Distribusi Barang.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    daftarKode.AddRange(asetList);
                }
                else
                {
                    MessageBox.Show("Silakan pilih satu baris data dari tabel terlebih dahulu.");
                    return;
                }
            }
            else
            {
                var pengadaanIds = dg.Rows.Cast<DataGridViewRow>()
                    .Where(r => r.DataBoundItem is PengadaanViewModel)
                    .Select(r => ((PengadaanViewModel)r.DataBoundItem).IdPengadaan)
                    .ToList();

                var asetList = db.Aset
                    .Where(a => db.DetailPengadaan.Any(d => d.IdDetailPengadaan == a.IdDetailPengadaan && pengadaanIds.Contains(d.IdPengadaan)))
                    .Select(a => a.KodeInventaris)
                    .ToList();

                if (asetList.Count == 0)
                {
                    MessageBox.Show("Belum ada aset / kode inventaris yang bisa dicetak.\n\nData aset baru dibuat setelah barang diverifikasi pada halaman Distribusi Barang.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                daftarKode.AddRange(asetList);
            }

            if (daftarKode.Count == 0) return;

            try
            {
                BarcodeFormat formatBarcode = BarcodeFormat.CODE_128;
                int lebar = 300, tinggi = 100;

                string jenisPilihan = cmbJenis.SelectedItem.ToString();
                if (jenisPilihan == "Code 39") formatBarcode = BarcodeFormat.CODE_39;
                else if (jenisPilihan == "QR Code") { formatBarcode = BarcodeFormat.QR_CODE; lebar = 200; tinggi = 200; }

                var writer = new BarcodeWriter
                {
                    Format = formatBarcode,
                    Options = new EncodingOptions { Height = tinggi, Width = lebar, Margin = 1 }
                };
                Image MakeBarcode2(string kd) { if (jenisPilihan == "QR Code" || (typeof(BarcodeFormat).Name!=null && jenisPilihan=="QR Code")) return QrCodeHelper.GenerateQrCode(kd, lebar>0?lebar:200, tinggi>0?tinggi:200); return QrCodeHelper.GenerateBarcode128(kd, lebar>0?lebar:300, tinggi>0?tinggi:100); }


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
                        Image img = MakeBarcode2(kode) ?? writer.Write(kode);
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
                MessageBox.Show("Terjadi kesalahan sistem saat memproses cetak:\n\n" + ex.Message, "Error Cetak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is PengadaanViewModel vm)
            {
                var p = vm.ObjekAsli;

                if (DetailColumn.Index == e.ColumnIndex)
                {
                    InputPengadaanBarangForm form = new InputPengadaanBarangForm();
                    form.selectedPengadaan = db.Pengadaan.Find(p.IdPengadaan);
                    form.isDetailMode = true;
                    form.ShowDialog();
                }
            }
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pengadaan p)
            {
                if (p.Status == "Selesai Dibelanjakan")
                {
                    MessageBox.Show("Pengadaan ini sudah selesai dibelanjakan (Tuntas). Anda tidak dapat memprosesnya lagi.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show(
                    "Menandai pengadaan sebagai selesai dibelanjakan:\n" +
                    "1. Memilih item barang yang saat ini fisiknya telah datang\n" +
                    "2. Mengisi tanggal selesai dibelanjakan\n" +
                    "3. Menandai item sebagai sudah dibelanjakan (belum menjadi aset)\n\n" +
                    "Pembuatan data aset dilakukan di halaman Distribusi Barang setelah diverifikasi.\n\n" +
                    "Apakah Anda yakin ingin melanjutkan?",
                    "Konfirmasi Selesai Dibelanjakan",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    JalankanProsesBelanja(p.IdPengadaan);
                }
            }
            else
            {
                MessageBox.Show("Pilih data pengadaan yang ingin diproses terlebih dahulu.");
            }
        }

        public class ComboItemVM
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        private void JalankanProsesBelanja(int idPengadaanTarget)
        {
            try
            {
                var unpurchasedDetails = db.DetailPengadaan
                    .Include(d => d.IdMasterBarangNavigation)
                    .Where(d => d.IdPengadaan == idPengadaanTarget && (d.Status == false || d.Status == null))
                    .ToList();

                if (unpurchasedDetails.Count == 0)
                {
                    var targetPgd = db.Pengadaan.Find(idPengadaanTarget);
                    if (targetPgd != null)
                    {
                        targetPgd.Status = "Selesai Dibelanjakan";
                        if (!targetPgd.TanggalSelesaiBelanja.HasValue)
                            targetPgd.TanggalSelesaiBelanja = DateTime.Now.Date;
                        db.SaveChanges();
                    }
                    MessageBox.Show("Semua item barang pada pengadaan ini sudah ditandai selesai dibelanjakan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                    return;
                }

                var pgdData = db.Pengadaan.Find(idPengadaanTarget);

                Form shopForm = new Form
                {
                    Text = "Selesai Dibelanjakan",
                    Size = new Size(470, 265),
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label lblInfo = new Label { Text = "Pilih item barang yang saat ini telah dibeli / datang:", Location = new Point(20, 18), Size = new Size(420, 20) };
                ComboBox cmbItems = new ComboBox { Location = new Point(20, 42), Size = new Size(410, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                Label lblTanggal = new Label { Text = "Tanggal Selesai Dibelanjakan:", Location = new Point(20, 82), Size = new Size(200, 20) };
                DateTimePicker dtpTanggal = new DateTimePicker
                {
                    Location = new Point(20, 105),
                    Size = new Size(410, 25),
                    Format = DateTimePickerFormat.Long,
                    Value = pgdData != null && pgdData.WaktuBelanja.HasValue ? pgdData.WaktuBelanja.Value : DateTime.Now
                };
                Button btnExecute = new Button { Text = "Selesaikan", Location = new Point(280, 155), Size = new Size(150, 35), BackColor = Color.LightGreen };

                var comboSource = new List<ComboItemVM>();

                if (unpurchasedDetails.Count > 1)
                {
                    comboSource.Add(new ComboItemVM { Text = "=== SEMUA SISA BARANG ===", Value = -1 });
                }

                foreach (var d in unpurchasedDetails)
                {
                    var masterBrg = db.MasterBarang.Find(d.IdMasterBarang);
                    string namaBrg = masterBrg != null ? masterBrg.NamaBarang : "Unknown";
                    string kodeRek = string.IsNullOrEmpty(d.KodeRekening) ? "-" : d.KodeRekening;
                    comboSource.Add(new ComboItemVM { Text = $"{namaBrg} [{kodeRek}] ({d.JumlahMasuk} Unit)", Value = d.IdDetailPengadaan });
                }

                cmbItems.DataSource = comboSource;
                cmbItems.DisplayMember = "Text";
                cmbItems.ValueMember = "Value";

                shopForm.Controls.Add(lblInfo);
                shopForm.Controls.Add(cmbItems);
                shopForm.Controls.Add(lblTanggal);
                shopForm.Controls.Add(dtpTanggal);
                shopForm.Controls.Add(btnExecute);

                btnExecute.Click += (s, ev) =>
                {
                    if (cmbItems.SelectedValue == null) return;
                    shopForm.DialogResult = DialogResult.OK;
                    shopForm.Tag = new object[] { (int)cmbItems.SelectedValue, dtpTanggal.Value.Date };
                    shopForm.Close();
                };

                if (shopForm.ShowDialog() != DialogResult.OK)
                {
                    loadData();
                    return;
                }

                var tag = (object[])shopForm.Tag;
                int selectedChoice = (int)tag[0];
                DateTime tanggalSelesai = (DateTime)tag[1];

                var targetDetails = new List<DetailPengadaan>();

                if (selectedChoice == -1)
                {
                    targetDetails = unpurchasedDetails;
                }
                else
                {
                    var singleDetail = unpurchasedDetails.FirstOrDefault(d => d.IdDetailPengadaan == selectedChoice);
                    if (singleDetail != null) targetDetails.Add(singleDetail);
                }

                if (targetDetails.Count == 0) return;

                this.Cursor = Cursors.WaitCursor;

                foreach (var detail in targetDetails)
                {
                    detail.Status = true;
                }

                db.SaveChanges();

                int remainingUnpurchased = db.DetailPengadaan.Count(d => d.IdPengadaan == idPengadaanTarget && (d.Status == false || d.Status == null));

                var targetPengadaanUpdate = db.Pengadaan.Find(idPengadaanTarget);
                if (targetPengadaanUpdate != null)
                {
                    targetPengadaanUpdate.Status = remainingUnpurchased > 0 ? "Sedang Dibelanjakan" : "Selesai Dibelanjakan";
                    targetPengadaanUpdate.TanggalSelesaiBelanja = remainingUnpurchased > 0
                        ? targetPengadaanUpdate.TanggalSelesaiBelanja
                        : tanggalSelesai;
                    db.SaveChanges();
                }

                this.Cursor = Cursors.Default;

                string infoText = selectedChoice == -1 ? "semua sisa barang" : $"item '{db.MasterBarang.Find(targetDetails[0].IdMasterBarang)?.NamaBarang}'";

                MessageBox.Show(
                    $"Sukses menandai {infoText} sebagai selesai dibelanjakan pada {tanggalSelesai:dd/MM/yyyy}.\n\n" +
                    "Data aset BELUM dibuat. Barang akan masuk ke tabel aset setelah diverifikasi pada halaman Distribusi Barang.",
                    "Selesai Dibelanjakan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                loadData();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Terjadi kesalahan fatal saat memproses belanja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
