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
    public partial class LaporanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        MainForm parent = Application.OpenForms.OfType<MainForm>().FirstOrDefault();

        public LaporanForm()
        {
            InitializeComponent();
        }

        private void LaporanForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Laporan");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new KartuInventarisRuanganUC());
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanPengadaanBarangUC());
            this.Close();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanBarangInventarisUC());
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanPenyusutanNilaiBarangUC());
            this.Close();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanTanahInventarisUC());
            this.Close();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanBangunanInventarisUC());
            this.Close();
        }

        private void linkLabel10_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanPengembalianBarangUC());
            this.Close();
        }

        private void linkLabel14_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanProsesOpnameUC());
            this.Close();
        }

        private void linkLabel13_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanMutasiBarangUC());
            this.Close();
        }

        private void linkLabel12_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanPeminjamanBarangUC());
            this.Close();
        }

        private void linkLabel11_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanJatuhTempoPeminjamanUC());
            this.Close();
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanBarangNonAktifUC());
            this.Close();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanBangunanNonAktifUC());
            this.Close();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (parent != null) parent.ChangeView(new LaporanTanahNonAktifUC());
            this.Close();
        }
    }
}