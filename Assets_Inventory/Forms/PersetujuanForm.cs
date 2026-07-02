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

namespace Assets_Inventory
{
    public partial class PersetujuanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Permintaan selectedPermintaan;
        public PermintaanHp selectedPermintaanHp;
        public bool isHabisPakai = false;

        AppDbContext db = new AppDbContext();

        public PersetujuanForm()
        {
            InitializeComponent();
        }

        private void PersetujuanForm_Load(object sender, EventArgs e)
        {
            if (isHabisPakai)
            {
                if (selectedPermintaanHp == null)
                {
                    MessageBox.Show("Data permintaan habis pakai tidak valid atau gagal dimuat.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                var dataPermintaanHp = db.PermintaanHp
                               .Include(p => p.IdJurusanNavigation)
                               .Include(p => p.IdPenggunaNavigation)
                               .FirstOrDefault(p => p.KodePermintaanHp == selectedPermintaanHp.KodePermintaanHp);

                if (dataPermintaanHp == null)
                {
                    MessageBox.Show("Data permintaan habis pakai tidak ditemukan di database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                int jumlahBarang = db.DetailPermintaanHp.Count(d => d.KodePermintaanHp == selectedPermintaanHp.KodePermintaanHp);

                if (jumlahBarang == 0)
                {
                    MessageBox.Show("Permintaan ini belum memiliki daftar barang yang diminta.\n\nSilakan buka menu Detail/Ubah untuk memasukkan barang yang ingin diminta sebelum melakukan persetujuan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                lblTitle.Text = $"PERSETUJUAN FORM - {dataPermintaanHp.KodePermintaanHp}";
            }
            else
            {
                if (selectedPermintaan == null)
                {
                    MessageBox.Show("Data permintaan tidak valid atau gagal dimuat.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                var dataPermintaan = db.Permintaan
                               .Include(p => p.IdJurusanNavigation)
                               .Include(p => p.IdPenggunaNavigation)
                               .FirstOrDefault(p => p.KodePermintaan == selectedPermintaan.KodePermintaan);

                if (dataPermintaan == null)
                {
                    MessageBox.Show("Data permintaan tidak ditemukan di database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                int jumlahBarang = db.DetailPermintaan.Count(d => d.KodePermintaan == selectedPermintaan.KodePermintaan);

                if (jumlahBarang == 0)
                {
                    MessageBox.Show("Permintaan ini belum memiliki daftar barang yang diminta.\n\nSilakan buka menu Detail/Ubah untuk memasukkan barang yang ingin diminta sebelum melakukan persetujuan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                lblTitle.Text = $"PERSETUJUAN FORM - {dataPermintaan.KodePermintaan}";
            }
        }

        private void btnSetuju_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlasan.Text))
            {
                MessageBox.Show("Alasan persetujuan harus diisi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int userId = Properties.Settings.Default.userId > 0 ? Properties.Settings.Default.userId : 1;

                if (isHabisPakai)
                {
                    var permintaanHp = db.PermintaanHp.Find(selectedPermintaanHp.KodePermintaanHp);
                    if (permintaanHp != null)
                    {
                        permintaanHp.AlasanDisetujui = txtAlasan.Text;
                        permintaanHp.IdPenyetuju = userId;
                        permintaanHp.TanggalPersetujuan = DateTime.Now.Date;
                        permintaanHp.StatusPersetujuan = "Disetujui";
                    }
                }
                else
                {
                    var permintaan = db.Permintaan.Find(selectedPermintaan.KodePermintaan);
                    if (permintaan != null)
                    {
                        permintaan.AlasanDisetujui = txtAlasan.Text;
                        permintaan.IdPenyetuju = userId;
                        permintaan.TanggalPersetujuan = DateTime.Now.Date;
                        permintaan.StatusPersetujuan = "Disetujui";
                    }
                }

                db.SaveChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}