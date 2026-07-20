namespace Assets_Inventory.UserControls
{
    partial class NotifikasiUC
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblUnread = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTandaiBaca = new System.Windows.Forms.Button();
            this.btnHapusDibaca = new System.Windows.Forms.Button();
            this.dgNotif = new System.Windows.Forms.DataGridView();
            this.tmrAuto = new System.Windows.Forms.Timer(this.components);
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotif)).BeginInit();
            this.SuspendLayout();
            // pnlTop
            this.pnlTop.Controls.Add(this.lblUnread);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.btnTandaiBaca);
            this.pnlTop.Controls.Add(this.btnHapusDibaca);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(800, 50);
            this.pnlTop.TabIndex = 0;
            // lblUnread - badge count
            this.lblUnread.AutoSize = true;
            this.lblUnread.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnread.ForeColor = System.Drawing.Color.Firebrick;
            this.lblUnread.Location = new System.Drawing.Point(10, 15);
            this.lblUnread.Name = "lblUnread";
            this.lblUnread.Size = new System.Drawing.Size(110, 15);
            this.lblUnread.TabIndex = 0;
            this.lblUnread.Text = "0 belum dibaca";
            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(160, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 26);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // btnTandaiBaca
            this.btnTandaiBaca.Location = new System.Drawing.Point(240, 10);
            this.btnTandaiBaca.Name = "btnTandaiBaca";
            this.btnTandaiBaca.Size = new System.Drawing.Size(95, 26);
            this.btnTandaiBaca.TabIndex = 2;
            this.btnTandaiBaca.Text = "Tandai Baca";
            this.btnTandaiBaca.UseVisualStyleBackColor = true;
            this.btnTandaiBaca.Click += new System.EventHandler(this.btnTandaiBaca_Click);
            // btnHapusDibaca
            this.btnHapusDibaca.Location = new System.Drawing.Point(340, 10);
            this.btnHapusDibaca.Name = "btnHapusDibaca";
            this.btnHapusDibaca.Size = new System.Drawing.Size(105, 26);
            this.btnHapusDibaca.TabIndex = 3;
            this.btnHapusDibaca.Text = "Hapus Dibaca";
            this.btnHapusDibaca.UseVisualStyleBackColor = true;
            this.btnHapusDibaca.Click += new System.EventHandler(this.btnHapusDibaca_Click);
            // dgNotif
            this.dgNotif.AllowUserToAddRows = false;
            this.dgNotif.AllowUserToDeleteRows = false;
            this.dgNotif.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgNotif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNotif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNotif.Location = new System.Drawing.Point(0, 50);
            this.dgNotif.Name = "dgNotif";
            this.dgNotif.ReadOnly = true;
            this.dgNotif.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNotif.Size = new System.Drawing.Size(800, 400);
            this.dgNotif.TabIndex = 4;
            // tmrAuto - 5 menit
            this.tmrAuto.Interval = 300000;
            this.tmrAuto.Tick += new System.EventHandler(this.tmrAuto_Tick);
            // NotifikasiUC
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgNotif);
            this.Controls.Add(this.pnlTop);
            this.Name = "NotifikasiUC";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.NotifikasiUC_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotif)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblUnread;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTandaiBaca;
        private System.Windows.Forms.Button btnHapusDibaca;
        private System.Windows.Forms.DataGridView dgNotif;
        private System.Windows.Forms.Timer tmrAuto;
    }
}
