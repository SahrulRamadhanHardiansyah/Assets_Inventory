namespace Assets_Inventory
{
    partial class TransaksiPengembalianForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtNoTelepon = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamaPeminjam = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgDipinjam = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgDikembalikan = new System.Windows.Forms.DataGridView();
            this.dtpTglKembali = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNoPeminjaman = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCetakBukti = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTutup = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgPeminjaman = new System.Windows.Forms.DataGridView();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDipinjam)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDikembalikan)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPeminjaman)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNoTelepon
            // 
            this.txtNoTelepon.Location = new System.Drawing.Point(10, 157);
            this.txtNoTelepon.Margin = new System.Windows.Forms.Padding(2);
            this.txtNoTelepon.Name = "txtNoTelepon";
            this.txtNoTelepon.ReadOnly = true;
            this.txtNoTelepon.Size = new System.Drawing.Size(209, 20);
            this.txtNoTelepon.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 142);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "No Telepon";
            // 
            // txtNamaPeminjam
            // 
            this.txtNamaPeminjam.Location = new System.Drawing.Point(11, 115);
            this.txtNamaPeminjam.Margin = new System.Windows.Forms.Padding(2);
            this.txtNamaPeminjam.Name = "txtNamaPeminjam";
            this.txtNamaPeminjam.ReadOnly = true;
            this.txtNamaPeminjam.Size = new System.Drawing.Size(208, 20);
            this.txtNamaPeminjam.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.dgDipinjam);
            this.groupBox4.Location = new System.Drawing.Point(246, 168);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(428, 193);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Daftar Barang yang Dipinjam";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 172);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(363, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Double Klik Nama Barang Diatas Untuk Memilih Barang yang DIkembalikan";
            // 
            // dgDipinjam
            // 
            this.dgDipinjam.AllowUserToAddRows = false;
            this.dgDipinjam.AllowUserToDeleteRows = false;
            this.dgDipinjam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDipinjam.Location = new System.Drawing.Point(11, 17);
            this.dgDipinjam.Name = "dgDipinjam";
            this.dgDipinjam.ReadOnly = true;
            this.dgDipinjam.RowHeadersWidth = 62;
            this.dgDipinjam.Size = new System.Drawing.Size(406, 150);
            this.dgDipinjam.TabIndex = 0;
            this.dgDipinjam.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDipinjam_CellDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 100);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Nama Peminjam";
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgDikembalikan);
            this.groupBox2.Location = new System.Drawing.Point(12, 365);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(662, 129);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Daftar Barang yang Dikembalikan";
            // 
            // dgDikembalikan
            // 
            this.dgDikembalikan.AllowUserToAddRows = false;
            this.dgDikembalikan.AllowUserToDeleteRows = false;
            this.dgDikembalikan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgDikembalikan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDikembalikan.Location = new System.Drawing.Point(11, 17);
            this.dgDikembalikan.Margin = new System.Windows.Forms.Padding(2);
            this.dgDikembalikan.Name = "dgDikembalikan";
            this.dgDikembalikan.ReadOnly = true;
            this.dgDikembalikan.RowHeadersVisible = false;
            this.dgDikembalikan.RowHeadersWidth = 62;
            this.dgDikembalikan.RowTemplate.Height = 28;
            this.dgDikembalikan.Size = new System.Drawing.Size(637, 105);
            this.dgDikembalikan.TabIndex = 4;
            this.dgDikembalikan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDikembalikan_CellDoubleClick);
            // 
            // dtpTglKembali
            // 
            this.dtpTglKembali.Location = new System.Drawing.Point(10, 75);
            this.dtpTglKembali.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTglKembali.Name = "dtpTglKembali";
            this.dtpTglKembali.Size = new System.Drawing.Size(209, 20);
            this.dtpTglKembali.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tgl Pengembalian";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNoTelepon);
            this.groupBox1.Controls.Add(this.dtpTglKembali);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNoPeminjaman);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtNamaPeminjam);
            this.groupBox1.Location = new System.Drawing.Point(12, 168);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(230, 193);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Pengembalian";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "No Peminjaman :";
            // 
            // txtNoPeminjaman
            // 
            this.txtNoPeminjaman.Location = new System.Drawing.Point(11, 33);
            this.txtNoPeminjaman.Margin = new System.Windows.Forms.Padding(2);
            this.txtNoPeminjaman.Name = "txtNoPeminjaman";
            this.txtNoPeminjaman.ReadOnly = true;
            this.txtNoPeminjaman.Size = new System.Drawing.Size(208, 20);
            this.txtNoPeminjaman.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 17);
            this.label2.TabIndex = 37;
            this.label2.Text = "TRANSAKSI PENGEMBALIAN";
            // 
            // btnCetakBukti
            // 
            this.btnCetakBukti.Location = new System.Drawing.Point(116, 17);
            this.btnCetakBukti.Margin = new System.Windows.Forms.Padding(2);
            this.btnCetakBukti.Name = "btnCetakBukti";
            this.btnCetakBukti.Size = new System.Drawing.Size(159, 26);
            this.btnCetakBukti.TabIndex = 4;
            this.btnCetakBukti.Text = "Cetak Bukti Pengembalian";
            this.btnCetakBukti.UseVisualStyleBackColor = true;
            this.btnCetakBukti.Click += new System.EventHandler(this.btnCetakBukti_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(11, 17);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(2);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(93, 26);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCetakBukti);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnSimpan);
            this.groupBox3.Location = new System.Drawing.Point(12, 498);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(662, 52);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(558, 17);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(93, 26);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgPeminjaman);
            this.groupBox5.Location = new System.Drawing.Point(11, 33);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(662, 131);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Daftar Peminjaman";
            // 
            // dgPeminjaman
            // 
            this.dgPeminjaman.AllowUserToAddRows = false;
            this.dgPeminjaman.AllowUserToDeleteRows = false;
            this.dgPeminjaman.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPeminjaman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPeminjaman.Location = new System.Drawing.Point(11, 17);
            this.dgPeminjaman.Margin = new System.Windows.Forms.Padding(2);
            this.dgPeminjaman.Name = "dgPeminjaman";
            this.dgPeminjaman.ReadOnly = true;
            this.dgPeminjaman.RowHeadersVisible = false;
            this.dgPeminjaman.RowHeadersWidth = 62;
            this.dgPeminjaman.RowTemplate.Height = 28;
            this.dgPeminjaman.Size = new System.Drawing.Size(637, 105);
            this.dgPeminjaman.TabIndex = 4;
            this.dgPeminjaman.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPeminjaman_CellClick);
            // 
            // TransaksiPengembalianForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(685, 555);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TransaksiPengembalianForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaksi Pengembalian";
            this.Load += new System.EventHandler(this.TransaksiPengembalianForm_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDipinjam)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDikembalikan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPeminjaman)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNoTelepon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamaPeminjam;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgDikembalikan;
        private System.Windows.Forms.DateTimePicker dtpTglKembali;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNoPeminjaman;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCetakBukti;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgDipinjam;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgPeminjaman;
    }
}