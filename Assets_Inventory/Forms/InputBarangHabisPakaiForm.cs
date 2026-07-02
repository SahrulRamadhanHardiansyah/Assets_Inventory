using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory.Forms
{
    public partial class InputBarangHabisPakaiForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        public AsetHabisPakai selectedAset;

        public InputBarangHabisPakaiForm()
        {
            InitializeComponent();
        }

        private void InputBarangHabisPakaiForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            SetupAutoComplete();

            if (selectedAset == null)
            {
                txtKodeBarang.Text = "KB-HP-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();
                txtStatus.Text = "Tersedia";
                txtStok.Text = "0";
                txtStokAktual.Text = "0";
                dtpTanggalRegistrasi.Value = DateTime.Now;
                chkDapatDipinjam.Checked = false;
            }
            else
            {
                LoadDataToForm();
            }
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
        }

        private void SetupAutoComplete()
        {
            var listBarang = db.MasterBarang.ToList();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            foreach (var b in listBarang)
            {
                collection.Add(b.NamaBarang);
            }

            txtNamaBarang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNamaBarang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNamaBarang.AutoCompleteCustomSource = collection;
        }

        private void LoadDataToForm()
        {
            txtKodeBarang.Text = selectedAset.KodeBarang;

            var masterBarang = db.MasterBarang.Find(selectedAset.IdMasterBarang);
            txtNamaBarang.Text = masterBarang != null ? masterBarang.NamaBarang : "";

            txtStok.Text = selectedAset.Stok.ToString();
            txtStokAktual.Text = selectedAset.StokAktual.ToString();
            txtKeterangan.Text = selectedAset.Keterangan;

            cmbJurusan.SelectedValue = selectedAset.IdJurusan ?? -1;
            cmbRuang.SelectedValue = selectedAset.IdRuang ?? -1;
            cmbLokasi.SelectedValue = selectedAset.IdLokasi ?? -1;

            txtStatus.Text = selectedAset.Status;
            dtpTanggalRegistrasi.Value = selectedAset.TanggalRegistrasi;
            chkDapatDipinjam.Checked = selectedAset.IsReturnable ?? false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string namaInput = txtNamaBarang.Text.Trim();
            if (string.IsNullOrEmpty(namaInput))
            {
                MessageBox.Show("Nama Barang wajib diisi.");
                return;
            }

            var cekMaster = db.MasterBarang.FirstOrDefault(m => m.NamaBarang == namaInput);
            if (cekMaster == null)
            {
                MessageBox.Show("Nama Barang tidak ditemukan di data Master! Pastikan memilih dari saran Autocomplete yang muncul.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtStok.Text, out int stokAwal);
            int.TryParse(txtStokAktual.Text, out int stokAktual);

            string statusFinal = stokAktual > 0 ? "Tersedia" : "Habis";

            try
            {
                if (selectedAset == null)
                {
                    var asetBaru = new AsetHabisPakai
                    {
                        KodeBarang = txtKodeBarang.Text,
                        IdMasterBarang = cekMaster.IdMasterBarang,
                        Stok = stokAwal,
                        StokAktual = stokAktual,
                        Keterangan = txtKeterangan.Text,
                        IdJurusan = cmbJurusan.SelectedIndex != -1 ? (int?)cmbJurusan.SelectedValue : null,
                        IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null,
                        IdLokasi = cmbLokasi.SelectedIndex != -1 ? (int?)cmbLokasi.SelectedValue : null,
                        Status = statusFinal,
                        TanggalRegistrasi = dtpTanggalRegistrasi.Value,
                        IsReturnable = chkDapatDipinjam.Checked
                    };

                    db.AsetHabisPakai.Add(asetBaru);
                }
                else
                {
                    var asetUpdate = db.AsetHabisPakai.Find(selectedAset.KodeBarang);
                    if (asetUpdate != null)
                    {
                        asetUpdate.IdMasterBarang = cekMaster.IdMasterBarang;
                        asetUpdate.Stok = stokAwal;
                        asetUpdate.StokAktual = stokAktual;
                        asetUpdate.Keterangan = txtKeterangan.Text;
                        asetUpdate.IdJurusan = cmbJurusan.SelectedIndex != -1 ? (int?)cmbJurusan.SelectedValue : null;
                        asetUpdate.IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null;
                        asetUpdate.IdLokasi = cmbLokasi.SelectedIndex != -1 ? (int?)cmbLokasi.SelectedValue : null;
                        asetUpdate.TanggalRegistrasi = dtpTanggalRegistrasi.Value;
                        asetUpdate.IsReturnable = chkDapatDipinjam.Checked;
                        asetUpdate.Status = statusFinal;
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Data barang habis pakai berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtStok_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void txtStokAktual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}