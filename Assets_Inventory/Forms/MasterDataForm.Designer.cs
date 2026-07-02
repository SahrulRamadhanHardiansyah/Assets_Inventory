namespace Assets_Inventory
{
    partial class MasterDataForm
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
            this.btnKategori = new System.Windows.Forms.Button();
            this.btnBarang = new System.Windows.Forms.Button();
            this.btnLokasi = new System.Windows.Forms.Button();
            this.btnSatuan = new System.Windows.Forms.Button();
            this.btnMerk = new System.Windows.Forms.Button();
            this.btnRuang = new System.Windows.Forms.Button();
            this.btnKondisi = new System.Windows.Forms.Button();
            this.btnNonAktif = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSumber = new System.Windows.Forms.Button();
            this.btnRombel = new System.Windows.Forms.Button();
            this.btnTahun = new System.Windows.Forms.Button();
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
            // btnKategori
            // 
            this.btnKategori.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKategori.Location = new System.Drawing.Point(41, 33);
            this.btnKategori.Name = "btnKategori";
            this.btnKategori.Size = new System.Drawing.Size(270, 45);
            this.btnKategori.TabIndex = 0;
            this.btnKategori.Text = "Master Kategori";
            this.btnKategori.UseVisualStyleBackColor = true;
            this.btnKategori.Click += new System.EventHandler(this.btnKategori_Click);
            // 
            // btnBarang
            // 
            this.btnBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarang.Location = new System.Drawing.Point(41, 84);
            this.btnBarang.Name = "btnBarang";
            this.btnBarang.Size = new System.Drawing.Size(270, 45);
            this.btnBarang.TabIndex = 1;
            this.btnBarang.Text = "Master Barang";
            this.btnBarang.UseVisualStyleBackColor = true;
            this.btnBarang.Click += new System.EventHandler(this.btnBarang_Click);
            // 
            // btnLokasi
            // 
            this.btnLokasi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLokasi.Location = new System.Drawing.Point(41, 135);
            this.btnLokasi.Name = "btnLokasi";
            this.btnLokasi.Size = new System.Drawing.Size(270, 45);
            this.btnLokasi.TabIndex = 2;
            this.btnLokasi.Text = "Master Lokasi";
            this.btnLokasi.UseVisualStyleBackColor = true;
            this.btnLokasi.Click += new System.EventHandler(this.btnLokasi_Click);
            // 
            // btnSatuan
            // 
            this.btnSatuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSatuan.Location = new System.Drawing.Point(41, 288);
            this.btnSatuan.Name = "btnSatuan";
            this.btnSatuan.Size = new System.Drawing.Size(270, 45);
            this.btnSatuan.TabIndex = 5;
            this.btnSatuan.Text = "Master Satuan";
            this.btnSatuan.UseVisualStyleBackColor = true;
            this.btnSatuan.Click += new System.EventHandler(this.btnSatuan_Click);
            // 
            // btnMerk
            // 
            this.btnMerk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMerk.Location = new System.Drawing.Point(41, 237);
            this.btnMerk.Name = "btnMerk";
            this.btnMerk.Size = new System.Drawing.Size(270, 45);
            this.btnMerk.TabIndex = 4;
            this.btnMerk.Text = "Master Merk";
            this.btnMerk.UseVisualStyleBackColor = true;
            this.btnMerk.Click += new System.EventHandler(this.btnMerk_Click);
            // 
            // btnRuang
            // 
            this.btnRuang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRuang.Location = new System.Drawing.Point(41, 186);
            this.btnRuang.Name = "btnRuang";
            this.btnRuang.Size = new System.Drawing.Size(270, 45);
            this.btnRuang.TabIndex = 3;
            this.btnRuang.Text = "Master Ruang && Lemari";
            this.btnRuang.UseVisualStyleBackColor = true;
            this.btnRuang.Click += new System.EventHandler(this.btnRuang_Click);
            // 
            // btnKondisi
            // 
            this.btnKondisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKondisi.Location = new System.Drawing.Point(317, 84);
            this.btnKondisi.Name = "btnKondisi";
            this.btnKondisi.Size = new System.Drawing.Size(270, 45);
            this.btnKondisi.TabIndex = 7;
            this.btnKondisi.Text = "Master Kondisi";
            this.btnKondisi.UseVisualStyleBackColor = true;
            this.btnKondisi.Click += new System.EventHandler(this.btnKondisi_Click);
            // 
            // btnNonAktif
            // 
            this.btnNonAktif.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNonAktif.Location = new System.Drawing.Point(317, 33);
            this.btnNonAktif.Name = "btnNonAktif";
            this.btnNonAktif.Size = new System.Drawing.Size(270, 45);
            this.btnNonAktif.TabIndex = 6;
            this.btnNonAktif.Text = "Master Non Aktif";
            this.btnNonAktif.UseVisualStyleBackColor = true;
            this.btnNonAktif.Click += new System.EventHandler(this.btnNonAktif_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTutup.Location = new System.Drawing.Point(41, 405);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(546, 45);
            this.btnTutup.TabIndex = 8;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSumber
            // 
            this.btnSumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSumber.Location = new System.Drawing.Point(317, 135);
            this.btnSumber.Name = "btnSumber";
            this.btnSumber.Size = new System.Drawing.Size(270, 45);
            this.btnSumber.TabIndex = 9;
            this.btnSumber.Text = "Master Sumber Perolehan";
            this.btnSumber.UseVisualStyleBackColor = true;
            this.btnSumber.Click += new System.EventHandler(this.btnSumber_Click);
            // 
            // btnRombel
            // 
            this.btnRombel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRombel.Location = new System.Drawing.Point(317, 186);
            this.btnRombel.Name = "btnRombel";
            this.btnRombel.Size = new System.Drawing.Size(270, 45);
            this.btnRombel.TabIndex = 10;
            this.btnRombel.Text = "Master Rombel";
            this.btnRombel.UseVisualStyleBackColor = true;
            this.btnRombel.Click += new System.EventHandler(this.btnRombel_Click);
            // 
            // btnTahun
            // 
            this.btnTahun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTahun.Location = new System.Drawing.Point(317, 237);
            this.btnTahun.Name = "btnTahun";
            this.btnTahun.Size = new System.Drawing.Size(270, 45);
            this.btnTahun.TabIndex = 11;
            this.btnTahun.Text = "Master Tahun Ajaran";
            this.btnTahun.UseVisualStyleBackColor = true;
            this.btnTahun.Click += new System.EventHandler(this.btnTahun_Click);
            // 
            // MasterDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(628, 463);
            this.Controls.Add(this.btnTahun);
            this.Controls.Add(this.btnRombel);
            this.Controls.Add(this.btnSumber);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.btnKondisi);
            this.Controls.Add(this.btnNonAktif);
            this.Controls.Add(this.btnSatuan);
            this.Controls.Add(this.btnMerk);
            this.Controls.Add(this.btnRuang);
            this.Controls.Add(this.btnLokasi);
            this.Controls.Add(this.btnBarang);
            this.Controls.Add(this.btnKategori);
            this.Name = "MasterDataForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Data";
            this.Load += new System.EventHandler(this.MasterDataForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button btnKategori;
        private System.Windows.Forms.Button btnBarang;
        private System.Windows.Forms.Button btnLokasi;
        private System.Windows.Forms.Button btnSatuan;
        private System.Windows.Forms.Button btnMerk;
        private System.Windows.Forms.Button btnRuang;
        private System.Windows.Forms.Button btnKondisi;
        private System.Windows.Forms.Button btnNonAktif;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSumber;
        private System.Windows.Forms.Button btnRombel;
        private System.Windows.Forms.Button btnTahun;
    }
}