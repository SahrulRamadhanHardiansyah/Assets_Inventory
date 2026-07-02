using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class MasterKondisiForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MasterKondisiForm()
        {
            InitializeComponent();
        }

        private void MasterKondisiForm_Load(object sender, EventArgs e)
        {
            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtKondisi.Enabled = false;
                txtKeterangan.Enabled = false;
                btnTambah.Enabled = true;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                txtKondisi.Enabled = true;
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
            dg.DataSource = new SortableBindingList<Kondisi>(db.Kondisi.Where(mb => mb.NamaKondisi.ToLower().Contains(cari)).ToList());
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Kondisi k)
            {
                var kondisi = db.Kondisi.Find(k.IdKondisi);
                if (kondisi != null)
                {
                    bindingSource1.DataSource = kondisi;
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
            if (bindingSource1.Current is Kondisi)
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
            if (string.IsNullOrEmpty(txtKondisi.Text))
            {
                MessageBox.Show("Nama kondisi harus diisi.");
                return;
            }

            if (bindingSource1.Current is Kondisi k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdKondisi == 0) 
                    {
                        var baru = new Kondisi
                        {
                            NamaKondisi = txtKondisi.Text,
                            Keterangan = txtKeterangan.Text
                        };
                        db.Kondisi.Add(baru);
                    }
                    else 
                    {
                        k.NamaKondisi = txtKondisi.Text;
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
            if (bindingSource1.Current is Kondisi k && k.IdKondisi != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaKondisi}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Kondisi.Remove(k);
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                ofd.Title = "Pilih File Excel Kondisi";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;

                        int sukses = 0;
                        int gagal = 0;

                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });

                                DataTable dt = result.Tables[0];

                                foreach (DataRow row in dt.Rows)
                                {
                                    Application.DoEvents();

                                    string nama = row[0]?.ToString().Trim();
                                    string keterangan = row[1]?.ToString().Trim();

                                    if (!string.IsNullOrEmpty(nama))
                                    {
                                        var kondisiImpor = new Kondisi
                                        {
                                            NamaKondisi = nama,
                                            Keterangan = keterangan
                                        };

                                        db.Kondisi.Add(kondisiImpor);
                                        sukses++;
                                    }
                                    else
                                    {
                                        gagal++;
                                    }
                                }

                                db.SaveChanges();
                            }
                        }

                        MessageBox.Show($"Proses Import Selesai!\n\nBerhasil: {sukses} data\nData tidak valid: {gagal} data", "Info Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgv();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang digunakan oleh aplikasi lain. Harap tutup file tersebut terlebih dahulu.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat membaca file Excel: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Enabled = true;
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
    }
}