using ComponentFactory.Krypton.Toolkit;
using ExcelDataReader;
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

namespace Assets_Inventory
{
    public partial class MasterBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private Client apiClient;

        public MasterBarangForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
        }

        private async void MasterBarangForm_Load(object sender, EventArgs e)
        {
            cmbKategori.DataSource = (await apiClient.IndexMasterBarangAsync()).Data.ToList();
            cmbKategori.SelectedIndex = -1;
            loadData();
            SetMode("View");
        }

        private async void loadData()
        {
            dg.DataSource = (await apiClient.IndexMasterBarangAsync()).Data
                .Select(b => new
                {
                    b.Id_master_barang,
                    b.Nama_barang,
                    b.Kategori.Nama_kategori,
                })
                .ToList();

            dg.Columns["Id_master_barang"].HeaderText = "ID";
            dg.Columns["Nama_barang"].HeaderText = "Nama Barang";
            dg.Columns["Nama_kategori"].HeaderText = "MasterBarang";
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
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is MasterBarangResource barang)
            {
                bindingSource1.DataSource = barang;
                cmbKategori.SelectedItem = barang.Kategori;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is MasterBarangResource k)
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

            if (bindingSource1.Current is MasterBarangResource k)
            {
                bindingSource1.EndEdit();

                k.Nama_barang = txtNama.Text;
                k.Id_kategori = (int)cmbKategori.SelectedValue;
                k.Keterangan = txtKeterangan.Text;

                try
                {
                    if (string.IsNullOrEmpty(txtKode.Text) || txtKode.Text == "0")
                    {
                        await apiClient.StoreMasterBarangAsync(new StoreMasterBarangRequest
                        {
                            Nama_barang = k.Nama_barang,
                            Id_kategori = (int)cmbKategori.SelectedValue,
                            Keterangan = k.Keterangan
                        });
                    }
                    else
                    {
                        await apiClient.UpdateMasterBarangAsync(k.Id_master_barang, new UpdateMasterBarangRequest
                        {
                            Nama_barang = k.Nama_barang,
                            Keterangan = k.Keterangan
                        });
                    }

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                    SetMode("View");
                }
                catch (Assets_Inventory.ApiException apiEx)
                {
                    MessageBox.Show("Gagal menyimpan data: " + apiEx.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is MasterBarangResource k)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.Nama_barang}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        await apiClient.DestroyMasterBarangAsync(k.Id_master_barang);
                    }
                    catch (Assets_Inventory.ApiException apiEx)
                    {
                        MessageBox.Show("Gagal menghapus data: " + apiEx.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Berhasil dihapus!");
                    loadData();
                    bindingSource1.AddNew();
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
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
                                    string nama = row[0]?.ToString().Trim();
                                    int IdKategori = int.Parse(row[1]?.ToString().Trim());
                                    string keterangan = row[2]?.ToString().Trim();

                                    if (!string.IsNullOrEmpty(nama))
                                    {
                                        try
                                        {
                                            await apiClient.StoreMasterBarangAsync(new StoreMasterBarangRequest
                                            {
                                                Nama_barang = nama,
                                                Id_kategori = IdKategori, 
                                                Keterangan = keterangan
                                            });
                                            sukses++;
                                        }
                                        catch
                                        {
                                            gagal++;
                                        }
                                    }
                                }
                            }
                        }

                        MessageBox.Show($"Proses Import Selesai!\n\nBerhasil: {sukses} data\nGagal: {gagal} data",
                                        "Info Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
