using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

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
