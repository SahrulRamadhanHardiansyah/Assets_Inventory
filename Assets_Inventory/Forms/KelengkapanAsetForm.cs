using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class KelengkapanAsetForm : KryptonForm
    {
        private AppDbContext db = new AppDbContext();
        private List<Aset> listAsetBaru;
        private string base64Image = "";
        private int currentKodeBarang = 0;

        public KelengkapanAsetForm(List<Aset> asetBaru)
        {
            InitializeComponent();
            listAsetBaru = asetBaru;
        }

        private void KelengkapanAsetForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();

            cmbRuang.SelectedIndexChanged -= CmbRuang_SelectedIndexChanged;
            cmbRuang.SelectedIndexChanged += CmbRuang_SelectedIndexChanged;

            if (listAsetBaru != null && listAsetBaru.Count > 0)
            {
                var listKode = listAsetBaru.Select(a => a.KodeBarang).ToList();
                listAsetBaru = db.Aset.Where(a => listKode.Contains(a.KodeBarang)).ToList();

                foreach (var aset in listAsetBaru)
                {
                    if (aset.IdMasterBarangNavigation == null)
                    {
                        aset.IdMasterBarangNavigation = db.MasterBarang.Find(aset.IdMasterBarang);
                    }
                }
            }

            LoadGrid();

            if (listAsetBaru != null && listAsetBaru.Count > 0)
            {
                TampilkanDetailAset(listAsetBaru[0].KodeBarang);
            }
        }

        private void SetupComboBoxes()
        {
            cmbRuang.DataSource = db.Ruang.ToList();
            cmbRuang.DisplayMember = "NamaRuang";
            cmbRuang.ValueMember = "IdRuang";
            cmbRuang.SelectedIndex = -1;

            cmbLokasi.DataSource = db.Lokasi.ToList();
            cmbLokasi.DisplayMember = "NamaLokasi";
            cmbLokasi.ValueMember = "IdLokasi";
            cmbLokasi.SelectedIndex = -1;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Di Gudang");
            cmbStatus.Items.Add("Aktif");
            cmbStatus.Items.Add("Dipinjam");
            cmbStatus.Items.Add("Nonaktif");
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

        private void LoadGrid()
        {
            dgAset.DataSource = listAsetBaru.Select(a => new
            {
                a.KodeBarang,
                a.KodeInventaris,
                NamaBarang = a.IdMasterBarangNavigation != null ? a.IdMasterBarangNavigation.NamaBarang : "N/A",
                a.NoSeri,
                a.Status,
                a.Keterangan
            }).ToList();

            if (dgAset.Columns["KodeBarang"] != null) dgAset.Columns["KodeBarang"].Visible = false;

            if (dgAset.Columns["KodeInventaris"] != null) dgAset.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
            if (dgAset.Columns["NamaBarang"] != null) dgAset.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dgAset.Columns["NoSeri"] != null) dgAset.Columns["NoSeri"].HeaderText = "No Seri";
        }

        private void TampilkanDetailAset(int kodeBarang)
        {
            var aset = listAsetBaru.FirstOrDefault(x => x.KodeBarang == kodeBarang);
            if (aset != null)
            {
                currentKodeBarang = aset.KodeBarang;

                txtKodeInventaris.Text = aset.KodeInventaris;
                txtNamaBarang.Text = aset.IdMasterBarangNavigation?.NamaBarang ?? "N/A";
                txtNoSeri.Text = aset.NoSeri;
                txtUmurEkonomi.Text = aset.UmurEkonomi?.ToString();
                txtNilaiResidu.Text = Convert.ToInt32(aset.NilaiResidu).ToString();

                if (aset.IdRuang != null) cmbRuang.SelectedValue = aset.IdRuang;
                else cmbRuang.SelectedIndex = -1;

                if (aset.IdLokasi != null) cmbLokasi.SelectedValue = aset.IdLokasi;
                else cmbLokasi.SelectedIndex = -1;

                if (aset.IdLemari != null && cmbLemari.Enabled) cmbLemari.SelectedValue = aset.IdLemari;
                else cmbLemari.SelectedIndex = -1;

                txtNomorRak.Text = aset.NomorRak;
                cmbStatus.SelectedItem = !string.IsNullOrEmpty(aset.Status) ? aset.Status : "Di Gudang";
                txtKeterangan.Text = aset.Keterangan;

                if (!string.IsNullOrEmpty(aset.Gambar))
                {
                    try
                    {
                        base64Image = aset.Gambar;
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
                else
                {
                    pbGambar.Image = null;
                    base64Image = "";
                }
            }
        }

        private void dgAset_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (int.TryParse(dgAset.Rows[e.RowIndex].Cells["KodeBarang"].Value?.ToString(), out int kode))
                {
                    TampilkanDetailAset(kode);
                }
            }
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

        private void btnSimpanAset_Click(object sender, EventArgs e)
        {
            if (currentKodeBarang <= 0)
            {
                MessageBox.Show("Pilih aset dari grid terlebih dahulu.");
                return;
            }

            var aset = listAsetBaru.FirstOrDefault(a => a.KodeBarang == currentKodeBarang);
            if (aset == null) return;

            aset.NoSeri = string.IsNullOrEmpty(txtNoSeri.Text) ? null : txtNoSeri.Text;
            aset.UmurEkonomi = int.TryParse(txtUmurEkonomi.Text, out int umur) ? (int?)umur : null;
            aset.NilaiResidu = decimal.TryParse(txtNilaiResidu.Text, out decimal residu) ? residu : 0;

            aset.IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null;
            aset.IdLokasi = cmbLokasi.SelectedIndex != -1 ? (int?)cmbLokasi.SelectedValue : null;

            aset.IdLemari = (cmbLemari.Enabled && cmbLemari.SelectedIndex != -1) ? (int?)cmbLemari.SelectedValue : null;
            aset.NomorRak = (txtNomorRak.Enabled && !string.IsNullOrWhiteSpace(txtNomorRak.Text)) ? txtNomorRak.Text : null;

            aset.Status = cmbStatus.SelectedItem?.ToString() ?? "Di Gudang";
            aset.Keterangan = txtKeterangan.Text;
            aset.Gambar = base64Image;

            var dbAset = db.Aset.Find(aset.KodeBarang);
            if (dbAset != null)
            {
                dbAset.NoSeri = aset.NoSeri;
                dbAset.UmurEkonomi = aset.UmurEkonomi;
                dbAset.NilaiResidu = aset.NilaiResidu;
                dbAset.IdRuang = aset.IdRuang;
                dbAset.IdLokasi = aset.IdLokasi;
                dbAset.IdLemari = aset.IdLemari;
                dbAset.NomorRak = aset.NomorRak;
                dbAset.Status = aset.Status;
                dbAset.Keterangan = aset.Keterangan;
                dbAset.Gambar = aset.Gambar;

                db.SaveChanges();
            }

            MessageBox.Show("Data kelengkapan aset berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var listKode = listAsetBaru.Select(a => a.KodeBarang).ToList();
            listAsetBaru = db.Aset.Where(a => listKode.Contains(a.KodeBarang)).ToList();
            foreach (var a in listAsetBaru)
            {
                if (a.IdMasterBarangNavigation == null)
                    a.IdMasterBarangNavigation = db.MasterBarang.Find(a.IdMasterBarang);
            }

            LoadGrid();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            var belumLengkap = listAsetBaru.Count(a =>
                string.IsNullOrEmpty(a.NoSeri) &&
                a.IdRuang == null &&
                string.IsNullOrEmpty(a.Gambar)
            );

            if (belumLengkap > 0)
            {
                if (MessageBox.Show(
                    $"{belumLengkap} aset masih belum dilengkapi datanya secara penuh.\n" +
                    "Anda tetap dapat melengkapinya nanti melalui menu Master Aset.\n" +
                    "Lanjutkan menutup form?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}