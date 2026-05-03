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
            this.btnHbsPakai = new System.Windows.Forms.Button();
            this.btnInvNonAktif = new System.Windows.Forms.Button();
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
            this.btnTutup.Location = new System.Drawing.Point(41, 369);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(331, 45);
            this.btnTutup.TabIndex = 17;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnHbsPakai
            // 
            this.btnHbsPakai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHbsPakai.Location = new System.Drawing.Point(41, 277);
            this.btnHbsPakai.Name = "btnHbsPakai";
            this.btnHbsPakai.Size = new System.Drawing.Size(331, 45);
            this.btnHbsPakai.TabIndex = 14;
            this.btnHbsPakai.Text = "Reset Data Barang Habis Pakai";
            this.btnHbsPakai.UseVisualStyleBackColor = true;
            // 
            // btnInvNonAktif
            // 
            this.btnInvNonAktif.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvNonAktif.Location = new System.Drawing.Point(41, 226);
            this.btnInvNonAktif.Name = "btnInvNonAktif";
            this.btnInvNonAktif.Size = new System.Drawing.Size(331, 45);
            this.btnInvNonAktif.TabIndex = 13;
            this.btnInvNonAktif.Text = "Rest Semua Inventaris Non Aktif";
            this.btnInvNonAktif.UseVisualStyleBackColor = true;
            // 
            // btnBangunan
            // 
            this.btnBangunan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBangunan.Location = new System.Drawing.Point(41, 175);
            this.btnBangunan.Name = "btnBangunan";
            this.btnBangunan.Size = new System.Drawing.Size(331, 45);
            this.btnBangunan.TabIndex = 12;
            this.btnBangunan.Text = "Reset Data Bangunan";
            this.btnBangunan.UseVisualStyleBackColor = true;
            // 
            // btnTanah
            // 
            this.btnTanah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTanah.Location = new System.Drawing.Point(41, 124);
            this.btnTanah.Name = "btnTanah";
            this.btnTanah.Size = new System.Drawing.Size(331, 45);
            this.btnTanah.TabIndex = 11;
            this.btnTanah.Text = "Reset Data Tanah";
            this.btnTanah.UseVisualStyleBackColor = true;
            // 
            // btnBarang
            // 
            this.btnBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarang.Location = new System.Drawing.Point(41, 73);
            this.btnBarang.Name = "btnBarang";
            this.btnBarang.Size = new System.Drawing.Size(331, 45);
            this.btnBarang.TabIndex = 10;
            this.btnBarang.Text = "Reset Data Barang";
            this.btnBarang.UseVisualStyleBackColor = true;
            // 
            // btnMaster
            // 
            this.btnMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaster.Location = new System.Drawing.Point(41, 22);
            this.btnMaster.Name = "btnMaster";
            this.btnMaster.Size = new System.Drawing.Size(331, 45);
            this.btnMaster.TabIndex = 9;
            this.btnMaster.Text = "Reset Data Master";
            this.btnMaster.UseVisualStyleBackColor = true;
            // 
            // ResetDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(418, 437);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.btnHbsPakai);
            this.Controls.Add(this.btnInvNonAktif);
            this.Controls.Add(this.btnBangunan);
            this.Controls.Add(this.btnTanah);
            this.Controls.Add(this.btnBarang);
            this.Controls.Add(this.btnMaster);
            this.Name = "ResetDbForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Database";
            this.Load += new System.EventHandler(this.ResetDbForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnHbsPakai;
        private System.Windows.Forms.Button btnInvNonAktif;
        private System.Windows.Forms.Button btnBangunan;
        private System.Windows.Forms.Button btnTanah;
        private System.Windows.Forms.Button btnBarang;
        private System.Windows.Forms.Button btnMaster;
    }
}