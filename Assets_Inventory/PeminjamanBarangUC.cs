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
    public partial class PeminjamanBarangUC : UserControl
    {
        public PeminjamanBarangUC()
        {
            InitializeComponent();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            TransaksiPeminjamanForm form = new TransaksiPeminjamanForm();
            form.ShowDialog();
        }
    }
}
