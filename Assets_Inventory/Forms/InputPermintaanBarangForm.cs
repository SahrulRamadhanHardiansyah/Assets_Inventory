using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class InputPermintaanBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Permintaan selectedPermintaan = null;
        public bool isDetailMode = false; 

        AppDbContext db = new AppDbContext();

        private Dictionary<string, int> dictBarang = new Dictionary<string, int>();
        private Dictionary<string, int> dictTahunAjaran = new Dictionary<string, int>(); 

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

            if (isDetailMode)
            {
                this.Text = "Detail Permintaan Barang";
                txtKode.ReadOnly = true;
                dtTgl.Enabled = false;
                cmbJurusan.Enabled = false;
                txtKeterangan.ReadOnly = true;
                txtTahunAjaran.ReadOnly = true;

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

                listDetail = new BindingList<DetailPermintaan>();
                dg.DataSource = listDetail;
            }
            else
            {
                txtKode.Text = selectedPermintaan.KodePermintaan;
                cmbJurusan.SelectedValue = selectedPermintaan.IdJurusan;
                dtTgl.Value = selectedPermintaan.TanggalPermintaan;
                txtKeterangan.Text = selectedPermintaan.KeteranganKeperluan;

                if (selectedPermintaan.IdTahunAjaran.HasValue)
                {
                    var taHistory = db.TahunAjaran.Find(selectedPermintaan.IdTahunAjaran.Value);
                    if (taHistory != null)
                    {
                        txtTahunAjaran.Text = $"{taHistory.TahunAjaran1} - Semester {taHistory.Semester}";
                    }
                }

                var details = db.DetailPermintaan
                                .Include(d => d.IdMasterBarangNavigation)
                                .Where(d => d.KodePermintaan == selectedPermintaan.KodePermintaan)
                                .ToList();

                listDetail = new BindingList<DetailPermintaan>(details);
                dg.DataSource = listDetail;
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

            var listTA = db.TahunAjaran.ToList();
            AutoCompleteStringCollection collectionTA = new AutoCompleteStringCollection();
            dictTahunAjaran.Clear();

            foreach (var ta in listTA)
            {
                string formatTA = $"{ta.TahunAjaran1} - Semester {ta.Semester}";
                collectionTA.Add(formatTA);

                if (!dictTahunAjaran.ContainsKey(formatTA))
                {
                    dictTahunAjaran.Add(formatTA, ta.IdTahunAjaran);
                }

                if (selectedPermintaan == null && ta.IsActive == true)
                {
                    txtTahunAjaran.Text = formatTA;
                }
            }

            txtTahunAjaran.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTahunAjaran.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTahunAjaran.AutoCompleteCustomSource = collectionTA;
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
                KodePermintaan = txtKode.Text
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

            if (!dictTahunAjaran.TryGetValue(txtTahunAjaran.Text, out int idTahunAjaranTerpilih))
            {
                MessageBox.Show("Tahun Ajaran tidak valid! Silakan hapus ketikan lalu pilih ulang dari saran yang muncul.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

                    var baru = new Permintaan
                    {
                        KodePermintaan = txtKode.Text,
                        IdJurusan = (int)cmbJurusan.SelectedValue,
                        IdTahunAjaran = idTahunAjaranTerpilih,
                        IdPengguna = userId,
                        KeteranganKeperluan = txtKeterangan.Text,
                        StatusPersetujuan = "Menunggu",
                        TanggalPermintaan = dtTgl.Value,
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
            if (dg.Rows[e.RowIndex].DataBoundItem is DetailPermintaan dp)
            {
                if (idMasterBarangNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = dp.IdMasterBarangNavigation?.NamaBarang;
                }
            }
        }
    }
}