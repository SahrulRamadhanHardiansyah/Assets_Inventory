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
            this.kondisiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.kodeBangunanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namaBangunanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusBangunanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.luasBangunanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idKondisiNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggalBangunanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukuranPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukuranLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.konstruksiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nilaiAsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keteranganDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btynRefresh = new System.Windows.Forms.Button();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
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
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kondisiBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 15);
            this.label1.TabIndex = 41;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Bangunan / Gedung Yang Termasuk Inventaris";
            // 
            // txtNilai
            // 
            this.txtNilai.Location = new System.Drawing.Point(144, 222);
            this.txtNilai.Margin = new System.Windows.Forms.Padding(2);
            this.txtNilai.Name = "txtNilai";
            this.txtNilai.Size = new System.Drawing.Size(144, 20);
            this.txtNilai.TabIndex = 38;
            this.txtNilai.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNilai_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 224);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Nilai Ekonomi ( Rp) :";
            // 
            // txtKonstruksi
            // 
            this.txtKonstruksi.Location = new System.Drawing.Point(144, 198);
            this.txtKonstruksi.Margin = new System.Windows.Forms.Padding(2);
            this.txtKonstruksi.Name = "txtKonstruksi";
            this.txtKonstruksi.Size = new System.Drawing.Size(144, 20);
            this.txtKonstruksi.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(73, 200);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Konstruksi :";
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(144, 173);
            this.txtP.Margin = new System.Windows.Forms.Padding(2);
            this.txtP.Name = "txtP";
            this.txtP.Size = new System.Drawing.Size(65, 20);
            this.txtP.TabIndex = 34;
            this.txtP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtP_KeyPress);
            // 
            // cmbKondisi
            // 
            this.cmbKondisi.DataSource = this.kondisiBindingSource;
            this.cmbKondisi.DisplayMember = "NamaKondisi";
            this.cmbKondisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKondisi.FormattingEnabled = true;
            this.cmbKondisi.Location = new System.Drawing.Point(144, 123);
            this.cmbKondisi.Margin = new System.Windows.Forms.Padding(2);
            this.cmbKondisi.Name = "cmbKondisi";
            this.cmbKondisi.Size = new System.Drawing.Size(144, 21);
            this.cmbKondisi.TabIndex = 33;
            this.cmbKondisi.ValueMember = "IdKondisi";
            // 
            // kondisiBindingSource
            // 
            this.kondisiBindingSource.DataSource = typeof(Assets_Inventory.Models.Kondisi);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRecord);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Controls.Add(this.btynRefresh);
            this.groupBox3.Controls.Add(this.btnCari);
            this.groupBox3.Controls.Add(this.txtCari);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(343, 53);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(437, 299);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Bangunan";
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(16, 280);
            this.lblRecord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(81, 13);
            this.lblRecord.TabIndex = 39;
            this.lblRecord.Text = "Total Record :  ";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kodeBangunanDataGridViewTextBoxColumn,
            this.namaBangunanDataGridViewTextBoxColumn,
            this.statusBangunanDataGridViewTextBoxColumn,
            this.luasBangunanDataGridViewTextBoxColumn,
            this.idKondisiNavigationDataGridViewTextBoxColumn,
            this.tanggalBangunanDataGridViewTextBoxColumn,
            this.ukuranPDataGridViewTextBoxColumn,
            this.ukuranLDataGridViewTextBoxColumn,
            this.konstruksiDataGridViewTextBoxColumn,
            this.nilaiAsetDataGridViewTextBoxColumn,
            this.keteranganDataGridViewTextBoxColumn});
            this.dg.DataSource = this.bindingSource1;
            this.dg.Location = new System.Drawing.Point(19, 64);
            this.dg.Margin = new System.Windows.Forms.Padding(2);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(400, 212);
            this.dg.TabIndex = 41;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            this.dg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            // 
            // kodeBangunanDataGridViewTextBoxColumn
            // 
            this.kodeBangunanDataGridViewTextBoxColumn.DataPropertyName = "KodeBangunan";
            this.kodeBangunanDataGridViewTextBoxColumn.HeaderText = "Kode";
            this.kodeBangunanDataGridViewTextBoxColumn.Name = "kodeBangunanDataGridViewTextBoxColumn";
            this.kodeBangunanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // namaBangunanDataGridViewTextBoxColumn
            // 
            this.namaBangunanDataGridViewTextBoxColumn.DataPropertyName = "NamaBangunan";
            this.namaBangunanDataGridViewTextBoxColumn.HeaderText = "Nama";
            this.namaBangunanDataGridViewTextBoxColumn.Name = "namaBangunanDataGridViewTextBoxColumn";
            this.namaBangunanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusBangunanDataGridViewTextBoxColumn
            // 
            this.statusBangunanDataGridViewTextBoxColumn.DataPropertyName = "StatusBangunan";
            this.statusBangunanDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusBangunanDataGridViewTextBoxColumn.Name = "statusBangunanDataGridViewTextBoxColumn";
            this.statusBangunanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // luasBangunanDataGridViewTextBoxColumn
            // 
            this.luasBangunanDataGridViewTextBoxColumn.DataPropertyName = "LuasBangunan";
            this.luasBangunanDataGridViewTextBoxColumn.HeaderText = "Luas (M2)";
            this.luasBangunanDataGridViewTextBoxColumn.Name = "luasBangunanDataGridViewTextBoxColumn";
            this.luasBangunanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idKondisiNavigationDataGridViewTextBoxColumn
            // 
            this.idKondisiNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdKondisiNavigation";
            this.idKondisiNavigationDataGridViewTextBoxColumn.HeaderText = "Kondisi";
            this.idKondisiNavigationDataGridViewTextBoxColumn.Name = "idKondisiNavigationDataGridViewTextBoxColumn";
            this.idKondisiNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tanggalBangunanDataGridViewTextBoxColumn
            // 
            this.tanggalBangunanDataGridViewTextBoxColumn.DataPropertyName = "TanggalBangunan";
            this.tanggalBangunanDataGridViewTextBoxColumn.HeaderText = "Tanggal Pembangunan";
            this.tanggalBangunanDataGridViewTextBoxColumn.Name = "tanggalBangunanDataGridViewTextBoxColumn";
            this.tanggalBangunanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ukuranPDataGridViewTextBoxColumn
            // 
            this.ukuranPDataGridViewTextBoxColumn.DataPropertyName = "UkuranP";
            this.ukuranPDataGridViewTextBoxColumn.HeaderText = "Panjang (M)";
            this.ukuranPDataGridViewTextBoxColumn.Name = "ukuranPDataGridViewTextBoxColumn";
            this.ukuranPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ukuranLDataGridViewTextBoxColumn
            // 
            this.ukuranLDataGridViewTextBoxColumn.DataPropertyName = "UkuranL";
            this.ukuranLDataGridViewTextBoxColumn.HeaderText = "Lebar (M)";
            this.ukuranLDataGridViewTextBoxColumn.Name = "ukuranLDataGridViewTextBoxColumn";
            this.ukuranLDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // konstruksiDataGridViewTextBoxColumn
            // 
            this.konstruksiDataGridViewTextBoxColumn.DataPropertyName = "Konstruksi";
            this.konstruksiDataGridViewTextBoxColumn.HeaderText = "Konstruksi";
            this.konstruksiDataGridViewTextBoxColumn.Name = "konstruksiDataGridViewTextBoxColumn";
            this.konstruksiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nilaiAsetDataGridViewTextBoxColumn
            // 
            this.nilaiAsetDataGridViewTextBoxColumn.DataPropertyName = "NilaiAset";
            this.nilaiAsetDataGridViewTextBoxColumn.HeaderText = "Nilai Aset";
            this.nilaiAsetDataGridViewTextBoxColumn.Name = "nilaiAsetDataGridViewTextBoxColumn";
            this.nilaiAsetDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // keteranganDataGridViewTextBoxColumn
            // 
            this.keteranganDataGridViewTextBoxColumn.DataPropertyName = "Keterangan";
            this.keteranganDataGridViewTextBoxColumn.HeaderText = "Keterangan";
            this.keteranganDataGridViewTextBoxColumn.Name = "keteranganDataGridViewTextBoxColumn";
            this.keteranganDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.AsetBangunan);
            // 
            // btynRefresh
            // 
            this.btynRefresh.Location = new System.Drawing.Point(297, 34);
            this.btynRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btynRefresh.Name = "btynRefresh";
            this.btynRefresh.Size = new System.Drawing.Size(68, 26);
            this.btynRefresh.TabIndex = 40;
            this.btynRefresh.Text = "Refresh";
            this.btynRefresh.UseVisualStyleBackColor = true;
            this.btynRefresh.Click += new System.EventHandler(this.btynRefresh_Click);
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(218, 34);
            this.btnCari.Margin = new System.Windows.Forms.Padding(2);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(68, 26);
            this.btnCari.TabIndex = 6;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(19, 39);
            this.txtCari.Margin = new System.Windows.Forms.Padding(2);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(187, 20);
            this.txtCari.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 21);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(170, 13);
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
            this.groupBox1.Location = new System.Drawing.Point(19, 53);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(313, 299);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Bangunan";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(144, 246);
            this.txtKeterangan.Margin = new System.Windows.Forms.Padding(2);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(144, 40);
            this.txtKeterangan.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 248);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Keterangan :";
            // 
            // txtL
            // 
            this.txtL.Location = new System.Drawing.Point(223, 173);
            this.txtL.Margin = new System.Windows.Forms.Padding(2);
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(65, 20);
            this.txtL.TabIndex = 39;
            this.txtL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtL_KeyPress);
            // 
            // txtLuas
            // 
            this.txtLuas.Location = new System.Drawing.Point(144, 98);
            this.txtLuas.Margin = new System.Windows.Forms.Padding(2);
            this.txtLuas.Name = "txtLuas";
            this.txtLuas.Size = new System.Drawing.Size(144, 20);
            this.txtLuas.TabIndex = 32;
            this.txtLuas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLuas_KeyPress);
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(144, 48);
            this.txtNama.Margin = new System.Windows.Forms.Padding(2);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(144, 20);
            this.txtNama.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(88, 125);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 29;
            this.label17.Text = "Kondisi :";
            // 
            // dtpTglPembangunan
            // 
            this.dtpTglPembangunan.Location = new System.Drawing.Point(144, 148);
            this.dtpTglPembangunan.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTglPembangunan.Name = "dtpTglPembangunan";
            this.dtpTglPembangunan.Size = new System.Drawing.Size(144, 20);
            this.dtpTglPembangunan.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 175);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Ukuran P : L ( M ) :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 148);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Tanggal Pembangunan :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(69, 99);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Luas ( M2 ) :";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(144, 72);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(144, 21);
            this.cmbStatus.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 74);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Status :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 50);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Nama Bangunan :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Kode Bangunan :";
            // 
            // txtKode
            // 
            this.txtKode.Location = new System.Drawing.Point(144, 25);
            this.txtKode.Margin = new System.Windows.Forms.Padding(2);
            this.txtKode.Name = "txtKode";
            this.txtKode.ReadOnly = true;
            this.txtKode.Size = new System.Drawing.Size(144, 20);
            this.txtKode.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUbah);
            this.groupBox2.Controls.Add(this.btnHapus);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnBatal);
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.btnSimpan);
            this.groupBox2.Controls.Add(this.btnTambah);
            this.groupBox2.Location = new System.Drawing.Point(19, 353);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(761, 57);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(117, 18);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(93, 26);
            this.btnUbah.TabIndex = 13;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(408, 18);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(93, 26);
            this.btnHapus.TabIndex = 12;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(505, 18);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(93, 26);
            this.btnImport.TabIndex = 11;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(311, 18);
            this.btnBatal.Margin = new System.Windows.Forms.Padding(2);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(93, 26);
            this.btnBatal.TabIndex = 10;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(650, 18);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(93, 26);
            this.btnTutup.TabIndex = 9;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(214, 18);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(2);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(93, 26);
            this.btnSimpan.TabIndex = 8;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(20, 18);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(93, 26);
            this.btnTambah.TabIndex = 7;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 17);
            this.label2.TabIndex = 40;
            this.label2.Text = "FORM INVENTARIS BANGUNAN / GEDUNG";
            // 
            // InputBangunanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(799, 411);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InputBangunanForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bangunan/Gedung";
            this.Load += new System.EventHandler(this.InputBangunanForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kondisiBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodeBangunanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namaBangunanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusBangunanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn luasBangunanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idKondisiNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanggalBangunanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukuranPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukuranLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn konstruksiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nilaiAsetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keteranganDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource kondisiBindingSource;
    }
}