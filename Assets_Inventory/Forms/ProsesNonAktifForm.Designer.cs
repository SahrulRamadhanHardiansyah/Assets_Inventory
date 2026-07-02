namespace Assets_Inventory
{
    partial class ProsesNonAktifForm
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
            this.btnBarang = new System.Windows.Forms.Button();
            this.btnTanah = new System.Windows.Forms.Button();
            this.btnBangunan = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.SuspendLayout();
            // 
            // btnBarang
            // 
            this.btnBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarang.Location = new System.Drawing.Point(29, 29);
            this.btnBarang.Name = "btnBarang";
            this.btnBarang.Size = new System.Drawing.Size(407, 41);
            this.btnBarang.TabIndex = 0;
            this.btnBarang.Text = "Proses Non Akitf Barang Inventaris";
            this.btnBarang.UseVisualStyleBackColor = true;
            this.btnBarang.Click += new System.EventHandler(this.btnBarang_Click);
            // 
            // btnTanah
            // 
            this.btnTanah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTanah.Location = new System.Drawing.Point(29, 76);
            this.btnTanah.Name = "btnTanah";
            this.btnTanah.Size = new System.Drawing.Size(407, 41);
            this.btnTanah.TabIndex = 1;
            this.btnTanah.Text = "Proses Non Akitf Tanah Inventaris";
            this.btnTanah.UseVisualStyleBackColor = true;
            this.btnTanah.Click += new System.EventHandler(this.btnTanah_Click);
            // 
            // btnBangunan
            // 
            this.btnBangunan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBangunan.Location = new System.Drawing.Point(29, 123);
            this.btnBangunan.Name = "btnBangunan";
            this.btnBangunan.Size = new System.Drawing.Size(407, 41);
            this.btnBangunan.TabIndex = 2;
            this.btnBangunan.Text = "Proses Non Akitf Bangunan Inventaris";
            this.btnBangunan.UseVisualStyleBackColor = true;
            this.btnBangunan.Click += new System.EventHandler(this.btnBangunan_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTutup.Location = new System.Drawing.Point(320, 199);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(116, 41);
            this.btnTutup.TabIndex = 3;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
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
            // ProsesNonAktifForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 261);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.btnBangunan);
            this.Controls.Add(this.btnTanah);
            this.Controls.Add(this.btnBarang);
            this.Name = "ProsesNonAktifForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proses Non Aktif";
            this.Load += new System.EventHandler(this.ProsesNonAktifForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBarang;
        private System.Windows.Forms.Button btnTanah;
        private System.Windows.Forms.Button btnBangunan;
        private System.Windows.Forms.Button btnTutup;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
    }
}