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
            dg.DataSource = db.DetailPengadaan
                .Include(d => d.IdPemasokNavigation)
                .Include(d => d.IdMasterBarangNavigation)
                .Where(d => d.IdPengadaan == selectedPengadaanId).ToList();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
