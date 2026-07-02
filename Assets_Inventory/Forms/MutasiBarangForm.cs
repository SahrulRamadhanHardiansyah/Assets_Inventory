using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
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

namespace Assets_Inventory
{
    public partial class MutasiBarangForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        bool hasEditRights = false; 

        public class AsetMutasiViewModel
        {
            public int KodeBarang { get; set; }
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string JurusanSaatIni { get; set; }
            public Aset ObjekAsli { get; set; }
        }

        public class HistoryMutasiViewModel
        {
            public int IdMutasi { get; set; }
            public DateTime Tanggal { get; set; }
            public string KodeInventaris { get; set; }
            public string NamaBarang { get; set; }
            public string JurusanAsal { get; set; }
            public string JurusanTujuan { get; set; }
            public string Alasan { get; set; }
            public string StatusPersetujuan { get; set; }
            public Mutasi ObjekAsli { get; set; }
        }

        public MutasiBarangForm()
        {
            InitializeComponent();
        }

        private void MutasiBarangForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Mutasi Barang");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            hasEditRights = hakAkses.HakUbah;
            btnTambah.Enabled = hakAkses.HakBuat;
            btnSimpan.Enabled = hakAkses.HakBuat;

            string peranUser = AuthManager.CurrentRole;

            if (peranUser == "Toolman")
            {
                btnApprove.Visible = true;
                btnApprove.Enabled = hasEditRights;
            }
            else
            {
                btnApprove.Visible = false;
                btnApprove.Enabled = false;
            }

            LoadComboBoxData();
            loadAset();
            loadMutasi();
            SetMode("View");
        }

        private void LoadComboBoxData()
        {
            cmbJurusanAsal.DataSource = db.Jurusan.ToList();
            cmbJurusanAsal.DisplayMember = "NamaJurusan";
            cmbJurusanAsal.ValueMember = "IdJurusan";
            cmbJurusanAsal.SelectedIndex = -1;

            cmbJurusanTujuan.DataSource = db.Jurusan.ToList();
            cmbJurusanTujuan.DisplayMember = "NamaJurusan";
            cmbJurusanTujuan.ValueMember = "IdJurusan";
            cmbJurusanTujuan.SelectedIndex = -1;
        }

        private void SetMode(string mode)
        {
            cmbJurusanAsal.Enabled = false;

            if (mode == "View")
            {
                dtTgl.Enabled = false;
                cmbJurusanTujuan.Enabled = false;
                txtAlasan.Enabled = false;

                btnTambah.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                dtTgl.Enabled = true;
                cmbJurusanTujuan.Enabled = true;
                txtAlasan.Enabled = true;

                btnTambah.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private void ClearInputs()
        {
            txtKodeInv.Clear();
            txtNama.Clear();
            txtAlasan.Clear();
            cmbJurusanAsal.SelectedIndex = -1;
            cmbJurusanTujuan.SelectedIndex = -1;
            dtTgl.Value = DateTime.Now;
        }

        private void loadAset()
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cari = txtCari.Text.ToLower().Trim();
            var dictBarang = db.MasterBarang.ToDictionary(b => b.IdMasterBarang, b => b.NamaBarang);
            var dictJurusan = db.Jurusan.ToDictionary(j => j.IdJurusan, j => j.NamaJurusan);

            var rawAset = db.Aset.AsNoTracking()
                             .Where(a => a.KodeInventaris != null
                                      && a.NoSeri != null
                                      && a.IdRuang != null
                                      && a.IdLokasi != null)
                             .ToList();

            var dataTampil = rawAset.Select(a => {
                int? jurAset = a.IdJurusan;

                return new AsetMutasiViewModel
                {
                    KodeBarang = a.KodeBarang,
                    KodeInventaris = a.KodeInventaris,
                    NamaBarang = dictBarang.ContainsKey(a.IdMasterBarang) ? dictBarang[a.IdMasterBarang] : "Tidak Diketahui",
                    JurusanSaatIni = (jurAset.HasValue && dictJurusan.ContainsKey(jurAset.Value)) ? dictJurusan[jurAset.Value] : "Gudang Utama",
                    ObjekAsli = a
                };
            })
            .Where(x => x.NamaBarang.ToLower().Contains(cari) || x.KodeInventaris.ToLower().Contains(cari))
            .ToList();

            dg.DataSource = new SortableBindingList<AsetMutasiViewModel>(dataTampil);

            if (dg.Columns["KodeBarang"] != null) dg.Columns["KodeBarang"].Visible = false;
            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
            if (dg.Columns["KodeInventaris"] != null) dg.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
            if (dg.Columns["NamaBarang"] != null) dg.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dg.Columns["JurusanSaatIni"] != null) dg.Columns["JurusanSaatIni"].HeaderText = "Jurusan Saat Ini";
        }

        private void loadMutasi()
        {
            var dictBarang = db.MasterBarang.ToDictionary(b => b.IdMasterBarang, b => b.NamaBarang);
            var dictJurusan = db.Jurusan.ToDictionary(j => j.IdJurusan, j => j.NamaJurusan);
            var rawAset = db.Aset.AsNoTracking().ToList();

            var rawMutasi = db.Mutasi.AsNoTracking().OrderByDescending(m => m.TanggalMutasi).ToList();

            var dataTampil = rawMutasi.Select(m => {
                var asetTerkait = rawAset.FirstOrDefault(a => a.KodeInventaris == m.KodeInventaris);

                string namaBrg = "Tidak Diketahui";
                if (asetTerkait != null && dictBarang.ContainsKey(asetTerkait.IdMasterBarang))
                {
                    namaBrg = dictBarang[asetTerkait.IdMasterBarang];
                }

                int? jurAsal = m.IdJurusanAsal;
                int? jurTujuan = m.IdJurusanTujuan;

                return new HistoryMutasiViewModel
                {
                    IdMutasi = m.IdMutasi,
                    Tanggal = m.TanggalMutasi,
                    KodeInventaris = m.KodeInventaris,
                    NamaBarang = namaBrg,
                    JurusanAsal = (jurAsal.HasValue && dictJurusan.ContainsKey(jurAsal.Value)) ? dictJurusan[jurAsal.Value] : "Gudang Utama",
                    JurusanTujuan = (jurTujuan.HasValue && dictJurusan.ContainsKey(jurTujuan.Value)) ? dictJurusan[jurTujuan.Value] : "N/A",
                    Alasan = m.AlasanMutasi,
                    StatusPersetujuan = (m.IsApproved.HasValue && m.IsApproved.Value) ? "Telah Disetujui" : "Menunggu Approval",
                    ObjekAsli = m
                };
            }).ToList();

            dg2.DataSource = new SortableBindingList<HistoryMutasiViewModel>(dataTampil);

            if (dg2.Columns["ObjekAsli"] != null) dg2.Columns["ObjekAsli"].Visible = false;
            if (dg2.Columns["IdMutasi"] != null) dg2.Columns["IdMutasi"].HeaderText = "ID Mutasi";
            if (dg2.Columns["KodeInventaris"] != null) dg2.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
            if (dg2.Columns["NamaBarang"] != null) dg2.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dg2.Columns["JurusanAsal"] != null) dg2.Columns["JurusanAsal"].HeaderText = "Jurusan Asal";
            if (dg2.Columns["JurusanTujuan"] != null) dg2.Columns["JurusanTujuan"].HeaderText = "Jurusan Tujuan";
            if (dg2.Columns["StatusPersetujuan"] != null) dg2.Columns["StatusPersetujuan"].HeaderText = "Status Mutasi";

            if (dg2.Columns["Tanggal"] != null)
            {
                dg2.Columns["Tanggal"].HeaderText = "Tgl. Mutasi";
                dg2.Columns["Tanggal"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadAset();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is AsetMutasiViewModel vm)
            {
                var a = vm.ObjekAsli;

                txtKodeInv.Text = a.KodeInventaris;
                txtNama.Text = vm.NamaBarang;

                int? asetJur = a.IdJurusan;
                if (asetJur.HasValue)
                    cmbJurusanAsal.SelectedValue = asetJur.Value;
                else
                    cmbJurusanAsal.SelectedIndex = -1;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
            ClearInputs();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKodeInv.Text))
            {
                MessageBox.Show("Pilih aset yang ingin dimutasi dari daftar atas terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbJurusanTujuan.SelectedValue == null)
            {
                MessageBox.Show("Jurusan tujuan harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtAlasan.Text))
            {
                MessageBox.Show("Alasan pemindahan aset harus diisi untuk dokumentasi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int jurusanTujuanId = (int)cmbJurusanTujuan.SelectedValue;

                var aset = db.Aset.FirstOrDefault(a => a.KodeInventaris == txtKodeInv.Text);
                if (aset == null)
                {
                    MessageBox.Show("Aset tidak ditemukan di sistem database.");
                    return;
                }

                if (aset.IdJurusan == jurusanTujuanId)
                {
                    MessageBox.Show("Aset sudah berada di jurusan tersebut. Tidak ada perpindahan yang dilakukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pendingMutasi = db.Mutasi.FirstOrDefault(m => m.KodeInventaris == aset.KodeInventaris && m.IsApproved == false);
                if (pendingMutasi != null)
                {
                    MessageBox.Show("Aset ini sedang dalam proses mutasi (Menunggu Approval Toolman jurusan tujuan). Harap selesaikan mutasi sebelumnya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var mutasiBaru = new Mutasi
                {
                    TanggalMutasi = dtTgl.Value,
                    KodeInventaris = txtKodeInv.Text,
                    IdJurusanAsal = aset.IdJurusan,
                    IdJurusanTujuan = jurusanTujuanId,
                    AlasanMutasi = txtAlasan.Text,
                    IsApproved = false 
                };

                db.Mutasi.Add(mutasiBaru);
                db.SaveChanges();

                MessageBox.Show("Permintaan mutasi berhasil diajukan! Menunggu konfirmasi persetujuan dari Toolman jurusan tujuan.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadMutasi();
                loadAset();
                ClearInputs();
                SetMode("View");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!hasEditRights)
            {
                MessageBox.Show("Anda tidak memiliki hak akses (Hak Ubah) untuk menyetujui mutasi aset ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dg2.CurrentRow != null && dg2.CurrentRow.DataBoundItem is HistoryMutasiViewModel vm)
            {
                if (vm.ObjekAsli.IsApproved == true)
                {
                    MessageBox.Show("Data mutasi ini sudah berstatus Disetujui.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show($"Setujui mutasi barang '{vm.NamaBarang}' ke {vm.JurusanTujuan}?\n\nAset fisik akan dipindahkan dan status inventaris akan berubah.", "Konfirmasi Mutasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var mutasiTarget = db.Mutasi.Find(vm.IdMutasi);
                        if (mutasiTarget != null)
                        {
                            mutasiTarget.IsApproved = true;

                            var asetTerkait = db.Aset.FirstOrDefault(a => a.KodeInventaris == mutasiTarget.KodeInventaris);
                            if (asetTerkait != null)
                            {
                                asetTerkait.IdJurusan = mutasiTarget.IdJurusanTujuan;
                                asetTerkait.Status = $"Di {vm.JurusanTujuan}";
                            }

                            db.SaveChanges();
                            MessageBox.Show("Mutasi aset berhasil disetujui! Barang resmi berpindah kepemilikan ruangan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            loadMutasi();
                            loadAset();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan sistem saat menyetujui mutasi: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih riwayat mutasi yang ingin disetujui dari tabel bawah terlebih dahulu.");
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            ClearInputs();
            loadMutasi();
            loadAset();
            SetMode("View");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}