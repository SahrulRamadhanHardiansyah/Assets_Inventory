namespace Assets_Inventory.UserControls
{
    partial class AsetLampiranUC
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblKet = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnLihat = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.dgLampiran = new System.Windows.Forms.DataGridView();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.lblPreviewTitle = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLampiran)).BeginInit();
            this.pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            //
            // pnlTop
            //
            this.pnlTop.Controls.Add(this.btnHapus);
            this.pnlTop.Controls.Add(this.btnLihat);
            this.pnlTop.Controls.Add(this.btnTambah);
            this.pnlTop.Controls.Add(this.txtKeterangan);
            this.pnlTop.Controls.Add(this.lblKet);
            this.pnlTop.Controls.Add(this.lblInfo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(760, 68);
            this.pnlTop.TabIndex = 0;
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfo.Location = new System.Drawing.Point(8, 6);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(58, 15);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Lampiran";
            //
            // lblKet
            //
            this.lblKet.AutoSize = true;
            this.lblKet.Location = new System.Drawing.Point(8, 28);
            this.lblKet.Name = "lblKet";
            this.lblKet.Size = new System.Drawing.Size(62, 13);
            this.lblKet.TabIndex = 1;
            this.lblKet.Text = "Keterangan";
            //
            // txtKeterangan
            //
            this.txtKeterangan.Location = new System.Drawing.Point(76, 25);
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(260, 20);
            this.txtKeterangan.TabIndex = 2;
            //
            // btnTambah
            //
            this.btnTambah.Location = new System.Drawing.Point(345, 23);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(68, 24);
            this.btnTambah.TabIndex = 3;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            //
            // btnLihat
            //
            this.btnLihat.Location = new System.Drawing.Point(419, 23);
            this.btnLihat.Name = "btnLihat";
            this.btnLihat.Size = new System.Drawing.Size(56, 24);
            this.btnLihat.TabIndex = 4;
            this.btnLihat.Text = "Lihat";
            this.btnLihat.UseVisualStyleBackColor = true;
            this.btnLihat.Click += new System.EventHandler(this.btnLihat_Click);
            //
            // btnHapus
            //
            this.btnHapus.Location = new System.Drawing.Point(481, 23);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(56, 24);
            this.btnHapus.TabIndex = 5;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            //
            // splitMain
            //
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 68);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitMain.Panel1.Controls.Add(this.dgLampiran);
            this.splitMain.Panel2.Controls.Add(this.pnlPreview);
            this.splitMain.Size = new System.Drawing.Size(760, 380);
            this.splitMain.SplitterDistance = 220;
            this.splitMain.TabIndex = 1;
            //
            // dgLampiran
            //
            this.dgLampiran.AllowUserToAddRows = false;
            this.dgLampiran.AllowUserToDeleteRows = false;
            this.dgLampiran.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgLampiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLampiran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLampiran.ReadOnly = true;
            this.dgLampiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLampiran.MultiSelect = false;
            this.dgLampiran.Name = "dgLampiran";
            this.dgLampiran.TabIndex = 0;
            this.dgLampiran.SelectionChanged += new System.EventHandler(this.dgLampiran_SelectionChanged);
            this.dgLampiran.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLampiran_CellDoubleClick);
            //
            // pnlPreview
            //
            this.pnlPreview.Controls.Add(this.picPreview);
            this.pnlPreview.Controls.Add(this.lblPreviewTitle);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 0);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(760, 156);
            this.pnlPreview.TabIndex = 0;
            //
            // lblPreviewTitle
            //
            this.lblPreviewTitle.AutoSize = true;
            this.lblPreviewTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPreviewTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPreviewTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPreviewTitle.Name = "lblPreviewTitle";
            this.lblPreviewTitle.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.lblPreviewTitle.Size = new System.Drawing.Size(52, 17);
            this.lblPreviewTitle.TabIndex = 0;
            this.lblPreviewTitle.Text = "Preview";
            //
            // picPreview
            //
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(0, 17);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(760, 139);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 1;
            this.picPreview.TabStop = false;
            //
            // AsetLampiranUC
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "AsetLampiranUC";
            this.Size = new System.Drawing.Size(760, 448);
            this.Load += new System.EventHandler(this.AsetLampiranUC_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLampiran)).EndInit();
            this.pnlPreview.ResumeLayout(false);
            this.pnlPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblKet;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnLihat;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgLampiran;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.PictureBox picPreview;
    }
}
