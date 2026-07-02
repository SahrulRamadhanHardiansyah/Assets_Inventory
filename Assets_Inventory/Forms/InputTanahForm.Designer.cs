namespace Assets_Inventory
{
    partial class InputTanahForm
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
            this.label17 = new System.Windows.Forms.Label();
            this.dtpTglPerolehan = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPenggunaan = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.txtHarga = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLetakTanah = new System.Windows.Forms.TextBox();
            this.cmbSumber = new System.Windows.Forms.ComboBox();
            this.txtLuas = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtNoSertif = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.kodeTanahDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomorSertifikatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namaPemilikDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.luasTanahDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.letakTanahDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusHakDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nilaiAsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.penggunaanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggalPerolehanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumberPerolehanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btynRefresh = new System.Windows.Forms.Button();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
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
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(35, 150);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 13);
            this.label17.TabIndex = 29;
            this.label17.Text = "Sumber Perolehan :";
            // 
            // dtpTglPerolehan
            // 
            this.dtpTglPerolehan.Location = new System.Drawing.Point(144, 174);
            this.dtpTglPerolehan.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTglPerolehan.Name = "dtpTglPerolehan";
            this.dtpTglPerolehan.Size = new System.Drawing.Size(144, 20);
            this.dtpTglPerolehan.TabIndex = 26;
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
            this.groupBox2.Location = new System.Drawing.Point(20, 349);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(761, 57);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(116, 21);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(93, 26);
            this.btnUbah.TabIndex = 6;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(407, 21);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(93, 26);
            this.btnHapus.TabIndex = 5;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(504, 21);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(93, 26);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(310, 21);
            this.btnBatal.Margin = new System.Windows.Forms.Padding(2);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(93, 26);
            this.btnBatal.TabIndex = 3;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(649, 21);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(93, 26);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(213, 21);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(2);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(93, 26);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(19, 21);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(93, 26);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPenggunaan);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtHarga);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtLetakTanah);
            this.groupBox1.Controls.Add(this.cmbSumber);
            this.groupBox1.Controls.Add(this.txtLuas);
            this.groupBox1.Controls.Add(this.txtNama);
            this.groupBox1.Controls.Add(this.txtNoSertif);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.dtpTglPerolehan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtKode);
            this.groupBox1.Location = new System.Drawing.Point(20, 53);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(313, 291);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Tanah";
            // 
            // txtPenggunaan
            // 
            this.txtPenggunaan.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Penggunaan", true));
            this.txtPenggunaan.Location = new System.Drawing.Point(144, 247);
            this.txtPenggunaan.Margin = new System.Windows.Forms.Padding(2);
            this.txtPenggunaan.Name = "txtPenggunaan";
            this.txtPenggunaan.Size = new System.Drawing.Size(144, 20);
            this.txtPenggunaan.TabIndex = 38;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.AsetTanah);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(61, 249);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Penggunaan :";
            // 
            // txtHarga
            // 
            this.txtHarga.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NilaiAset", true));
            this.txtHarga.Location = new System.Drawing.Point(144, 223);
            this.txtHarga.Margin = new System.Windows.Forms.Padding(2);
            this.txtHarga.Name = "txtHarga";
            this.txtHarga.Size = new System.Drawing.Size(144, 20);
            this.txtHarga.TabIndex = 36;
            this.txtHarga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHarga_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(65, 225);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Harga ( Rp ) :";
            // 
            // txtLetakTanah
            // 
            this.txtLetakTanah.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "LetakTanah", true));
            this.txtLetakTanah.Location = new System.Drawing.Point(144, 198);
            this.txtLetakTanah.Margin = new System.Windows.Forms.Padding(2);
            this.txtLetakTanah.Name = "txtLetakTanah";
            this.txtLetakTanah.Size = new System.Drawing.Size(144, 20);
            this.txtLetakTanah.TabIndex = 34;
            // 
            // cmbSumber
            // 
            this.cmbSumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSumber.FormattingEnabled = true;
            this.cmbSumber.Location = new System.Drawing.Point(144, 148);
            this.cmbSumber.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSumber.Name = "cmbSumber";
            this.cmbSumber.Size = new System.Drawing.Size(144, 21);
            this.cmbSumber.TabIndex = 33;
            // 
            // txtLuas
            // 
            this.txtLuas.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "LuasTanah", true));
            this.txtLuas.Location = new System.Drawing.Point(144, 123);
            this.txtLuas.Margin = new System.Windows.Forms.Padding(2);
            this.txtLuas.Name = "txtLuas";
            this.txtLuas.Size = new System.Drawing.Size(144, 20);
            this.txtLuas.TabIndex = 32;
            this.txtLuas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLuas_KeyPress);
            // 
            // txtNama
            // 
            this.txtNama.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NamaPemilik", true));
            this.txtNama.Location = new System.Drawing.Point(144, 72);
            this.txtNama.Margin = new System.Windows.Forms.Padding(2);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(144, 20);
            this.txtNama.TabIndex = 31;
            // 
            // txtNoSertif
            // 
            this.txtNoSertif.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NomorSertifikat", true));
            this.txtNoSertif.Location = new System.Drawing.Point(144, 46);
            this.txtNoSertif.Margin = new System.Windows.Forms.Padding(2);
            this.txtNoSertif.Name = "txtNoSertif";
            this.txtNoSertif.Size = new System.Drawing.Size(144, 20);
            this.txtNoSertif.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(63, 200);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Letak Tanah :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 175);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Tanggal Perolehan :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(70, 125);
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
            this.cmbStatus.Location = new System.Drawing.Point(144, 98);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(144, 21);
            this.cmbStatus.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 99);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Status :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "No Sertifikat :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nama Pemilik :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Kode Tanah :";
            // 
            // txtKode
            // 
            this.txtKode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "KodeTanah", true));
            this.txtKode.Location = new System.Drawing.Point(144, 23);
            this.txtKode.Margin = new System.Windows.Forms.Padding(2);
            this.txtKode.Name = "txtKode";
            this.txtKode.ReadOnly = true;
            this.txtKode.Size = new System.Drawing.Size(144, 20);
            this.txtKode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Barang Yang Berupa Tanah";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "FORM INVENTARIS TANAH";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRecord);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Controls.Add(this.btynRefresh);
            this.groupBox3.Controls.Add(this.btnCari);
            this.groupBox3.Controls.Add(this.txtCari);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(344, 53);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(437, 291);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Tanah";
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(16, 270);
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
            this.kodeTanahDataGridViewTextBoxColumn,
            this.nomorSertifikatDataGridViewTextBoxColumn,
            this.namaPemilikDataGridViewTextBoxColumn,
            this.luasTanahDataGridViewTextBoxColumn,
            this.letakTanahDataGridViewTextBoxColumn,
            this.statusHakDataGridViewTextBoxColumn,
            this.nilaiAsetDataGridViewTextBoxColumn,
            this.penggunaanDataGridViewTextBoxColumn,
            this.tanggalPerolehanDataGridViewTextBoxColumn,
            this.sumberPerolehanDataGridViewTextBoxColumn});
            this.dg.DataSource = this.bindingSource1;
            this.dg.Location = new System.Drawing.Point(19, 62);
            this.dg.Margin = new System.Windows.Forms.Padding(2);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(399, 202);
            this.dg.TabIndex = 41;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            this.dg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            // 
            // kodeTanahDataGridViewTextBoxColumn
            // 
            this.kodeTanahDataGridViewTextBoxColumn.DataPropertyName = "KodeTanah";
            this.kodeTanahDataGridViewTextBoxColumn.HeaderText = "Kode Tanah";
            this.kodeTanahDataGridViewTextBoxColumn.Name = "kodeTanahDataGridViewTextBoxColumn";
            this.kodeTanahDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nomorSertifikatDataGridViewTextBoxColumn
            // 
            this.nomorSertifikatDataGridViewTextBoxColumn.DataPropertyName = "NomorSertifikat";
            this.nomorSertifikatDataGridViewTextBoxColumn.HeaderText = "Nomor Sertifikat";
            this.nomorSertifikatDataGridViewTextBoxColumn.Name = "nomorSertifikatDataGridViewTextBoxColumn";
            this.nomorSertifikatDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // namaPemilikDataGridViewTextBoxColumn
            // 
            this.namaPemilikDataGridViewTextBoxColumn.DataPropertyName = "NamaPemilik";
            this.namaPemilikDataGridViewTextBoxColumn.HeaderText = "Nama Pemilik";
            this.namaPemilikDataGridViewTextBoxColumn.Name = "namaPemilikDataGridViewTextBoxColumn";
            this.namaPemilikDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // luasTanahDataGridViewTextBoxColumn
            // 
            this.luasTanahDataGridViewTextBoxColumn.DataPropertyName = "LuasTanah";
            this.luasTanahDataGridViewTextBoxColumn.HeaderText = "Luas Tanah (M2)";
            this.luasTanahDataGridViewTextBoxColumn.Name = "luasTanahDataGridViewTextBoxColumn";
            this.luasTanahDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // letakTanahDataGridViewTextBoxColumn
            // 
            this.letakTanahDataGridViewTextBoxColumn.DataPropertyName = "LetakTanah";
            this.letakTanahDataGridViewTextBoxColumn.HeaderText = "Letak Tanah";
            this.letakTanahDataGridViewTextBoxColumn.Name = "letakTanahDataGridViewTextBoxColumn";
            this.letakTanahDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusHakDataGridViewTextBoxColumn
            // 
            this.statusHakDataGridViewTextBoxColumn.DataPropertyName = "StatusHak";
            this.statusHakDataGridViewTextBoxColumn.HeaderText = "Status Hak";
            this.statusHakDataGridViewTextBoxColumn.Name = "statusHakDataGridViewTextBoxColumn";
            this.statusHakDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nilaiAsetDataGridViewTextBoxColumn
            // 
            this.nilaiAsetDataGridViewTextBoxColumn.DataPropertyName = "NilaiAset";
            this.nilaiAsetDataGridViewTextBoxColumn.HeaderText = "Nilai Aset";
            this.nilaiAsetDataGridViewTextBoxColumn.Name = "nilaiAsetDataGridViewTextBoxColumn";
            this.nilaiAsetDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // penggunaanDataGridViewTextBoxColumn
            // 
            this.penggunaanDataGridViewTextBoxColumn.DataPropertyName = "Penggunaan";
            this.penggunaanDataGridViewTextBoxColumn.HeaderText = "Penggunaan";
            this.penggunaanDataGridViewTextBoxColumn.Name = "penggunaanDataGridViewTextBoxColumn";
            this.penggunaanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tanggalPerolehanDataGridViewTextBoxColumn
            // 
            this.tanggalPerolehanDataGridViewTextBoxColumn.DataPropertyName = "TanggalPerolehan";
            this.tanggalPerolehanDataGridViewTextBoxColumn.HeaderText = "Tanggal Perolehan";
            this.tanggalPerolehanDataGridViewTextBoxColumn.Name = "tanggalPerolehanDataGridViewTextBoxColumn";
            this.tanggalPerolehanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sumberPerolehanDataGridViewTextBoxColumn
            // 
            this.sumberPerolehanDataGridViewTextBoxColumn.DataPropertyName = "SumberPerolehan";
            this.sumberPerolehanDataGridViewTextBoxColumn.HeaderText = "Sumber Perolehan";
            this.sumberPerolehanDataGridViewTextBoxColumn.Name = "sumberPerolehanDataGridViewTextBoxColumn";
            this.sumberPerolehanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // btynRefresh
            // 
            this.btynRefresh.Location = new System.Drawing.Point(297, 32);
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
            this.btnCari.Location = new System.Drawing.Point(218, 32);
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
            this.txtCari.Location = new System.Drawing.Point(19, 37);
            this.txtCari.Margin = new System.Windows.Forms.Padding(2);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(187, 20);
            this.txtCari.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 20);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Masukkan Kode/Nama Pemilik";
            // 
            // InputTanahForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(799, 411);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InputTanahForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tanah";
            this.Load += new System.EventHandler(this.InputTanahForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dtpTglPerolehan;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtNoSertif;
        private System.Windows.Forms.TextBox txtPenggunaan;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtHarga;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLetakTanah;
        private System.Windows.Forms.ComboBox cmbSumber;
        private System.Windows.Forms.TextBox txtLuas;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btynRefresh;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodeTanahDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomorSertifikatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namaPemilikDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn luasTanahDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn letakTanahDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusHakDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nilaiAsetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn penggunaanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanggalPerolehanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sumberPerolehanDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnUbah;
    }
}