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
    public partial class DetailPengadaanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private int selectedPengadaanId = 0;
        AppDbContext db = new AppDbContext();

        public DetailPengadaanForm(int selectedPengadaanId)
        {
            InitializeComponent();
            this.selectedPengadaanId = selectedPengadaanId;
        }

        private void DetailPengadaanForm_Load(object sender, EventArgs e)
        {
            if (selectedPengadaanId != 0)
            {
                lblTitle.Text = $"Detail Pengadaan - ID {selectedPengadaanId}";
                loadDetailData();
            } else
            {
                MessageBox.Show("ID Pengadaan tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void loadDetailData()
        {
            dg.DataSource = db.DetailPengadaan.AsNoTracking()
                .Where(d => d.IdPengadaan == selectedPengadaanId)
                .Select(d => new
                {
                    d.IdMasterBarangNavigation.NamaBarang,
                    d.IdPemasokNavigation.NamaPemasok,
                    d.HargaSatuan,
                    d.JumlahMasuk,
                })
                .ToList(); ;

            dg.Columns["NamaBarang"].HeaderText = "Nama Barang";
            dg.Columns["NamaPemasok"].HeaderText = "Nama Pemasok";
            dg.Columns["JumlahMasuk"].HeaderText = "Jumlah Masuk";
            dg.Columns["HargaSatuan"].HeaderText = "Harga Satuan";
            dg.Columns["HargaSatuan"].DefaultCellStyle.Format = "C2";
            dg.Columns["HargaSatuan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
