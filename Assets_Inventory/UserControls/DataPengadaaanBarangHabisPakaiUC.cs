using Assets_Inventory.Forms;
using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace Assets_Inventory.UserControls
{
    public partial class DataPengadaaanBarangHabisPakaiUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        public string DefaultStatusFilter = "Semua";

        public class PengadaanHpViewModel
        {
            public int IdPengadaan { get; set; }
            public DateTime TanggalPengadaan { get; set; }
            public string TahunAjaran { get; set; }
            public string NamaSumber { get; set; }
            public string NamaGudang { get; set; }
            public decimal TotalHarga { get; set; }
            public string Status { get; set; }
            public PengadaanHabisPakai ObjekAsli { get; set; }
        }

        public DataPengadaaanBarangHabisPakaiUC()
        {
            InitializeComponent();
        }

        private void DataPengadaaanBarangHabisPakaiUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Pengadaan Brg Habis Pakai");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainForm parentForm = this.ParentForm as MainForm;
                DashboardUC dashboardUC = new DashboardUC();
                if (parentForm != null) parentForm.ChangeView(dashboardUC);
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnUbah.Enabled = hakAkses.HakUbah;
            btnHapus.Enabled = hakAkses.HakHapus;
            btnImport.Enabled = hakAkses.HakBuat;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Semua", "Menunggu Proses", "Dibelanjakan", "Selesai" });
            cmbStatus.SelectedItem = DefaultStatusFilter;

            loadData();
        }

        private void loadData()
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cari = txtCari.Text.ToLower().Trim();
            string statusFilter = cmbStatus.SelectedItem?.ToString() ?? "Semua";

            var query = db.PengadaanHabisPakai.AsNoTracking()
                                              .Where(x => x.IdPengadaan.ToString().Contains(cari));

            if (statusFilter != "Semua")
            {
                query = query.Where(x => x.Status == statusFilter);
            }

            var listPengadaan = query.OrderByDescending(p => p.IdPengadaan).ToList();

            var dictSumber = db.SumberPerolehan.ToDictionary(s => s.IdSumberPerolehan, s => s.NamaSumber);
            var dictGudang = db.Gudang.ToDictionary(g => g.KodeGudang, g => g.NamaGudang);
            var dictTA = db.TahunAjaran.ToDictionary(ta => ta.IdTahunAjaran, ta => $"{ta.TahunAjaran1} - Smt {ta.Semester}");

            var data = listPengadaan.Select(p => new PengadaanHpViewModel
            {
                IdPengadaan = p.IdPengadaan,
                TanggalPengadaan = p.TanggalPengadaan,
                TahunAjaran = (p.IdTahunAjaran.HasValue && dictTA.ContainsKey(p.IdTahunAjaran.Value)) ? dictTA[p.IdTahunAjaran.Value] : "-",
                NamaSumber = (p.IdSumberPerolehan.HasValue && dictSumber.ContainsKey(p.IdSumberPerolehan.Value)) ? dictSumber[p.IdSumberPerolehan.Value] : "N/A",
                NamaGudang = (!string.IsNullOrEmpty(p.KodeGudang) && dictGudang.ContainsKey(p.KodeGudang)) ? dictGudang[p.KodeGudang] : "N/A",
                TotalHarga = p.TotalHarga ?? 0,
                Status = p.Status ?? "Menunggu Proses",
                ObjekAsli = p
            }).ToList();

            dg.DataSource = new SortableBindingList<PengadaanHpViewModel>(data);
            lblTotal.Text = $"Total Record: {data.Count}";

            if (dg.Columns["IdPengadaan"] != null) dg.Columns["IdPengadaan"].HeaderText = "ID Pengadaan";
            if (dg.Columns["TanggalPengadaan"] != null) dg.Columns["TanggalPengadaan"].HeaderText = "Tanggal";
            if (dg.Columns["TahunAjaran"] != null) dg.Columns["TahunAjaran"].HeaderText = "Tahun Ajaran";
            if (dg.Columns["NamaSumber"] != null) dg.Columns["NamaSumber"].HeaderText = "Sumber Perolehan";
            if (dg.Columns["NamaGudang"] != null) dg.Columns["NamaGudang"].HeaderText = "Gudang Tujuan";
            if (dg.Columns["Status"] != null) dg.Columns["Status"].HeaderText = "Status";
            if (dg.Columns["TotalHarga"] != null)
            {
                dg.Columns["TotalHarga"].HeaderText = "Total Harga";
                dg.Columns["TotalHarga"].DefaultCellStyle.Format = "C2";
                dg.Columns["TotalHarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dg.Columns["DetailColumn"] != null) dg.Columns["DetailColumn"].DisplayIndex = dg.Columns.Count - 1;
            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            cmbStatus.SelectedIndex = 0;
            loadData();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is PengadaanHpViewModel vm)
            {
                PengadaanHabisPakai p = vm.ObjekAsli;
                var pengadaan = db.PengadaanHabisPakai.Find(p.IdPengadaan);
                if (pengadaan != null)
                {
                    bindingSource1.DataSource = pengadaan;
                }
            }
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is PengadaanHpViewModel vm)
            {
                var p = vm.ObjekAsli;
                if (dg.Columns["DetailColumn"] != null && dg.Columns["DetailColumn"].Index == e.ColumnIndex)
                {
                    InputPengadaanBarangHabisPakaiForm form = new InputPengadaanBarangHabisPakaiForm();
                    form.selectedPengadaan = db.PengadaanHabisPakai.Find(p.IdPengadaan);
                    form.isDetailMode = true;
                    form.ShowDialog();
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            InputPengadaanBarangHabisPakaiForm form = new InputPengadaanBarangHabisPakaiForm();
            if (DialogResult.OK == form.ShowDialog()) loadData();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PengadaanHabisPakai p)
            {
                if (p.Status == "Selesai" || p.Status == "Dibelanjakan")
                {
                    MessageBox.Show("Pengadaan yang sudah dibelanjakan atau selesai tidak dapat diubah lagi. Anda hanya bisa melihat detailnya.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pengadaan = db.PengadaanHabisPakai.Find(p.IdPengadaan);
                InputPengadaanBarangHabisPakaiForm form = new InputPengadaanBarangHabisPakaiForm();
                form.selectedPengadaan = pengadaan;
                if (form.ShowDialog() == DialogResult.OK) loadData();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PengadaanHabisPakai k)
            {
                if (k.Status != "Menunggu Proses")
                {
                    MessageBox.Show("Hanya pengadaan dengan status 'Menunggu Proses' yang dapat dihapus. Jika barang sudah masuk gudang, Anda harus membatalkannya melalui menu lain.", "Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data Pengadaan ID: {k.IdPengadaan}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var toDelete = db.PengadaanHabisPakai.Find(k.IdPengadaan);
                        db.PengadaanHabisPakai.Remove(toDelete);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadData();
                        bindingSource1.AddNew();
                    }
                    catch (Exception ex)
                    {
                        db.Entry(k).Reload();
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem saat menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang valid untuk dihapus.");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Semua File Data|*.csv;*.xlsx;*.xls";
                ofd.Title = "Pilih File Data Pengadaan Habis Pakai";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(ofd.FileName).ToLower();
                        int berhasil = 0;

                        if (ext == ".csv") berhasil = ImportCsv(ofd.FileName);
                        else if (ext == ".xlsx" || ext == ".xls") berhasil = ImportExcel(ofd.FileName);
                        else
                        {
                            MessageBox.Show("Format file tidak didukung!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        loadData();
                        MessageBox.Show($"Import selesai! {berhasil} data baru berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private int ImportCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            int berhasil = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var data = lines[i].Split(',');

                if (data.Length >= 5)
                {
                    if (DateTime.TryParse(data[0].Trim(), out DateTime tgl) &&
                        decimal.TryParse(data[2].Trim(), out decimal totalHarga))
                    {
                        var pengadaan = new PengadaanHabisPakai
                        {
                            TanggalPengadaan = tgl,
                            TotalHarga = totalHarga,
                            KodeGudang = data[3].Trim(),
                            Status = data[4].Trim(),
                            Keterangan = data.Length > 5 ? data[5].Trim() : null
                        };

                        db.PengadaanHabisPakai.Add(pengadaan);
                        berhasil++;
                    }
                }
            }
            db.SaveChanges();
            return berhasil;
        }

        private int ImportExcel(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            int berhasil = 0;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    DataTable table = result.Tables[0];

                    for (int i = 1; i < table.Rows.Count; i++)
                    {
                        var row = table.Rows[i];

                        if (row.ItemArray.Length >= 5 && !string.IsNullOrWhiteSpace(row[0]?.ToString()))
                        {
                            if (DateTime.TryParse(row[0]?.ToString().Trim(), out DateTime tgl) &&
                                decimal.TryParse(row[2]?.ToString().Trim(), out decimal totalHarga))
                            {
                                var pengadaan = new PengadaanHabisPakai
                                {
                                    TanggalPengadaan = tgl,
                                    TotalHarga = totalHarga,
                                    KodeGudang = row[3]?.ToString().Trim(),
                                    Status = row[4]?.ToString().Trim(),
                                    Keterangan = row.ItemArray.Length > 5 ? row[5]?.ToString().Trim() : null
                                };

                                db.PengadaanHabisPakai.Add(pengadaan);
                                berhasil++;
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
            return berhasil;
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PengadaanHabisPakai p)
            {
                if (p.Status != "Menunggu Proses")
                {
                    MessageBox.Show("Pengadaan ini sudah diproses atau selesai.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show(
                    "Proses belanja akan:\n" +
                    "1. Memasukkan stok barang ke tabel Aset Habis Pakai (Status: Tersedia)\n" +
                    "2. Mengubah status pengadaan menjadi 'Dibelanjakan'\n\n" +
                    "Apakah Anda yakin ingin melanjutkan eksekusi ini?",
                    "Konfirmasi Proses Belanja Habis Pakai",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    JalankanProsesBelanja(p.IdPengadaan);
                }
            }
            else
            {
                MessageBox.Show("Pilih data pengadaan yang ingin diproses terlebih dahulu.");
            }
        }

        private void JalankanProsesBelanja(int idPengadaanTarget)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var detailList = db.DetailPengadaanHp
                    .Include(d => d.IdMasterBarangNavigation)
                    .Where(d => d.IdPengadaanHp == idPengadaanTarget)
                    .ToList();

                if (detailList.Count == 0)
                {
                    MessageBox.Show("Tidak ada detail barang untuk diproses pada pengadaan ini.");
                    return;
                }

                string tahun = DateTime.Now.Year.ToString();
                int counter = 1;

                foreach (var detail in detailList)
                {
                    string kodeBarang = $"KB-HP-{tahun}-{idPengadaanTarget:D3}-{counter:D3}";

                    var asetHp = new AsetHabisPakai
                    {
                        KodeBarang = kodeBarang, 
                        IdMasterBarang = detail.IdMasterBarang,
                        Stok = detail.JumlahMasuk,
                        StokAktual = detail.JumlahMasuk, 
                        IdPengadaanHp = detail.IdPengadaanHp,
                        Status = "Tersedia",
                        TanggalRegistrasi = DateTime.Now,
                        IsReturnable = false 
                    };

                    db.AsetHabisPakai.Add(asetHp);
                    counter++;
                }

                var targetPengadaan = db.PengadaanHabisPakai.Find(idPengadaanTarget);
                targetPengadaan.Status = "Dibelanjakan";

                db.SaveChanges();
                this.Cursor = Cursors.Default;

                MessageBox.Show($"Proses belanja selesai! Stok barang telah dimasukkan ke gudang Habis Pakai.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadData();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Terjadi kesalahan fatal saat proses belanja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}