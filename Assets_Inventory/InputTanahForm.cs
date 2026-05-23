using Assets_Inventory.Models;
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
    public partial class InputTanahForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public InputTanahForm()
        {
            InitializeComponent();
        }

        private void InputTanahForm_Load(object sender, EventArgs e)
        {
            loadDgv();
            loadComboBox();
            SetMode("View");
        }

        private void loadDgv()
        {
            var cari = txtCari.Text.ToLower().Trim();
            var data = db.AsetTanah.Where(a => a.NamaPemilik.ToLower().Contains(cari) || a.KodeTanah.ToString().Contains(cari)).ToList();
            dg.DataSource = data;
            lblRecord.Text = $"Total Record: {data.Count}";
        }

        private void loadComboBox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Hak Milik");
            cmbStatus.Items.Add("Hak Pakai");
            cmbStatus.Items.Add("Hak Guna Bangunan");
            cmbStatus.SelectedIndex = -1;

            cmbSumber.Items.Clear();
            cmbSumber.Items.Add("Beli");
            cmbSumber.Items.Add("Hibah");
            cmbSumber.Items.Add("Sumbangan");
            cmbSumber.Items.Add("Lainnya");
            cmbSumber.SelectedIndex = -1;
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtNama.Enabled = false;
                txtNoSertif.Enabled = false;
                txtLuas.Enabled = false;
                txtLetakTanah.Enabled = false;
                txtHarga.Enabled = false;
                cmbStatus.Enabled = false;
                cmbSumber.Enabled = false;
                txtPenggunaan.Enabled = false;
                dtpTglPerolehan.Enabled = false;
                btnTambah.Enabled = true;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                txtNama.Enabled = true;
                txtNoSertif.Enabled = true;
                txtLuas.Enabled = true;
                txtLetakTanah.Enabled = true;
                txtHarga.Enabled = true;
                cmbStatus.Enabled = true;
                cmbSumber.Enabled = true;
                txtPenggunaan.Enabled = true;
                dtpTglPerolehan.Enabled = true;
                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadDgv();
        }

        private void btynRefresh_Click(object sender, EventArgs e)
        {
            txtCari.Text = "";
            loadDgv();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is AsetTanah t)
            {
                var tanah = db.AsetTanah.Find(t.KodeTanah);
                if (tanah != null)
                {
                    bindingSource1.DataSource = tanah;
                }
            }
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is AsetTanah tanah)
            {
                if (nilaiAsetDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = tanah.NilaiAset.HasValue ? tanah.NilaiAset.Value.ToString("C2") : "0";
            }
        }

        private void txtLuas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtHarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
            cmbStatus.SelectedIndex = -1;
            cmbSumber.SelectedIndex = -1;
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is AsetTanah)
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
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama Pemilik harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtLuas.Text, out int luasTanah) || luasTanah <= 0)
            {
                MessageBox.Show("Luas tanah harus berupa angka dan lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtHarga.Text, out decimal hargaAset);

            if (bindingSource1.Current is AsetTanah k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.KodeTanah == 0)
                    {
                        var baru = new AsetTanah
                        {
                            NamaPemilik = txtNama.Text,
                            NomorSertifikat = txtNoSertif.Text,
                            LuasTanah = luasTanah,
                            LetakTanah = txtLetakTanah.Text,
                            NilaiAset = hargaAset > 0 ? hargaAset : (decimal?)null,
                            StatusHak = cmbStatus.SelectedItem?.ToString(),
                            SumberPerolehan = cmbSumber.SelectedItem?.ToString(),
                            Penggunaan = txtPenggunaan.Text,
                            TanggalPerolehan = dtpTglPerolehan.Value.Date
                        };

                        db.AsetTanah.Add(baru);
                    }
                    else 
                    {
                        var updateData = db.AsetTanah.Find(k.KodeTanah);
                        if (updateData != null)
                        {
                            updateData.NamaPemilik = txtNama.Text;
                            updateData.NomorSertifikat = txtNoSertif.Text;
                            updateData.LuasTanah = luasTanah;
                            updateData.LetakTanah = txtLetakTanah.Text;
                            updateData.NilaiAset = hargaAset > 0 ? hargaAset : (decimal?)null;
                            updateData.StatusHak = cmbStatus.SelectedItem?.ToString();
                            updateData.SumberPerolehan = cmbSumber.SelectedItem?.ToString();
                            updateData.Penggunaan = txtPenggunaan.Text;
                            updateData.TanggalPerolehan = dtpTglPerolehan.Value.Date;
                        }
                    }

                    db.SaveChanges();
                    MessageBox.Show("Data aset tanah berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loadDgv();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            bindingSource1.AddNew();
            loadDgv();
            loadComboBox();
            SetMode("View");
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is AsetTanah k && k.KodeTanah != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.KodeTanah}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.AsetTanah.Remove(k);
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Semua File Data|*.csv;*.xlsx;*.xls|Excel Files (*.xlsx, *.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv";
                ofd.Title = "Pilih File Data Aset Tanah";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    try
                    {
                        int berhasil = 0;

                        if (fileExtension == ".csv")
                        {
                            var lines = File.ReadAllLines(filePath);
                            for (int i = 1; i < lines.Length; i++)
                            {
                                var data = lines[i].Split(',');

                                if (data.Length >= 8)
                                {
                                    int.TryParse(data[2].Trim(), out int luas);
                                    decimal.TryParse(data[7].Trim(), out decimal nilai);

                                    db.AsetTanah.Add(new AsetTanah
                                    {
                                        NamaPemilik = data[0].Trim(),
                                        NomorSertifikat = data[1].Trim(),
                                        LuasTanah = luas > 0 ? luas : 0,
                                        LetakTanah = data[3].Trim(),
                                        StatusHak = data[4].Trim(),
                                        Penggunaan = data[5].Trim(),
                                        SumberPerolehan = data[6].Trim(),
                                        NilaiAset = nilai > 0 ? nilai : (decimal?)null,
                                        TanggalPerolehan = DateTime.Now.Date
                                    });
                                    berhasil++;
                                }
                            }
                        }
                        else if (fileExtension == ".xlsx" || fileExtension == ".xls")
                        {
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                            {
                                using (var reader = ExcelReaderFactory.CreateReader(stream))
                                {
                                    var result = reader.AsDataSet();
                                    DataTable table = result.Tables[0];

                                    for (int i = 1; i < table.Rows.Count; i++)
                                    {
                                        var row = table.Rows[i];

                                        if (row.ItemArray.Length >= 8 && !string.IsNullOrWhiteSpace(row[0]?.ToString()))
                                        {
                                            int.TryParse(row[2]?.ToString().Trim(), out int luas);
                                            decimal.TryParse(row[7]?.ToString().Trim(), out decimal nilai);

                                            db.AsetTanah.Add(new AsetTanah
                                            {
                                                NamaPemilik = row[0]?.ToString().Trim(),
                                                NomorSertifikat = row[1]?.ToString().Trim(),
                                                LuasTanah = luas > 0 ? luas : 0,
                                                LetakTanah = row[3]?.ToString().Trim(),
                                                StatusHak = row[4]?.ToString().Trim(),
                                                Penggunaan = row[5]?.ToString().Trim(),
                                                SumberPerolehan = row[6]?.ToString().Trim(),
                                                NilaiAset = nilai > 0 ? nilai : (decimal?)null,
                                                TanggalPerolehan = DateTime.Now.Date
                                            });
                                            berhasil++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Format file tidak didukung!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        db.SaveChanges();
                        MessageBox.Show($"Import selesai! {berhasil} data berhasil ditambahkan dari file {fileExtension.ToUpper()}.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgv();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka oleh program lain. Tutup file tersebut dan coba lagi.", "Error Akses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
