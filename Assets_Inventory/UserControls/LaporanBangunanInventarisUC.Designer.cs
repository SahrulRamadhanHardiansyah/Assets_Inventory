namespace Assets_Inventory
{
    partial class LaporanBangunanInventarisUC
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dg = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.rbStatus = new System.Windows.Forms.RadioButton();
            this.rbSemua = new System.Windows.Forms.RadioButton();
            this.cnbKondisi = new System.Windows.Forms.ComboBox();
            this.rbKondisi = new System.Windows.Forms.RadioButton();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnTampilkan = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Location = new System.Drawing.Point(219, 67);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(791, 452);
            this.groupBox3.TabIndex = 53;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Bangunan / Gedung Inventaris";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(13, 23);
            this.dg.Margin = new System.Windows.Forms.Padding(2);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(760, 418);
            this.dg.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 17);
            this.label1.TabIndex = 51;
            this.label1.Text = "Gunakan Form Ini Untuk Mencetak Data Bangunan / Gedung Invetaris";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 20);
            this.label2.TabIndex = 50;
            this.label2.Text = "LAPORAN BANGUNAN INVENTARIS";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbStatus);
            this.groupBox2.Controls.Add(this.rbStatus);
            this.groupBox2.Controls.Add(this.rbSemua);
            this.groupBox2.Controls.Add(this.cnbKondisi);
            this.groupBox2.Controls.Add(this.rbKondisi);
            this.groupBox2.Controls.Add(this.btnTutup);
            this.groupBox2.Controls.Add(this.btnPreview);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.btnTampilkan);
            this.groupBox2.Location = new System.Drawing.Point(28, 67);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(179, 452);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Cetak";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(16, 68);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(143, 21);
            this.cmbStatus.TabIndex = 58;
            // 
            // rbStatus
            // 
            this.rbStatus.AutoSize = true;
            this.rbStatus.Location = new System.Drawing.Point(16, 48);
            this.rbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.rbStatus.Name = "rbStatus";
            this.rbStatus.Size = new System.Drawing.Size(80, 17);
            this.rbStatus.TabIndex = 57;
            this.rbStatus.TabStop = true;
            this.rbStatus.Text = "Filter Status";
            this.rbStatus.UseVisualStyleBackColor = true;
            // 
            // rbSemua
            // 
            this.rbSemua.AutoSize = true;
            this.rbSemua.Location = new System.Drawing.Point(16, 23);
            this.rbSemua.Margin = new System.Windows.Forms.Padding(2);
            this.rbSemua.Name = "rbSemua";
            this.rbSemua.Size = new System.Drawing.Size(58, 17);
            this.rbSemua.TabIndex = 56;
            this.rbSemua.TabStop = true;
            this.rbSemua.Text = "Semua";
            this.rbSemua.UseVisualStyleBackColor = true;
            // 
            // cnbKondisi
            // 
            this.cnbKondisi.FormattingEnabled = true;
            this.cnbKondisi.Location = new System.Drawing.Point(16, 114);
            this.cnbKondisi.Margin = new System.Windows.Forms.Padding(2);
            this.cnbKondisi.Name = "cnbKondisi";
            this.cnbKondisi.Size = new System.Drawing.Size(143, 21);
            this.cnbKondisi.TabIndex = 55;
            // 
            // rbKondisi
            // 
            this.rbKondisi.AutoSize = true;
            this.rbKondisi.Location = new System.Drawing.Point(16, 95);
            this.rbKondisi.Margin = new System.Windows.Forms.Padding(2);
            this.rbKondisi.Name = "rbKondisi";
            this.rbKondisi.Size = new System.Drawing.Size(84, 17);
            this.rbKondisi.TabIndex = 54;
            this.rbKondisi.TabStop = true;
            this.rbKondisi.Text = "Filter Kondisi";
            this.rbKondisi.UseVisualStyleBackColor = true;
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(23, 259);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(142, 26);
            this.btnTutup.TabIndex = 33;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(23, 200);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(142, 26);
            this.btnPreview.TabIndex = 43;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(23, 229);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(142, 26);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export To Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnTampilkan
            // 
            this.btnTampilkan.Location = new System.Drawing.Point(23, 170);
            this.btnTampilkan.Margin = new System.Windows.Forms.Padding(2);
            this.btnTampilkan.Name = "btnTampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(142, 26);
            this.btnTampilkan.TabIndex = 4;
            this.btnTampilkan.Text = "Tampilkan Data";
            this.btnTampilkan.UseVisualStyleBackColor = true;
            // 
            // LaporanBangunanInventarisUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LaporanBangunanInventarisUC";
            this.Size = new System.Drawing.Size(1041, 535);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbSemua;
        private System.Windows.Forms.ComboBox cnbKondisi;
        private System.Windows.Forms.RadioButton rbKondisi;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnTampilkan;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.RadioButton rbStatus;
    }
}
