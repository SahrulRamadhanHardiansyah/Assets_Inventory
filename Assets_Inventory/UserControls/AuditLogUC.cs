using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Assets_Inventory.UserControls
{
    public partial class AuditLogUC : UserControl
    {
        private AppDbContext db = new AppDbContext();
        private int _currentPage = 0;
        private const int _pageSize = 100;

        public AuditLogUC()
        {
            InitializeComponent();
        }

        private void AuditLogUC_Load(object sender, EventArgs e)
        {
            var hak = AuthManager.GetAkses("Audit Log");
            if (!hak.HakBaca && !AuthManager.GetAkses("Laporan").HakBaca)
            {
                // Allow admin also
                var adminHak = AuthManager.GetAkses("Admin");
                if (!adminHak.HakBaca)
                {
                    MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            cmbAction.Items.Clear();
            cmbAction.Items.Add("Semua");
            cmbAction.Items.Add("INSERT");
            cmbAction.Items.Add("UPDATE");
            cmbAction.Items.Add("DELETE");
            cmbAction.SelectedIndex = 0;

            dtpStart.Value = DateTime.Now.AddDays(-7);
            dtpEnd.Value = DateTime.Now;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var query = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(db.AuditLog).AsQueryable();

                var start = dtpStart.Value.Date;
                var end = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(a => a.Timestamp >= start && a.Timestamp <= end);

                if (cmbAction.SelectedIndex > 0)
                {
                    string act = cmbAction.SelectedItem.ToString();
                    query = query.Where(a => a.Action == act);
                }

                var cari = txtCari.Text?.Trim();
                if (!string.IsNullOrEmpty(cari))
                {
                    cari = cari.ToLower();
                    query = query.Where(a => a.TableName.ToLower().Contains(cari) ||
                                             (a.Username != null && a.Username.ToLower().Contains(cari)) ||
                                             (a.Modul != null && a.Modul.ToLower().Contains(cari)));
                }

                var total = query.Count();
                var data = query.OrderByDescending(a => a.Timestamp)
                                .Skip(_currentPage * _pageSize)
                                .Take(_pageSize)
                                .ToList();

                dgLog.DataSource = new SortableBindingList<AuditLog>(data);
                lblTotal.Text = $"Total: {total} | Hal {_currentPage + 1}/{(int)Math.Ceiling((double)total / _pageSize)}";

                if (dgLog.Columns["OldJson"] != null) dgLog.Columns["OldJson"].Visible = false;
                if (dgLog.Columns["NewJson"] != null) dgLog.Columns["NewJson"].Visible = false;
                if (dgLog.Columns["IpAddress"] != null) dgLog.Columns["IpAddress"].Visible = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("AuditLog load error: " + ex.Message);
                MessageBox.Show("Gagal memuat audit log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            _currentPage = 0;
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            LoadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 0) { _currentPage--; LoadData(); }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExportHelper.ShowSaveDialog(out string path, "audit_log"))
                {
                    ExportHelper.ExportDataGridView(dgLog, path);
                    MessageBox.Show("Export berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Export audit error: " + ex.Message);
                MessageBox.Show("Gagal export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgLog.Rows[e.RowIndex].DataBoundItem is AuditLog log)
            {
                string detail = $"Table: {log.TableName}\nPK: {log.RecordPK}\nAction: {log.Action}\nUser: {log.Username}\nModul: {log.Modul}\nTime: {log.Timestamp}\n\nOLD:\n{log.OldJson}\n\nNEW:\n{log.NewJson}";
                MessageBox.Show(detail, "Detail Audit Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}



