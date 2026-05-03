using ComponentFactory.Krypton.Toolkit;
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
    public partial class MasterSatuanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private Client apiClient;

        public MasterSatuanForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
        }

        private void MasterSatuanForm_Load(object sender, EventArgs e)
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

        private async void loadDgv()
        {
            dg.DataSource = (await apiClient.IndexSatuanAsync()).Data.ToList();
            dg.Columns["Id_satuan"].HeaderText = "ID";
            dg.Columns["Nama_satuan"].HeaderText = "Nama Satuan";
            dg.Columns["AdditionalProperties"].Visible = false;
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is SatuanResource satuan)
            {
                bindingSource1.DataSource = satuan;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is SatuanResource k)
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
            if (string.IsNullOrEmpty(txtNama.Text))
            {
                MessageBox.Show("Nama satuan harus diisi.");
                return;
            }

            if (bindingSource1.Current is SatuanResource k)
            {
                bindingSource1.EndEdit();

                k.Nama_satuan = txtNama.Text;
                k.Keterangan = txtKeterangan.Text;

                try
                {
                    if (string.IsNullOrEmpty(txtKode.Text) || txtKode.Text == "0")
                    {
                        await apiClient.StoreSatuanAsync(new StoreSatuanRequest
                        {
                            Nama_satuan = k.Nama_satuan,
                            Keterangan = k.Keterangan
                        });
                    }
                    else
                    {
                        await apiClient.UpdateSatuanAsync(k.Id_satuan, new UpdateSatuanRequest
                        {
                            Nama_satuan = k.Nama_satuan,
                            Keterangan = k.Keterangan
                        });
                    }

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgv();
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
            if (bindingSource1.Current is SatuanResource k)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.Nama_satuan}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        await apiClient.DestroySatuanAsync(k.Id_satuan);
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
                    loadDgv();
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
            loadDgv();
            SetMode("View");
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                ofd.Title = "Pilih File Excel Satuan";

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
                                    string keterangan = row[1]?.ToString().Trim();

                                    if (!string.IsNullOrEmpty(nama))
                                    {
                                        try
                                        {
                                            await apiClient.StoreSatuanAsync(new StoreSatuanRequest
                                            {
                                                Nama_satuan = nama,
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
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
