namespace Assets_Inventory.Forms
{
    partial class InputPermintaanBarangHabisPakaiForm
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
            this.idDetailPermintaanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idMasterBarangNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jumlahDimintaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alasanKebutuhanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.jurusanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.HapusColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTahunAjaran = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAlasan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBarang = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.dtTgl = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.btnBrowseBarang = new System.Windows.Forms.Button();
            this.cmbJurusan = new System.Windows.Forms.ComboBox();
            this.bindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jurusanBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // idDetailPermintaanDataGridViewTextBoxColumn
            // 
            this.idDetailPermintaanDataGridViewTextBoxColumn.DataPropertyName = "IdDetailPermintaan";
            this.idDetailPermintaanDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idDetailPermintaanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.idDetailPermintaanDataGridViewTextBoxColumn.Name = "idDetailPermintaanDataGridViewTextBoxColumn";
            this.idDetailPermintaanDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDetailPermintaanDataGridViewTextBoxColumn.Width = 150;
            // 
            // idMasterBarangNavigationDataGridViewTextBoxColumn
            // 
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdMasterBarangNavigation";
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.HeaderText = "Nama Barang";
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.Name = "idMasterBarangNavigationDataGridViewTextBoxColumn";
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.Width = 150;
            // 
            // jumlahDimintaDataGridViewTextBoxColumn
            // 
            this.jumlahDimintaDataGridViewTextBoxColumn.DataPropertyName = "JumlahDiminta";
            this.jumlahDimintaDataGridViewTextBoxColumn.HeaderText = "Jumlah";
            this.jumlahDimintaDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.jumlahDimintaDataGridViewTextBoxColumn.Name = "jumlahDimintaDataGridViewTextBoxColumn";
            this.jumlahDimintaDataGridViewTextBoxColumn.ReadOnly = true;
            this.jumlahDimintaDataGridViewTextBoxColumn.Width = 150;
            // 
            // alasanKebutuhanDataGridViewTextBoxColumn
            // 
            this.alasanKebutuhanDataGridViewTextBoxColumn.DataPropertyName = "AlasanKebutuhan";
            this.alasanKebutuhanDataGridViewTextBoxColumn.HeaderText = "Alasan";
            this.alasanKebutuhanDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.alasanKebutuhanDataGridViewTextBoxColumn.Name = "alasanKebutuhanDataGridViewTextBoxColumn";
            this.alasanKebutuhanDataGridViewTextBoxColumn.ReadOnly = true;
            this.alasanKebutuhanDataGridViewTextBoxColumn.Width = 150;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.DetailPermintaan);
            // 
            // jurusanBindingSource
            // 
            this.jurusanBindingSource.DataSource = typeof(Assets_Inventory.Models.Jurusan);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(382, 25);
            this.label2.TabIndex = 23;
            this.label2.Text = "INPUT DATA PERMINTAAN BARANG";
            // 
            // HapusColumn
            // 
            this.HapusColumn.HeaderText = "Aksi";
            this.HapusColumn.MinimumWidth = 8;
            this.HapusColumn.Name = "HapusColumn";
            this.HapusColumn.ReadOnly = true;
            this.HapusColumn.Text = "Hapus";
            this.HapusColumn.UseColumnTextForButtonValue = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 22);
            this.label1.TabIndex = 24;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Permintaan Barang";
            // 
            // txtTahunAjaran
            // 
            this.txtTahunAjaran.Location = new System.Drawing.Point(710, 37);
            this.txtTahunAjaran.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTahunAjaran.Name = "txtTahunAjaran";
            this.txtTahunAjaran.Size = new System.Drawing.Size(298, 26);
            this.txtTahunAjaran.TabIndex = 45;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTahunAjaran);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtAlasan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtJumlah);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBarang);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.dg);
            this.groupBox1.Controls.Add(this.dtTgl);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.btnTambah);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtKeterangan);
            this.groupBox1.Controls.Add(this.btnBrowseBarang);
            this.groupBox1.Controls.Add(this.cmbJurusan);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtKode);
            this.groupBox1.Location = new System.Drawing.Point(33, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1044, 623);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Barang";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(581, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 20);
            this.label8.TabIndex = 44;
            this.label8.Text = "Tahun Ajaran :";
            // 
            // txtAlasan
            // 
            this.txtAlasan.Location = new System.Drawing.Point(179, 196);
            this.txtAlasan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAlasan.Name = "txtAlasan";
            this.txtAlasan.Size = new System.Drawing.Size(336, 26);
            this.txtAlasan.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(96, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "Alasan :";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(179, 156);
            this.txtJumlah.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(336, 26);
            this.txtJumlah.TabIndex = 41;
            this.txtJumlah.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJumlah_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 40;
            this.label6.Text = "Jumlah :";
            // 
            // txtBarang
            // 
            this.txtBarang.Location = new System.Drawing.Point(179, 116);
            this.txtBarang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBarang.Name = "txtBarang";
            this.txtBarang.Size = new System.Drawing.Size(336, 26);
            this.txtBarang.TabIndex = 39;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(377, 244);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 40);
            this.btnRefresh.TabIndex = 38;
            this.btnRefresh.Text = "Reset";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.HapusColumn});
            this.dg.DataSource = this.bindingSource2;
            this.dg.Location = new System.Drawing.Point(24, 291);
            this.dg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersWidth = 62;
            this.dg.Size = new System.Drawing.Size(986, 315);
            this.dg.TabIndex = 37;
            this.dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellContentClick);
            this.dg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdDetailPermintaan";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "IdMasterBarangNavigation";
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama Barang";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "JumlahDiminta";
            this.dataGridViewTextBoxColumn3.HeaderText = "Jumlah";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "AlasanKebutuhan";
            this.dataGridViewTextBoxColumn4.HeaderText = "Alasan";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(Assets_Inventory.Models.DetailPermintaan);
            // 
            // dtTgl
            // 
            this.dtTgl.Location = new System.Drawing.Point(710, 77);
            this.dtTgl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtTgl.Name = "dtTgl";
            this.dtTgl.Size = new System.Drawing.Size(298, 26);
            this.dtTgl.TabIndex = 36;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(618, 82);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 20);
            this.label21.TabIndex = 35;
            this.label21.Text = "Tanggal :";
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(232, 244);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 40);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah Barang";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(594, 122);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 20);
            this.label20.TabIndex = 34;
            this.label20.Text = "Keterangan :";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(710, 120);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(298, 82);
            this.txtKeterangan.TabIndex = 33;
            // 
            // btnBrowseBarang
            // 
            this.btnBrowseBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseBarang.Location = new System.Drawing.Point(530, 117);
            this.btnBrowseBarang.Name = "btnBrowseBarang";
            this.btnBrowseBarang.Size = new System.Drawing.Size(42, 31);
            this.btnBrowseBarang.TabIndex = 27;
            this.btnBrowseBarang.Text = "...";
            this.btnBrowseBarang.UseVisualStyleBackColor = true;
            this.btnBrowseBarang.Click += new System.EventHandler(this.btnBrowseBarang_Click);
            // 
            // cmbJurusan
            // 
            this.cmbJurusan.DataSource = this.bindingSource3;
            this.cmbJurusan.DisplayMember = "NamaJurusan";
            this.cmbJurusan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJurusan.Enabled = false;
            this.cmbJurusan.FormattingEnabled = true;
            this.cmbJurusan.Location = new System.Drawing.Point(179, 77);
            this.cmbJurusan.Name = "cmbJurusan";
            this.cmbJurusan.Size = new System.Drawing.Size(336, 28);
            this.cmbJurusan.TabIndex = 6;
            this.cmbJurusan.ValueMember = "IdJurusan";
            // 
            // bindingSource3
            // 
            this.bindingSource3.DataSource = typeof(Assets_Inventory.Models.Jurusan);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(89, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Jurusan :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nama Barang :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Kode Permintaan :";
            // 
            // txtKode
            // 
            this.txtKode.Location = new System.Drawing.Point(179, 37);
            this.txtKode.Name = "txtKode";
            this.txtKode.ReadOnly = true;
            this.txtKode.Size = new System.Drawing.Size(336, 26);
            this.txtKode.TabIndex = 0;
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
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(872, 34);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(26, 34);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.btnSimpan);
            this.groupBox2.Location = new System.Drawing.Point(32, 714);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1044, 88);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
            // 
            // InputPermintaanBarangHabisPakaiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1113, 837);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "InputPermintaanBarangHabisPakaiForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Permintaan Barang Habis PakaiForm";
            this.Load += new System.EventHandler(this.InputPermintaanBarangHabisPakaiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jurusanBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn idDetailPermintaanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMasterBarangNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn jumlahDimintaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alasanKebutuhanDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource jurusanBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewButtonColumn HapusColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTahunAjaran;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAlasan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBarang;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.DateTimePicker dtTgl;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Button btnBrowseBarang;
        private System.Windows.Forms.ComboBox cmbJurusan;
        private System.Windows.Forms.BindingSource bindingSource3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKode;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}