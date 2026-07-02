namespace Assets_Inventory.UserControls
{
    partial class DashboardUC
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.lblTotalAset = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.lblPermintaanPending = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.kryptonGroup3 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.lblPengadaanProses = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.kryptonGroup4 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.lblAsetBelumLengkap = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chartAset = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgNotifikasi = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chartKondisi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dgPermintaanPending = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).BeginInit();
            this.kryptonGroup2.Panel.SuspendLayout();
            this.kryptonGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).BeginInit();
            this.kryptonGroup3.Panel.SuspendLayout();
            this.kryptonGroup3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4.Panel)).BeginInit();
            this.kryptonGroup4.Panel.SuspendLayout();
            this.kryptonGroup4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotifikasi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartKondisi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPermintaanPending)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Location = new System.Drawing.Point(31, 51);
            this.kryptonGroup1.Name = "kryptonGroup1";
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.lblTotalAset);
            this.kryptonGroup1.Panel.Controls.Add(this.label2);
            this.kryptonGroup1.Size = new System.Drawing.Size(210, 134);
            this.kryptonGroup1.TabIndex = 0;
            // 
            // lblTotalAset
            // 
            this.lblTotalAset.BackColor = System.Drawing.Color.White;
            this.lblTotalAset.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAset.Location = new System.Drawing.Point(50, 33);
            this.lblTotalAset.Name = "lblTotalAset";
            this.lblTotalAset.Size = new System.Drawing.Size(111, 42);
            this.lblTotalAset.TabIndex = 3;
            this.lblTotalAset.Text = "0";
            this.lblTotalAset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(46, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "TOTAL ASET";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "DASHBOARD";
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Location = new System.Drawing.Point(272, 51);
            this.kryptonGroup2.Name = "kryptonGroup2";
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.lblPermintaanPending);
            this.kryptonGroup2.Panel.Controls.Add(this.label4);
            this.kryptonGroup2.Panel.Click += new System.EventHandler(this.BoxPermintaanTertunda_Click);
            this.kryptonGroup2.Size = new System.Drawing.Size(210, 134);
            this.kryptonGroup2.TabIndex = 4;
            // 
            // lblPermintaanPending
            // 
            this.lblPermintaanPending.BackColor = System.Drawing.Color.White;
            this.lblPermintaanPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermintaanPending.Location = new System.Drawing.Point(38, 33);
            this.lblPermintaanPending.Name = "lblPermintaanPending";
            this.lblPermintaanPending.Size = new System.Drawing.Size(137, 42);
            this.lblPermintaanPending.TabIndex = 3;
            this.lblPermintaanPending.Text = "0";
            this.lblPermintaanPending.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 43);
            this.label4.TabIndex = 2;
            this.label4.Text = "PERMINTAAN TERTUNDA";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kryptonGroup3
            // 
            this.kryptonGroup3.Location = new System.Drawing.Point(513, 51);
            this.kryptonGroup3.Name = "kryptonGroup3";
            // 
            // kryptonGroup3.Panel
            // 
            this.kryptonGroup3.Panel.AccessibleName = "";
            this.kryptonGroup3.Panel.Controls.Add(this.lblPengadaanProses);
            this.kryptonGroup3.Panel.Controls.Add(this.label6);
            this.kryptonGroup3.Panel.Click += new System.EventHandler(this.BoxPengadaanDiproses_Click);
            this.kryptonGroup3.Size = new System.Drawing.Size(210, 134);
            this.kryptonGroup3.TabIndex = 4;
            // 
            // lblPengadaanProses
            // 
            this.lblPengadaanProses.BackColor = System.Drawing.Color.White;
            this.lblPengadaanProses.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPengadaanProses.Location = new System.Drawing.Point(41, 33);
            this.lblPengadaanProses.Name = "lblPengadaanProses";
            this.lblPengadaanProses.Size = new System.Drawing.Size(129, 42);
            this.lblPengadaanProses.TabIndex = 3;
            this.lblPengadaanProses.Text = "0";
            this.lblPengadaanProses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 40);
            this.label6.TabIndex = 2;
            this.label6.Text = "PENGADAAN DIPROSES";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kryptonGroup4
            // 
            this.kryptonGroup4.Location = new System.Drawing.Point(755, 51);
            this.kryptonGroup4.Name = "kryptonGroup4";
            // 
            // kryptonGroup4.Panel
            // 
            this.kryptonGroup4.Panel.Controls.Add(this.lblAsetBelumLengkap);
            this.kryptonGroup4.Panel.Controls.Add(this.label8);
            this.kryptonGroup4.Panel.Click += new System.EventHandler(this.BoxAsetBelumLengkap_Click);
            this.kryptonGroup4.Size = new System.Drawing.Size(210, 134);
            this.kryptonGroup4.TabIndex = 5;
            // 
            // lblAsetBelumLengkap
            // 
            this.lblAsetBelumLengkap.BackColor = System.Drawing.Color.White;
            this.lblAsetBelumLengkap.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsetBelumLengkap.Location = new System.Drawing.Point(41, 33);
            this.lblAsetBelumLengkap.Name = "lblAsetBelumLengkap";
            this.lblAsetBelumLengkap.Size = new System.Drawing.Size(129, 42);
            this.lblAsetBelumLengkap.TabIndex = 3;
            this.lblAsetBelumLengkap.Text = "0";
            this.lblAsetBelumLengkap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(37, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 40);
            this.label8.TabIndex = 2;
            this.label8.Text = "ASET BELUM LENGKAP";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chartAset
            // 
            this.chartAset.BorderlineColor = System.Drawing.SystemColors.Control;
            this.chartAset.BorderlineWidth = 0;
            chartArea3.Name = "ChartArea1";
            this.chartAset.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartAset.Legends.Add(legend3);
            this.chartAset.Location = new System.Drawing.Point(31, 229);
            this.chartAset.Name = "chartAset";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartAset.Series.Add(series3);
            this.chartAset.Size = new System.Drawing.Size(451, 235);
            this.chartAset.TabIndex = 6;
            this.chartAset.Text = "chart1";
            // 
            // dgNotifikasi
            // 
            this.dgNotifikasi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNotifikasi.Location = new System.Drawing.Point(31, 504);
            this.dgNotifikasi.Name = "dgNotifikasi";
            this.dgNotifikasi.RowHeadersWidth = 62;
            this.dgNotifikasi.Size = new System.Drawing.Size(451, 188);
            this.dgNotifikasi.TabIndex = 7;
            this.dgNotifikasi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgNotifikasi_CellDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 473);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Notifikasi";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "Aset";
            // 
            // chartKondisi
            // 
            this.chartKondisi.BorderlineColor = System.Drawing.SystemColors.Control;
            this.chartKondisi.BorderlineWidth = 0;
            chartArea4.Name = "ChartArea1";
            this.chartKondisi.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartKondisi.Legends.Add(legend4);
            this.chartKondisi.Location = new System.Drawing.Point(513, 229);
            this.chartKondisi.Name = "chartKondisi";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartKondisi.Series.Add(series4);
            this.chartKondisi.Size = new System.Drawing.Size(451, 235);
            this.chartKondisi.TabIndex = 10;
            this.chartKondisi.Text = "chart1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(509, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 24);
            this.label7.TabIndex = 11;
            this.label7.Text = "Kondisi";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(509, 473);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(205, 24);
            this.label9.TabIndex = 13;
            this.label9.Text = "Permintaan Tertunda";
            // 
            // dgPermintaanPending
            // 
            this.dgPermintaanPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPermintaanPending.Location = new System.Drawing.Point(513, 504);
            this.dgPermintaanPending.Name = "dgPermintaanPending";
            this.dgPermintaanPending.RowHeadersWidth = 62;
            this.dgPermintaanPending.Size = new System.Drawing.Size(451, 188);
            this.dgPermintaanPending.TabIndex = 12;
            // 
            // DashboardUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dgPermintaanPending);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chartKondisi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgNotifikasi);
            this.Controls.Add(this.chartAset);
            this.Controls.Add(this.kryptonGroup4);
            this.Controls.Add(this.kryptonGroup3);
            this.Controls.Add(this.kryptonGroup2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.kryptonGroup1);
            this.Name = "DashboardUC";
            this.Size = new System.Drawing.Size(1021, 747);
            this.Load += new System.EventHandler(this.DashboardUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).EndInit();
            this.kryptonGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).EndInit();
            this.kryptonGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).EndInit();
            this.kryptonGroup3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).EndInit();
            this.kryptonGroup3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4.Panel)).EndInit();
            this.kryptonGroup4.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4)).EndInit();
            this.kryptonGroup4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotifikasi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartKondisi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPermintaanPending)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalAset;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private System.Windows.Forms.Label lblPermintaanPending;
        private System.Windows.Forms.Label label4;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup3;
        private System.Windows.Forms.Label lblPengadaanProses;
        private System.Windows.Forms.Label label6;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup4;
        private System.Windows.Forms.Label lblAsetBelumLengkap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAset;
        private System.Windows.Forms.DataGridView dgNotifikasi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKondisi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgPermintaanPending;
    }
}
