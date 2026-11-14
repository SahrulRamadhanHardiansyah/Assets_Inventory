using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace Assets_Inventory
{
    public partial class InputPermintaanBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Permintaan selectedPermintaan = null;
        public bool isDetailMode = false;

        AppDbContext db = new AppDbContext();

        private Dictionary<string, int> dictBarang = new Dictionary<string, int>();
        private BindingList<DetailPermintaan> listDetail = new BindingList<DetailPermintaan>();

        public InputPermintaanBarangForm()
        {
            InitializeComponent();
        }

        private void InputPermintaanBarangForm_Load(object sender, EventArgs e)
        {
            cmbJurusan.DataSource = db.Jurusan.ToList();
            var userId = Properties.Settings.Default.userId;
            var user = db.Pengguna.Find(userId);

            if (user == null || user.IdJurusanNavigation == null)
            {
                MessageBox.Show("Data pengguna tidak ditemukan atau tidak memiliki jurusan terkait. Silakan periksa kembali data pengguna Anda.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            cmbJurusan.SelectedValue = user.IdJurusanNavigation.IdJurusan;
            loadTextBox();
            loadData();
            updateBudgetStatus();

            if (isDetailMode)
            {
                this.Text = "Detail Permintaan Barang";
                txtKode.ReadOnly = true;
                dtTgl.Enabled = false;
                dtWaktuBelanja.Enabled = false;
                cmbJurusan.Enabled = false;
                txtKeterangan.ReadOnly = true;
                cmbTahunAjaran.Enabled = false;

                txtBarang.ReadOnly = true;
                txtJumlah.ReadOnly = true;
                txtAlasan.ReadOnly = true;
                btnTambah.Enabled = false;
                btnRefresh.Enabled = false;

                btnSimpan.Visible = false;
                dg.ReadOnly = true;

                if (dg.Columns["HapusColumn"] != null) dg.Columns["HapusColumn"].Visible = false;
            }
        }

        private string GenerateKodePermintaan()
        {
            string prefix = $"PRM-{DateTime.Now.Year}-";

            var lastPermintaan = db.Permintaan
                                   .Where(p => p.KodePermintaan.StartsWith(prefix))
                                   .OrderByDescending(p => p.KodePermintaan)
                                   .FirstOrDefault();

            int nextUrutan = 1;

            if (lastPermintaan != null && !string.IsNullOrEmpty(lastPermintaan.KodePermintaan))
            {
                string strAngka = lastPermintaan.KodePermintaan.Substring(prefix.Length);

                if (int.TryParse(strAngka, out int angkaTerakhir))
                {
                    nextUrutan = angkaTerakhir + 1;
                }
            }

            return $"{prefix}{nextUrutan.ToString("D3")}";
        }

        private void loadData()
        {
            if (selectedPermintaan == null)
            {
                txtKode.Text = GenerateKodePermintaan();
                dtTgl.Value = DateTime.Now;
                dtWaktuBelanja.Value = DateTime.Now;

                listDetail = new BindingList<DetailPermintaan>();
                dg.DataSource = listDetail;
            }
            else
            {
                txtKode.Text = selectedPermintaan.KodePermintaan;
                cmbJurusan.SelectedValue = selectedPermintaan.IdJurusan;
                dtTgl.Value = selectedPermintaan.TanggalPermintaan;
                txtKeterangan.Text = selectedPermintaan.KeteranganKeperluan;

                TahunAjaranInput.Fill(db, cmbTahunAjaran, selectedPermintaan.IdTahunAjaran);
                if (selectedPermintaan.WaktuBelanja.HasValue)
                    dtWaktuBelanja.Value = selectedPermintaan.WaktuBelanja.Value;
                if (selectedPermintaan.Budget.HasValue)
                    txtBudget.Text = selectedPermintaan.Budget.Value.ToString("N0", CultureInfo.InvariantCulture);

                var details = db.DetailPermintaan
                                .Include(d => d.IdMasterBarangNavigation)
                                .Where(d => d.KodePermintaan == selectedPermintaan.KodePermintaan)
                                .ToList();

                listDetail = new BindingList<DetailPermintaan>(details);
                dg.DataSource = listDetail;
            }
            updateBudgetStatus();
        }

        private void loadTextBox()
        {
            var listBarang = db.MasterBarang
                               .Include(b => b.IdKategoriNavigation)
                               .ToList();

            AutoCompleteStringCollection collectionBarang = new AutoCompleteStringCollection();
            dictBarang.Clear();

            foreach (var b in listBarang)
            {
                string namaKategori = b.IdKategoriNavigation != null ? b.IdKategoriNavigation.NamaKategori : "Tanpa Kategori";
                string formattedText = $"{b.NamaBarang} ({namaKategori})";

                collectionBarang.Add(formattedText);

                if (!dictBarang.ContainsKey(formattedText))
                {
                    dictBarang.Add(formattedText, b.IdMasterBarang);
                }
            }

            txtBarang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBarang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBarang.AutoCompleteCustomSource = collectionBarang;

            TahunAjaranInput.Fill(db, cmbTahunAjaran);
        }

        private void btnBrowseBarang_Click(object sender, EventArgs e)
        {
            MasterBarangForm form = new MasterBarangForm();
            if (DialogResult.OK == form.ShowDialog()) loadTextBox();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarang.Text))
            {
                MessageBox.Show("Pilih barang terlebih dahulu.");
                return;
            }

            if (string.IsNullOrEmpty(txtJumlah.Text) || !int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Masukkan jumlah yang valid.");
                return;
            }

            if (string.IsNullOrEmpty(txtAlasan.Text))
            {
                MessageBox.Show("Alasan harus diisi.");
                return;
            }

            if (!dictBarang.TryGetValue(txtBarang.Text, out int idBarangTerpilih))
            {
                MessageBox.Show("Barang tidak ditemukan! Silakan pilih dari daftar saran yang muncul.");
                return;
            }

            var barang = db.MasterBarang.Find(idBarangTerpilih);

            listDetail.Add(new DetailPermintaan
            {
                IdMasterBarang = idBarangTerpilih,
                IdMasterBarangNavigation = barang,
                JumlahDiminta = jumlah,
                AlasanKebutuhan = txtAlasan.Text,
                KodePermintaan = txtKode.Text,
                Hps = barang?.HargaSatuan
            });

            txtBarang.Clear();
            txtJumlah.Clear();
            txtAlasan.Clear();
            txtBarang.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset semua isian barang?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                listDetail.Clear();
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbJurusan.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih jurusan terlebih dahulu.");
                return;
            }

            if (string.IsNullOrEmpty(txtKeterangan.Text))
            {
                MessageBox.Show("Keterangan keperluan keseluruhan harus diisi.");
                return;
            }

            if (cmbTahunAjaran.SelectedValue == null)
            {
                MessageBox.Show("Tahun Ajaran belum dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idTahunAjaranTerpilih = (int)cmbTahunAjaran.SelectedValue;

            if (listDetail.Count == 0)
            {
                MessageBox.Show("Anda belum menambahkan barang satupun ke dalam daftar keranjang permintaan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi Budget vs HPS (Total HPS = SUM(hps), tanpa dikali jumlah)
            decimal totalHps = 0;
            foreach (var d in listDetail)
            {
                totalHps += d.Hps ?? 0m;
            }

            decimal budget = 0;
            bool hasBudget = txtBudget.Text.Trim().Length > 0 && decimal.TryParse(txtBudget.Text, out budget);

            if (hasBudget && totalHps > budget)
            {
                var selisih = totalHps - budget;
                MessageBox.Show(
                    $"Total HPS melebihi budget yang diinputkan.\n\n" +
                    $"Total HPS   : {totalHps.ToString("N0", CultureInfo.InvariantCulture)}\n" +
                    $"Budget      : {budget.ToString("N0", CultureInfo.InvariantCulture)}\n" +
                    $"Melebihi    : {selisih.ToString("N0", CultureInfo.InvariantCulture)}\n\n" +
                    "Silakan kurangi nilai HPS pada daftar barang, atau naikkan budget, lalu coba lagi.",
                    "Budget Melebihi Total HPS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (selectedPermintaan == null)
                {
                    int userId = Properties.Settings.Default.userId > 0 ? Properties.Settings.Default.userId : 1;

                    var baru = new Permintaan
                    {
                        KodePermintaan = txtKode.Text,
                        IdJurusan = (int)cmbJurusan.SelectedValue,
                        IdTahunAjaran = idTahunAjaranTerpilih,
                        IdPengguna = userId,
                        KeteranganKeperluan = txtKeterangan.Text,
                        StatusPersetujuan = "Menunggu",
                        TanggalPermintaan = dtTgl.Value,
                        WaktuBelanja = dtWaktuBelanja.Value,
                        Budget = hasBudget ? budget : (decimal?)null,
                        DetailPermintaan = listDetail.ToList()
                    };

                    db.Permintaan.Add(baru);
                    db.SaveChanges();

                    MessageBox.Show("Data permintaan berhasil disubmit dan menunggu persetujuan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isDetailMode) return;

            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is DetailPermintaan dp)
            {
                if (dg.Columns["HapusColumn"] != null && dg.Columns["HapusColumn"].Index == e.ColumnIndex)
                {
                    if (MessageBox.Show($"Apakah anda yakin ingin menghapus barang {dp.IdMasterBarangNavigation.NamaBarang} dari keranjang?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        listDetail.Remove(dp);
                        updateBudgetStatus();
                    }
                }
            }
        }

        private void dg_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isDetailMode || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dg.Rows[e.RowIndex];
            if (row.DataBoundItem is DetailPermintaan dp &&
                dg.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn col &&
                col.DataPropertyName == "Hps")
            {
                decimal hpsVal = 0m;
                var val = row.Cells[e.ColumnIndex].Value;
                if (val == null || val == DBNull.Value)
                {
                    dp.Hps = null;
                    updateBudgetStatus();
                    return;
                }
                if (val is decimal dec)
                {
                    hpsVal = dec;
                }
                else if (val is string s && decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out hpsVal))
                {
                    // OK
                }
                else
                {
                    return;
                }

                dp.Hps = hpsVal;
                updateBudgetStatus();
            }
        }

        private void txtBudget_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtBudget_Leave(object sender, EventArgs e)
        {
            updateBudgetStatus();
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dg.Rows[e.RowIndex].DataBoundItem is DetailPermintaan dp)
            {
                if (idMasterBarangNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = dp.IdMasterBarangNavigation?.NamaBarang;
                }

                if (hpsDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    // Tampilkan dengan pemisah ribuan gaya Indonesia (titik), konsisten
                    // dengan cara user mengetik; editor tetap angka mentah (EditingControlShowing).
                    e.Value = (dp.Hps ?? 0m).ToString("N0", new CultureInfo("id-ID"));
                    e.FormattingApplied = true;
                }
            }
        }

        private void updateBudgetStatus()
        {
            if (listDetail.Count == 0) return;

            decimal totalHps = 0;
            foreach (var d in listDetail) totalHps += d.Hps ?? 0m;

            decimal budget = 0;
            bool hasBudget = txtBudget.Text.Trim().Length > 0 && decimal.TryParse(txtBudget.Text, out budget);

            string status;
            if (!hasBudget)
            {
                status = "Belum diinputkan";
            }
            else if (totalHps > budget)
            {
                status = $"LEBIH ({(totalHps - budget).ToString("N0", CultureInfo.InvariantCulture)} di atas budget)";
            }
            else
            {
                status = "OK";
            }

            var label = new Label
            {
                Name = "lblBudgetStatus",
                AutoSize = true,
                TextAlign = ContentAlignment.TopRight,
                ForeColor = (hasBudget && totalHps > budget) ? Color.Red : Color.Green,
                Text = $"Total HPS: {totalHps.ToString("N0", CultureInfo.InvariantCulture)}  |  Budget: {budget.ToString("N0", CultureInfo.InvariantCulture)}  |  Status: {status}"
            };

            var existing = this.Controls.Find("lblBudgetStatus", true).FirstOrDefault();
            if (existing != null)
            {
                existing.Dispose();
            }

            label.Top = dg.Bottom + 8;
            label.Left = this.ClientSize.Width - label.PreferredWidth - 20;
            this.Controls.Add(label);
        }

        #region Bulk Import Excel

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!ImportTemplateHelper.ConfirmOrGenerate(this, "Import Barang (SSH)",
                new[] {
                    ("Nama Barang / Kode", "ATM 900mA 9 Volt"),
                    ("Nama Kategori", "Elektronik"),
                    ("Nama Merek", "Sony"),
                    ("Nama Satuan", "Pcs"),
                    ("Harga Satuan / SSH", "75000"),
                    ("Keterangan", "Baterai / adaptor")
                }))
                return;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel & CSV Files|*.xlsx;*.xls;*.csv|Semua File (*.*)|*.*";
                ofd.Title = "Pilih File Data Barang";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int berhasil = 0, skip = 0;
                        List<string> pesanPerBaris = new List<string>();

                        string ekstensi = Path.GetExtension(ofd.FileName).ToLower();

                        if (ekstensi == ".csv")
                        {
                            var lines = File.ReadAllLines(ofd.FileName);
                            for (int i = 1; i < lines.Length; i++)
                            {
                                Application.DoEvents();
                                var data = lines[i].Split(',');
                                if (data.Length >= 5)
                                {
                                    pesanPerBaris.Add(ProcessImportRow(data));
                                    if (pesanPerBaris.Last().StartsWith("OK", StringComparison.OrdinalIgnoreCase))
                                        berhasil++;
                                    else
                                        skip++;
                                }
                            }
                        }
                        else
                        {
                            using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                            {
                                using (var reader = ExcelReaderFactory.CreateReader(stream))
                                {
                                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                    {
                                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                        {
                                            UseHeaderRow = true
                                        }
                                    });

                                    DataTable dt = result.Tables[0];

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        Application.DoEvents();
                                        if (IsRowNullOrEmpty(row)) continue;

                                        string[] data = new string[dt.Columns.Count];
                                        for (int c = 0; c < dt.Columns.Count; c++)
                                            data[c] = row[c]?.ToString() ?? "";

                                        pesanPerBaris.Add(ProcessImportRow(data));
                                        if (pesanPerBaris.Last().StartsWith("OK", StringComparison.OrdinalIgnoreCase))
                                            berhasil++;
                                        else
                                            skip++;
                                    }
                                }
                            }
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Import selesai!");
                        sb.AppendLine($"Berhasil: {berhasil} baris");
                        sb.AppendLine($"Dilewati/error: {skip} baris");
                        if (pesanPerBaris.Any(p => p.Length > 3))
                        {
                            sb.AppendLine();
                            sb.AppendLine("Detail per baris:");
                            for (int i = 0; i < pesanPerBaris.Count; i++)
                                if (pesanPerBaris[i].Length > 3)
                                    sb.AppendLine($"  Baris {i + 2}: {pesanPerBaris[i]}");
                        }

                        MessageBox.Show(sb.ToString(), "Hasil Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Catat: JANGAN panggil loadData()/loadTextBox() di sini!
                        // loadData() melakukan listDetail = new BindingList<DetailPermintaan>()
                        // yang akan MENGHAPUS seluruh baris hasil import dari DataGridView.
                        // Grid sudah berisi item hasil import via InsertDetail() -> listDetail.Add().
                        // Cukup refresh status budget.
                        updateBudgetStatus();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka atau digunakan oleh program lain (seperti Microsoft Excel). Harap tutup file tersebut dan coba lagi.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private static bool IsRowNullOrEmpty(DataRow row)
        {
            return row.ItemArray.All(v => v == null || (v is string s && string.IsNullOrWhiteSpace(s)));
        }

        private string ProcessImportRow(string[] data)
        {
            string namaBarang = data[0].Trim();
            if (string.IsNullOrEmpty(namaBarang))
                return "Baris kosong / tidak ada nama barang";

            string namaKategori = data[1].Trim();
            string namaMerek = data[2].Trim();
            string namaSatuan = data[3].Trim();
            string hargaStr = data[4].Trim();
            string keterangan = data.Length > 5 ? data[5].Trim() : "";

            if (!decimal.TryParse(hargaStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal harga))
                return $"Harga satuan tidak valid: {hargaStr}";

            // Lookup barang (nama_barang) - case insensitive
            var existing = db.MasterBarang
                .FirstOrDefault(m => m.NamaBarang.Trim().ToLower() == namaBarang.ToLower());

            if (existing != null)
            {
                // Update harga_satuan (SSH) jika berubah
                if (!existing.HargaSatuan.HasValue || Math.Abs(existing.HargaSatuan.Value - harga) > 0.005m)
                {
                    existing.HargaSatuan = harga;
                    db.SaveChanges();
                }

                InsertDetail(existing.IdMasterBarang, harga, namaBarang, keterangan);
                return $"OK - Barang '{namaBarang}' (ID {existing.IdMasterBarang}) dari master";
            }

            // Barang belum ada -> auto-create master + detail
            int idBarang = CreateMasterBarang(namaBarang, namaKategori, namaMerek, namaSatuan, harga, keterangan);
            if (idBarang == -1)
                return "Gagal membuat master barang (kategori/merek/satuan tidak valid)";

            InsertDetail(idBarang, harga, namaBarang, keterangan);
            return $"OK - Baru: '{namaBarang}' dibuat di master_barang (ID {idBarang})";
        }

        private void InsertDetail(int idBarang, decimal harga, string namaBarang, string keterangan)
        {
            var nav = db.MasterBarang.Find(idBarang);
            listDetail.Add(new DetailPermintaan
            {
                IdMasterBarang = idBarang,
                IdMasterBarangNavigation = nav,
                JumlahDiminta = 1,
                AlasanKebutuhan = keterangan,
                KodePermintaan = txtKode.Text,
                Hps = harga
            });
        }

        private int CreateMasterBarang(string namaBarang, string namaKategori, string namaMerek, string namaSatuan, decimal harga, string keterangan)
        {
            int idKategori = LookupOrCreateReference<Kategori>(db.Kategori, namaKategori, "NamaKategori", 1);
            int idMerek = LookupOrCreateReference<Merek>(db.Merek, namaMerek, "NamaMerek", 1);
            int idSatuan = LookupOrCreateReference<Satuan>(db.Satuan, namaSatuan, "NamaSatuan", 1);

            var barang = new MasterBarang
            {
                NamaBarang = namaBarang,
                IdKategori = idKategori,
                IdMerek = idMerek,
                IdSatuan = idSatuan,
                JenisBarang = "Aset",          // WAJIB: otomatis diisi "Aset"
                Keterangan = keterangan,
                HargaSatuan = harga
            };

            db.MasterBarang.Add(barang);
            db.SaveChanges();
            return barang.IdMasterBarang;
        }

        private int LookupOrCreateReference<T>(Microsoft.EntityFrameworkCore.DbSet<T> set, string nilai, string nameProp, int idDefault) where T : class
        {
            if (string.IsNullOrWhiteSpace(nilai))
                return idDefault;

            // pencarian case-insensitive via reflection pada hasil query (client-side eval untuk EF Core 3.1)
            var all = set.ToList();
            var found = all.FirstOrDefault(e => getRefName(e, nameProp)?.Trim().ToLower() == nilai.Trim().ToLower());

            if (found != null)
                return getRefId(found);

            // auto-create
            var create = Activator.CreateInstance<T>();
            var prop = typeof(T).GetProperty(nameProp);
            if (prop != null)
                prop.SetValue(create, nilai);

            set.Add(create);
            db.SaveChanges();
            return getRefId(create);
        }

        private static string getRefName(object obj, string propName)
        {
            var p = obj.GetType().GetProperty(propName);
            return p?.GetValue(obj) as string;
        }

        private static int getRefId(object obj)
        {
            var type = obj.GetType();
            foreach (var prop in type.GetProperties())
            {
                // cari properti ID: exact "Id" atau pattern "IdXxx" (IdKategori, IdMerek, IdSatuan)
                if (prop.Name == "Id")
                    return (int)prop.GetValue(obj);
                if (prop.Name.Length >= 4 && prop.Name.StartsWith("Id") && char.IsUpper(prop.Name[2]))
                    return (int)prop.GetValue(obj);
            }
            return 0;
        }

        #region Validasi & Parsing Kolom HPS (DataGridView)

        /// <summary>
        /// Dipanggil saat DataGridView mencoba mengkonversi string input user
        /// menjadi tipe sel (decimal). Membersihkan input uang Indonesia
        /// (Rp, titik pemisah ribuan, spasi) sebelum diparsing.
        /// </summary>
        private void dg_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            var col = dg.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn;
            if (col == null || col.DataPropertyName != "Hps")
                return;

            // Jika nilai sudah berupa decimal (dipicu dari DataSource/db), lewati parsing.
            if (!(e.Value is string raw))
                return;

            if (string.IsNullOrWhiteSpace(raw))
            {
                e.Value = 0m;
                e.ParsingApplied = true;
                return;
            }

            string cleaned = NormalizeMoney(raw);
            if (decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed))
            {
                e.Value = parsed;
            }
            else
            {
                // Input tidak valid: batalkan dengan 0 agar tidak muncul FormatException.
                e.Value = 0m;
            }

            e.ParsingApplied = true;
        }

        /// <summary>
        /// Bersihkan format uang Indonesia (Rp, spasi, pemisah ribuan) menjadi
        /// angka mentah dengan titik desimal (InvariantCulture).
        /// Aturan:
        ///  - Ada koma            -> koma = desimal, semua titik = pemisah ribuan.
        ///  - Hanya titik (>=1)   -> jika blok terakhir tepat 3 digit -> pemisah ribuan;
        ///                           selain itu -> titik terakhir = desimal, sisanya ribuan.
        ///  - Tanpa pemisah       -> ambil digit saja.
        /// </summary>
        private static string NormalizeMoney(string raw)
        {
            // Hapus label mata uang (case-insensitive manual untuk .NET Framework)
            string s = raw;
            int idx = s.IndexOf("Rp", StringComparison.OrdinalIgnoreCase);
            while (idx >= 0)
            {
                s = s.Remove(idx, 2);
                idx = s.IndexOf("Rp", StringComparison.OrdinalIgnoreCase);
            }
            idx = s.IndexOf("IDR", StringComparison.OrdinalIgnoreCase);
            while (idx >= 0)
            {
                s = s.Remove(idx, 3);
                idx = s.IndexOf("IDR", StringComparison.OrdinalIgnoreCase);
            }

            // Hapus spasi (bisa jadi pemisah ribuan)
            s = new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());

            bool hasComma = s.Contains(",");
            bool hasDot = s.Contains(".");

            if (hasComma)
            {
                // Koma = desimal, semua titik = pemisah ribuan
                s = s.Replace(".", "");
                s = s.Replace(",", ".");
            }
            else if (hasDot)
            {
                int lastDot = s.LastIndexOf('.');
                string afterLastDot = s.Substring(lastDot + 1);
                if (afterLastDot.Length == 3 && s.Count(c => c == '.') >= 1 && afterLastDot.All(char.IsDigit))
                {
                    // Pola ribuan Indonesia: "75.000" / "1.000.000" -> hapus semua titik
                    s = s.Replace(".", "");
                }
                else
                {
                    // Titik terakhir = desimal, titik sebelumnya = ribuan
                    string head = s.Substring(0, lastDot).Replace(".", "");
                    s = head + "." + afterLastDot;
                }
            }
            else
            {
                // Tanpa pemisah: ambil digit saja
                s = new string(s.Where(char.IsDigit).ToArray());
            }

            if (string.IsNullOrEmpty(s) || s == ".")
                return "0";
            return s;
        }

        /// <summary>
        /// Cegah DataGridView Default Error Dialog muncul.
        /// Tunjukkan pesan ramah pengguna dan matikan exception bawaan.
        /// </summary>
        private void dg_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;

            string colName = "kolom";
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dg.Columns.Count)
                colName = dg.Columns[e.ColumnIndex].HeaderText;

            MessageBox.Show(
                $"Nilai pada kolom \"{colName}\" tidak sesuai format. " +
                "Masukkan hanya angka (contoh: 1000000 untuk Rp 1.000.000).",
                "Format Tidak Valid",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Saat user mulai mengedit sel HPS, batasi input keyboard
        /// hanya angka, titik, dan koma.
        /// </summary>
        private void dg_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dg.Columns[dg.CurrentCell.ColumnIndex] is DataGridViewTextBoxColumn col &&
                col.DataPropertyName == "Hps" && e.Control is TextBox tb)
            {
                // Isi TextBox editor dengan ANGKA MENTAH tanpa format/pemisah ribuan,
                // agar user mengedit digit murni dan tidak memicu FormatException.
                var dp = dg.CurrentRow?.DataBoundItem as DetailPermintaan;
                tb.Text = (dp?.Hps ?? 0m).ToString(CultureInfo.InvariantCulture);

                tb.KeyPress -= HpsTextBox_KeyPress;
                tb.KeyPress += HpsTextBox_KeyPress;
            }
        }

        private void HpsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Izinkan: tombol kontrol (Backspace dll), angka 0-9, titik (.), koma (,)
            bool allowed = char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)
                           || e.KeyChar == '.' || e.KeyChar == ',';
            if (!allowed)
            {
                e.Handled = true;
            }
        }

        #endregion

        #endregion

    }
}
