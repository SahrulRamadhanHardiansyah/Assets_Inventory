using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.Forms;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ExcelDataReader;

namespace Assets_Inventory.UserControls
{
    public partial class DataPermintaanBarangHabisPakaiUC : UserControl
    {
        AppDbContext db = new AppDbContext();
        public string DefaultStatusFilter = "Semua";

        public class PermintaanHpViewModel
        {
            public string KodePermintaanHp { get; set; }
            public string TahunAjaran { get; set; }
            public string NamaJurusan { get; set; }
            public string NamaPeminta { get; set; }
            public string NamaPenyetuju { get; set; }
            public string KeteranganKeperluan { get; set; }
            public string StatusPersetujuan { get; set; }
            public DateTime TanggalPermintaan { get; set; }
            public PermintaanHp ObjekAsli { get; set; }
        }

        public DataPermintaanBarangHabisPakaiUC()
        {
            InitializeComponent();
        }

        private void DataPermintaanBarangHabisPakaiUC_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Permintaan Brg Habis Pakai");

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
            btnHapus.Enabled = hakAkses.HakHapus;

            cmbJumlah.Items.Clear();
            cmbJumlah.Items.Add("1");
            cmbJumlah.Items.Add("Semua");
            cmbJumlah.SelectedIndex = 0;

            cmbJenis.Items.Clear();
            cmbJenis.Items.Add("Code 128");
            cmbJenis.Items.Add("Code 39");
            cmbJenis.Items.Add("QR Code");
            cmbJenis.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Semua", "Menunggu", "Disetujui", "Ditolak" });
            cmbStatus.SelectedItem = DefaultStatusFilter;

            loadData();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            if (db != null) db.Dispose();
            db = new AppDbContext();

            var cari = txtCari.Text.ToLower().Trim();
            string statusFilter = cmbStatus.SelectedItem?.ToString() ?? "Semua";

            var query = db.PermintaanHp.AsNoTracking()
                                       .Where(x => x.KodePermintaanHp.ToLower().Contains(cari));

            if (statusFilter != "Semua")
            {
                query = query.Where(x => x.StatusPersetujuan == statusFilter);
            }

            var listPermintaan = query.ToList();

            var dictJurusan = db.Jurusan.ToDictionary(j => j.IdJurusan, j => j.NamaJurusan);
            var dictPengguna = db.Pengguna.ToDictionary(p => p.IdPengguna, p => p.Username);
            var dictTahunAjaran = db.TahunAjaran.ToDictionary(ta => ta.IdTahunAjaran, ta => $"{ta.TahunAjaran1} - Smt {ta.Semester}");

            var data = listPermintaan.Select(p => new PermintaanHpViewModel
            {
                KodePermintaanHp = p.KodePermintaanHp,
                TahunAjaran = (p.IdTahunAjaran.HasValue && dictTahunAjaran.ContainsKey(p.IdTahunAjaran.Value)) ? dictTahunAjaran[p.IdTahunAjaran.Value] : "-",
                NamaJurusan = (p.IdJurusan.HasValue && dictJurusan.ContainsKey(p.IdJurusan.Value)) ? dictJurusan[p.IdJurusan.Value] : "N/A",
                NamaPeminta = (p.IdPengguna.HasValue && dictPengguna.ContainsKey(p.IdPengguna.Value)) ? dictPengguna[p.IdPengguna.Value] : "N/A",
                NamaPenyetuju = (p.IdPenyetuju.HasValue && dictPengguna.ContainsKey(p.IdPenyetuju.Value)) ? dictPengguna[p.IdPenyetuju.Value] : "-",
                KeteranganKeperluan = p.KeteranganKeperluan,
                StatusPersetujuan = p.StatusPersetujuan,
                TanggalPermintaan = p.TanggalPermintaan,
                ObjekAsli = p
            })
            .OrderByDescending(p => p.TanggalPermintaan)
            .ToList();

            dg.DataSource = new SortableBindingList<PermintaanHpViewModel>(data);
            lblTotal.Text = $"Total Record: {data.Count}";

            if (dg.Columns["KodePermintaanHp"] != null) dg.Columns["KodePermintaanHp"].HeaderText = "Kode Permintaan";
            if (dg.Columns["TahunAjaran"] != null) dg.Columns["TahunAjaran"].HeaderText = "Tahun Ajaran";
            if (dg.Columns["NamaJurusan"] != null) dg.Columns["NamaJurusan"].HeaderText = "Nama Jurusan";
            if (dg.Columns["NamaPeminta"] != null) dg.Columns["NamaPeminta"].HeaderText = "Nama Peminta";
            if (dg.Columns["NamaPenyetuju"] != null) dg.Columns["NamaPenyetuju"].HeaderText = "Nama Penyetuju";
            if (dg.Columns["KeteranganKeperluan"] != null) dg.Columns["KeteranganKeperluan"].HeaderText = "Keterangan";
            if (dg.Columns["StatusPersetujuan"] != null) dg.Columns["StatusPersetujuan"].HeaderText = "Status";

            if (dg.Columns["TanggalPermintaan"] != null)
            {
                dg.Columns["TanggalPermintaan"].HeaderText = "Tanggal";
                dg.Columns["TanggalPermintaan"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
            }

            if (dg.Columns["DetailColumn"] != null) dg.Columns["DetailColumn"].DisplayIndex = dg.Columns.Count - 1;
            if (dg.Columns["SetujuColumn"] != null) dg.Columns["SetujuColumn"].DisplayIndex = dg.Columns.Count - 1;
            if (dg.Columns["ObjekAsli"] != null) dg.Columns["ObjekAsli"].Visible = false;
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

        private void btnTambah_Click(object sender, EventArgs e)
        {
            InputPermintaanBarangHabisPakaiForm form = new InputPermintaanBarangHabisPakaiForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PermintaanHpViewModel vm)
            {
                PermintaanHp k = vm.ObjekAsli;

                if (k.StatusPersetujuan == "Ditolak")
                {
                    MessageBox.Show("Permintaan ini sudah dalam status ditolak.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show($"Apakah anda yakin ingin MENOLAK transaksi permintaan {k.KodePermintaanHp}?\n\nStatus akan langsung diubah menjadi 'Ditolak'.", "Konfirmasi Penolakan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var toReject = db.PermintaanHp.Find(k.KodePermintaanHp);
                    if (toReject != null)
                    {
                        try
                        {
                            toReject.StatusPersetujuan = "Ditolak";
                            toReject.TanggalPersetujuan = DateTime.Now;
                            toReject.IdPenyetuju = Properties.Settings.Default.userId > 0 ? Properties.Settings.Default.userId : 1;

                            db.SaveChanges();

                            MessageBox.Show("Transaksi permintaan berhasil ditolak!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadData();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem saat menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang valid dari tabel terlebih dahulu.");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel & CSV Files|*.xlsx;*.xls;*.csv|Semua File (*.*)|*.*";
                ofd.Title = "Pilih File Data Permintaan Habis Pakai";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int berhasil = 0;
                        string ekstensi = Path.GetExtension(ofd.FileName).ToLower();

                        if (ekstensi == ".csv")
                        {
                            var lines = File.ReadAllLines(ofd.FileName);
                            for (int i = 1; i < lines.Length; i++)
                            {
                                Application.DoEvents();
                                var data = lines[i].Split(',');
                                if (data.Length >= 3)
                                {
                                    string kode = data[0].Trim();
                                    if (!string.IsNullOrEmpty(kode) && !db.PermintaanHp.Any(p => p.KodePermintaanHp == kode))
                                    {
                                        db.PermintaanHp.Add(new PermintaanHp
                                        {
                                            KodePermintaanHp = kode,
                                            IdJurusan = int.TryParse(data[1], out int idJ) ? idJ : 1,
                                            KeteranganKeperluan = data[2].Trim(),
                                            StatusPersetujuan = "Menunggu",
                                            TanggalPermintaan = DateTime.Now
                                        });
                                        berhasil++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                            {
                                using (var reader = ExcelReaderFactory.CreateReader(stream))
                                {
                                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                    {
                                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                    });

                                    DataTable dt = result.Tables[0];

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        Application.DoEvents();

                                        string kode = row[0]?.ToString().Trim();
                                        string jurusanStr = row[1]?.ToString().Trim();
                                        string keterangan = row[2]?.ToString().Trim();

                                        if (!string.IsNullOrEmpty(kode) && !db.PermintaanHp.Any(p => p.KodePermintaanHp == kode))
                                        {
                                            db.PermintaanHp.Add(new PermintaanHp
                                            {
                                                KodePermintaanHp = kode,
                                                IdJurusan = int.TryParse(jurusanStr, out int idJ) ? idJ : 1,
                                                KeteranganKeperluan = keterangan,
                                                StatusPersetujuan = "Menunggu",
                                                TanggalPermintaan = DateTime.Now
                                            });
                                            berhasil++;
                                        }
                                    }
                                }
                            }
                        }

                        db.SaveChanges();
                        MessageBox.Show($"Import selesai! {berhasil} data permintaan baru berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka atau digunakan oleh program lain (seperti Microsoft Excel). Harap tutup file tersebut dan coba lagi.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is PermintaanHpViewModel vm)
            {
                PermintaanHp p = vm.ObjekAsli;

                if (dg.Columns["SetujuColumn"] != null && dg.Columns["SetujuColumn"].Index == e.ColumnIndex && p.StatusPersetujuan != "Disetujui" && p.StatusPersetujuan != "Ditolak")
                {
                    var permintaan = db.PermintaanHp.Find(p.KodePermintaanHp);

                    if (permintaan == null)
                    {
                        MessageBox.Show("Data permintaan tidak ditemukan di database. Coba refresh tabel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (PersetujuanForm form = new PersetujuanForm())
                    {
                        form.isHabisPakai = true; 
                        form.selectedPermintaanHp = permintaan;

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            loadData();
                        }
                    }
                }

                if (dg.Columns["DetailColumn"] != null && dg.Columns["DetailColumn"].Index == e.ColumnIndex)
                {
                    InputPermintaanBarangHabisPakaiForm form = new InputPermintaanBarangHabisPakaiForm();
                    form.selectedPermintaan = db.PermintaanHp.Find(p.KodePermintaanHp);
                    form.isDetailMode = true;
                    form.ShowDialog();
                }
            }
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dg.Rows[e.RowIndex].DataBoundItem == null) return;

            if (dg.Rows[e.RowIndex].DataBoundItem is PermintaanHpViewModel p)
            {
                if (dg.Columns[e.ColumnIndex].Name == "SetujuColumn")
                {
                    if (p.StatusPersetujuan == "Disetujui")
                    {
                        e.Value = "Disetujui";
                        e.CellStyle.BackColor = Color.LightGreen;
                    }
                    else if (p.StatusPersetujuan == "Ditolak")
                    {
                        e.Value = "Ditolak";
                        e.CellStyle.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        e.Value = "Setujui";
                    }
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (cmbJenis.SelectedIndex == -1 || cmbJumlah.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih Jenis Barcode dan Jumlah terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> daftarKode = new List<string>();

            if (cmbJumlah.SelectedItem.ToString() == "1")
            {
                if (dg.CurrentRow != null && dg.CurrentRow.DataBoundItem is PermintaanHpViewModel vm)
                {
                    if (!string.IsNullOrEmpty(vm.KodePermintaanHp))
                    {
                        daftarKode.Add(vm.KodePermintaanHp);
                    }
                }
                else
                {
                    MessageBox.Show("Silakan pilih satu baris data dari tabel terlebih dahulu.");
                    return;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if (row.DataBoundItem is PermintaanHpViewModel vm && !string.IsNullOrEmpty(vm.KodePermintaanHp))
                    {
                        daftarKode.Add(vm.KodePermintaanHp);
                    }
                }
            }

            if (daftarKode.Count == 0)
            {
                MessageBox.Show("Tidak ada kode valid yang bisa dicetak.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                BarcodeFormat formatBarcode = BarcodeFormat.CODE_128;
                int lebar = 300, tinggi = 100;

                string jenisPilihan = cmbJenis.SelectedItem.ToString();
                if (jenisPilihan == "Code 39") formatBarcode = BarcodeFormat.CODE_39;
                else if (jenisPilihan == "QR Code") { formatBarcode = BarcodeFormat.QR_CODE; lebar = 200; tinggi = 200; }

                var writer = new BarcodeWriter
                {
                    Format = formatBarcode,
                    Options = new EncodingOptions { Height = tinggi, Width = lebar, Margin = 1 }
                };

                PrintDocument pd = new PrintDocument();
                int currentItemIndex = 0;

                pd.BeginPrint += (s, ev) => { currentItemIndex = 0; };
                pd.PrintPage += (s, ev) =>
                {
                    float x = ev.MarginBounds.Left, y = ev.MarginBounds.Top;
                    float itemWidth = lebar + 20, itemHeight = tinggi + 40;

                    while (currentItemIndex < daftarKode.Count)
                    {
                        string kode = daftarKode[currentItemIndex];
                        Image img = writer.Write(kode);

                        ev.Graphics.DrawImage(img, x, y, lebar, tinggi);

                        Font fontTeks = new Font("Arial", 10, FontStyle.Bold);
                        SizeF textSize = ev.Graphics.MeasureString(kode, fontTeks);
                        float textX = x + ((lebar - textSize.Width) / 2);
                        ev.Graphics.DrawString(kode, fontTeks, Brushes.Black, textX, y + tinggi + 5);

                        currentItemIndex++;
                        x += itemWidth;

                        if (x + itemWidth > ev.MarginBounds.Right) { x = ev.MarginBounds.Left; y += itemHeight; }
                        if (y + itemHeight > ev.MarginBounds.Bottom && currentItemIndex < daftarKode.Count)
                        {
                            ev.HasMorePages = true; return;
                        }
                    }
                    ev.HasMorePages = false;
                };

                PrintPreviewDialog preview = new PrintPreviewDialog { Document = pd, Width = 850, Height = 600, ShowIcon = false };
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem saat memproses cetak:\n\n" + ex.Message, "Error Cetak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}