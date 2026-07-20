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
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory
{
    public partial class NonAktifBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        private string currentKodeInventaris = "";
        private string currentStatusAset = "";

        public class AsetViewModel
        {
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string Status { get; set; }
        }

        public class NonAktifViewModel
        {
            public int IdBarangNonAktif { get; set; }
            public string NamaBarang { get; set; }
            public DateTime Tanggal { get; set; }
            public string Penyebab { get; set; }
            public int Jumlah { get; set; }
            public string Keterangan { get; set; }
            public BarangNonAktif ObjekAsli { get; set; }
        }

        public NonAktifBarangForm()
        {
            InitializeComponent();
        }

        private void NonAktifBarangForm_Load(object sender, EventArgs e)
        {
            LoadComboPenyebab();
            SetupGrids();
            SetMode("View");
        }

        private void SetupGrids()
        {
            dgAset.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgAset.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgAset.ReadOnly = true;
            dgAset.RowHeadersVisible = false;

            dgNonAktif.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgNonAktif.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgNonAktif.ReadOnly = true;
            dgNonAktif.RowHeadersVisible = false;

            txtJumlahBarang.Text = "1";
            txtJumlahNonAktif.Text = "1";

            btnCariAset_Click(null, null);
        }

        private void LoadComboPenyebab()
        {
            var listStatus = db.StatusBarang.ToList();
            cmbPenyebab.DataSource = listStatus;
            cmbPenyebab.DisplayMember = "NamaStatus";
            cmbPenyebab.ValueMember = "IdStatus";
            cmbPenyebab.SelectedIndex = -1;
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                dtpTanggal.Enabled = false;
                cmbPenyebab.Enabled = false;
                txtKeterangan.ReadOnly = true;

                bool bisaDitambah = !string.IsNullOrEmpty(currentKodeInventaris) && currentStatusAset != "Non Aktif";

                btnTambah.Enabled = bisaDitambah;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else 
            {
                dtpTanggal.Enabled = true;
                cmbPenyebab.Enabled = true;
                txtKeterangan.ReadOnly = false;

                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private void btnCariAset_Click(object sender, EventArgs e)
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            string cari = txtCariAset.Text.ToLower().Trim();

            var dictBarang = db.MasterBarang.ToDictionary(b => b.IdMasterBarang, b => b.NamaBarang);

            var rawAset = db.Aset.AsNoTracking()
                .Where(a => a.KodeInventaris != null && a.KodeInventaris != "")
                .ToList();

            var dataGrid = rawAset.Select(a => {
                return new AsetViewModel
                {
                    KodeInventaris = a.KodeInventaris,
                    NamaBarang = dictBarang.ContainsKey(a.IdMasterBarang) ? dictBarang[a.IdMasterBarang] : "N/A",
                    Status = a.Status
                };
            })
            .Where(a => a.KodeInventaris.ToLower().Contains(cari) ||
                        a.NamaBarang.ToLower().Contains(cari))
            .Take(50) 
            .ToList();

            dgAset.DataSource = dataGrid;

            if (dgAset.Columns["KodeInventaris"] != null) dgAset.Columns["KodeInventaris"].HeaderText = "Kode Inv.";
            if (dgAset.Columns["NamaBarang"] != null) dgAset.Columns["NamaBarang"].HeaderText = "Barang";
            if (dgAset.Columns["Status"] != null) dgAset.Columns["Status"].HeaderText = "Status";
        }

        private void txtCariAset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCariAset_Click(sender, e);
            }
        }

        private void dgAset_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSimpan.Enabled) return; 

            if (e.RowIndex >= 0 && dgAset.Rows[e.RowIndex].DataBoundItem is AsetViewModel vm)
            {
                txtKodeInventaris.Text = vm.KodeInventaris;
                txtNamaBarang.Text = vm.NamaBarang;

                currentKodeInventaris = vm.KodeInventaris;
                currentStatusAset = vm.Status;

                loadDgvNonAktif();
                SetMode("View");

                if (currentStatusAset == "NonAktif")
                {
                    MessageBox.Show("Aset ini sudah berstatus Non-Aktif. Anda tidak dapat menonaktifkannya lagi.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void loadDgvNonAktif()
        {
            if (string.IsNullOrEmpty(currentKodeInventaris)) return;

            var dictStatus = db.StatusBarang.ToDictionary(s => s.IdStatus, s => s.NamaStatus);

            var listNonAktif = db.BarangNonAktif.AsNoTracking()
                .Where(n => n.KodeInventaris == currentKodeInventaris)
                .OrderByDescending(n => n.Tanggal)
                .ToList();

            string namaBarangTerpilih = txtNamaBarang.Text;

            var dataGrid = listNonAktif.Select(n => {
                int? idStat = n.IdStatus; 

                return new NonAktifViewModel
                {
                    IdBarangNonAktif = n.IdBarangNonAktif,
                    NamaBarang = namaBarangTerpilih,
                    Tanggal = n.Tanggal,
                    Penyebab = (idStat.HasValue && dictStatus.ContainsKey(idStat.Value)) ? dictStatus[idStat.Value] : "-",
                    Jumlah = n.JumlahNonaktif ?? 1,
                    Keterangan = n.Keterangan,
                    ObjekAsli = n
                };
            }).ToList();

            dgNonAktif.DataSource = new SortableBindingList<NonAktifViewModel>(dataGrid);

            if (dgNonAktif.Columns["IdBarangNonAktif"] != null) dgNonAktif.Columns["IdBarangNonAktif"].Visible = false;
            if (dgNonAktif.Columns["ObjekAsli"] != null) dgNonAktif.Columns["ObjekAsli"].Visible = false;
            if (dgNonAktif.Columns["NamaBarang"] != null) dgNonAktif.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dgNonAktif.Columns["Tanggal"] != null) dgNonAktif.Columns["Tanggal"].HeaderText = "Tanggal";
            if (dgNonAktif.Columns["Penyebab"] != null) dgNonAktif.Columns["Penyebab"].HeaderText = "Penyebab";
            if (dgNonAktif.Columns["Jumlah"] != null) dgNonAktif.Columns["Jumlah"].HeaderText = "Jumlah";
            if (dgNonAktif.Columns["Keterangan"] != null) dgNonAktif.Columns["Keterangan"].HeaderText = "Keterangan";
        }

        private void dgNonAktif_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSimpan.Enabled) return;

            if (e.RowIndex >= 0 && dgNonAktif.Rows[e.RowIndex].DataBoundItem is NonAktifViewModel vm)
            {
                var nonAktif = db.BarangNonAktif.Find(vm.IdBarangNonAktif);
                if (nonAktif != null)
                {
                    bindingSource1.DataSource = nonAktif;
                    dtpTanggal.Value = nonAktif.Tanggal;
                    cmbPenyebab.SelectedValue = nonAktif.IdStatus ?? -1;
                    txtKeterangan.Text = nonAktif.Keterangan;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentKodeInventaris))
            {
                MessageBox.Show("Pilih aset dari tabel kiri terlebih dahulu.");
                return;
            }

            SetMode("Insert");
            bindingSource1.AddNew();

            dtpTanggal.Value = DateTime.Now;
            cmbPenyebab.SelectedIndex = -1;
            txtKeterangan.Clear();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is BarangNonAktif)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data riwayat yang ingin diubah dari tabel bawah.");
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbPenyebab.SelectedIndex == -1)
            {
                MessageBox.Show("Penyebab (Status) harus dipilih.");
                return;
            }

            if (bindingSource1.Current is BarangNonAktif k)
            {
                bindingSource1.EndEdit();

                try
                {
                    int idPenyebab = (int)cmbPenyebab.SelectedValue;

                    if (k.IdBarangNonAktif == 0)
                    {
                        var baru = new BarangNonAktif
                        {
                            KodeInventaris = currentKodeInventaris,
                            Tanggal = dtpTanggal.Value,
                            IdStatus = idPenyebab,
                            JumlahNonaktif = 1, 
                            Keterangan = txtKeterangan.Text
                        };
                        db.BarangNonAktif.Add(baru);
                    }
                    else 
                    {
                        k.Tanggal = dtpTanggal.Value;
                        k.IdStatus = idPenyebab;
                        k.Keterangan = txtKeterangan.Text;
                    }

                    var asetUtama = db.Aset.FirstOrDefault(a => a.KodeInventaris == currentKodeInventaris);
                    if (asetUtama != null)
                    {
                        asetUtama.Status = "NonAktif";
                        currentStatusAset = "NonAktif"; 
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan! Status aset telah diperbarui menjadi Non Aktif.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loadDgvNonAktif();
                    btnCariAset_Click(null, null);
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is BarangNonAktif k && k.IdBarangNonAktif != 0)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus data non-aktif ini?\n\nStatus aset akan dikembalikan menjadi 'Di Gudang'.", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        db.BarangNonAktif.Remove(k);

                        var asetUtama = db.Aset.FirstOrDefault(a => a.KodeInventaris == currentKodeInventaris);
                        if (asetUtama != null)
                        {
                            asetUtama.Status = "Di Gudang";
                            currentStatusAset = "Di Gudang";
                        }

                        db.SaveChanges();

                        MessageBox.Show("Data non-aktif berhasil dihapus. Status aset telah dipulihkan.");
                        loadDgvNonAktif();
                        bindingSource1.AddNew();
                        btnCariAset_Click(null, null);

                        dtpTanggal.Value = DateTime.Now;
                        cmbPenyebab.SelectedIndex = -1;
                        txtKeterangan.Clear();
                    }
                    catch (Exception ex)
                    {
                        db.Entry(k).Reload();
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem saat menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang valid untuk dihapus.");
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            bindingSource1.AddNew();
            loadDgvNonAktif();

            dtpTanggal.Value = DateTime.Now;
            cmbPenyebab.SelectedIndex = -1;
            txtKeterangan.Clear();

            SetMode("View");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}