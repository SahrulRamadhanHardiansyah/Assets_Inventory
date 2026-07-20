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
            dgNotif.CellDoubleClick += dgNotif_CellDoubleClick;
        }

        private void NotifikasiUC_Load(object sender, EventArgs e)
        {
            LoadData();
            tmrAuto.Start(); // ponytail: auto refresh 5 menit; MainForm bisa pakai SetAutoRefresh(false) dan call LoadData() sendiri
        }

        public void LoadData()
        {
            try
            {
                var data = NotificationService.GetAllUnread(50);
                dgNotif.DataSource = data;
                if (dgNotif.Columns["IdPenerima"] != null) dgNotif.Columns["IdPenerima"].Visible = false;
                if (dgNotif.Columns["IdPenerimaNavigation"] != null) dgNotif.Columns["IdPenerimaNavigation"].Visible = false;
                if (dgNotif.Columns["IsRead"] != null) dgNotif.Columns["IsRead"].Visible = false;
                if (dgNotif.Columns["CreatedBy"] != null) dgNotif.Columns["CreatedBy"].Visible = false;
                if (dgNotif.Columns["Message"] != null) dgNotif.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                lblUnread.Text = $"{data.Count} belum dibaca";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("NotifikasiUC load: " + ex.Message);
            }
        }

        private int? GetSelectedId()
        {
            if (dgNotif.CurrentRow?.DataBoundItem is Notifikasi n) return n.Id;
            return null;
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();

        private void btnTandaiBaca_Click(object sender, EventArgs e)
        {
            var id = GetSelectedId();
            if (id == null) return;
            NotificationService.MarkAsRead(id.Value);
            LoadData();
        }

        private void btnHapusDibaca_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Hapus semua notifikasi yang sudah dibaca?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                using (var db = new AppDbContext())
                {
                    var read = db.Notifikasi.Where(x => x.IsRead).ToList();
                    if (read.Count == 0) return;
                    db.Notifikasi.RemoveRange(read);
                    db.SaveChanges();
                }
                LoadData();
            }
            catch { }
        }

        private void dgNotif_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgNotif.Rows[e.RowIndex].DataBoundItem is Notifikasi row)
            {
                NotificationService.MarkAsRead(row.Id);
                LoadData();
            }
        }

        private void tmrAuto_Tick(object sender, EventArgs e) => LoadData();

        public void SetAutoRefresh(bool enabled)
        {
            if (enabled) tmrAuto.Start(); else tmrAuto.Stop();
        }
    }
}
