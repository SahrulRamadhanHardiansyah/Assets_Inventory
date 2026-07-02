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
    public partial class MasterRuangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MasterRuangForm()
        {
            InitializeComponent();
        }

        private void MasterRuangForm_Load(object sender, EventArgs e)
        {
            loadDgvRuang();
            loadDgvLemari();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                // Ruang State
                txtNama.Enabled = false;
                txtKode.Enabled = false;
                txtLantai.Enabled = false;
                txtKeterangan.Enabled = false;
                cbAktif.Enabled = false;
                btnTambah.Enabled = true;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;

                // Lemari State
                txtNama2.Enabled = false;
                txtKode2.Enabled = false;
                cmbRuang.Enabled = false;
                btnTambah2.Enabled = true;
                btnUbah2.Enabled = true;
                btnHapus2.Enabled = true;
                btnSimpan2.Enabled = false;
                btnBatal2.Enabled = false;
            }
            else
            {
                // Ruang State
                txtNama.Enabled = true;
                txtKode.Enabled = true;
                txtLantai.Enabled = true;
                txtKeterangan.Enabled = true;
                cbAktif.Enabled = true;
                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;

                // Lemari State 
                txtNama2.Enabled = true;
                txtKode2.Enabled = true;
                cmbRuang.Enabled = true;
                btnTambah2.Enabled = false;
                btnUbah2.Enabled = false;
                btnHapus2.Enabled = false;
                btnSimpan2.Enabled = true;
                btnBatal2.Enabled = true;
            }
        }

        // Ruang Logic

        private void loadDgvRuang()
        {
            var cari = txtCari.Text.Trim().ToLower();
            dg.DataSource = new SortableBindingList<Ruang>(db.Ruang.Where(mb => mb.NamaRuang.ToLower().Contains(cari) || mb.KodeRuang.ToLower().Contains(cari)).ToList());
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Ruang r)
            {
                var ruang = db.Ruang.Find(r.IdRuang);
                if (ruang != null)
                {
                    bindingSource1.DataSource = ruang;

                    if (ruang.IsActive == true) cbAktif.Checked = true;
                    else cbAktif.Checked = false;
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
            if (bindingSource1.Current is Ruang)
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
                MessageBox.Show("Nama ruang harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtKode.Text))
            {
                MessageBox.Show("Kode ruang harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtLantai.Text))
            {
                MessageBox.Show("Lantai harus diisi.");
                return;
            }

            if (bindingSource1.Current is Ruang k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdRuang == 0)
                    {
                        var baru = new Ruang
                        {
                            NamaRuang = txtNama.Text,
                            KodeRuang = txtKode.Text.ToUpper(),
                            Lantai = txtLantai.Text,
                            Keterangan = txtKeterangan.Text,
                            IsActive = cbAktif.Checked
                        };
                        db.Ruang.Add(baru);
                    }
                    else
                    {
                        k.NamaRuang = txtNama.Text;
                        k.KodeRuang = txtKode.Text.ToUpper();
                        k.Lantai = txtLantai.Text;
                        k.Keterangan = txtKeterangan.Text;
                        k.IsActive = cbAktif.Checked;
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvRuang();
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
            if (bindingSource1.Current is Ruang k && k.IdRuang != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaRuang}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Ruang.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadDgvRuang();
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
            loadDgvRuang();
            SetMode("View");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadDgvRuang();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                ofd.Title = "Pilih File Excel Ruang";

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
                                    string lantai = row[2]?.ToString().Trim();
                                    string keterangan = row[3]?.ToString().Trim();
                                    bool isActive = row[4]?.ToString().Trim().ToLower() == "aktif";

                                    if (!string.IsNullOrEmpty(nama))
                                    {
                                        var ruangImpor = new Ruang
                                        {
                                            NamaRuang = nama,
                                            KodeRuang = kode,
                                            Lantai = lantai,
                                            Keterangan = keterangan,
                                            IsActive = isActive
                                        };

                                        db.Ruang.Add(ruangImpor);
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
                        loadDgvRuang();
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

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is Ruang r)
            {
                if (IsActiveColumn.Index == e.ColumnIndex) e.Value = r.IsActive == true ? "Aktif" : "Tidak Aktif";
            }
        }

        private void tb_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbRuang.Refresh();
            tbLemari.Refresh();
        }

        // Lemari Logic

        private void loadDgvLemari()
        {
            cmbRuang.DataSource = db.Ruang.ToList();
            var cari = txtCari2.Text.Trim().ToLower();
            dg2.DataSource = new SortableBindingList<Lemari>(db.Lemari.Where(mb => mb.Nama.ToLower().Contains(cari) || mb.KodeLemari.ToLower().Contains(cari)).ToList());
        }

        private void btnCari2_Click(object sender, EventArgs e)
        {
            loadDgvLemari();
        }

        private void dg2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg2.Rows[e.RowIndex].DataBoundItem is Lemari l)
            {
                var lemari = db.Lemari.Find(l.IdLemari);
                if (lemari != null)
                {
                    bindingSource2.DataSource = lemari;
                    cmbRuang.SelectedValue = lemari.IdRuang;
                }
            }
        }

        private void dg2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg2.Rows[e.RowIndex].DataBoundItem is Lemari l)
            {
                if (idRuangNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = l.IdRuangNavigation?.NamaRuang;
            }
        }

        private void btnTambah2_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource2.AddNew();
        }

        private void btnUbah2_Click(object sender, EventArgs e)
        {
            if (bindingSource2.Current is Lemari)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private void btnSimpan2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNama2.Text))
            {
                MessageBox.Show("Nama lemari harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtKode2.Text))
            {
                MessageBox.Show("Kode lemari harus diisi.");
                return;
            }

            if (cmbRuang.SelectedItem == null)
            {
                MessageBox.Show("Pilih ruang yang terlebih dahulu.");
                return;
            }

            if (bindingSource2.Current is Lemari l)
            {
                bindingSource2.EndEdit();

                try
                {
                    if (l.IdLemari == 0)
                    {
                        var baru = new Lemari
                        {
                            Nama = txtNama2.Text,
                            KodeLemari = txtKode2.Text.ToUpper(),
                            IdRuang = (int)cmbRuang.SelectedValue,
                        };
                        db.Lemari.Add(baru);
                    }
                    else
                    {
                        l.Nama = txtNama2.Text;
                        l.KodeLemari = txtKode2.Text.ToUpper();
                        l.IdRuang = (int)cmbRuang.SelectedValue;
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvLemari();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus2_Click(object sender, EventArgs e)
        {
            if (bindingSource2.Current is Lemari k && k.IdLemari != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.Nama}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Lemari.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadDgvLemari();
                        bindingSource2.AddNew();
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

        private void btnBatal2_Click(object sender, EventArgs e)
        {
            bindingSource2.CancelEdit();
            bindingSource2.AddNew();
            loadDgvLemari();
            SetMode("View");
        }

        private void btnTutup2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}