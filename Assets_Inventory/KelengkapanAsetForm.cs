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
        private string tempGambarPath = null;

        public KelengkapanAsetForm(List<Aset> asetBaru)
        {
            InitializeComponent();
            listAsetBaru = asetBaru;
        }

        private void KelengkapanAsetForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            LoadGrid();
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
        }

        private void LoadGrid()
        {
            dgAset.DataSource = listAsetBaru.Select(a => new
            {
                a.KodeInventaris,
                a.IdMasterBarangNavigation.NamaBarang,
                a.NoSeri,
                a.Status,
                a.Keterangan
            }).ToList();
        }

        private void dgAset_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var kodeInv = dgAset.Rows[e.RowIndex].Cells["KodeInventaris"].Value?.ToString();
                var aset = listAsetBaru.FirstOrDefault(x => x.KodeInventaris == kodeInv);

                if (aset != null)
                {
                    txtKodeInventaris.Text = aset.KodeInventaris;
                    txtNamaBarang.Text = aset.IdMasterBarangNavigation?.NamaBarang;
                    txtNoSeri.Text = aset.NoSeri;
                    txtUmurEkonomi.Text = aset.UmurEkonomi?.ToString();
                    txtNilaiResidu.Text = Convert.ToInt32(aset.NilaiResidu).ToString();
                    cmbRuang.SelectedValue = aset.IdRuang;
                    cmbLokasi.SelectedValue = aset.IdLokasi;
                    cmbStatus.SelectedItem = aset.Status;
                    txtKeterangan.Text = aset.Keterangan;

                    if (!string.IsNullOrEmpty(aset.Gambar) && File.Exists(aset.Gambar))
                    {
                        pbGambar.Image = Image.FromFile(aset.Gambar);
                        tempGambarPath = aset.Gambar;
                    }
                    else
                    {
                        pbGambar.Image = null;
                        tempGambarPath = null;
                    }
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
                    pbGambar.Image = Image.FromFile(ofd.FileName);
                    tempGambarPath = ofd.FileName;
                }
            }
        }

        private void btnHapusGambar_Click(object sender, EventArgs e)
        {
            pbGambar.Image = null;
            tempGambarPath = null;
        }

        private void btnSimpanAset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKodeInventaris.Text))
            {
                MessageBox.Show("Pilih aset dari grid terlebih dahulu.");
                return;
            }

            var aset = listAsetBaru.FirstOrDefault(a => a.KodeInventaris == txtKodeInventaris.Text);
            if (aset == null) return;

            aset.NoSeri = string.IsNullOrEmpty(txtNoSeri.Text) ? null : txtNoSeri.Text;
            aset.UmurEkonomi = int.TryParse(txtUmurEkonomi.Text, out int umur) ? (int?)umur : null;
            aset.NilaiResidu = decimal.TryParse(txtNilaiResidu.Text, out decimal residu) ? residu : 0;
            aset.IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null;
            aset.IdLokasi = cmbLokasi.SelectedIndex != -1 ? (int?)cmbLokasi.SelectedValue : null;
            aset.Status = cmbStatus.SelectedItem?.ToString() ?? "Di Gudang";
            aset.Keterangan = txtKeterangan.Text;

            if (!string.IsNullOrEmpty(tempGambarPath))
            {
                string folderPath = Path.Combine(Application.StartupPath, "GambarAset");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string fileName = $"{aset.KodeInventaris}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(tempGambarPath)}";
                string destPath = Path.Combine(folderPath, fileName);

                File.Copy(tempGambarPath, destPath, true);
                aset.Gambar = destPath;
            }

            var dbAset = db.Aset.Find(aset.KodeBarang);
            if (dbAset != null)
            {
                dbAset.NoSeri = aset.NoSeri;
                dbAset.UmurEkonomi = aset.UmurEkonomi;
                dbAset.NilaiResidu = aset.NilaiResidu;
                dbAset.IdRuang = aset.IdRuang;
                dbAset.IdLokasi = aset.IdLokasi;
                dbAset.Status = aset.Status;
                dbAset.Keterangan = aset.Keterangan;
                dbAset.Gambar = aset.Gambar;

                db.SaveChanges();
            }

            MessageBox.Show("Data aset berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGrid();
        }

        private void btnNantiSaja_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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
                    $"{belumLengkap} aset masih belum dilengkapi datanya.\n" +
                    "Data ini akan muncul di notifikasi dashboard.\n" +
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