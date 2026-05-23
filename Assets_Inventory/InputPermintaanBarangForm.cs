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

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            dictBarang.Clear();

            foreach (var b in listBarang)
            {
                string namaKategori = b.IdKategoriNavigation != null ? b.IdKategoriNavigation.NamaKategori : "Tanpa Kategori";
                string formattedText = $"{b.NamaBarang} ({namaKategori})";

                collection.Add(formattedText);

                if (!dictBarang.ContainsKey(formattedText))
                {
                    dictBarang.Add(formattedText, b.IdMasterBarang);
                }
            }

            txtBarang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBarang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBarang.AutoCompleteCustomSource = collection;
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
                MessageBox.Show("Barang tidak ditemukan! Silakan pilih dari daftar yang muncul.");
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
                MessageBox.Show("Keterangan keperluan harus diisi.");
                return;
            }

            if (listDetail.Count == 0)
            {
                MessageBox.Show("Anda belum menambahkan barang satupun ke dalam daftar permintaan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        IdPengguna = userId,
                        KeteranganKeperluan = txtKeterangan.Text,
                        StatusPersetujuan = "Menunggu",
                        TanggalPermintaan = dtTgl.Value,
                        DetailPermintaan = listDetail.ToList()
                    };

                    db.Permintaan.Add(baru);
                }
                else 
                {
                    var prmUpdate = db.Permintaan
                                      .Include(p => p.DetailPermintaan)
                                      .FirstOrDefault(p => p.KodePermintaan == txtKode.Text);

                    if (prmUpdate != null)
                    {
                        prmUpdate.IdJurusan = (int)cmbJurusan.SelectedValue;
                        prmUpdate.TanggalPermintaan = dtTgl.Value;
                        prmUpdate.KeteranganKeperluan = txtKeterangan.Text;

                        db.DetailPermintaan.RemoveRange(prmUpdate.DetailPermintaan);

                        foreach (var item in listDetail)
                        {
                            db.DetailPermintaan.Add(new DetailPermintaan
                            {
                                KodePermintaan = prmUpdate.KodePermintaan,
                                IdMasterBarang = item.IdMasterBarang,
                                JumlahDiminta = item.JumlahDiminta,
                                AlasanKebutuhan = item.AlasanKebutuhan
                            });
                        }
                    }
                }

                db.SaveChanges();

                MessageBox.Show("Data permintaan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
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
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is DetailPermintaan dp)
            {
                if (HapusColumn.Index == e.ColumnIndex)
                {
                    if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {dp.IdMasterBarangNavigation.NamaBarang}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
