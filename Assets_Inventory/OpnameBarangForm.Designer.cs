namespace Assets_Inventory
{
    partial class OpnameBarangForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.dg2 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKodeInv = new System.Windows.Forms.TextBox();
            this.txtNamaBarang = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtTgl = new System.Windows.Forms.DateTimePicker();
            this.cmbKondisi = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dg = new System.Windows.Forms.DataGridView();
            this.kodeinventarisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.masterbarangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lokasiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ruangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jumlahpengadaanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.satuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kondisiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.idopnameasetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodeinventarisDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namaBarangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggalopnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RuangLokasiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kondisiDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keteranganDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.kondisiResourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kondisiResourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Gunakan Form Ini Untuk Melakukan Proses Opname Barang Inventaris";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 33;
            this.label2.Text = "Opname Barang";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dg);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnCari);
            this.groupBox2.Controls.Add(this.txtCari);
            this.groupBox2.Location = new System.Drawing.Point(29, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 644);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Barang";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 15);
            this.label11.TabIndex = 36;
            this.label11.Text = "Cari Kode / Nama Barang";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(302, 55);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(121, 36);
            this.btnCari.TabIndex = 34;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(20, 60);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(257, 26);
            this.txtCari.TabIndex = 35;
            // 
            // dg2
            // 
            this.dg2.AllowUserToAddRows = false;
            this.dg2.AllowUserToDeleteRows = false;
            this.dg2.AutoGenerateColumns = false;
            this.dg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idopnameasetDataGridViewTextBoxColumn,
            this.kodeinventarisDataGridViewTextBoxColumn1,
            this.namaBarangDataGridViewTextBoxColumn,
            this.tanggalopnameDataGridViewTextBoxColumn,
            this.RuangLokasiColumn,
            this.kondisiDataGridViewTextBoxColumn1,
            this.keteranganDataGridViewTextBoxColumn});
            this.dg2.DataSource = this.bindingSource2;
            this.dg2.Location = new System.Drawing.Point(6, 25);
            this.dg2.Name = "dg2";
            this.dg2.ReadOnly = true;
            this.dg2.RowHeadersWidth = 62;
            this.dg2.RowTemplate.Height = 28;
            this.dg2.Size = new System.Drawing.Size(614, 196);
            this.dg2.TabIndex = 0;
            this.dg2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg2_CellClick);
            this.dg2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg2_CellFormatting);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dg2);
            this.groupBox4.Location = new System.Drawing.Point(487, 384);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(626, 227);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Opname";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tanggal :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Kode Inventaris :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Keterangan :";
            // 
            // txtKodeInv
            // 
            this.txtKodeInv.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource2, "Kode_inventaris", true));
            this.txtKodeInv.Location = new System.Drawing.Point(172, 69);
            this.txtKodeInv.Name = "txtKodeInv";
            this.txtKodeInv.ReadOnly = true;
            this.txtKodeInv.Size = new System.Drawing.Size(257, 26);
            this.txtKodeInv.TabIndex = 10;
            // 
            // txtNamaBarang
            // 
            this.txtNamaBarang.Location = new System.Drawing.Point(172, 108);
            this.txtNamaBarang.Name = "txtNamaBarang";
            this.txtNamaBarang.ReadOnly = true;
            this.txtNamaBarang.Size = new System.Drawing.Size(257, 26);
            this.txtNamaBarang.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Nama Barang :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Kondisi Barang :";
            // 
            // dtTgl
            // 
            this.dtTgl.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource2, "Tanggal_opname", true));
            this.dtTgl.Location = new System.Drawing.Point(172, 30);
            this.dtTgl.Name = "dtTgl";
            this.dtTgl.Size = new System.Drawing.Size(257, 26);
            this.dtTgl.TabIndex = 17;
            // 
            // cmbKondisi
            // 
            this.cmbKondisi.DataSource = this.kondisiResourceBindingSource;
            this.cmbKondisi.DisplayMember = "Nama_kondisi";
            this.cmbKondisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKondisi.FormattingEnabled = true;
            this.cmbKondisi.Location = new System.Drawing.Point(172, 148);
            this.cmbKondisi.Name = "cmbKondisi";
            this.cmbKondisi.Size = new System.Drawing.Size(257, 28);
            this.cmbKondisi.TabIndex = 24;
            this.cmbKondisi.ValueMember = "Id_kondisi";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtKeterangan);
            this.groupBox1.Controls.Add(this.cmbKondisi);
            this.groupBox1.Controls.Add(this.dtTgl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtNamaBarang);
            this.groupBox1.Controls.Add(this.txtKodeInv);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(487, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 279);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Opname";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource2, "Keterangan", true));
            this.txtKeterangan.Location = new System.Drawing.Point(172, 188);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(257, 71);
            this.txtKeterangan.TabIndex = 25;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.PengadaanResource);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kodeinventarisDataGridViewTextBoxColumn,
            this.masterbarangDataGridViewTextBoxColumn,
            this.lokasiDataGridViewTextBoxColumn,
            this.ruangDataGridViewTextBoxColumn,
            this.jumlahpengadaanDataGridViewTextBoxColumn,
            this.satuanDataGridViewTextBoxColumn,
            this.kondisiDataGridViewTextBoxColumn});
            this.dg.DataSource = this.bindingSource1;
            this.dg.Location = new System.Drawing.Point(19, 108);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(403, 522);
            this.dg.TabIndex = 37;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            this.dg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            // 
            // kodeinventarisDataGridViewTextBoxColumn
            // 
            this.kodeinventarisDataGridViewTextBoxColumn.DataPropertyName = "Kode_inventaris";
            this.kodeinventarisDataGridViewTextBoxColumn.HeaderText = "Kode Inventaris";
            this.kodeinventarisDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.kodeinventarisDataGridViewTextBoxColumn.Name = "kodeinventarisDataGridViewTextBoxColumn";
            this.kodeinventarisDataGridViewTextBoxColumn.ReadOnly = true;
            this.kodeinventarisDataGridViewTextBoxColumn.Width = 150;
            // 
            // masterbarangDataGridViewTextBoxColumn
            // 
            this.masterbarangDataGridViewTextBoxColumn.DataPropertyName = "Master_barang";
            this.masterbarangDataGridViewTextBoxColumn.HeaderText = "Nama Barang";
            this.masterbarangDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.masterbarangDataGridViewTextBoxColumn.Name = "masterbarangDataGridViewTextBoxColumn";
            this.masterbarangDataGridViewTextBoxColumn.ReadOnly = true;
            this.masterbarangDataGridViewTextBoxColumn.Width = 150;
            // 
            // lokasiDataGridViewTextBoxColumn
            // 
            this.lokasiDataGridViewTextBoxColumn.DataPropertyName = "Lokasi";
            this.lokasiDataGridViewTextBoxColumn.HeaderText = "Lokasi";
            this.lokasiDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.lokasiDataGridViewTextBoxColumn.Name = "lokasiDataGridViewTextBoxColumn";
            this.lokasiDataGridViewTextBoxColumn.ReadOnly = true;
            this.lokasiDataGridViewTextBoxColumn.Width = 150;
            // 
            // ruangDataGridViewTextBoxColumn
            // 
            this.ruangDataGridViewTextBoxColumn.DataPropertyName = "Ruang";
            this.ruangDataGridViewTextBoxColumn.HeaderText = "Ruang";
            this.ruangDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.ruangDataGridViewTextBoxColumn.Name = "ruangDataGridViewTextBoxColumn";
            this.ruangDataGridViewTextBoxColumn.ReadOnly = true;
            this.ruangDataGridViewTextBoxColumn.Width = 150;
            // 
            // jumlahpengadaanDataGridViewTextBoxColumn
            // 
            this.jumlahpengadaanDataGridViewTextBoxColumn.DataPropertyName = "Jumlah_pengadaan";
            this.jumlahpengadaanDataGridViewTextBoxColumn.HeaderText = "Jumlah";
            this.jumlahpengadaanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.jumlahpengadaanDataGridViewTextBoxColumn.Name = "jumlahpengadaanDataGridViewTextBoxColumn";
            this.jumlahpengadaanDataGridViewTextBoxColumn.ReadOnly = true;
            this.jumlahpengadaanDataGridViewTextBoxColumn.Width = 150;
            // 
            // satuanDataGridViewTextBoxColumn
            // 
            this.satuanDataGridViewTextBoxColumn.DataPropertyName = "Satuan";
            this.satuanDataGridViewTextBoxColumn.HeaderText = "Satuan";
            this.satuanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.satuanDataGridViewTextBoxColumn.Name = "satuanDataGridViewTextBoxColumn";
            this.satuanDataGridViewTextBoxColumn.ReadOnly = true;
            this.satuanDataGridViewTextBoxColumn.Width = 150;
            // 
            // kondisiDataGridViewTextBoxColumn
            // 
            this.kondisiDataGridViewTextBoxColumn.DataPropertyName = "Kondisi";
            this.kondisiDataGridViewTextBoxColumn.HeaderText = "Kondisi";
            this.kondisiDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.kondisiDataGridViewTextBoxColumn.Name = "kondisiDataGridViewTextBoxColumn";
            this.kondisiDataGridViewTextBoxColumn.ReadOnly = true;
            this.kondisiDataGridViewTextBoxColumn.Width = 150;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(Assets_Inventory.OpnameAsetResource);
            // 
            // idopnameasetDataGridViewTextBoxColumn
            // 
            this.idopnameasetDataGridViewTextBoxColumn.DataPropertyName = "Id_opname_aset";
            this.idopnameasetDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idopnameasetDataGridViewTextBoxColumn.Name = "idopnameasetDataGridViewTextBoxColumn";
            this.idopnameasetDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kodeinventarisDataGridViewTextBoxColumn1
            // 
            this.kodeinventarisDataGridViewTextBoxColumn1.DataPropertyName = "Kode_inventaris";
            this.kodeinventarisDataGridViewTextBoxColumn1.HeaderText = "Kode Inventaris";
            this.kodeinventarisDataGridViewTextBoxColumn1.Name = "kodeinventarisDataGridViewTextBoxColumn1";
            this.kodeinventarisDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // namaBarangDataGridViewTextBoxColumn
            // 
            this.namaBarangDataGridViewTextBoxColumn.DataPropertyName = "Pengadaan";
            this.namaBarangDataGridViewTextBoxColumn.HeaderText = "Nama Barang";
            this.namaBarangDataGridViewTextBoxColumn.Name = "namaBarangDataGridViewTextBoxColumn";
            this.namaBarangDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tanggalopnameDataGridViewTextBoxColumn
            // 
            this.tanggalopnameDataGridViewTextBoxColumn.DataPropertyName = "Tanggal_opname";
            this.tanggalopnameDataGridViewTextBoxColumn.HeaderText = "Tanggal Opname";
            this.tanggalopnameDataGridViewTextBoxColumn.Name = "tanggalopnameDataGridViewTextBoxColumn";
            this.tanggalopnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // RuangLokasiColumn
            // 
            this.RuangLokasiColumn.DataPropertyName = "Pengadaan";
            this.RuangLokasiColumn.HeaderText = "Ruang/Lokasi";
            this.RuangLokasiColumn.Name = "RuangLokasiColumn";
            this.RuangLokasiColumn.ReadOnly = true;
            // 
            // kondisiDataGridViewTextBoxColumn1
            // 
            this.kondisiDataGridViewTextBoxColumn1.DataPropertyName = "Kondisi";
            this.kondisiDataGridViewTextBoxColumn1.HeaderText = "Kondisi";
            this.kondisiDataGridViewTextBoxColumn1.Name = "kondisiDataGridViewTextBoxColumn1";
            this.kondisiDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // keteranganDataGridViewTextBoxColumn
            // 
            this.keteranganDataGridViewTextBoxColumn.DataPropertyName = "Keterangan";
            this.keteranganDataGridViewTextBoxColumn.HeaderText = "Keterangan";
            this.keteranganDataGridViewTextBoxColumn.Name = "keteranganDataGridViewTextBoxColumn";
            this.keteranganDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUbah);
            this.groupBox3.Controls.Add(this.btnBatal);
            this.groupBox3.Controls.Add(this.btnHapus);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnTambah);
            this.groupBox3.Controls.Add(this.btnSimpan);
            this.groupBox3.Location = new System.Drawing.Point(487, 616);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(626, 127);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(166, 27);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(140, 40);
            this.btnUbah.TabIndex = 7;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(137, 73);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(140, 40);
            this.btnBatal.TabIndex = 6;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(467, 27);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(140, 40);
            this.btnHapus.TabIndex = 4;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(348, 73);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(16, 27);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 40);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(316, 27);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // kondisiResourceBindingSource
            // 
            this.kondisiResourceBindingSource.DataSource = typeof(Assets_Inventory.KondisiResource);
            // 
            // OpnameBarangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1141, 754);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "OpnameBarangForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opname Barang";
            this.Load += new System.EventHandler(this.OpnameBarangForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kondisiResourceBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dg2;
        private System.Windows.Forms.GroupBox groupBox4;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKodeInv;
        private System.Windows.Forms.TextBox txtNamaBarang;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtTgl;
        private System.Windows.Forms.ComboBox cmbKondisi;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodeinventarisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn masterbarangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lokasiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ruangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn jumlahpengadaanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn satuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kondisiDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idopnameasetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodeinventarisDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn namaBarangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanggalopnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuangLokasiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kondisiDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keteranganDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.BindingSource kondisiResourceBindingSource;
    }
}