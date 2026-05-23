using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory
{
    public partial class MutasiBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MutasiBarangForm()
        {
            InitializeComponent();
        }

        private void MutasiBarangForm_Load(object sender, EventArgs e)
        {
            cmbJurusanAsal.DataSource = db.Jurusan.ToList();
            cmbJurusanTujuan.DataSource = db.Jurusan.ToList();
            loadAset();
            loadMutasi();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                dtTgl.Enabled = false;
                cmbJurusanAsal.Enabled = false;
                cmbJurusanTujuan.Enabled = false;
                txtAlasan.Enabled = false;
                btnTambah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                dtTgl.Enabled = true;
                cmbJurusanAsal.Enabled = true;
                cmbJurusanTujuan.Enabled = true;
                txtAlasan.Enabled = true;
                btnTambah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private void loadAset()
        {
            var cari = txtCari.Text.ToLower().Trim();
            dg.DataSource = new SortableBindingList<Aset>(db.Aset.Where(a => a.IdMasterBarangNavigation.NamaBarang.ToLower().Contains(cari) || a.KodeBarang.ToLower().Contains(cari)).ToList());
        }

        private void loadMutasi()
        {
            dg2.DataSource = new SortableBindingList<Mutasi>(db.Mutasi.ToList());
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadAset();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Aset a)
            {
                var aset = db.Aset.Find(a.KodeBarang);
                if (aset != null)
                {
                    txtKodeInv.Text = aset.KodeInventaris;
                    txtNama.Text = aset.IdMasterBarangNavigation.NamaBarang;
                    if (aset.IdJurusanNavigation != null) cmbJurusanAsal.SelectedValue = aset.IdJurusan;
                    else cmbJurusanAsal.SelectedIndex = -1;
                }
            }  
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is Aset aset)
            {
                if (hargaSatuanDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = aset.HargaSatuan.HasValue ? aset.HargaSatuan.Value.ToString("C2") : "0.00";
                if (nilaiResiduDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = aset.NilaiResidu.HasValue ? aset.NilaiResidu.Value.ToString("C2") : "0.00";
                if (idJurusanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = aset.IdJurusanNavigation != null ? aset.IdJurusanNavigation.NamaJurusan : "";
                if (idRuangNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = aset.IdRuangNavigation != null ? aset.IdRuangNavigation.NamaRuang : "";
                if (idLokasiNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = aset.IdLokasiNavigation != null ? aset.IdLokasiNavigation.NamaLokasi : "";
            }
        }

        private void dg2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg2.Rows[e.RowIndex].DataBoundItem is Mutasi m)
            {
                var mutasi = db.Mutasi.Find(m.IdMutasi);
                if (mutasi != null)
                {
                    bindingSource1.DataSource = mutasi;
                    txtNama.Text = mutasi.KodeInventarisNavigation.IdMasterBarangNavigation.NamaBarang;
                    if (mutasi.IdJurusanAsal != null) cmbJurusanAsal.SelectedValue = mutasi.IdJurusanAsal;
                    else cmbJurusanAsal.SelectedIndex = -1;
                    if (mutasi.IdJurusanTujuan != null) cmbJurusanTujuan.SelectedValue = mutasi.IdJurusanTujuan;
                    else cmbJurusanTujuan.SelectedIndex = -1;
                }
            }
        }

        private void dg2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg2.Rows[e.RowIndex].DataBoundItem is Mutasi m)
            {
                if (kodeInventarisNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = m.KodeInventarisNavigation != null ? m.KodeInventarisNavigation.IdMasterBarangNavigation.NamaBarang : "";
                if (idJurusanAsalNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = m.IdJurusanAsalNavigation != null ? m.IdJurusanAsalNavigation.NamaJurusan : "";
                if (idJurusanTujuanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = m.IdJurusanTujuanNavigation != null ? m.IdJurusanTujuanNavigation.NamaJurusan : "";
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKodeInv.Text))
            {
                MessageBox.Show("Pilih aset dari daftar terlebih dahulu.");
                return;
            }

            if (string.IsNullOrEmpty(txtAlasan.Text))
            {
                MessageBox.Show("Alasan harus diisi.");
                return;
            }

            if (cmbJurusanTujuan.SelectedItem == null)
            {
                MessageBox.Show("Jurusan tujuan harus dipilih.");
                return;
            }

            if (bindingSource1.Current is Mutasi k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdMutasi == 0)
                    {
                        int? idJurusanAsal = cmbJurusanAsal.SelectedIndex != -1 ? (int?)cmbJurusanAsal.SelectedValue : null;

                        var baru = new Mutasi
                        {
                            TanggalMutasi = dtTgl.Value,
                            KodeInventaris = txtKodeInv.Text,
                            IdJurusanAsal = idJurusanAsal,
                            IdJurusanTujuan = (int)cmbJurusanTujuan.SelectedValue,
                            AlasanMutasi = txtAlasan.Text,
                        };
                        
                        var aset = db.Aset.FirstOrDefault(a => a.KodeInventaris == k.KodeInventaris); ;

                        if (aset != null) aset.IdJurusan = (int)cmbJurusanTujuan.SelectedValue;
                        else
                        {
                            MessageBox.Show("Aset tidak ditemukan.");
                            return;
                        }

                        db.Mutasi.Add(baru);
                    }
                    else
                    {
                        k.TanggalMutasi = dtTgl.Value;
                        k.IdJurusanAsal = (int)cmbJurusanAsal.SelectedValue;
                        k.IdJurusanTujuan = (int)cmbJurusanTujuan.SelectedValue;
                        k.AlasanMutasi = txtAlasan.Text;

                        var aset = db.Aset.FirstOrDefault(a => a.KodeInventaris == k.KodeInventaris); ;

                        if (aset != null) aset.IdJurusan = (int)cmbJurusanTujuan.SelectedValue;
                        else
                        {
                            MessageBox.Show("Aset tidak ditemukan.");
                            return;
                        }
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadMutasi();
                    loadAset();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Mutasi k && k.IdMutasi != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data mutasi ID {k.IdMutasi} dengan aset barang {k.KodeInventarisNavigation.IdMasterBarangNavigation.NamaBarang}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var aset = db.Aset.FirstOrDefault(a => a.KodeInventaris == k.KodeInventaris);
                        if (aset != null) aset.IdJurusan = k.IdJurusanAsal;

                        db.Mutasi.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadMutasi();
                        bindingSource1.AddNew();
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        db.Entry(k).Reload();
                        MessageBox.Show("Tidak dapat menghapus data ini karena data masih digunakan oleh data lain di dalam sistem.", "Peringatan Relasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            loadMutasi();
            loadAset();
            SetMode("View");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Mutasi)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }
    }
}
