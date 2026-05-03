namespace Assets_Inventory
{
    partial class NonAktifBangunanForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.dg = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFullname = new System.Windows.Forms.TextBox();
            this.dg2 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 25);
            this.label2.TabIndex = 45;
            this.label2.Text = "NON AKTIF BANGUNAN";
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(24, 27);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 40);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnHapus);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnTambah);
            this.groupBox3.Controls.Add(this.btnSimpan);
            this.groupBox3.Location = new System.Drawing.Point(487, 566);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(626, 84);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(313, 27);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(140, 40);
            this.btnHapus.TabIndex = 4;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(459, 27);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(170, 27);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(302, 55);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(121, 36);
            this.btnCari.TabIndex = 6;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(20, 60);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(257, 26);
            this.txtCari.TabIndex = 13;
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(20, 108);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(403, 429);
            this.dg.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnCari);
            this.groupBox2.Controls.Add(this.txtCari);
            this.groupBox2.Controls.Add(this.dg);
            this.groupBox2.Location = new System.Drawing.Point(29, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 554);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Bangunan";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(239, 22);
            this.label11.TabIndex = 33;
            this.label11.Text = "Cari Kode / Nama Bangunan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(500, 22);
            this.label1.TabIndex = 46;
            this.label1.Text = "Gunakan Form Ini Untuk Menon-aktifkan Bangunan Inventaris";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(172, 147);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(257, 28);
            this.comboBox1.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(68, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "Penyebab :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 187);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(257, 26);
            this.textBox1.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "Keterangan :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(172, 30);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(257, 26);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Nama Bangunan :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Kode Bangunan :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tanggal :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtFullname);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(487, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 230);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Bangunan Non Aktif";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(172, 108);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(257, 26);
            this.txtPassword.TabIndex = 13;
            // 
            // txtFullname
            // 
            this.txtFullname.Location = new System.Drawing.Point(172, 69);
            this.txtFullname.Name = "txtFullname";
            this.txtFullname.Size = new System.Drawing.Size(257, 26);
            this.txtFullname.TabIndex = 10;
            // 
            // dg2
            // 
            this.dg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg2.Location = new System.Drawing.Point(6, 25);
            this.dg2.Name = "dg2";
            this.dg2.RowHeadersWidth = 62;
            this.dg2.RowTemplate.Height = 28;
            this.dg2.Size = new System.Drawing.Size(614, 196);
            this.dg2.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dg2);
            this.groupBox4.Location = new System.Drawing.Point(487, 332);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(626, 227);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Bangunan Non Aktif";
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
            // NonAktifBangunanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1141, 667);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Name = "NonAktifBangunanForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Non Aktif Bangunan";
            this.Load += new System.EventHandler(this.NonAktifBangunanForm_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtFullname;
        private System.Windows.Forms.DataGridView dg2;
        private System.Windows.Forms.GroupBox groupBox4;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
    }
}