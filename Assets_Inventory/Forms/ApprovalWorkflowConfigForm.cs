using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.Forms
{
    public partial class ApprovalWorkflowConfigForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private AppDbContext db = new AppDbContext();

        public ApprovalWorkflowConfigForm()
        {
            InitializeComponent();
        }

        private void ApprovalWorkflowConfigForm_Load(object sender, EventArgs e)
        {
            var hak = AuthManager.GetAkses("Group User");
            if (!hak.HakBaca && !AuthManager.GetAkses("Admin").HakBaca)
            {
                MessageBox.Show("Akses ditolak.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            cmbWorkflowType.Items.Clear();
            cmbWorkflowType.Items.AddRange(new[] { "Permintaan", "Mutasi", "Penghapusan", "Pengadaan", "PermintaanHp" });
            cmbWorkflowType.SelectedIndex = 0;

            cmbPeran.DataSource = db.Peran.ToList();
            cmbPeran.DisplayMember = "NamaPeran";
            cmbPeran.ValueMember = "IdPeran";

            LoadData();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            bool isEdit = mode != "View";
            cmbWorkflowType.Enabled = isEdit;
            cmbPeran.Enabled = isEdit;
            txtLevel.Enabled = isEdit;
            txtDesc.Enabled = isEdit;
            chkIsRequired.Enabled = isEdit;
            chkIsActive.Enabled = isEdit;
            btnTambah.Enabled = !isEdit;
            btnUbah.Enabled = !isEdit;
            btnHapus.Enabled = !isEdit;
            btnSimpan.Enabled = isEdit;
            btnBatal.Enabled = isEdit;
        }

        private void LoadData()
        {
            try
            {
                var filter = cmbWorkflowType.SelectedItem?.ToString();
                var q = db.ApprovalWorkflowConfig.AsQueryable();
                if (!string.IsNullOrEmpty(filter))
                    q = q.Where(x => x.WorkflowType == filter);

                dgConfig.DataSource = q.OrderBy(x => x.Level).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load workflow config error: " + ex.Message);
                MessageBox.Show("Gagal memuat data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e) => LoadData();

        private void dgConfig_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgConfig.Rows[e.RowIndex].DataBoundItem is ApprovalWorkflowConfig cfg)
            {
                var tracked = db.ApprovalWorkflowConfig.Find(cfg.Id);
                if (tracked != null)
                {
                    bindingSource1.DataSource = tracked;
                    cmbWorkflowType.SelectedItem = tracked.WorkflowType;
                    cmbPeran.SelectedValue = tracked.IdPeranApprover;
                    txtLevel.Text = tracked.Level.ToString();
                    txtDesc.Text = tracked.Description;
                    chkIsRequired.Checked = tracked.IsRequired;
                    chkIsActive.Checked = tracked.IsActive;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            txtLevel.Text = "1";
            chkIsRequired.Checked = true;
            chkIsActive.Checked = true;
            SetMode("Insert");
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null) SetMode("Update");
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            LoadData();
            SetMode("View");
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbWorkflowType.SelectedIndex < 0 || cmbPeran.SelectedIndex < 0)
            {
                MessageBox.Show("Workflow type dan peran harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtLevel.Text, out int level) || level < 1)
            {
                MessageBox.Show("Level harus angka >=1.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (bindingSource1.Current is ApprovalWorkflowConfig current)
                {
                    bindingSource1.EndEdit();

                    if (current.Id == 0)
                    {
                        current.WorkflowType = cmbWorkflowType.SelectedItem.ToString();
                        current.IdPeranApprover = (int)cmbPeran.SelectedValue;
                        current.Level = level;
                        current.Description = txtDesc.Text;
                        current.IsRequired = chkIsRequired.Checked;
                        current.IsActive = chkIsActive.Checked;
                        current.CreatedAt = DateTime.Now;
                        db.ApprovalWorkflowConfig.Add(current);
                    }
                    else
                    {
                        var existing = db.ApprovalWorkflowConfig.Find(current.Id);
                        if (existing != null)
                        {
                            existing.WorkflowType = cmbWorkflowType.SelectedItem.ToString();
                            existing.IdPeranApprover = (int)cmbPeran.SelectedValue;
                            existing.Level = level;
                            existing.Description = txtDesc.Text;
                            existing.IsRequired = chkIsRequired.Checked;
                            existing.IsActive = chkIsActive.Checked;
                        }
                    }

                    db.SaveChanges();
                    AuditHelper.Log("approval_workflow_config", current.Id.ToString(), current.Id == 0 ? "INSERT" : "UPDATE", (object)null, (object)current, "Workflow");
                    MessageBox.Show("Berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    SetMode("View");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Save workflow config error: " + ex.Message);
                MessageBox.Show("Gagal menyimpan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is ApprovalWorkflowConfig cfg && cfg.Id != 0)
            {
                if (MessageBox.Show($"Hapus config {cfg.WorkflowType} Level {cfg.Level}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var toDel = db.ApprovalWorkflowConfig.Find(cfg.Id);
                        if (toDel != null)
                        {
                            db.ApprovalWorkflowConfig.Remove(toDel);
                            db.SaveChanges();
                            AuditHelper.Log("approval_workflow_config", cfg.Id.ToString(), "DELETE", (object)cfg, (object)null, "Workflow");
                            LoadData();
                        }
                    }
                    catch { MessageBox.Show("Gagal menghapus.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e) => this.Close();

    }
}
