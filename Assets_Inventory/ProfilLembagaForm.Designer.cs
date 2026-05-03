namespace Assets_Inventory
{
    partial class ProfilLembagaForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtInventaris = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNip = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtKepsek = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKota = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTelp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.cbHapus = new System.Windows.Forms.CheckBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nama";
            // 
            // txtNama
            // 
            this.txtNama.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Nama_instansi", true));
            this.txtNama.Location = new System.Drawing.Point(30, 64);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(307, 26);
            this.txtNama.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSimpan);
            this.groupBox1.Controls.Add(this.btnTutup);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtInventaris);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtNip);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtKepsek);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtKota);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtWebsite);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtTelp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAlamat);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNama);
            this.groupBox1.Location = new System.Drawing.Point(249, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 464);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Identitas Lembaga";
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(378, 412);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(140, 40);
            this.btnSimpan.TabIndex = 26;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(545, 412);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(140, 40);
            this.btnTutup.TabIndex = 25;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(374, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "Bagian Inventaris";
            // 
            // txtInventaris
            // 
            this.txtInventaris.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Bagian_inventaris", true));
            this.txtInventaris.Location = new System.Drawing.Point(378, 206);
            this.txtInventaris.Name = "txtInventaris";
            this.txtInventaris.Size = new System.Drawing.Size(307, 26);
            this.txtInventaris.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(374, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 20);
            this.label13.TabIndex = 22;
            this.label13.Text = "NIP";
            // 
            // txtNip
            // 
            this.txtNip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NIP", true));
            this.txtNip.Location = new System.Drawing.Point(378, 134);
            this.txtNip.Name = "txtNip";
            this.txtNip.Size = new System.Drawing.Size(307, 26);
            this.txtNip.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(374, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 20);
            this.label14.TabIndex = 20;
            this.label14.Text = "Kepala Sekolah";
            // 
            // txtKepsek
            // 
            this.txtKepsek.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Kepala_sekolah", true));
            this.txtKepsek.Location = new System.Drawing.Point(378, 64);
            this.txtKepsek.Name = "txtKepsek";
            this.txtKepsek.Size = new System.Drawing.Size(307, 26);
            this.txtKepsek.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 391);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Kota";
            // 
            // txtKota
            // 
            this.txtKota.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Kota", true));
            this.txtKota.Location = new System.Drawing.Point(30, 419);
            this.txtKota.Name = "txtKota";
            this.txtKota.Size = new System.Drawing.Size(307, 26);
            this.txtKota.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 321);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Website";
            // 
            // txtWebsite
            // 
            this.txtWebsite.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Website", true));
            this.txtWebsite.Location = new System.Drawing.Point(30, 349);
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(307, 26);
            this.txtWebsite.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Email", true));
            this.txtEmail.Location = new System.Drawing.Point(30, 276);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(307, 26);
            this.txtEmail.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Telepon";
            // 
            // txtTelp
            // 
            this.txtTelp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Telpon", true));
            this.txtTelp.Location = new System.Drawing.Point(30, 206);
            this.txtTelp.Name = "txtTelp";
            this.txtTelp.Size = new System.Drawing.Size(307, 26);
            this.txtTelp.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Alamat";
            // 
            // txtAlamat
            // 
            this.txtAlamat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Alamat_instansi", true));
            this.txtAlamat.Location = new System.Drawing.Point(30, 134);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(307, 26);
            this.txtAlamat.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 22);
            this.label1.TabIndex = 23;
            this.label1.Text = "Gunakan Form Ini Untuk Mengisi Identitas Lembaga";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 25);
            this.label2.TabIndex = 22;
            this.label2.Text = "PROFIL LEMBAGA";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.pbLogo);
            this.groupBox2.Location = new System.Drawing.Point(33, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 269);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logo";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 223);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(186, 34);
            this.btnLoad.TabIndex = 26;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(6, 25);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(186, 192);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // cbHapus
            // 
            this.cbHapus.AutoSize = true;
            this.cbHapus.Location = new System.Drawing.Point(33, 395);
            this.cbHapus.Name = "cbHapus";
            this.cbHapus.Size = new System.Drawing.Size(122, 24);
            this.cbHapus.TabIndex = 26;
            this.cbHapus.Text = "Hapus Logo";
            this.cbHapus.UseVisualStyleBackColor = true;
            this.cbHapus.CheckedChanged += new System.EventHandler(this.cbHapus_CheckedChanged);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.PengaturanResource);
            // 
            // ProfilLembagaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1013, 584);
            this.Controls.Add(this.cbHapus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "ProfilLembagaForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profil Lembaga";
            this.Load += new System.EventHandler(this.ProfilLembagaForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.CheckBox cbHapus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTelp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtInventaris;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNip;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtKepsek;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtKota;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}