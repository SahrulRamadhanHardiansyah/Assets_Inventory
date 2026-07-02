using Assets_Inventory.Forms;
using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class DataBarangHabisPakaiKeluarUC : UserControl
    {
        AppDbContext db = new AppDbContext();

        public class BarangKeluarViewModel
        {
            public int NoTransaksi { get; set; }
            public DateTime TanggalKeluar { get; set; }
            public string KodeBarang { get; set; }
            public string NamaBarang { get; set; }
            public int JumlahKeluar { get; set; }
            public string Gudang { get; set; }
            public string Ruang { get; set; }
            public string Penerima { get; set; }
            public BarangKeluar ObjekAsli { get; set; }
        }

        public DataBarangHabisPakaiKeluarUC()
        {
            InitializeComponent();
        }

        private void LoadComboBoxFilters()
        {
            var listGudang = db.Gudang.ToList();
            listGudang.Insert(0, new Gudang { KodeGudang = "", NamaGudang = "-- Semua Gudang --" });
            cmbFilterGudang.DataSource = listGudang;
            cmbFilterGudang.DisplayMember = "NamaGudang";
            cmbFilterGudang.ValueMember = "KodeGudang";

            var listRuang = db.Ruang.ToList();
            listRuang.Insert(0, new Ruang { IdRuang = 0, NamaRuang = "-- Semua Ruang --" });
            cmbFilterRuang.DataSource = listRuang;
            cmbFilterRuang.DisplayMember = "NamaRuang";
            cmbFilterRuang.ValueMember = "IdRuang";
        }

        private void loadData()
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cari = txtCari.Text.ToLower().Trim();
            var tglMulai = dtpMulai.Value.Date;
            var tglSampai = dtpSampai.Value.Date.AddDays(1).AddTicks(-1);

            string filterGudang = cmbFilterGudang.SelectedValue?.ToString() ?? "";
            int filterRuang = cmbFilterRuang.SelectedValue != null ? (int)cmbFilterRuang.SelectedValue : 0;

            var dictPengguna = db.Pengguna.ToDictionary(p => p.IdPengguna, p => p.Username);
            var dictBarang = db.MasterBarang.ToDictionary(m => m.IdMasterBarang, m => m.NamaBarang);
            var dictAset = db.AsetHabisPakai.AsNoTracking().ToDictionary(a => a.KodeBarang, a => a.IdMasterBarang);

            var dictRuang = db.Ruang.ToDictionary(r => r.IdRuang, r => r.NamaRuang);
            var dictGudang = db.Gudang.ToDictionary(g => g.KodeGudang, g => g.NamaGudang);

            var query = db.BarangKeluar.AsNoTracking()
                .Where(b => b.TanggalKeluar >= tglMulai && b.TanggalKeluar <= tglSampai);

            if (!string.IsNullOrEmpty(filterGudang)) query = query.Where(b => b.KodeGudang == filterGudang);
            if (filterRuang != 0) query = query.Where(b => b.IdRuang == filterRuang);

            var dataRaw = query.ToList();

            var dataTampil = dataRaw.Select(b => {
                string namaBrg = "N/A";

                if (!string.IsNullOrEmpty(b.KodeBarang) && dictAset.ContainsKey(b.KodeBarang))
                {
                    int idMaster = dictAset[b.KodeBarang];
                    if (dictBarang.ContainsKey(idMaster))
                    {
                        namaBrg = dictBarang[idMaster];
                    }
                }

                string namaPenerima = dictPengguna.ContainsKey(b.NamaPenerima) ? dictPengguna[b.NamaPenerima] : "-";
                string namaRuang = (b.IdRuang.HasValue && dictRuang.ContainsKey(b.IdRuang.Value)) ? dictRuang[b.IdRuang.Value] : "-";
                string namaGudang = (!string.IsNullOrEmpty(b.KodeGudang) && dictGudang.ContainsKey(b.KodeGudang)) ? dictGudang[b.KodeGudang] : "-";

                return new BarangKeluarViewModel
                {
                    NoTransaksi = b.NoTransaksi,
                    TanggalKeluar = b.TanggalKeluar,
                    KodeBarang = b.KodeBarang,
                    NamaBarang = namaBrg,
                    JumlahKeluar = b.JumlahKeluar, // Dihapus operator ?? 1
                    Gudang = namaGudang,
                    Ruang = namaRuang,
                    Penerima = namaPenerima,
                    ObjekAsli = b
                };
            })
            .Where(b => b.NoTransaksi.ToString().Contains(cari) ||
                        b.KodeBarang.ToLower().Contains(cari) ||
                        b.NamaBarang.ToLower().Contains(cari) ||
                        b.Penerima.ToLower().Contains(cari))
            .OrderByDescending(b => b.TanggalKeluar)
            .ToList();

            dg.DataSource = new SortableBindingList<BarangKeluarViewModel>(dataTampil);
            lblTotal.Text = $"Total Record : {dataTampil.Count}";

            if (dg.Columns["NoTransaksi"] != null) dg.Columns["NoTransaksi"].HeaderText = "No. Transaksi";
            if (dg.Columns["TanggalKeluar"] != null)
            {
                dg.Columns["TanggalKeluar"].HeaderText = "Tanggal";
                dg.Columns["TanggalKeluar"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
            }
            if (dg.Columns["KodeBarang"] != null) dg.Columns["KodeBarang"].HeaderText = "Kode Barang";
            if (dg.Columns["NamaBarang"] != null) dg.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dg.Columns["JumlahKeluar"] != null) dg.Columns["JumlahKeluar"].HeaderText = "Jumlah Keluar";
            if (dg.Columns["Gudang"] != null) dg.Columns["Gudang"].HeaderText = "Gudang Asal";
            if (dg.Columns["Ruang"] != null) dg.Columns["Ruang"].HeaderText = "Ruang Tujuan";
            if (dg.Columns["Penerima"] != null) dg.Columns["Penerima"].HeaderText = "Nama Penerima";
            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
        }

        private void btnCari_Click(object sender, EventArgs e) => loadData();
        private void txtCari_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; loadData(); } }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            InputBarangHabisPakaiKeluarForm form = new InputBarangHabisPakaiKeluarForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        //private void btnUbah_Click(object sender, EventArgs e)
        //{
        //    if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is BarangKeluarViewModel vm)
        //    {
        //        var keluar = db.BarangKeluar.Find(vm.NoTransaksi);
        //        InputBarangHabisPakaiKeluarForm form = new InputBarangHabisPakaiKeluarForm();
        //        form.selectedKeluar = keluar;
        //        if (form.ShowDialog() == DialogResult.OK) loadData();
        //    }
        //    else MessageBox.Show("Pilih data yang ingin diubah terlebih dahulu.");
        //}

        //private void btnHapus_Click(object sender, EventArgs e)
        //{
        //    if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is BarangKeluarViewModel vm)
        //    {
        //        if (MessageBox.Show($"Yakin ingin menghapus transaksi keluar No.{vm.NoTransaksi}?\nStok aset akan dikembalikan ke gudang.", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                var keluar = db.BarangKeluar.Find(vm.NoTransaksi);

        //                var aset = db.AsetHabisPakai.FirstOrDefault(a => a.KodeBarang == keluar.KodeBarang);
        //                if (aset != null)
        //                {
        //                    aset.StokAktual += keluar.JumlahKeluar;
        //                    if (aset.StokAktual > 0) aset.Status = "Tersedia";
        //                }

        //                db.BarangKeluar.Remove(keluar);
        //                db.SaveChanges();
        //                loadData();
        //            }
        //            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        //        }
        //    }
        //}

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is BarangKeluarViewModel vm)
            {
                var keluar = db.BarangKeluar.Find(vm.NoTransaksi);
                InputBarangHabisPakaiKeluarForm form = new InputBarangHabisPakaiKeluarForm();
                form.selectedKeluar = keluar;
                form.isDetailMode = true;
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pilih data transaksi yang ingin dilihat detailnya terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataBarangHabisPakaiKeluarUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Barang Keluar");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainForm parentForm = this.ParentForm as MainForm;
                DashboardUC dashboardUC = new DashboardUC();
                if (parentForm != null) parentForm.ChangeView(dashboardUC);
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnImport.Enabled = hakAkses.HakBuat;

            dtpMulai.Value = DateTime.Now.AddMonths(-1);
            dtpSampai.Value = DateTime.Now;

            LoadComboBoxFilters();

            loadData();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xlsx;*.xls;*.xlsm";
                ofd.Title = "Pilih File Excel Transaksi Barang Keluar";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.UseWaitCursor = true;
                        if (this.ParentForm != null) this.ParentForm.Cursor = Cursors.WaitCursor;

                        int sukses = 0;
                        int gagal = 0;

                        var dictPengguna = db.Pengguna.ToDictionary(u => u.Username.ToLower(), u => u.IdPengguna);
                        var dictRuang = db.Ruang.ToDictionary(r => r.NamaRuang.ToLower(), r => r.IdRuang);
                        var dictGudang = db.Gudang.Select(g => g.KodeGudang).ToHashSet();

                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });

                                DataTable dt = result.Tables[0];

                                foreach (DataRow row in dt.Rows)
                                {
                                    Application.DoEvents();

                                    string strTanggal = row[0]?.ToString().Trim();
                                    string kodeBrg = row[1]?.ToString().Trim();
                                    string namaPenerima = row[2]?.ToString().Trim().ToLower();
                                    string namaPetugas = row[3]?.ToString().Trim().ToLower();
                                    string kodeGudang = row[4]?.ToString().Trim();
                                    string namaRuang = row[5]?.ToString().Trim().ToLower();
                                    string jumlahStr = row[6]?.ToString().Trim(); // Misal kolom 6 untuk jumlah
                                    string keterangan = row[7]?.ToString().Trim();

                                    if (!string.IsNullOrEmpty(kodeBrg) && DateTime.TryParse(strTanggal, out DateTime tglKeluar))
                                    {
                                        int idPenerima = dictPengguna.ContainsKey(namaPenerima) ? dictPengguna[namaPenerima] : 1;
                                        int idPetugas = dictPengguna.ContainsKey(namaPetugas) ? dictPengguna[namaPetugas] : 1;
                                        int? idRuang = dictRuang.ContainsKey(namaRuang) ? dictRuang[namaRuang] : (int?)null;
                                        string gudangFinal = dictGudang.Contains(kodeGudang) ? kodeGudang : null;

                                        int jumlahKeluar = int.TryParse(jumlahStr, out int qty) ? qty : 1;

                                        // Cek ketersediaan stok
                                        var aset = db.AsetHabisPakai.FirstOrDefault(a => a.KodeBarang == kodeBrg);
                                        if (aset != null && aset.StokAktual >= jumlahKeluar)
                                        {
                                            var baru = new BarangKeluar
                                            {
                                                TanggalKeluar = tglKeluar,
                                                KodeBarang = kodeBrg,
                                                JumlahKeluar = jumlahKeluar,
                                                Keterangan = keterangan,
                                                IdRuang = idRuang,
                                                KodeGudang = gudangFinal,
                                                NamaPenerima = idPenerima,
                                                Petugas = idPetugas
                                            };
                                            db.BarangKeluar.Add(baru);

                                            // Potong stok otomatis dari import
                                            aset.StokAktual -= jumlahKeluar;
                                            if (aset.StokAktual <= 0) aset.Status = "Habis";

                                            sukses++;
                                        }
                                        else
                                        {
                                            gagal++; // Gagal jika stok tidak cukup atau barang tidak ada
                                        }
                                    }
                                    else
                                    {
                                        gagal++;
                                    }
                                }

                                db.SaveChanges();
                            }
                        }

                        MessageBox.Show($"Proses Import Selesai!\n\nBerhasil: {sukses} transaksi\nGagal/Stok Kosong: {gagal} baris", "Info Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka atau digunakan oleh program lain (seperti Microsoft Excel). Harap tutup file tersebut dan coba lagi.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan sistem saat membaca file Excel: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.UseWaitCursor = false;
                        if (this.ParentForm != null) this.ParentForm.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dg.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data di tabel untuk diexport.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                sfd.Title = "Export Data Transaksi Barang Keluar ke Excel";
                sfd.FileName = "Laporan_Barang_Habis_Pakai_Keluar_" + DateTime.Now.ToString("yyyyMMdd");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;

                        using (var package = new OfficeOpenXml.ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Data Barang Keluar");

                            string[] headers = { "No. Transaksi", "Tanggal Keluar", "Kode Barang", "Nama Barang", "Jumlah", "Gudang Asal", "Ruang Tujuan", "Nama Penerima" };
                            for (int i = 0; i < headers.Length; i++)
                            {
                                worksheet.Cells[1, i + 1].Value = headers[i];
                            }

                            using (var range = worksheet.Cells[1, 1, 1, headers.Length])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            }

                            int baris = 2;
                            foreach (DataGridViewRow row in dg.Rows)
                            {
                                if (row.DataBoundItem is BarangKeluarViewModel vm)
                                {
                                    Application.DoEvents();

                                    worksheet.Cells[baris, 1].Value = vm.NoTransaksi;
                                    worksheet.Cells[baris, 2].Value = vm.TanggalKeluar.ToString("yyyy-MM-dd HH:mm");
                                    worksheet.Cells[baris, 3].Value = vm.KodeBarang;
                                    worksheet.Cells[baris, 4].Value = vm.NamaBarang;
                                    worksheet.Cells[baris, 5].Value = vm.JumlahKeluar;
                                    worksheet.Cells[baris, 6].Value = vm.Gudang;
                                    worksheet.Cells[baris, 7].Value = vm.Ruang;
                                    worksheet.Cells[baris, 8].Value = vm.Penerima;

                                    baris++;
                                }
                            }

                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                            System.IO.FileInfo fi = new System.IO.FileInfo(sfd.FileName);
                            package.SaveAs(fi);
                        }

                        MessageBox.Show("Data berhasil diekspor ke Excel!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat mengekspor data: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Enabled = true;
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
        private void btnCetakBarcode_Click(object sender, EventArgs e) { }
    }
}