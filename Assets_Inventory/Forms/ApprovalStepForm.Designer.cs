namespace Assets_Inventory.Forms
{
    partial class ApprovalStepForm
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
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.dgSteps = new System.Windows.Forms.DataGridView();
            this.grpCatatan = new System.Windows.Forms.GroupBox();
            this.lblCatatan = new System.Windows.Forms.Label();
            this.txtCatatan = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSteps)).BeginInit();
            this.grpCatatan.SuspendLayout();
            this.grpAction.SuspendLayout();
            this.SuspendLayout();
            // kryptonPalette1
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 15;
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(208, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "APPROVAL STEPS - DETAIL";
            // lblInfo
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblInfo.Location = new System.Drawing.Point(20, 35);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(110, 15);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Workflow: - | Ref: -";
            // grpGrid
            this.grpGrid.Controls.Add(this.dgSteps);
            this.grpGrid.Location = new System.Drawing.Point(23, 60);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(740, 220);
            this.grpGrid.TabIndex = 2;
            this.grpGrid.TabStop = false;
            this.grpGrid.Text = "Steps";
            // dgSteps
            this.dgSteps.AllowUserToAddRows = false;
            this.dgSteps.AllowUserToDeleteRows = false;
            this.dgSteps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSteps.Location = new System.Drawing.Point(3, 22);
            this.dgSteps.Name = "dgSteps";
            this.dgSteps.ReadOnly = true;
            this.dgSteps.RowHeadersVisible = false;
            this.dgSteps.Size = new System.Drawing.Size(734, 195);
            this.dgSteps.TabIndex = 0;
            // grpCatatan
            this.grpCatatan.Controls.Add(this.lblCatatan);
            this.grpCatatan.Controls.Add(this.txtCatatan);
            this.grpCatatan.Controls.Add(this.lblStatus);
            this.grpCatatan.Location = new System.Drawing.Point(23, 286);
            this.grpCatatan.Name = "grpCatatan";
            this.grpCatatan.Size = new System.Drawing.Size(740, 110);
            this.grpCatatan.TabIndex = 3;
            this.grpCatatan.TabStop = false;
            this.grpCatatan.Text = "Keputusan";
            // lblCatatan
            this.lblCatatan.AutoSize = true;
            this.lblCatatan.Location = new System.Drawing.Point(10, 25);
            this.lblCatatan.Name = "lblCatatan";
            this.lblCatatan.Size = new System.Drawing.Size(63, 20);
            this.lblCatatan.TabIndex = 0;
            this.lblCatatan.Text = "Catatan";
            // txtCatatan
            this.txtCatatan.Location = new System.Drawing.Point(90, 22);
            this.txtCatatan.Multiline = true;
            this.txtCatatan.Name = "txtCatatan";
            this.txtCatatan.Size = new System.Drawing.Size(640, 60);
            this.txtCatatan.TabIndex = 1;
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 88);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 15);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status: -";
            // grpAction
            this.grpAction.Controls.Add(this.btnApprove);
            this.grpAction.Controls.Add(this.btnReject);
            this.grpAction.Controls.Add(this.btnRefresh);
            this.grpAction.Controls.Add(this.btnTutup);
            this.grpAction.Location = new System.Drawing.Point(23, 402);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(740, 55);
            this.grpAction.TabIndex = 4;
            this.grpAction.TabStop = false;
            // btnApprove
            this.btnApprove.Location = new System.Drawing.Point(10, 15);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(110, 32);
            this.btnApprove.TabIndex = 0;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // btnReject
            this.btnReject.Location = new System.Drawing.Point(126, 15);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(110, 32);
            this.btnReject.TabIndex = 1;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(242, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // btnTutup
            this.btnTutup.Location = new System.Drawing.Point(640, 15);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(90, 32);
            this.btnTutup.TabIndex = 3;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // ApprovalStepForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 480);
            this.Controls.Add(this.grpAction);
            this.Controls.Add(this.grpCatatan);
            this.Controls.Add(this.grpGrid);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblTitle);
            this.Name = "ApprovalStepForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Approval Steps";
            this.Load += new System.EventHandler(this.ApprovalStepForm_Load);
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSteps)).EndInit();
            this.grpCatatan.ResumeLayout(false);
            this.grpCatatan.PerformLayout();
            this.grpAction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.DataGridView dgSteps;
        private System.Windows.Forms.GroupBox grpCatatan;
        private System.Windows.Forms.Label lblCatatan;
        private System.Windows.Forms.TextBox txtCatatan;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpAction;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTutup;
    }
}
