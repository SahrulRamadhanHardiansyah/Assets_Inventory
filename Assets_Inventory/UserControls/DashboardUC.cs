using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Assets_Inventory.UserControls
{
    public partial class DashboardUC : UserControl
    {
        AppDbContext db = new AppDbContext();

        public DashboardUC()
        {
            InitializeComponent();
        }

        private void DashboardUC_Load(object sender, EventArgs e)
        {
            SetupGridDesign();
            LoadDashboardData();
        }

        public void LoadDashboardData()
        {
            try
            {
                lblTotalAset.Text = db.Aset.Count().ToString();

                lblPermintaanPending.Text = db.Permintaan
                    .Count(p => p.StatusPersetujuan == "Menunggu").ToString();

                lblPengadaanProses.Text = db.Pengadaan
                    .Count(p => p.Status == "diproses").ToString();

                lblAsetBelumLengkap.Text = db.Aset
                    .Count(a => a.NoSeri == null || a.IdRuang == null).ToString();

                LoadChartStatusData();
                LoadChartKondisiData();
                LoadNotifikasiGrid();
                LoadPermintaanPendingGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error memuat dashboard: " + ex.Message);
            }
        }

        private void LoadChartStatusData()
        {
            if (chartAset == null) return;

            var dataStatus = db.Aset
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Jumlah = g.Count() })
                .ToList();

            chartAset.Series.Clear();
            chartAset.Titles.Clear();
            chartAset.Titles.Add("Distribusi Status Aset");
            chartAset.Titles[0].Font = new Font("Arial", 12F, FontStyle.Bold);

            Series series = new Series("StatusAset");
            series.ChartType = SeriesChartType.Doughnut;
            series.IsValueShownAsLabel = true;

            foreach (var item in dataStatus)
            {
                string namaStatus = string.IsNullOrEmpty(item.Status) ? "Tidak Diketahui" : item.Status;
                series.Points.AddXY(namaStatus, item.Jumlah);
            }
            chartAset.Series.Add(series);
        }

        private void LoadChartKondisiData()
        {
            if (chartKondisi == null) return;

            var dataKondisi = db.Aset
                .Include(a => a.IdKondisiNavigation)
                .GroupBy(a => a.IdKondisiNavigation != null ? a.IdKondisiNavigation.NamaKondisi : "Tanpa Kondisi")
                .Select(g => new { Kondisi = g.Key, Jumlah = g.Count() })
                .ToList();

            chartKondisi.Series.Clear();
            chartKondisi.Titles.Clear();
            chartKondisi.Titles.Add("Kondisi Fisik Aset");
            chartKondisi.Titles[0].Font = new Font("Arial", 12F, FontStyle.Bold);

            Series series = new Series("KondisiAset");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            foreach (var item in dataKondisi)
            {
                var point = series.Points.AddXY(item.Kondisi, item.Jumlah);

                if (item.Kondisi.ToLower().Contains("baik"))
                    series.Points.Last().Color = Color.MediumSeaGreen;
                else if (item.Kondisi.ToLower().Contains("rusak"))
                    series.Points.Last().Color = Color.Tomato;
                else
                    series.Points.Last().Color = Color.Gold;
            }
            chartKondisi.Series.Add(series);
            chartKondisi.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        }

        private void LoadNotifikasiGrid()
        {
            if (dgNotifikasi == null) return;

            var asetPending = db.Aset.AsNoTracking()
                .Include(a => a.IdMasterBarangNavigation)
                .Where(a => a.NoSeri == null || a.IdRuang == null)
                .OrderByDescending(a => a.TanggalRegistrasi)
                .Take(50)
                .Select(a => new
                {
                    a.KodeInventaris,
                    NamaBarang = a.IdMasterBarangNavigation != null ? a.IdMasterBarangNavigation.NamaBarang : "Barang Tidak Diketahui",
                    a.TanggalRegistrasi,
                    Status = "Lengkapi Detail",
                    ObjekAsli = a
                }).ToList();

            dgNotifikasi.DataSource = asetPending;

            if (dgNotifikasi.Columns["KodeInventaris"] != null) dgNotifikasi.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
            if (dgNotifikasi.Columns["NamaBarang"] != null) dgNotifikasi.Columns["NamaBarang"].HeaderText = "Nama Barang";
            if (dgNotifikasi.Columns["TanggalRegistrasi"] != null) dgNotifikasi.Columns["TanggalRegistrasi"].HeaderText = "Tgl Masuk";
            if (dgNotifikasi.Columns["Status"] != null) dgNotifikasi.Columns["Status"].HeaderText = "Tindakan";
            if (dgNotifikasi.Columns["ObjekAsli"] != null) dgNotifikasi.Columns["ObjekAsli"].Visible = false;
        }

        private void LoadPermintaanPendingGrid()
        {
            if (dgPermintaanPending == null) return;

            var pendingReqRaw = db.Permintaan.AsNoTracking()
                .Where(p => p.StatusPersetujuan == "Menunggu")
                .OrderBy(p => p.TanggalPermintaan)
                .Take(30)
                .ToList();

            // Only load dictionaries for IDs actually needed (perf optimization)
            var jurusanIds = pendingReqRaw.Where(p => p.IdJurusan.HasValue).Select(p => p.IdJurusan.Value).Distinct().ToList();
            var penggunaIds = pendingReqRaw.Where(p => p.IdPengguna.HasValue).Select(p => p.IdPengguna.Value).Distinct().ToList();

            var dictJurusan = jurusanIds.Count > 0 ? db.Jurusan.Where(j => jurusanIds.Contains(j.IdJurusan)).ToDictionary(j => j.IdJurusan, j => j.NamaJurusan) : new System.Collections.Generic.Dictionary<int, string>();
            var dictPengguna = penggunaIds.Count > 0 ? db.Pengguna.Where(u => penggunaIds.Contains(u.IdPengguna)).ToDictionary(u => u.IdPengguna, u => u.Username) : new System.Collections.Generic.Dictionary<int, string>();

            var pendingReq = pendingReqRaw.Select(p => new
                {
                    p.KodePermintaan,
                    Peminta = (p.IdPengguna.HasValue && dictPengguna.ContainsKey(p.IdPengguna.Value)) ? dictPengguna[p.IdPengguna.Value] : "N/A",
                    Jurusan = (p.IdJurusan.HasValue && dictJurusan.ContainsKey(p.IdJurusan.Value)) ? dictJurusan[p.IdJurusan.Value] : "N/A",
                    Tanggal = p.TanggalPermintaan,
                    Keperluan = p.KeteranganKeperluan
                }).ToList();

            dgPermintaanPending.DataSource = pendingReq;

            if (dgPermintaanPending.Columns["KodePermintaan"] != null) dgPermintaanPending.Columns["KodePermintaan"].HeaderText = "Kode";
            if (dgPermintaanPending.Columns["Tanggal"] != null) dgPermintaanPending.Columns["Tanggal"].HeaderText = "Tgl Minta";
            if (dgPermintaanPending.Columns["Keperluan"] != null) dgPermintaanPending.Columns["Keperluan"].HeaderText = "Keperluan";
        }

        private void SetupGridDesign()
        {
            if (dgNotifikasi != null)
            {
                dgNotifikasi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgNotifikasi.ReadOnly = true;
                dgNotifikasi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgNotifikasi.AllowUserToAddRows = false;
                dgNotifikasi.RowHeadersVisible = false;
            }

            if (dgPermintaanPending != null)
            {
                dgPermintaanPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgPermintaanPending.ReadOnly = true;
                dgPermintaanPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgPermintaanPending.AllowUserToAddRows = false;
                dgPermintaanPending.RowHeadersVisible = false;
            }
        }

        private void dgNotifikasi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dynamic rowData = dgNotifikasi.Rows[e.RowIndex].DataBoundItem;
                Aset asetTerpilih = rowData.ObjekAsli;

                List<Aset> listLengkapi = new List<Aset> { asetTerpilih };
                KelengkapanAsetForm formLengkap = new KelengkapanAsetForm(listLengkapi);

                if (formLengkap.ShowDialog() == DialogResult.OK)
                {
                    LoadDashboardData();
                }
            }
        }

        private void BoxPermintaanTertunda_Click(object sender, EventArgs e)
        {
            PermintaanBarangUC uc = new PermintaanBarangUC();
            uc.DefaultStatusFilter = "Menunggu"; 
            MainForm parentForm = this.ParentForm as MainForm;
            if (parentForm != null) parentForm.ChangeView(uc);
        }

        private void BoxPengadaanDiproses_Click(object sender, EventArgs e)
        {
            PengadaanBarangUC uc = new PengadaanBarangUC();
            uc.DefaultStatusFilter = "Menunggu Proses"; 
            MainForm parentForm = this.ParentForm as MainForm;
            if (parentForm != null) parentForm.ChangeView(uc);
        }

        private void BoxAsetBelumLengkap_Click(object sender, EventArgs e)
        {
            AsetPerluDilengkapiUC uc = new AsetPerluDilengkapiUC();
            MainForm parentForm = this.ParentForm as MainForm;
            if (parentForm != null) parentForm.ChangeView(uc);
        }
    }
}