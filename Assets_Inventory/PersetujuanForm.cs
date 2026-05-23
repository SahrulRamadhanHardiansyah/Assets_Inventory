using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class PersetujuanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Permintaan selectedPermintaan;
        AppDbContext db = new AppDbContext();

        public PersetujuanForm()
        {
            InitializeComponent();
            if (selectedPermintaan == null)
            {
                MessageBox.Show("Data permintaan tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void PersetujuanForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSetuju_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlasan.Text))
            {
                MessageBox.Show("Alasan persetujuan harus diisi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var permintaan = db.Permintaan.Find(selectedPermintaan.KodePermintaan);
                permintaan.AlasanDisetujui = txtAlasan.Text;
                permintaan.IdPenyetuju = Properties.Settings.Default.userId;
                permintaan.TanggalPersetujuan = DateTime.Now.Date;
                permintaan.StatusPersetujuan = "Disetujui";
                db.SaveChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
