namespace Assets_Inventory
{
    partial class MasterSatuanForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dg = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtNama = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dg);
            this.groupBox2.Location = new System.Drawing.Point(35, 288);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(715, 303);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Satuan";
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(18, 27);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(677, 260);
            this.dg.TabIndex = 4;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtKeterangan);
            this.groupBox1.Controls.Add(this.txtNama);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtKode);
            this.groupBox1.Location = new System.Drawing.Point(36, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(715, 194);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail Satuan";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(56, 116);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 20);
            this.label20.TabIndex = 36;
            this.label20.Text = "Keterangan :";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Keterangan", true));
            this.txtKeterangan.Location = new System.Drawing.Point(172, 113);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(327, 60);
            this.txtKeterangan.TabIndex = 35;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.SatuanResource);
            // 
            // txtNama
            // 
            this.txtNama.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Nama_satuan", true));
            this.txtNama.Location = new System.Drawing.Point(172, 73);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(327, 26);
            this.txtNama.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nama Satuan :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Kode Satuan :";
            // 
            // txtKode
            // 
            this.txtKode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Id_satuan", true));
            this.txtKode.Location = new System.Drawing.Point(172, 33);
            this.txtKode.Name = "txtKode";
            this.txtKode.ReadOnly = true;
            this.txtKode.Size = new System.Drawing.Size(327, 26);
            this.txtKode.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 22);
            this.label1.TabIndex = 33;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Master Merek";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "MASTER SATUAN";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBatal);
            this.groupBox3.Controls.Add(this.btnUbah);
            this.groupBox3.Controls.Add(this.btnHapus);
            this.groupBox3.Controls.Add(this.btnImport);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnSimpan);
            this.groupBox3.Controls.Add(this.btnTambah);
            this.groupBox3.Location = new System.Drawing.Point(36, 604);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(714, 143);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(113, 84);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(140, 40);
            this.btnBatal.TabIndex = 13;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(200, 32);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(140, 40);
            this.btnUbah.TabIndex = 12;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(550, 32);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(140, 40);
            this.btnHapus.TabIndex = 11;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(291, 84);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(140, 40);
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(465, 84);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 9;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(377, 32);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 8;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(24, 32);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 40);
            this.btnTambah.TabIndex = 7;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
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
            // MasterSatuanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(785, 758);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Name = "MasterSatuanForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Satuan";
            this.Load += new System.EventHandler(this.MasterSatuanForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}