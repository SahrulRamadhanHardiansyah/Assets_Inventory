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
    public partial class MasterDataForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public MasterDataForm()
        {
            InitializeComponent();
        }

        private void MasterDataForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Data Master");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {
            MasterKategoriForm form = new MasterKategoriForm();
            form.ShowDialog();
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            MasterBarangForm form = new MasterBarangForm();
            form.ShowDialog();
        }

        private void btnLokasi_Click(object sender, EventArgs e)
        {
            MasterLokasiForm form = new MasterLokasiForm();
            form.ShowDialog();
        }

        private void btnRuang_Click(object sender, EventArgs e)
        {
            MasterRuangForm form = new MasterRuangForm();
            form.ShowDialog();
        }

        private void btnMerk_Click(object sender, EventArgs e)
        {
            MasterMerekForm form = new MasterMerekForm();
            form.ShowDialog();
        }

        private void btnSatuan_Click(object sender, EventArgs e)
        {
            MasterSatuanForm form = new MasterSatuanForm();
            form.ShowDialog();
        }

        private void btnNonAktif_Click(object sender, EventArgs e)
        {
            MasterNonAktifForm form = new MasterNonAktifForm();
            form.ShowDialog();
        }

        private void btnKondisi_Click(object sender, EventArgs e)
        {
            MasterKondisiForm form = new MasterKondisiForm();
            form.ShowDialog();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSumber_Click(object sender, EventArgs e)
        {
            MasterSumberPerolehanForm form = new MasterSumberPerolehanForm();
            form.ShowDialog();
        }

        private void btnRombel_Click(object sender, EventArgs e)
        {
            MasterRombelForm form = new MasterRombelForm();
            form.ShowDialog();
        }

        private void btnTahun_Click(object sender, EventArgs e)
        {
            MasterTahunAjaranForm form = new MasterTahunAjaranForm();
            form.ShowDialog();
        }
    }
}
