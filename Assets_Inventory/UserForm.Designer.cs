namespace Assets_Inventory
{
    partial class UserForm
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
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUbah = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbMapel = new System.Windows.Forms.ComboBox();
            this.mapelResourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblMapel = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.unitResourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUnit = new System.Windows.Forms.Label();
            this.cmbKelas = new System.Windows.Forms.ComboBox();
            this.kelasResourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblKelas = new System.Windows.Forms.Label();
            this.rbMurid = new System.Windows.Forms.RadioButton();
            this.rbGuru = new System.Windows.Forms.RadioButton();
            this.rbStaff = new System.Windows.Forms.RadioButton();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.peranResourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapelResourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitResourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kelasResourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peranResourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(348, 33);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(140, 40);
            this.btnHapus.TabIndex = 4;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(289, 347);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(140, 40);
            this.btnBatal.TabIndex = 3;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(746, 25);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(29, 33);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 40);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // cmbLevel
            // 
            this.cmbLevel.DataSource = this.peranResourceBindingSource;
            this.cmbLevel.DisplayMember = "Nama_peran";
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(172, 149);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(257, 28);
            this.cmbLevel.TabIndex = 12;
            this.cmbLevel.ValueMember = "Id_peran";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Level Group :";
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(189, 33);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(140, 40);
            this.btnUbah.TabIndex = 5;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUbah);
            this.groupBox3.Controls.Add(this.btnHapus);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnTambah);
            this.groupBox3.Location = new System.Drawing.Point(35, 515);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(911, 96);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(25, 347);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(20, 69);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(403, 318);
            this.dg.TabIndex = 4;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nama User :";
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Username", true));
            this.txtUsername.Location = new System.Drawing.Point(172, 33);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(257, 26);
            this.txtUsername.TabIndex = 7;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.PenggunaResource);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCari);
            this.groupBox2.Controls.Add(this.txtCari);
            this.groupBox2.Controls.Add(this.dg);
            this.groupBox2.Location = new System.Drawing.Point(34, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 403);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data User";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(302, 25);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(121, 36);
            this.btnCari.TabIndex = 6;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(20, 30);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(257, 26);
            this.txtCari.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbMapel);
            this.groupBox1.Controls.Add(this.lblMapel);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Controls.Add(this.lblUnit);
            this.groupBox1.Controls.Add(this.cmbKelas);
            this.groupBox1.Controls.Add(this.lblKelas);
            this.groupBox1.Controls.Add(this.rbMurid);
            this.groupBox1.Controls.Add(this.rbGuru);
            this.groupBox1.Controls.Add(this.rbStaff);
            this.groupBox1.Controls.Add(this.txtPassword2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnBatal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.btnSimpan);
            this.groupBox1.Controls.Add(this.cmbLevel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Location = new System.Drawing.Point(492, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 403);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail User";
            // 
            // cmbMapel
            // 
            this.cmbMapel.DataSource = this.mapelResourceBindingSource;
            this.cmbMapel.DisplayMember = "Nama_mapel";
            this.cmbMapel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapel.Enabled = false;
            this.cmbMapel.FormattingEnabled = true;
            this.cmbMapel.Location = new System.Drawing.Point(172, 235);
            this.cmbMapel.Name = "cmbMapel";
            this.cmbMapel.Size = new System.Drawing.Size(257, 28);
            this.cmbMapel.TabIndex = 25;
            this.cmbMapel.ValueMember = "Id_mapel";
            this.cmbMapel.Visible = false;
            // 
            // mapelResourceBindingSource
            // 
            this.mapelResourceBindingSource.DataSource = typeof(Assets_Inventory.MapelResource);
            // 
            // lblMapel
            // 
            this.lblMapel.AutoSize = true;
            this.lblMapel.Enabled = false;
            this.lblMapel.Location = new System.Drawing.Point(96, 238);
            this.lblMapel.Name = "lblMapel";
            this.lblMapel.Size = new System.Drawing.Size(60, 20);
            this.lblMapel.TabIndex = 24;
            this.lblMapel.Text = "Mapel :";
            this.lblMapel.Visible = false;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DataSource = this.unitResourceBindingSource;
            this.cmbUnit.DisplayMember = "Nama_unit";
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Enabled = false;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(172, 235);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(257, 28);
            this.cmbUnit.TabIndex = 23;
            this.cmbUnit.ValueMember = "Id_unit";
            this.cmbUnit.Visible = false;
            // 
            // unitResourceBindingSource
            // 
            this.unitResourceBindingSource.DataSource = typeof(Assets_Inventory.UnitResource);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Enabled = false;
            this.lblUnit.Location = new System.Drawing.Point(110, 238);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(46, 20);
            this.lblUnit.TabIndex = 22;
            this.lblUnit.Text = "Unit :";
            this.lblUnit.Visible = false;
            // 
            // cmbKelas
            // 
            this.cmbKelas.DataSource = this.kelasResourceBindingSource;
            this.cmbKelas.DisplayMember = "Nama_kelas";
            this.cmbKelas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKelas.Enabled = false;
            this.cmbKelas.FormattingEnabled = true;
            this.cmbKelas.Location = new System.Drawing.Point(172, 235);
            this.cmbKelas.Name = "cmbKelas";
            this.cmbKelas.Size = new System.Drawing.Size(257, 28);
            this.cmbKelas.TabIndex = 21;
            this.cmbKelas.ValueMember = "Id_kelas";
            this.cmbKelas.Visible = false;
            // 
            // kelasResourceBindingSource
            // 
            this.kelasResourceBindingSource.DataSource = typeof(Assets_Inventory.KelasResource);
            // 
            // lblKelas
            // 
            this.lblKelas.AutoSize = true;
            this.lblKelas.Enabled = false;
            this.lblKelas.Location = new System.Drawing.Point(100, 238);
            this.lblKelas.Name = "lblKelas";
            this.lblKelas.Size = new System.Drawing.Size(56, 20);
            this.lblKelas.TabIndex = 20;
            this.lblKelas.Text = "Kelas :";
            this.lblKelas.Visible = false;
            // 
            // rbMurid
            // 
            this.rbMurid.AutoSize = true;
            this.rbMurid.Location = new System.Drawing.Point(273, 196);
            this.rbMurid.Name = "rbMurid";
            this.rbMurid.Size = new System.Drawing.Size(73, 24);
            this.rbMurid.TabIndex = 19;
            this.rbMurid.TabStop = true;
            this.rbMurid.Text = "Murid";
            this.rbMurid.UseVisualStyleBackColor = true;
            this.rbMurid.CheckedChanged += new System.EventHandler(this.rbMurid_CheckedChanged);
            // 
            // rbGuru
            // 
            this.rbGuru.AutoSize = true;
            this.rbGuru.Location = new System.Drawing.Point(187, 196);
            this.rbGuru.Name = "rbGuru";
            this.rbGuru.Size = new System.Drawing.Size(70, 24);
            this.rbGuru.TabIndex = 18;
            this.rbGuru.TabStop = true;
            this.rbGuru.Text = "Guru";
            this.rbGuru.UseVisualStyleBackColor = true;
            this.rbGuru.CheckedChanged += new System.EventHandler(this.rbGuru_CheckedChanged);
            // 
            // rbStaff
            // 
            this.rbStaff.AutoSize = true;
            this.rbStaff.Location = new System.Drawing.Point(102, 196);
            this.rbStaff.Name = "rbStaff";
            this.rbStaff.Size = new System.Drawing.Size(69, 24);
            this.rbStaff.TabIndex = 17;
            this.rbStaff.TabStop = true;
            this.rbStaff.Text = "Staff";
            this.rbStaff.UseVisualStyleBackColor = true;
            // 
            // txtPassword2
            // 
            this.txtPassword2.Location = new System.Drawing.Point(172, 109);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(257, 26);
            this.txtPassword2.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ulangi Password :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(172, 69);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(257, 26);
            this.txtPassword.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(504, 22);
            this.label1.TabIndex = 23;
            this.label1.Text = "Gunakan Form Ini Untuk Menambah User / Pengguna Aplikasi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 25);
            this.label2.TabIndex = 22;
            this.label2.Text = "DATA USER";
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
            // peranResourceBindingSource
            // 
            this.peranResourceBindingSource.DataSource = typeof(Assets_Inventory.PeranResource);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(976, 634);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "UserForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapelResourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitResourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kelasResourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peranResourceBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbMapel;
        private System.Windows.Forms.Label lblMapel;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.ComboBox cmbKelas;
        private System.Windows.Forms.Label lblKelas;
        private System.Windows.Forms.RadioButton rbMurid;
        private System.Windows.Forms.RadioButton rbGuru;
        private System.Windows.Forms.RadioButton rbStaff;
        private System.Windows.Forms.BindingSource unitResourceBindingSource;
        private System.Windows.Forms.BindingSource kelasResourceBindingSource;
        private System.Windows.Forms.BindingSource mapelResourceBindingSource;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource peranResourceBindingSource;
    }
}