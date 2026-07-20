namespace Assets_Inventory.UserControls
{
    partial class AsetLampiranUC
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnLihat = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.dgLampiran = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLampiran)).BeginInit();
            this.SuspendLayout();
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 70;
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(10, 10);
            this.lblInfo.Text = "Lampiran";
            this.txtKeterangan.Location = new System.Drawing.Point(10, 35);
            this.txtKeterangan.Width = 300;
            this.btnTambah.Location = new System.Drawing.Point(320, 33);
            this.btnTambah.Text = "Tambah";
            this.btnTambah.Size = new System.Drawing.Size(65, 23);
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            this.btnLihat.Location = new System.Drawing.Point(390, 33);
            this.btnLihat.Text = "Lihat";
            this.btnLihat.Size = new System.Drawing.Size(55, 23);
            this.btnLihat.Click += new System.EventHandler(this.btnLihat_Click);
            this.btnHapus.Location = new System.Drawing.Point(450, 33);
            this.btnHapus.Text = "Hapus";
            this.btnHapus.Size = new System.Drawing.Size(55, 23);
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            this.pnlTop.Controls.Add(this.lblInfo);
            this.pnlTop.Controls.Add(this.txtKeterangan);
            this.pnlTop.Controls.Add(this.btnTambah);
            this.pnlTop.Controls.Add(this.btnLihat);
            this.pnlTop.Controls.Add(this.btnHapus);
            this.dgLampiran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLampiran.ReadOnly = true;
            this.dgLampiran.AllowUserToAddRows = false;
            this.dgLampiran.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgLampiran.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLampiran_CellDoubleClick);
            this.Controls.Add(this.dgLampiran);
            this.Controls.Add(this.pnlTop);
            this.Name = "AsetLampiranUC";
            this.Size = new System.Drawing.Size(700, 400);
            this.Load += new System.EventHandler(this.AsetLampiranUC_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLampiran)).EndInit();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnLihat;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.DataGridView dgLampiran;
    }
}
