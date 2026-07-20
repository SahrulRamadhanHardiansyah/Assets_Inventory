using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Assets_Inventory
{
    public partial class MasterBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        public MasterBarangForm()
        {
            InitializeComponent();
        }

        private async void MasterBarangForm_Load(object sender, EventArgs e)
        {
            cmbKategori.DataSource = db.Kategori.ToList();
            cmbKategori.SelectedIndex = -1;
            loadData();
            SetMode("View");
        }

        private async void loadData()
        {
            var cari = txtCari.Text.Trim().ToLower();
            dg.DataSource = new SortableBindingList<MasterBarang>(db.MasterBarang.Where(mb => mb.NamaBarang.ToLower().Contains(cari) || mb.IdKategoriNavigation.NamaKategori.ToLower().Contains(cari)).ToList());
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

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is MasterBarang mb)
            {
                var barang = db.MasterBarang.Find(mb.IdMasterBarang);
                if (barang != null)
                {
                    bindingSource1.DataSource = barang;
                    cmbKategori.SelectedValue = barang.IdKategori ?? -1;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
            cmbKategori.SelectedIndex = -1;
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is MasterBarang)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private async void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNama.Text) || cmbKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Nama barang dan kategori harus diisi.");
                return;
            }

            if (bindingSource1.Current is MasterBarang k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.IdMasterBarang == 0) 
                    {
                        var baru = new MasterBarang
                        {
                            NamaBarang = txtNama.Text,
                            IdKategori = (int)cmbKategori.SelectedValue,
                            Keterangan = txtKeterangan.Text,
                            JenisBarang = "Aset"
                        };

                        db.MasterBarang.Add(baru);
                    }
                    else 
                    {
                        k.NamaBarang = txtNama.Text;
                        k.IdKategori = (int)cmbKategori.SelectedValue;
                        k.Keterangan = txtKeterangan.Text;
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + ex.InnerException?.Message ?? ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is MasterBarang k && k.IdMasterBarang != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaBarang}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.MasterBarang.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadData();
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
            loadData();
            SetMode("View");
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                ofd.Title = "Pilih File Excel MasterBarang";

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
                                    string strKategori = row[1]?.ToString().Trim();
                                    string keterangan = row[2]?.ToString().Trim();

                                    if (!string.IsNullOrEmpty(nama) && int.TryParse(strKategori, out int IdKategori))
                                    {
                                        var barangImpor = new MasterBarang
                                        {
                                            NamaBarang = nama,
                                            IdKategori = IdKategori,
                                            Keterangan = keterangan,
                                            JenisBarang = "Aset"
                                        };

                                        db.MasterBarang.Add(barangImpor);
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
                        loadData();
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

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is MasterBarang mb)
            {
                if (KategoriNavigation.Index == e.ColumnIndex) e.Value = mb.IdKategoriNavigation?.NamaKategori;
            }
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                sfd.Title = "Export Data Master Barang ke Excel";
                sfd.FileName = "Data_Master_Barang_" + DateTime.Now.ToString("yyyyMMdd");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;

                        var dataList = db.MasterBarang
                                         .Include(mb => mb.IdKategoriNavigation)
                                         .ToList();

                        using (var package = new OfficeOpenXml.ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Data Master Barang");

                            worksheet.Cells[1, 1].Value = "Nama Barang";
                            worksheet.Cells[1, 2].Value = "Kategori";
                            worksheet.Cells[1, 3].Value = "Keterangan";

                            using (var range = worksheet.Cells[1, 1, 1, 3])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            }

                            int baris = 2;
                            foreach (var item in dataList)
                            {
                                Application.DoEvents();

                                worksheet.Cells[baris, 1].Value = item.NamaBarang;
                                worksheet.Cells[baris, 2].Value = item.IdKategoriNavigation?.NamaKategori ?? "Tanpa Kategori";
                                worksheet.Cells[baris, 3].Value = item.Keterangan;

                                baris++;
                            }

                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                            FileInfo fi = new FileInfo(sfd.FileName);
                            package.SaveAs(fi);
                        }

                        MessageBox.Show("Data berhasil diekspor ke Excel!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat mengekspor data: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
