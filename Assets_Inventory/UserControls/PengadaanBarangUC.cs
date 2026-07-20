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
    public partial class PengadaanBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        public string DefaultStatusFilter = "Semua";

        public class PengadaanViewModel
        {
            public int IdPengadaan { get; set; }
            public DateTime TanggalPengadaan { get; set; }
            public string TahunAjaran { get; set; }
            public string NamaSumber { get; set; }
            public string NamaGudang { get; set; }
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

            var data = listPengadaan.Select(p => new PengadaanViewModel
            {
                IdPengadaan = p.IdPengadaan,
                TanggalPengadaan = p.TanggalPengadaan,
                TahunAjaran = (p.IdTahunAjaran.HasValue && dictTA.ContainsKey(p.IdTahunAjaran.Value)) ? dictTA[p.IdTahunAjaran.Value] : "-",
                NamaSumber = (p.IdSumberPerolehan.HasValue && dictSumber.ContainsKey(p.IdSumberPerolehan.Value)) ? dictSumber[p.IdSumberPerolehan.Value] : "N/A",
                NamaGudang = (!string.IsNullOrEmpty(p.KodeGudang) && dictGudang.ContainsKey(p.KodeGudang)) ? dictGudang[p.KodeGudang] : "N/A",
                TotalHarga = p.TotalHarga ?? 0,
                Status = p.Status ?? "Menunggu Proses",
                ObjekAsli = p
            }).ToList();

            dg.DataSource = new SortableBindingList<PengadaanViewModel>(data);
            lblTotal.Text = $"Total Record: {data.Count}";

            if (dg.Columns["IdPengadaan"] != null) dg.Columns["IdPengadaan"].HeaderText = "ID Pengadaan";
            if (dg.Columns["TanggalPengadaan"] != null) dg.Columns["TanggalPengadaan"].HeaderText = "Tanggal";
            if (dg.Columns["TahunAjaran"] != null) dg.Columns["TahunAjaran"].HeaderText = "Tahun Ajaran";
            if (dg.Columns["NamaSumber"] != null) dg.Columns["NamaSumber"].HeaderText = "Sumber Perolehan";
            if (dg.Columns["NamaGudang"] != null) dg.Columns["NamaGudang"].HeaderText = "Gudang Tujuan";
            if (dg.Columns["Status"] != null) dg.Columns["Status"].HeaderText = "Status";
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
            int berhasil = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var data = lines[i].Split(',');

                if (data.Length >= 5)
                {
                    if (DateTime.TryParse(data[0].Trim(), out DateTime tgl) &&
                        int.TryParse(data[1].Trim(), out int idPemasok) &&
                        decimal.TryParse(data[2].Trim(), out decimal totalHarga))
                    {
                        var pengadaan = new Pengadaan
                        {
                            TanggalPengadaan = tgl,
                            TotalHarga = totalHarga,
                            KodeGudang = data[3].Trim(),
                            Status = data[4].Trim(),
                            Keterangan = data.Length > 5 ? data[5].Trim() : null
                        };

                        db.Pengadaan.Add(pengadaan);
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

                        if (row.ItemArray.Length >= 5 && !string.IsNullOrWhiteSpace(row[0]?.ToString()))
                        {
                            if (DateTime.TryParse(row[0]?.ToString().Trim(), out DateTime tgl) &&
                                int.TryParse(row[1]?.ToString().Trim(), out int idPemasok) &&
                                decimal.TryParse(row[2]?.ToString().Trim(), out decimal totalHarga))
                            {
                                var pengadaan = new Pengadaan
                                {
                                    TanggalPengadaan = tgl,
                                    TotalHarga = totalHarga,
                                    KodeGudang = row[3]?.ToString().Trim(),
                                    Status = row[4]?.ToString().Trim(),
                                    Keterangan = row.ItemArray.Length > 5 ? row[5]?.ToString().Trim() : null
                                };

                                db.Pengadaan.Add(pengadaan);
                                berhasil++;
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
            return berhasil;
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
                    if (p.Status == "Menunggu Proses")
                    {
                        MessageBox.Show("Pengadaan ini belum diproses (Barang belum punya kode inventaris).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var asetList = db.Aset
                        .Where(a => db.DetailPengadaan.Any(d => d.IdDetailPengadaan == a.IdDetailPengadaan && d.IdPengadaan == p.IdPengadaan))
                        .Select(a => a.KodeInventaris)
                        .ToList();

                    if (asetList.Count == 0)
                    {
                        MessageBox.Show("Tidak ada aset dengan kode inventaris yang terikat pada pengadaan ini.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Tidak ada aset dengan kode inventaris yang bisa dicetak.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    "Proses belanja akan dilakukan per-item barang:\n" +
                    "1. Memilih item barang yang saat ini fisiknya telah datang\n" +
                    "2. Meng-generate kode inventaris untuk unit barang tersebut\n" +
                    "3. Memasukkan barang ke tabel aset (Status: Di Gudang)\n" +
                    "4. Memperbarui status dokumen pengadaan\n\n" +
                    "Apakah Anda yakin ingin melanjutkan eksekusi ini?",
                    "Konfirmasi Proses Belanja",
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
                        db.SaveChanges();
                    }
                    MessageBox.Show("Semua item barang pada pengadaan ini sudah selesai dibelanjakan masuk gudang.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                    return;
                }

                Form shopForm = new Form
                {
                    Text = "Proses Belanja Per-Barang",
                    Size = new Size(450, 200),
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label lblInfo = new Label { Text = "Pilih item barang yang saat ini telah dibeli / datang:", Location = new Point(20, 20), Size = new Size(400, 20) };
                ComboBox cmbItems = new ComboBox { Location = new Point(20, 45), Size = new Size(390, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                Button btnExecute = new Button { Text = "Selesaikan Item Ini", Location = new Point(260, 95), Size = new Size(150, 35), BackColor = Color.LightGreen };

                var comboSource = new List<ComboItemVM>();

                if (unpurchasedDetails.Count > 1)
                {
                    comboSource.Add(new ComboItemVM { Text = "=== SEMUA SISA BARANG ===", Value = -1 });
                }

                foreach (var d in unpurchasedDetails)
                {
                    var masterBrg = db.MasterBarang.Find(d.IdMasterBarang);
                    string namaBrg = masterBrg != null ? masterBrg.NamaBarang : "Unknown";
                    comboSource.Add(new ComboItemVM { Text = $"{namaBrg} ({d.JumlahMasuk} Unit)", Value = d.IdDetailPengadaan });
                }

                cmbItems.DataSource = comboSource;
                cmbItems.DisplayMember = "Text";
                cmbItems.ValueMember = "Value";

                shopForm.Controls.Add(lblInfo);
                shopForm.Controls.Add(cmbItems);
                shopForm.Controls.Add(btnExecute);

                btnExecute.Click += (s, ev) =>
                {
                    if (cmbItems.SelectedValue == null) return;
                    shopForm.DialogResult = DialogResult.OK;
                    shopForm.Tag = (int)cmbItems.SelectedValue;
                    shopForm.Close();
                };

                if (shopForm.ShowDialog() != DialogResult.OK)
                {
                    loadData();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                int selectedChoice = (int)shopForm.Tag;

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

                int? idJurusanTarget = null;
                var listKodePermintaan = db.PengadaanPermintaan
                                           .Where(pp => pp.IdPengadaan == idPengadaanTarget)
                                           .Select(pp => pp.KodePermintaan)
                                           .ToList();

                if (listKodePermintaan.Count > 0)
                {
                    string kodePrmPertama = listKodePermintaan.FirstOrDefault();
                    var prm = db.Permintaan.FirstOrDefault(x => x.KodePermintaan == kodePrmPertama);
                    if (prm != null) idJurusanTarget = prm.IdJurusan;
                }

                string prefix = "INV";
                var pengaturan = db.Pengaturan.FirstOrDefault();
                if (pengaturan != null && !string.IsNullOrEmpty(pengaturan.KustomPrefix))
                {
                    prefix = pengaturan.KustomPrefix;
                }

                var targetPgdData = db.Pengadaan.Find(idPengadaanTarget);
                string tahunPgd = targetPgdData != null ? targetPgdData.TanggalPengadaan.Year.ToString() : DateTime.Now.Year.ToString();

                var urutanDetailIds = db.DetailPengadaan
                                        .Where(d => d.IdPengadaan == idPengadaanTarget)
                                        .OrderBy(d => d.IdDetailPengadaan)
                                        .Select(d => d.IdDetailPengadaan)
                                        .ToList();

                List<Aset> asetBaruList = new List<Aset>();

                foreach (var detail in targetDetails)
                {
                    int urutanBarang = urutanDetailIds.IndexOf(detail.IdDetailPengadaan) + 1;
                    string strTotalBarang = detail.JumlahMasuk.ToString("D3");

                    for (int i = 1; i <= detail.JumlahMasuk; i++)
                    {
                        string strUrutanUnit = i.ToString("D3");

                        string kodeInventaris = $"{prefix}-{tahunPgd}-{idPengadaanTarget:D3}-{urutanBarang:D3}-{strTotalBarang}-{strUrutanUnit}";

                        var aset = new Aset
                        {
                            IdDetailPengadaan = detail.IdDetailPengadaan,
                            IdMasterBarang = detail.IdMasterBarang,
                            IdJurusan = idJurusanTarget,
                            IdRuang = null,
                            IdLokasi = null,
                            NoSeri = null,
                            HargaSatuan = detail.HargaSatuan,
                            NilaiResidu = 0,
                            UmurEkonomi = null,
                            KodeInventaris = kodeInventaris,
                            Status = "Di Gudang",
                            TanggalRegistrasi = DateTime.Now,
                            Gambar = null,
                            Keterangan = $"Dari pengadaan #{idPengadaanTarget}",
                            KodeBarang2 = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper()
                        };

                        db.Aset.Add(aset);
                        asetBaruList.Add(aset);
                    }

                    detail.Status = true;
                }

                db.SaveChanges();

                int remainingUnpurchased = db.DetailPengadaan.Count(d => d.IdPengadaan == idPengadaanTarget && (d.Status == false || d.Status == null));

                var targetPengadaanUpdate = db.Pengadaan.Find(idPengadaanTarget);
                if (targetPengadaanUpdate != null)
                {
                    targetPengadaanUpdate.Status = remainingUnpurchased > 0 ? "Sedang Dibelanjakan" : "Selesai Dibelanjakan";
                    db.SaveChanges();
                }

                this.Cursor = Cursors.Default;

                string infoText = selectedChoice == -1 ? "Semua sisa barang" : $"Item '{db.MasterBarang.Find(targetDetails[0].IdMasterBarang)?.NamaBarang}'";

                DialogResult opsi = MessageBox.Show(
                    $"Sukses memproses {infoText}. {asetBaruList.Count} unit barang berhasil dimasukkan ke Gudang Utama.\n\n" +
                    $"Apakah Anda ingin melengkapi data fisik (No. Seri, Gambar, Ruang) untuk item ini sekarang?",
                    "Lengkapi Data Aset",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (opsi == DialogResult.Yes)
                {
                    KelengkapanAsetForm formLengkap = new KelengkapanAsetForm(asetBaruList);
                    formLengkap.ShowDialog();
                }

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