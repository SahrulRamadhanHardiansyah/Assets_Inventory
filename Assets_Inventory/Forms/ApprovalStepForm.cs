using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.Forms
{
    public partial class ApprovalStepForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private readonly AppDbContext db = new AppDbContext();
        private string _workflowType;
        private string _refId;

        public string WorkflowType { get => _workflowType; set => _workflowType = value; }
        public string RefId { get => _refId; set => _refId = value; }

        public ApprovalStepForm()
        {
            InitializeComponent();
        }

        public ApprovalStepForm(string workflowType, string refId) : this()
        {
            _workflowType = workflowType;
            _refId = refId;
        }

        private void ApprovalStepForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_workflowType) || string.IsNullOrEmpty(_refId))
            {
                MessageBox.Show("WorkflowType dan RefId harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            lblInfo.Text = $"Workflow: {_workflowType} | Ref: {_refId}";
            LoadSteps();
            ApplyAuth();
        }

        private void ApplyAuth()
        {
            var hakApproval = AuthManager.GetAkses("Approval");
            var hakGroup = AuthManager.GetAkses("Group User");
            bool canApprove = hakApproval.HakApprove || hakApproval.HakBaca || hakGroup.HakBaca;
            btnApprove.Enabled = canApprove;
            btnReject.Enabled = canApprove;
            if (!canApprove)
            {
                // still allow view, disable actions
            }
        }

        private void LoadSteps()
        {
            try
            {
                var peranDict = db.Peran.ToDictionary(p => p.IdPeran, p => p.NamaPeran);
                var penggunaDict = db.Pengguna.ToDictionary(p => p.IdPengguna, p => p.Username);

                var steps = db.ApprovalStep
                    .Where(s => s.WorkflowType == _workflowType && s.RefId == _refId)
                    .OrderBy(s => s.Level)
                    .ToList();

                var display = steps.Select(s => new
                {
                    s.Id,
                    s.Level,
                    PeranApprover = peranDict.TryGetValue(s.IdPeranApprover, out var rn) ? rn : s.IdPeranApprover.ToString(),
                    s.Status,
                    Approver = s.IdApprover.HasValue && penggunaDict.TryGetValue(s.IdApprover.Value, out var un) ? un : "-",
                    s.Catatan,
                    s.TanggalKeputusan,
                    s.CreatedAt
                }).ToList();

                dgSteps.DataSource = null;
                dgSteps.DataSource = display;

                // enable/disable based on pending
                bool hasPending = steps.Any(x => x.Status == "Pending");
                lblStatus.Text = hasPending ? $"Menunggu approval Level {steps.Where(x => x.Status == "Pending").Min(x => x.Level)}" : "Selesai";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load steps error: " + ex.Message);
                MessageBox.Show("Gagal memuat approval steps.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ApprovalStep GetCurrentPendingStep()
        {
            var currentRole = AuthManager.CurrentRoleId;
            var pending = db.ApprovalStep
                .Where(s => s.WorkflowType == _workflowType && s.RefId == _refId && s.Status == "Pending")
                .OrderBy(s => s.Level)
                .ToList();
            if (!pending.Any()) return null;
            // role match first
            var byRole = pending.FirstOrDefault(p => p.IdPeranApprover == currentRole);
            return byRole ?? pending.First();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCatatan.Text))
            {
                MessageBox.Show("Catatan harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var step = GetCurrentPendingStep();
            if (step == null)
            {
                MessageBox.Show("Tidak ada step pending.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ponytail: cek role approver jika config require spesifik peran, tapi tetap izinkan jika user punya HakApprove
            var hak = AuthManager.GetAkses("Approval");
            if (step.IdPeranApprover != AuthManager.CurrentRoleId && !hak.HakApprove)
            {
                var res = MessageBox.Show($"Step Level {step.Level} seharusnya di-approve oleh peran berbeda. Lanjut tetap approve?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes) return;
            }

            try
            {
                var oldJson = AuditHelper.SerializeObject(step);
                step.Status = "Approved";
                step.IdApprover = AuthManager.CurrentUserId > 0 ? (int?)AuthManager.CurrentUserId : null;
                step.Catatan = txtCatatan.Text.Trim();
                step.TanggalKeputusan = DateTime.Now;
                db.SaveChanges();

                AuditHelper.Log("approval_step", step.Id.ToString(), "APPROVE", oldJson, AuditHelper.SerializeObject(step), "Approval", $"Approve {_workflowType}/{_refId} L{step.Level}");

                // cek apakah masih ada required yang pending
                FinalizeIfComplete();

                MessageBox.Show($"Level {step.Level} disetujui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCatatan.Clear();
                LoadSteps();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Approve error: " + ex.Message);
                MessageBox.Show("Gagal approve.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCatatan.Text))
            {
                MessageBox.Show("Alasan penolakan harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var step = GetCurrentPendingStep();
            if (step == null)
            {
                MessageBox.Show("Tidak ada step pending.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Tolak approval Level {step.Level}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                var oldJson = AuditHelper.SerializeObject(step);
                step.Status = "Rejected";
                step.IdApprover = AuthManager.CurrentUserId > 0 ? (int?)AuthManager.CurrentUserId : null;
                step.Catatan = txtCatatan.Text.Trim();
                step.TanggalKeputusan = DateTime.Now;
                db.SaveChanges();

                AuditHelper.Log("approval_step", step.Id.ToString(), "REJECT", oldJson, AuditHelper.SerializeObject(step), "Approval", $"Reject {_workflowType}/{_refId} L{step.Level}");

                // jika ada IsRequired yang reject, tolak keseluruhan
                var cfg = db.ApprovalWorkflowConfig.FirstOrDefault(c => c.WorkflowType == _workflowType && c.Level == step.Level && c.IsActive);
                if (cfg == null || cfg.IsRequired)
                {
                    FinalizeMainEntity(false);
                }

                MessageBox.Show("Ditolak.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCatatan.Clear();
                LoadSteps();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Reject error: " + ex.Message);
                MessageBox.Show("Gagal reject.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FinalizeIfComplete()
        {
            try
            {
                var configs = db.ApprovalWorkflowConfig.Where(c => c.WorkflowType == _workflowType && c.IsActive).OrderBy(c => c.Level).ToList();
                var requiredLevels = configs.Where(c => c.IsRequired).Select(c => c.Level).ToList();
                if (!requiredLevels.Any())
                {
                    // jika tidak ada required flag, anggap semua harus approved
                    requiredLevels = configs.Select(c => c.Level).ToList();
                }

                var steps = db.ApprovalStep.Where(s => s.WorkflowType == _workflowType && s.RefId == _refId).ToList();
                bool allRequiredApproved = requiredLevels.All(lvl => steps.Any(s => s.Level == lvl && s.Status == "Approved"));

                if (allRequiredApproved)
                {
                    FinalizeMainEntity(true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Finalize check error: " + ex.Message);
            }
        }

        private void FinalizeMainEntity(bool isApproved)
        {
            try
            {
                int userId = AuthManager.CurrentUserId > 0 ? AuthManager.CurrentUserId : 1;
                if (_workflowType == "Permintaan")
                {
                    var ent = db.Permintaan.Find(_refId);
                    if (ent != null)
                    {
                        ent.StatusPersetujuan = isApproved ? "Disetujui" : "Ditolak";
                        ent.TanggalPersetujuan = DateTime.Now.Date;
                        ent.IdPenyetuju = userId;
                        if (!isApproved && !string.IsNullOrEmpty(txtCatatan.Text))
                            ent.AlasanDisetujui = txtCatatan.Text.Trim();
                        db.SaveChanges();
                        AuditHelper.Log("permintaan", _refId, isApproved ? "APPROVE_FINAL" : "REJECT_FINAL", null, ent, "Approval");
                    }
                }
                else if (_workflowType == "PermintaanHp")
                {
                    var ent = db.PermintaanHp.Find(_refId);
                    if (ent != null)
                    {
                        ent.StatusPersetujuan = isApproved ? "Disetujui" : "Ditolak";
                        ent.TanggalPersetujuan = DateTime.Now.Date;
                        ent.IdPenyetuju = userId;
                        if (!isApproved && !string.IsNullOrEmpty(txtCatatan.Text))
                            ent.AlasanDisetujui = txtCatatan.Text.Trim();
                        db.SaveChanges();
                        AuditHelper.Log("permintaan_hp", _refId, isApproved ? "APPROVE_FINAL" : "REJECT_FINAL", null, ent, "Approval");
                    }
                }
                else if (_workflowType == "Mutasi")
                {
                    if (int.TryParse(_refId, out int idMutasi))
                    {
                        var ent = db.Mutasi.Find(idMutasi);
                        if (ent != null)
                        {
                            ent.IsApproved = isApproved;
                            db.SaveChanges();
                            AuditHelper.Log("mutasi", _refId, isApproved ? "APPROVE_FINAL" : "REJECT_FINAL", null, ent, "Approval");
                        }
                    }
                }
                else if (_workflowType == "Penghapusan")
                {
                    // best effort: PenghapusanAset tidak punya status approval, hanya log
                    AuditHelper.Log("penghapusan", _refId, isApproved ? "APPROVE_FINAL" : "REJECT_FINAL", null, new { RefId = _refId, Approved = isApproved }, "Approval");
                }
                else if (_workflowType == "Pengadaan")
                {
                    if (int.TryParse(_refId, out int idPengadaan))
                    {
                        var ent = db.Pengadaan.Find(idPengadaan);
                        if (ent != null)
                        {
                            ent.Status = isApproved ? "Selesai Dibelanjakan" : "Menunggu Proses";
                            // tidak ada field approver di pengadaan, hanya status
                            db.SaveChanges();
                            AuditHelper.Log("pengadaan", _refId, isApproved ? "APPROVE_FINAL" : "REJECT_FINAL", null, ent, "Approval");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FinalizeMainEntity error: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadSteps();
        private void btnTutup_Click(object sender, EventArgs e) => this.Close();
    }
}
