using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
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
    public partial class PeminjamanBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();

        public class PeminjamanViewModel
        {
            public string NomorPeminjaman { get; set; }
            public DateTime TanggalPinjam { get; set; }
            public string NamaPeminjam { get; set; }
            public int LamaPinjamHari { get; set; }
            public string StatusPeminjaman { get; set; }
            public Peminjaman ObjekAsli { get; set; }
        }

        public PeminjamanBarangUC()
        {
            InitializeComponent();
        }

        private void PeminjamanBarangUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Data Peminjaman");

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
            var cari = txtCari.Text.ToLower().Trim();
            var tglMulai = dtpMulai.Value.Date;
            var tglSampai = dtpSampai.Value.Date.AddDays(1).AddTicks(-1); 

            var dataRaw = db.Peminjaman.AsNoTracking()
                .Where(p => p.TanggalPinjam >= tglMulai && p.TanggalPinjam <= tglSampai)
                .Where(p => p.NomorPeminjaman.ToLower().Contains(cari) || p.NamaPeminjam.ToLower().Contains(cari))
                .OrderByDescending(p => p.TanggalPinjam)
                .ToList();

            var dataTampil = dataRaw.Select(p => new PeminjamanViewModel
            {
                NomorPeminjaman = p.NomorPeminjaman,
                TanggalPinjam = p.TanggalPinjam,
                NamaPeminjam = p.NamaPeminjam,
                LamaPinjamHari = p.LamaPinjamHari,
                StatusPeminjaman = p.StatusPeminjaman,
                ObjekAsli = p
            }).ToList();

            dg.DataSource = new SortableBindingList<PeminjamanViewModel>(dataTampil);
            lblTotal.Text = $"Total Record : {dataTampil.Count}";

            if (dg.Columns["NomorPeminjaman"] != null) dg.Columns["NomorPeminjaman"].HeaderText = "No. Peminjaman";
            if (dg.Columns["TanggalPinjam"] != null) dg.Columns["TanggalPinjam"].HeaderText = "Tanggal Pinjam";
            if (dg.Columns["NamaPeminjam"] != null) dg.Columns["NamaPeminjam"].HeaderText = "Nama Peminjam";
            if (dg.Columns["LamaPinjamHari"] != null) dg.Columns["LamaPinjamHari"].HeaderText = "Lama (Hari)";
            if (dg.Columns["StatusPeminjaman"] != null) dg.Columns["StatusPeminjaman"].HeaderText = "Status";
            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            TransaksiPeminjamanForm form = new TransaksiPeminjamanForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PeminjamanViewModel vm)
            {
                var pinjam = db.Peminjaman.Find(vm.NomorPeminjaman);
                TransaksiPeminjamanForm form = new TransaksiPeminjamanForm();
                form.selectedPeminjaman = pinjam;
                if (form.ShowDialog() == DialogResult.OK) loadData();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah terlebih dahulu.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PeminjamanViewModel vm)
            {
                if (MessageBox.Show($"Yakin ingin menghapus transaksi {vm.NomorPeminjaman}?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        var pinjam = db.Peminjaman.Include(p => p.DetailPeminjaman).FirstOrDefault(p => p.NomorPeminjaman == vm.NomorPeminjaman);

                        foreach (var d in pinjam.DetailPeminjaman)
                        {
                            var aset = db.Aset.Find(d.KodeBarang);
                            if (aset != null) aset.Status = "Di Gudang";
                        }

                        db.Peminjaman.Remove(pinjam);
                        db.SaveChanges();
                        MessageBox.Show("Data peminjaman berhasil dihapus dan aset dikembalikan ke gudang.");
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus: " + ex.Message);
                    }
                }
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PeminjamanViewModel vm)
            {
                var pinjam = db.Peminjaman.Find(vm.NomorPeminjaman);
                TransaksiPeminjamanForm form = new TransaksiPeminjamanForm();
                form.selectedPeminjaman = pinjam;
                form.isDetailMode = true; 
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin dilihat detailnya.");
            }
        }

        private void btnCetakBukti_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PeminjamanViewModel vm)
            {
                var pinjam = db.Peminjaman.Find(vm.NomorPeminjaman);

                TransaksiPeminjamanForm form = new TransaksiPeminjamanForm();
                form.selectedPeminjaman = pinjam;
                form.isDetailMode = true;

                form.Opacity = 0;
                form.Show();

                Button btnCetak = form.Controls.Find("btnCetakBukti", true).FirstOrDefault() as Button;
                if (btnCetak != null) btnCetak.PerformClick();

                form.Close();
            }
            else
            {
                MessageBox.Show("Pilih data peminjaman yang ingin dicetak.");
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            MainForm parentForm = this.ParentForm as MainForm;
            DashboardUC dashboardUC = new DashboardUC();
            if (parentForm != null) parentForm.ChangeView(dashboardUC);
        }
    }
}