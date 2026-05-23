namespace Assets_Inventory
{
    partial class InputPengadaanBarangForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalHarga = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbGudang = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbPemasok = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSumber = new System.Windows.Forms.TextBox();
            this.dtpTglPengadaan = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbKondisi = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnProsesBelanja = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgDetailBon = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPilihPermintaan = new System.Windows.Forms.Button();
            this.dgPermintaan = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.PilihColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.kodePermintaanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idJurusanNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keteranganKeperluanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusPersetujuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggalPermintaanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggalPersetujuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alasanDisetujuiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPenggunaNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPenyetujuNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetailBon)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPermintaan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
            this.label1.Location = new System.Drawing.Point(14, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 22);
            this.label1.TabIndex = 16;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Pengadaan Barang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "INPUT DATA PENGADAAN BARANG";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtTotalHarga);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.cmbGudang);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cmbPemasok);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtKeterangan);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtSumber);
            this.groupBox1.Controls.Add(this.dtpTglPengadaan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbKondisi);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Location = new System.Drawing.Point(537, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1060, 298);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Pengadaan";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(580, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 20);
            this.label11.TabIndex = 46;
            this.label11.Text = "Total Harga :";
            // 
            // txtTotalHarga
            // 
            this.txtTotalHarga.Location = new System.Drawing.Point(699, 37);
            this.txtTotalHarga.Name = "txtTotalHarga";
            this.txtTotalHarga.Size = new System.Drawing.Size(324, 26);
            this.txtTotalHarga.TabIndex = 45;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(699, 112);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(324, 28);
            this.cmbStatus.TabIndex = 44;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(620, 117);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 20);
            this.label23.TabIndex = 43;
            this.label23.Text = "Status :";
            // 
            // cmbGudang
            // 
            this.cmbGudang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGudang.FormattingEnabled = true;
            this.cmbGudang.Location = new System.Drawing.Point(188, 198);
            this.cmbGudang.Name = "cmbGudang";
            this.cmbGudang.Size = new System.Drawing.Size(326, 28);
            this.cmbGudang.TabIndex = 42;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(94, 198);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(75, 20);
            this.label22.TabIndex = 41;
            this.label22.Text = "Gudang :";
            // 
            // cmbPemasok
            // 
            this.cmbPemasok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPemasok.FormattingEnabled = true;
            this.cmbPemasok.Location = new System.Drawing.Point(188, 115);
            this.cmbPemasok.Name = "cmbPemasok";
            this.cmbPemasok.Size = new System.Drawing.Size(326, 28);
            this.cmbPemasok.TabIndex = 40;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(86, 120);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 20);
            this.label16.TabIndex = 39;
            this.label16.Text = "Pemasok :";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(582, 154);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 20);
            this.label20.TabIndex = 34;
            this.label20.Text = "Keterangan :";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(698, 152);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(326, 76);
            this.txtKeterangan.TabIndex = 33;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(534, 78);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(149, 20);
            this.label18.TabIndex = 31;
            this.label18.Text = "Sumber Perolehan :";
            // 
            // txtSumber
            // 
            this.txtSumber.Location = new System.Drawing.Point(699, 75);
            this.txtSumber.Name = "txtSumber";
            this.txtSumber.Size = new System.Drawing.Size(324, 26);
            this.txtSumber.TabIndex = 30;
            // 
            // dtpTglPengadaan
            // 
            this.dtpTglPengadaan.Location = new System.Drawing.Point(188, 75);
            this.dtpTglPengadaan.Name = "dtpTglPengadaan";
            this.dtpTglPengadaan.Size = new System.Drawing.Size(326, 26);
            this.dtpTglPengadaan.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Tanggal Pengadaan :";
            // 
            // cmbKondisi
            // 
            this.cmbKondisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKondisi.FormattingEnabled = true;
            this.cmbKondisi.Location = new System.Drawing.Point(188, 157);
            this.cmbKondisi.Name = "cmbKondisi";
            this.cmbKondisi.Size = new System.Drawing.Size(326, 28);
            this.cmbKondisi.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(100, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Kondisi :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID Pengadaan :";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(186, 35);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(326, 26);
            this.txtId.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.btnSimpan);
            this.groupBox2.Controls.Add(this.btnProsesBelanja);
            this.groupBox2.Location = new System.Drawing.Point(18, 749);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1580, 88);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(1404, 26);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(20, 29);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnProsesBelanja
            // 
            this.btnProsesBelanja.Location = new System.Drawing.Point(165, 29);
            this.btnProsesBelanja.Name = "btnProsesBelanja";
            this.btnProsesBelanja.Size = new System.Drawing.Size(252, 40);
            this.btnProsesBelanja.TabIndex = 0;
            this.btnProsesBelanja.Text = "Telah Dibelanjakan";
            this.btnProsesBelanja.UseVisualStyleBackColor = true;
            this.btnProsesBelanja.Click += new System.EventHandler(this.btnProsesBelanja_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgDetailBon);
            this.groupBox3.Location = new System.Drawing.Point(18, 395);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(1580, 346);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Barang";
            // 
            // dgDetailBon
            // 
            this.dgDetailBon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetailBon.Location = new System.Drawing.Point(18, 29);
            this.dgDetailBon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgDetailBon.Name = "dgDetailBon";
            this.dgDetailBon.RowHeadersWidth = 62;
            this.dgDetailBon.Size = new System.Drawing.Size(1527, 297);
            this.dgDetailBon.TabIndex = 73;
            this.dgDetailBon.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetailBon_CellEndEdit);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPilihPermintaan);
            this.groupBox4.Controls.Add(this.dgPermintaan);
            this.groupBox4.Location = new System.Drawing.Point(18, 88);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(512, 298);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Permintaan";
            // 
            // btnPilihPermintaan
            // 
            this.btnPilihPermintaan.Location = new System.Drawing.Point(352, 249);
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
            this.dgPermintaan.AutoGenerateColumns = false;
            this.dgPermintaan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPermintaan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PilihColumn,
            this.kodePermintaanDataGridViewTextBoxColumn,
            this.idJurusanNavigationDataGridViewTextBoxColumn,
            this.keteranganKeperluanDataGridViewTextBoxColumn,
            this.statusPersetujuanDataGridViewTextBoxColumn,
            this.tanggalPermintaanDataGridViewTextBoxColumn,
            this.tanggalPersetujuanDataGridViewTextBoxColumn,
            this.alasanDisetujuiDataGridViewTextBoxColumn,
            this.idPenggunaNavigationDataGridViewTextBoxColumn,
            this.idPenyetujuNavigationDataGridViewTextBoxColumn});
            this.dgPermintaan.DataSource = this.bindingSource1;
            this.dgPermintaan.Location = new System.Drawing.Point(18, 29);
            this.dgPermintaan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgPermintaan.Name = "dgPermintaan";
            this.dgPermintaan.ReadOnly = true;
            this.dgPermintaan.RowHeadersWidth = 62;
            this.dgPermintaan.Size = new System.Drawing.Size(474, 214);
            this.dgPermintaan.TabIndex = 0;
            this.dgPermintaan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPermintaan_CellFormatting);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.Permintaan);
            // 
            // PilihColumn
            // 
            this.PilihColumn.HeaderText = "Pilih";
            this.PilihColumn.MinimumWidth = 8;
            this.PilihColumn.Name = "PilihColumn";
            this.PilihColumn.ReadOnly = true;
            this.PilihColumn.Width = 150;
            // 
            // kodePermintaanDataGridViewTextBoxColumn
            // 
            this.kodePermintaanDataGridViewTextBoxColumn.DataPropertyName = "KodePermintaan";
            this.kodePermintaanDataGridViewTextBoxColumn.HeaderText = "Kode Permintaan";
            this.kodePermintaanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.kodePermintaanDataGridViewTextBoxColumn.Name = "kodePermintaanDataGridViewTextBoxColumn";
            this.kodePermintaanDataGridViewTextBoxColumn.ReadOnly = true;
            this.kodePermintaanDataGridViewTextBoxColumn.Width = 150;
            // 
            // idJurusanNavigationDataGridViewTextBoxColumn
            // 
            this.idJurusanNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdJurusanNavigation";
            this.idJurusanNavigationDataGridViewTextBoxColumn.HeaderText = "Jurusan";
            this.idJurusanNavigationDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.idJurusanNavigationDataGridViewTextBoxColumn.Name = "idJurusanNavigationDataGridViewTextBoxColumn";
            this.idJurusanNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            this.idJurusanNavigationDataGridViewTextBoxColumn.Width = 150;
            // 
            // keteranganKeperluanDataGridViewTextBoxColumn
            // 
            this.keteranganKeperluanDataGridViewTextBoxColumn.DataPropertyName = "KeteranganKeperluan";
            this.keteranganKeperluanDataGridViewTextBoxColumn.HeaderText = "Keperluan";
            this.keteranganKeperluanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.keteranganKeperluanDataGridViewTextBoxColumn.Name = "keteranganKeperluanDataGridViewTextBoxColumn";
            this.keteranganKeperluanDataGridViewTextBoxColumn.ReadOnly = true;
            this.keteranganKeperluanDataGridViewTextBoxColumn.Width = 150;
            // 
            // statusPersetujuanDataGridViewTextBoxColumn
            // 
            this.statusPersetujuanDataGridViewTextBoxColumn.DataPropertyName = "StatusPersetujuan";
            this.statusPersetujuanDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusPersetujuanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.statusPersetujuanDataGridViewTextBoxColumn.Name = "statusPersetujuanDataGridViewTextBoxColumn";
            this.statusPersetujuanDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusPersetujuanDataGridViewTextBoxColumn.Width = 150;
            // 
            // tanggalPermintaanDataGridViewTextBoxColumn
            // 
            this.tanggalPermintaanDataGridViewTextBoxColumn.DataPropertyName = "TanggalPermintaan";
            this.tanggalPermintaanDataGridViewTextBoxColumn.HeaderText = "Tanggal Permintaan";
            this.tanggalPermintaanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.tanggalPermintaanDataGridViewTextBoxColumn.Name = "tanggalPermintaanDataGridViewTextBoxColumn";
            this.tanggalPermintaanDataGridViewTextBoxColumn.ReadOnly = true;
            this.tanggalPermintaanDataGridViewTextBoxColumn.Width = 150;
            // 
            // tanggalPersetujuanDataGridViewTextBoxColumn
            // 
            this.tanggalPersetujuanDataGridViewTextBoxColumn.DataPropertyName = "TanggalPersetujuan";
            this.tanggalPersetujuanDataGridViewTextBoxColumn.HeaderText = "Tanggal Persetujuan";
            this.tanggalPersetujuanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.tanggalPersetujuanDataGridViewTextBoxColumn.Name = "tanggalPersetujuanDataGridViewTextBoxColumn";
            this.tanggalPersetujuanDataGridViewTextBoxColumn.ReadOnly = true;
            this.tanggalPersetujuanDataGridViewTextBoxColumn.Width = 150;
            // 
            // alasanDisetujuiDataGridViewTextBoxColumn
            // 
            this.alasanDisetujuiDataGridViewTextBoxColumn.DataPropertyName = "AlasanDisetujui";
            this.alasanDisetujuiDataGridViewTextBoxColumn.HeaderText = "Alasan Disetujui";
            this.alasanDisetujuiDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.alasanDisetujuiDataGridViewTextBoxColumn.Name = "alasanDisetujuiDataGridViewTextBoxColumn";
            this.alasanDisetujuiDataGridViewTextBoxColumn.ReadOnly = true;
            this.alasanDisetujuiDataGridViewTextBoxColumn.Width = 150;
            // 
            // idPenggunaNavigationDataGridViewTextBoxColumn
            // 
            this.idPenggunaNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdPenggunaNavigation";
            this.idPenggunaNavigationDataGridViewTextBoxColumn.HeaderText = "Peminta";
            this.idPenggunaNavigationDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.idPenggunaNavigationDataGridViewTextBoxColumn.Name = "idPenggunaNavigationDataGridViewTextBoxColumn";
            this.idPenggunaNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            this.idPenggunaNavigationDataGridViewTextBoxColumn.Width = 150;
            // 
            // idPenyetujuNavigationDataGridViewTextBoxColumn
            // 
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdPenyetujuNavigation";
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.HeaderText = "Penyetuju";
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.Name = "idPenyetujuNavigationDataGridViewTextBoxColumn";
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.Width = 150;
            // 
            // InputPengadaanBarangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1617, 846);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "InputPengadaanBarangForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Pengadaan Barang";
            this.Load += new System.EventHandler(this.InputPengadaanBarangForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetailBon)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPermintaan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpTglPengadaan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbKondisi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtSumber;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnProsesBelanja;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbGudang;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbPemasok;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgDetailBon;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgPermintaan;
        private System.Windows.Forms.Button btnPilihPermintaan;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTotalHarga;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PilihColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodePermintaanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idJurusanNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keteranganKeperluanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusPersetujuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanggalPermintaanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanggalPersetujuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alasanDisetujuiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPenggunaNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPenyetujuNavigationDataGridViewTextBoxColumn;
    }
}