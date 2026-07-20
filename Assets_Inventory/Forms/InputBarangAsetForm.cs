using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.Forms
{
    public partial class InputBarangAsetForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        public Aset selectedAset = null;
        private string base64Image = "";

        public InputBarangAsetForm()
        {
            InitializeComponent();
        }

        private void InputBarangAsetForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            SetupAutoComplete();

            cmbRuang.SelectedIndexChanged -= CmbRuang_SelectedIndexChanged;
            cmbRuang.SelectedIndexChanged += CmbRuang_SelectedIndexChanged;

            if (selectedAset == null)
            {
                this.Text = "Tambah Aset Baru Manual";
                cmbStatus.SelectedItem = "Aktif";

                string prefix = "INV";
                var pengaturan = db.Pengaturan.FirstOrDefault();
                if (pengaturan != null && !string.IsNullOrEmpty(pengaturan.KustomPrefix))
                {
                    prefix = pengaturan.KustomPrefix;
                }

                string tahun = DateTime.Now.Year.ToString();
                string timestamp = DateTime.Now.ToString("MMddHHmmss");

                txtKodeInventaris.Text = $"{prefix}-{tahun}-MNL-{timestamp}";
            }
            else
            {
                this.Text = "Ubah Data Aset";
                LoadDataExisting();
            }
        }
        private void SetupAutoComplete()
        {
            var listBarang = db.MasterBarang.Select(b => b.NamaBarang).ToArray();

            var source = new AutoCompleteStringCollection();
            source.AddRange(listBarang);

            txtNamaBarang.AutoCompleteCustomSource = source;
            txtNamaBarang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNamaBarang.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void SetupComboBoxes()
        {
            cmbJurusan.DataSource = db.Jurusan.ToList();
            cmbJurusan.DisplayMember = "NamaJurusan";
            cmbJurusan.ValueMember = "IdJurusan";
            cmbJurusan.SelectedIndex = -1;

            cmbRuang.DataSource = db.Ruang.ToList();
            cmbRuang.DisplayMember = "NamaRuang";
            cmbRuang.ValueMember = "IdRuang";
            cmbRuang.SelectedIndex = -1;

            cmbLokasi.DataSource = db.Lokasi.ToList();
            cmbLokasi.DisplayMember = "NamaLokasi";
            cmbLokasi.ValueMember = "IdLokasi";
            cmbLokasi.SelectedIndex = -1;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Aktif");
            cmbStatus.Items.Add("Di Gudang");
            cmbStatus.Items.Add("Rusak");
            cmbStatus.Items.Add("Hilang");
            cmbStatus.SelectedIndex = 0;

            cmbLemari.Enabled = false;
            txtNomorRak.Enabled = false;
        }

        private void CmbRuang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRuang.SelectedValue != null && cmbRuang.SelectedValue is int idRuangTepilih)
            {
                var listLemari = db.Lemari.Where(L => L.IdRuang == idRuangTepilih).ToList();

                if (listLemari.Count > 0)
                {
                    cmbLemari.DataSource = listLemari;
                    cmbLemari.DisplayMember = "Nama";
                    cmbLemari.ValueMember = "IdLemari";
                    cmbLemari.SelectedIndex = -1;

                    cmbLemari.Enabled = true;
                    txtNomorRak.Enabled = true;
                }
                else
                {
                    cmbLemari.DataSource = null;
                    cmbLemari.Enabled = false;
                    txtNomorRak.Enabled = false;
                    txtNomorRak.Clear();
                }
            }
            else
            {
                cmbLemari.DataSource = null;
                cmbLemari.Enabled = false;
                txtNomorRak.Enabled = false;
                txtNomorRak.Clear();
            }
        }

        private void LoadDataExisting()
        {
            txtKodeInventaris.Text = selectedAset.KodeInventaris;
            txtNoSeri.Text = selectedAset.NoSeri;
            txtUmurEkonomi.Text = selectedAset.UmurEkonomi?.ToString();
            txtNilaiResidu.Text = Convert.ToInt32(selectedAset.NilaiResidu).ToString();
            txtKeterangan.Text = selectedAset.Keterangan;

            var masterBarang = db.MasterBarang.Find(selectedAset.IdMasterBarang);
            if (masterBarang != null)
            {
                txtNamaBarang.Text = masterBarang.NamaBarang;
            }

            if (selectedAset.IdJurusan != null) cmbJurusan.SelectedValue = selectedAset.IdJurusan;

            if (selectedAset.IdRuang != null) cmbRuang.SelectedValue = selectedAset.IdRuang;
            if (selectedAset.IdLokasi != null) cmbLokasi.SelectedValue = selectedAset.IdLokasi;

            if (selectedAset.IdLemari != null && cmbLemari.Enabled) cmbLemari.SelectedValue = selectedAset.IdLemari;
            txtNomorRak.Text = selectedAset.NomorRak;

            cmbStatus.SelectedItem = !string.IsNullOrEmpty(selectedAset.Status) ? selectedAset.Status : "Aktif";

            if (!string.IsNullOrEmpty(selectedAset.Gambar))
            {
                try
                {
                    base64Image = selectedAset.Gambar;
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        pbGambar.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    pbGambar.Image = null;
                    base64Image = "";
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string namaInput = txtNamaBarang.Text.Trim();

            if (string.IsNullOrWhiteSpace(namaInput))
            {
                MessageBox.Show("Nama Barang wajib diisi!", "Validasi Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKodeInventaris.Text))
            {
                MessageBox.Show("Kode Inventaris wajib diisi!", "Validasi Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ponytail: validate TryParse return before use
            int umurEkonomi = 0;
            bool hasUmurEkonomi = false;
            if (!string.IsNullOrWhiteSpace(txtUmurEkonomi.Text))
            {
                if (!int.TryParse(txtUmurEkonomi.Text, out umurEkonomi))
                {
                    MessageBox.Show("Umur Ekonomi harus berupa angka valid!", "Validasi Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                hasUmurEkonomi = true;
            }

            decimal nilaiResidu = 0;
            if (!string.IsNullOrWhiteSpace(txtNilaiResidu.Text))
            {
                if (!decimal.TryParse(txtNilaiResidu.Text, out nilaiResidu))
                {
                    MessageBox.Show("Nilai Residu harus berupa angka valid!", "Validasi Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    var masterDb = db.MasterBarang.FirstOrDefault(m => m.NamaBarang.ToLower() == namaInput.ToLower());
                    int finalIdMaster;

                    if (masterDb == null)
                    {
                        var newMaster = new MasterBarang { NamaBarang = namaInput };
                        db.MasterBarang.Add(newMaster);
                        db.SaveChanges();
                        finalIdMaster = newMaster.IdMasterBarang;
                    }
                    else
                    {
                        finalIdMaster = masterDb.IdMasterBarang;
                    }

                    if (selectedAset == null)
                    {
                        Aset asetBaru = new Aset
                        {
                            IdMasterBarang = finalIdMaster,
                            KodeInventaris = txtKodeInventaris.Text.Trim(),
                            NoSeri = string.IsNullOrWhiteSpace(txtNoSeri.Text) ? null : txtNoSeri.Text.Trim(),
                            UmurEkonomi = hasUmurEkonomi ? (int?)umurEkonomi : null,
                            NilaiResidu = nilaiResidu,
                            IdJurusan = cmbJurusan.SelectedIndex != -1 ? (int?)cmbJurusan.SelectedValue : null,
                            IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null,
                            IdLokasi = cmbLokasi.SelectedIndex != -1 ? (int?)cmbLokasi.SelectedValue : null,
                            IdLemari = (cmbLemari.Enabled && cmbLemari.SelectedIndex != -1) ? (int?)cmbLemari.SelectedValue : null,
                            NomorRak = (txtNomorRak.Enabled && !string.IsNullOrWhiteSpace(txtNomorRak.Text)) ? txtNomorRak.Text.Trim() : null,
                            Status = cmbStatus.SelectedItem?.ToString() ?? "Aktif",
                            Keterangan = txtKeterangan.Text.Trim(),
                            Gambar = base64Image,
                            TanggalRegistrasi = DateTime.Now,
                            KodeBarang2 = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper()
                        };

                        db.Aset.Add(asetBaru);
                        db.SaveChanges();
                    }
                    else
                    {
                        var existingAset = db.Aset.Find(selectedAset.KodeBarang);
                        if (existingAset != null)
                        {
                            existingAset.IdMasterBarang = finalIdMaster;
                            existingAset.KodeInventaris = txtKodeInventaris.Text.Trim();
                            existingAset.NoSeri = string.IsNullOrWhiteSpace(txtNoSeri.Text) ? null : txtNoSeri.Text.Trim();
                            existingAset.UmurEkonomi = hasUmurEkonomi ? (int?)umurEkonomi : null;
                            existingAset.NilaiResidu = nilaiResidu;
                            existingAset.IdJurusan = cmbJurusan.SelectedIndex != -1 ? (int?)cmbJurusan.SelectedValue : null;
                            existingAset.IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null;
                            existingAset.IdLokasi = cmbLokasi.SelectedIndex != -1 ? (int?)cmbLokasi.SelectedValue : null;
                            existingAset.IdLemari = (cmbLemari.Enabled && cmbLemari.SelectedIndex != -1) ? (int?)cmbLemari.SelectedValue : null;
                            existingAset.NomorRak = (txtNomorRak.Enabled && !string.IsNullOrWhiteSpace(txtNomorRak.Text)) ? txtNomorRak.Text.Trim() : null;
                            existingAset.Status = cmbStatus.SelectedItem?.ToString() ?? "Aktif";
                            existingAset.Keterangan = txtKeterangan.Text.Trim();
                            existingAset.Gambar = base64Image;

                            db.SaveChanges();
                        }
                    }

                    tx.Commit();
                    MessageBox.Show(selectedAset == null ? "Aset manual berhasil ditambahkan!" : "Data Aset berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    MessageBox.Show("Terjadi kesalahan sistem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Pilih Gambar Aset";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Image originalImg = Image.FromFile(ofd.FileName);

                    int maxWidth = 600;
                    int maxHeight = 600;
                    int newWidth = originalImg.Width;
                    int newHeight = originalImg.Height;

                    if (originalImg.Width > maxWidth || originalImg.Height > maxHeight)
                    {
                        double ratioX = (double)maxWidth / originalImg.Width;
                        double ratioY = (double)maxHeight / originalImg.Height;
                        double ratio = Math.Min(ratioX, ratioY);
                        newWidth = (int)(originalImg.Width * ratio);
                        newHeight = (int)(originalImg.Height * ratio);
                    }

                    Bitmap resizedImg = new Bitmap(newWidth, newHeight);
                    using (Graphics g = Graphics.FromImage(resizedImg))
                    {
                        g.DrawImage(originalImg, 0, 0, newWidth, newHeight);
                    }

                    pbGambar.Image = resizedImg;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        resizedImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] imageBytes = ms.ToArray();
                        base64Image = Convert.ToBase64String(imageBytes);
                    }
                }
            }
        }

        private void btnHapusGambar_Click(object sender, EventArgs e)
        {
            pbGambar.Image = null;
            base64Image = "";
        }
    }
}