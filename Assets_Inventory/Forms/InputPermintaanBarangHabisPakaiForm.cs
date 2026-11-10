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

namespace Assets_Inventory.Forms
{
    public partial class InputPermintaanBarangHabisPakaiForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public PermintaanHp selectedPermintaan = null;
        public bool isDetailMode = false;

        AppDbContext db = new AppDbContext();

        private Dictionary<string, int> dictBarang = new Dictionary<string, int>();

        private BindingList<DetailPermintaanHp> listDetail = new BindingList<DetailPermintaanHp>();

        public InputPermintaanBarangHabisPakaiForm()
        {
            InitializeComponent();
        }

        private void InputPermintaanBarangHabisPakaiForm_Load(object sender, EventArgs e)
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

            if (isDetailMode)
            {
                this.Text = "Detail Permintaan Habis Pakai";
                txtKode.ReadOnly = true;
                dtTgl.Enabled = false;
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
            string prefix = $"PRH-{DateTime.Now.Year}-";

            var lastPermintaan = db.PermintaanHp
                                   .Where(p => p.KodePermintaanHp.StartsWith(prefix))
                                   .OrderByDescending(p => p.KodePermintaanHp)
                                   .FirstOrDefault();

            int nextUrutan = 1;

            if (lastPermintaan != null && !string.IsNullOrEmpty(lastPermintaan.KodePermintaanHp))
            {
                string strAngka = lastPermintaan.KodePermintaanHp.Substring(prefix.Length);
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

                listDetail = new BindingList<DetailPermintaanHp>();
                dg.DataSource = listDetail;
            }
            else
            {
                txtKode.Text = selectedPermintaan.KodePermintaanHp;
                cmbJurusan.SelectedValue = selectedPermintaan.IdJurusan;
                dtTgl.Value = selectedPermintaan.TanggalPermintaan;
                txtKeterangan.Text = selectedPermintaan.KeteranganKeperluan;

                TahunAjaranInput.Fill(db, cmbTahunAjaran, selectedPermintaan.IdTahunAjaran);

                var details = db.DetailPermintaanHp
                                .Include(d => d.IdMasterBarangNavigation)
                                .Where(d => d.KodePermintaanHp == selectedPermintaan.KodePermintaanHp)
                                .ToList();

                listDetail = new BindingList<DetailPermintaanHp>(details);
                // bindingSource2 masih menunjuk DetailPermintaan (model salah) — lepas dulu
                // supaya DataSource langsung ke list tanpa remap properti yang tidak ada.
                dg.DataSource = null;
                dg.DataSource = listDetail;
            }
            AturKolomDetail();
        }

        private void AturKolomDetail()
        {
            // Kolom ID di-bound ke IdDetailPermintaan (punya DetailPermintaan, BUKAN DetailPermintaanHp
            // yang PK-nya IdDetailPermintaanHp) -> selalu kosong. Bind ulang + sembunyikan untuk baru.
            if (dg.Columns["dataGridViewTextBoxColumn1"] != null)
            {
                dg.Columns["dataGridViewTextBoxColumn1"].DataPropertyName = "IdDetailPermintaanHp";
                if (selectedPermintaan == null) dg.Columns["dataGridViewTextBoxColumn1"].Visible = false;
            }
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

            listDetail.Add(new DetailPermintaanHp
            {
                IdMasterBarang = idBarangTerpilih,
                IdMasterBarangNavigation = barang,
                JumlahDiminta = jumlah,
                AlasanKebutuhan = txtAlasan.Text,
                KodePermintaanHp = txtKode.Text
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

            try
            {
                if (selectedPermintaan == null)
                {
                    int userId = Properties.Settings.Default.userId > 0 ? Properties.Settings.Default.userId : 1;

                    var baru = new PermintaanHp
                    {
                        KodePermintaanHp = txtKode.Text,
                        IdJurusan = (int)cmbJurusan.SelectedValue,
                        IdTahunAjaran = idTahunAjaranTerpilih,
                        IdPengguna = userId,
                        KeteranganKeperluan = txtKeterangan.Text,
                        StatusPersetujuan = "Menunggu",
                        TanggalPermintaan = dtTgl.Value,
                        DetailPermintaanHp = listDetail.ToList()
                    };

                    db.PermintaanHp.Add(baru);
                    db.SaveChanges();

                    MessageBox.Show("Data permintaan habis pakai berhasil disubmit dan menunggu persetujuan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is DetailPermintaanHp dp)
            {
                if (dg.Columns["HapusColumn"] != null && dg.Columns["HapusColumn"].Index == e.ColumnIndex)
                {
                    if (MessageBox.Show($"Apakah anda yakin ingin menghapus barang {dp.IdMasterBarangNavigation.NamaBarang} dari keranjang?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        listDetail.Remove(dp);
                    }
                }
            }
        }

        private void txtJumlah_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dg.Columns[e.ColumnIndex].DataPropertyName != "IdMasterBarangNavigation") return;

            // Kolom bound ke properti navigasi (objek) — tanpa override ini DataGridView
            // menampilkan ToString() = "Assets_Inventory.Models.MasterBarang".
            if (dg.Rows[e.RowIndex].DataBoundItem is DetailPermintaanHp dp)
            {
                e.Value = dp.IdMasterBarangNavigation?.NamaBarang ?? "(barang tidak ditemukan)";
                e.FormattingApplied = true;
            }
        }
    }
}