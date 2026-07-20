using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Assets_Inventory.Helper;

namespace Assets_Inventory
{
    public partial class MainForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        // Field DbContext with proper dispose in Dispose override
        private AppDbContext db = new AppDbContext();
        private bool _isClosing = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                var userId = Properties.Settings.Default.userId;
                if (userId == 0 || !AuthManager.IsAuthenticated)
                {
                    // Session invalid, return to login
                    lblUser.Text = "User: Unknown";
                }
                else
                {
                    var userData = db.Pengguna.FirstOrDefault(p => p.IdPengguna == userId);
                    if (userData != null)
                    {
                        lblUser.Text = $"User Aktif: {userData.Username}";
                    }
                }

                var pengaturan = db.Pengaturan.FirstOrDefault();
                if (pengaturan != null)
                {
                    this.Text = $"Inventaris Aset Sekolah - {pengaturan.NamaInstansi}";

                    if (!string.IsNullOrEmpty(pengaturan.WallpaperAplikasi))
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath = Path.Combine(baseDirectory, "Resources", pengaturan.WallpaperAplikasi);

                        // Validate wallpaper file name for traversal
                        string fileName = Path.GetFileName(pengaturan.WallpaperAplikasi);
                        if (fileName == pengaturan.WallpaperAplikasi && File.Exists(imagePath))
                        {
                            try
                            {
                                // Validate image magic bytes
                                using (var bmpTemp = new Bitmap(imagePath))
                                {
                                    pnlContent.BackgroundImage = new Bitmap(bmpTemp);
                                }
                                pnlContent.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { /* invalid image */ }
                        }
                    }
                }

                string connStr = ConnectionStringProtector.GetDecryptedConnectionString();
                if (!string.IsNullOrEmpty(connStr))
                {
                    var builder = new DbConnectionStringBuilder { ConnectionString = connStr };
                    if (builder.TryGetValue("Server", out object host))
                        lblHost.Text = $"Host: {host}";
                }

                int intervalMenit = Properties.Settings.Default.BackupInterval;

                if (intervalMenit > 0)
                {
                    timerAutoBackup.Interval = intervalMenit * 60000;
                    timerAutoBackup.Enabled = true;
                    timerAutoBackup.Start();
                }

                // Cache permissions once instead of 30x DB queries
                TerapkanHakAksesMenu();
                CekNotifikasiAset();
                DashboardUC uc = new DashboardUC();
                ChangeView(uc);
            }
            catch
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data utama.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TerapkanHakAksesMenu()
        {
            try
            {
                // Cache all permissions once
                var allAkses = AuthManager.GetAllAkses();

                // Helper lambda with fallback to individual GetAkses if not in cache (for backward compat)
                System.Func<string, bool> canRead = (modul) =>
                {
                    if (allAkses.TryGetValue(modul, out var akses)) return akses.HakBaca;
                    return AuthManager.GetAkses(modul).HakBaca;
                };

                iNVENTARISToolStripMenuItem.Visible = canRead("Inventaris");
                pROSESToolStripMenuItem.Visible = canRead("Proses");
                bRGHABISPAKAIToolStripMenuItem.Visible = canRead("Brg Habis Pakai");
                laporanToolStripMenuItem1.Visible = canRead("Laporan");
                tOOLSToolStripMenuItem.Visible = canRead("Tools");
                aDMINToolStripMenuItem.Visible = canRead("Admin");
                hELPToolStripMenuItem.Visible = canRead("Help");

                permintaanToolStripMenuItem.Visible = canRead("Permintaan Barang");
                pengadaanBarangToolStripMenuItem.Visible = canRead("Pengadaan Barang");
                inputTanahToolStripMenuItem.Visible = canRead("Input Tanah");
                inpiutBangunanToolStripMenuItem.Visible = canRead("Input Bangunan");
                // FIX bug: second assignment was duplicate Visible for same menu, should be Data Aset
                // Search for Data Aset menu item by name if exists
                try
                {
                    var dataAsetItem = menuStrip1.Items.Find("dataBarangAsetToolStripMenuItem", true).FirstOrDefault() as ToolStripMenuItem;
                    if (dataAsetItem != null)
                        dataAsetItem.Visible = canRead("Data Aset");
                    else
                        inpiutBangunanToolStripMenuItem.Visible = canRead("Data Aset") || canRead("Input Bangunan");
                }
                catch
                {
                    inpiutBangunanToolStripMenuItem.Visible = canRead("Data Aset") || canRead("Input Bangunan");
                }

                mutasiBarangToolStripMenuItem.Visible = canRead("Mutasi Barang");
                prosesOpnameToolStripMenuItem.Visible = canRead("Proses Opname");
                barangNonAktifToolStripMenuItem.Visible = canRead("Non Aktif Barang");
                peminjamanToolStripMenuItem.Visible = canRead("Peminjaman");
                pengembalianToolStripMenuItem.Visible = canRead("Pengembalian");
                dataPeminjamanToolStripMenuItem.Visible = canRead("Data Peminjaman");
                dataPengembalianToolStripMenuItem.Visible = canRead("Data Pengembalian");

                masterDataToolStripMenuItem1.Visible = canRead("Master Data Brg Habis Pakai");
                dataBarangHabisPakaiToolStripMenuItem.Visible = canRead("Data Barang Habis Pakai");

                pengadaanBarangHabisPakaiToolStripMenuItem.Visible = canRead("Pengadaan Brg Habis Pakai");
                permintaanHabisPakaiToolStripMenuItem.Visible = canRead("Permintaan Brg Habis Pakai");
                barangHabisPakaiKeluarToolStripMenuItem.Visible = canRead("Barang Keluar");
                laporanSTokToolStripMenuItem1.Visible = canRead("Lap Stok Barang");
                lapStokToolStripMenuItem.Visible = canRead("Lap Stok Brg Habis Pakai");
                lapStokMinimalToolStripMenuItem.Visible = canRead("Lap Stok Minimal Brg Habis Pakai");

                koneksiToolStripMenuItem.Visible = canRead("Koneksi");
                backupToolStripMenuItem.Visible = canRead("Backup");
                restoreToolStripMenuItem.Visible = canRead("Restore");
                resetToolStripMenuItem.Visible = canRead("Reset");

                masterDataToolStripMenuItem.Visible = canRead("Data Master");
                dataLembagaToolStripMenuItem.Visible = canRead("Set Lembaga");
                userToolStripMenuItem.Visible = canRead("User");
                groupUserToolStripMenuItem.Visible = canRead("Group User");
                wallpaperToolStripMenuItem.Visible = canRead("Wallpaper");

                aboutToolStripMenuItem.Visible = canRead("About");
                tutorialToolStripMenuItem.Visible = canRead("Tutorial");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TerapkanHakAksesMenu error: " + ex.Message);
            }
        }

        public void ChangeView(UserControl uc)
        {
            try
            {
                // Dispose previous UC if needed
                foreach (Control c in pnlContent.Controls)
                {
                    try { c.Dispose(); } catch { }
                }
                pnlContent.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(uc);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ChangeView error: " + ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isClosing)
                return;

            DialogResult dr = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            _isClosing = true;
            timerAutoBackup.Stop();
            timerAutoBackup.Enabled = false;

            // Backup is optional and should NOT block closing or throw
            string path = Properties.Settings.Default.BackupPath;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                try
                {
                    // Do not freeze UI - run with timeout consideration, but keep synchronous to avoid orphan
                    DatabaseHelper.PerformBackup(path);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Auto backup on exit failed: " + ex.Message);
                    // Do not block closing for backup failure
                }
            }

            AuthManager.ClearSession();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin keluar aplikasi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                _isClosing = true;
                timerAutoBackup.Stop();
                AuthManager.ClearSession();
                Application.Exit();
            }
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
                    // Validate path does not contain traversal
                    string fullPdf = Path.GetFullPath(pdfPath);
                    string baseResolved = Path.GetFullPath(baseDirectory);
                    if (!fullPdf.StartsWith(baseResolved, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Path file tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fullPdf,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("Gagal membuka file panduan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File panduan tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        // Validate file exists and is safe
                        if (!File.Exists(ofd.FileName)) return;
                        string ext = Path.GetExtension(ofd.FileName).ToLower();
                        if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".bmp")
                        {
                            MessageBox.Show("Format file tidak valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Check file size (max 10MB)
                        var fi = new FileInfo(ofd.FileName);
                        if (fi.Length > 10 * 1024 * 1024)
                        {
                            MessageBox.Show("Ukuran file terlalu besar (max 10MB).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Validate image magic bytes
                        try
                        {
                            using (var testBmp = new Bitmap(ofd.FileName)) { /* just validate */ }
                        }
                        catch
                        {
                            MessageBox.Show("File bukan gambar yang valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var pengaturan = db.Pengaturan.FirstOrDefault();
                        if (pengaturan == null)
                        {
                            MessageBox.Show("Data pengaturan lembaga belum dibuat. Silakan isi profil lembaga terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string resourceFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                        if (!Directory.Exists(resourceFolder)) Directory.CreateDirectory(resourceFolder);

                        // Generate safe file name with GUID to avoid collision and traversal
                        string safeFileName = Guid.NewGuid().ToString("N") + ext;
                        string destFilePath = Path.Combine(resourceFolder, safeFileName);

                        File.Copy(ofd.FileName, destFilePath, true);

                        pengaturan.WallpaperAplikasi = safeFileName;
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
                        System.Diagnostics.Debug.WriteLine("Wallpaper error: " + ex.Message);
                        MessageBox.Show("Terjadi kesalahan saat memproses gambar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            TransaksiPeminjamanForm form = new TransaksiPeminjamanForm();
            form.ShowDialog();
        }

        private void pengembalianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransaksiPengembalianForm form = new TransaksiPengembalianForm();
            form.ShowDialog();
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

        private void dataBarangHabisPakaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBarangHabisPakaiUC uc = new DataBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void pengadaanBarangHabisPakaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPengadaaanBarangHabisPakaiUC uc = new DataPengadaaanBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void barangHabisPakaiKeluarToolStripMenuItem_Click(object sender, EventArgs e)
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
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Auto backup failed: " + ex.Message);
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
                        lblNotifAset.Text = $"Terdapat {jumlahBelumLengkap} data aset yang perlu dilengkapi!";
                    }
                    else
                    {
                        lblNotifAset.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Gagal mengecek notifikasi: " + ex.Message);
            }
        }

        private void lblNotifAset_Click(object sender, EventArgs e)
        {
            AsetPerluDilengkapiUC uc = new AsetPerluDilengkapiUC();
            ChangeView(uc);
        }

        private void dASHBOARDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashboardUC uc = new DashboardUC();
            ChangeView(uc);
        }

        private void laporanSTokToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void masterDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBarangHabisPakaiUC uc = new DataBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void permintaanHabisPakaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPermintaanBarangHabisPakaiUC uc = new DataPermintaanBarangHabisPakaiUC();
            ChangeView(uc);
        }

        private void dataBarangAsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBarangAsetUC uc = new DataBarangAsetUC();
            ChangeView(uc);
        }

        private void dataPeminjamanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeminjamanBarangUC uc = new PeminjamanBarangUC();
            ChangeView(uc);
        }

        private void dataPengembalianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PengembalianBarangUC uc = new PengembalianBarangUC();
            ChangeView(uc);
        }

    }
}
