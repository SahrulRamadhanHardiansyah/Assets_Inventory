using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
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
            LoadDashboardDataAsync();
        }

        public void LoadDashboardData()
        {
            LoadDashboardDataAsync();
        }

        // ponytail: sync DB calls on the UI thread; fine for 4 counts + 2 small aggregates locally.
        // Add real async (EF Core migration) if the dashboard ever feels slow.
        public void LoadDashboardDataAsync()
        {
            try
            {
                lblTotalAset.Text = db.Aset.Count().ToString();
                lblPermintaanPending.Text = db.Permintaan.Count(p => p.StatusPersetujuan == "Menunggu").ToString();
                lblPengadaanProses.Text = db.Pengadaan.Count(p => p.Status == "diproses").ToString();
                lblAsetBelumLengkap.Text = db.Aset.Count(a => a.NoSeri == null || a.IdRuang == null).ToString();

                LoadChartStatusData();
                LoadChartKondisiData();
                LoadNotifikasiGrid();
                LoadPermintaanPendingGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat dashboard: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadChartStatusData()
        {
            if (chartAset == null) return;

            var dataStatus = db.Aset
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Jumlah = g.Count() })
                .ToList();

            Action ui = () =>
            {
                chartAset.Series.Clear();
                chartAset.Titles.Clear();
                chartAset.Titles.Add("Distribusi Status Aset");
                chartAset.Titles[0].Font = new Font("Arial", 12F, FontStyle.Bold);

                Series series = new Series("StatusAset")
                {
                    ChartArea = "ChartArea1",
                    Legend = "Legend1",
                    ChartType = SeriesChartType.Doughnut,
                    IsValueShownAsLabel = true
                };

                if (dataStatus.Count == 0)
                {
                    // ponytail: placeholder for empty data; swap for a real empty-state design when requested
                    chartAset.Titles.Add("Belum ada data aset");
                    chartAset.Titles[1].Font = new Font("Arial", 9F, FontStyle.Italic);
                    chartAset.Titles[1].ForeColor = Color.Gray;
                    series.Points.AddXY("", 1);
                    series.Points[0].Color = Color.Gainsboro;
                    series.Points[0].IsValueShownAsLabel = false;
                }

                foreach (var item in dataStatus)
                {
                    string namaStatus = string.IsNullOrEmpty(item.Status) ? "Tidak Diketahui" : item.Status;
                    series.Points.AddXY(namaStatus, item.Jumlah);
                }
                chartAset.Series.Add(series);
            };
            if (InvokeRequired) Invoke(ui); else ui();
        }

        private void LoadChartKondisiData()
        {
            if (chartKondisi == null) return;

            var dataKondisi = db.Aset
                .Include(a => a.IdKondisiNavigation)
                .GroupBy(a => a.IdKondisiNavigation != null ? a.IdKondisiNavigation.NamaKondisi : "Tanpa Kondisi")
                .Select(g => new { Kondisi = g.Key, Jumlah = g.Count() })
                .ToList();

            Action ui = () =>
            {
                chartKondisi.Series.Clear();
                chartKondisi.Titles.Clear();
                chartKondisi.Titles.Add("Kondisi Fisik Aset");
                chartKondisi.Titles[0].Font = new Font("Arial", 12F, FontStyle.Bold);

                Series series = new Series("KondisiAset")
                {
                    ChartArea = "ChartArea1",
                    Legend = "Legend1",
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true
                };

                if (dataKondisi.Count == 0)
                {
                    // ponytail: placeholder for empty data; swap for a real empty-state design when requested
                    chartKondisi.Titles.Add("Belum ada data kondisi");
                    chartKondisi.Titles[1].Font = new Font("Arial", 9F, FontStyle.Italic);
                    chartKondisi.Titles[1].ForeColor = Color.Gray;
                }
                else
                {
                    chartKondisi.Titles.Add("");
                }

                foreach (var item in dataKondisi)
                {
                    series.Points.AddXY(item.Kondisi, item.Jumlah);
                    if (item.Kondisi.ToLower().Contains("baik"))
                        series.Points.Last().Color = Color.MediumSeaGreen;
                    else if (item.Kondisi.ToLower().Contains("rusak"))
                        series.Points.Last().Color = Color.Tomato;
                    else
                        series.Points.Last().Color = Color.Gold;
                }
                chartKondisi.Series.Add(series);
                chartKondisi.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            };
            if (InvokeRequired) Invoke(ui); else ui();
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

            Action ui = () =>
            {
                dgNotifikasi.DataSource = asetPending;
                if (dgNotifikasi.Columns["KodeInventaris"] != null) dgNotifikasi.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
                if (dgNotifikasi.Columns["NamaBarang"] != null) dgNotifikasi.Columns["NamaBarang"].HeaderText = "Nama Barang";
                if (dgNotifikasi.Columns["TanggalRegistrasi"] != null) dgNotifikasi.Columns["TanggalRegistrasi"].HeaderText = "Tgl Masuk";
                if (dgNotifikasi.Columns["Status"] != null) dgNotifikasi.Columns["Status"].HeaderText = "Tindakan";
                if (dgNotifikasi.Columns["ObjekAsli"] != null) dgNotifikasi.Columns["ObjekAsli"].Visible = false;
            };
            if (InvokeRequired) Invoke(ui); else ui();
        }

        private void LoadPermintaanPendingGrid()
        {
            if (dgPermintaanPending == null) return;

            var pendingReqRaw = db.Permintaan.AsNoTracking()
                .Where(p => p.StatusPersetujuan == "Menunggu")
                .OrderBy(p => p.TanggalPermintaan)
                .Take(30)
                .ToList();

            var ids = pendingReqRaw.Select(p => p.IdJurusan).Where(x => x.HasValue).Select(x => x.Value).Distinct().ToList();
            var dictJurusan = ids.Count > 0
                ? db.Jurusan.Where(j => ids.Contains(j.IdJurusan)).ToList().ToDictionary(j => j.IdJurusan, j => j.NamaJurusan)
                : new Dictionary<int, string>();

            var idsPengguna = pendingReqRaw.Select(p => p.IdPengguna).Where(x => x.HasValue).Select(x => x.Value).Distinct().ToList();
            var dictPengguna = idsPengguna.Count > 0
                ? db.Pengguna.Where(u => idsPengguna.Contains(u.IdPengguna)).ToList().ToDictionary(u => u.IdPengguna, u => u.Username)
                : new Dictionary<int, string>();

            var pendingReq = pendingReqRaw.Select(p => new
            {
                p.KodePermintaan,
                Peminta = (p.IdPengguna.HasValue && dictPengguna.ContainsKey(p.IdPengguna.Value)) ? dictPengguna[p.IdPengguna.Value] : "N/A",
                Jurusan = (p.IdJurusan.HasValue && dictJurusan.ContainsKey(p.IdJurusan.Value)) ? dictJurusan[p.IdJurusan.Value] : "N/A",
                Tanggal = p.TanggalPermintaan,
                Keperluan = p.KeteranganKeperluan
            }).ToList();

            Action ui = () =>
            {
                dgPermintaanPending.DataSource = pendingReq;
                if (dgPermintaanPending.Columns["KodePermintaan"] != null) dgPermintaanPending.Columns["KodePermintaan"].HeaderText = "Kode";
                if (dgPermintaanPending.Columns["Tanggal"] != null) dgPermintaanPending.Columns["Tanggal"].HeaderText = "Tgl Minta";
                if (dgPermintaanPending.Columns["Keperluan"] != null) dgPermintaanPending.Columns["Keperluan"].HeaderText = "Keperluan";
            };
            if (InvokeRequired) Invoke(ui); else ui();
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
                    LoadDashboardDataAsync();
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
