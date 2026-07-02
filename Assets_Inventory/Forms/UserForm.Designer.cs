namespace Assets_Inventory
{
    partial class UserForm
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
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.cmbPeran = new System.Windows.Forms.ComboBox();
            this.peranBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.btnUbah = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.idPenggunaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPeranNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idJurusanNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idKelasNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idMapelNavigationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFullname = new System.Windows.Forms.TextBox();
            this.cmbMapel = new System.Windows.Forms.ComboBox();
            this.mapelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblMapel = new System.Windows.Forms.Label();
            this.cmbJurusan = new System.Windows.Forms.ComboBox();
            this.jurusanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblJurusan = new System.Windows.Forms.Label();
            this.cmbKelas = new System.Windows.Forms.ComboBox();
            this.kelasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblKelas = new System.Windows.Forms.Label();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.peranBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jurusanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kelasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(232, 21);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(93, 26);
            this.btnHapus.TabIndex = 4;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(193, 226);
            this.btnBatal.Margin = new System.Windows.Forms.Padding(2);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(93, 26);
            this.btnBatal.TabIndex = 3;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(497, 16);
            this.btnTutup.Margin = new System.Windows.Forms.Padding(2);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(93, 26);
            this.btnTutup.TabIndex = 2;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(19, 21);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(93, 26);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // cmbPeran
            // 
            this.cmbPeran.DataSource = this.peranBindingSource;
            this.cmbPeran.DisplayMember = "NamaPeran";
            this.cmbPeran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeran.FormattingEnabled = true;
            this.cmbPeran.Location = new System.Drawing.Point(115, 124);
            this.cmbPeran.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPeran.Name = "cmbPeran";
            this.cmbPeran.Size = new System.Drawing.Size(173, 21);
            this.cmbPeran.TabIndex = 12;
            this.cmbPeran.ValueMember = "IdPeran";
            this.cmbPeran.SelectedIndexChanged += new System.EventHandler(this.cmbPeran_SelectedIndexChanged);
            // 
            // peranBindingSource
            // 
            this.peranBindingSource.DataSource = typeof(Assets_Inventory.Models.Peran);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 127);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Peran :";
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(126, 21);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(93, 26);
            this.btnUbah.TabIndex = 5;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUbah);
            this.groupBox3.Controls.Add(this.btnHapus);
            this.groupBox3.Controls.Add(this.btnTutup);
            this.groupBox3.Controls.Add(this.btnTambah);
            this.groupBox3.Location = new System.Drawing.Point(23, 335);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(607, 62);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proses";
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(17, 226);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(2);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(93, 26);
            this.btnSimpan.TabIndex = 1;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idPenggunaDataGridViewTextBoxColumn,
            this.usernameDataGridViewTextBoxColumn,
            this.FullName,
            this.idPeranNavigationDataGridViewTextBoxColumn,
            this.idJurusanNavigationDataGridViewTextBoxColumn,
            this.idKelasNavigationDataGridViewTextBoxColumn,
            this.idMapelNavigationDataGridViewTextBoxColumn});
            this.dg.DataSource = this.bindingSource1;
            this.dg.Location = new System.Drawing.Point(13, 45);
            this.dg.Margin = new System.Windows.Forms.Padding(2);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            this.dg.RowHeadersWidth = 62;
            this.dg.RowTemplate.Height = 28;
            this.dg.Size = new System.Drawing.Size(269, 207);
            this.dg.TabIndex = 4;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            this.dg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            // 
            // idPenggunaDataGridViewTextBoxColumn
            // 
            this.idPenggunaDataGridViewTextBoxColumn.DataPropertyName = "IdPengguna";
            this.idPenggunaDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idPenggunaDataGridViewTextBoxColumn.Name = "idPenggunaDataGridViewTextBoxColumn";
            this.idPenggunaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            this.usernameDataGridViewTextBoxColumn.DataPropertyName = "Username";
            this.usernameDataGridViewTextBoxColumn.HeaderText = "Username";
            this.usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            this.usernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "FullName";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // idPeranNavigationDataGridViewTextBoxColumn
            // 
            this.idPeranNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdPeranNavigation";
            this.idPeranNavigationDataGridViewTextBoxColumn.HeaderText = "Peran";
            this.idPeranNavigationDataGridViewTextBoxColumn.Name = "idPeranNavigationDataGridViewTextBoxColumn";
            this.idPeranNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idJurusanNavigationDataGridViewTextBoxColumn
            // 
            this.idJurusanNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdJurusanNavigation";
            this.idJurusanNavigationDataGridViewTextBoxColumn.HeaderText = "Jurusan";
            this.idJurusanNavigationDataGridViewTextBoxColumn.Name = "idJurusanNavigationDataGridViewTextBoxColumn";
            this.idJurusanNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idKelasNavigationDataGridViewTextBoxColumn
            // 
            this.idKelasNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdKelasNavigation";
            this.idKelasNavigationDataGridViewTextBoxColumn.HeaderText = "Kelas";
            this.idKelasNavigationDataGridViewTextBoxColumn.Name = "idKelasNavigationDataGridViewTextBoxColumn";
            this.idKelasNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idMapelNavigationDataGridViewTextBoxColumn
            // 
            this.idMapelNavigationDataGridViewTextBoxColumn.DataPropertyName = "IdMapelNavigation";
            this.idMapelNavigationDataGridViewTextBoxColumn.HeaderText = "Mapel";
            this.idMapelNavigationDataGridViewTextBoxColumn.Name = "idMapelNavigationDataGridViewTextBoxColumn";
            this.idMapelNavigationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Assets_Inventory.Models.Pengguna);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nama User :";
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Username", true));
            this.txtUsername.Location = new System.Drawing.Point(115, 21);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(173, 20);
            this.txtUsername.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCari);
            this.groupBox2.Controls.Add(this.txtCari);
            this.groupBox2.Controls.Add(this.dg);
            this.groupBox2.Location = new System.Drawing.Point(23, 63);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(294, 262);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data User";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(201, 16);
            this.btnCari.Margin = new System.Windows.Forms.Padding(2);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(81, 23);
            this.btnCari.TabIndex = 6;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(13, 20);
            this.txtCari.Margin = new System.Windows.Forms.Padding(2);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(173, 20);
            this.txtCari.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFullname);
            this.groupBox1.Controls.Add(this.cmbMapel);
            this.groupBox1.Controls.Add(this.lblMapel);
            this.groupBox1.Controls.Add(this.cmbJurusan);
            this.groupBox1.Controls.Add(this.lblJurusan);
            this.groupBox1.Controls.Add(this.cmbKelas);
            this.groupBox1.Controls.Add(this.lblKelas);
            this.groupBox1.Controls.Add(this.txtPassword2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnBatal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.btnSimpan);
            this.groupBox1.Controls.Add(this.cmbPeran);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Location = new System.Drawing.Point(328, 63);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(303, 262);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Nama Lengkap :";
            // 
            // txtFullname
            // 
            this.txtFullname.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FullName", true));
            this.txtFullname.Location = new System.Drawing.Point(115, 46);
            this.txtFullname.Margin = new System.Windows.Forms.Padding(2);
            this.txtFullname.Name = "txtFullname";
            this.txtFullname.Size = new System.Drawing.Size(173, 20);
            this.txtFullname.TabIndex = 26;
            // 
            // cmbMapel
            // 
            this.cmbMapel.DataSource = this.mapelBindingSource;
            this.cmbMapel.DisplayMember = "NamaMapel";
            this.cmbMapel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapel.Enabled = false;
            this.cmbMapel.FormattingEnabled = true;
            this.cmbMapel.Location = new System.Drawing.Point(115, 151);
            this.cmbMapel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbMapel.Name = "cmbMapel";
            this.cmbMapel.Size = new System.Drawing.Size(173, 21);
            this.cmbMapel.TabIndex = 25;
            this.cmbMapel.ValueMember = "IdMapel";
            this.cmbMapel.Visible = false;
            // 
            // mapelBindingSource
            // 
            this.mapelBindingSource.DataSource = typeof(Assets_Inventory.Models.Mapel);
            // 
            // lblMapel
            // 
            this.lblMapel.AutoSize = true;
            this.lblMapel.Enabled = false;
            this.lblMapel.Location = new System.Drawing.Point(63, 154);
            this.lblMapel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMapel.Name = "lblMapel";
            this.lblMapel.Size = new System.Drawing.Size(42, 13);
            this.lblMapel.TabIndex = 24;
            this.lblMapel.Text = "Mapel :";
            this.lblMapel.Visible = false;
            // 
            // cmbJurusan
            // 
            this.cmbJurusan.DataSource = this.jurusanBindingSource;
            this.cmbJurusan.DisplayMember = "NamaJurusan";
            this.cmbJurusan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJurusan.Enabled = false;
            this.cmbJurusan.FormattingEnabled = true;
            this.cmbJurusan.Location = new System.Drawing.Point(115, 177);
            this.cmbJurusan.Margin = new System.Windows.Forms.Padding(2);
            this.cmbJurusan.Name = "cmbJurusan";
            this.cmbJurusan.Size = new System.Drawing.Size(173, 21);
            this.cmbJurusan.TabIndex = 23;
            this.cmbJurusan.ValueMember = "IdJurusan";
            this.cmbJurusan.Visible = false;
            // 
            // jurusanBindingSource
            // 
            this.jurusanBindingSource.DataSource = typeof(Assets_Inventory.Models.Jurusan);
            // 
            // lblJurusan
            // 
            this.lblJurusan.AutoSize = true;
            this.lblJurusan.Enabled = false;
            this.lblJurusan.Location = new System.Drawing.Point(56, 180);
            this.lblJurusan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJurusan.Name = "lblJurusan";
            this.lblJurusan.Size = new System.Drawing.Size(50, 13);
            this.lblJurusan.TabIndex = 22;
            this.lblJurusan.Text = "Jurusan :";
            this.lblJurusan.Visible = false;
            // 
            // cmbKelas
            // 
            this.cmbKelas.DataSource = this.kelasBindingSource;
            this.cmbKelas.DisplayMember = "NamaKelas";
            this.cmbKelas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKelas.Enabled = false;
            this.cmbKelas.FormattingEnabled = true;
            this.cmbKelas.Location = new System.Drawing.Point(115, 151);
            this.cmbKelas.Margin = new System.Windows.Forms.Padding(2);
            this.cmbKelas.Name = "cmbKelas";
            this.cmbKelas.Size = new System.Drawing.Size(173, 21);
            this.cmbKelas.TabIndex = 21;
            this.cmbKelas.ValueMember = "IdKelas";
            this.cmbKelas.Visible = false;
            // 
            // kelasBindingSource
            // 
            this.kelasBindingSource.DataSource = typeof(Assets_Inventory.Models.Kelas);
            // 
            // lblKelas
            // 
            this.lblKelas.AutoSize = true;
            this.lblKelas.Enabled = false;
            this.lblKelas.Location = new System.Drawing.Point(67, 154);
            this.lblKelas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblKelas.Name = "lblKelas";
            this.lblKelas.Size = new System.Drawing.Size(39, 13);
            this.lblKelas.TabIndex = 20;
            this.lblKelas.Text = "Kelas :";
            this.lblKelas.Visible = false;
            // 
            // txtPassword2
            // 
            this.txtPassword2.Location = new System.Drawing.Point(115, 98);
            this.txtPassword2.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(173, 20);
            this.txtPassword2.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 101);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ulangi Password :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 74);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(115, 72);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(173, 20);
            this.txtPassword.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Gunakan Form Ini Untuk Menambah User / Pengguna Aplikasi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "DATA USER";
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
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(651, 412);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User";
            this.Load += new System.EventHandler(this.UserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.peranBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jurusanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kelasBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.ComboBox cmbPeran;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbMapel;
        private System.Windows.Forms.Label lblMapel;
        private System.Windows.Forms.ComboBox cmbJurusan;
        private System.Windows.Forms.Label lblJurusan;
        private System.Windows.Forms.ComboBox cmbKelas;
        private System.Windows.Forms.Label lblKelas;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource mapelBindingSource;
        private System.Windows.Forms.BindingSource kelasBindingSource;
        private System.Windows.Forms.BindingSource peranBindingSource;
        private System.Windows.Forms.BindingSource jurusanBindingSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPenggunaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPeranNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idJurusanNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idKelasNavigationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMapelNavigationDataGridViewTextBoxColumn;
    }
}