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
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbWorkflowType = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.grpForm = new System.Windows.Forms.GroupBox();
            this.lblPeran = new System.Windows.Forms.Label();
            this.cmbPeranApprover = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.chkIsRequired = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.dgConfig = new System.Windows.Forms.DataGridView();
            this.grpFilter.SuspendLayout();
            this.grpForm.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgConfig)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPalette1
            //
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 15;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(309, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KONFIGURASI APPROVAL WORKFLOW";
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblInfo.Location = new System.Drawing.Point(20, 35);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(380, 15);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Atur level approval per WorkflowType dan peran approver.";
            //
            // grpFilter
            //
            this.grpFilter.Controls.Add(this.lblType);
            this.grpFilter.Controls.Add(this.cmbWorkflowType);
            this.grpFilter.Controls.Add(this.btnFilter);
            this.grpFilter.Location = new System.Drawing.Point(23, 60);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(750, 55);
            this.grpFilter.TabIndex = 2;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Filter";
            //
            // lblType
            //
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(10, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(105, 20);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Workflow Type";
            //
            // cmbWorkflowType
            //
            this.cmbWorkflowType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkflowType.FormattingEnabled = true;
            this.cmbWorkflowType.Location = new System.Drawing.Point(120, 19);
            this.cmbWorkflowType.Name = "cmbWorkflowType";
            this.cmbWorkflowType.Size = new System.Drawing.Size(160, 28);
            this.cmbWorkflowType.TabIndex = 1;
            this.cmbWorkflowType.SelectedIndexChanged += new System.EventHandler(this.cmbWorkflowType_SelectedIndexChanged);
            //
            // btnFilter
            //
            this.btnFilter.Location = new System.Drawing.Point(290, 18);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(70, 28);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            //
            // grpForm
            //
            this.grpForm.Controls.Add(this.lblPeran);
            this.grpForm.Controls.Add(this.cmbPeranApprover);
            this.grpForm.Controls.Add(this.lblLevel);
            this.grpForm.Controls.Add(this.txtLevel);
            this.grpForm.Controls.Add(this.lblDesc);
            this.grpForm.Controls.Add(this.txtDesc);
            this.grpForm.Controls.Add(this.chkIsRequired);
            this.grpForm.Controls.Add(this.chkIsActive);
            this.grpForm.Location = new System.Drawing.Point(23, 121);
            this.grpForm.Name = "grpForm";
            this.grpForm.Size = new System.Drawing.Size(750, 120);
            this.grpForm.TabIndex = 3;
            this.grpForm.TabStop = false;
            this.grpForm.Text = "Detail Config";
            //
            // lblPeran
            //
            this.lblPeran.AutoSize = true;
            this.lblPeran.Location = new System.Drawing.Point(10, 28);
            this.lblPeran.Name = "lblPeran";
            this.lblPeran.Size = new System.Drawing.Size(71, 20);
            this.lblPeran.TabIndex = 0;
            this.lblPeran.Text = "Peran Approver";
            //
            // cmbPeranApprover
            //
            this.cmbPeranApprover.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeranApprover.FormattingEnabled = true;
            this.cmbPeranApprover.Location = new System.Drawing.Point(120, 25);
            this.cmbPeranApprover.Name = "cmbPeranApprover";
            this.cmbPeranApprover.Size = new System.Drawing.Size(200, 28);
            this.cmbPeranApprover.TabIndex = 1;
            //
            // lblLevel
            //
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(340, 28);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(44, 20);
            this.lblLevel.TabIndex = 2;
            this.lblLevel.Text = "Level";
            //
            // txtLevel
            //
            this.txtLevel.Location = new System.Drawing.Point(390, 25);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.Size = new System.Drawing.Size(60, 26);
            this.txtLevel.TabIndex = 3;
            //
            // lblDesc
            //
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(10, 60);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(67, 20);
            this.lblDesc.TabIndex = 4;
            this.lblDesc.Text = "Deskripsi";
            //
            // txtDesc
            //
            this.txtDesc.Location = new System.Drawing.Point(120, 57);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(330, 26);
            this.txtDesc.TabIndex = 5;
            //
            // chkIsRequired
            //
            this.chkIsRequired.AutoSize = true;
            this.chkIsRequired.Location = new System.Drawing.Point(120, 90);
            this.chkIsRequired.Name = "chkIsRequired";
            this.chkIsRequired.Size = new System.Drawing.Size(102, 24);
            this.chkIsRequired.TabIndex = 6;
            this.chkIsRequired.Text = "IsRequired";
            this.chkIsRequired.UseVisualStyleBackColor = true;
            //
            // chkIsActive
            //
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(230, 90);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(83, 24);
            this.chkIsActive.TabIndex = 7;
            this.chkIsActive.Text = "IsActive";
            this.chkIsActive.UseVisualStyleBackColor = true;
            //
            // grpButtons
            //
            this.grpButtons.Controls.Add(this.btnTambah);
            this.grpButtons.Controls.Add(this.btnUbah);
            this.grpButtons.Controls.Add(this.btnHapus);
            this.grpButtons.Controls.Add(this.btnSimpan);
            this.grpButtons.Controls.Add(this.btnBatal);
            this.grpButtons.Controls.Add(this.btnTutup);
            this.grpButtons.Location = new System.Drawing.Point(23, 247);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(750, 55);
            this.grpButtons.TabIndex = 4;
            this.grpButtons.TabStop = false;
            //
            // btnTambah
            //
            this.btnTambah.Location = new System.Drawing.Point(10, 15);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(90, 30);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            //
            // btnUbah
            //
            this.btnUbah.Location = new System.Drawing.Point(106, 15);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(90, 30);
            this.btnUbah.TabIndex = 1;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            //
            // btnHapus
            //
            this.btnHapus.Location = new System.Drawing.Point(202, 15);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(90, 30);
            this.btnHapus.TabIndex = 2;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            //
            // btnSimpan
            //
            this.btnSimpan.Location = new System.Drawing.Point(298, 15);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(90, 30);
            this.btnSimpan.TabIndex = 3;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            //
            // btnBatal
            //
            this.btnBatal.Location = new System.Drawing.Point(394, 15);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(90, 30);
            this.btnBatal.TabIndex = 4;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            //
            // btnTutup
            //
            this.btnTutup.Location = new System.Drawing.Point(490, 15);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(90, 30);
            this.btnTutup.TabIndex = 5;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            //
            // grpGrid
            //
            this.grpGrid.Controls.Add(this.dgConfig);
            this.grpGrid.Location = new System.Drawing.Point(23, 308);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(750, 230);
            this.grpGrid.TabIndex = 5;
            this.grpGrid.TabStop = false;
            this.grpGrid.Text = "Data Config";
            //
            // dgConfig
            //
            this.dgConfig.AllowUserToAddRows = false;
            this.dgConfig.AllowUserToDeleteRows = false;
            this.dgConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgConfig.Location = new System.Drawing.Point(3, 22);
            this.dgConfig.Name = "dgConfig";
            this.dgConfig.ReadOnly = true;
            this.dgConfig.RowHeadersVisible = false;
            this.dgConfig.Size = new System.Drawing.Size(744, 205);
            this.dgConfig.TabIndex = 0;
            this.dgConfig.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgConfig_CellClick);
            //
            // ApprovalWorkflowConfigForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.grpGrid);
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.grpForm);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblTitle);
            this.Name = "ApprovalWorkflowConfigForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Konfigurasi Approval Workflow";
            this.Load += new System.EventHandler(this.ApprovalWorkflowConfigForm_Load);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpForm.ResumeLayout(false);
            this.grpForm.PerformLayout();
            this.grpButtons.ResumeLayout(false);
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbWorkflowType;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.GroupBox grpForm;
        private System.Windows.Forms.Label lblPeran;
        private System.Windows.Forms.ComboBox cmbPeranApprover;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.CheckBox chkIsRequired;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView dgConfig;
    }
}
