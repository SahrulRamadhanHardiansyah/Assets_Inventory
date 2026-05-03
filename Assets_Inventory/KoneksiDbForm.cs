using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory
{
    public partial class KoneksiDbForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private Client apiClient;
        public KoneksiDbForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
        }

        private void KoneksiDbForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnTes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHost.Text))
            {
                MessageBox.Show("Host harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtPort.Text))
            {
                MessageBox.Show("Port harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtDatabase.Text))
            {
                MessageBox.Show("Database harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("User harus diisi.");
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password harus diisi.");
                return;
            }

            try
            {
                await apiClient.ChangeConnectionDatabaseAsync(new DatabaseChangeConnectionRequest
                {
                    Db_host = txtHost.Text,
                    Db_name = txtDatabase.Text,
                    Db_user = txtUser.Text,
                    Db_pass = txtPassword.Text
                });

                MessageBox.Show("Koneksi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
