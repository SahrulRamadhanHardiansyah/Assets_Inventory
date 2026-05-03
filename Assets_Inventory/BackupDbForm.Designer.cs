namespace Assets_Inventory
{
    partial class BackupDbForm
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
            this.btnTutup = new System.Windows.Forms.Button();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMenit = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(496, 175);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 12;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnSimpan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMenit);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.btnBackup);
            this.groupBox1.Controls.Add(this.btnTutup);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Location = new System.Drawing.Point(37, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 234);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backup";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Menit";
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(496, 122);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 17;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Otomatis Backup Setiap";
            // 
            // txtMenit
            // 
            this.txtMenit.Location = new System.Drawing.Point(40, 129);
            this.txtMenit.Name = "txtMenit";
            this.txtMenit.Size = new System.Drawing.Size(177, 26);
            this.txtMenit.TabIndex = 15;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(496, 52);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(140, 40);
            this.btnBrowse.TabIndex = 14;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(326, 122);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(140, 40);
            this.btnBackup.TabIndex = 13;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Simpan Hasil Backup di";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(40, 59);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(426, 26);
            this.txtPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 22);
            this.label1.TabIndex = 25;
            this.label1.Text = "Gunakan Form Ini Untuk Backup Database Anda";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "BACKUP DATABASE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(34, 326);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(452, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Backup hanya bisa dilakukan di komputer yang dijadikan server";
            // 
            // BackupDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 371);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "BackupDbForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup Database";
            this.Load += new System.EventHandler(this.BackupDbForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTutup;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMenit;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label6;
    }
}