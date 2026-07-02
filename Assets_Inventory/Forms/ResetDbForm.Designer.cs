namespace Assets_Inventory
{
    partial class ResetDbForm
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
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnBarangHabisPakai = new System.Windows.Forms.Button();
            this.btnInventarisNonAktif = new System.Windows.Forms.Button();
            this.btnBangunan = new System.Windows.Forms.Button();
            this.btnTanah = new System.Windows.Forms.Button();
            this.btnBarang = new System.Windows.Forms.Button();
            this.btnMaster = new System.Windows.Forms.Button();
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
            // btnTutup
            // 
            this.btnTutup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTutup.Location = new System.Drawing.Point(27, 240);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(221, 29);
            this.btnTutup.TabIndex = 17;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnBarangHabisPakai
            // 
            this.btnBarangHabisPakai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarangHabisPakai.Location = new System.Drawing.Point(27, 180);
            this.btnBarangHabisPakai.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBarangHabisPakai.Name = "btnBarangHabisPakai";
            this.btnBarangHabisPakai.Size = new System.Drawing.Size(221, 29);
            this.btnBarangHabisPakai.TabIndex = 14;
            this.btnBarangHabisPakai.Text = "Reset Data Barang Habis Pakai";
            this.btnBarangHabisPakai.UseVisualStyleBackColor = true;
            this.btnBarangHabisPakai.Click += new System.EventHandler(this.btnBarangHabisPakai_Click);
            // 
            // btnInventarisNonAktif
            // 
            this.btnInventarisNonAktif.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventarisNonAktif.Location = new System.Drawing.Point(27, 147);
            this.btnInventarisNonAktif.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnInventarisNonAktif.Name = "btnInventarisNonAktif";
            this.btnInventarisNonAktif.Size = new System.Drawing.Size(221, 29);
            this.btnInventarisNonAktif.TabIndex = 13;
            this.btnInventarisNonAktif.Text = "Rest Semua Inventaris Non Aktif";
            this.btnInventarisNonAktif.UseVisualStyleBackColor = true;
            this.btnInventarisNonAktif.Click += new System.EventHandler(this.btnInventarisNonAktif_Click);
            // 
            // btnBangunan
            // 
            this.btnBangunan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBangunan.Location = new System.Drawing.Point(27, 114);
            this.btnBangunan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBangunan.Name = "btnBangunan";
            this.btnBangunan.Size = new System.Drawing.Size(221, 29);
            this.btnBangunan.TabIndex = 12;
            this.btnBangunan.Text = "Reset Data Bangunan";
            this.btnBangunan.UseVisualStyleBackColor = true;
            this.btnBangunan.Click += new System.EventHandler(this.btnBangunan_Click);
            // 
            // btnTanah
            // 
            this.btnTanah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTanah.Location = new System.Drawing.Point(27, 81);
            this.btnTanah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTanah.Name = "btnTanah";
            this.btnTanah.Size = new System.Drawing.Size(221, 29);
            this.btnTanah.TabIndex = 11;
            this.btnTanah.Text = "Reset Data Tanah";
            this.btnTanah.UseVisualStyleBackColor = true;
            this.btnTanah.Click += new System.EventHandler(this.btnTanah_Click);
            // 
            // btnBarang
            // 
            this.btnBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarang.Location = new System.Drawing.Point(27, 47);
            this.btnBarang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBarang.Name = "btnBarang";
            this.btnBarang.Size = new System.Drawing.Size(221, 29);
            this.btnBarang.TabIndex = 10;
            this.btnBarang.Text = "Reset Data Barang";
            this.btnBarang.UseVisualStyleBackColor = true;
            this.btnBarang.Click += new System.EventHandler(this.btnBarang_Click);
            // 
            // btnMaster
            // 
            this.btnMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaster.Location = new System.Drawing.Point(27, 14);
            this.btnMaster.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMaster.Name = "btnMaster";
            this.btnMaster.Size = new System.Drawing.Size(221, 29);
            this.btnMaster.TabIndex = 9;
            this.btnMaster.Text = "Reset Data Master";
            this.btnMaster.UseVisualStyleBackColor = true;
            this.btnMaster.Click += new System.EventHandler(this.btnMaster_Click);
            // 
            // ResetDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(279, 284);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.btnBarangHabisPakai);
            this.Controls.Add(this.btnInventarisNonAktif);
            this.Controls.Add(this.btnBangunan);
            this.Controls.Add(this.btnTanah);
            this.Controls.Add(this.btnBarang);
            this.Controls.Add(this.btnMaster);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ResetDbForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Database";
            this.Load += new System.EventHandler(this.ResetDbForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnBarangHabisPakai;
        private System.Windows.Forms.Button btnInventarisNonAktif;
        private System.Windows.Forms.Button btnBangunan;
        private System.Windows.Forms.Button btnTanah;
        private System.Windows.Forms.Button btnBarang;
        private System.Windows.Forms.Button btnMaster;
    }
}