using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.Forms
{
    public partial class ApprovalWorkflowConfigForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private readonly AppDbContext db = new AppDbContext();
        private int _editingId = 0;
        private string _mode = "View";

        public ApprovalWorkflowConfigForm()
        {
            InitializeComponent();
        }

        private void ApprovalWorkflowConfigForm_Load(object sender, EventArgs e)
        {
            // ponytail: dual check Group User OR Approval, fallback Admin
            var hakGu = AuthManager.GetAkses("Group User");
            var hakAp = AuthManager.GetAkses("Approval");
            var hakAdmin = AuthManager.GetAkses("Admin");
            if (!hakGu.HakBaca && !hakAp.HakBaca && !hakAdmin.HakBaca)
            {
                MessageBox.Show("Akses ditolak. Butuh hak Group User atau Approval.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            cmbWorkflowType.Items.Clear();
            cmbWorkflowType.Items.AddRange(new[] { "Permintaan", "PermintaanHp", "Mutasi", "Penghapusan", "Pengadaan" });
            cmbWorkflowType.SelectedIndex = 0;

            LoadPeran();
            LoadData();
            SetMode("View");
        }

        private void LoadPeran()
        {
            try
            {
                var perans = db.Peran.OrderBy(p => p.NamaPeran).ToList();
                cmbPeranApprover.DataSource = perans;
                cmbPeranApprover.DisplayMember = "NamaPeran";
                cmbPeranApprover.ValueMember = "IdPeran";
            }
            catch { }
        }

        private void SetMode(string mode)
        {
            _mode = mode;
            bool isEdit = mode != "View";
            var hakGu = AuthManager.GetAkses("Group User");
            var hakAp = AuthManager.GetAkses("Approval");
            bool canWrite = hakGu.HakBuat || hakGu.HakUbah || hakAp.HakBuat || hakAp.HakUbah || AuthManager.GetAkses("Admin").HakBaca;
            // View mode respects RBAC
            if (!isEdit)
            {
                cmbWorkflowType.Enabled = false;
                cmbPeranApprover.Enabled = false;
                txtLevel.Enabled = false;
                txtDesc.Enabled = false;
                chkIsRequired.Enabled = false;
                chkIsActive.Enabled = false;

                btnTambah.Enabled = canWrite || true; // allow, checked again on click
                btnUbah.Enabled = dgConfig.CurrentRow != null;
                btnHapus.Enabled = dgConfig.CurrentRow != null;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                cmbWorkflowType.Enabled = true;
                cmbPeranApprover.Enabled = true;
                txtLevel.Enabled = true;
                txtDesc.Enabled = true;
                chkIsRequired.Enabled = true;
                chkIsActive.Enabled = true;

                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private void LoadData()
        {
            try
            {
                var filter = cmbWorkflowType.SelectedItem?.ToString();
                var q = db.ApprovalWorkflowConfig.AsQueryable();
                // if filter set, show only that type; user can select all via null handling
                if (!string.IsNullOrEmpty(filter))
                    q = q.Where(x => x.WorkflowType == filter);

                var list = q.OrderBy(x => x.WorkflowType).ThenBy(x => x.Level).ToList();
                // project for display with peran name to avoid lazy load issues
                var peranDict = db.Peran.ToDictionary(p => p.IdPeran, p => p.NamaPeran);
                var display = list.Select(c => new
                {
                    c.Id,
                    c.WorkflowType,
                    c.Level,
                    Peran = peranDict.TryGetValue(c.IdPeranApprover, out var nm) ? nm : c.IdPeranApprover.ToString(),
                    c.IdPeranApprover,
                    c.IsRequired,
                    c.IsActive,
                    c.Description
                }).ToList();

                dgConfig.DataSource = null;
                dgConfig.DataSource = display;
                if (dgConfig.Columns["Id"] != null) dgConfig.Columns["Id"].HeaderText = "ID";
                if (dgConfig.Columns["IdPeranApprover"] != null) dgConfig.Columns["IdPeranApprover"].Visible = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load workflow config error: " + ex.Message);
                MessageBox.Show("Gagal memuat data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e) => LoadData();
        private void cmbWorkflowType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mode == "View") LoadData();
        }

        private void dgConfig_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                var row = dgConfig.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                var entity = db.ApprovalWorkflowConfig.Find(id);
                if (entity == null) return;
                _editingId = entity.Id;
                cmbWorkflowType.SelectedItem = entity.WorkflowType;
                if (cmbPeranApprover.DataSource != null)
                    cmbPeranApprover.SelectedValue = entity.IdPeranApprover;
                txtLevel.Text = entity.Level.ToString();
                txtDesc.Text = entity.Description;
                chkIsRequired.Checked = entity.IsRequired;
                chkIsActive.Checked = entity.IsActive;
            }
            catch { }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            var hakGu = AuthManager.GetAkses("Group User");
            var hakAp = AuthManager.GetAkses("Approval");
            if (!hakGu.HakBuat && !hakAp.HakBuat && !AuthManager.GetAkses("Admin").HakBaca)
            {
                MessageBox.Show("Tidak punya hak tambah.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _editingId = 0;
            txtLevel.Text = "1";
            txtDesc.Text = "";
            chkIsRequired.Checked = true;
            chkIsActive.Checked = true;
            if (cmbPeranApprover.Items.Count > 0) cmbPeranApprover.SelectedIndex = 0;
            SetMode("Insert");
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (_editingId == 0)
            {
                MessageBox.Show("Pilih data yang akan diubah.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var hakGu = AuthManager.GetAkses("Group User");
            var hakAp = AuthManager.GetAkses("Approval");
            if (!hakGu.HakUbah && !hakAp.HakUbah && !AuthManager.GetAkses("Admin").HakBaca)
            {
                MessageBox.Show("Tidak punya hak ubah.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetMode("Update");
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            _editingId = 0;
            LoadData();
            SetMode("View");
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbWorkflowType.SelectedIndex < 0)
            {
                MessageBox.Show("Workflow type harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbPeranApprover.SelectedIndex < 0 || cmbPeranApprover.SelectedValue == null)
            {
                MessageBox.Show("Peran approver harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtLevel.Text.Trim(), out int level) || level < 1)
            {
                MessageBox.Show("Level harus angka >=1.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string wtype = cmbWorkflowType.SelectedItem.ToString();
                int peranId = Convert.ToInt32(cmbPeranApprover.SelectedValue);

                if (_editingId == 0)
                {
                    // cek duplikat level per type
                    bool exists = db.ApprovalWorkflowConfig.Any(x => x.WorkflowType == wtype && x.Level == level);
                    if (exists)
                    {
                        MessageBox.Show($"Level {level} untuk {wtype} sudah ada.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var ent = new ApprovalWorkflowConfig
                    {
                        WorkflowType = wtype,
                        IdPeranApprover = peranId,
                        Level = level,
                        Description = txtDesc.Text?.Trim(),
                        IsRequired = chkIsRequired.Checked,
                        IsActive = chkIsActive.Checked,
                        CreatedAt = DateTime.Now
                    };
                    db.ApprovalWorkflowConfig.Add(ent);
                    db.SaveChanges();
                    AuditHelper.Log("approval_workflow_config", ent.Id.ToString(), "INSERT", null, ent, "Workflow", $"Create {wtype} L{level}");
                    _editingId = ent.Id;
                }
                else
                {
                    var existing = db.ApprovalWorkflowConfig.Find(_editingId);
                    if (existing == null)
                    {
                        MessageBox.Show("Data tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // cek duplikat jika level/type berubah
                    if ((existing.WorkflowType != wtype || existing.Level != level) &&
                        db.ApprovalWorkflowConfig.Any(x => x.Id != _editingId && x.WorkflowType == wtype && x.Level == level))
                    {
                        MessageBox.Show($"Level {level} untuk {wtype} sudah ada.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var oldJson = AuditHelper.SerializeObject(existing);
                    existing.WorkflowType = wtype;
                    existing.IdPeranApprover = peranId;
                    existing.Level = level;
                    existing.Description = txtDesc.Text?.Trim();
                    existing.IsRequired = chkIsRequired.Checked;
                    existing.IsActive = chkIsActive.Checked;
                    db.SaveChanges();
                    AuditHelper.Log("approval_workflow_config", existing.Id.ToString(), "UPDATE", oldJson, AuditHelper.SerializeObject(existing), "Workflow");
                }

                MessageBox.Show("Berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetMode("View");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Save workflow config error: " + ex.Message);
                MessageBox.Show("Gagal menyimpan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_editingId == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var hakGu = AuthManager.GetAkses("Group User");
            var hakAp = AuthManager.GetAkses("Approval");
            if (!hakGu.HakHapus && !hakAp.HakHapus && !AuthManager.GetAkses("Admin").HakBaca)
            {
                MessageBox.Show("Tidak punya hak hapus.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var entity = db.ApprovalWorkflowConfig.Find(_editingId);
            if (entity == null) return;

            if (MessageBox.Show($"Hapus config {entity.WorkflowType} Level {entity.Level}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    db.ApprovalWorkflowConfig.Remove(entity);
                    db.SaveChanges();
                    AuditHelper.Log("approval_workflow_config", _editingId.ToString(), "DELETE", entity, null, "Workflow", $"Delete {entity.WorkflowType} L{entity.Level}");
                    _editingId = 0;
                    LoadData();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Delete config error: " + ex.Message);
                    MessageBox.Show("Gagal menghapus.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e) => this.Close();
    }
}
