using Assets_Inventory.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class MasterRombelForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MasterRombelForm()
        {
            InitializeComponent();
        }

        private void MasterRombelForm_Load(object sender, EventArgs e)
        {
            cmbJurusan.DataSource = db.Jurusan.ToList();
            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtNama.Enabled = false;
                txtKode.Enabled = false;
                txtTingkat.Enabled = false;
                cmbJurusan.Enabled = false;
                cbAktif.Enabled = false;
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
                txtTingkat.Enabled = true;
                cmbJurusan.Enabled = true;
                cbAktif.Enabled = true;
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
            dg.DataSource = new SortableBindingList<Rombel>(db.Rombel.Where(mb => mb.NamaRombel.ToLower().Contains(cari) || mb.KodeRombel.ToLower().Contains(cari)).ToList());
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Rombel r)
            {
                var rombel = db.Rombel.Find(r.IdRombel);
                if (rombel != null)
                {
                    bindingSource1.DataSource = rombel;

                    if (rombel.IsActive == true) cbAktif.Checked = true;
                    else cbAktif.Checked = false;
                }
            }
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is Rombel r)
            {
                if (idJurusanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = r.IdJurusanNavigation.NamaJurusan;
                }

                if (isActiveDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = r.IsActive == true ? "Aktif" : "Tidak Aktif";

            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadDgv();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Rombel)
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

            if (string.IsNullOrEmpty(txtTingkat.Text))
            {
                MessageBox.Show("Tingkat harus diisi.");
                return;
            }

            if (cmbJurusan.SelectedItem == null)
            {
                MessageBox.Show("Jurusan harus dipilih.");
                return;
            }

            if (bindingSource1.Current is Rombel k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdRombel == 0)
                    {
                        var baru = new Rombel
                        {
                            NamaRombel = txtNama.Text,
                            KodeRombel = txtKode.Text.ToUpper(),
                            Tingkat = txtTingkat.Text,
                            IdJurusan = (int)cmbJurusan.SelectedValue,
                            IsActive = cbAktif.Checked
                        };
                        db.Rombel.Add(baru);
                    }
                    else
                    {
                        k.NamaRombel = txtNama.Text;
                        k.KodeRombel = txtKode.Text.ToUpper();
                        k.Tingkat = txtTingkat.Text;
                        k.IdJurusan = (int)cmbJurusan.SelectedValue;
                        k.IsActive = cbAktif.Checked;
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
            if (bindingSource1.Current is Rombel k && k.IdRombel != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaRombel}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Rombel.Remove(k);
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
                                    string tingkat = row[2]?.ToString().Trim().ToUpper();
                                    int idJurusan = int.TryParse(row[3]?.ToString().Trim(), out int tempId) ? tempId : 0;
                                    bool isActive = row[4]?.ToString().Trim().ToLower() == "aktif";

                                    if (!string.IsNullOrEmpty(nama) && idJurusan > 0)
                                    {
                                        var rombelImpor = new Rombel
                                        {
                                            NamaRombel = nama,
                                            KodeRombel = kode,
                                            Tingkat = tingkat,
                                            IdJurusan = idJurusan,
                                            IsActive = isActive
                                        };

                                        db.Rombel.Add(rombelImpor);
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
