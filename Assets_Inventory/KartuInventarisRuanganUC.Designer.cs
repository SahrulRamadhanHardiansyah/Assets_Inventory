namespace Assets_Inventory
{
    partial class KartuInventarisRuanganUC
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
            this.btnExport = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnTampilkan = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbLokasi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGudang = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.cbIcludeGmbr = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(29, 342);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(213, 40);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export To Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(29, 388);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(213, 40);
            this.btnTutup.TabIndex = 33;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            // 
            // btnTampilkan
            // 
            this.btnTampilkan.Location = new System.Drawing.Point(29, 204);
            this.btnTampilkan.Name = "btnTampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(213, 40);
            this.btnTampilkan.TabIndex = 4;
            this.btnTampilkan.Text = "Tampilkan Data";
            this.btnTampilkan.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTotal);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Location = new System.Drawing.Point(325, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1186, 695);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Barang Inventaris";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(21, 662);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(121, 20);
            this.lblTotal.TabIndex = 27;
            this.lblTotal.Text = "Total Record : 1";
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(25, 33);
            this.dg.Name = "dg";
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
            this.label1.Location = new System.Drawing.Point(47, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(539, 25);
            this.label1.TabIndex = 35;
            this.label1.Text = "Gunakan Form Ini Untuk Mencetak Kartu Inventaris Ruangan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(391, 29);
            this.label2.TabIndex = 34;
            this.label2.Text = "KARTU INVENTARIS RUANGAN";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.cbIcludeGmbr);
            this.groupBox2.Controls.Add(this.btnPreview);
            this.groupBox2.Controls.Add(this.cmbLokasi);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbGudang);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.btnTampilkan);
            this.groupBox2.Location = new System.Drawing.Point(39, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 695);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Cetak";
            // 
            // cmbLokasi
            // 
            this.cmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLokasi.FormattingEnabled = true;
            this.cmbLokasi.Location = new System.Drawing.Point(29, 60);
            this.cmbLokasi.Name = "cmbLokasi";
            this.cmbLokasi.Size = new System.Drawing.Size(213, 28);
            this.cmbLokasi.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 42;
            this.label5.Text = "Filter Lokasi";
            // 
            // cmbGudang
            // 
            this.cmbGudang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGudang.FormattingEnabled = true;
            this.cmbGudang.Location = new System.Drawing.Point(29, 119);
            this.cmbGudang.Name = "cmbGudang";
            this.cmbGudang.Size = new System.Drawing.Size(213, 28);
            this.cmbGudang.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 40;
            this.label4.Text = "Filter Gudang";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(29, 250);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(213, 40);
            this.btnPreview.TabIndex = 43;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // cbIcludeGmbr
            // 
            this.cbIcludeGmbr.AutoSize = true;
            this.cbIcludeGmbr.Location = new System.Drawing.Point(29, 306);
            this.cbIcludeGmbr.Name = "cbIcludeGmbr";
            this.cbIcludeGmbr.Size = new System.Drawing.Size(149, 24);
            this.cbIcludeGmbr.TabIndex = 44;
            this.cbIcludeGmbr.Text = "Include Gambar";
            this.cbIcludeGmbr.UseVisualStyleBackColor = true;
            // 
            // KartuInventarisRuanganUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Name = "KartuInventarisRuanganUC";
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

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnTampilkan;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbIcludeGmbr;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.ComboBox cmbLokasi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGudang;
        private System.Windows.Forms.Label label4;
    }
}
