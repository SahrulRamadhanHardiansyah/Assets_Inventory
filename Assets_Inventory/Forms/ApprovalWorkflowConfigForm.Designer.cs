namespace Assets_Inventory.Forms
{
    partial class ApprovalWorkflowConfigForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing) try { db?.Dispose(); } catch { }
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbWorkflowType = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblPeran = new System.Windows.Forms.Label();
            this.cmbPeran = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.chkIsRequired = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.dgConfig = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgConfig)).BeginInit();
            this.SuspendLayout();
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 40;
            this.lblType.Text = "Filter Type:"; this.lblType.Location = new System.Drawing.Point(10, 12); this.lblType.AutoSize = true;
            this.cmbWorkflowType.Location = new System.Drawing.Point(80, 10); this.cmbWorkflowType.Width = 150;
            this.btnFilter.Location = new System.Drawing.Point(235, 9); this.btnFilter.Text = "Filter"; this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            this.pnlTop.Controls.Add(this.lblType); this.pnlTop.Controls.Add(this.cmbWorkflowType); this.pnlTop.Controls.Add(this.btnFilter);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Top; this.pnlForm.Height = 120;
            this.lblPeran.Text = "Peran Approver:"; this.lblPeran.Location = new System.Drawing.Point(10, 10); this.lblPeran.AutoSize = true;
            this.cmbPeran.Location = new System.Drawing.Point(120, 8); this.cmbPeran.Width = 200;
            this.lblLevel.Text = "Level:"; this.lblLevel.Location = new System.Drawing.Point(340, 10); this.lblLevel.AutoSize = true;
            this.txtLevel.Location = new System.Drawing.Point(380, 8); this.txtLevel.Width = 50;
            this.lblDesc.Text = "Deskripsi:"; this.lblDesc.Location = new System.Drawing.Point(10, 40); this.lblDesc.AutoSize = true;
            this.txtDesc.Location = new System.Drawing.Point(120, 38); this.txtDesc.Width = 310;
            this.chkIsRequired.Text = "Required"; this.chkIsRequired.Location = new System.Drawing.Point(10, 70); this.chkIsRequired.AutoSize = true;
            this.chkIsActive.Text = "Active"; this.chkIsActive.Location = new System.Drawing.Point(100, 70); this.chkIsActive.AutoSize = true;
            this.pnlForm.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblPeran, this.cmbPeran, this.lblLevel, this.txtLevel, this.lblDesc, this.txtDesc, this.chkIsRequired, this.chkIsActive });
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top; this.pnlButtons.Height = 40;
            this.btnTambah.Text = "Tambah"; this.btnTambah.Location = new System.Drawing.Point(10, 8); this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            this.btnUbah.Text = "Ubah"; this.btnUbah.Location = new System.Drawing.Point(85, 8); this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            this.btnHapus.Text = "Hapus"; this.btnHapus.Location = new System.Drawing.Point(160, 8); this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            this.btnSimpan.Text = "Simpan"; this.btnSimpan.Location = new System.Drawing.Point(235, 8); this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            this.btnBatal.Text = "Batal"; this.btnBatal.Location = new System.Drawing.Point(310, 8); this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            this.btnTutup.Text = "Tutup"; this.btnTutup.Location = new System.Drawing.Point(385, 8); this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            this.pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnTambah, this.btnUbah, this.btnHapus, this.btnSimpan, this.btnBatal, this.btnTutup });
            this.dgConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgConfig.ReadOnly = true; this.dgConfig.AllowUserToAddRows = false; this.dgConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgConfig.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgConfig_CellClick);
            this.Controls.Add(this.dgConfig); this.Controls.Add(this.pnlButtons); this.Controls.Add(this.pnlForm); this.Controls.Add(this.pnlTop);
            this.Name = "ApprovalWorkflowConfigForm"; this.Text = "Konfigurasi Approval Workflow"; this.Size = new System.Drawing.Size(700, 450);
            this.Load += new System.EventHandler(this.ApprovalWorkflowConfigForm_Load);
            this.pnlTop.ResumeLayout(false); this.pnlTop.PerformLayout();
            this.pnlForm.ResumeLayout(false); this.pnlForm.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgConfig)).EndInit();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbWorkflowType;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblPeran;
        private System.Windows.Forms.ComboBox cmbPeran;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.CheckBox chkIsRequired;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.DataGridView dgConfig;
    }
}
