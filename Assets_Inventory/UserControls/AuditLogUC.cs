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
            try
            {
                var hak = AuthManager.GetAkses("Audit Log");
                var lapHak = AuthManager.GetAkses("Laporan");
                if (!hak.HakBaca && !lapHak.HakBaca)
                {
                    var adminHak = AuthManager.GetAkses("Admin");
                    if (!adminHak.HakBaca)
                    {
                        MessageBox.Show("Anda tidak memiliki akses ke Audit Log.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                cmbAction.Items.Clear();
                cmbAction.Items.AddRange(new object[] { "Semua", "INSERT", "UPDATE", "DELETE", "EXPORT", "LOGIN", "LOGOUT" });
                cmbAction.SelectedIndex = 0;

                cmbTable.Items.Clear();
                cmbTable.Items.Add("Semua");
                // Populate distinct table names - with error handling for missing table / schema mismatch
                try
                {
                    // Use raw SQL fallback to avoid EF mapping issues if column names differ
                    var tables = db.AuditLog.AsNoTracking().Select(x => x.TableName).Distinct().OrderBy(x => x).ToList();
                    foreach (var t in tables)
                    {
                        if (!string.IsNullOrEmpty(t))
                            cmbTable.Items.Add(t);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"AuditLog cmbTable load failed: {ex.Message}\nInner: {ex.InnerException?.Message}");
                }

                if (cmbTable.Items.Count == 1)
                {
                    cmbTable.Items.AddRange(new object[] { "aset", "peminjaman", "pengadaan", "user", "laporan", "audit_log" });
                }
                cmbTable.SelectedIndex = 0;

                dtpStart.Value = DateTime.Now.AddDays(-7);
                dtpEnd.Value = DateTime.Now;

                LoadData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AuditLogUC_Load error: {ex.Message}\nInner: {ex.InnerException?.Message}\nStack: {ex.StackTrace}");
                MessageBox.Show($"Gagal memuat Audit Log.\n\nError: {ex.Message}\nInner: {ex.InnerException?.Message}\n\nPeriksa apakah tabel audit_log sudah dibuat di MySQL dan kolom sesuai mapping AppDbContext.", "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // STEP: Build query with null-safe handling for nullable Timestamp (fix for 0000-00-00 zero date)
                var q = db.AuditLog.AsNoTracking().AsQueryable();

                var start = dtpStart.Value.Date;
                var end = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);

                // Handle nullable Timestamp: filter only where Timestamp has value and in range
                q = q.Where(a => a.Timestamp.HasValue && a.Timestamp.Value >= start && a.Timestamp.Value <= end);

                if (cmbTable.SelectedIndex > 0)
                {
                    var table = cmbTable.SelectedItem?.ToString();
                    if (!string.IsNullOrEmpty(table))
                        q = q.Where(a => a.TableName == table);
                }

                if (cmbAction.SelectedIndex > 0)
                {
                    var act = cmbAction.SelectedItem?.ToString();
                    if (!string.IsNullOrEmpty(act))
                        q = q.Where(a => a.Action == act);
                }

                var cari = txtCari.Text?.Trim();
                if (!string.IsNullOrEmpty(cari))
                {
                    var lc = cari.ToLower();
                    q = q.Where(a => (a.Username != null && a.Username.ToLower().Contains(lc))
                                  || (a.TableName != null && a.TableName.ToLower().Contains(lc))
                                  || (a.Description != null && a.Description.ToLower().Contains(lc))
                                  || (a.Modul != null && a.Modul.ToLower().Contains(lc)));
                }

                var total = q.Count();
                var data = q.OrderByDescending(a => a.Timestamp)
                            .Skip(_currentPage * _pageSize)
                            .Take(_pageSize)
                            .ToList();

                dgLog.DataSource = new SortableBindingList<AuditLog>(data);
                lblTotal.Text = $"Total: {total} | Hal {_currentPage + 1}/{Math.Max(1, (int)Math.Ceiling((double)total / _pageSize))}";

                // Hide large JSON columns to keep grid readable, handle null-safe
                if (dgLog.Columns["OldJson"] != null) dgLog.Columns["OldJson"].Visible = false;
                if (dgLog.Columns["NewJson"] != null) dgLog.Columns["NewJson"].Visible = false;
                if (dgLog.Columns["IpAddress"] != null) dgLog.Columns["IpAddress"].Visible = false;
                if (dgLog.Columns["IdPengguna"] != null) dgLog.Columns["IdPengguna"].Visible = false;

                btnPrev.Enabled = _currentPage > 0;
                btnNext.Enabled = (_currentPage + 1) * _pageSize < total;
            }
            catch (Exception ex)
            {
                // Penyebab pasti: schema mismatch atau table belum ada atau zero date
                // Tampilkan detail lengkap termasuk inner exception
                string detailMsg = $"Gagal memuat audit log.\n\nError: {ex.Message}\nInner: {ex.InnerException?.Message}\n\nStack: {ex.StackTrace}";
                System.Diagnostics.Debug.WriteLine(detailMsg);

                // Self-correction: coba tampilkan pesan yang membantu
                string userMsg = $"Error: {ex.Message}\nInner: {ex.InnerException?.Message}";

                // Jika tabel belum ada
                if (ex.InnerException != null && ex.InnerException.Message.Contains("doesn't exist"))
                {
                    userMsg += "\n\nTabel audit_log belum ada. Silakan jalankan migrasi:\n" +
                               "CREATE TABLE audit_log (id INT AUTO_INCREMENT PRIMARY KEY, table_name VARCHAR(100), record_pk VARCHAR(100), action VARCHAR(20), old_json LONGTEXT, new_json LONGTEXT, id_pengguna INT NULL, username VARCHAR(100), timestamp DATETIME NULL, modul VARCHAR(100), ip_address VARCHAR(45), description TEXT);";
                }
                // Jika zero date issue
                else if (ex.Message.Contains("0000-00-00") || ex.Message.Contains("Unable to convert MySQL date"))
                {
                    userMsg += "\n\nPenyebab: ada baris dengan timestamp 0000-00-00 di MySQL. Jalankan:\nUPDATE audit_log SET timestamp = NOW() WHERE timestamp = '0000-00-00 00:00:00' OR timestamp IS NULL;";
                }

                MessageBox.Show(userMsg, "Error Audit Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                System.Diagnostics.Debug.WriteLine("Export: " + ex.Message + "\nInner: " + ex.InnerException?.Message);
                MessageBox.Show($"Gagal export.\n\nError: {ex.Message}\nInner: {ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgLog.Rows[e.RowIndex].DataBoundItem is AuditLog log)
            {
                // Null-safe display for old_json/new_json
                string oldJsonDisplay = string.IsNullOrEmpty(log.OldJson) ? "(kosong)" : log.OldJson;
                string newJsonDisplay = string.IsNullOrEmpty(log.NewJson) ? "(kosong)" : log.NewJson;
                string timestampDisplay = log.Timestamp.HasValue ? log.Timestamp.Value.ToString("dd/MM/yyyy HH:mm:ss") : "(null/0000-00-00)";

                var detail = $"Table: {log.TableName}\nPK: {log.RecordPK}\nAction: {log.Action}\nUser: {log.Username}\nModul: {log.Modul}\nTime: {timestampDisplay}\n\nOLD:\n{oldJsonDisplay}\n\nNEW:\n{newJsonDisplay}\n\nDesc: {log.Description ?? "(kosong)"}";
                MessageBox.Show(detail, "Detail Audit Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try { db?.Dispose(); } catch { }
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
