namespace Assets_Inventory
{
    partial class TransaksiPeminjamanForm
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
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCetak = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDurasi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoPinjam = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtPinjam = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(749, 36);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(19, 36);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 40);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCetak);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnSimpan);
            this.groupBox3.Controls.Add(this.btnTambah);
            this.groupBox3.Location = new System.Drawing.Point(17, 554);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(912, 95);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnCetak
            // 
            this.btnCetak.Location = new System.Drawing.Point(331, 36);
            this.btnCetak.Name = "btnCetak";
            this.btnCetak.Size = new System.Drawing.Size(188, 40);
            this.btnCetak.TabIndex = 4;
            this.btnCetak.Text = "Cetak Bukti Pinjam";
            this.btnCetak.UseVisualStyleBackColor = true;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(174, 36);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "TRANSAKSI PEMINJAMAN";
            // 
            // txtDurasi
            // 
            this.txtDurasi.Location = new System.Drawing.Point(19, 108);
            this.txtDurasi.Name = "txtDurasi";
            this.txtDurasi.Size = new System.Drawing.Size(396, 26);
            this.txtDurasi.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lama Pinjam (Hari)";
            // 
            // txtNoPinjam
            // 
            this.txtNoPinjam.Location = new System.Drawing.Point(19, 48);
            this.txtNoPinjam.Name = "txtNoPinjam";
            this.txtNoPinjam.Size = new System.Drawing.Size(185, 26);
            this.txtNoPinjam.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtPinjam);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDurasi);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNoPinjam);
            this.groupBox1.Location = new System.Drawing.Point(17, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 159);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Peminjaman";
            // 
            // dtPinjam
            // 
            this.dtPinjam.Location = new System.Drawing.Point(230, 48);
            this.dtPinjam.Name = "dtPinjam";
            this.dtPinjam.Size = new System.Drawing.Size(185, 26);
            this.dtPinjam.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tgl Pinjam";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "No Peminjaman :";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(19, 90);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(874, 224);
            this.dg.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtKode);
            this.groupBox2.Controls.Add(this.dg);
            this.groupBox2.Location = new System.Drawing.Point(17, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(912, 326);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detail Item";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(230, 48);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(116, 34);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Kode Barang";
            // 
            // txtKode
            // 
            this.txtKode.Location = new System.Drawing.Point(19, 52);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(185, 26);
            this.txtKode.TabIndex = 14;
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTelepon);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtKeterangan);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtNama);
            this.groupBox4.Location = new System.Drawing.Point(491, 57);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(438, 159);
            this.groupBox4.TabIndex = 35;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Peminjam";
            // 
            // txtTelepon
            // 
            this.txtTelepon.Location = new System.Drawing.Point(230, 48);
            this.txtTelepon.Name = "txtTelepon";
            this.txtTelepon.Size = new System.Drawing.Size(185, 26);
            this.txtTelepon.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "No Telepon";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(19, 108);
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(396, 26);
            this.txtKeterangan.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Keterangan";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Nama Peminjam";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(19, 48);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(185, 26);
            this.txtNama.TabIndex = 7;
            // 
            // TransaksiPeminjamanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 669);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "TransaksiPeminjamanForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaksi Peminjaman";
            this.Load += new System.EventHandler(this.TransaksiPeminjamanForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCetak;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDurasi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNoPinjam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.DateTimePicker dtPinjam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.TextBox txtTelepon;
        private System.Windows.Forms.Button btnOpen;
    }
}