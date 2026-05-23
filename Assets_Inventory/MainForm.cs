using ComponentFactory.Krypton.Toolkit;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assets_Inventory.Models;

namespace Assets_Inventory
{
    public partial class MainForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                var userId = Properties.Settings.Default.userId;
                var userData = db.Pengguna.FirstOrDefault(p => p.IdPengguna == userId);
                if (userData != null)
                {
                    lblUser.Text = $"User Aktif: {userData.Username}";
                }

                var pengaturan = db.Pengaturan.FirstOrDefault();
                if (pengaturan != null && !string.IsNullOrEmpty(pengaturan.WallpaperAplikasi))
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string imagePath = Path.Combine(baseDirectory, "Resources", pengaturan.WallpaperAplikasi);

                    if (File.Exists(imagePath))
                    {
                        using (var bmpTemp = new Bitmap(imagePath))
                        {
                            pnlContent.BackgroundImage = new Bitmap(bmpTemp);
                        }
                        pnlContent.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }

                int intervalMenit = Properties.Settings.Default.BackupInterval;

                if (intervalMenit > 0)
                {
                    timerAutoBackup.Interval = intervalMenit * 60000;
                    timerAutoBackup.Enabled = true;
                    timerAutoBackup.Start();
                }

                CekNotifikasiAset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ChangeView(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
            {
                e.Cancel = true; 
            }

            string path = Properties.Settings.Default.BackupPath;
            if (!string.IsNullOrEmpty(path))
            {
                DatabaseHelper.PerformBackup(path);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin keluar aplikasi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) Application.Exit();
        }

        private void pengadaanBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PengadaanBarangUC uc = new PengadaanBarangUC();
            ChangeView(uc);
        }

        private void inputTanahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputTanahForm form = new InputTanahForm();
            form.ShowDialog();
        }

        private void inpiutBangunanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBangunanForm form = new InputBangunanForm();
            form.ShowDialog();
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void tutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string pdfPath = Path.Combine(baseDirectory, "Resources", "panduan.pdf");

            if (File.Exists(pdfPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = pdfPath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal membuka PDF: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("File PDF tidak ditemukan di: " + pdfPath);
            }
        }

        private void aktivasiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTanggal.Text = $"Tanggal: {DateTime.Now.ToString("dd MMMM yyyy")}";
            lblWaktu.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void masterDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MasterDataForm form = new MasterDataForm();
            form.ShowDialog();
        }

        private void laporanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LaporanForm form = new LaporanForm();
            form.ShowDialog();
        }

        private void koneksiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KoneksiDbForm form = new KoneksiDbForm();
            form.ShowDialog();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupDbForm form = new BackupDbForm();
            form.ShowDialog();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreDbForm form = new RestoreDbForm();
            form.ShowDialog();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetDbForm form = new ResetDbForm();
            form.ShowDialog();
        }

        private void dataLembagaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ProfilLembagaForm form = new ProfilLembagaForm();
            form.ShowDialog();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm form = new UserForm();
            form.ShowDialog();
        }

        private void groupUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupUserForm form = new GroupUserForm();
            form.ShowDialog();
        }

        private void wallpaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Pilih Wallpaper Aplikasi";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var pengaturan = db.Pengaturan.FirstOrDefault();
                        if (pengaturan == null)
                        {
                            MessageBox.Show("Data pengaturan lembaga belum dibuat. Silakan isi profil lembaga terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string resourceFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

                        if (!Directory.Exists(resourceFolder)) Directory.CreateDirectory(resourceFolder);

                        string fileName = Path.GetFileName(ofd.FileName);
                        string destFilePath = Path.Combine(resourceFolder, fileName);

                        File.Copy(ofd.FileName, destFilePath, true);

                        pengaturan.WallpaperAplikasi = fileName;
                        db.SaveChanges();

                        if (pnlContent.BackgroundImage != null)
                        {
                            pnlContent.BackgroundImage.Dispose(); 
                        }

                        using (var bmpTemp = new Bitmap(destFilePath))
                        {
                            pnlContent.BackgroundImage = new Bitmap(bmpTemp);
                        }

                        pnlContent.BackgroundImageLayout = ImageLayout.Stretch;
                        MessageBox.Show("Wallpaper berhasil diperbarui dan disimpan ke sistem!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan sistem saat memproses gambar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mutasiBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MutasiBarangForm form = new MutasiBarangForm();
            form.ShowDialog();
        }

        private void prosesOpnameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpnameBarangForm form = new OpnameBarangForm();
            form.ShowDialog();
        }

        private void barangNonAktifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProsesNonAktifForm form = new ProsesNonAktifForm();
            form.ShowDialog();
        }

        private void peminjamanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeminjamanBarangUC uc = new PeminjamanBarangUC();
            ChangeView(uc);
        }

        private void pengembalianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBarangHabisPakaiUC uc = new DataBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void dataSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterSupplierForm form = new MasterSupplierForm();
            form.ShowDialog();
        }

        private void dataGudangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterGudangForm form = new MasterGudangForm();
            form.ShowDialog();
        }

        private void dataVarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBarangHabisPakaiUC uc = new DataBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void barangKeluarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPengadaaanBarangHabisPakaiUC uc = new DataPengadaaanBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void laporanStokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBarangHabisPakaiKeluarUC uc = new DataBarangHabisPakaiKeluarUC();
            ChangeView(uc);
        }

        private void lapStokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaporanStokBarangHabisPakaiUC uc = new LaporanStokBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void lapStokMinimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaporanStokMinimalBarangHabisPakaiUC uc = new LaporanStokMinimalBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void timerAutoBackup_Tick(object sender, EventArgs e)
        {
            string path = Properties.Settings.Default.BackupPath;

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                try
                {
                    DatabaseHelper.PerformBackup(path);
                }
                catch
                {
                }
            }
        }

        private void permintaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PermintaanBarangUC uc = new PermintaanBarangUC();
            ChangeView(uc);
        }

        public void CekNotifikasiAset()
        {
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    int jumlahBelumLengkap = dbContext.Aset.Count(a =>
                        string.IsNullOrEmpty(a.NoSeri) ||
                        a.IdRuang == null ||
                        a.IdLokasi == null ||
                        string.IsNullOrEmpty(a.Gambar)
                    );

                    if (jumlahBelumLengkap > 0)
                    {
                        lblNotifAset.Visible = true;
                        lblNotifAset.Text = $"⚠️ Terdapat {jumlahBelumLengkap} data aset yang perlu dilengkapi!";
                    }
                    else
                    {
                        lblNotifAset.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gagal mengecek notifikasi: " + ex.Message);
            }
        }

        private void lblNotifAset_Click(object sender, EventArgs e)
        {
            AsetPerluDilengkapiUC uc = new AsetPerluDilengkapiUC();
            ChangeView(uc);
        }
    }
}
