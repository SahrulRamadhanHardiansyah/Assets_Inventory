namespace Assets_Inventory
{
    partial class PermintaanBarangUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbJenis = new System.Windows.Forms.ComboBox();
            this.cmbJumlah = new System.Windows.Forms.ComboBox();
            this.btnCetak = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dg = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCari = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kodePermintaanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idJurusanNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPenggunaNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPenyetujuNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keteranganKeperluanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusPersetujuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggalPermintaanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggalPersetujuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alasanDisetujuiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SetujuColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbJenis);
            this.groupBox4.Controls.Add(this.cmbJumlah);
            this.groupBox4.Controls.Add(this.btnCetak);
            this.groupBox4.Location = new System.Drawing.Point(500, 463);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(507, 57);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cetak Barcode";
            // 
            // cmbJenis
            // 
            this.cmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenis.FormattingEnabled = true;
            this.cmbJenis.Location = new System.Drawing.Point(152, 25);
            this.cmbJenis.Margin = new System.Windows.Forms.Padding(2);
            this.cmbJenis.Name = "cmbJenis";
            this.cmbJenis.Size = new System.Drawing.Size(128, 21);
            this.cmbJenis.TabIndex = 32;
            // 
            // cmbJumlah
            // 
            this.cmbJumlah.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJumlah.FormattingEnabled = true;
            this.cmbJumlah.Location = new System.Drawing.Point(9, 25);
            this.cmbJumlah.Margin = new System.Windows.Forms.Padding(2);
            this.cmbJumlah.Name = "cmbJumlah";
            this.cmbJumlah.Size = new System.Drawing.Size(128, 21);
            this.cmbJumlah.TabIndex = 31;
            // 
            // btnCetak
            // 
            this.btnCetak.Location = new System.Drawing.Point(361, 21);
            this.btnCetak.Margin = new System.Windows.Forms.Padding(2);
            this.btnCetak.Name = "btnCetak";
            this.btnCetak.Size = new System.Drawing.Size(117, 26);
            this.btnCetak.TabIndex = 2;
            this.btnCetak.Text = "Cetak Barcode";
            this.btnCetak.UseVisualStyleBackColor = true;
            this.btnCetak.Click += new System.EventHandler(this.btnCetak_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(14, 368);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(84, 13);
            this.lblTotal.TabIndex = 27;
            this.lblTotal.Text = "Total Record : 1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTotal);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.btnCari);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtCari);
            this.groupBox3.Location = new System.Drawing.Point(35, 63);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(972, 387);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Permintaan Barang";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kodePermintaanDataGridViewTextBoxColumn,
            this.idJurusanNavigationDataGridViewTextBoxColumn,
            this.idPenggunaNavigationDataGridViewTextBoxColumn,
            this.idPenyetujuNavigationDataGridViewTextBoxColumn,
            this.keteranganKeperluanDataGridViewTextBoxColumn,
            this.statusPersetujuanDataGridViewTextBoxColumn,
            this.tanggalPermintaanDataGridViewTextBoxColumn,
            this.tanggalPersetujuanDataGridViewTextBoxColumn,
            this.alasanDisetujuiDataGridViewTextBoxColumn,
            this.DetailColumn,
            this.SetujuColumn});
            this.dg.DataSource = this.bindingSource1;
            this.dg.Location = new System.Drawing.Point(17, 61);
            this.dg.Margin = new System.Windows.Forms.Padding(2);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(939, 300);
            this.dg.TabIndex = 30;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            this.dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellContentClick);
            this.dg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.Permintaan);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(288, 31);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(71, 21);
            this.btnRefresh.TabIndex = 29;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(208, 31);
            this.btnCari.Margin = new System.Windows.Forms.Padding(2);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(71, 21);
            this.btnCari.TabIndex = 3;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 17);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(141, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "Masukkan Kode Permintaan";
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(16, 33);
            this.txtCari.Margin = new System.Windows.Forms.Padding(2);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(178, 20);
            this.txtCari.TabIndex = 27;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(335, 21);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(93, 26);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Import Excel";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(229, 21);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(93, 26);
            this.btnHapus.TabIndex = 2;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(125, 21);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(93, 26);
            this.btnUbah.TabIndex = 1;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnHapus);
            this.groupBox2.Controls.Add(this.btnUbah);
            this.groupBox2.Controls.Add(this.btnTambah);
            this.groupBox2.Location = new System.Drawing.Point(35, 463);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(449, 57);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Permintaan Barang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "DATA PERMINTAAN BARANG";
            // 
            // kodePermintaanDataGridViewTextBoxColumn
            // 
            this.kodePermintaanDataGridViewTextBoxColumn.DataPropertyName = "KodePermintaan";
            this.kodePermintaanDataGridViewTextBoxColumn.HeaderText = "Kode Permintaan";
            this.kodePermintaanDataGridViewTextBoxColumn.Name = "kodePermintaanDataGridViewTextBoxColumn";
            this.kodePermintaanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idJurusanNavigationDataGridViewTextBoxColumn
            // 
            this.idJurusanNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdJurusanNavigation";
            this.idJurusanNavigationDataGridViewTextBoxColumn.HeaderText = "Jurusan";
            this.idJurusanNavigationDataGridViewTextBoxColumn.Name = "idJurusanNavigationDataGridViewTextBoxColumn";
            this.idJurusanNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idPenggunaNavigationDataGridViewTextBoxColumn
            // 
            this.idPenggunaNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdPenggunaNavigation";
            this.idPenggunaNavigationDataGridViewTextBoxColumn.HeaderText = "Peminta";
            this.idPenggunaNavigationDataGridViewTextBoxColumn.Name = "idPenggunaNavigationDataGridViewTextBoxColumn";
            this.idPenggunaNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idPenyetujuNavigationDataGridViewTextBoxColumn
            // 
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdPenyetujuNavigation";
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.HeaderText = "Penyetuju";
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.Name = "idPenyetujuNavigationDataGridViewTextBoxColumn";
            this.idPenyetujuNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // keteranganKeperluanDataGridViewTextBoxColumn
            // 
            this.keteranganKeperluanDataGridViewTextBoxColumn.DataPropertyName = "KeteranganKeperluan";
            this.keteranganKeperluanDataGridViewTextBoxColumn.HeaderText = "Keterangan Keperluan";
            this.keteranganKeperluanDataGridViewTextBoxColumn.Name = "keteranganKeperluanDataGridViewTextBoxColumn";
            this.keteranganKeperluanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusPersetujuanDataGridViewTextBoxColumn
            // 
            this.statusPersetujuanDataGridViewTextBoxColumn.DataPropertyName = "StatusPersetujuan";
            this.statusPersetujuanDataGridViewTextBoxColumn.HeaderText = "Status Persetujuan";
            this.statusPersetujuanDataGridViewTextBoxColumn.Name = "statusPersetujuanDataGridViewTextBoxColumn";
            this.statusPersetujuanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tanggalPermintaanDataGridViewTextBoxColumn
            // 
            this.tanggalPermintaanDataGridViewTextBoxColumn.DataPropertyName = "TanggalPermintaan";
            this.tanggalPermintaanDataGridViewTextBoxColumn.HeaderText = "Tanggal Permintaan";
            this.tanggalPermintaanDataGridViewTextBoxColumn.Name = "tanggalPermintaanDataGridViewTextBoxColumn";
            this.tanggalPermintaanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tanggalPersetujuanDataGridViewTextBoxColumn
            // 
            this.tanggalPersetujuanDataGridViewTextBoxColumn.DataPropertyName = "TanggalPersetujuan";
            this.tanggalPersetujuanDataGridViewTextBoxColumn.HeaderText = "Tanggal Persetujuan";
            this.tanggalPersetujuanDataGridViewTextBoxColumn.Name = "tanggalPersetujuanDataGridViewTextBoxColumn";
            this.tanggalPersetujuanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // alasanDisetujuiDataGridViewTextBoxColumn
            // 
            this.alasanDisetujuiDataGridViewTextBoxColumn.DataPropertyName = "AlasanDisetujui";
            this.alasanDisetujuiDataGridViewTextBoxColumn.HeaderText = "Alasan Disetujui";
            this.alasanDisetujuiDataGridViewTextBoxColumn.Name = "alasanDisetujuiDataGridViewTextBoxColumn";
            this.alasanDisetujuiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // DetailColumn
            // 
            this.DetailColumn.HeaderText = "";
            this.DetailColumn.Name = "DetailColumn";
            this.DetailColumn.ReadOnly = true;
            this.DetailColumn.Text = "Detail";
            this.DetailColumn.UseColumnTextForButtonValue = true;
            // 
            // SetujuColumn
            // 
            this.SetujuColumn.HeaderText = "Aksi";
            this.SetujuColumn.Name = "SetujuColumn";
            this.SetujuColumn.ReadOnly = true;
            this.SetujuColumn.Text = "Seuju";
            this.SetujuColumn.UseColumnTextForButtonValue = true;
            // 
            // PermintaanBarangUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "PermintaanBarangUC";
            this.Size = new System.Drawing.Size(1041, 535);
            this.Load += new System.EventHandler(this.PermintaanBarangUC_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.ComboBox cmbJumlah;
        private System.Windows.Forms.Button btnCetak;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodePermintaanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idJurusanNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPenggunaNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPenyetujuNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keteranganKeperluanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusPersetujuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanggalPermintaanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanggalPersetujuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alasanDisetujuiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn DetailColumn;
        private System.Windows.Forms.DataGridViewButtonColumn SetujuColumn;
    }
}
