namespace Assets_Inventory
{
    partial class AksesItemControl
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
            this.cbModul = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbModul
            // 
            this.cbModul.AutoSize = true;
            this.cbModul.Location = new System.Drawing.Point(18, 11);
            this.cbModul.Name = "cbModul";
            this.cbModul.Size = new System.Drawing.Size(173, 24);
            this.cbModul.TabIndex = 0;
            this.cbModul.Text = "Pengadaan Barang";
            this.cbModul.UseVisualStyleBackColor = true;
            // 
            // AksesItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbModul);
            this.Name = "AksesItemControl";
            this.Size = new System.Drawing.Size(212, 45);
            this.Load += new System.EventHandler(this.AksesItemControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbModul;
    }
}
