using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory
{
    public partial class LoginForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private static readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://127.0.0.1:8000/")
        };

        private Client apiClient;

        public LoginForm()
        {
            InitializeComponent();
            apiClient = new Client(httpClient);
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtUsername.Text?.Trim();
                var password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username dan password harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var loginRequest = new LoginRequest
                {
                    Username = username,
                    Password = password
                };

                var loginResponse = await apiClient.LoginAsync(loginRequest);
                string token = loginResponse.Data?.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    Properties.Settings.Default.AuthToken = token;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Login  Berhasil.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm form = new MainForm();
                    form.Show();
                    this.Hide();
                    form.FormClosed += (s, args) => this.Close();
                }
                else
                { 
                    MessageBox.Show("Login gagal: token tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Assets_Inventory.ApiException apiEx)
            {
                MessageBox.Show("Gagal: " + apiEx.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        
    }
}
