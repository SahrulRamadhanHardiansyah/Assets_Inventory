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
            this.btnTolak = new System.Windows.Forms.Button();
            this.btnLihatSteps = new System.Windows.Forms.Button();
            this.lblMultiInfo = new System.Windows.Forms.Label();
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
            // lblMultiInfo
            //
            this.lblMultiInfo.AutoSize = true;
            this.lblMultiInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblMultiInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblMultiInfo.Location = new System.Drawing.Point(37, 45);
            this.lblMultiInfo.Name = "lblMultiInfo";
            this.lblMultiInfo.Size = new System.Drawing.Size(200, 15);
            this.lblMultiInfo.TabIndex = 21;
            this.lblMultiInfo.Text = "Multi-level approval aktif";
            this.lblMultiInfo.Visible = false;
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
            this.txtAlasan.Size = new System.Drawing.Size(438, 90);
            this.txtAlasan.TabIndex = 35;
            //
            // btnLihatSteps
            //
            this.btnLihatSteps.Location = new System.Drawing.Point(340, 200);
            this.btnLihatSteps.Name = "btnLihatSteps";
            this.btnLihatSteps.Size = new System.Drawing.Size(138, 30);
            this.btnLihatSteps.TabIndex = 39;
            this.btnLihatSteps.Text = "Lihat Steps";
            this.btnLihatSteps.UseVisualStyleBackColor = true;
            this.btnLihatSteps.Visible = false;
            this.btnLihatSteps.Click += new System.EventHandler(this.btnLihatSteps_Click);
            //
            // btnTolak
            //
            this.btnTolak.Location = new System.Drawing.Point(188, 250);
            this.btnTolak.Name = "btnTolak";
            this.btnTolak.Size = new System.Drawing.Size(140, 40);
            this.btnTolak.TabIndex = 40;
            this.btnTolak.Text = "Tolak";
            this.btnTolak.UseVisualStyleBackColor = true;
            this.btnTolak.Click += new System.EventHandler(this.btnTolak_Click);
            //
            // btnTutup
            //
            this.btnTutup.Location = new System.Drawing.Point(340, 250);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 37;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            //
            // btnSetuju
            //
            this.btnSetuju.Location = new System.Drawing.Point(40, 250);
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
            this.ClientSize = new System.Drawing.Size(522, 330);
            this.Controls.Add(this.lblMultiInfo);
            this.Controls.Add(this.btnLihatSteps);
            this.Controls.Add(this.btnTolak);
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
            this.Text = "Persetujuan Form - Multi-level Support";
            this.Load += new System.EventHandler(this.PersetujuanForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblMultiInfo;
        private System.Windows.Forms.TextBox txtAlasan;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSetuju;
        private System.Windows.Forms.Button btnTolak;
        private System.Windows.Forms.Button btnLihatSteps;
    }
}
