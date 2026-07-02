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
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class TransaksiPeminjamanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        public Peminjaman selectedPeminjaman = null;
        private BindingList<DetailItemViewModel> listKeranjang = new BindingList<DetailItemViewModel>();
        public bool isDetailMode = false;

        public class DetailItemViewModel
        {
            public int KodeBarangGUID { get; set; }
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string Kondisi { get; set; }
        }

        public TransaksiPeminjamanForm()
        {
            InitializeComponent();
            dg.DataSource = listKeranjang;
        }

        private void TransaksiPeminjamanForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Peminjaman");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            SetupGrid();

            var listPengguna = db.Pengguna.ToList();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            foreach (var b in listPengguna)
            {
                string formattedText = $"{b.Username}";
                collection.Add(formattedText);
            }

            txtNamaPeminjam.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNamaPeminjam.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNamaPeminjam.AutoCompleteCustomSource = collection;

            if (selectedPeminjaman == null)
            {
                txtNoPeminjaman.Text = GenerateNoPinjam();
                dtpTglPinjam.Value = DateTime.Now;
                txtLamaPinjam.Text = "1";
            }
            else
            {
                LoadExistingData();
            }
        }

        private void SetupGrid()
        {
            if (dg.Columns["KodeBarangGUID"] != null) dg.Columns["KodeBarangGUID"].Visible = false;
            if (dg.Columns["KodeInventaris"] != null) dg.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
            if (dg.Columns["NamaBarang"] != null) dg.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dg.Columns["Kondisi"] != null) dg.Columns["Kondisi"].HeaderText = "Kondisi";

            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dg.ReadOnly = true;
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private string GenerateNoPinjam()
        {
            string datePrefix = DateTime.Now.ToString("yyyyMMdd");
            var lastData = db.Peminjaman
                .Where(p => p.NomorPeminjaman.StartsWith("PMJ-" + datePrefix))
                .OrderByDescending(p => p.NomorPeminjaman)
                .FirstOrDefault();

            if (lastData == null)
            {
                return $"PMJ-{datePrefix}-001";
            }
            else
            {
                string lastNumber = lastData.NomorPeminjaman.Substring(lastData.NomorPeminjaman.Length - 3);
                int nextNumber = int.Parse(lastNumber) + 1;
                return $"PMJ-{datePrefix}-{nextNumber:D3}";
            }
        }

        private void LoadExistingData()
        {
            txtNoPeminjaman.Text = selectedPeminjaman.NomorPeminjaman;
            dtpTglPinjam.Value = selectedPeminjaman.TanggalPinjam;
            txtLamaPinjam.Text = selectedPeminjaman.LamaPinjamHari.ToString();
            txtNamaPeminjam.Text = selectedPeminjaman.NamaPeminjam;
            txtNoTelepon.Text = selectedPeminjaman.NomorTelepon;
            txtKeterangan.Text = selectedPeminjaman.Keterangan;

            var details = db.DetailPeminjaman
                .Where(d => d.NomorPeminjaman == selectedPeminjaman.NomorPeminjaman)
                .AsNoTracking()
                .ToList();

            listKeranjang.Clear();
            foreach (var d in details)
            {
                var aset = db.Aset.AsNoTracking().FirstOrDefault(a => a.KodeBarang == d.KodeBarang);
                string namaBrg = "Tidak Diketahui";
                string namaKondisi = "-";
                string kodeInv = "Tidak Diketahui";

                if (aset != null)
                {
                    kodeInv = aset.KodeInventaris;

                    var master = db.MasterBarang.AsNoTracking().FirstOrDefault(m => m.IdMasterBarang == aset.IdMasterBarang);
                    if (master != null) namaBrg = master.NamaBarang;

                    if (aset.IdKondisi.HasValue)
                    {
                        var kondisi = db.Kondisi.AsNoTracking().FirstOrDefault(k => k.IdKondisi == aset.IdKondisi.Value);
                        if (kondisi != null) namaKondisi = kondisi.NamaKondisi;
                    }
                }

                listKeranjang.Add(new DetailItemViewModel
                {
                    KodeBarangGUID = d.KodeBarang,
                    KodeInventaris = kodeInv,
                    NamaBarang = namaBrg,
                    Kondisi = namaKondisi
                });
            }

            if (isDetailMode || (selectedPeminjaman.StatusPeminjaman != "Sedang Dipinjam" && selectedPeminjaman.StatusPeminjaman != "Pinjam"))
            {
                txtNamaPeminjam.ReadOnly = true;
                txtNoTelepon.ReadOnly = true;
                txtLamaPinjam.ReadOnly = true;
                txtKeterangan.ReadOnly = true;
                txtKodeBarang.ReadOnly = true;
                dtpTglPinjam.Enabled = false;

                btnSimpan.Enabled = false;
                btnOpen.Enabled = false;

                if (!isDetailMode) MessageBox.Show("Status sudah dikembalikan. Data tidak bisa diubah.");
            }
            else
            {
                MessageBox.Show("Mode Ubah: Anda dapat menambah barang baru, atau klik ganda (Double-Click) pada baris tabel untuk membatalkan/menghapus barang dari daftar pinjaman.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string kodeInput = txtKodeBarang.Text.Trim();
            if (string.IsNullOrEmpty(kodeInput))
            {
                MessageBox.Show("Masukkan Kode Inventaris terlebih dahulu.");
                return;
            }

            int.TryParse(kodeInput, out int idBarangAnka);

            var aset = db.Aset.FirstOrDefault(a => a.KodeInventaris == kodeInput ||
                                                   a.NoSeri == kodeInput ||
                                                   a.KodeBarang2 == kodeInput ||
                                                   (idBarangAnka > 0 && a.KodeBarang == idBarangAnka));

            if (aset == null)
            {
                MessageBox.Show("Barang dengan kode tersebut tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (aset.Status == "Dipinjam" || aset.Status == "Sedang Dipinjam")
            {
                MessageBox.Show("Gagal! Barang tersebut saat ini sedang DIPINJAM oleh orang lain.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (aset.Status != "Di Gudang" && aset.Status != "Tersedia")
            {
                MessageBox.Show($"Barang tidak bisa dipinjam karena statusnya saat ini: {aset.Status}", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listKeranjang.Any(k => k.KodeBarangGUID == aset.KodeBarang))
            {
                MessageBox.Show("Barang ini sudah ada di dalam daftar pinjaman di bawah.");
                return;
            }

            string namaBrg = "Tidak Diketahui";
            var master = db.MasterBarang.FirstOrDefault(m => m.IdMasterBarang == aset.IdMasterBarang);
            if (master != null) namaBrg = master.NamaBarang;

            string namaKondisi = "-";
            if (aset.IdKondisi.HasValue)
            {
                var kondisi = db.Kondisi.FirstOrDefault(k => k.IdKondisi == aset.IdKondisi.Value);
                if (kondisi != null) namaKondisi = kondisi.NamaKondisi;
            }

            listKeranjang.Add(new DetailItemViewModel
            {
                KodeBarangGUID = aset.KodeBarang,
                KodeInventaris = aset.KodeInventaris,
                NamaBarang = namaBrg,
                Kondisi = namaKondisi
            });

            txtKodeBarang.Clear();
            txtKodeBarang.Focus();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaPeminjam.Text) || listKeranjang.Count == 0)
            {
                MessageBox.Show("Pastikan Nama Peminjam terisi dan keranjang barang tidak kosong.");
                return;
            }

            if (!db.Pengguna.Any(p => p.Username == txtNamaPeminjam.Text))
            {
                MessageBox.Show("Nama Peminjam tidak valid. Pastikan nama tersebut sudah terdaftar di sistem.");
                return;
            }

            int lamaPinjam = 1;
            int.TryParse(txtLamaPinjam.Text, out lamaPinjam);

            try
            {
                using (var saveDb = new AppDbContext())
                {
                    if (selectedPeminjaman == null)
                    {
                        var peminjamanBaru = new Peminjaman
                        {
                            NomorPeminjaman = txtNoPeminjaman.Text,
                            TanggalPinjam = dtpTglPinjam.Value,
                            NamaPeminjam = txtNamaPeminjam.Text,
                            NomorTelepon = txtNoTelepon.Text,
                            LamaPinjamHari = lamaPinjam,
                            Keterangan = txtKeterangan.Text,
                            StatusPeminjaman = "Sedang Dipinjam"
                        };

                        saveDb.Peminjaman.Add(peminjamanBaru);
                        saveDb.SaveChanges();

                        foreach (var item in listKeranjang)
                        {
                            saveDb.DetailPeminjaman.Add(new DetailPeminjaman
                            {
                                NomorPeminjaman = peminjamanBaru.NomorPeminjaman,
                                KodeBarang = item.KodeBarangGUID
                            });

                            var asetDb = saveDb.Aset.Find(item.KodeBarangGUID);
                            if (asetDb != null) asetDb.Status = "Dipinjam";
                        }
                        saveDb.SaveChanges();
                    }
                    else
                    {
                        var existingPeminjaman = saveDb.Peminjaman.Find(selectedPeminjaman.NomorPeminjaman);
                        if (existingPeminjaman != null)
                        {
                            existingPeminjaman.NamaPeminjam = txtNamaPeminjam.Text;
                            existingPeminjaman.NomorTelepon = txtNoTelepon.Text;
                            existingPeminjaman.LamaPinjamHari = lamaPinjam;
                            existingPeminjaman.Keterangan = txtKeterangan.Text;

                            var oldDetails = saveDb.DetailPeminjaman
                                .Where(d => d.NomorPeminjaman == existingPeminjaman.NomorPeminjaman)
                                .ToList();

                            foreach (var old in oldDetails)
                            {
                                var asetLama = saveDb.Aset.Find(old.KodeBarang);
                                if (asetLama != null) asetLama.Status = "Di Gudang";
                            }
                            saveDb.DetailPeminjaman.RemoveRange(oldDetails);
                            saveDb.SaveChanges();

                            foreach (var item in listKeranjang)
                            {
                                saveDb.DetailPeminjaman.Add(new DetailPeminjaman
                                {
                                    NomorPeminjaman = existingPeminjaman.NomorPeminjaman,
                                    KodeBarang = item.KodeBarangGUID
                                });

                                var asetBaru = saveDb.Aset.Find(item.KodeBarangGUID);
                                if (asetBaru != null) asetBaru.Status = "Dipinjam";
                            }
                            saveDb.SaveChanges();
                        }
                    }
                }

                MessageBox.Show("Transaksi peminjaman berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCetakBukti_Click(null, null);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCetakBukti_Click(object sender, EventArgs e)
        {
            if (listKeranjang.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak.");
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(CetakHalamanBukti);

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = pd,
                Width = 800,
                Height = 600,
                ShowIcon = false
            };
            preview.ShowDialog();
        }

        private void CetakHalamanBukti(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontJudul = new Font("Arial", 16, FontStyle.Bold);
            Font fontReg = new Font("Arial", 11, FontStyle.Regular);
            Font fontBold = new Font("Arial", 11, FontStyle.Bold);
            int y = 50;

            g.DrawString("BUKTI PEMINJAMAN ASET", fontJudul, Brushes.Black, new Point(250, y));
            y += 40;

            g.DrawString("SMK NEGERI 1 BANGIL", fontBold, Brushes.Black, new Point(300, y));
            y += 50;

            g.DrawString($"No. Pinjam : {txtNoPeminjaman.Text}", fontReg, Brushes.Black, new Point(50, y));
            g.DrawString($"Tgl Pinjam : {dtpTglPinjam.Value.ToString("dd MMM yyyy")}", fontReg, Brushes.Black, new Point(500, y));
            y += 30;
            g.DrawString($"Peminjam   : {txtNamaPeminjam.Text}", fontReg, Brushes.Black, new Point(50, y));
            g.DrawString($"Lama (Hari): {txtLamaPinjam.Text} Hari", fontReg, Brushes.Black, new Point(500, y));
            y += 40;

            g.DrawLine(Pens.Black, 50, y, 750, y); y += 10;
            g.DrawString("Kode Inventaris", fontBold, Brushes.Black, new Point(50, y));
            g.DrawString("Nama Barang", fontBold, Brushes.Black, new Point(250, y));
            g.DrawString("Kondisi", fontBold, Brushes.Black, new Point(600, y));
            y += 25;
            g.DrawLine(Pens.Black, 50, y, 750, y); y += 15;

            foreach (var item in listKeranjang)
            {
                g.DrawString(item.KodeInventaris, fontReg, Brushes.Black, new Point(50, y));

                string namaBrg = item.NamaBarang.Length > 35 ? item.NamaBarang.Substring(0, 35) + "..." : item.NamaBarang;
                g.DrawString(namaBrg, fontReg, Brushes.Black, new Point(250, y));

                g.DrawString(item.Kondisi, fontReg, Brushes.Black, new Point(600, y));
                y += 25;
            }

            y += 10;
            g.DrawLine(Pens.Black, 50, y, 750, y); y += 50;

            g.DrawString("Mengetahui,", fontReg, Brushes.Black, new Point(100, y));
            g.DrawString("Peminjam,", fontReg, Brushes.Black, new Point(600, y));
            y += 80;
            g.DrawString("( Admin/Petugas )", fontReg, Brushes.Black, new Point(80, y));
            g.DrawString($"( {txtNamaPeminjam.Text} )", fontReg, Brushes.Black, new Point(580, y));
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtKodeBarang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnOpen_Click(sender, e);
            }
        }

        private void dg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isDetailMode && (selectedPeminjaman == null || selectedPeminjaman.StatusPeminjaman == "Sedang Dipinjam" || selectedPeminjaman.StatusPeminjaman == "Pinjam"))
            {
                if (e.RowIndex >= 0)
                {
                    if (MessageBox.Show("Hapus barang ini dari daftar pinjaman?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        listKeranjang.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtLamaPinjam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtNoTelepon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }
    }
}