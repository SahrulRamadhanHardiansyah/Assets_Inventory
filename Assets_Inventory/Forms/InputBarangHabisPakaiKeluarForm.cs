using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
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

namespace Assets_Inventory.Forms
{
    public partial class InputBarangHabisPakaiKeluarForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        public BarangKeluar selectedKeluar;
        private int currentUserId;
        public bool isDetailMode = false;

        public InputBarangHabisPakaiKeluarForm()
        {
            InitializeComponent();
        }

        private void InputBarangHabisPakaiKeluarForm_Load(object sender, EventArgs e)
        {
            currentUserId = Properties.Settings.Default.userId;

            SetupComboBoxes();
            SetupAutocomplete();

            if (selectedKeluar == null)
            {
                txtNoTransaksi.Text = GenerateNoTransaksi().ToString();
                dtpTanggalKeluar.Value = DateTime.Now;
                LoadDataPetugas(currentUserId);
            }
            else
            {
                LoadDataToForm();
            }
        }

        private int GenerateNoTransaksi()
        {
            int maxId = db.BarangKeluar.Select(b => (int?)b.NoTransaksi).Max() ?? 0;
            return maxId + 1;
        }

        private void LoadDataPetugas(int userId)
        {
            var petugas = db.Pengguna.Find(userId);
            txtNamaPetugas.Text = petugas != null ? petugas.Username : "Sistem";
        }

        private void SetupComboBoxes()
        {
            cmbRuang.DataSource = db.Ruang.ToList();
            cmbRuang.DisplayMember = "NamaRuang";
            cmbRuang.ValueMember = "IdRuang";
            cmbRuang.SelectedIndex = -1;

            cmbGudang.DataSource = db.Gudang.ToList();
            cmbGudang.DisplayMember = "NamaGudang";
            cmbGudang.ValueMember = "KodeGudang";
            cmbGudang.SelectedIndex = -1;
        }

        private void SetupAutocomplete()
        {
            var listPengguna = db.Pengguna.AsNoTracking().ToList();
            AutoCompleteStringCollection colPenerima = new AutoCompleteStringCollection();
            foreach (var p in listPengguna)
            {
                colPenerima.Add(p.Username);
            }

            txtNamaPenerima.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNamaPenerima.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNamaPenerima.AutoCompleteCustomSource = colPenerima;

            var dictBarang = db.MasterBarang.AsNoTracking().ToDictionary(m => m.IdMasterBarang, m => m.NamaBarang);

            var listAset = db.AsetHabisPakai.AsNoTracking()
                .Where(a => !string.IsNullOrEmpty(a.KodeBarang) && a.StokAktual > 0)
                .ToList();

            AutoCompleteStringCollection colBarang = new AutoCompleteStringCollection();
            foreach (var a in listAset)
            {
                string namaBrg = dictBarang.ContainsKey(a.IdMasterBarang) ? dictBarang[a.IdMasterBarang] : "N/A";
                string formatText = $"{namaBrg} - [{a.KodeBarang}] (Sisa: {a.StokAktual})";
                colBarang.Add(formatText);
            }

            txtNamaBarang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNamaBarang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNamaBarang.AutoCompleteCustomSource = colBarang;
        }

        private void ExtractKodeBarang()
        {
            string text = txtNamaBarang.Text;
            if (text.Contains("[") && text.Contains("]"))
            {
                int start = text.IndexOf('[') + 1;
                int len = text.IndexOf(']') - start;
                txtKodeBarang.Text = text.Substring(start, len).Trim();
            }
        }

        private void txtNamaBarang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ExtractKodeBarang();
        }

        private void txtNamaBarang_Leave(object sender, EventArgs e)
        {
            ExtractKodeBarang();
        }

        private void LoadDataToForm()
        {
            txtNoTransaksi.Text = selectedKeluar.NoTransaksi.ToString();
            dtpTanggalKeluar.Value = selectedKeluar.TanggalKeluar;
            txtKodeBarang.Text = selectedKeluar.KodeBarang;
            txtJumlahKeluar.Text = selectedKeluar.JumlahKeluar.ToString(); 

            var aset = db.AsetHabisPakai.Include(a => a.IdMasterBarangNavigation).FirstOrDefault(a => a.KodeBarang == selectedKeluar.KodeBarang);
            if (aset != null) txtNamaBarang.Text = $"{aset.IdMasterBarangNavigation?.NamaBarang} - [{aset.KodeBarang}]";

            cmbRuang.SelectedValue = selectedKeluar.IdRuang ?? -1;
            cmbGudang.SelectedValue = selectedKeluar.KodeGudang ?? "";

            var penerima = db.Pengguna.Find(selectedKeluar.NamaPenerima);
            txtNamaPenerima.Text = penerima != null ? penerima.Username : "";

            var petugas = db.Pengguna.Find(selectedKeluar.Petugas);
            txtNamaPetugas.Text = petugas != null ? petugas.Username : "";

            txtKeterangan.Text = selectedKeluar.Keterangan;

            if (isDetailMode)
            {
                txtNamaBarang.ReadOnly = true;
                txtNamaPenerima.ReadOnly = true;
                txtKeterangan.ReadOnly = true;
                txtJumlahKeluar.ReadOnly = true;
                dtpTanggalKeluar.Enabled = false;
                cmbRuang.Enabled = false;
                cmbGudang.Enabled = false;
                btnSimpan.Visible = false;
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKodeBarang.Text))
            {
                MessageBox.Show("Silakan pilih barang dari saran Autocomplete yang muncul agar Kode Barang terisi otomatis.");
                return;
            }

            var penerimaDb = db.Pengguna.FirstOrDefault(p => p.Username == txtNamaPenerima.Text.Trim());
            if (penerimaDb == null)
            {
                MessageBox.Show("Nama Penerima tidak valid. Pastikan memilih dari daftar pengguna yang ada di sistem.");
                return;
            }

            if (!int.TryParse(txtJumlahKeluar.Text, out int jumlahKeluarDiinginkan) || jumlahKeluarDiinginkan <= 0)
            {
                MessageBox.Show("Masukkan jumlah pengeluaran barang yang valid (Angka lebih dari 0).");
                return;
            }

            try
            {
                if (selectedKeluar == null)
                {
                    var asetDb = db.AsetHabisPakai.FirstOrDefault(a => a.KodeBarang == txtKodeBarang.Text);
                    if (asetDb == null)
                    {
                        MessageBox.Show("Aset tidak ditemukan dalam stok database.");
                        return;
                    }

                    if (asetDb.StokAktual < jumlahKeluarDiinginkan)
                    {
                        MessageBox.Show($"Stok tidak mencukupi! Sisa stok saat ini hanya: {asetDb.StokAktual}");
                        return;
                    }

                    var baru = new BarangKeluar
                    {
                        NoTransaksi = GenerateNoTransaksi(),
                        TanggalKeluar = dtpTanggalKeluar.Value,
                        KodeBarang = txtKodeBarang.Text,
                        JumlahKeluar = jumlahKeluarDiinginkan,
                        Keterangan = txtKeterangan.Text,
                        IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null,
                        KodeGudang = cmbGudang.SelectedIndex != -1 ? cmbGudang.SelectedValue.ToString() : null,
                        NamaPenerima = penerimaDb.IdPengguna,
                        Petugas = currentUserId
                    };
                    db.BarangKeluar.Add(baru);

                    asetDb.StokAktual -= jumlahKeluarDiinginkan;
                    if (asetDb.StokAktual <= 0) asetDb.Status = "Habis";
                }
                else
                {
                    var update = db.BarangKeluar.Find(selectedKeluar.NoTransaksi);
                    if (update != null)
                    {
                        var asetLama = db.AsetHabisPakai.FirstOrDefault(a => a.KodeBarang == update.KodeBarang);
                        if (asetLama != null)
                        {
                            asetLama.StokAktual += update.JumlahKeluar;
                            if (asetLama.StokAktual > 0) asetLama.Status = "Tersedia";
                        }

                        var asetBaru = db.AsetHabisPakai.FirstOrDefault(a => a.KodeBarang == txtKodeBarang.Text);
                        if (asetBaru == null)
                        {
                            MessageBox.Show("Aset tujuan ubah tidak ditemukan di database.");
                            return;
                        }

                        if (asetBaru.StokAktual < jumlahKeluarDiinginkan)
                        {
                            MessageBox.Show($"Stok pengganti tidak mencukupi! Sisa stok aset tujuan hanya: {asetBaru.StokAktual}");
                            return;
                        }

                        update.TanggalKeluar = dtpTanggalKeluar.Value;
                        update.KodeBarang = txtKodeBarang.Text;
                        update.JumlahKeluar = jumlahKeluarDiinginkan; 
                        update.Keterangan = txtKeterangan.Text;
                        update.IdRuang = cmbRuang.SelectedIndex != -1 ? (int?)cmbRuang.SelectedValue : null;
                        update.KodeGudang = cmbGudang.SelectedIndex != -1 ? cmbGudang.SelectedValue.ToString() : null;
                        update.NamaPenerima = penerimaDb.IdPengguna;
                        update.Petugas = currentUserId;

                        asetBaru.StokAktual -= jumlahKeluarDiinginkan;
                        if (asetBaru.StokAktual <= 0) asetBaru.Status = "Habis";
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Transaksi Barang Keluar berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}