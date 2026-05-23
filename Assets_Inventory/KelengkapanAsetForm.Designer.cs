namespace Assets_Inventory
{
    partial class KelengkapanAsetForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbRuang = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbLokasi = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgAset = new System.Windows.Forms.DataGridView();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbGambar = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnHapusGambar = new System.Windows.Forms.Button();
            this.btnSimpanAset = new System.Windows.Forms.Button();
            this.btnNantiSaja = new System.Windows.Forms.Button();
            this.btnSelesai = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtKodeInventaris = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNamaBarang = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNoSeri = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNilaiResidu = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUmurEkonomi = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgAset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGambar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Kelengkapan Aset";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "INPUT KELENGKAPAN ASET";
            // 
            // cmbRuang
            // 
            this.cmbRuang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRuang.FormattingEnabled = true;
            this.cmbRuang.Location = new System.Drawing.Point(167, 260);
            this.cmbRuang.Name = "cmbRuang";
            this.cmbRuang.Size = new System.Drawing.Size(264, 28);
            this.cmbRuang.TabIndex = 42;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(87, 263);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 20);
            this.label16.TabIndex = 41;
            this.label16.Text = "Ruang :";
            // 
            // cmbLokasi
            // 
            this.cmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLokasi.FormattingEnabled = true;
            this.cmbLokasi.Location = new System.Drawing.Point(167, 294);
            this.cmbLokasi.Name = "cmbLokasi";
            this.cmbLokasi.Size = new System.Drawing.Size(264, 28);
            this.cmbLokasi.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "Lokasi :";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(552, 70);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(264, 28);
            this.cmbStatus.TabIndex = 46;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(476, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 45;
            this.label4.Text = "Status :";
            // 
            // dgAset
            // 
            this.dgAset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAset.Location = new System.Drawing.Point(29, 342);
            this.dgAset.Name = "dgAset";
            this.dgAset.RowTemplate.Height = 28;
            this.dgAset.Size = new System.Drawing.Size(866, 218);
            this.dgAset.TabIndex = 47;
            this.dgAset.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAset_CellClick);
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
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-23, -46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pbGambar
            // 
            this.pbGambar.Location = new System.Drawing.Point(479, 134);
            this.pbGambar.Name = "pbGambar";
            this.pbGambar.Size = new System.Drawing.Size(129, 164);
            this.pbGambar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGambar.TabIndex = 48;
            this.pbGambar.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(475, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 49;
            this.label5.Text = "Gambar :";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(624, 134);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(140, 40);
            this.btnBrowse.TabIndex = 50;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnHapusGambar
            // 
            this.btnHapusGambar.Location = new System.Drawing.Point(624, 180);
            this.btnHapusGambar.Name = "btnHapusGambar";
            this.btnHapusGambar.Size = new System.Drawing.Size(140, 40);
            this.btnHapusGambar.TabIndex = 51;
            this.btnHapusGambar.Text = "Hapus Gambar";
            this.btnHapusGambar.UseVisualStyleBackColor = true;
            this.btnHapusGambar.Click += new System.EventHandler(this.btnHapusGambar_Click);
            // 
            // btnSimpanAset
            // 
            this.btnSimpanAset.Location = new System.Drawing.Point(29, 578);
            this.btnSimpanAset.Name = "btnSimpanAset";
            this.btnSimpanAset.Size = new System.Drawing.Size(140, 40);
            this.btnSimpanAset.TabIndex = 52;
            this.btnSimpanAset.Text = "Simpan";
            this.btnSimpanAset.UseVisualStyleBackColor = true;
            this.btnSimpanAset.Click += new System.EventHandler(this.btnSimpanAset_Click);
            // 
            // btnNantiSaja
            // 
            this.btnNantiSaja.Location = new System.Drawing.Point(755, 578);
            this.btnNantiSaja.Name = "btnNantiSaja";
            this.btnNantiSaja.Size = new System.Drawing.Size(140, 40);
            this.btnNantiSaja.TabIndex = 53;
            this.btnNantiSaja.Text = "Nanti";
            this.btnNantiSaja.UseVisualStyleBackColor = true;
            this.btnNantiSaja.Click += new System.EventHandler(this.btnNantiSaja_Click);
            // 
            // btnSelesai
            // 
            this.btnSelesai.Location = new System.Drawing.Point(187, 578);
            this.btnSelesai.Name = "btnSelesai";
            this.btnSelesai.Size = new System.Drawing.Size(140, 40);
            this.btnSelesai.TabIndex = 54;
            this.btnSelesai.Text = "Selesai";
            this.btnSelesai.UseVisualStyleBackColor = true;
            this.btnSelesai.Click += new System.EventHandler(this.btnSelesai_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 20);
            this.label11.TabIndex = 56;
            this.label11.Text = "Kode Inventaris :";
            // 
            // txtKodeInventaris
            // 
            this.txtKodeInventaris.Location = new System.Drawing.Point(167, 70);
            this.txtKodeInventaris.Name = "txtKodeInventaris";
            this.txtKodeInventaris.ReadOnly = true;
            this.txtKodeInventaris.Size = new System.Drawing.Size(264, 26);
            this.txtKodeInventaris.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 20);
            this.label6.TabIndex = 58;
            this.label6.Text = "Nama Barang :";
            // 
            // txtNamaBarang
            // 
            this.txtNamaBarang.Location = new System.Drawing.Point(167, 101);
            this.txtNamaBarang.Name = "txtNamaBarang";
            this.txtNamaBarang.ReadOnly = true;
            this.txtNamaBarang.Size = new System.Drawing.Size(264, 26);
            this.txtNamaBarang.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(83, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.TabIndex = 60;
            this.label7.Text = "No Seri :";
            // 
            // txtNoSeri
            // 
            this.txtNoSeri.Location = new System.Drawing.Point(167, 133);
            this.txtNoSeri.Name = "txtNoSeri";
            this.txtNoSeri.Size = new System.Drawing.Size(264, 26);
            this.txtNoSeri.TabIndex = 59;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 231);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 66;
            this.label8.Text = "Keterangan :";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(167, 228);
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(264, 26);
            this.txtKeterangan.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(52, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 20);
            this.label9.TabIndex = 64;
            this.label9.Text = "Nilai Residu :";
            // 
            // txtNilaiResidu
            // 
            this.txtNilaiResidu.Location = new System.Drawing.Point(167, 196);
            this.txtNilaiResidu.Name = "txtNilaiResidu";
            this.txtNilaiResidu.Size = new System.Drawing.Size(264, 26);
            this.txtNilaiResidu.TabIndex = 63;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 20);
            this.label10.TabIndex = 62;
            this.label10.Text = "Umur Ekonomi :";
            // 
            // txtUmurEkonomi
            // 
            this.txtUmurEkonomi.Location = new System.Drawing.Point(167, 165);
            this.txtUmurEkonomi.Name = "txtUmurEkonomi";
            this.txtUmurEkonomi.Size = new System.Drawing.Size(264, 26);
            this.txtUmurEkonomi.TabIndex = 61;
            // 
            // KelengkapanAsetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(920, 630);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNilaiResidu);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtUmurEkonomi);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNoSeri);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNamaBarang);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtKodeInventaris);
            this.Controls.Add(this.btnSelesai);
            this.Controls.Add(this.btnNantiSaja);
            this.Controls.Add(this.btnSimpanAset);
            this.Controls.Add(this.btnHapusGambar);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbGambar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dgAset);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbLokasi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbRuang);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "KelengkapanAsetForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kelengkapan Aset Form";
            this.Load += new System.EventHandler(this.KelengkapanAsetForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGambar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbRuang;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbLokasi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgAset;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbGambar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnHapusGambar;
        private System.Windows.Forms.Button btnSimpanAset;
        private System.Windows.Forms.Button btnNantiSaja;
        private System.Windows.Forms.Button btnSelesai;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtKodeInventaris;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNamaBarang;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNoSeri;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNilaiResidu;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUmurEkonomi;
    }
}