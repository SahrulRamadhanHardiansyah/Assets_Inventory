namespace Assets_Inventory.UserControls
{
    partial class AuditLogUC
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        // Dispose handled in Designer, no duplicate in code-behind

        private void InitializeComponent()
        {
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblAction = new System.Windows.Forms.Label();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnTampilkan = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgLog = new System.Windows.Forms.DataGridView();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLog)).BeginInit();
            this.SuspendLayout();
            // pnlFilter
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 80;
            this.pnlFilter.Controls.Add(this.lblStart);
            this.pnlFilter.Controls.Add(this.dtpStart);
            this.pnlFilter.Controls.Add(this.lblEnd);
            this.pnlFilter.Controls.Add(this.dtpEnd);
            this.pnlFilter.Controls.Add(this.lblAction);
            this.pnlFilter.Controls.Add(this.cmbAction);
            this.pnlFilter.Controls.Add(this.txtCari);
            this.pnlFilter.Controls.Add(this.btnTampilkan);
            this.pnlFilter.Controls.Add(this.btnExport);
            this.pnlFilter.Controls.Add(this.btnPrev);
            this.pnlFilter.Controls.Add(this.btnNext);
            this.pnlFilter.Controls.Add(this.lblTotal);
            // lblStart
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(10, 15);
            this.lblStart.Text = "Dari:";
            // dtpStart
            this.dtpStart.Location = new System.Drawing.Point(45, 12);
            this.dtpStart.Width = 130;
            // lblEnd
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(185, 15);
            this.lblEnd.Text = "Sampai:";
            // dtpEnd
            this.dtpEnd.Location = new System.Drawing.Point(230, 12);
            this.dtpEnd.Width = 130;
            // lblAction
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(370, 15);
            this.lblAction.Text = "Aksi:";
            // cmbAction
            this.cmbAction.Location = new System.Drawing.Point(405, 12);
            this.cmbAction.Width = 100;
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // txtCari
            this.txtCari.Location = new System.Drawing.Point(515, 12);
            this.txtCari.Width = 150;
            // btnTampilkan
            this.btnTampilkan.Location = new System.Drawing.Point(10, 45);
            this.btnTampilkan.Text = "Tampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(80, 25);
            this.btnTampilkan.Click += new System.EventHandler(this.btnTampilkan_Click);
            // btnPrev
            this.btnPrev.Location = new System.Drawing.Point(100, 45);
            this.btnPrev.Text = "< Prev";
            this.btnPrev.Size = new System.Drawing.Size(60, 25);
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // btnNext
            this.btnNext.Location = new System.Drawing.Point(165, 45);
            this.btnNext.Text = "Next >";
            this.btnNext.Size = new System.Drawing.Size(60, 25);
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // btnExport
            this.btnExport.Location = new System.Drawing.Point(235, 45);
            this.btnExport.Text = "Export";
            this.btnExport.Size = new System.Drawing.Size(60, 25);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(310, 50);
            this.lblTotal.Text = "Total: 0";
            // dgLog
            this.dgLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLog.AllowUserToAddRows = false;
            this.dgLog.ReadOnly = true;
            this.dgLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLog_CellDoubleClick);
            // AuditLogUC
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
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button btnTampilkan;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgLog;
    }
}
