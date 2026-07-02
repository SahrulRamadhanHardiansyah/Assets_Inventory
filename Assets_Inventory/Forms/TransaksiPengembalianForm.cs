using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class TransaksiPengembalianForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        public Pengembalian selectedPengembalian = null;
        public bool isDetailMode = false;

        private BindingList<ItemKembaliViewModel> listDipinjam = new BindingList<ItemKembaliViewModel>();
        private BindingList<ItemKembaliViewModel> listDikembalikan = new BindingList<ItemKembaliViewModel>();

        public class ItemKembaliViewModel
        {
            public int KodeBarangGUID { get; set; }
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string Status { get; set; }
        }

        public class PeminjamanAktifViewModel
        {
            public string NomorPeminjaman { get; set; }
            public string NamaPeminjam { get; set; }
            public DateTime TanggalPinjam { get; set; }
            public string NoTelepon { get; set; }
        }

        public TransaksiPengembalianForm()
        {
            InitializeComponent();
            dgDipinjam.DataSource = listDipinjam;
            dgDikembalikan.DataSource = listDikembalikan;
        }

        private void TransaksiPengembalianForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Pengembalian");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtNoPeminjaman.ReadOnly = true;
            txtNamaPeminjam.ReadOnly = true;
            txtNoTelepon.ReadOnly = true;

            SetupGrids();

            if (selectedPengembalian != null)
            {
                LoadDataExisting();
            }
            else
            {
                LoadDaftarPeminjamanAktif();
            }
        }

        private void SetupGrids()
        {
            if (dgDipinjam.Columns["KodeBarangGUID"] != null) dgDipinjam.Columns["KodeBarangGUID"].Visible = false;
            if (dgDipinjam.Columns["KodeInventaris"] != null) dgDipinjam.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
            if (dgDipinjam.Columns["NamaBarang"] != null) dgDipinjam.Columns["NamaBarang"].HeaderText = "Nama Barang";
            dgDipinjam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgDipinjam.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgDipinjam.ReadOnly = true;

            if (dgDikembalikan.Columns["KodeBarangGUID"] != null) dgDikembalikan.Columns["KodeBarangGUID"].Visible = false;
            if (dgDikembalikan.Columns["KodeInventaris"] != null) dgDikembalikan.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
            if (dgDikembalikan.Columns["NamaBarang"] != null) dgDikembalikan.Columns["NamaBarang"].HeaderText = "Nama Barang";
            dgDikembalikan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgDikembalikan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgDikembalikan.ReadOnly = true;
        }

        private void LoadDaftarPeminjamanAktif()
        {
            var pinjamanAktif = db.Peminjaman.AsNoTracking()
                .Where(p => p.StatusPeminjaman == "Sedang Dipinjam" || p.StatusPeminjaman == "Dipinjam")
                .OrderByDescending(p => p.TanggalPinjam)
                .Select(p => new PeminjamanAktifViewModel
                {
                    NomorPeminjaman = p.NomorPeminjaman,
                    NamaPeminjam = p.NamaPeminjam,
                    TanggalPinjam = p.TanggalPinjam,
                    NoTelepon = p.NomorTelepon
                }).ToList();

            dgPeminjaman.DataSource = new SortableBindingList<PeminjamanAktifViewModel>(pinjamanAktif);

            if (dgPeminjaman.Columns["NomorPeminjaman"] != null) dgPeminjaman.Columns["NomorPeminjaman"].HeaderText = "No. Transaksi";
            if (dgPeminjaman.Columns["NamaPeminjam"] != null) dgPeminjaman.Columns["NamaPeminjam"].HeaderText = "Nama Peminjam";
            if (dgPeminjaman.Columns["TanggalPinjam"] != null) dgPeminjaman.Columns["TanggalPinjam"].HeaderText = "Tgl Pinjam";
            if (dgPeminjaman.Columns["NoTelepon"] != null) dgPeminjaman.Columns["NoTelepon"].Visible = false; // Disembunyikan, hanya ditarik untuk TextBox

            dgPeminjaman.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPeminjaman.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPeminjaman.ReadOnly = true;

        }

        private void dgPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isDetailMode) return;

            if (e.RowIndex >= 0 && dgPeminjaman.Rows[e.RowIndex].DataBoundItem is PeminjamanAktifViewModel vm)
            {
                txtNoPeminjaman.Text = vm.NomorPeminjaman;
                txtNamaPeminjam.Text = vm.NamaPeminjam;
                txtNoTelepon.Text = vm.NoTelepon;

                LoadDetailBarangKeKeranjang(vm.NomorPeminjaman);
            }
        }

        private void LoadDetailBarangKeKeranjang(string noPinjam)
        {
            listDipinjam.Clear();
            listDikembalikan.Clear();

            var details = db.DetailPeminjaman.Where(d => d.NomorPeminjaman == noPinjam).ToList();

            foreach (var d in details)
            {
                var aset = db.Aset.FirstOrDefault(a => a.KodeBarang == d.KodeBarang);
                string namaBrg = "Tidak Diketahui";
                string statusBrg = "Tidak Diketahui";
                string kodeInv = "-";

                if (aset != null)
                {
                    kodeInv = aset.KodeInventaris;
                    statusBrg = aset.Status;

                    var master = db.MasterBarang.FirstOrDefault(m => m.IdMasterBarang == aset.IdMasterBarang);
                    if (master != null) namaBrg = master.NamaBarang;
                }

                var itemVM = new ItemKembaliViewModel
                {
                    KodeBarangGUID = d.KodeBarang,
                    KodeInventaris = kodeInv,
                    NamaBarang = namaBrg,
                    Status = statusBrg
                };

                if (itemVM.Status == "Dipinjam" || itemVM.Status == "Pinjam")
                {
                    listDipinjam.Add(itemVM);
                }
                else
                {
                    listDikembalikan.Add(itemVM);
                }
            }

            if (!isDetailMode && listDipinjam.Count == 0)
            {
                MessageBox.Show("Semua barang pada transaksi peminjaman ini sudah lunas dikembalikan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSimpan.Enabled = false;
            }
            else
            {
                btnSimpan.Enabled = true;
            }
        }

        private void LoadDataExisting()
        {
            txtNoPeminjaman.Text = selectedPengembalian.NomorPeminjaman;
            dtpTglKembali.Value = selectedPengembalian.TanggalKembali;

            var pinjam = db.Peminjaman.FirstOrDefault(p => p.NomorPeminjaman == selectedPengembalian.NomorPeminjaman);
            if (pinjam != null)
            {
                txtNamaPeminjam.Text = pinjam.NamaPeminjam;
                txtNoTelepon.Text = pinjam.NomorTelepon;
            }

            if (dgPeminjaman != null) dgPeminjaman.Enabled = false;

            LoadDetailBarangKeKeranjang(selectedPengembalian.NomorPeminjaman);

            if (isDetailMode)
            {
                btnSimpan.Enabled = false;
                dtpTglKembali.Enabled = false;
                MessageBox.Show("Mode Detail: Melihat barang yang sudah dikembalikan dan yang masih dipinjam (jika ada).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Mode Ubah: Anda dapat mengubah tanggal kembali, atau mengoreksi barang dengan meng-klik ganda (Double-Click) pada tabel.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgDipinjam_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isDetailMode) return;
            if (e.RowIndex >= 0)
            {
                var item = listDipinjam[e.RowIndex];
                listDikembalikan.Add(item);
                listDipinjam.RemoveAt(e.RowIndex);
            }
        }

        private void dgDikembalikan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isDetailMode) return;
            if (e.RowIndex >= 0)
            {
                var item = listDikembalikan[e.RowIndex];
                listDipinjam.Add(item);
                listDikembalikan.RemoveAt(e.RowIndex);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNoPeminjaman.Text))
            {
                MessageBox.Show("Pilih transaksi peminjaman terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listDikembalikan.Count == 0)
            {
                MessageBox.Show("Pindahkan minimal 1 barang ke tabel bawah untuk dikembalikan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (selectedPengembalian == null)
                {
                    var kembali = new Pengembalian
                    {
                        NomorPeminjaman = txtNoPeminjaman.Text,
                        TanggalKembali = dtpTglKembali.Value
                    };
                    db.Pengembalian.Add(kembali);
                }
                else
                {
                    var extKembali = db.Pengembalian.Find(selectedPengembalian.IdPengembalian);
                    if (extKembali != null) extKembali.TanggalKembali = dtpTglKembali.Value;
                }

                foreach (var item in listDikembalikan)
                {
                    var aset = db.Aset.Find(item.KodeBarangGUID);
                    if (aset != null) aset.Status = "Di Gudang";
                }

                foreach (var item in listDipinjam)
                {
                    var aset = db.Aset.Find(item.KodeBarangGUID);
                    if (aset != null) aset.Status = "Dipinjam";
                }

                var pinjam = db.Peminjaman.Find(txtNoPeminjaman.Text);
                if (pinjam != null)
                {
                    pinjam.StatusPeminjaman = listDipinjam.Count == 0 ? "Dikembalikan" : "Sedang Dipinjam";
                }

                db.SaveChanges();

                MessageBox.Show("Data pengembalian berhasil diproses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMsg += "\n\nDetail:\n" + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                    {
                        errorMsg += "\n" + ex.InnerException.InnerException.Message;
                    }
                }

                MessageBox.Show("Terjadi kesalahan sistem:\n\n" + errorMsg, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCetakBukti_Click(object sender, EventArgs e)
        {
            if (listDikembalikan.Count == 0 && listDipinjam.Count == 0) return;

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(CetakStruk);

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = pd,
                Width = 800,
                Height = 600,
                ShowIcon = false
            };
            preview.ShowDialog();
        }

        private void CetakStruk(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fJudul = new Font("Arial", 16, FontStyle.Bold);
            Font fReg = new Font("Arial", 11, FontStyle.Regular);
            int y = 50;

            g.DrawString("BUKTI PENGEMBALIAN ASET", fJudul, Brushes.Black, new Point(230, y)); y += 50;
            g.DrawString($"No. Pinjam : {txtNoPeminjaman.Text}", fReg, Brushes.Black, new Point(50, y));
            g.DrawString($"Tgl Kembali: {dtpTglKembali.Value.ToString("dd MMM yyyy")}", fReg, Brushes.Black, new Point(500, y)); y += 30;
            g.DrawString($"Peminjam   : {txtNamaPeminjam.Text}", fReg, Brushes.Black, new Point(50, y)); y += 40;

            g.DrawLine(Pens.Black, 50, y, 750, y); y += 10;
            g.DrawString("Daftar Barang Dikembalikan", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(50, y)); y += 25;
            g.DrawLine(Pens.Black, 50, y, 750, y); y += 15;

            foreach (var item in listDikembalikan)
            {
                g.DrawString(item.KodeInventaris, fReg, Brushes.Black, new Point(50, y));
                g.DrawString(item.NamaBarang, fReg, Brushes.Black, new Point(250, y));
                y += 25;
            }

            y += 20;
            g.DrawString("Penerima (Admin),", fReg, Brushes.Black, new Point(550, y)); y += 70;
            g.DrawString("( ........................ )", fReg, Brushes.Black, new Point(550, y));
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}