namespace Assets_Inventory
{
    partial class LaporanPeminjamanBarangUC
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtAkhir = new System.Windows.Forms.DateTimePicker();
            this.dtAwal = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBarang = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnTampilkan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtAkhir);
            this.groupBox2.Controls.Add(this.dtAwal);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbStatus);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbBarang);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.btnPreview);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.btnTampilkan);
            this.groupBox2.Location = new System.Drawing.Point(40, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 695);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Cetak";
            // 
            // dtAkhir
            // 
            this.dtAkhir.Location = new System.Drawing.Point(28, 121);
            this.dtAkhir.Name = "dtAkhir";
            this.dtAkhir.Size = new System.Drawing.Size(213, 26);
            this.dtAkhir.TabIndex = 71;
            // 
            // dtAwal
            // 
            this.dtAwal.Location = new System.Drawing.Point(29, 62);
            this.dtAwal.Name = "dtAwal";
            this.dtAwal.Size = new System.Drawing.Size(213, 26);
            this.dtAwal.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 20);
            this.label7.TabIndex = 69;
            this.label7.Text = "Tanggal Awal";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 68;
            this.label8.Text = "Tanggal Akhir";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(28, 207);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(213, 28);
            this.cmbStatus.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 67;
            this.label3.Text = "Status";
            // 
            // cmbBarang
            // 
            this.cmbBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarang.FormattingEnabled = true;
            this.cmbBarang.Location = new System.Drawing.Point(28, 266);
            this.cmbBarang.Name = "cmbBarang";
            this.cmbBarang.Size = new System.Drawing.Size(213, 28);
            this.cmbBarang.TabIndex = 64;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 20);
            this.label6.TabIndex = 65;
            this.label6.Text = "Filter Nama Barang";
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(28, 486);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(213, 40);
            this.btnTutup.TabIndex = 57;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(29, 394);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(213, 40);
            this.btnPreview.TabIndex = 62;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(28, 440);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(213, 40);
            this.btnExport.TabIndex = 56;
            this.btnExport.Text = "Export To Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnTampilkan
            // 
            this.btnTampilkan.Location = new System.Drawing.Point(29, 348);
            this.btnTampilkan.Name = "btnTampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(213, 40);
            this.btnTampilkan.TabIndex = 55;
            this.btnTampilkan.Text = "Tampilkan Data";
            this.btnTampilkan.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(541, 25);
            this.label1.TabIndex = 59;
            this.label1.Text = "Gunakan Form Ini Untuk Mencetak Data Peminjaman Barang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(418, 29);
            this.label2.TabIndex = 58;
            this.label2.Text = "LAPORAN PEMINJAMAN BARANG";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRecord);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Location = new System.Drawing.Point(326, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1186, 695);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Peminjaman Barang";
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(16, 664);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(121, 20);
            this.lblRecord.TabIndex = 27;
            this.lblRecord.Text = "Total Record : 1";
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(20, 35);
            this.dg.Name = "dg";
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(1140, 617);
            this.dg.TabIndex = 30;
            // 
            // LaporanPeminjamanBarangUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Name = "LaporanPeminjamanBarangUC";
            this.Size = new System.Drawing.Size(1561, 823);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtAkhir;
        private System.Windows.Forms.DateTimePicker dtAwal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBarang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnTampilkan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.DataGridView dg;
    }
}
