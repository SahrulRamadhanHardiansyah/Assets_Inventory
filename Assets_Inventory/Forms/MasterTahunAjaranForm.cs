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
    public partial class MasterTahunAjaranForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        public MasterTahunAjaranForm()
        {
            InitializeComponent();
        }

        private void MasterTahunAjaranForm_Load(object sender, EventArgs e)
        {
            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtTahun.Enabled = false;
                txtSemester.Enabled = false;
                dtMulai.Enabled = false;
                dtSelesai.Enabled = false;
                cbAktif.Enabled = false;
                btnTambah.Enabled = true;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                txtTahun.Enabled = true;
                txtSemester.Enabled = true;
                dtMulai.Enabled = true;
                dtSelesai.Enabled = true;
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
            dg.DataSource = new SortableBindingList<TahunAjaran>(db.TahunAjaran.Where(mb => mb.TahunAjaran1.ToLower().Contains(cari)).ToList());
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is TahunAjaran r)
            {
                var tahunAjaran = db.TahunAjaran.Find(r.IdTahunAjaran);
                if (tahunAjaran != null)
                {
                    bindingSource1.DataSource = tahunAjaran;
                    dtMulai.Value = tahunAjaran.TanggalMulai ?? DateTime.Now;
                    dtSelesai.Value = tahunAjaran.TanggalSelesai ?? DateTime.Now;

                    if (tahunAjaran.IsActive == true) cbAktif.Checked = true;
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
            if (bindingSource1.Current is TahunAjaran)
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
            if (string.IsNullOrEmpty(txtTahun.Text))
            {
                MessageBox.Show("Tahun Ajaran harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtSemester.Text))
            {
                MessageBox.Show("Semester harus diisi.");
                return;
            }


            if (bindingSource1.Current is TahunAjaran k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdTahunAjaran == 0)
                    {
                        var baru = new TahunAjaran
                        {
                            TahunAjaran1 = txtTahun.Text,
                            Semester = txtSemester.Text,
                            TanggalMulai = dtMulai.Value.Date,
                            TanggalSelesai = dtSelesai.Value.Date,
                            IsActive = cbAktif.Checked
                        };
                        db.TahunAjaran.Add(baru);
                    }
                    else
                    {
                        k.TahunAjaran1 = txtTahun.Text;
                        k.Semester = txtSemester.Text;
                        k.TanggalMulai = dtMulai.Value.Date;
                        k.TanggalSelesai = dtSelesai.Value.Date;
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
            if (bindingSource1.Current is TahunAjaran k && k.IdTahunAjaran != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.TahunAjaran1}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.TahunAjaran.Remove(k);
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

                                    try
                                    {
                                        string tahun = row[0]?.ToString().Trim();
                                        string semester = row[1]?.ToString().Trim().ToUpper();

                                        DateTime? mulai = null;
                                        if (row[2] != null && row[2] != DBNull.Value && !string.IsNullOrWhiteSpace(row[2].ToString()))
                                        {
                                            if (row[2] is DateTime dtMulai)
                                            {
                                                mulai = dtMulai;
                                            }
                                            else if (DateTime.TryParse(row[2].ToString(), out DateTime parsedMulai))
                                            {
                                                mulai = parsedMulai;
                                            }
                                        }

                                        DateTime? selesai = null;
                                        if (row[3] != null && row[3] != DBNull.Value && !string.IsNullOrWhiteSpace(row[3].ToString()))
                                        {
                                            if (row[3] is DateTime dtSelesai)
                                            {
                                                selesai = dtSelesai;
                                            }
                                            else if (DateTime.TryParse(row[3].ToString(), out DateTime parsedSelesai))
                                            {
                                                selesai = parsedSelesai;
                                            }
                                        }

                                        bool isActive = row[4]?.ToString().Trim().ToLower() == "aktif";

                                        if (!string.IsNullOrEmpty(tahun) && !string.IsNullOrEmpty(semester))
                                        {
                                            var tahunImpor = new TahunAjaran
                                            {
                                                TahunAjaran1 = tahun,
                                                Semester = semester,
                                                TanggalMulai = mulai, 
                                                TanggalSelesai = selesai,
                                                IsActive = isActive
                                            };

                                            db.TahunAjaran.Add(tahunImpor);
                                            sukses++;
                                        }
                                        else
                                        {
                                            gagal++; 
                                        }
                                    }
                                    catch
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
            if (dg.Rows[e.RowIndex].DataBoundItem is TahunAjaran r)
            {
                if (isActiveDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = r.IsActive == true ? "Aktif" : "Tidak Aktif";
            }
        }
    }
}
