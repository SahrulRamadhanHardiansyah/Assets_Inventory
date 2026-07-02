namespace Assets_Inventory.Forms
{
    partial class InputPengadaanBarangHabisPakaiForm
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
            this.pemasokBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kondisiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.txtTahunAjaran = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSumber = new System.Windows.Forms.ComboBox();
            this.sumberPerolehanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalHarga = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTerapkanPemasok = new System.Windows.Forms.Button();
            this.dgDetailBon = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbPemasok = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPilihPermintaan = new System.Windows.Forms.Button();
            this.dgPermintaan = new System.Windows.Forms.DataGridView();
            this.cmbGudang = new System.Windows.Forms.ComboBox();
            this.gudangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.dtpTglPengadaan = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pemasokBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kondisiBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sumberPerolehanBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetailBon)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPermintaan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gudangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pemasokBindingSource
            // 
            this.pemasokBindingSource.DataSource = typeof(Assets_Inventory.Models.Pemasok);
            // 
            // kondisiBindingSource
            // 
            this.kondisiBindingSource.DataSource = typeof(Assets_Inventory.Models.Kondisi);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.btnSimpan);
            this.groupBox2.Location = new System.Drawing.Point(22, 818);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1278, 88);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(1117, 28);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(18, 31);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // txtTahunAjaran
            // 
            this.txtTahunAjaran.Location = new System.Drawing.Point(202, 105);
            this.txtTahunAjaran.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTahunAjaran.Name = "txtTahunAjaran";
            this.txtTahunAjaran.Size = new System.Drawing.Size(340, 26);
            this.txtTahunAjaran.TabIndex = 49;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 20);
            this.label8.TabIndex = 48;
            this.label8.Text = "Tahun Ajaran :";
            // 
            // cmbSumber
            // 
            this.cmbSumber.DataSource = this.sumberPerolehanBindingSource;
            this.cmbSumber.DisplayMember = "NamaSumber";
            this.cmbSumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSumber.FormattingEnabled = true;
            this.cmbSumber.Location = new System.Drawing.Point(200, 220);
            this.cmbSumber.Name = "cmbSumber";
            this.cmbSumber.Size = new System.Drawing.Size(342, 28);
            this.cmbSumber.TabIndex = 47;
            this.cmbSumber.ValueMember = "IdSumberPerolehan";
            // 
            // sumberPerolehanBindingSource
            // 
            this.sumberPerolehanBindingSource.DataSource = typeof(Assets_Inventory.Models.SumberPerolehan);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(82, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 20);
            this.label11.TabIndex = 46;
            this.label11.Text = "Total Harga :";
            // 
            // txtTotalHarga
            // 
            this.txtTotalHarga.Location = new System.Drawing.Point(200, 145);
            this.txtTotalHarga.Name = "txtTotalHarga";
            this.txtTotalHarga.Size = new System.Drawing.Size(342, 26);
            this.txtTotalHarga.TabIndex = 45;
            this.txtTotalHarga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalHarga_KeyPress);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(200, 259);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(342, 28);
            this.cmbStatus.TabIndex = 44;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTerapkanPemasok);
            this.groupBox3.Controls.Add(this.dgDetailBon);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.cmbPemasok);
            this.groupBox3.Location = new System.Drawing.Point(22, 466);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(1278, 345);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Barang";
            // 
            // btnTerapkanPemasok
            // 
            this.btnTerapkanPemasok.Location = new System.Drawing.Point(448, 290);
            this.btnTerapkanPemasok.Name = "btnTerapkanPemasok";
            this.btnTerapkanPemasok.Size = new System.Drawing.Size(176, 40);
            this.btnTerapkanPemasok.TabIndex = 4;
            this.btnTerapkanPemasok.Text = "Terapkan Pemasok";
            this.btnTerapkanPemasok.UseVisualStyleBackColor = true;
            this.btnTerapkanPemasok.Click += new System.EventHandler(this.btnTerapkanPemasok_Click);
            // 
            // dgDetailBon
            // 
            this.dgDetailBon.AllowUserToAddRows = false;
            this.dgDetailBon.AllowUserToDeleteRows = false;
            this.dgDetailBon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgDetailBon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetailBon.Location = new System.Drawing.Point(16, 31);
            this.dgDetailBon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgDetailBon.Name = "dgDetailBon";
            this.dgDetailBon.RowHeadersWidth = 62;
            this.dgDetailBon.Size = new System.Drawing.Size(1240, 249);
            this.dgDetailBon.TabIndex = 73;
            this.dgDetailBon.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetailBon_CellEndEdit);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 297);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 20);
            this.label16.TabIndex = 39;
            this.label16.Text = "Pemasok :";
            // 
            // cmbPemasok
            // 
            this.cmbPemasok.DataSource = this.pemasokBindingSource;
            this.cmbPemasok.DisplayMember = "NamaPemasok";
            this.cmbPemasok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPemasok.FormattingEnabled = true;
            this.cmbPemasok.Location = new System.Drawing.Point(114, 293);
            this.cmbPemasok.Name = "cmbPemasok";
            this.cmbPemasok.Size = new System.Drawing.Size(326, 28);
            this.cmbPemasok.TabIndex = 40;
            this.cmbPemasok.ValueMember = "IdPemasok";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(121, 264);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 20);
            this.label23.TabIndex = 43;
            this.label23.Text = "Status :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(109, 187);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(75, 20);
            this.label22.TabIndex = 41;
            this.label22.Text = "Gudang :";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(84, 300);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 20);
            this.label20.TabIndex = 34;
            this.label20.Text = "Keterangan :";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(202, 300);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(340, 55);
            this.txtKeterangan.TabIndex = 33;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPilihPermintaan);
            this.groupBox4.Controls.Add(this.dgPermintaan);
            this.groupBox4.Location = new System.Drawing.Point(22, 84);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(705, 372);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Permintaan";
            // 
            // btnPilihPermintaan
            // 
            this.btnPilihPermintaan.Location = new System.Drawing.Point(554, 319);
            this.btnPilihPermintaan.Name = "btnPilihPermintaan";
            this.btnPilihPermintaan.Size = new System.Drawing.Size(140, 40);
            this.btnPilihPermintaan.TabIndex = 3;
            this.btnPilihPermintaan.Text = "Pilih";
            this.btnPilihPermintaan.UseVisualStyleBackColor = true;
            this.btnPilihPermintaan.Click += new System.EventHandler(this.btnPilihPermintaan_Click);
            // 
            // dgPermintaan
            // 
            this.dgPermintaan.AllowUserToAddRows = false;
            this.dgPermintaan.AllowUserToDeleteRows = false;
            this.dgPermintaan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPermintaan.Location = new System.Drawing.Point(16, 31);
            this.dgPermintaan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgPermintaan.Name = "dgPermintaan";
            this.dgPermintaan.ReadOnly = true;
            this.dgPermintaan.RowHeadersWidth = 62;
            this.dgPermintaan.Size = new System.Drawing.Size(678, 280);
            this.dgPermintaan.TabIndex = 0;
            // 
            // cmbGudang
            // 
            this.cmbGudang.DataSource = this.gudangBindingSource;
            this.cmbGudang.DisplayMember = "NamaGudang";
            this.cmbGudang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGudang.FormattingEnabled = true;
            this.cmbGudang.Location = new System.Drawing.Point(200, 182);
            this.cmbGudang.Name = "cmbGudang";
            this.cmbGudang.Size = new System.Drawing.Size(342, 28);
            this.cmbGudang.TabIndex = 42;
            this.cmbGudang.ValueMember = "KodeGudang";
            // 
            // gudangBindingSource
            // 
            this.gudangBindingSource.DataSource = typeof(Assets_Inventory.Models.Gudang);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.Permintaan);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(36, 225);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(149, 20);
            this.label18.TabIndex = 31;
            this.label18.Text = "Sumber Perolehan :";
            // 
            // dtpTglPengadaan
            // 
            this.dtpTglPengadaan.Location = new System.Drawing.Point(202, 67);
            this.dtpTglPengadaan.Name = "dtpTglPengadaan";
            this.dtpTglPengadaan.Size = new System.Drawing.Size(340, 26);
            this.dtpTglPengadaan.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Tanggal Pengadaan :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID Pengadaan :";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(202, 27);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(340, 26);
            this.txtId.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTahunAjaran);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbSumber);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtTotalHarga);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.cmbGudang);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtKeterangan);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.dtpTglPengadaan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Location = new System.Drawing.Point(734, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(566, 371);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Pengadaan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(574, 22);
            this.label1.TabIndex = 22;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Pengadaan Barang Habis Pakai";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(518, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "INPUT DATA PENGADAAN BARANG HABIS PAKAI";
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
            // InputPengadaanBarangHabisPakaiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1322, 958);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "InputPengadaanBarangHabisPakaiForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Pengadaan Barang Habis Pakai Form";
            this.Load += new System.EventHandler(this.InputPengadaanBarangHabisPakaiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pemasokBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kondisiBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sumberPerolehanBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetailBon)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPermintaan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gudangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource pemasokBindingSource;
        private System.Windows.Forms.BindingSource kondisiBindingSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.TextBox txtTahunAjaran;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbSumber;
        private System.Windows.Forms.BindingSource sumberPerolehanBindingSource;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTotalHarga;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTerapkanPemasok;
        private System.Windows.Forms.DataGridView dgDetailBon;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbPemasok;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPilihPermintaan;
        private System.Windows.Forms.DataGridView dgPermintaan;
        private System.Windows.Forms.ComboBox cmbGudang;
        private System.Windows.Forms.BindingSource gudangBindingSource;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DateTimePicker dtpTglPengadaan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
    }
}