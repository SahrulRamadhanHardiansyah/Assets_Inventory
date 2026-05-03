using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public static class ApiClientHelper
    {
        public static readonly HttpClient SharedHttpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://127.0.0.1:8000/")
        };

        public static bool TrySetToken()
        {
            string token = Properties.Settings.Default.AuthToken;
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Anda belum login!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            SharedHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
    }
}