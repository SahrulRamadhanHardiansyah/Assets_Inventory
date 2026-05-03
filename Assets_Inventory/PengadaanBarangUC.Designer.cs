namespace Assets_Inventory
{
    partial class PengadaanBarangUC
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.btnCari = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnCetak = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtLokasi = new System.Windows.Forms.ComboBox();
            this.txtBarang = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(356, 29);
            this.label2.TabIndex = 13;
            this.label2.Text = "DATA PENGADAAN BARANG";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(516, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Data Pengadaan Barang";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnHapus);
            this.groupBox2.Controls.Add(this.btnUbah);
            this.groupBox2.Controls.Add(this.btnTambah);
            this.groupBox2.Location = new System.Drawing.Point(53, 714);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(673, 88);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proses";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(502, 32);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(140, 40);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Import Excel";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(344, 32);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(140, 40);
            this.btnHapus.TabIndex = 2;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(187, 33);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(140, 40);
            this.btnUbah.TabIndex = 1;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTotal);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.btnCari);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtBarang);
            this.groupBox3.Controls.Add(this.txtLokasi);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new System.Drawing.Point(53, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1458, 596);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Pengadaan Barang";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(21, 566);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(121, 20);
            this.lblTotal.TabIndex = 27;
            this.lblTotal.Text = "Total Record : 1";
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(25, 94);
            this.dg.Name = "dg";
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(1408, 462);
            this.dg.TabIndex = 30;
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(547, 53);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(106, 32);
            this.btnCari.TabIndex = 3;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(256, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(225, 20);
            this.label17.TabIndex = 28;
            this.label17.Text = "Masukkan Kode/Nama Barang";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(21, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 20);
            this.label16.TabIndex = 27;
            this.label16.Text = "Filter Lokasi";
            // 
            // btnCetak
            // 
            this.btnCetak.Location = new System.Drawing.Point(541, 32);
            this.btnCetak.Name = "btnCetak";
            this.btnCetak.Size = new System.Drawing.Size(175, 40);
            this.btnCetak.TabIndex = 2;
            this.btnCetak.Text = "Cetak Barcode";
            this.btnCetak.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(13, 39);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(190, 28);
            this.comboBox2.TabIndex = 31;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(228, 39);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(190, 28);
            this.comboBox3.TabIndex = 32;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox3);
            this.groupBox4.Controls.Add(this.comboBox2);
            this.groupBox4.Controls.Add(this.btnCetak);
            this.groupBox4.Location = new System.Drawing.Point(750, 714);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(761, 88);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cetak Barcode";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(668, 53);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(106, 32);
            this.btnRefresh.TabIndex = 29;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // txtLokasi
            // 
            this.txtLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtLokasi.FormattingEnabled = true;
            this.txtLokasi.Location = new System.Drawing.Point(25, 56);
            this.txtLokasi.Name = "txtLokasi";
            this.txtLokasi.Size = new System.Drawing.Size(214, 28);
            this.txtLokasi.TabIndex = 27;
            // 
            // txtBarang
            // 
            this.txtBarang.Location = new System.Drawing.Point(260, 56);
            this.txtBarang.Name = "txtBarang";
            this.txtBarang.Size = new System.Drawing.Size(265, 26);
            this.txtBarang.TabIndex = 27;
            // 
            // PengadaanBarangUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "PengadaanBarangUC";
            this.Size = new System.Drawing.Size(1561, 823);
            this.Load += new System.EventHandler(this.PengadaanBarangUC_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtBarang;
        private System.Windows.Forms.ComboBox txtLokasi;
        private System.Windows.Forms.Button btnCetak;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}
