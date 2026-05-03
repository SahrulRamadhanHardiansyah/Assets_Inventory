namespace Assets_Inventory
{
    partial class InputBangunanForm
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
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtNilai = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtKonstruksi = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtP = new System.Windows.Forms.TextBox();
            this.cmbKondisi = new System.Windows.Forms.ComboBox();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.btynRefresh = new System.Windows.Forms.Button();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLuas = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpTglPembangunan = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(693, 22);
            this.label1.TabIndex = 41;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Bangunan / Gedung Yang Termasuk Inventaris";
            // 
            // txtNilai
            // 
            this.txtNilai.Location = new System.Drawing.Point(216, 341);
            this.txtNilai.Name = "txtNilai";
            this.txtNilai.Size = new System.Drawing.Size(214, 26);
            this.txtNilai.TabIndex = 38;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(49, 344);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 20);
            this.label11.TabIndex = 37;
            this.label11.Text = "Nilai Ekonomi ( Rp) :";
            // 
            // txtKonstruksi
            // 
            this.txtKonstruksi.Location = new System.Drawing.Point(216, 304);
            this.txtKonstruksi.Name = "txtKonstruksi";
            this.txtKonstruksi.Size = new System.Drawing.Size(214, 26);
            this.txtKonstruksi.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(109, 307);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 20);
            this.label10.TabIndex = 35;
            this.label10.Text = "Konstruksi :";
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(216, 266);
            this.txtP.Name = "txtP";
            this.txtP.Size = new System.Drawing.Size(96, 26);
            this.txtP.TabIndex = 34;
            // 
            // cmbKondisi
            // 
            this.cmbKondisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKondisi.FormattingEnabled = true;
            this.cmbKondisi.Location = new System.Drawing.Point(216, 189);
            this.cmbKondisi.Name = "cmbKondisi";
            this.cmbKondisi.Size = new System.Drawing.Size(214, 28);
            this.cmbKondisi.TabIndex = 33;
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(653, 36);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(140, 40);
            this.btnHapus.TabIndex = 5;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(497, 36);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(140, 40);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(342, 36);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(140, 40);
            this.btnBatal.TabIndex = 3;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRecord);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Controls.Add(this.btynRefresh);
            this.groupBox3.Controls.Add(this.btnCari);
            this.groupBox3.Controls.Add(this.txtCari);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(514, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(576, 460);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Bangunan";
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(24, 418);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(116, 20);
            this.lblRecord.TabIndex = 39;
            this.lblRecord.Text = "Total Record :  ";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(28, 98);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(517, 311);
            this.dg.TabIndex = 41;
            // 
            // btynRefresh
            // 
            this.btynRefresh.Location = new System.Drawing.Point(446, 53);
            this.btynRefresh.Name = "btynRefresh";
            this.btynRefresh.Size = new System.Drawing.Size(102, 40);
            this.btynRefresh.TabIndex = 40;
            this.btynRefresh.Text = "Refresh";
            this.btynRefresh.UseVisualStyleBackColor = true;
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(327, 53);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(102, 40);
            this.btnCari.TabIndex = 6;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(28, 60);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(279, 26);
            this.txtCari.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(247, 20);
            this.label12.TabIndex = 39;
            this.label12.Text = "Masukkan Kode/Nama Bangunan";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtKeterangan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtL);
            this.groupBox1.Controls.Add(this.txtNilai);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtKonstruksi);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtP);
            this.groupBox1.Controls.Add(this.cmbKondisi);
            this.groupBox1.Controls.Add(this.txtLuas);
            this.groupBox1.Controls.Add(this.txtNama);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.dtpTglPembangunan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtKode);
            this.groupBox1.Location = new System.Drawing.Point(28, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 460);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Bangunan";
            // 
            // txtLuas
            // 
            this.txtLuas.Location = new System.Drawing.Point(216, 150);
            this.txtLuas.Name = "txtLuas";
            this.txtLuas.Size = new System.Drawing.Size(214, 26);
            this.txtLuas.TabIndex = 32;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(216, 74);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(214, 26);
            this.txtNama.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(132, 192);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 20);
            this.label17.TabIndex = 29;
            this.label17.Text = "Kondisi :";
            // 
            // dtpTglPembangunan
            // 
            this.dtpTglPembangunan.Location = new System.Drawing.Point(216, 228);
            this.dtpTglPembangunan.Name = "dtpTglPembangunan";
            this.dtpTglPembangunan.Size = new System.Drawing.Size(214, 26);
            this.dtpTglPembangunan.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Ukuran P : L ( M ) :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Tanggal Pembangunan :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Luas ( M2 ) :";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(216, 111);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(214, 28);
            this.cmbStatus.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(136, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Status :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Nama Bangunan :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Kode Bangunan :";
            // 
            // txtKode
            // 
            this.txtKode.Location = new System.Drawing.Point(216, 38);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(214, 26);
            this.txtKode.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHapus);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnBatal);
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.btnSimpan);
            this.groupBox2.Controls.Add(this.btnTambah);
            this.groupBox2.Location = new System.Drawing.Point(28, 553);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1062, 88);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(891, 36);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(186, 36);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(29, 36);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 40);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(445, 25);
            this.label2.TabIndex = 40;
            this.label2.Text = "FORM INVENTARIS BANGUNAN / GEDUNG";
            // 
            // txtL
            // 
            this.txtL.Location = new System.Drawing.Point(334, 266);
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(96, 26);
            this.txtL.TabIndex = 39;
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(216, 378);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(214, 60);
            this.txtKeterangan.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 381);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 40;
            this.label4.Text = "Keterangan :";
            // 
            // InputBangunanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 650);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Name = "InputBangunanForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bangunan/Gedung";
            this.Load += new System.EventHandler(this.InputBangunanForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNilai;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtKonstruksi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.ComboBox cmbKondisi;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btynRefresh;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLuas;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dtpTglPembangunan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Label label4;
    }
}