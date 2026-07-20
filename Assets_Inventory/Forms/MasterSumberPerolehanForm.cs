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
    public partial class MasterSumberPerolehanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MasterSumberPerolehanForm()
        {
            InitializeComponent();
        }

        private void MasterSumberPerolehanForm_Load(object sender, EventArgs e)
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
                txtKeterangan.Enabled = true;
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
            dg.DataSource = new SortableBindingList<SumberPerolehan>(db.SumberPerolehan.Where(mb => mb.NamaSumber.ToLower().Contains(cari) || mb.KodeSumber.ToLower().Contains(cari)).ToList());
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is SumberPerolehan r)
            {
                var sumberPerolehan = db.SumberPerolehan.Find(r.IdSumberPerolehan);
                if (sumberPerolehan != null)
                {
                    bindingSource1.DataSource = sumberPerolehan;

                    if (sumberPerolehan.IsActive == true) cbAktif.Checked = true;
                    else cbAktif.Checked = false;
                }
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
            if (bindingSource1.Current is SumberPerolehan)
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

            if (bindingSource1.Current is SumberPerolehan k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdSumberPerolehan == 0)
                    {
                        var baru = new SumberPerolehan
                        {
                            NamaSumber = txtNama.Text,
                            KodeSumber = txtKode.Text.ToUpper(),
                            Keterangan = txtKeterangan.Text,
                            IsActive = cbAktif.Checked
                        };
                        db.SumberPerolehan.Add(baru);
                    }
                    else
                    {
                        k.NamaSumber = txtNama.Text;
                        k.KodeSumber = txtKode.Text.ToUpper();
                        k.Keterangan = txtKeterangan.Text;
                        k.IsActive = cbAktif.Checked;
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgv();
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
            if (bindingSource1.Current is SumberPerolehan k && k.IdSumberPerolehan != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaSumber}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.SumberPerolehan.Remove(k);
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
                                    string keterangan = row[2]?.ToString().Trim();
                                    bool isActive = row[3]?.ToString().Trim().ToLower() == "aktif";

                                    if (!string.IsNullOrEmpty(nama))
                                    {
                                        var sumberImpor = new SumberPerolehan
                                        {
                                            NamaSumber = nama,
                                            KodeSumber = kode,
                                            Keterangan = keterangan,
                                            IsActive = isActive
                                        };

                                        db.SumberPerolehan.Add(sumberImpor);
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

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is SumberPerolehan r)
            {
                if (isActiveDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = r.IsActive == true ? "Aktif" : "Tidak Aktif";
            }
        }
    }
}
