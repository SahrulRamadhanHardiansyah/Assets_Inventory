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
            var lapHak = AuthManager.GetAkses("Laporan");
            if (!hak.HakBaca && !lapHak.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses ke Audit Log.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cmbAction.Items.Clear();
            cmbAction.Items.AddRange(new object[] { "Semua", "INSERT", "UPDATE", "DELETE", "EXPORT", "LOGIN", "LOGOUT" });
            cmbAction.SelectedIndex = 0;

            cmbTable.Items.Clear();
            cmbTable.Items.Add("Semua");
            // Populate from existing logs distinct + common known tables
            try
            {
                var tables = db.AuditLog.AsNoTracking().Select(x => x.TableName).Distinct().OrderBy(x => x).ToList();
                foreach (var t in tables) cmbTable.Items.Add(t);
            }
            catch { }
            if (cmbTable.Items.Count == 1)
            {
                cmbTable.Items.AddRange(new object[] { "aset", "peminjaman", "pengadaan", "user", "laporan" });
            }
            cmbTable.SelectedIndex = 0;

            dtpStart.Value = DateTime.Now.AddDays(-7);
            dtpEnd.Value = DateTime.Now;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var q = db.AuditLog.AsNoTracking().AsQueryable();

                var start = dtpStart.Value.Date;
                var end = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);
                q = q.Where(a => a.Timestamp >= start && a.Timestamp <= end);

                if (cmbTable.SelectedIndex > 0)
                {
                    var table = cmbTable.SelectedItem.ToString();
                    q = q.Where(a => a.TableName == table);
                }

                if (cmbAction.SelectedIndex > 0)
                {
                    var act = cmbAction.SelectedItem.ToString();
                    q = q.Where(a => a.Action == act);
                }

                var cari = txtCari.Text?.Trim();
                if (!string.IsNullOrEmpty(cari))
                {
                    var lc = cari.ToLower();
                    q = q.Where(a => (a.Username != null && a.Username.ToLower().Contains(lc))
                                  || (a.TableName != null && a.TableName.ToLower().Contains(lc))
                                  || (a.Description != null && a.Description.ToLower().Contains(lc)));
                }

                var total = q.Count();
                var data = q.OrderByDescending(a => a.Timestamp)
                            .Skip(_currentPage * _pageSize)
                            .Take(_pageSize)
                            .ToList();

                dgLog.DataSource = new SortableBindingList<AuditLog>(data);
                lblTotal.Text = $"Total: {total} | Hal {_currentPage + 1}/{Math.Max(1, (int)Math.Ceiling((double)total / _pageSize))}";

                if (dgLog.Columns["OldJson"] != null) dgLog.Columns["OldJson"].Visible = false;
                if (dgLog.Columns["NewJson"] != null) dgLog.Columns["NewJson"].Visible = false;
                if (dgLog.Columns["IpAddress"] != null) dgLog.Columns["IpAddress"].Visible = false;
                if (dgLog.Columns["IdPengguna"] != null) dgLog.Columns["IdPengguna"].Visible = false;

                btnPrev.Enabled = _currentPage > 0;
                btnNext.Enabled = (_currentPage + 1) * _pageSize < total;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("AuditLog load: " + ex.Message);
                MessageBox.Show("Gagal memuat audit log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTampilkan_Click(object sender, EventArgs e) { _currentPage = 0; LoadData(); }
        private void btnRefresh_Click(object sender, EventArgs e) { _currentPage = 0; txtCari.Clear(); cmbTable.SelectedIndex = 0; cmbAction.SelectedIndex = 0; LoadData(); }
        private void btnPrev_Click(object sender, EventArgs e) { if (_currentPage > 0) { _currentPage--; LoadData(); } }
        private void btnNext_Click(object sender, EventArgs e) { _currentPage++; LoadData(); }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var hak = AuthManager.GetAkses("Audit Log");
            var lapHak = AuthManager.GetAkses("Laporan");
            if (!hak.HakExport && !lapHak.HakExport && !hak.HakBaca && !lapHak.HakBaca)
            {
                MessageBox.Show("Tidak ada akses export.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (dgLog.Rows.Count == 0) { MessageBox.Show("Tidak ada data untuk diekspor.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (ExportHelper.ShowSaveDialog(out string path, "audit_log"))
                {
                    ExportHelper.ExportDataGridView(dgLog, path);
                    MessageBox.Show("Export berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Export: " + ex.Message);
                MessageBox.Show("Gagal export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgLog.Rows[e.RowIndex].DataBoundItem is AuditLog log)
            {
                var detail = $"Table: {log.TableName}\nPK: {log.RecordPK}\nAction: {log.Action}\nUser: {log.Username}\nModul: {log.Modul}\nTime: {log.Timestamp}\n\nOLD:\n{log.OldJson}\n\nNEW:\n{log.NewJson}\n\nDesc: {log.Description}";
                MessageBox.Show(detail, "Detail Audit Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
