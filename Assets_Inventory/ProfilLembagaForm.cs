using Assets_Inventory.Models;
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
            try
            {
                var lembaga = db.Pengaturan.FirstOrDefault();

                if (lembaga != null)
                {
                    bindingSource1.DataSource = lembaga;

                    if (!string.IsNullOrEmpty(lembaga.LogoInstansi))
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath = Path.Combine(baseDirectory, "Resources", lembaga.LogoInstansi);

                        if (File.Exists(imagePath))
                        {
                            using (var bmpTemp = new Bitmap(imagePath))
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
                }
                else
                {
                    bindingSource1.DataSource = new Pengaturan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat profil lembaga: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Pilih Logo Instansi";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;

                    if (pbLogo.Image != null) pbLogo.Image.Dispose();

                    using (var bmpTemp = new Bitmap(selectedImagePath))
                    {
                        pbLogo.Image = new Bitmap(bmpTemp);
                    }
                    pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
                    cbHapus.Checked = false;
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
                        string extension = Path.GetExtension(selectedImagePath);
                        namaFileLogoBaru = "Logo_Lembaga_" + DateTime.Now.Ticks + extension;

                        string destFilePath = Path.Combine(resourceFolder, namaFileLogoBaru);
                        File.Copy(selectedImagePath, destFilePath, true);
                    }

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
                    MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}