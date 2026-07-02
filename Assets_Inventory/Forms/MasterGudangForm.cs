using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class MasterGudangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MasterGudangForm()
        {
            InitializeComponent();
        }

        private void MasterGudangForm_Load(object sender, EventArgs e)
        {
            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtNama.Enabled = false;
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
            dg.DataSource = new SortableBindingList<Gudang>(db.Gudang.Where(g => g.NamaGudang.ToLower().Contains(cari) || g.KodeGudang.ToLower().Contains(cari)).ToList());
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Gudang g)
            {
                var gudang = db.Gudang.Find(g.KodeGudang);
                if (gudang != null)
                {
                    bindingSource1.DataSource = gudang;
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
            if (bindingSource1.Current is Gudang)
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
                MessageBox.Show("Nama gudang harus diisi.");
                return;
            }

            if (bindingSource1.Current is Gudang k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (string.IsNullOrEmpty(txtKode.Text) || txtKode.Text == "0")
                    {
                        var lastGudang = db.Gudang
                                           .OrderByDescending(g => g.KodeGudang)
                                           .FirstOrDefault();

                        int nextUrutan = 1; 

                        if (lastGudang != null && !string.IsNullOrEmpty(lastGudang.KodeGudang))
                        {
                            string strAngka = lastGudang.KodeGudang.Substring(4);

                            if (int.TryParse(strAngka, out int angkaTerakhir))
                            {
                                nextUrutan = angkaTerakhir + 1;
                            }
                        }

                        var currentCode = $"GDG-{nextUrutan.ToString("D3")}";

                        var baru = new Gudang
                        {
                            KodeGudang = currentCode,
                            NamaGudang = txtNama.Text,
                            Keterangan = txtKeterangan.Text
                        };
                        db.Gudang.Add(baru);
                    }
                    else 
                    {
                        k.NamaGudang = txtNama.Text;
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
            if (bindingSource1.Current is Gudang k && !string.IsNullOrEmpty(k.KodeGudang))
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaGudang}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Gudang.Remove(k);
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
    }
}