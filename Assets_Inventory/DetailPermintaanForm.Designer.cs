namespace Assets_Inventory
{
    partial class DetailPermintaanForm
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
            this.dg = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnTutup = new System.Windows.Forms.Button();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.idMasterBarangNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jumlahDimintaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alasanKebutuhanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idMasterBarangNavigationDataGridViewTextBoxColumn,
            this.jumlahDimintaDataGridViewTextBoxColumn,
            this.alasanKebutuhanDataGridViewTextBoxColumn});
            this.dg.DataSource = this.bindingSource1;
            this.dg.Location = new System.Drawing.Point(24, 50);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.Size = new System.Drawing.Size(620, 225);
            this.dg.TabIndex = 34;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.DetailPermintaan);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(21, 19);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(166, 17);
            this.lblTitle.TabIndex = 33;
            this.lblTitle.Text = "DETAIL PERMINTAAN";
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(551, 293);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(93, 26);
            this.btnTutup.TabIndex = 35;
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
            // idMasterBarangNavigationDataGridViewTextBoxColumn
            // 
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdMasterBarangNavigation";
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.HeaderText = "Nama Barang";
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.Name = "idMasterBarangNavigationDataGridViewTextBoxColumn";
            this.idMasterBarangNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // jumlahDimintaDataGridViewTextBoxColumn
            // 
            this.jumlahDimintaDataGridViewTextBoxColumn.DataPropertyName = "JumlahDiminta";
            this.jumlahDimintaDataGridViewTextBoxColumn.HeaderText = "Jumlah Diminta";
            this.jumlahDimintaDataGridViewTextBoxColumn.Name = "jumlahDimintaDataGridViewTextBoxColumn";
            this.jumlahDimintaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // alasanKebutuhanDataGridViewTextBoxColumn
            // 
            this.alasanKebutuhanDataGridViewTextBoxColumn.DataPropertyName = "AlasanKebutuhan";
            this.alasanKebutuhanDataGridViewTextBoxColumn.HeaderText = "Alasan Kebutuhan";
            this.alasanKebutuhanDataGridViewTextBoxColumn.Name = "alasanKebutuhanDataGridViewTextBoxColumn";
            this.alasanKebutuhanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // DetailPermintaanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(667, 330);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnTutup);
            this.Name = "DetailPermintaanForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detail Permintaan Form";
            this.Load += new System.EventHandler(this.DetailPermintaanForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnTutup;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMasterBarangNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn jumlahDimintaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alasanKebutuhanDataGridViewTextBoxColumn;
    }
}