using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class BarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private Client apiClient;
        private int IdKategori;
        public MasterBarangResource selectedBarang;

        public BarangForm(int IdKategori)
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
            this.IdKategori = IdKategori;
        }

        private void BarangForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            var response = await apiClient.IndexMasterBarangAsync();
            var cari = txtSearch.Text.ToLower();

            var filteredData = response.Data
                .Where(b => b.Id_kategori == IdKategori &&
                           (string.IsNullOrEmpty(cari) || b.Nama_barang.ToLower().Contains(cari)))
                .ToList();

            dg.DataSource = filteredData;

            foreach (DataGridViewColumn col in dg.Columns)
            {
                col.Visible = false; 
            }

            dg.Columns["Id_master_barang"].Visible = true;
            dg.Columns["Id_master_barang"].HeaderText = "ID";

            dg.Columns["Nama_barang"].Visible = true;
            dg.Columns["Nama_barang"].HeaderText = "Nama Barang";
        }

        private async void dg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is MasterBarangResource b)
            {
                selectedBarang = b;

                if (selectedBarang != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                } else
                {
                    MessageBox.Show("Data tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Data tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            MasterBarangForm form = new MasterBarangForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
