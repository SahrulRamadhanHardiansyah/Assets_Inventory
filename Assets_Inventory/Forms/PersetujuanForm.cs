using Assets_Inventory.Forms;
using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class PersetujuanForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Permintaan selectedPermintaan;
        public PermintaanHp selectedPermintaanHp;
        public bool isHabisPakai = false;

        private readonly AppDbContext db = new AppDbContext();

        public PersetujuanForm()
        {
            InitializeComponent();
        }

        private void PersetujuanForm_Load(object sender, EventArgs e)
        {
            btnLihatSteps.Visible = false;
            lblMultiInfo.Visible = false;

            if (isHabisPakai)
            {
                if (selectedPermintaanHp == null)
                {
                    MessageBox.Show("Data permintaan habis pakai tidak valid atau gagal dimuat.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                var dataPermintaanHp = db.PermintaanHp
                               .Include(p => p.IdJurusanNavigation)
                               .Include(p => p.IdPenggunaNavigation)
                               .FirstOrDefault(p => p.KodePermintaanHp == selectedPermintaanHp.KodePermintaanHp);
                if (dataPermintaanHp == null)
                {
                    MessageBox.Show("Data permintaan habis pakai tidak ditemukan di database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                int jumlahBarang = db.DetailPermintaanHp.Count(d => d.KodePermintaanHp == selectedPermintaanHp.KodePermintaanHp);
                if (jumlahBarang == 0)
                {
                    MessageBox.Show("Permintaan ini belum memiliki daftar barang yang diminta.\n\nSilakan buka menu Detail/Ubah untuk memasukkan barang yang ingin diminta sebelum melakukan persetujuan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                lblTitle.Text = $"PERSETUJUAN FORM - {dataPermintaanHp.KodePermintaanHp}";
                TryInitMultiLevel(true, dataPermintaanHp.KodePermintaanHp);
            }
            else
            {
                if (selectedPermintaan == null)
                {
                    MessageBox.Show("Data permintaan tidak valid atau gagal dimuat.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                var dataPermintaan = db.Permintaan
                               .Include(p => p.IdJurusanNavigation)
                               .Include(p => p.IdPenggunaNavigation)
                               .FirstOrDefault(p => p.KodePermintaan == selectedPermintaan.KodePermintaan);
                if (dataPermintaan == null)
                {
                    MessageBox.Show("Data permintaan tidak ditemukan di database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                int jumlahBarang = db.DetailPermintaan.Count(d => d.KodePermintaan == selectedPermintaan.KodePermintaan);
                if (jumlahBarang == 0)
                {
                    MessageBox.Show("Permintaan ini belum memiliki daftar barang yang diminta.\n\nSilakan buka menu Detail/Ubah untuk memasukkan barang yang ingin diminta sebelum melakukan persetujuan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                lblTitle.Text = $"PERSETUJUAN FORM - {dataPermintaan.KodePermintaan}";
                TryInitMultiLevel(false, dataPermintaan.KodePermintaan);
            }
        }

        private void TryInitMultiLevel(bool isHabisPakai, string refId)
        {
            try
            {
                string wType = isHabisPakai ? "PermintaanHp" : "Permintaan";
                bool hasMulti = ApprovalWorkflowHelper.HasConfig(db, wType) || (isHabisPakai && ApprovalWorkflowHelper.HasConfig(db, "Permintaan"));
                if (!hasMulti) return;

                ApprovalWorkflowHelper.EnsureSteps(db, wType, refId);
                var pending = db.ApprovalStep.Where(s => s.RefId == refId && s.Status == "Pending").OrderBy(s => s.Level).ToList();
                if (pending.Any())
                {
                    lblTitle.Text += $" | L{pending.Min(x => x.Level)} pending";
                    btnLihatSteps.Visible = true;
                    lblMultiInfo.Visible = true;
                    lblMultiInfo.Text = $"Multi-level: {pending.Count} step pending (Level {pending.Min(x => x.Level)}-{pending.Max(x => x.Level)})";
                }
            }
            catch { }
        }

        private bool TryMultiLevelApprove(string refId, string wType, string catatan)
        {
            try
            {
                bool hasMulti = ApprovalWorkflowHelper.HasConfig(db, wType) || (wType == "PermintaanHp" && ApprovalWorkflowHelper.HasConfig(db, "Permintaan"));
                if (!hasMulti || string.IsNullOrEmpty(refId)) return false;

                ApprovalWorkflowHelper.EnsureSteps(db, wType, refId);
                var steps = db.ApprovalStep.Where(s => s.RefId == refId && s.Status == "Pending").OrderBy(s => s.Level).ToList();
                if (!steps.Any()) return false;

                var curRole = AuthManager.CurrentRoleId;
                var cur = steps.FirstOrDefault(s => s.IdPeranApprover == curRole) ?? steps.First();

                var oldJson = AuditHelper.SerializeObject(cur);
                cur.Status = "Approved";
                int? uid = AuthManager.CurrentUserId > 0 ? (int?)AuthManager.CurrentUserId : (int?)Properties.Settings.Default.userId;
                if (uid == 0) uid = null;
                cur.IdApprover = uid;
                cur.Catatan = catatan;
                cur.TanggalKeputusan = DateTime.Now;
                db.SaveChanges();
                AuditHelper.Log("approval_step", cur.Id.ToString(), "APPROVE", oldJson, AuditHelper.SerializeObject(cur), "Approval", $"Approve {wType}/{refId} L{cur.Level}");

                bool allApproved = ApprovalWorkflowHelper.IsAllRequiredApproved(db, wType, refId);
                if (!allApproved)
                {
                    MessageBox.Show($"Level {cur.Level} disetujui. Masih menunggu level lain sebelum final.", "Sukses - Menunggu Level Lain", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Multi approve error: " + ex.Message);
                return false;
            }
        }

        private void btnSetuju_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAlasan.Text))
            {
                MessageBox.Show("Alasan persetujuan harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string wType = isHabisPakai ? "PermintaanHp" : "Permintaan";
                string refId = isHabisPakai ? selectedPermintaanHp?.KodePermintaanHp : selectedPermintaan?.KodePermintaan;

                if (!string.IsNullOrEmpty(refId))
                {
                    bool earlyHandled = TryMultiLevelApprove(refId, wType, txtAlasan.Text.Trim());
                    if (earlyHandled) return;
                }

                int userId = AuthManager.CurrentUserId > 0 ? AuthManager.CurrentUserId : (Properties.Settings.Default.userId > 0 ? Properties.Settings.Default.userId : 1);

                if (isHabisPakai)
                {
                    var permintaanHp = db.PermintaanHp.Find(selectedPermintaanHp.KodePermintaanHp);
                    if (permintaanHp != null)
                    {
                        var oldJson = AuditHelper.SerializeObject(permintaanHp);
                        permintaanHp.AlasanDisetujui = txtAlasan.Text.Trim();
                        permintaanHp.IdPenyetuju = userId;
                        permintaanHp.TanggalPersetujuan = DateTime.Now.Date;
                        permintaanHp.StatusPersetujuan = "Disetujui";
                        db.SaveChanges();
                        AuditHelper.Log("permintaan_hp", permintaanHp.KodePermintaanHp, "APPROVE", oldJson, AuditHelper.SerializeObject(permintaanHp), "Persetujuan");
                    }
                }
                else
                {
                    var permintaan = db.Permintaan.Find(selectedPermintaan.KodePermintaan);
                    if (permintaan != null)
                    {
                        var oldJson = AuditHelper.SerializeObject(permintaan);
                        permintaan.AlasanDisetujui = txtAlasan.Text.Trim();
                        permintaan.IdPenyetuju = userId;
                        permintaan.TanggalPersetujuan = DateTime.Now.Date;
                        permintaan.StatusPersetujuan = "Disetujui";
                        db.SaveChanges();
                        AuditHelper.Log("permintaan", permintaan.KodePermintaan, "APPROVE", oldJson, AuditHelper.SerializeObject(permintaan), "Persetujuan");
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem saat menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTolak_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAlasan.Text))
            {
                MessageBox.Show("Alasan penolakan harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string wType = isHabisPakai ? "PermintaanHp" : "Permintaan";
                string refId = isHabisPakai ? selectedPermintaanHp?.KodePermintaanHp : selectedPermintaan?.KodePermintaan;

                if (!string.IsNullOrEmpty(refId))
                {
                    try
                    {
                        bool hasMulti = ApprovalWorkflowHelper.HasConfig(db, wType) || (wType == "PermintaanHp" && ApprovalWorkflowHelper.HasConfig(db, "Permintaan"));
                        if (hasMulti)
                        {
                            ApprovalWorkflowHelper.EnsureSteps(db, wType, refId);
                            var steps = db.ApprovalStep.Where(s => s.RefId == refId && s.Status == "Pending").OrderBy(s => s.Level).ToList();
                            if (steps.Any())
                            {
                                var cur = steps.FirstOrDefault(s => s.IdPeranApprover == AuthManager.CurrentRoleId) ?? steps.First();
                                var oldJson = AuditHelper.SerializeObject(cur);
                                cur.Status = "Rejected";
                                int? uid = AuthManager.CurrentUserId > 0 ? (int?)AuthManager.CurrentUserId : (int?)Properties.Settings.Default.userId;
                                if (uid == 0) uid = null;
                                cur.IdApprover = uid;
                                cur.Catatan = txtAlasan.Text.Trim();
                                cur.TanggalKeputusan = DateTime.Now;
                                db.SaveChanges();
                                AuditHelper.Log("approval_step", cur.Id.ToString(), "REJECT", oldJson, AuditHelper.SerializeObject(cur), "Approval");
                            }
                        }
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine("Reject step error: " + ex.Message); }
                }

                int userId = AuthManager.CurrentUserId > 0 ? AuthManager.CurrentUserId : (Properties.Settings.Default.userId > 0 ? Properties.Settings.Default.userId : 1);
                if (isHabisPakai)
                {
                    var p = db.PermintaanHp.Find(selectedPermintaanHp.KodePermintaanHp);
                    if (p != null)
                    {
                        var oldJson = AuditHelper.SerializeObject(p);
                        p.StatusPersetujuan = "Ditolak";
                        p.AlasanDisetujui = txtAlasan.Text.Trim();
                        p.IdPenyetuju = userId;
                        p.TanggalPersetujuan = DateTime.Now.Date;
                        db.SaveChanges();
                        AuditHelper.Log("permintaan_hp", p.KodePermintaanHp, "REJECT", oldJson, AuditHelper.SerializeObject(p), "Persetujuan");
                    }
                }
                else
                {
                    var p = db.Permintaan.Find(selectedPermintaan.KodePermintaan);
                    if (p != null)
                    {
                        var oldJson = AuditHelper.SerializeObject(p);
                        p.StatusPersetujuan = "Ditolak";
                        p.AlasanDisetujui = txtAlasan.Text.Trim();
                        p.IdPenyetuju = userId;
                        p.TanggalPersetujuan = DateTime.Now.Date;
                        db.SaveChanges();
                        AuditHelper.Log("permintaan", p.KodePermintaan, "REJECT", oldJson, AuditHelper.SerializeObject(p), "Persetujuan");
                    }
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Tolak error: " + ex.Message);
                MessageBox.Show("Gagal menyimpan penolakan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLihatSteps_Click(object sender, EventArgs e)
        {
            string wType = isHabisPakai ? "PermintaanHp" : "Permintaan";
            string refId = isHabisPakai ? selectedPermintaanHp?.KodePermintaanHp : selectedPermintaan?.KodePermintaan;
            if (string.IsNullOrEmpty(refId)) return;
            try { ApprovalWorkflowHelper.EnsureSteps(db, wType, refId); } catch { }
            using (var f = new ApprovalStepForm(wType, refId))
            {
                f.ShowDialog();
            }
            TryInitMultiLevel(isHabisPakai, refId);
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
