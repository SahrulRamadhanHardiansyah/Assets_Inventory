namespace Assets_Inventory
{
    partial class LaporanPengadaanBarangUC
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
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnTampilkanData = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTotalHarga = new System.Windows.Forms.Label();
            this.lblTotalItem = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpSampai = new System.Windows.Forms.DateTimePicker();
            this.dtpMulai = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbNamaBarang = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkIncludeGambar = new System.Windows.Forms.CheckBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.cmbLokasi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRuang = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(24, 634);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(213, 40);
            this.btnTutup.TabIndex = 33;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(24, 588);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(213, 40);
            this.btnExportToExcel.TabIndex = 5;
            this.btnExportToExcel.Text = "Export To Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            // 
            // btnTampilkanData
            // 
            this.btnTampilkanData.Location = new System.Drawing.Point(24, 450);
            this.btnTampilkanData.Name = "btnTampilkanData";
            this.btnTampilkanData.Size = new System.Drawing.Size(213, 40);
            this.btnTampilkanData.TabIndex = 4;
            this.btnTampilkanData.Text = "Tampilkan Data";
            this.btnTampilkanData.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTotalHarga);
            this.groupBox3.Controls.Add(this.lblTotalItem);
            this.groupBox3.Controls.Add(this.lblTotal);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Location = new System.Drawing.Point(326, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1186, 695);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Pengadaan Barang";
            // 
            // lblTotalHarga
            // 
            this.lblTotalHarga.AutoSize = true;
            this.lblTotalHarga.Location = new System.Drawing.Point(461, 664);
            this.lblTotalHarga.Name = "lblTotalHarga";
            this.lblTotalHarga.Size = new System.Drawing.Size(113, 20);
            this.lblTotalHarga.TabIndex = 32;
            this.lblTotalHarga.Text = "Total Harga : 1";
            // 
            // lblTotalItem
            // 
            this.lblTotalItem.AutoSize = true;
            this.lblTotalItem.Location = new System.Drawing.Point(233, 664);
            this.lblTotalItem.Name = "lblTotalItem";
            this.lblTotalItem.Size = new System.Drawing.Size(101, 20);
            this.lblTotalItem.TabIndex = 31;
            this.lblTotalItem.Text = "Total Item : 1";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(16, 664);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(121, 20);
            this.lblTotal.TabIndex = 27;
            this.lblTotal.Text = "Total Record : 1";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(20, 35);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(1140, 617);
            this.dg.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(534, 25);
            this.label1.TabIndex = 39;
            this.label1.Text = "Gunakan Form Ini Untuk Mencetak Data Pengadaan Barang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(409, 29);
            this.label2.TabIndex = 38;
            this.label2.Text = "LAPORAN PENGADAAN BARANG";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpSampai);
            this.groupBox2.Controls.Add(this.dtpMulai);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbKategori);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbNamaBarang);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.chkIncludeGambar);
            this.groupBox2.Controls.Add(this.btnPreview);
            this.groupBox2.Controls.Add(this.cmbLokasi);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbRuang);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnExportToExcel);
            this.groupBox2.Controls.Add(this.btnTampilkanData);
            this.groupBox2.Location = new System.Drawing.Point(40, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 695);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Cetak";
            // 
            // dtpSampai
            // 
            this.dtpSampai.Location = new System.Drawing.Point(23, 121);
            this.dtpSampai.Name = "dtpSampai";
            this.dtpSampai.Size = new System.Drawing.Size(213, 26);
            this.dtpSampai.TabIndex = 54;
            // 
            // dtpMulai
            // 
            this.dtpMulai.Location = new System.Drawing.Point(24, 62);
            this.dtpMulai.Name = "dtpMulai";
            this.dtpMulai.Size = new System.Drawing.Size(213, 26);
            this.dtpMulai.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 20);
            this.label7.TabIndex = 52;
            this.label7.Text = "Tanggal Awal";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 50;
            this.label8.Text = "Tanggal Akhir";
            // 
            // cmbKategori
            // 
            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(24, 208);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(213, 28);
            this.cmbKategori.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 48;
            this.label3.Text = "Filter Kategori";
            // 
            // cmbNamaBarang
            // 
            this.cmbNamaBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNamaBarang.FormattingEnabled = true;
            this.cmbNamaBarang.Location = new System.Drawing.Point(24, 267);
            this.cmbNamaBarang.Name = "cmbNamaBarang";
            this.cmbNamaBarang.Size = new System.Drawing.Size(213, 28);
            this.cmbNamaBarang.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 20);
            this.label6.TabIndex = 46;
            this.label6.Text = "Filter Nama Barang";
            // 
            // chkIncludeGambar
            // 
            this.chkIncludeGambar.AutoSize = true;
            this.chkIncludeGambar.Location = new System.Drawing.Point(24, 552);
            this.chkIncludeGambar.Name = "chkIncludeGambar";
            this.chkIncludeGambar.Size = new System.Drawing.Size(149, 24);
            this.chkIncludeGambar.TabIndex = 44;
            this.chkIncludeGambar.Text = "Include Gambar";
            this.chkIncludeGambar.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(24, 496);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(213, 40);
            this.btnPreview.TabIndex = 43;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // cmbLokasi
            // 
            this.cmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLokasi.FormattingEnabled = true;
            this.cmbLokasi.Location = new System.Drawing.Point(24, 328);
            this.cmbLokasi.Name = "cmbLokasi";
            this.cmbLokasi.Size = new System.Drawing.Size(213, 28);
            this.cmbLokasi.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 42;
            this.label5.Text = "Filter Lokasi";
            // 
            // cmbRuang
            // 
            this.cmbRuang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRuang.FormattingEnabled = true;
            this.cmbRuang.Location = new System.Drawing.Point(24, 387);
            this.cmbRuang.Name = "cmbRuang";
            this.cmbRuang.Size = new System.Drawing.Size(213, 28);
            this.cmbRuang.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 362);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 40;
            this.label4.Text = "Filter Ruang";
            // 
            // LaporanPengadaanBarangUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Name = "LaporanPengadaanBarangUC";
            this.Size = new System.Drawing.Size(1561, 823);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnTampilkanData;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkIncludeGambar;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.ComboBox cmbLokasi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRuang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpMulai;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbNamaBarang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpSampai;
        private System.Windows.Forms.Label lblTotalHarga;
        private System.Windows.Forms.Label lblTotalItem;
    }
}
