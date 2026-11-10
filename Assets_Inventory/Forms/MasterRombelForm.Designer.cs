namespace Assets_Inventory
{
    partial class MasterRombelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterRombelForm));
            this.cbAktif = new System.Windows.Forms.CheckBox();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnImport = new System.Windows.Forms.Button();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbJurusan = new System.Windows.Forms.ComboBox();
            this.jurusanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtTingkat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.idRombelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namaRombelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodeRombelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tingkatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idJurusanNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jurusanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbAktif
            // 
            this.cbAktif.AutoSize = true;
            this.cbAktif.Location = new System.Drawing.Point(367, 23);
            this.cbAktif.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbAktif.Name = "cbAktif";
            this.cbAktif.Size = new System.Drawing.Size(47, 17);
            this.cbAktif.TabIndex = 39;
            this.cbAktif.Text = "Aktif";
            this.cbAktif.UseVisualStyleBackColor = true;
            // 
            // txtKode
            // 
            this.txtKode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "KodeRombel", true));
            this.txtKode.Location = new System.Drawing.Point(117, 72);
            this.txtKode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(219, 20);
            this.txtKode.TabIndex = 38;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.Rombel);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 74);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Kode Rombel :";
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBatal);
            this.groupBox3.Controls.Add(this.btnUbah);
            this.groupBox3.Controls.Add(this.btnHapus);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnSimpan);
            this.groupBox3.Controls.Add(this.btnTambah);
            this.groupBox3.Location = new System.Drawing.Point(24, 444);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(476, 93);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(75, 54);
            this.btnBatal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(93, 26);
            this.btnBatal.TabIndex = 13;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(133, 20);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(93, 26);
            this.btnUbah.TabIndex = 12;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(367, 20);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(93, 26);
            this.btnHapus.TabIndex = 11;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(310, 54);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(93, 26);
            this.btnTutup.TabIndex = 9;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(251, 20);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(93, 26);
            this.btnSimpan.TabIndex = 8;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(16, 20);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(93, 26);
            this.btnTambah.TabIndex = 7;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Master Rombel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 17);
            this.label2.TabIndex = 37;
            this.label2.Text = "MASTER ROMBEL";
            // 
            // btnImport
            // 
            this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
            this.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImport.Location = new System.Drawing.Point(442, 29);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(23, 23);
            this.btnImport.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnImport, "Import dari excel");
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtNama
            // 
            this.txtNama.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NamaRombel", true));
            this.txtNama.Location = new System.Drawing.Point(117, 47);
            this.txtNama.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(219, 20);
            this.txtNama.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbJurusan);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTingkat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbAktif);
            this.groupBox1.Controls.Add(this.txtKode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNama);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Location = new System.Drawing.Point(24, 53);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(477, 150);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Rombel";
            // 
            // cmbJurusan
            // 
            this.cmbJurusan.DataSource = this.jurusanBindingSource;
            this.cmbJurusan.DisplayMember = "NamaJurusan";
            this.cmbJurusan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJurusan.FormattingEnabled = true;
            this.cmbJurusan.Location = new System.Drawing.Point(117, 121);
            this.cmbJurusan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbJurusan.Name = "cmbJurusan";
            this.cmbJurusan.Size = new System.Drawing.Size(219, 21);
            this.cmbJurusan.TabIndex = 43;
            this.cmbJurusan.ValueMember = "IdJurusan";
            // 
            // jurusanBindingSource
            // 
            this.jurusanBindingSource.DataSource = typeof(Assets_Inventory.Models.Jurusan);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 123);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Jurusan :";
            // 
            // txtTingkat
            // 
            this.txtTingkat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Tingkat", true));
            this.txtTingkat.Location = new System.Drawing.Point(117, 97);
            this.txtTingkat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTingkat.Name = "txtTingkat";
            this.txtTingkat.Size = new System.Drawing.Size(219, 20);
            this.txtTingkat.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 99);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Tingkat :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nama Rombel :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "ID :";
            // 
            // txtId
            // 
            this.txtId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "IdRombel", true));
            this.txtId.Location = new System.Drawing.Point(117, 22);
            this.txtId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(219, 20);
            this.txtId.TabIndex = 7;
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(239, 29);
            this.btnCari.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(71, 21);
            this.btnCari.TabIndex = 21;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(14, 31);
            this.txtCari.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(219, 20);
            this.txtCari.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Cari Nama/Kode Rombel: ";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idRombelDataGridViewTextBoxColumn,
            this.namaRombelDataGridViewTextBoxColumn,
            this.kodeRombelDataGridViewTextBoxColumn,
            this.tingkatDataGridViewTextBoxColumn,
            this.idJurusanNavigationDataGridViewTextBoxColumn,
            this.isActiveDataGridViewTextBoxColumn});
            this.dg.DataSource = this.bindingSource1;
            this.dg.Location = new System.Drawing.Point(14, 55);
            this.dg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(451, 169);
            this.dg.TabIndex = 4;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            this.dg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            // 
            // idRombelDataGridViewTextBoxColumn
            // 
            this.idRombelDataGridViewTextBoxColumn.DataPropertyName = "IdRombel";
            this.idRombelDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idRombelDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.idRombelDataGridViewTextBoxColumn.Name = "idRombelDataGridViewTextBoxColumn";
            this.idRombelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // namaRombelDataGridViewTextBoxColumn
            // 
            this.namaRombelDataGridViewTextBoxColumn.DataPropertyName = "NamaRombel";
            this.namaRombelDataGridViewTextBoxColumn.HeaderText = "Nama Rombel";
            this.namaRombelDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.namaRombelDataGridViewTextBoxColumn.Name = "namaRombelDataGridViewTextBoxColumn";
            this.namaRombelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kodeRombelDataGridViewTextBoxColumn
            // 
            this.kodeRombelDataGridViewTextBoxColumn.DataPropertyName = "KodeRombel";
            this.kodeRombelDataGridViewTextBoxColumn.HeaderText = "Kode Rombel";
            this.kodeRombelDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.kodeRombelDataGridViewTextBoxColumn.Name = "kodeRombelDataGridViewTextBoxColumn";
            this.kodeRombelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tingkatDataGridViewTextBoxColumn
            // 
            this.tingkatDataGridViewTextBoxColumn.DataPropertyName = "Tingkat";
            this.tingkatDataGridViewTextBoxColumn.HeaderText = "Tingkat";
            this.tingkatDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.tingkatDataGridViewTextBoxColumn.Name = "tingkatDataGridViewTextBoxColumn";
            this.tingkatDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idJurusanNavigationDataGridViewTextBoxColumn
            // 
            this.idJurusanNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdJurusanNavigation";
            this.idJurusanNavigationDataGridViewTextBoxColumn.HeaderText = "Jurusan";
            this.idJurusanNavigationDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.idJurusanNavigationDataGridViewTextBoxColumn.Name = "idJurusanNavigationDataGridViewTextBoxColumn";
            this.idJurusanNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isActiveDataGridViewTextBoxColumn
            // 
            this.isActiveDataGridViewTextBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewTextBoxColumn.HeaderText = "Aktif";
            this.isActiveDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.isActiveDataGridViewTextBoxColumn.Name = "isActiveDataGridViewTextBoxColumn";
            this.isActiveDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCari);
            this.groupBox2.Controls.Add(this.txtCari);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.dg);
            this.groupBox2.Location = new System.Drawing.Point(23, 207);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(477, 233);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Rombel";
            // 
            // MasterRombelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 545);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MasterRombelForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Rombel";
            this.Load += new System.EventHandler(this.MasterRombelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jurusanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbAktif;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label5;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbJurusan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTingkat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn idRombelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namaRombelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodeRombelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tingkatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idJurusanNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActiveDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource jurusanBindingSource;
    }
}