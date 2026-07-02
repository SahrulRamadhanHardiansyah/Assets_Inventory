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
    public partial class DetailPermintaanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private string selectedPermintaan = "";
        AppDbContext db = new AppDbContext();

        public DetailPermintaanForm(string selectedPermintaan)
        {
            InitializeComponent();
            this.selectedPermintaan = selectedPermintaan;
        }

        private void DetailPermintaanForm_Load(object sender, EventArgs e)
        {
            if (selectedPermintaan != "")
            {
                lblTitle.Text = $"Detail Permintaan - KODE {selectedPermintaan}";
                loadDetailData();
            }
            else
            {
                MessageBox.Show("ID Permintaan tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void loadDetailData()
        {
            dg.DataSource = db.DetailPermintaan
                .Where(d => d.KodePermintaan == selectedPermintaan)
                .Select(d => new
                {
                    NamaBarang = d.IdMasterBarangNavigation != null ? d.IdMasterBarangNavigation.NamaBarang : "N/A",
                    d.JumlahDiminta,
                    d.AlasanKebutuhan
                }).ToList();

            dg.Columns["NamaBarang"].HeaderText = "Nama Barang";
            dg.Columns["JumlahDiminta"].HeaderText = "Jumlah Diminta";
            dg.Columns["AlasanKebutuhan"].HeaderText = "Alasan Kebutuhan";
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }
    }
}
