using Assets_Inventory.Helper;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
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
    public partial class ProsesNonAktifForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public ProsesNonAktifForm()
        {
            InitializeComponent();
        }

        private void ProsesNonAktifForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Non Aktif Barang");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            NonAktifBarangForm form = new NonAktifBarangForm();
            form.ShowDialog();
        }

        private void btnTanah_Click(object sender, EventArgs e)
        {
            NonAktifTanahForm form = new NonAktifTanahForm();
            form.ShowDialog();
        }

        private void btnBangunan_Click(object sender, EventArgs e)
        {
            NonAktifBangunanForm form = new NonAktifBangunanForm();
            form.ShowDialog();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
