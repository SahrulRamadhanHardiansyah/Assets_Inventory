namespace Assets_Inventory
{
    partial class MainForm
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
            if (disposing)
            {
                try { db?.Dispose(); } catch { }
                if (components != null) components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dASHBOARDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iNVENTARISToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permintaanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pengadaanBarangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputTanahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inpiutBangunanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBarangAsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pROSESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mutasiBarangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prosesOpnameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barangNonAktifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peminjamanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pengembalianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lAPORANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laporanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bRGHABISPAKAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGudangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBarangHabisPakaiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permintaanHabisPakaiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pengadaanBarangHabisPakaiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barangHabisPakaiKeluarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laporanSTokToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lapStokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lapStokMinimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOOLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.koneksiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDMINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataLembagaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallpaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTanggal = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWaktu = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHost = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNotifAset = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerAutoBackup = new System.Windows.Forms.Timer(this.components);
            this.dataPeminjamanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPengembalianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dASHBOARDToolStripMenuItem,
            this.fILEToolStripMenuItem,
            this.iNVENTARISToolStripMenuItem,
            this.pROSESToolStripMenuItem,
            this.lAPORANToolStripMenuItem,
            this.bRGHABISPAKAIToolStripMenuItem,
            this.tOOLSToolStripMenuItem,
            this.aDMINToolStripMenuItem,
            this.hELPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1041, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dASHBOARDToolStripMenuItem
            // 
            this.dASHBOARDToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dASHBOARDToolStripMenuItem.Name = "dASHBOARDToolStripMenuItem";
            this.dASHBOARDToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.dASHBOARDToolStripMenuItem.Text = "DASHBOARD";
            this.dASHBOARDToolStripMenuItem.Click += new System.EventHandler(this.dASHBOARDToolStripMenuItem_Click);
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fILEToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(41, 22);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // iNVENTARISToolStripMenuItem
            // 
            this.iNVENTARISToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.permintaanToolStripMenuItem,
            this.pengadaanBarangToolStripMenuItem,
            this.inputTanahToolStripMenuItem,
            this.inpiutBangunanToolStripMenuItem,
            this.dataBarangAsetToolStripMenuItem});
            this.iNVENTARISToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iNVENTARISToolStripMenuItem.Name = "iNVENTARISToolStripMenuItem";
            this.iNVENTARISToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.iNVENTARISToolStripMenuItem.Text = "INVENTARIS";
            // 
            // permintaanToolStripMenuItem
            // 
            this.permintaanToolStripMenuItem.Name = "permintaanToolStripMenuItem";
            this.permintaanToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.permintaanToolStripMenuItem.Text = "Permintaan Barang";
            this.permintaanToolStripMenuItem.Click += new System.EventHandler(this.permintaanToolStripMenuItem_Click);
            // 
            // pengadaanBarangToolStripMenuItem
            // 
            this.pengadaanBarangToolStripMenuItem.Name = "pengadaanBarangToolStripMenuItem";
            this.pengadaanBarangToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.pengadaanBarangToolStripMenuItem.Text = "Pengadaan Barang";
            this.pengadaanBarangToolStripMenuItem.Click += new System.EventHandler(this.pengadaanBarangToolStripMenuItem_Click);
            // 
            // inputTanahToolStripMenuItem
            // 
            this.inputTanahToolStripMenuItem.Name = "inputTanahToolStripMenuItem";
            this.inputTanahToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.inputTanahToolStripMenuItem.Text = "Input Pengadaan Tanah";
            this.inputTanahToolStripMenuItem.Click += new System.EventHandler(this.inputTanahToolStripMenuItem_Click);
            // 
            // inpiutBangunanToolStripMenuItem
            // 
            this.inpiutBangunanToolStripMenuItem.Name = "inpiutBangunanToolStripMenuItem";
            this.inpiutBangunanToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.inpiutBangunanToolStripMenuItem.Text = "Input Pengadaan Bangunan";
            this.inpiutBangunanToolStripMenuItem.Click += new System.EventHandler(this.inpiutBangunanToolStripMenuItem_Click);
            // 
            // dataBarangAsetToolStripMenuItem
            // 
            this.dataBarangAsetToolStripMenuItem.Name = "dataBarangAsetToolStripMenuItem";
            this.dataBarangAsetToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.dataBarangAsetToolStripMenuItem.Text = "Data Barang Aset";
            this.dataBarangAsetToolStripMenuItem.Click += new System.EventHandler(this.dataBarangAsetToolStripMenuItem_Click);
            // 
            // pROSESToolStripMenuItem
            // 
            this.pROSESToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mutasiBarangToolStripMenuItem,
            this.prosesOpnameToolStripMenuItem,
            this.barangNonAktifToolStripMenuItem,
            this.peminjamanToolStripMenuItem,
            this.pengembalianToolStripMenuItem,
            this.dataPeminjamanToolStripMenuItem,
            this.dataPengembalianToolStripMenuItem});
            this.pROSESToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pROSESToolStripMenuItem.Name = "pROSESToolStripMenuItem";
            this.pROSESToolStripMenuItem.Size = new System.Drawing.Size(63, 22);
            this.pROSESToolStripMenuItem.Text = "PROSES";
            // 
            // mutasiBarangToolStripMenuItem
            // 
            this.mutasiBarangToolStripMenuItem.Name = "mutasiBarangToolStripMenuItem";
            this.mutasiBarangToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mutasiBarangToolStripMenuItem.Text = "Mutasi Barang";
            this.mutasiBarangToolStripMenuItem.Click += new System.EventHandler(this.mutasiBarangToolStripMenuItem_Click);
            // 
            // prosesOpnameToolStripMenuItem
            // 
            this.prosesOpnameToolStripMenuItem.Name = "prosesOpnameToolStripMenuItem";
            this.prosesOpnameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.prosesOpnameToolStripMenuItem.Text = "Proses Opname";
            this.prosesOpnameToolStripMenuItem.Click += new System.EventHandler(this.prosesOpnameToolStripMenuItem_Click);
            // 
            // barangNonAktifToolStripMenuItem
            // 
            this.barangNonAktifToolStripMenuItem.Name = "barangNonAktifToolStripMenuItem";
            this.barangNonAktifToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.barangNonAktifToolStripMenuItem.Text = "Barang Non-Aktif";
            this.barangNonAktifToolStripMenuItem.Click += new System.EventHandler(this.barangNonAktifToolStripMenuItem_Click);
            // 
            // peminjamanToolStripMenuItem
            // 
            this.peminjamanToolStripMenuItem.Name = "peminjamanToolStripMenuItem";
            this.peminjamanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.peminjamanToolStripMenuItem.Text = "Peminjaman";
            this.peminjamanToolStripMenuItem.Click += new System.EventHandler(this.peminjamanToolStripMenuItem_Click);
            // 
            // pengembalianToolStripMenuItem
            // 
            this.pengembalianToolStripMenuItem.Name = "pengembalianToolStripMenuItem";
            this.pengembalianToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pengembalianToolStripMenuItem.Text = "Pengembalian";
            this.pengembalianToolStripMenuItem.Click += new System.EventHandler(this.pengembalianToolStripMenuItem_Click);
            // 
            // lAPORANToolStripMenuItem
            // 
            this.lAPORANToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.laporanToolStripMenuItem1});
            this.lAPORANToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAPORANToolStripMenuItem.Name = "lAPORANToolStripMenuItem";
            this.lAPORANToolStripMenuItem.Size = new System.Drawing.Size(74, 22);
            this.lAPORANToolStripMenuItem.Text = "LAPORAN";
            // 
            // laporanToolStripMenuItem1
            // 
            this.laporanToolStripMenuItem1.Name = "laporanToolStripMenuItem1";
            this.laporanToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.laporanToolStripMenuItem1.Text = "Laporan";
            this.laporanToolStripMenuItem1.Click += new System.EventHandler(this.laporanToolStripMenuItem1_Click);
            // 
            // bRGHABISPAKAIToolStripMenuItem
            // 
            this.bRGHABISPAKAIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterDataToolStripMenuItem,
            this.dataBarangHabisPakaiToolStripMenuItem,
            this.permintaanHabisPakaiToolStripMenuItem,
            this.pengadaanBarangHabisPakaiToolStripMenuItem,
            this.barangHabisPakaiKeluarToolStripMenuItem,
            this.laporanSTokToolStripMenuItem1});
            this.bRGHABISPAKAIToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRGHABISPAKAIToolStripMenuItem.Name = "bRGHABISPAKAIToolStripMenuItem";
            this.bRGHABISPAKAIToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.bRGHABISPAKAIToolStripMenuItem.Text = "BRG HABIS PAKAI";
            // 
            // masterDataToolStripMenuItem
            // 
            this.masterDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataSupplierToolStripMenuItem,
            this.dataGudangToolStripMenuItem});
            this.masterDataToolStripMenuItem.Name = "masterDataToolStripMenuItem";
            this.masterDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.masterDataToolStripMenuItem.Text = "Master Data";
            this.masterDataToolStripMenuItem.Click += new System.EventHandler(this.masterDataToolStripMenuItem_Click);
            // 
            // dataSupplierToolStripMenuItem
            // 
            this.dataSupplierToolStripMenuItem.Name = "dataSupplierToolStripMenuItem";
            this.dataSupplierToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dataSupplierToolStripMenuItem.Text = "Data Supplier";
            this.dataSupplierToolStripMenuItem.Click += new System.EventHandler(this.dataSupplierToolStripMenuItem_Click);
            // 
            // dataGudangToolStripMenuItem
            // 
            this.dataGudangToolStripMenuItem.Name = "dataGudangToolStripMenuItem";
            this.dataGudangToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dataGudangToolStripMenuItem.Text = "Data Gudang";
            this.dataGudangToolStripMenuItem.Click += new System.EventHandler(this.dataGudangToolStripMenuItem_Click);
            // 
            // dataBarangHabisPakaiToolStripMenuItem
            // 
            this.dataBarangHabisPakaiToolStripMenuItem.Name = "dataBarangHabisPakaiToolStripMenuItem";
            this.dataBarangHabisPakaiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dataBarangHabisPakaiToolStripMenuItem.Text = "Data Barang";
            this.dataBarangHabisPakaiToolStripMenuItem.Click += new System.EventHandler(this.dataBarangHabisPakaiToolStripMenuItem_Click);
            // 
            // permintaanHabisPakaiToolStripMenuItem
            // 
            this.permintaanHabisPakaiToolStripMenuItem.Name = "permintaanHabisPakaiToolStripMenuItem";
            this.permintaanHabisPakaiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.permintaanHabisPakaiToolStripMenuItem.Text = "Permintaan";
            this.permintaanHabisPakaiToolStripMenuItem.Click += new System.EventHandler(this.permintaanHabisPakaiToolStripMenuItem_Click);
            // 
            // pengadaanBarangHabisPakaiToolStripMenuItem
            // 
            this.pengadaanBarangHabisPakaiToolStripMenuItem.Name = "pengadaanBarangHabisPakaiToolStripMenuItem";
            this.pengadaanBarangHabisPakaiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pengadaanBarangHabisPakaiToolStripMenuItem.Text = "Pengadaan";
            this.pengadaanBarangHabisPakaiToolStripMenuItem.Click += new System.EventHandler(this.pengadaanBarangHabisPakaiToolStripMenuItem_Click);
            // 
            // barangHabisPakaiKeluarToolStripMenuItem
            // 
            this.barangHabisPakaiKeluarToolStripMenuItem.Name = "barangHabisPakaiKeluarToolStripMenuItem";
            this.barangHabisPakaiKeluarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.barangHabisPakaiKeluarToolStripMenuItem.Text = "Barang Keluar";
            this.barangHabisPakaiKeluarToolStripMenuItem.Click += new System.EventHandler(this.barangHabisPakaiKeluarToolStripMenuItem_Click);
            // 
            // laporanSTokToolStripMenuItem1
            // 
            this.laporanSTokToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lapStokToolStripMenuItem,
            this.lapStokMinimalToolStripMenuItem});
            this.laporanSTokToolStripMenuItem1.Name = "laporanSTokToolStripMenuItem1";
            this.laporanSTokToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.laporanSTokToolStripMenuItem1.Text = "Laporan Stok";
            this.laporanSTokToolStripMenuItem1.Click += new System.EventHandler(this.laporanSTokToolStripMenuItem1_Click);
            // 
            // lapStokToolStripMenuItem
            // 
            this.lapStokToolStripMenuItem.Name = "lapStokToolStripMenuItem";
            this.lapStokToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.lapStokToolStripMenuItem.Text = "Lap. Stok";
            this.lapStokToolStripMenuItem.Click += new System.EventHandler(this.lapStokToolStripMenuItem_Click);
            // 
            // lapStokMinimalToolStripMenuItem
            // 
            this.lapStokMinimalToolStripMenuItem.Name = "lapStokMinimalToolStripMenuItem";
            this.lapStokMinimalToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.lapStokMinimalToolStripMenuItem.Text = "Lap. Stok Minimal";
            this.lapStokMinimalToolStripMenuItem.Click += new System.EventHandler(this.lapStokMinimalToolStripMenuItem_Click);
            // 
            // tOOLSToolStripMenuItem
            // 
            this.tOOLSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.koneksiToolStripMenuItem,
            this.backupToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.tOOLSToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tOOLSToolStripMenuItem.Name = "tOOLSToolStripMenuItem";
            this.tOOLSToolStripMenuItem.Size = new System.Drawing.Size(57, 22);
            this.tOOLSToolStripMenuItem.Text = "TOOLS";
            // 
            // koneksiToolStripMenuItem
            // 
            this.koneksiToolStripMenuItem.Name = "koneksiToolStripMenuItem";
            this.koneksiToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.koneksiToolStripMenuItem.Text = "Koneksi";
            this.koneksiToolStripMenuItem.Click += new System.EventHandler(this.koneksiToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // aDMINToolStripMenuItem
            // 
            this.aDMINToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterDataToolStripMenuItem1,
            this.dataLembagaToolStripMenuItem,
            this.groupUserToolStripMenuItem,
            this.userToolStripMenuItem,
            this.wallpaperToolStripMenuItem});
            this.aDMINToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aDMINToolStripMenuItem.Name = "aDMINToolStripMenuItem";
            this.aDMINToolStripMenuItem.Size = new System.Drawing.Size(60, 22);
            this.aDMINToolStripMenuItem.Text = "ADMIN";
            // 
            // masterDataToolStripMenuItem1
            // 
            this.masterDataToolStripMenuItem1.Name = "masterDataToolStripMenuItem1";
            this.masterDataToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.masterDataToolStripMenuItem1.Tag = "Master ";
            this.masterDataToolStripMenuItem1.Text = "Master Data";
            this.masterDataToolStripMenuItem1.Click += new System.EventHandler(this.masterDataToolStripMenuItem1_Click);
            // 
            // dataLembagaToolStripMenuItem
            // 
            this.dataLembagaToolStripMenuItem.Name = "dataLembagaToolStripMenuItem";
            this.dataLembagaToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.dataLembagaToolStripMenuItem.Text = "Data Lembaga";
            this.dataLembagaToolStripMenuItem.Click += new System.EventHandler(this.dataLembagaToolStripMenuItem_Click_1);
            // 
            // groupUserToolStripMenuItem
            // 
            this.groupUserToolStripMenuItem.Name = "groupUserToolStripMenuItem";
            this.groupUserToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.groupUserToolStripMenuItem.Text = "Group User";
            this.groupUserToolStripMenuItem.Click += new System.EventHandler(this.groupUserToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.userToolStripMenuItem.Text = "User";
            this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // wallpaperToolStripMenuItem
            // 
            this.wallpaperToolStripMenuItem.Name = "wallpaperToolStripMenuItem";
            this.wallpaperToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.wallpaperToolStripMenuItem.Text = "Wallpaper";
            this.wallpaperToolStripMenuItem.Click += new System.EventHandler(this.wallpaperToolStripMenuItem_Click);
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.tutorialToolStripMenuItem});
            this.hELPToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(47, 22);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tutorialToolStripMenuItem
            // 
            this.tutorialToolStripMenuItem.Name = "tutorialToolStripMenuItem";
            this.tutorialToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.tutorialToolStripMenuItem.Text = "Tutorial";
            this.tutorialToolStripMenuItem.Click += new System.EventHandler(this.tutorialToolStripMenuItem_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 24);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1041, 532);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTanggal,
            this.lblWaktu,
            this.lblHost,
            this.lblNum,
            this.lblTitle,
            this.lblUser,
            this.lblNotifAset});
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1041, 36);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTanggal
            // 
            this.lblTanggal.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblTanggal.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblTanggal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTanggal.Margin = new System.Windows.Forms.Padding(0, 4, 7, 3);
            this.lblTanggal.Name = "lblTanggal";
            this.lblTanggal.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblTanggal.Size = new System.Drawing.Size(156, 29);
            this.lblTanggal.Text = "Tanggal: 19 April 2026 ";
            // 
            // lblWaktu
            // 
            this.lblWaktu.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblWaktu.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblWaktu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaktu.Margin = new System.Windows.Forms.Padding(0, 4, 7, 3);
            this.lblWaktu.Name = "lblWaktu";
            this.lblWaktu.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblWaktu.Size = new System.Drawing.Size(100, 29);
            this.lblWaktu.Text = "05:56:00 PM";
            // 
            // lblHost
            // 
            this.lblHost.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblHost.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblHost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHost.Margin = new System.Windows.Forms.Padding(0, 4, 7, 3);
            this.lblHost.Name = "lblHost";
            this.lblHost.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblHost.Size = new System.Drawing.Size(112, 29);
            this.lblHost.Text = "Host: localhost";
            // 
            // lblNum
            // 
            this.lblNum.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblNum.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblNum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNum.Margin = new System.Windows.Forms.Padding(0, 4, 7, 3);
            this.lblNum.Name = "lblNum";
            this.lblNum.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblNum.Size = new System.Drawing.Size(60, 29);
            this.lblNum.Text = "NUM";
            // 
            // lblTitle
            // 
            this.lblTitle.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblTitle.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 4, 7, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(100, 5, 100, 5);
            this.lblTitle.Size = new System.Drawing.Size(380, 29);
            this.lblTitle.Text = "Aset Inventaris Sekolah V1.0.0";
            // 
            // lblUser
            // 
            this.lblUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblUser.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Margin = new System.Windows.Forms.Padding(0, 4, 7, 3);
            this.lblUser.Name = "lblUser";
            this.lblUser.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblUser.Size = new System.Drawing.Size(130, 29);
            this.lblUser.Text = "User Aktif: Admin";
            // 
            // lblNotifAset
            // 
            this.lblNotifAset.ForeColor = System.Drawing.Color.Red;
            this.lblNotifAset.Name = "lblNotifAset";
            this.lblNotifAset.Size = new System.Drawing.Size(118, 15);
            this.lblNotifAset.Text = "toolStripStatusLabel1";
            this.lblNotifAset.Visible = false;
            this.lblNotifAset.Click += new System.EventHandler(this.lblNotifAset_Click);
            // 
            // timerAutoBackup
            // 
            this.timerAutoBackup.Tick += new System.EventHandler(this.timerAutoBackup_Tick);
            // 
            // dataPeminjamanToolStripMenuItem
            // 
            this.dataPeminjamanToolStripMenuItem.Name = "dataPeminjamanToolStripMenuItem";
            this.dataPeminjamanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dataPeminjamanToolStripMenuItem.Text = "Data Peminjaman";
            this.dataPeminjamanToolStripMenuItem.Click += new System.EventHandler(this.dataPeminjamanToolStripMenuItem_Click);
            // 
            // dataPengembalianToolStripMenuItem
            // 
            this.dataPengembalianToolStripMenuItem.Name = "dataPengembalianToolStripMenuItem";
            this.dataPengembalianToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dataPengembalianToolStripMenuItem.Text = "Data Pengembalian";
            this.dataPengembalianToolStripMenuItem.Click += new System.EventHandler(this.dataPengembalianToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 556);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventaris Aset Sekolah - SMKN 1 Bangil";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iNVENTARISToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pROSESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lAPORANToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bRGHABISPAKAIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tOOLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDMINToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pengadaanBarangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputTanahToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inpiutBangunanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mutasiBarangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prosesOpnameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barangNonAktifToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peminjamanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pengembalianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laporanToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem masterDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataBarangHabisPakaiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pengadaanBarangHabisPakaiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barangHabisPakaiKeluarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem koneksiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataGudangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laporanSTokToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem masterDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dataLembagaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wallpaperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tutorialToolStripMenuItem;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTanggal;
        private System.Windows.Forms.ToolStripStatusLabel lblWaktu;
        private System.Windows.Forms.ToolStripStatusLabel lblHost;
        private System.Windows.Forms.ToolStripStatusLabel lblNum;
        private System.Windows.Forms.ToolStripStatusLabel lblTitle;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripMenuItem lapStokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lapStokMinimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permintaanToolStripMenuItem;
        private System.Windows.Forms.Timer timerAutoBackup;
        private System.Windows.Forms.ToolStripStatusLabel lblNotifAset;
        private System.Windows.Forms.ToolStripMenuItem dASHBOARDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permintaanHabisPakaiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataBarangAsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataPeminjamanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataPengembalianToolStripMenuItem;
    }
}