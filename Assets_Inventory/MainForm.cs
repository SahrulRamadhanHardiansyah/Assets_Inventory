using ComponentFactory.Krypton.Toolkit;
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

namespace Assets_Inventory
{
    public partial class MainForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private Client apiClient;

        public MainForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                var userData = (await apiClient.MeAsync()).Data;

                if (userData != null)
                {
                    lblUser.Text = $"User Aktif: {userData.Username}";
                }

                var pengaturan = (await apiClient.IndexPengaturanAsync()).Data;

                if (pengaturan != null && !string.IsNullOrEmpty(pengaturan.Wallpaper_aplikasi))
                {
                    string imageUrl = "http://127.0.0.1:8000/storage/" + pengaturan.Wallpaper_aplikasi;
                    imageUrl = imageUrl.Replace("\\", "/");

                    using (var httpClient = new HttpClient())
                    {
                        var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                        using (var ms = new MemoryStream(imageBytes))
                        {
                            pnlContent.BackgroundImage = Image.FromStream(ms);
                            pnlContent.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }
            }
            catch (Assets_Inventory.ApiException apiEx)
            {
                if (apiEx.StatusCode == 401)
                {
                    MessageBox.Show("Sesi Anda habis atau tidak valid. Silakan login ulang.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoginForm form = new LoginForm();
                    form.Show();
                    this.Hide();
                    form.FormClosing += (s, args) => this.Close();
                }
                else
                {
                    MessageBox.Show("Gagal mengambil data: " + apiEx.Message, "Error API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private async void wallpaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Pilih Wallpaper Aplikasi";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var currentPengaturan = (await apiClient.IndexPengaturanAsync()).Data;

                        if (currentPengaturan == null)
                        {
                            MessageBox.Show("Data pengaturan lembaga belum dibuat. Silakan isi profil lembaga terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        using (var stream = File.OpenRead(ofd.FileName))
                        {
                            var fileUpload = new FileParameter(stream, Path.GetFileName(ofd.FileName), "image/jpeg");

                            await apiClient.UpdatePengaturanAsync(
                                currentPengaturan.Id_pengaturan,
                                currentPengaturan.Nama_instansi,
                                currentPengaturan.Alamat_instansi,
                                fileUpload,       
                                currentPengaturan.Telpon,
                                currentPengaturan.Website,
                                currentPengaturan.Email,
                                currentPengaturan.Kota,
                                currentPengaturan.Kepala_sekolah,
                                currentPengaturan.NIP,
                                currentPengaturan.Bagian_inventaris
                            );
                        }

                        pnlContent.BackgroundImage = Image.FromFile(ofd.FileName);
                        pnlContent.BackgroundImageLayout = ImageLayout.Stretch;

                        MessageBox.Show("Wallpaper berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Assets_Inventory.ApiException apiEx)
                    {
                        MessageBox.Show("Gagal menyimpan wallpaper: " + apiEx.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
