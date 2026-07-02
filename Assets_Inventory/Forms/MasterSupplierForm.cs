using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class MasterSupplierForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MasterSupplierForm()
        {
            InitializeComponent();
        }

        private void MasterSupplierForm_Load(object sender, EventArgs e)
        {
            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtNama.Enabled = false;
                txtKontak.Enabled = false;
                txtAlamat.Enabled = false;
                txtKeterangan.Enabled = false;
                btnTambah.Enabled = true;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                txtNama.Enabled = true;
                txtKontak.Enabled = true;
                txtAlamat.Enabled = true;
                txtKeterangan.Enabled = true;
                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private void loadDgv()
        {
            var cari = txtCari.Text.Trim().ToLower();
            dg.DataSource = new SortableBindingList<Pemasok>(db.Pemasok.Where(p => p.NamaPemasok.ToLower().Contains(cari) || p.NomorTelepon.ToLower().Contains(cari) || p.NomorTelepon.ToLower().Contains(cari)).ToList());
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Pemasok p)
            {
                var pemasok = db.Pemasok.Find(p.IdPemasok);
                if (pemasok != null)
                {
                    bindingSource1.DataSource = pemasok;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pemasok)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNama.Text))
            {
                MessageBox.Show("Nama pemasok harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtAlamat.Text))
            {
                MessageBox.Show("Alamat harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtKontak.Text))
            {
                MessageBox.Show("Kontak harus diisi.");
                return;
            }

            if (bindingSource1.Current is Pemasok k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdPemasok == 0) 
                    {
                        var baru = new Pemasok
                        {
                            NamaPemasok = txtNama.Text,
                            NomorTelepon = txtKontak.Text,
                            Alamat = txtAlamat.Text,
                            Keterangan = txtKeterangan.Text
                        };
                        db.Pemasok.Add(baru);
                    }
                    else 
                    {
                        k.NamaPemasok = txtNama.Text;
                        k.NomorTelepon = txtKontak.Text;
                        k.Alamat = txtAlamat.Text;
                        k.Keterangan = txtKeterangan.Text;
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgv();
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
            if (bindingSource1.Current is Pemasok k && k.IdPemasok != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaPemasok}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Pemasok.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadDgv();
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
            loadDgv();
            SetMode("View");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadDgv();
        }

        private void txtKontak_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtKontak_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }
    }
}