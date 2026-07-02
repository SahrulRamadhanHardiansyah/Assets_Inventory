namespace Assets_Inventory
{
    partial class PersetujuanForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtAlasan = new System.Windows.Forms.TextBox();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSetuju = new System.Windows.Forms.Button();
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
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(37, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(169, 17);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "PERSETUJUAN FORM";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(36, 69);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(131, 20);
            this.label20.TabIndex = 36;
            this.label20.Text = "Alasan Disetujui :";
            // 
            // txtAlasan
            // 
            this.txtAlasan.Location = new System.Drawing.Point(40, 97);
            this.txtAlasan.Multiline = true;
            this.txtAlasan.Name = "txtAlasan";
            this.txtAlasan.Size = new System.Drawing.Size(438, 70);
            this.txtAlasan.TabIndex = 35;
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(340, 246);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 37;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSetuju
            // 
            this.btnSetuju.Location = new System.Drawing.Point(40, 246);
            this.btnSetuju.Name = "btnSetuju";
            this.btnSetuju.Size = new System.Drawing.Size(140, 40);
            this.btnSetuju.TabIndex = 38;
            this.btnSetuju.Text = "Setujui";
            this.btnSetuju.UseVisualStyleBackColor = true;
            this.btnSetuju.Click += new System.EventHandler(this.btnSetuju_Click);
            // 
            // PersetujuanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(522, 312);
            this.Controls.Add(this.btnSetuju);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtAlasan);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PersetujuanForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Persetujuan Form";
            this.Load += new System.EventHandler(this.PersetujuanForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtAlasan;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSetuju;
    }
}