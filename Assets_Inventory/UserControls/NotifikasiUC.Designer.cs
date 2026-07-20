namespace Assets_Inventory.UserControls
{
    partial class NotifikasiUC
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblUnread = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTandaiBaca = new System.Windows.Forms.Button();
            this.btnTandaiSemua = new System.Windows.Forms.Button();
            this.btnCekUlang = new System.Windows.Forms.Button();
            this.dgNotif = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotif)).BeginInit();
            this.SuspendLayout();
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 60;
            this.lblUnread.AutoSize = true;
            this.lblUnread.Location = new System.Drawing.Point(10, 10);
            this.lblUnread.Text = "Belum dibaca: 0";
            this.btnRefresh.Location = new System.Drawing.Point(10, 30);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 23);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnTandaiBaca.Location = new System.Drawing.Point(85, 30);
            this.btnTandaiBaca.Text = "Tandai Baca";
            this.btnTandaiBaca.Size = new System.Drawing.Size(90, 23);
            this.btnTandaiBaca.Click += new System.EventHandler(this.btnTandaiBaca_Click);
            this.btnTandaiSemua.Location = new System.Drawing.Point(180, 30);
            this.btnTandaiSemua.Text = "Tandai Semua";
            this.btnTandaiSemua.Size = new System.Drawing.Size(100, 23);
            this.btnTandaiSemua.Click += new System.EventHandler(this.btnTandaiSemua_Click);
            this.btnCekUlang.Location = new System.Drawing.Point(285, 30);
            this.btnCekUlang.Text = "Cek Ulang";
            this.btnCekUlang.Size = new System.Drawing.Size(80, 23);
            this.btnCekUlang.Click += new System.EventHandler(this.btnCekUlang_Click);
            this.pnlTop.Controls.Add(this.lblUnread);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.btnTandaiBaca);
            this.pnlTop.Controls.Add(this.btnTandaiSemua);
            this.pnlTop.Controls.Add(this.btnCekUlang);
            this.dgNotif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNotif.ReadOnly = true;
            this.dgNotif.AllowUserToAddRows = false;
            this.dgNotif.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
        private System.Windows.Forms.Button btnTandaiSemua;
        private System.Windows.Forms.Button btnCekUlang;
        private System.Windows.Forms.DataGridView dgNotif;
    }
}
