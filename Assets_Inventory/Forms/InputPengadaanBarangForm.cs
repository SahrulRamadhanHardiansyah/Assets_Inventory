using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class InputPengadaanBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Pengadaan selectedPengadaan;
        public bool isDetailMode = false;

        AppDbContext db = new AppDbContext();
        private List<Permintaan> selectedPermintaanList = new List<Permintaan>();
        private BindingList<DetailPengadaanViewModel> listDetailBon = new BindingList<DetailPengadaanViewModel>();

        public class DetailPengadaanViewModel
        {
            public int IdMasterBarang { get; set; }
            public string NamaBarang { get; set; }
            public string Kategori { get; set; }
            public string KodeRekening { get; set; }
            public int JumlahMasuk { get; set; }
            public decimal HargaSatuan { get; set; }
            public decimal TotalHarga { get; set; }
            public int? IdPemasok { get; set; }
            public string NamaPemasok { get; set; }
            public string KodePermintaanAsal { get; set; }
            public int? IdJurusanTarget { get; set; }
            public bool? StatusTelahDibelanjakan { get; set; }
        }

        public class PermintaanSimpleViewModel
        {
            public bool Pilih { get; set; }
            public string KodePermintaan { get; set; }
            public string NamaJurusan { get; set; }
            public string NamaPeminta { get; set; }
            public string Status { get; set; }
            public DateTime Tanggal { get; set; }
            public Permintaan ObjekAsli { get; set; }
        }

        public InputPengadaanBarangForm()
        {
            InitializeComponent();
        }

        private void InputPengadaanBarangForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            SetupAutoComplete();

            dgPermintaan.CurrentCellDirtyStateChanged -= DgPermintaan_CurrentCellDirtyStateChanged;
            dgPermintaan.CurrentCellDirtyStateChanged += DgPermintaan_CurrentCellDirtyStateChanged;

            if (selectedPengadaan == null)
            {
                txtId.Text = GenerateIdPengadaan();
                dtpTglPengadaan.Value = DateTime.Now;
                cmbStatus.SelectedItem = "Menunggu Proses";
                cmbStatus.Enabled = false;

                LoadDaftarPermintaan();

                listDetailBon = new BindingList<DetailPengadaanViewModel>();
                dgDetailBon.DataSource = listDetailBon;
                AturFormatKolomDetailBon();
            }
            else
            {
                LoadDataPengadaan();
            }

            if (isDetailMode)
            {
                this.Text = "Detail Pengadaan Barang";
                dtpTglPengadaan.Enabled = false;
                cmbGudang.Enabled = false;
                cmbSumber.Enabled = false;
                txtPemasok.Enabled = false; 
                txtKeterangan.ReadOnly = true;
                cmbTahunAjaran.Enabled = false;
                txtTotalHarga.ReadOnly = true;
                btnPilihPermintaan.Enabled = false;
                btnTerapkanPemasok.Enabled = false;
                btnSimpan.Visible = false;
                dgPermintaan.ReadOnly = true;
                dgDetailBon.ReadOnly = true;
                cmbStatus.Enabled = false;
            }
        }

        private void DgPermintaan_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgPermintaan.IsCurrentCellDirty)
            {
                dgPermintaan.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void SetupAutoComplete()
        {
            var listPemasok = db.Pemasok.Select(p => p.NamaPemasok).ToArray();

            var source = new AutoCompleteStringCollection();
            source.AddRange(listPemasok);

            txtPemasok.AutoCompleteCustomSource = source;
            txtPemasok.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPemasok.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void SetupComboBoxes()
        {
            cmbGudang.DataSource = db.Gudang.ToList();
            cmbGudang.SelectedIndex = -1;
            cmbSumber.DataSource = db.SumberPerolehan.ToList();
            cmbSumber.SelectedIndex = -1;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Menunggu Proses");
            cmbStatus.Items.Add("Sedang Dibelanjakan");
            cmbStatus.Items.Add("Selesai Dibelanjakan");

            TahunAjaranInput.Fill(db, cmbTahunAjaran);
        }

        private string GenerateIdPengadaan()
        {
            int lastId = db.Pengadaan.OrderByDescending(p => p.IdPengadaan)
                                     .Select(p => p.IdPengadaan)
                                     .FirstOrDefault();

            int nextId = lastId + 1;
            return $"PGD-{DateTime.Now.Year}-{nextId:D3}";
        }

        private void LoadDaftarPermintaan()
        {
            var permintaanDisetujui = db.Permintaan.AsNoTracking()
                .Include(p => p.DetailPermintaan)
                .Where(p => p.StatusPersetujuan == "Disetujui")
                .Where(p => !p.PengadaanPermintaan.Any())
                .ToList();

            var dictJurusan = db.Jurusan.ToDictionary(j => j.IdJurusan, j => j.NamaJurusan);
            var dictPengguna = db.Pengguna.ToDictionary(u => u.IdPengguna, u => u.Username);

            var dataTampil = permintaanDisetujui.Select(p => new PermintaanSimpleViewModel
            {
                Pilih = false,
                KodePermintaan = p.KodePermintaan,
                NamaJurusan = (p.IdJurusan.HasValue && dictJurusan.ContainsKey(p.IdJurusan.Value)) ? dictJurusan[p.IdJurusan.Value] : "-",
                NamaPeminta = (p.IdPengguna.HasValue && dictPengguna.ContainsKey(p.IdPengguna.Value)) ? dictPengguna[p.IdPengguna.Value] : "-",
                Status = p.StatusPersetujuan,
                Tanggal = p.TanggalPermintaan,
                ObjekAsli = p
            }).ToList();

            dgPermintaan.DataSource = new SortableBindingList<PermintaanSimpleViewModel>(dataTampil);
            AturFormatKolomPermintaan();
        }

        private void btnPilihPermintaan_Click(object sender, EventArgs e)
        {
            if (dgPermintaan.IsCurrentCellDirty)
            {
                dgPermintaan.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            dgPermintaan.EndEdit();

            selectedPermintaanList.Clear();

            foreach (DataGridViewRow row in dgPermintaan.Rows)
            {
                var vm = row.DataBoundItem as PermintaanSimpleViewModel;
                if (vm != null && vm.Pilih)
                {
                    selectedPermintaanList.Add(vm.ObjekAsli);
                }
            }

            if (selectedPermintaanList.Count == 0)
            {
                MessageBox.Show("Pilih minimal satu permintaan yang sudah disetujui (Centang pada kotak).");
                return;
            }

            GenerateDetailBon();
        }

        private void GenerateDetailBon()
        {
            var dictBarang = db.MasterBarang.ToDictionary(b => b.IdMasterBarang, b => b);
            var dictKategori = db.Kategori.ToDictionary(k => k.IdKategori, k => k.NamaKategori);

            var listKodeDipilih = selectedPermintaanList.Select(p => p.KodePermintaan).ToList();
            var allDetailsDb = db.DetailPermintaan.AsNoTracking()
                                 .Where(d => listKodeDipilih.Contains(d.KodePermintaan))
                                 .ToList();

            var detailsWithInduk = allDetailsDb.Select(d => new
            {
                Detail = d,
                PermintaanInduk = selectedPermintaanList.FirstOrDefault(p => p.KodePermintaan == d.KodePermintaan)
            }).Where(x => x.PermintaanInduk != null).ToList();

            var grouped = detailsWithInduk
                .GroupBy(x => new { x.Detail.IdMasterBarang, x.PermintaanInduk.KodePermintaan, x.PermintaanInduk.IdJurusan })
                .Select(g => new
                {
                    IdMasterBarang = g.Key.IdMasterBarang,
                    KodePermintaan = g.Key.KodePermintaan,
                    IdJurusan = g.Key.IdJurusan,
                    TotalJumlah = g.Sum(x => x.Detail.JumlahDiminta)
                }).ToList();

            foreach (var item in grouped)
            {
                bool isExist = listDetailBon.Any(d => d.IdMasterBarang == item.IdMasterBarang && d.KodePermintaanAsal == item.KodePermintaan);

                if (!isExist)
                {
                    var barang = dictBarang.ContainsKey(item.IdMasterBarang) ? dictBarang[item.IdMasterBarang] : null;
                    if (barang != null)
                    {
                        string namaKat = (barang.IdKategori.HasValue && dictKategori.ContainsKey(barang.IdKategori.Value))
                                         ? dictKategori[barang.IdKategori.Value] : "N/A";

                        listDetailBon.Add(new DetailPengadaanViewModel
                        {
                            IdMasterBarang = item.IdMasterBarang,
                            NamaBarang = barang.NamaBarang,
                            Kategori = namaKat,
                            KodeRekening = null,
                            JumlahMasuk = item.TotalJumlah,
                            HargaSatuan = 0,
                            TotalHarga = 0,
                            IdPemasok = null,
                            NamaPemasok = "-",
                            KodePermintaanAsal = item.KodePermintaan ?? "-",
                            IdJurusanTarget = item.IdJurusan,
                            StatusTelahDibelanjakan = false
                        });
                    }
                }
            }

            dgDetailBon.DataSource = null;
            dgDetailBon.DataSource = listDetailBon;
            AturFormatKolomDetailBon();
            HitungTotalKeseluruhan();
        }

        private void dgDetailBon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgDetailBon.Columns["HargaSatuan"].Index)
            {
                var row = dgDetailBon.Rows[e.RowIndex].DataBoundItem as DetailPengadaanViewModel;
                if (row != null)
                {
                    row.TotalHarga = row.JumlahMasuk * row.HargaSatuan;
                    dgDetailBon.Refresh();
                    HitungTotalKeseluruhan();
                }
            }
        }

        private void HitungTotalKeseluruhan()
        {
            decimal total = listDetailBon.Sum(x => x.TotalHarga);
            txtTotalHarga.Text = total.ToString("N2");
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbGudang.SelectedIndex == -1 || cmbSumber.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih Gudang dan Sumber Perolehan terlebih dahulu.");
                return;
            }

            if (cmbTahunAjaran.SelectedValue == null)
            {
                MessageBox.Show("Tahun Ajaran belum dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idTahunAjaranTerpilih = (int)cmbTahunAjaran.SelectedValue;

            if (listDetailBon.Count == 0)
            {
                MessageBox.Show("Keranjang barang masih kosong.");
                return;
            }

            if (listDetailBon.Any(d => d.HargaSatuan <= 0))
            {
                MessageBox.Show("Semua barang harus memiliki harga satuan yang valid (lebih dari 0).");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPemasok.Text))
            {
                MessageBox.Show("Rekanan wajib diisi. Satu transaksi pengadaan hanya untuk satu rekanan.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var belumAdaRekening = listDetailBon.Where(d => string.IsNullOrWhiteSpace(d.KodeRekening)).Select(d => d.NamaBarang).ToList();
            if (belumAdaRekening.Count > 0)
            {
                MessageBox.Show("Kode Rekening wajib diisi untuk setiap barang.\n\nBelum terisi:\n - " + string.Join("\n - ", belumAdaRekening),
                    "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idRekanan;
            string namaRekanan;
            try
            {
                idRekanan = SimpanAtauAmbilRekanan();
                if (idRekanan <= 0) return;
                namaRekanan = db.Pemasok.Find(idRekanan).NamaPemasok;
            }
            catch (Exception exSimpanRekanan)
            {
                System.Diagnostics.Debug.WriteLine(exSimpanRekanan.ToString());
                MessageBox.Show("Gagal menyimpan data rekanan: " + exSimpanRekanan.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Rekanan header berlaku untuk seluruh keranjang (1 pengadaan = 1 rekanan).
            foreach (var itemRek in listDetailBon)
            {
                itemRek.IdPemasok = idRekanan;
                itemRek.NamaPemasok = namaRekanan;
            }
            dgDetailBon.Refresh();

            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (selectedPengadaan == null)
                {
                    var pengadaan = new Pengadaan
                    {
                        TanggalPengadaan = dtpTglPengadaan.Value,
                        IdTahunAjaran = idTahunAjaranTerpilih,
                        TotalHarga = listDetailBon.Sum(x => x.TotalHarga),
                        Keterangan = txtKeterangan.Text,
                        KodeGudang = cmbGudang.SelectedValue.ToString(),
                        IdSumberPerolehan = (int)cmbSumber.SelectedValue,
                        IdPemasok = idRekanan,
                        // Rencana pengadaan diambil dari MIN(waktu_belanja) permintaan yang dipilih.
                        WaktuBelanja = selectedPermintaanList.Where(p => p.WaktuBelanja.HasValue)
                                                                .Select(p => p.WaktuBelanja)
                                                                .OrderBy(w => w)
                                                                .FirstOrDefault(),
                        Status = "Menunggu Proses"
                    };

                    db.Pengadaan.Add(pengadaan);
                    db.SaveChanges();

                    foreach (var item in listDetailBon)
                    {
                        db.DetailPengadaan.Add(new DetailPengadaan
                        {
                            IdPengadaan = pengadaan.IdPengadaan,
                            IdMasterBarang = item.IdMasterBarang,
                            KodeRekening = item.KodeRekening,
                            JumlahMasuk = item.JumlahMasuk,
                            HargaSatuan = item.HargaSatuan,
                            // dual-write: rekanan header ditulis juga ke detail agar
                            // laporan/view lama (v_pengadaan_aset) tetap konsisten.
                            IdPemasok = idRekanan
                        });
                    }

                    foreach (var permintaan in selectedPermintaanList)
                    {
                        db.PengadaanPermintaan.Add(new PengadaanPermintaan
                        {
                            IdPengadaan = pengadaan.IdPengadaan,
                            KodePermintaan = permintaan.KodePermintaan
                        });
                    }

                    db.SaveChanges();
                            transaction.Commit();
                            MessageBox.Show("Bon pengadaan berhasil dibuat! Silakan proses belanja di menu utama.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var updatePgd = db.Pengadaan.Find(selectedPengadaan.IdPengadaan);
                    if (updatePgd == null) return;

                    updatePgd.TanggalPengadaan = dtpTglPengadaan.Value;
                    updatePgd.IdTahunAjaran = idTahunAjaranTerpilih;
                    updatePgd.TotalHarga = listDetailBon.Sum(x => x.TotalHarga);
                    updatePgd.Keterangan = txtKeterangan.Text;
                    updatePgd.KodeGudang = cmbGudang.SelectedValue.ToString();
                    updatePgd.IdSumberPerolehan = (int)cmbSumber.SelectedValue;
                    updatePgd.IdPemasok = idRekanan;
                    updatePgd.WaktuBelanja = selectedPermintaanList.Where(p => p.WaktuBelanja.HasValue)
                                                                   .Select(p => p.WaktuBelanja)
                                                                   .OrderBy(w => w)
                                                                   .FirstOrDefault();

                    var oldDetails = db.DetailPengadaan.Where(d => d.IdPengadaan == updatePgd.IdPengadaan);
                    db.DetailPengadaan.RemoveRange(oldDetails);

                    var oldPermintaanLinks = db.PengadaanPermintaan.Where(p => p.IdPengadaan == updatePgd.IdPengadaan);
                    db.PengadaanPermintaan.RemoveRange(oldPermintaanLinks);

                    db.SaveChanges();

                    foreach (var item in listDetailBon)
                    {
                        db.DetailPengadaan.Add(new DetailPengadaan
                        {
                            IdPengadaan = updatePgd.IdPengadaan,
                            IdMasterBarang = item.IdMasterBarang,
                            KodeRekening = item.KodeRekening,
                            JumlahMasuk = item.JumlahMasuk,
                            HargaSatuan = item.HargaSatuan,
                            IdPemasok = idRekanan
                        });
                    }

                    foreach (var permintaan in selectedPermintaanList)
                    {
                        db.PengadaanPermintaan.Add(new PengadaanPermintaan
                        {
                            IdPengadaan = updatePgd.IdPengadaan,
                            KodePermintaan = permintaan.KodePermintaan
                        });
                    }

                    db.SaveChanges();
                            transaction.Commit();
                            MessageBox.Show("Data pengadaan berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch
                    {
                        try { transaction.Rollback(); } catch { }
                        throw;
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show("Terjadi kesalahan sistem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataPengadaan()
        {
            txtId.Text = selectedPengadaan.IdPengadaan.ToString();
            dtpTglPengadaan.Value = selectedPengadaan.TanggalPengadaan;
            cmbGudang.SelectedValue = selectedPengadaan.KodeGudang;
            cmbStatus.SelectedItem = selectedPengadaan.Status;
            cmbSumber.SelectedValue = selectedPengadaan.IdSumberPerolehan;
            txtKeterangan.Text = selectedPengadaan.Keterangan;
            txtTotalHarga.Text = selectedPengadaan.TotalHarga?.ToString("N2");

            TahunAjaranInput.Fill(db, cmbTahunAjaran, selectedPengadaan.IdTahunAjaran);

            // Rekanan disimpan di header pengadaan; null-kan kompatibel dengan data lama.
            txtPemasok.Text = selectedPengadaan.IdPemasok.HasValue
                ? (db.Pemasok.Find(selectedPengadaan.IdPemasok.Value)?.NamaPemasok ?? "")
                : "";

            var details = db.DetailPengadaan.AsNoTracking().Where(d => d.IdPengadaan == selectedPengadaan.IdPengadaan).ToList();
            var dictBarang = db.MasterBarang.ToDictionary(b => b.IdMasterBarang, b => b);
            var dictKategori = db.Kategori.ToDictionary(k => k.IdKategori, k => k.NamaKategori);
            var dictPemasok = db.Pemasok.ToDictionary(p => p.IdPemasok, p => p.NamaPemasok);

            var kodePermintaanList = db.PengadaanPermintaan.Where(pp => pp.IdPengadaan == selectedPengadaan.IdPengadaan).Select(pp => pp.KodePermintaan).ToList();
            var detailPermintaanTerkait = db.DetailPermintaan.AsNoTracking().Where(dp => kodePermintaanList.Contains(dp.KodePermintaan)).ToList();

            listDetailBon.Clear();

            foreach (var d in details)
            {
                var barang = dictBarang.ContainsKey(d.IdMasterBarang) ? dictBarang[d.IdMasterBarang] : null;
                string namaKat = (barang != null && barang.IdKategori.HasValue && dictKategori.ContainsKey(barang.IdKategori.Value)) ? dictKategori[barang.IdKategori.Value] : "N/A";
                string namaPemasok = (d.IdPemasok.HasValue && dictPemasok.ContainsKey(d.IdPemasok.Value)) ? dictPemasok[d.IdPemasok.Value] : "-";

                var asalReqs = detailPermintaanTerkait.Where(dp => dp.IdMasterBarang == d.IdMasterBarang).Select(dp => dp.KodePermintaan).Distinct().ToList();
                string kodeAsal = asalReqs.Count > 0 ? string.Join(", ", asalReqs) : "-";

                listDetailBon.Add(new DetailPengadaanViewModel
                {
                    IdMasterBarang = d.IdMasterBarang,
                    NamaBarang = barang != null ? barang.NamaBarang : "N/A",
                    Kategori = namaKat,
                    KodeRekening = d.KodeRekening,
                    JumlahMasuk = d.JumlahMasuk,
                    HargaSatuan = d.HargaSatuan,
                    TotalHarga = d.JumlahMasuk * d.HargaSatuan,
                    IdPemasok = d.IdPemasok,
                    NamaPemasok = namaPemasok,
                    KodePermintaanAsal = kodeAsal,
                    IdJurusanTarget = null,
                    StatusTelahDibelanjakan = d.Status
                });
            }

            dgDetailBon.DataSource = null;
            dgDetailBon.DataSource = listDetailBon;
            AturFormatKolomDetailBon();

            var permintaanListRaw = db.Permintaan.AsNoTracking().Include(p => p.DetailPermintaan).Where(p => kodePermintaanList.Contains(p.KodePermintaan)).ToList();
            selectedPermintaanList.Clear();
            selectedPermintaanList.AddRange(permintaanListRaw);

            var dictJurusan = db.Jurusan.ToDictionary(j => j.IdJurusan, j => j.NamaJurusan);
            var dictPengguna = db.Pengguna.ToDictionary(u => u.IdPengguna, u => u.Username);

            var dataPermintaanTampil = permintaanListRaw.Select(p => new PermintaanSimpleViewModel
            {
                Pilih = true,
                KodePermintaan = p.KodePermintaan,
                NamaJurusan = (p.IdJurusan.HasValue && dictJurusan.ContainsKey(p.IdJurusan.Value)) ? dictJurusan[p.IdJurusan.Value] : "-",
                NamaPeminta = (p.IdPengguna.HasValue && dictPengguna.ContainsKey(p.IdPengguna.Value)) ? dictPengguna[p.IdPengguna.Value] : "-",
                Status = p.StatusPersetujuan,
                Tanggal = p.TanggalPermintaan,
                ObjekAsli = p
            }).ToList();

            dgPermintaan.DataSource = new SortableBindingList<PermintaanSimpleViewModel>(dataPermintaanTampil);
            AturFormatKolomPermintaan();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AturFormatKolomPermintaan()
        {
            dgPermintaan.ReadOnly = false;
            foreach (DataGridViewColumn col in dgPermintaan.Columns)
            {
                if (col.Name == "Pilih")
                    col.ReadOnly = false;
                else
                    col.ReadOnly = true;
            }

            if (dgPermintaan.Columns["Pilih"] != null) dgPermintaan.Columns["Pilih"].HeaderText = "Pilih";
            if (dgPermintaan.Columns["KodePermintaan"] != null) dgPermintaan.Columns["KodePermintaan"].HeaderText = "Kode Permintaan";
            if (dgPermintaan.Columns["NamaJurusan"] != null) dgPermintaan.Columns["NamaJurusan"].HeaderText = "Nama Jurusan";
            if (dgPermintaan.Columns["NamaPeminta"] != null) dgPermintaan.Columns["NamaPeminta"].HeaderText = "Nama Peminta";
            if (dgPermintaan.Columns["Status"] != null) dgPermintaan.Columns["Status"].HeaderText = "Status";
            if (dgPermintaan.Columns["Tanggal"] != null) dgPermintaan.Columns["Tanggal"].HeaderText = "Tanggal";
            if (dgPermintaan.Columns["ObjekAsli"] != null) dgPermintaan.Columns["ObjekAsli"].Visible = false;
        }

        private void AturFormatKolomDetailBon()
        {
            if (dgDetailBon.Columns["IdMasterBarang"] != null) dgDetailBon.Columns["IdMasterBarang"].Visible = false;
            if (dgDetailBon.Columns["IdPemasok"] != null) dgDetailBon.Columns["IdPemasok"].Visible = false;
            if (dgDetailBon.Columns["IdJurusanTarget"] != null) dgDetailBon.Columns["IdJurusanTarget"].Visible = false;

            if (dgDetailBon.Columns["KodePermintaanAsal"] != null) dgDetailBon.Columns["KodePermintaanAsal"].HeaderText = "Permintaan Asal";
            if (dgDetailBon.Columns["NamaBarang"] != null) dgDetailBon.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dgDetailBon.Columns["Kategori"] != null) dgDetailBon.Columns["Kategori"].HeaderText = "Kategori";
            if (dgDetailBon.Columns["KodeRekening"] != null)
            {
                dgDetailBon.Columns["KodeRekening"].HeaderText = "Kode Rekening";
                dgDetailBon.Columns["KodeRekening"].DisplayIndex = dgDetailBon.Columns["Kategori"].DisplayIndex + 1;
            }
            if (dgDetailBon.Columns["JumlahMasuk"] != null) dgDetailBon.Columns["JumlahMasuk"].HeaderText = "Jumlah Masuk";
            if (dgDetailBon.Columns["NamaPemasok"] != null) dgDetailBon.Columns["NamaPemasok"].HeaderText = "Nama Rekanan";

            if (dgDetailBon.Columns["HargaSatuan"] != null)
            {
                dgDetailBon.Columns["HargaSatuan"].HeaderText = "Harga Satuan";
                dgDetailBon.Columns["HargaSatuan"].DefaultCellStyle.Format = "C2";
                dgDetailBon.Columns["HargaSatuan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgDetailBon.Columns["TotalHarga"] != null)
            {
                dgDetailBon.Columns["TotalHarga"].HeaderText = "Total Harga";
                dgDetailBon.Columns["TotalHarga"].DefaultCellStyle.Format = "C2";
                dgDetailBon.Columns["TotalHarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgDetailBon.Columns["StatusTelahDibelanjakan"] != null)
            {
                dgDetailBon.Columns["StatusTelahDibelanjakan"].HeaderText = "Sudah Dibelanjakan?";
                dgDetailBon.Columns["StatusTelahDibelanjakan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgDetailBon.CellFormatting -= DgDetailBon_CellFormatting;
            dgDetailBon.CellFormatting += DgDetailBon_CellFormatting;
        }

        // Cari rekanan berdasarkan nama (case-insensitive); buat baru bila belum ada.
        // Dipakai baik oleh tombol Terapkan maupun saat Simpan.
        private int SimpanAtauAmbilRekanan()
        {
            string nama = txtPemasok.Text.Trim();
            if (string.IsNullOrWhiteSpace(nama)) return 0;

            var rekananDb = db.Pemasok.FirstOrDefault(p => p.NamaPemasok.ToLower() == nama.ToLower());
            if (rekananDb == null)
            {
                rekananDb = new Pemasok { NamaPemasok = nama };
                db.Pemasok.Add(rekananDb);
                db.SaveChanges();
                SetupAutoComplete();
            }
            return rekananDb.IdPemasok;
        }

        private void btnTerapkanPemasok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPemasok.Text))
            {
                MessageBox.Show("Silakan ketik nama rekanan dari daftar atau buat baru terlebih dahulu.");
                return;
            }

            int idRekanan = SimpanAtauAmbilRekanan();
            if (idRekanan <= 0) return;

            string nama = db.Pemasok.Find(idRekanan).NamaPemasok;

            if (MessageBox.Show($"Terapkan rekanan '{nama}' sebagai rekanan transaksi pengadaan ini?\n\nSeluruh barang pada keranjang akan memakai rekanan yang sama.",
                "Konfirmasi Rekanan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            foreach (var item in listDetailBon)
            {
                item.IdPemasok = idRekanan;
                item.NamaPemasok = nama;
            }

            txtPemasok.Text = nama;
            dgDetailBon.Refresh();
            MessageBox.Show($"Rekanan '{nama}' diterapkan ke seluruh transaksi pengadaan ini.");
        }

        private void txtTotalHarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void DgDetailBon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgDetailBon.Columns[e.ColumnIndex].Name == "StatusTelahDibelanjakan" && e.Value != null)
            {
                bool status = (bool)e.Value;
                e.Value = status ? "Ya" : "Belum";
                e.FormattingApplied = true;
            }
        }
    }
}