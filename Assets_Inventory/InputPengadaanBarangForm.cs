using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class InputPengadaanBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Pengadaan selectedPengadaan;
        AppDbContext db = new AppDbContext();
        private List<Permintaan> selectedPermintaanList = new List<Permintaan>();
        private BindingList<DetailPengadaanViewModel> listDetailBon = new BindingList<DetailPengadaanViewModel>();

        public class DetailPengadaanViewModel
        {
            public int IdMasterBarang { get; set; }
            public string NamaBarang { get; set; }
            public string Kategori { get; set; }
            public int JumlahMasuk { get; set; }
            public decimal HargaSatuan { get; set; }
            public decimal TotalHarga { get; set; }
        }

        public InputPengadaanBarangForm()
        {
            InitializeComponent();
        }

        private void InputPengadaanBarangForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();

            if (selectedPengadaan == null)
            {
                txtId.Text = GenerateIdPengadaan();
                dtpTglPengadaan.Value = DateTime.Now;
                cmbStatus.SelectedItem = "diproses";
                cmbStatus.Enabled = false; 
                LoadDaftarPermintaan();

                listDetailBon = new BindingList<DetailPengadaanViewModel>();
                dgDetailBon.DataSource = listDetailBon;
            }
            else
            {
                LoadDataPengadaan();
            }
        }

        private void SetupComboBoxes()
        {
            cmbPemasok.DataSource = db.Pemasok.ToList();
            cmbPemasok.DisplayMember = "NamaPemasok";
            cmbPemasok.ValueMember = "IdPemasok";
            cmbPemasok.SelectedIndex = -1;
            cmbGudang.DataSource = db.Gudang.ToList();
            cmbGudang.DisplayMember = "NamaGudang";
            cmbGudang.ValueMember = "KodeGudang";
            cmbGudang.SelectedIndex = -1;
            cmbKondisi.DataSource = db.Kondisi.ToList();
            cmbKondisi.DisplayMember = "NamaKondisi";
            cmbKondisi.ValueMember = "IdKondisi";
            cmbKondisi.SelectedIndex = -1;
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("diproses");
            cmbStatus.Items.Add("dibelanjakan");
            cmbStatus.Items.Add("selesai");
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
            var permintaanDisetujui = db.Permintaan
                .Include(p => p.IdJurusanNavigation)
                .Include(p => p.DetailPermintaan)
                .Where(p => p.StatusPersetujuan == "Disetujui")
                .Where(p => !p.PengadaanPermintaan.Any()) 
                .ToList();

            dgPermintaan.DataSource = new SortableBindingList<Permintaan>(permintaanDisetujui);
        }

        private void btnPilihPermintaan_Click(object sender, EventArgs e)
        {
            selectedPermintaanList.Clear();

            foreach (DataGridViewRow row in dgPermintaan.Rows)
            {
                if (row.Cells["PilihColumn"].Value != null && (bool)row.Cells["PilihColumn"].Value)
                {
                    var kodePermintaan = row.Cells["KodePermintaan"].Value.ToString();
                    var permintaan = db.Permintaan
                        .Include(p => p.DetailPermintaan)
                        .Include(p => p.IdJurusanNavigation)
                        .FirstOrDefault(p => p.KodePermintaan == kodePermintaan);

                    if (permintaan != null)
                        selectedPermintaanList.Add(permintaan);
                }
            }

            if (selectedPermintaanList.Count == 0)
            {
                MessageBox.Show("Pilih minimal satu permintaan yang sudah disetujui.");
                return;
            }

            GenerateDetailBon();
        }

        private void GenerateDetailBon()
        {
            listDetailBon.Clear();

            var grouped = selectedPermintaanList
                .SelectMany(p => p.DetailPermintaan)
                .GroupBy(d => d.IdMasterBarang)
                .Select(g => new
                {
                    IdMasterBarang = g.Key,
                    TotalJumlah = g.Sum(x => x.JumlahDiminta),
                    Barang = db.MasterBarang
                        .Include(b => b.IdKategoriNavigation)
                        .FirstOrDefault(b => b.IdMasterBarang == g.Key)
                })
                .ToList();

            foreach (var item in grouped)
            {
                if (item.Barang != null)
                {
                    listDetailBon.Add(new DetailPengadaanViewModel
                    {
                        IdMasterBarang = item.IdMasterBarang,
                        NamaBarang = item.Barang.NamaBarang,
                        Kategori = item.Barang.IdKategoriNavigation?.NamaKategori ?? "-",
                        JumlahMasuk = item.TotalJumlah,
                        HargaSatuan = 0,
                        TotalHarga = 0
                    });
                }
            }

            dgDetailBon.DataSource = listDetailBon;
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
            if (cmbPemasok.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih pemasok terlebih dahulu.");
                return;
            }

            if (cmbGudang.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih gudang tujuan terlebih dahulu.");
                return;
            }

            if (listDetailBon.Count == 0)
            {
                MessageBox.Show("Pilih permintaan dan generate detail barang terlebih dahulu.");
                return;
            }

            if (listDetailBon.Any(d => d.HargaSatuan <= 0))
            {
                MessageBox.Show("Semua barang harus memiliki harga satuan yang valid.");
                return;
            }

            try
            {
                var pengadaan = new Pengadaan
                {
                    TanggalPengadaan = dtpTglPengadaan.Value,
                    TotalHarga = listDetailBon.Sum(x => x.TotalHarga),
                    Keterangan = txtKeterangan.Text,
                    KodeGudang = cmbGudang.SelectedValue.ToString(),
                    Status = "diproses"
                };

                db.Pengadaan.Add(pengadaan);
                db.SaveChanges(); 

                foreach (var item in listDetailBon)
                {
                    db.DetailPengadaan.Add(new DetailPengadaan
                    {
                        IdPengadaan = pengadaan.IdPengadaan,
                        IdMasterBarang = item.IdMasterBarang,
                        JumlahMasuk = item.JumlahMasuk,
                        HargaSatuan = item.HargaSatuan
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

                MessageBox.Show(
                    $"Bon pengadaan berhasil dibuat!\n\n" +
                    $"ID: {pengadaan.IdPengadaan}\n" +
                    $"Total: {pengadaan.TotalHarga:C}\n" +
                    $"Status: {pengadaan.Status}\n\n" +
                    $"Barang akan masuk ke gudang saat status diubah menjadi 'dibelanjakan'.",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProsesBelanja_Click(object sender, EventArgs e)
        {
            if (selectedPengadaan == null || selectedPengadaan.Status != "diproses")
            {
                MessageBox.Show("Hanya pengadaan dengan status 'diproses' yang bisa diproses.");
                return;
            }

            if (MessageBox.Show(
                "Proses belanja akan:\n" +
                "1. Generate kode inventaris per unit barang\n" +
                "2. Masukkan barang ke tabel aset (status: Di Gudang)\n" +
                "3. Ubah status pengadaan jadi 'dibelanjakan'\n\n" +
                "Lanjutkan?",
                "Konfirmasi Proses Belanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                var detailList = db.DetailPengadaan
                    .Include(d => d.IdMasterBarangNavigation)
                    .Where(d => d.IdPengadaan == selectedPengadaan.IdPengadaan)
                    .ToList();

                int inventarisCounter = 1;
                string tahun = DateTime.Now.Year.ToString();

                List<Aset> asetBaruList = new List<Aset>();

                foreach (var detail in detailList)
                {
                    for (int i = 0; i < detail.JumlahMasuk; i++)
                    {
                        string kodeInventaris = $"INV-{tahun}-{selectedPengadaan.IdPengadaan:D3}-{inventarisCounter:D3}";

                        var aset = new Aset
                        {
                            KodeBarang = Guid.NewGuid().ToString("N").Substring(0, 20),
                            IdDetailPengadaan = detail.IdDetailPengadaan,
                            IdMasterBarang = detail.IdMasterBarang,
                            IdJurusan = null,
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
                            Keterangan = $"Dari pengadaan #{selectedPengadaan.IdPengadaan}"
                        };

                        db.Aset.Add(aset);
                        asetBaruList.Add(aset); 
                        inventarisCounter++;
                    }
                }

                selectedPengadaan.Status = "dibelanjakan";
                db.SaveChanges();

                DialogResult opsi = MessageBox.Show(
                    $"Proses belanja selesai! {inventarisCounter - 1} unit barang telah masuk ke gudang.\n\n" +
                    $"Apakah Anda ingin melengkapi detail aset (No. Seri, Lokasi, Ruang, Gambar) sekarang?\n\n" +
                    $"Klik 'Yes' untuk melengkapi sekarang.\n" +
                    $"Klik 'No' untuk melengkapinya nanti via Notifikasi Dashboard.",
                    "Lengkapi Data Aset",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (opsi == DialogResult.Yes)
                {
                    KelengkapanAsetForm formLengkap = new KelengkapanAsetForm(asetBaruList);
                    formLengkap.ShowDialog();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataPengadaan()
        {
            txtId.Text = selectedPengadaan.IdPengadaan.ToString();
            dtpTglPengadaan.Value = selectedPengadaan.TanggalPengadaan;
            cmbGudang.SelectedValue = selectedPengadaan.KodeGudang;
            cmbStatus.SelectedItem = selectedPengadaan.Status;
            txtKeterangan.Text = selectedPengadaan.Keterangan;
            txtTotalHarga.Text = selectedPengadaan.TotalHarga?.ToString("N2");

            var details = db.DetailPengadaan
                .Include(d => d.IdMasterBarangNavigation)
                .Include(d => d.IdMasterBarangNavigation.IdKategoriNavigation)
                .Where(d => d.IdPengadaan == selectedPengadaan.IdPengadaan)
                .ToList();

            listDetailBon = new BindingList<DetailPengadaanViewModel> (
                details.Select(d => new DetailPengadaanViewModel
                {
                    IdMasterBarang = d.IdMasterBarang,
                    NamaBarang = d.IdMasterBarangNavigation.NamaBarang,
                    Kategori = d.IdMasterBarangNavigation.IdKategoriNavigation?.NamaKategori ?? "-",
                    JumlahMasuk = d.JumlahMasuk,
                    HargaSatuan = d.HargaSatuan,
                    TotalHarga = d.JumlahMasuk * d.HargaSatuan
                }).ToList()
            );

            dgDetailBon.DataSource = listDetailBon;

            var kodePermintaanList = db.PengadaanPermintaan
                .Where(pp => pp.IdPengadaan == selectedPengadaan.IdPengadaan)
                .Select(pp => pp.KodePermintaan)
                .ToList();

            selectedPermintaanList = db.Permintaan
                .Include(p => p.IdJurusanNavigation)
                .Where(p => kodePermintaanList.Contains(p.KodePermintaan))
                .ToList();

            dgPermintaan.DataSource = new SortableBindingList<Permintaan>(selectedPermintaanList);

            if (selectedPengadaan.Status == "dibelanjakan" || selectedPengadaan.Status == "selesai")
            {
                btnSimpan.Enabled = false;
                btnPilihPermintaan.Enabled = false;
                btnProsesBelanja.Enabled = false;
                cmbStatus.Enabled = false;
            }
            else if (selectedPengadaan.Status == "diproses")
            {
                btnProsesBelanja.Enabled = true;
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgPermintaan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgPermintaan.Rows[e.RowIndex].DataBoundItem is Permintaan p)
            {
                if (idJurusanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = p.IdJurusanNavigation?.NamaJurusan ?? "-";
                }
                if (idPenggunaNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = p.IdPenggunaNavigation?.Username ?? "-";
                }
                if (idPenyetujuNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = p.IdPenyetujuNavigation?.Username ?? "-";
                }
            }
        }
    }
}
