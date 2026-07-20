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
    public partial class OpnameBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        private string currentKodeInventaris = "";

        public class AsetViewModel
        {
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string Status { get; set; }
            public string KondisiSaatIni { get; set; }
        }

        public class OpnameViewModel
        {
            public int IdOpnameAset { get; set; }
            public DateTime Tanggal { get; set; }
            public string Kondisi { get; set; }
            public string Keterangan { get; set; }
            public OpnameAset ObjekAsli { get; set; }
        }

        public OpnameBarangForm()
        {
            InitializeComponent();
        }

        private void OpnameBarangForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Proses Opname");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnUbah.Enabled = hakAkses.HakUbah;
            btnSimpan.Enabled = hakAkses.HakBuat || hakAkses.HakUbah;
            btnHapus.Enabled = hakAkses.HakHapus;

            LoadComboKondisi();
            SetupGrids();
            SetMode("View");

            btnCariAset_Click(null, null);
        }

        private void SetupGrids()
        {
            dgAset.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgAset.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgAset.ReadOnly = true;
            dgAset.RowHeadersVisible = false;

            dgOpname.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgOpname.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgOpname.ReadOnly = true;
            dgOpname.RowHeadersVisible = false;
        }

        private void LoadComboKondisi()
        {
            var listKondisi = db.Kondisi.ToList();
            cmbKondisi.DataSource = listKondisi;
            cmbKondisi.DisplayMember = "NamaKondisi";
            cmbKondisi.ValueMember = "IdKondisi";
            cmbKondisi.SelectedIndex = -1;
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                dtpTanggal.Enabled = false;
                cmbKondisi.Enabled = false;
                txtKeterangan.ReadOnly = true;

                btnTambah.Enabled = !string.IsNullOrEmpty(txtKodeInventaris.Text);
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else 
            {
                dtpTanggal.Enabled = true;
                cmbKondisi.Enabled = true;
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
            var dictKondisi = db.Kondisi.ToDictionary(k => k.IdKondisi, k => k.NamaKondisi);

            var rawAset = db.Aset.AsNoTracking()
                .Where(a => a.KodeInventaris != null && a.KodeInventaris != "")
                .ToList();

            var dataGrid = rawAset.Select(a => {
                int? idKondisiAset = a.IdKondisi; 

                return new AsetViewModel
                {
                    KodeInventaris = a.KodeInventaris,
                    NamaBarang = dictBarang.ContainsKey(a.IdMasterBarang) ? dictBarang[a.IdMasterBarang] : "N/A",
                    Status = a.Status,
                    KondisiSaatIni = (idKondisiAset.HasValue && dictKondisi.ContainsKey(idKondisiAset.Value)) ? dictKondisi[idKondisiAset.Value] : "-"
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
            if (dgAset.Columns["KondisiSaatIni"] != null) dgAset.Columns["KondisiSaatIni"].HeaderText = "Kondisi";
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

                loadDgvOpname();
                SetMode("View");
            }
        }

        private void loadDgvOpname()
        {
            if (string.IsNullOrEmpty(currentKodeInventaris)) return;

            var opnameList = db.OpnameAset.AsNoTracking()
                .Include(o => o.IdKondisiNavigation)
                .Where(o => o.KodeInventaris == currentKodeInventaris)
                .OrderByDescending(o => o.TanggalOpname)
                .ToList();

            var dataGrid = opnameList.Select(o => new OpnameViewModel
            {
                IdOpnameAset = o.IdOpnameAset,
                Tanggal = o.TanggalOpname,
                Kondisi = o.IdKondisiNavigation?.NamaKondisi ?? "-",
                Keterangan = o.Keterangan,
                ObjekAsli = o
            }).ToList();

            dgOpname.DataSource = new SortableBindingList<OpnameViewModel>(dataGrid);

            if (dgOpname.Columns["IdOpnameAset"] != null) dgOpname.Columns["IdOpnameAset"].Visible = false;
            if (dgOpname.Columns["ObjekAsli"] != null) dgOpname.Columns["ObjekAsli"].Visible = false;
        }

        private void dgOpname_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSimpan.Enabled) return; 

            if (e.RowIndex >= 0 && dgOpname.Rows[e.RowIndex].DataBoundItem is OpnameViewModel vm)
            {
                var opname = db.OpnameAset.Find(vm.IdOpnameAset);
                if (opname != null)
                {
                    bindingSource1.DataSource = opname;

                    dtpTanggal.Value = opname.TanggalOpname;
                    cmbKondisi.SelectedValue = opname.IdKondisi ?? -1;
                    txtKeterangan.Text = opname.Keterangan;
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
            cmbKondisi.SelectedIndex = -1;
            txtKeterangan.Clear();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is OpnameAset)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih riwayat opname yang ingin diubah dari tabel bawah.");
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbKondisi.SelectedIndex == -1)
            {
                MessageBox.Show("Kondisi aset harus dipilih.");
                return;
            }

            if (bindingSource1.Current is OpnameAset k)
            {
                bindingSource1.EndEdit();

                try
                {
                    int idKondisiTerpilih = (int)cmbKondisi.SelectedValue;

                    if (k.IdOpnameAset == 0)
                    {
                        var baru = new OpnameAset
                        {
                            KodeInventaris = currentKodeInventaris,
                            TanggalOpname = dtpTanggal.Value,
                            IdKondisi = idKondisiTerpilih,
                            Keterangan = txtKeterangan.Text
                        };
                        db.OpnameAset.Add(baru);
                    }
                    else
                    {
                        k.TanggalOpname = dtpTanggal.Value;
                        k.IdKondisi = idKondisiTerpilih;
                        k.Keterangan = txtKeterangan.Text;
                    }

                    var asetUtama = db.Aset.FirstOrDefault(a => a.KodeInventaris == currentKodeInventaris);
                    if (asetUtama != null)
                    {
                        asetUtama.IdKondisi = idKondisiTerpilih;
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loadDgvOpname();
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
            if (bindingSource1.Current is OpnameAset k && k.IdOpnameAset != 0)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus riwayat opname ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.OpnameAset.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadDgvOpname();
                        bindingSource1.AddNew();

                        dtpTanggal.Value = DateTime.Now;
                        cmbKondisi.SelectedIndex = -1;
                        txtKeterangan.Clear();
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        db.Entry(k).Reload();
                        MessageBox.Show("Tidak dapat menghapus data ini karena data masih terikat relasi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            loadDgvOpname();

            dtpTanggal.Value = DateTime.Now;
            cmbKondisi.SelectedIndex = -1;
            txtKeterangan.Clear();

            SetMode("View");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}