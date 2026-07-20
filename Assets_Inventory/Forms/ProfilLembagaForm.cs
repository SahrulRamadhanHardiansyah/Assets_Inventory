using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class ProfilLembagaForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();
        private string selectedImagePath = null;

        public ProfilLembagaForm()
        {
            InitializeComponent();
        }

        private void ProfilLembagaForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            var hakAkses = AuthManager.GetAkses("Set Lembaga");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            btnSimpan.Enabled = hakAkses.HakBuat || hakAkses.HakUbah;

            try
            {
                var lembaga = db.Pengaturan.FirstOrDefault();

                if (lembaga != null)
                {
                    bindingSource1.DataSource = lembaga;

                    txtPrefix.Text = string.IsNullOrEmpty(lembaga.KustomPrefix) ? "INV" : lembaga.KustomPrefix;

                    if (!string.IsNullOrEmpty(lembaga.LogoInstansi))
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        // path traversal check: only file name allowed
                        string fileName = Path.GetFileName(lembaga.LogoInstansi);
                        if (fileName == lembaga.LogoInstansi)
                        {
                            string imagePath = Path.Combine(baseDirectory, "Resources", fileName);
                            try
                            {
                                string fullPath = Path.GetFullPath(imagePath);
                                string baseResolved = Path.GetFullPath(Path.Combine(baseDirectory, "Resources"));
                                if (fullPath.StartsWith(baseResolved, StringComparison.OrdinalIgnoreCase) && File.Exists(fullPath))
                                {
                                    using (var bmpTemp = new Bitmap(fullPath))
                                    {
                                        pbLogo.Image = new Bitmap(bmpTemp);
                                    }
                                    pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
                                }
                                else
                                {
                                    pbLogo.Image = null;
                                }
                            }
                            catch
                            {
                                pbLogo.Image = null;
                            }
                        }
                        else
                        {
                            pbLogo.Image = null;
                        }
                    }
                }
                else
                {
                    bindingSource1.DataSource = new Pengaturan();
                    txtPrefix.Text = "INV";
                }
            }
            catch
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png|*.jpg;*.jpeg;*.png";
                ofd.Title = "Pilih Logo Instansi";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (!File.Exists(ofd.FileName))
                            return;

                        string ext = Path.GetExtension(ofd.FileName).ToLowerInvariant();
                        if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                        {
                            MessageBox.Show("Format file tidak valid. Hanya jpg dan png yang diperbolehkan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var fi = new FileInfo(ofd.FileName);
                        if (fi.Length > 10L * 1024 * 1024)
                        {
                            MessageBox.Show("Ukuran file terlalu besar (max 10MB).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // magic bytes check via Bitmap load test
                        try
                        {
                            using (var testBmp = new Bitmap(ofd.FileName)) { /* validate */ }
                        }
                        catch
                        {
                            MessageBox.Show("File bukan gambar yang valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        selectedImagePath = ofd.FileName;

                        if (pbLogo.Image != null) pbLogo.Image.Dispose();

                        using (var bmpTemp = new Bitmap(selectedImagePath))
                        {
                            pbLogo.Image = new Bitmap(bmpTemp);
                        }
                        pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
                        cbHapus.Checked = false;
                    }
                    catch
                    {
                        MessageBox.Show("Terjadi kesalahan saat memproses gambar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cbHapus_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHapus.Checked)
            {
                if (pbLogo.Image != null)
                {
                    pbLogo.Image.Dispose();
                    pbLogo.Image = null;
                }
                selectedImagePath = null;
            }
            else
            {
                loadData();
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pengaturan k)
            {
                bindingSource1.EndEdit();

                try
                {
                    string namaFileLogoBaru = k.LogoInstansi;
                    string resourceFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

                    if (!Directory.Exists(resourceFolder))
                    {
                        Directory.CreateDirectory(resourceFolder);
                    }

                    if (cbHapus.Checked)
                    {
                        namaFileLogoBaru = null;
                    }
                    else if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        if (!File.Exists(selectedImagePath))
                        {
                            MessageBox.Show("File logo tidak ditemukan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string ext = Path.GetExtension(selectedImagePath).ToLowerInvariant();
                        if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                        {
                            MessageBox.Show("Format file tidak valid. Hanya jpg dan png yang diperbolehkan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var fi = new FileInfo(selectedImagePath);
                        if (fi.Length > 10L * 1024 * 1024)
                        {
                            MessageBox.Show("Ukuran file terlalu besar (max 10MB).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        try
                        {
                            using (var testBmp = new Bitmap(selectedImagePath)) { }
                        }
                        catch
                        {
                            MessageBox.Show("File bukan gambar yang valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // guid filename + path traversal check
                        string safeFileName = Guid.NewGuid().ToString("N") + ext;
                        string destFilePath = Path.Combine(resourceFolder, safeFileName);
                        string fullDest = Path.GetFullPath(destFilePath);
                        string baseResolved = Path.GetFullPath(resourceFolder);
                        if (!fullDest.StartsWith(baseResolved, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Path file tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        File.Copy(selectedImagePath, fullDest, true);
                        namaFileLogoBaru = safeFileName;
                    }

                    string finalPrefix = string.IsNullOrWhiteSpace(txtPrefix.Text) ? "INV" : txtPrefix.Text.Trim().ToUpper();

                    if (k.IdPengaturan == 0)
                    {
                        var baru = new Pengaturan
                        {
                            NamaInstansi = txtNama.Text,
                            AlamatInstansi = txtAlamat.Text,
                            Telpon = int.TryParse(txtTelp.Text, out int telp) ? telp : (int?)null,
                            Website = txtWebsite.Text,
                            Email = txtEmail.Text,
                            Kota = txtKota.Text,
                            KepalaSekolah = txtKepsek.Text,
                            Nip = txtNip.Text,
                            BagianInventaris = txtInventaris.Text,
                            KustomPrefix = finalPrefix, 
                            LogoInstansi = namaFileLogoBaru
                        };
                        db.Pengaturan.Add(baru);
                    }
                    else
                    {
                        k.NamaInstansi = txtNama.Text;
                        k.AlamatInstansi = txtAlamat.Text;
                        k.Telpon = int.TryParse(txtTelp.Text, out int telpUpdate) ? telpUpdate : (int?)null;
                        k.Website = txtWebsite.Text;
                        k.Email = txtEmail.Text;
                        k.Kota = txtKota.Text;
                        k.KepalaSekolah = txtKepsek.Text;
                        k.Nip = txtNip.Text;
                        k.BagianInventaris = txtInventaris.Text;
                        k.KustomPrefix = finalPrefix;
                        k.LogoInstansi = namaFileLogoBaru;
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data profil lembaga berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedImagePath = null;
                    cbHapus.Checked = false;
                    loadData();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ProfilLembagaForm save error: " + ex.Message);
                    MessageBox.Show("Terjadi kesalahan sistem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}