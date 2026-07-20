using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.UserControls
{
    public partial class NotifikasiUC : UserControl
    {
        public NotifikasiUC()
        {
            InitializeComponent();
        }

        private void NotifikasiUC_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var data = NotificationService.GetAllUnread(100);
                dgNotif.DataSource = data.Select(n => new
                {
                    n.Id,
                    n.Type,
                    n.Title,
                    n.Message,
                    n.RefTable,
                    n.RefId,
                    n.CreatedAt,
                    n.IsRead
                }).ToList();

                lblUnread.Text = $"Belum dibaca: {data.Count}";
                if (dgNotif.Columns["Message"] != null) dgNotif.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dgNotif.Columns["IsRead"] != null) dgNotif.Columns["IsRead"].Visible = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load notif error: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();

        private void btnTandaiBaca_Click(object sender, EventArgs e)
        {
            if (dgNotif.CurrentRow != null)
            {
                try
                {
                    var row = dgNotif.CurrentRow;
                    var idProp = row.DataBoundItem?.GetType().GetProperty("Id");
                    if (idProp != null)
                    {
                        int id = (int)idProp.GetValue(row.DataBoundItem);
                        NotificationService.MarkAsRead(id);
                        LoadData();
                    }
                }
                catch { }
            }
        }

        private void btnTandaiSemua_Click(object sender, EventArgs e)
        {
            NotificationService.MarkAllAsRead();
            LoadData();
        }

        private void btnCekUlang_Click(object sender, EventArgs e)
        {
            NotificationService.CheckOverduePeminjaman();
            NotificationService.CheckStokMinimal();
            LoadData();
            MessageBox.Show("Pengecekan notifikasi selesai.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
