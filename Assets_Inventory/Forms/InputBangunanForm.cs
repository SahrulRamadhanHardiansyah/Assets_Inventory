using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class InputBangunanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public InputBangunanForm()
        {
            InitializeComponent();
        }

        private void InputBangunanForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Input Bangunan");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnUbah.Enabled = hakAkses.HakUbah;
            btnSimpan.Enabled = hakAkses.HakBuat || hakAkses.HakUbah;
            btnHapus.Enabled = hakAkses.HakHapus;
            btnImport.Enabled = hakAkses.HakBuat;

            loadDgv();
            loadComboBox();
            SetMode("View");
        }

        private void loadDgv()
        {
            var cari = txtCari.Text.ToLower().Trim();
            var data = db.AsetBangunan
                         .Include(b => b.IdKondisiNavigation)
                         .Where(a => a.NamaBangunan.ToLower().Contains(cari) || a.KodeBangunan.ToString().Contains(cari))
                         .ToList();

            dg.DataSource = new SortableBindingList<AsetBangunan>(data);
            lblRecord.Text = $"Total Record: {data.Count}";

            if (dg.Columns["KodeBangunan"] != null) dg.Columns["KodeBangunan"].HeaderText = "Kode Bangunan";
            if (dg.Columns["NamaBangunan"] != null) dg.Columns["NamaBangunan"].HeaderText = "Nama Bangunan";
            if (dg.Columns["LuasBangunan"] != null) dg.Columns["LuasBangunan"].HeaderText = "Luas Bangunan";
            if (dg.Columns["StatusBangunan"] != null) dg.Columns["StatusBangunan"].HeaderText = "Status Bangunan";
            if (dg.Columns["TanggalBangunan"] != null) dg.Columns["TanggalBangunan"].HeaderText = "Tanggal Pembangunan";
            if (dg.Columns["UkuranP"] != null) dg.Columns["UkuranP"].HeaderText = "Panjang";
            if (dg.Columns["UkuranL"] != null) dg.Columns["UkuranL"].HeaderText = "Lebar";

            if (dg.Columns["NilaiAset"] != null)
            {
                dg.Columns["NilaiAset"].HeaderText = "Nilai Aset";
                dg.Columns["NilaiAset"].DefaultCellStyle.Format = "C2";
                dg.Columns["NilaiAset"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dg.Columns["IdKondisi"] != null) dg.Columns["IdKondisi"].Visible = false;
            if (dg.Columns["IdKondisiNavigation"] != null) dg.Columns["IdKondisiNavigation"].HeaderText = "Kondisi Bangunan";
        }

        private void loadComboBox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Milik Sendiri");
            cmbStatus.Items.Add("Sewa");
            cmbStatus.Items.Add("Lainnya");
            cmbStatus.SelectedIndex = -1;

            cmbKondisi.DataSource = db.Kondisi.ToList();
            cmbKondisi.DisplayMember = "NamaKondisi";
            cmbKondisi.ValueMember = "IdKondisi";
            cmbKondisi.SelectedIndex = -1;
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtNama.Enabled = false;
                txtL.Enabled = false;
                txtP.Enabled = false;
                txtLuas.Enabled = false;
                txtNilai.Enabled = false;
                txtKonstruksi.Enabled = false;
                cmbStatus.Enabled = false;
                cmbKondisi.Enabled = false;
                txtKeterangan.Enabled = false;
                dtpTglPembangunan.Enabled = false;
                btnTambah.Enabled = true;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                txtNama.Enabled = true;
                txtL.Enabled = true;
                txtP.Enabled = true;
                txtLuas.Enabled = true;
                txtNilai.Enabled = true;
                txtKonstruksi.Enabled = true;
                cmbStatus.Enabled = true;
                cmbKondisi.Enabled = true;
                txtKeterangan.Enabled = true;
                dtpTglPembangunan.Enabled = true;
                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private void ClearInputs()
        {
            txtNama.Clear();
            txtLuas.Clear();
            txtP.Clear();
            txtL.Clear();
            txtNilai.Clear();
            txtKonstruksi.Clear();
            txtKeterangan.Clear();
            cmbStatus.SelectedIndex = -1;
            cmbKondisi.SelectedIndex = -1;
            dtpTglPembangunan.Value = DateTime.Now;
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
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is AsetBangunan t)
            {
                var bangunan = db.AsetBangunan.Find(t.KodeBangunan);
                if (bangunan != null)
                {
                    bindingSource1.DataSource = bangunan; 

                    txtNama.Text = bangunan.NamaBangunan;
                    txtLuas.Text = bangunan.LuasBangunan.ToString();
                    txtP.Text = bangunan.UkuranP?.ToString() ?? "";
                    txtL.Text = bangunan.UkuranL?.ToString() ?? "";
                    txtNilai.Text = bangunan.NilaiAset?.ToString("0") ?? "";
                    txtKonstruksi.Text = bangunan.Konstruksi;
                    txtKeterangan.Text = bangunan.Keterangan;

                    if (bangunan.TanggalBangunan.HasValue)
                        dtpTglPembangunan.Value = bangunan.TanggalBangunan.Value;
                    else
                        dtpTglPembangunan.Value = DateTime.Now;

                    if (!string.IsNullOrEmpty(bangunan.StatusBangunan))
                        cmbStatus.SelectedItem = bangunan.StatusBangunan;
                    else
                        cmbStatus.SelectedIndex = -1;

                    if (bangunan.IdKondisi.HasValue)
                        cmbKondisi.SelectedValue = bangunan.IdKondisi;
                    else
                        cmbKondisi.SelectedIndex = -1;
                }
            }
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dg.Rows[e.RowIndex].DataBoundItem == null) return;

            if (dg.Rows[e.RowIndex].DataBoundItem is AsetBangunan bangunan)
            {
                string colName = dg.Columns[e.ColumnIndex].Name;

                if (colName == "IdKondisiNavigation" || colName == "idKondisiNavigationDataGridViewTextBoxColumn")
                {
                    e.Value = bangunan.IdKondisiNavigation != null ? bangunan.IdKondisiNavigation.NamaKondisi : "";
                    e.FormattingApplied = true;
                }
                else if (colName == "NilaiAset" || colName == "nilaiAsetDataGridViewTextBoxColumn")
                {
                    e.Value = bangunan.NilaiAset.HasValue ? bangunan.NilaiAset.Value.ToString("C2") : "0";
                    e.FormattingApplied = true;
                }
            }
        }

        private void txtLuas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
            ClearInputs(); 
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is AsetBangunan k && k.KodeBangunan != 0)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Silakan pilih data dari tabel terlebih dahulu sebelum mengubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama Bangunan harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtLuas.Text, out int luasBangunan) || luasBangunan <= 0)
            {
                MessageBox.Show("Luas bangunan harus berupa angka dan lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtNilai.Text, out decimal hargaAset);
            decimal.TryParse(txtP.Text, out decimal panjang);
            decimal.TryParse(txtL.Text, out decimal lebar);

            if (bindingSource1.Current is AsetBangunan k)
            {
                bindingSource1.EndEdit();

                try
                {
                    if (k.KodeBangunan == 0)
                    {
                        var baru = new AsetBangunan
                        {
                            NamaBangunan = txtNama.Text,
                            LuasBangunan = luasBangunan,
                            StatusBangunan = cmbStatus.SelectedItem?.ToString(),
                            NilaiAset = hargaAset > 0 ? hargaAset : (decimal?)null,
                            Keterangan = txtKeterangan.Text,
                            TanggalBangunan = dtpTglPembangunan.Value.Date,
                            IdKondisi = cmbKondisi.SelectedValue != null ? (int?)cmbKondisi.SelectedValue : null,
                            UkuranP = panjang > 0 ? panjang : (decimal?)null,
                            UkuranL = lebar > 0 ? lebar : (decimal?)null,
                            Konstruksi = txtKonstruksi.Text
                        };

                        db.AsetBangunan.Add(baru);
                    }
                    else
                    {
                        var updateData = db.AsetBangunan.Find(k.KodeBangunan);
                        if (updateData != null)
                        {
                            updateData.NamaBangunan = txtNama.Text;
                            updateData.LuasBangunan = luasBangunan;
                            updateData.StatusBangunan = cmbStatus.SelectedItem?.ToString();
                            updateData.NilaiAset = hargaAset > 0 ? hargaAset : (decimal?)null;
                            updateData.Keterangan = txtKeterangan.Text;
                            updateData.TanggalBangunan = dtpTglPembangunan.Value.Date;
                            updateData.IdKondisi = cmbKondisi.SelectedValue != null ? (int?)cmbKondisi.SelectedValue : null;
                            updateData.UkuranP = panjang > 0 ? panjang : (decimal?)null;
                            updateData.UkuranL = lebar > 0 ? lebar : (decimal?)null;
                            updateData.Konstruksi = txtKonstruksi.Text;
                        }
                    }

                    db.SaveChanges();
                    MessageBox.Show("Data aset bangunan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgv();
                    ClearInputs();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            ClearInputs(); 
            loadDgv();
            loadComboBox();
            SetMode("View");
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is AsetBangunan k && k.KodeBangunan != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.NamaBangunan}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var toDelete = db.AsetBangunan.Find(k.KodeBangunan);
                        if (toDelete != null)
                        {
                            db.AsetBangunan.Remove(toDelete);
                            db.SaveChanges();

                            MessageBox.Show("Berhasil dihapus!");
                            loadDgv();
                            ClearInputs(); 
                            SetMode("View");
                        }
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
                MessageBox.Show("Pilih data yang valid dari tabel untuk dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Semua File Data|*.csv;*.xlsx;*.xls|Excel Files (*.xlsx, *.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv";
                ofd.Title = "Pilih File Data Aset Bangunan";

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
                                    int.TryParse(data[1].Trim(), out int luas);
                                    decimal.TryParse(data[3].Trim(), out decimal nilai);
                                    decimal.TryParse(data[5].Trim(), out decimal panjang);
                                    decimal.TryParse(data[6].Trim(), out decimal lebar);

                                    string statusInput = data[2].Trim();
                                    if (statusInput != "Milik Sendiri" && statusInput != "Sewa" && statusInput != "Lainnya")
                                        statusInput = "Lainnya";

                                    db.AsetBangunan.Add(new AsetBangunan
                                    {
                                        NamaBangunan = data[0].Trim(),
                                        LuasBangunan = luas > 0 ? luas : 0,
                                        StatusBangunan = statusInput,
                                        NilaiAset = nilai > 0 ? nilai : (decimal?)null,
                                        Keterangan = data[4].Trim(),
                                        UkuranP = panjang > 0 ? panjang : (decimal?)null,
                                        UkuranL = lebar > 0 ? lebar : (decimal?)null,
                                        Konstruksi = data[7].Trim(),
                                        TanggalBangunan = DateTime.Now.Date
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
                                            int.TryParse(row[1]?.ToString().Trim(), out int luas);
                                            decimal.TryParse(row[3]?.ToString().Trim(), out decimal nilai);
                                            decimal.TryParse(row[5]?.ToString().Trim(), out decimal panjang);
                                            decimal.TryParse(row[6]?.ToString().Trim(), out decimal lebar);

                                            string statusInput = row[2]?.ToString().Trim();
                                            if (statusInput != "Milik Sendiri" && statusInput != "Sewa" && statusInput != "Lainnya")
                                                statusInput = "Lainnya";

                                            db.AsetBangunan.Add(new AsetBangunan
                                            {
                                                NamaBangunan = row[0]?.ToString().Trim(),
                                                LuasBangunan = luas > 0 ? luas : 0,
                                                StatusBangunan = statusInput,
                                                NilaiAset = nilai > 0 ? nilai : (decimal?)null,
                                                Keterangan = row[4]?.ToString().Trim(),
                                                UkuranP = panjang > 0 ? panjang : (decimal?)null,
                                                UkuranL = lebar > 0 ? lebar : (decimal?)null,
                                                Konstruksi = row[7]?.ToString().Trim(),
                                                TanggalBangunan = DateTime.Now.Date
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
                        MessageBox.Show("File sedang dibuka oleh program lain (seperti Microsoft Excel). Silakan tutup file tersebut dan coba lagi.", "Error Akses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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