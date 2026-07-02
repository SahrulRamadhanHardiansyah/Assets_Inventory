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
    public partial class NonAktifTanahForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        private int currentKodeTanah = 0;

        public class TanahViewModel
        {
            public int KodeTanah { get; set; }
            public string NomorSertifikat { get; set; }
            public string NamaPemilik { get; set; }
            public int LuasTanah { get; set; }
            public string Status { get; set; }
        }

        public class NonAktifTanahViewModel
        {
            public int IdTanahNonAktif { get; set; }
            public DateTime Tanggal { get; set; }
            public string Penyebab { get; set; }
            public string Keterangan { get; set; }
            public TanahNonAktif ObjekAsli { get; set; }
        }

        public NonAktifTanahForm()
        {
            InitializeComponent();
        }

        private void NonAktifTanahForm_Load(object sender, EventArgs e)
        {
            LoadComboPenyebab();
            SetupGrids();
            SetMode("View");
            btnCari_Click(null, null);
        }

        private void SetupGrids()
        {
            dgTanah.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgTanah.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgTanah.ReadOnly = true;
            dgTanah.RowHeadersVisible = false;

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

                btnTambah.Enabled = currentKodeTanah != 0;
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

            var tanahList = db.AsetTanah.AsNoTracking()
                .Where(t => t.KodeTanah.ToString().Contains(cari) ||
                            (t.NamaPemilik != null && t.NamaPemilik.ToLower().Contains(cari)) ||
                            (t.NomorSertifikat != null && t.NomorSertifikat.ToLower().Contains(cari)))
                .Take(50)
                .ToList();

            var dataGrid = tanahList.Select(t => new TanahViewModel
            {
                KodeTanah = t.KodeTanah,
                NomorSertifikat = t.NomorSertifikat ?? "-",
                NamaPemilik = t.NamaPemilik ?? "-",
                LuasTanah = t.LuasTanah,
                Status = t.Status ?? "-" 
            }).ToList();

            dgTanah.DataSource = dataGrid;

            if (dgTanah.Columns["KodeTanah"] != null) dgTanah.Columns["KodeTanah"].HeaderText = "Kode Tanah";
            if (dgTanah.Columns["NomorSertifikat"] != null) dgTanah.Columns["NomorSertifikat"].HeaderText = "No. Sertifikat";
            if (dgTanah.Columns["NamaPemilik"] != null) dgTanah.Columns["NamaPemilik"].HeaderText = "Pemilik";
            if (dgTanah.Columns["LuasTanah"] != null) dgTanah.Columns["LuasTanah"].HeaderText = "Luas (m2)";
            if (dgTanah.Columns["Status"] != null) dgTanah.Columns["Status"].HeaderText = "Status"; // <--- PERBAIKAN: Merapikan header
        }

        private void txtCari_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCari_Click(sender, e);
            }
        }

        private void dgTanah_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSimpan.Enabled) return; 

            if (e.RowIndex >= 0 && dgTanah.Rows[e.RowIndex].DataBoundItem is TanahViewModel vm)
            {
                txtKodeTanah.Text = vm.KodeTanah.ToString();
                txtNoSertifikat.Text = vm.NomorSertifikat;
                txtNamaPemilik.Text = vm.NamaPemilik;

                currentKodeTanah = vm.KodeTanah;

                loadDgvNonAktif();
                SetMode("View");
            }
        }

        private void loadDgvNonAktif()
        {
            if (currentKodeTanah == 0) return;

            var listNonAktif = db.TanahNonAktif.AsNoTracking()
                .Include(n => n.IdStatusNavigation)
                .Where(n => n.KodeTanah == currentKodeTanah)
                .OrderByDescending(n => n.Tanggal)
                .ToList();

            var dataGrid = listNonAktif.Select(n => new NonAktifTanahViewModel
            {
                IdTanahNonAktif = n.IdTanahNonAktif,
                Tanggal = n.Tanggal,
                Penyebab = n.IdStatusNavigation?.NamaStatus ?? "-",
                Keterangan = n.Keterangan,
                ObjekAsli = n
            }).ToList();

            dgNonAktif.DataSource = new SortableBindingList<NonAktifTanahViewModel>(dataGrid);

            if (dgNonAktif.Columns["IdTanahNonAktif"] != null) dgNonAktif.Columns["IdTanahNonAktif"].Visible = false;
            if (dgNonAktif.Columns["ObjekAsli"] != null) dgNonAktif.Columns["ObjekAsli"].Visible = false;
            if (dgNonAktif.Columns["Penyebab"] != null) dgNonAktif.Columns["Penyebab"].HeaderText = "Penyebab";
        }

        private void dgNonAktif_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSimpan.Enabled) return;

            if (e.RowIndex >= 0 && dgNonAktif.Rows[e.RowIndex].DataBoundItem is NonAktifTanahViewModel vm)
            {
                var nonAktif = db.TanahNonAktif.Find(vm.IdTanahNonAktif);
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
            if (currentKodeTanah == 0)
            {
                MessageBox.Show("Pilih aset tanah dari tabel kiri terlebih dahulu.");
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
            if (bindingSource1.Current is TanahNonAktif)
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

            if (bindingSource1.Current is TanahNonAktif k)
            {
                bindingSource1.EndEdit();

                try
                {
                    int idPenyebab = (int)cmbPenyebab.SelectedValue;

                    if (k.IdTanahNonAktif == 0)
                    {
                        var baru = new TanahNonAktif
                        {
                            KodeTanah = currentKodeTanah,
                            Tanggal = dtpTanggal.Value,
                            IdStatus = idPenyebab,
                            Keterangan = txtKeterangan.Text
                        };
                        db.TanahNonAktif.Add(baru);
                    }
                    else
                    {
                        k.Tanggal = dtpTanggal.Value;
                        k.IdStatus = idPenyebab;
                        k.Keterangan = txtKeterangan.Text;
                    }

                    var asetUtama = db.AsetTanah.FirstOrDefault(a => a.KodeTanah == currentKodeTanah);
                    if (asetUtama != null)
                    {
                        asetUtama.Status = "Nonaktif";
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data non-aktif tanah berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loadDgvNonAktif();
                    btnCari_Click(null, null); 
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is TanahNonAktif k && k.IdTanahNonAktif != 0)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus data non-aktif ini?\n\nStatus aset akan dikembalikan menjadi 'Aktif'.", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        db.TanahNonAktif.Remove(k);

                        var asetUtama = db.AsetTanah.FirstOrDefault(a => a.KodeTanah == currentKodeTanah);
                        if (asetUtama != null)
                        {
                            asetUtama.Status = "Aktif";
                        }

                        db.SaveChanges();

                        MessageBox.Show("Data non-aktif berhasil dihapus. Status aset telah dipulihkan.");
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
                        MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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