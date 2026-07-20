namespace Assets_Inventory.UserControls
{
    partial class AuditLogUC
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                try { db?.Dispose(); } catch { }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblTable = new System.Windows.Forms.Label();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.lblCari = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnTampilkan = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgLog = new System.Windows.Forms.DataGridView();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLog)).BeginInit();
            this.SuspendLayout();
            // pnlFilter
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 90;
            this.pnlFilter.Controls.Add(this.lblStart);
            this.pnlFilter.Controls.Add(this.dtpStart);
            this.pnlFilter.Controls.Add(this.lblEnd);
            this.pnlFilter.Controls.Add(this.dtpEnd);
            this.pnlFilter.Controls.Add(this.lblTable);
            this.pnlFilter.Controls.Add(this.cmbTable);
            this.pnlFilter.Controls.Add(this.lblAction);
            this.pnlFilter.Controls.Add(this.cmbAction);
            this.pnlFilter.Controls.Add(this.lblCari);
            this.pnlFilter.Controls.Add(this.txtCari);
            this.pnlFilter.Controls.Add(this.btnTampilkan);
            this.pnlFilter.Controls.Add(this.btnRefresh);
            this.pnlFilter.Controls.Add(this.btnExport);
            this.pnlFilter.Controls.Add(this.btnPrev);
            this.pnlFilter.Controls.Add(this.btnNext);
            this.pnlFilter.Controls.Add(this.lblTotal);
            // lblStart
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(10, 15);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(30, 13);
            this.lblStart.Text = "Dari:";
            // dtpStart
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(45, 12);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(110, 20);
            // lblEnd
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(165, 15);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Text = "Sampai:";
            // dtpEnd
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(215, 12);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(110, 20);
            // lblTable
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(335, 15);
            this.lblTable.Name = "lblTable";
            this.lblTable.Text = "Tabel:";
            // cmbTable
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.Location = new System.Drawing.Point(375, 12);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(120, 21);
            // lblAction
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(505, 15);
            this.lblAction.Name = "lblAction";
            this.lblAction.Text = "Aksi:";
            // cmbAction
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAction.Location = new System.Drawing.Point(540, 12);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(90, 21);
            // lblCari
            this.lblCari.AutoSize = true;
            this.lblCari.Location = new System.Drawing.Point(640, 15);
            this.lblCari.Name = "lblCari";
            this.lblCari.Text = "User:";
            // txtCari
            this.txtCari.Location = new System.Drawing.Point(675, 12);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(115, 20);
            // btnTampilkan
            this.btnTampilkan.Location = new System.Drawing.Point(10, 50);
            this.btnTampilkan.Name = "btnTampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(80, 26);
            this.btnTampilkan.Text = "Tampilkan";
            this.btnTampilkan.UseVisualStyleBackColor = true;
            this.btnTampilkan.Click += new System.EventHandler(this.btnTampilkan_Click);
            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(95, 50);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 26);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // btnExport
            this.btnExport.Location = new System.Drawing.Point(170, 50);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 26);
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // btnPrev
            this.btnPrev.Location = new System.Drawing.Point(250, 50);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(60, 26);
            this.btnPrev.Text = "< Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // btnNext
            this.btnNext.Location = new System.Drawing.Point(315, 50);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 26);
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(385, 57);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(47, 13);
            this.lblTotal.Text = "Total: 0";
            // dgLog
            this.dgLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLog.AllowUserToAddRows = false;
            this.dgLog.AllowUserToDeleteRows = false;
            this.dgLog.ReadOnly = true;
            this.dgLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLog.Location = new System.Drawing.Point(0, 90);
            this.dgLog.Name = "dgLog";
            this.dgLog.RowHeadersVisible = false;
            this.dgLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLog_CellDoubleClick);
            // AuditLogUC
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgLog);
            this.Controls.Add(this.pnlFilter);
            this.Name = "AuditLogUC";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.AuditLogUC_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLog)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.Label lblCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button btnTampilkan;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgLog;
    }
}
