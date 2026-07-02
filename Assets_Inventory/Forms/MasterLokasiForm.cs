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
    public partial class MasterLokasiForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MasterLokasiForm()
        {
            InitializeComponent();
        }

        private void MasterLokasiForm_Load(object sender, EventArgs e)
        {
            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtNama.Enabled = false;
                txtKode.Enabled = false;
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
                txtKode.Enabled = true;
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
            dg.DataSource = new SortableBindingList<Lokasi>(db.Lokasi.Where(l => l.NamaLokasi.ToLower().Contains(cari) || l.KodeLokasi.ToLower().Contains(cari)).ToList());
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Lokasi l)
            {
                var lokasi = db.Lokasi.Find(l.IdLokasi);
                if (lokasi != null)
                {
                    bindingSource1.DataSource = lokasi;
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
            if (bindingSource1.Current is Lokasi)
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
                MessageBox.Show("Nama lokasi harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtKode.Text))
            {
                MessageBox.Show("Kode lokasi harus diisi.");
                return;
            }

            if (bindingSource1.Current is Lokasi l)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (l.IdLokasi == 0) 
                    {
                        var baru = new Lokasi
                        {
                            NamaLokasi = txtNama.Text,
                            KodeLokasi = txtKode.Text.ToUpper(),
                            Keterangan = txtKeterangan.Text
                        };
                        db.Lokasi.Add(baru);
                    }
                    else 
                    {
                        l.NamaLokasi = txtNama.Text;
                        l.KodeLokasi = txtKode.Text.ToUpper();
                        l.Keterangan = txtKeterangan.Text;
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
            if (bindingSource1.Current is Lokasi l && l.IdLokasi != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {l.NamaLokasi}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Lokasi.Remove(l);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadDgv();
                        bindingSource1.AddNew();
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        db.Entry(l).Reload();
                        MessageBox.Show("Tidak dapat menghapus data ini karena data masih digunakan oleh data lain di dalam sistem.", "Peringatan Relasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        db.Entry(l).Reload();
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
                ofd.Title = "Pilih File Excel Lokasi";

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
                                    string kode = row[1]?.ToString().Trim().ToUpper();
                                    string keterangan = row[2]?.ToString().Trim();

                                    if (!string.IsNullOrEmpty(nama))
                                    {
                                        var lokasiImpor = new Lokasi
                                        {
                                            NamaLokasi = nama,
                                            KodeLokasi = kode,
                                            Keterangan = keterangan
                                        };

                                        db.Lokasi.Add(lokasiImpor);
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