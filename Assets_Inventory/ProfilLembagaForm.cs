using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class ProfilLembagaForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private Client apiClient;
        private string selectedImagePath = null;

        public ProfilLembagaForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
        }

        private void ProfilLembagaForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            try
            {
                var response = await apiClient.IndexPengaturanAsync();
                var lembaga = response.Data;

                if (lembaga != null)
                {
                    bindingSource1.DataSource = lembaga;

                    if (!string.IsNullOrEmpty(lembaga.Wallpaper_aplikasi))
                    {
                        string imageUrl = "http://http://127.0.0.1:8000/storage/" + lembaga.Wallpaper_aplikasi;
                        pbLogo.LoadAsync(imageUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat profil lembaga: " + ex.Message);
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
                    pbLogo.ImageLocation = selectedImagePath;
                    cbHapus.Checked = false;
                }
            }
        }

        private void cbHapus_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHapus.Checked)
            {
                pbLogo.Image = null;
                selectedImagePath = null;
            }
            else
            {
                loadData();
            }
        }

        private async void btnSimpan_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PengaturanResource k)
            {
                bindingSource1.EndEdit();

                FileParameter fileUpload = null;
                FileStream stream = null;

                try
                {
                    if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        stream = File.OpenRead(selectedImagePath);
                        fileUpload = new FileParameter(stream, Path.GetFileName(selectedImagePath), "image/jpeg");
                    }

                    await apiClient.UpdatePengaturanAsync(
                        k.Id_pengaturan,
                        txtNama.Text,
                        txtAlamat.Text,
                        fileUpload,           
                        txtTelp.Text,
                        txtWebsite.Text,
                        txtEmail.Text,
                        txtKota.Text,
                        txtKepsek.Text,
                        txtNip.Text,
                        txtInventaris.Text
                    );

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedImagePath = null;
                    cbHapus.Checked = false;
                    loadData();
                }
                catch (Assets_Inventory.ApiException apiEx)
                {
                    MessageBox.Show("Gagal menyimpan data: " + apiEx.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
