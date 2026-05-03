using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private Client apiClient;

        public InputPengadaanBarangForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
        }

        private void InputPengadaanBarangForm_Load(object sender, EventArgs e)
        {
            loadComboBox();
        }

        private async void loadComboBox()
        {
            var kategori = await apiClient.IndexKategoriAsync();
            var barang = await apiClient.IndexMasterBarangAsync();
            var lokasi = await apiClient.IndexLokasiAsync();
            var merk = await apiClient.IndexMerekAsync();
            var satuan = await apiClient.IndexSatuanAsync();

            cmbKategori.DataSource = kategori.Data.ToList();
            cmbKategori.DisplayMember = "Nama_kategori";
            cmbKategori.ValueMember = "Id_kategori";
            cmbKategori.SelectedIndex = -1;

            cmbNama.DataSource = barang.Data.ToList();
            cmbNama.DisplayMember = "Nama_barang";
            cmbNama.ValueMember = "Id_master_barang";
            cmbNama.SelectedIndex = -1;

            cmbLokasi.DataSource = lokasi.Data.ToList();
            cmbLokasi.DisplayMember = "Nama_lokasi";
            cmbLokasi.ValueMember = "Id_lokasi";
            cmbLokasi.SelectedIndex = -1;

            cmbMerk.DataSource = merk.Data.ToList();
            cmbMerk.DisplayMember = "Nama_merek";
            cmbMerk.ValueMember = "Id_merek";
            cmbMerk.SelectedIndex = -1;

            cmbSatuan.DataSource = satuan.Data.ToList();
            cmbSatuan.DisplayMember = "Nama_satuan";
            cmbSatuan.ValueMember = "Id_satuan";
            cmbSatuan.SelectedIndex = -1;

            cmbKondisi.Items.Clear();
            cmbKondisi.Items.Add("Baru");
            cmbKondisi.Items.Add("Baik");
            cmbKondisi.Items.Add("Sedang");
            cmbKondisi.Items.Add("Rusak");
            cmbKondisi.SelectedIndex = -1;
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowseBarang_Click(object sender, EventArgs e)
        {
            if (cmbKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih kategori terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BarangForm form = new BarangForm((int)cmbKategori.SelectedValue);
            if (form.ShowDialog() == DialogResult.OK) cmbNama.SelectedItem = form.selectedBarang;
        }
    }
}
