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
    public partial class NonAktifBangunanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        private int currentKodeBangunan = 0;
        private string currentStatusBangunan = "";
        public class BangunanViewModel
        {
            public int KodeBangunan { get; set; }
            public string NamaBangunan { get; set; }
            public int LuasBangunan { get; set; }
            public string Status { get; set; }
        }

        public class NonAktifBangunanViewModel
        {
            public int IdBangunanNonAktif { get; set; }
            public DateTime Tanggal { get; set; }
            public string Penyebab { get; set; }
            public string Keterangan { get; set; }
            public BangunanNonAktif ObjekAsli { get; set; }
        }

        public NonAktifBangunanForm()
        {
            InitializeComponent();
        }

        private void NonAktifBangunanForm_Load(object sender, EventArgs e)
        {
            LoadComboPenyebab();
            SetupGrids();
            SetMode("View");
            btnCari_Click(null, null);
        }

        private void SetupGrids()
        {
            dgBangunan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgBangunan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgBangunan.ReadOnly = true;
            dgBangunan.RowHeadersVisible = false;

            dgNonAktif.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgNonAktif.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgNonAktif.ReadOnly = true;
            dgNonAktif.RowHeadersVisible = false;
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

                bool bisaDitambah = currentKodeBangunan != 0 && currentStatusBangunan != "Non Aktif";

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

        private void btnCari_Click(object sender, EventArgs e)
        {
            string cari = txtCari.Text.ToLower().Trim();

            var bangunanList = db.AsetBangunan.AsNoTracking()
                .Where(b => b.KodeBangunan.ToString().Contains(cari) ||
                            (b.NamaBangunan != null && b.NamaBangunan.ToLower().Contains(cari)))
                .Take(50)
                .ToList();

            var dataGrid = bangunanList.Select(b => new BangunanViewModel
            {
                KodeBangunan = b.KodeBangunan,
                NamaBangunan = b.NamaBangunan ?? "-",
                LuasBangunan = b.LuasBangunan,
                Status = b.Status ?? "-"
            }).ToList();

            dgBangunan.DataSource = dataGrid;

            if (dgBangunan.Columns["KodeBangunan"] != null) dgBangunan.Columns["KodeBangunan"].HeaderText = "Kode Bangunan";
            if (dgBangunan.Columns["NamaBangunan"] != null) dgBangunan.Columns["NamaBangunan"].HeaderText = "Nama Bangunan";
            if (dgBangunan.Columns["LuasBangunan"] != null) dgBangunan.Columns["LuasBangunan"].HeaderText = "Luas (m2)";
            if (dgBangunan.Columns["Status"] != null) dgBangunan.Columns["Status"].HeaderText = "Status";
        }

        private void txtCari_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCari_Click(sender, e);
            }
        }

        private void dgBangunan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSimpan.Enabled) return; 

            if (e.RowIndex >= 0 && dgBangunan.Rows[e.RowIndex].DataBoundItem is BangunanViewModel vm)
            {
                txtKodeBangunan.Text = vm.KodeBangunan.ToString();
                txtNamaBangunan.Text = vm.NamaBangunan;

                currentKodeBangunan = vm.KodeBangunan;
                currentStatusBangunan = vm.Status;

                loadDgvNonAktif();
                SetMode("View");

                if (currentStatusBangunan == "Non Aktif")
                {
                    MessageBox.Show("Bangunan ini sudah berstatus Non-Aktif. Anda tidak dapat menonaktifkannya lagi.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void loadDgvNonAktif()
        {
            if (currentKodeBangunan == 0) return;

            var listNonAktif = db.BangunanNonAktif.AsNoTracking()
                .Include(n => n.IdStatusNavigation)
                .Where(n => n.KodeBangunan == currentKodeBangunan)
                .OrderByDescending(n => n.Tanggal)
                .ToList();

            var dataGrid = listNonAktif.Select(n => new NonAktifBangunanViewModel
            {
                IdBangunanNonAktif = n.IdBangunanNonAktif,
                Tanggal = n.Tanggal,
                Penyebab = n.IdStatusNavigation?.NamaStatus ?? "-",
                Keterangan = n.Keterangan,
                ObjekAsli = n
            }).ToList();

            dgNonAktif.DataSource = new SortableBindingList<NonAktifBangunanViewModel>(dataGrid);

            if (dgNonAktif.Columns["IdBangunanNonAktif"] != null) dgNonAktif.Columns["IdBangunanNonAktif"].Visible = false;
            if (dgNonAktif.Columns["ObjekAsli"] != null) dgNonAktif.Columns["ObjekAsli"].Visible = false;
            if (dgNonAktif.Columns["Penyebab"] != null) dgNonAktif.Columns["Penyebab"].HeaderText = "Penyebab";
        }

        private void dgNonAktif_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSimpan.Enabled) return;

            if (e.RowIndex >= 0 && dgNonAktif.Rows[e.RowIndex].DataBoundItem is NonAktifBangunanViewModel vm)
            {
                var nonAktif = db.BangunanNonAktif.Find(vm.IdBangunanNonAktif);
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
            if (currentKodeBangunan == 0)
            {
                MessageBox.Show("Pilih aset bangunan dari tabel kiri terlebih dahulu.");
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
            if (bindingSource1.Current is BangunanNonAktif)
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

            if (bindingSource1.Current is BangunanNonAktif k)
            {
                bindingSource1.EndEdit();

                try
                {
                    int idPenyebab = (int)cmbPenyebab.SelectedValue;

                    if (k.IdBangunanNonAktif == 0)
                    {
                        var baru = new BangunanNonAktif
                        {
                            KodeBangunan = currentKodeBangunan,
                            Tanggal = dtpTanggal.Value,
                            IdStatus = idPenyebab,
                            Keterangan = txtKeterangan.Text
                        };
                        db.BangunanNonAktif.Add(baru);
                    }
                    else
                    {
                        k.Tanggal = dtpTanggal.Value;
                        k.IdStatus = idPenyebab;
                        k.Keterangan = txtKeterangan.Text;
                    }

                    var asetUtama = db.AsetBangunan.FirstOrDefault(a => a.KodeBangunan == currentKodeBangunan);
                    if (asetUtama != null)
                    {
                        asetUtama.Status = "Nonaktif";
                        currentStatusBangunan = "Nonaktif";
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan! Status bangunan telah diperbarui menjadi Non Aktif.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loadDgvNonAktif();
                    btnCari_Click(null, null);
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
            if (bindingSource1.Current is BangunanNonAktif k && k.IdBangunanNonAktif != 0)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus data non-aktif ini?\n\nStatus bangunan akan dikembalikan menjadi 'Aktif'.", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        db.BangunanNonAktif.Remove(k);

                        var asetUtama = db.AsetBangunan.FirstOrDefault(a => a.KodeBangunan == currentKodeBangunan);
                        if (asetUtama != null)
                        {
                            asetUtama.Status = "Aktif";
                            currentStatusBangunan = "Aktif";
                        }

                        db.SaveChanges();

                        MessageBox.Show("Data non-aktif berhasil dihapus. Status bangunan telah dipulihkan.");
                        loadDgvNonAktif();
                        bindingSource1.AddNew();
                        btnCari_Click(null, null);

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