using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
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
    public partial class PengembalianBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();

        public class PengembalianViewModel
        {
            public int IdPengembalian { get; set; }
            public string NomorPeminjaman { get; set; }
            public string NamaPeminjam { get; set; }
            public DateTime TanggalKembali { get; set; }
            public Pengembalian ObjekAsli { get; set; }
        }

        public PengembalianBarangUC()
        {
            InitializeComponent();
        }

        private void PengembalianBarangUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Data Pengembalian");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainForm parentForm = this.ParentForm as MainForm;
                DashboardUC dashboardUC = new DashboardUC();
                if (parentForm != null) parentForm.ChangeView(dashboardUC);
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnUbah.Enabled = hakAkses.HakUbah;
            btnHapus.Enabled = hakAkses.HakHapus;

            dtpMulai.Value = DateTime.Now.AddYears(-5);
            dtpSampai.Value = DateTime.Now;
            loadData();
        }

        private void loadData()
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cari = txtCari.Text.ToLower().Trim();
            var tglMulai = dtpMulai.Value.Date;
            var tglSampai = dtpSampai.Value.Date.AddDays(1).AddTicks(-1);

            var dictPeminjam = db.Peminjaman.AsNoTracking().ToDictionary(p => p.NomorPeminjaman, p => p.NamaPeminjam);

            var dataRaw = db.Pengembalian.AsNoTracking()
                .Where(p => p.TanggalKembali >= tglMulai && p.TanggalKembali <= tglSampai)
                .ToList();

            var dataTampil = dataRaw.Select(p => new PengembalianViewModel
            {
                IdPengembalian = p.IdPengembalian,
                NomorPeminjaman = p.NomorPeminjaman,
                NamaPeminjam = dictPeminjam.ContainsKey(p.NomorPeminjaman) ? dictPeminjam[p.NomorPeminjaman] : "Tidak Diketahui",
                TanggalKembali = p.TanggalKembali,
                ObjekAsli = p
            })
            .Where(p => p.NomorPeminjaman.ToLower().Contains(cari) || p.NamaPeminjam.ToLower().Contains(cari))
            .OrderByDescending(p => p.TanggalKembali)
            .ToList();

            dg.DataSource = new SortableBindingList<PengembalianViewModel>(dataTampil);
            lblTotal.Text = $"Total Record : {dataTampil.Count}";

            if (dg.Columns["IdPengembalian"] != null) dg.Columns["IdPengembalian"].HeaderText = "ID";
            if (dg.Columns["NomorPeminjaman"] != null) dg.Columns["NomorPeminjaman"].HeaderText = "No. Peminjaman";
            if (dg.Columns["NamaPeminjam"] != null) dg.Columns["NamaPeminjam"].HeaderText = "Nama Peminjam";
            if (dg.Columns["TanggalKembali"] != null) dg.Columns["TanggalKembali"].HeaderText = "Tanggal Kembali";
            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            TransaksiPengembalianForm form = new TransaksiPengembalianForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PengembalianViewModel vm)
            {
                var kembali = db.Pengembalian.Find(vm.IdPengembalian);
                TransaksiPengembalianForm form = new TransaksiPengembalianForm();
                form.selectedPengembalian = kembali;
                form.isDetailMode = false;
                if (form.ShowDialog() == DialogResult.OK) loadData();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah terlebih dahulu.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PengembalianViewModel vm)
            {
                if (MessageBox.Show($"Yakin ingin menghapus riwayat pengembalian untuk transaksi {vm.NomorPeminjaman}?\n\nPerhatian: Ini akan mengembalikan status transaksi peminjaman menjadi 'Dipinjam', namun status aset di gudang harus dicek manual.", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        var kembali = db.Pengembalian.Find(vm.IdPengembalian);
                        if (kembali != null)
                        {
                            var pinjam = db.Peminjaman.Find(kembali.NomorPeminjaman);
                            if (pinjam != null) pinjam.StatusPeminjaman = "Sedang Dipinjam";

                            db.Pengembalian.Remove(kembali);
                            db.SaveChanges();
                            loadData();
                            MessageBox.Show("Riwayat pengembalian berhasil dihapus.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PengembalianViewModel vm)
            {
                var kembali = db.Pengembalian.Find(vm.IdPengembalian);
                TransaksiPengembalianForm form = new TransaksiPengembalianForm();
                form.selectedPengembalian = kembali;
                form.isDetailMode = true;
                form.ShowDialog();
            }
        }

        private void btnCetakBukti_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PengembalianViewModel vm)
            {
                TransaksiPengembalianForm form = new TransaksiPengembalianForm();
                form.selectedPengembalian = db.Pengembalian.Find(vm.IdPengembalian);
                form.isDetailMode = true;
                form.Opacity = 0;
                form.Show();

                Button btnCetak = form.Controls.Find("btnCetakBukti", true).FirstOrDefault() as Button;
                if (btnCetak != null) btnCetak.PerformClick();

                form.Close();
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            MainForm parentForm = this.ParentForm as MainForm;
            DashboardUC dashboardUC = new DashboardUC();
            if (parentForm != null) parentForm.ChangeView(dashboardUC);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}