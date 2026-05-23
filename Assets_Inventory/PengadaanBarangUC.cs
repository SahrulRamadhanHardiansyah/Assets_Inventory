using Assets_Inventory.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace Assets_Inventory
{
    public partial class PengadaanBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();

        public PengadaanBarangUC()
        {
            InitializeComponent();
        }

        private void PengadaanBarangUC_Load(object sender, EventArgs e)
        {
            cmbJumlah.Items.Clear();
            cmbJumlah.Items.Add("1");
            cmbJumlah.Items.Add("Semua");
            cmbJumlah.SelectedIndex = 0;

            cmbJenis.Items.Clear();
            cmbJenis.Items.Add("Code 128");
            cmbJenis.Items.Add("Code 39");
            cmbJenis.Items.Add("QR Code");
            cmbJenis.SelectedIndex = 0;

            loadData();
        }

        private void loadData()
        {
            var cari = txtCari.Text.ToLower().Trim();
            var data = db.Pengadaan
                         .Include(p => p.IdSumberPerolehanNavigation)
                         .Include(p => p.KodeGudangNavigation)
                         .Where(x => x.IdPengadaan.ToString().Contains(cari))
                         .ToList();

            dg.DataSource = data;
            lblTotal.Text = $"Total Record: {data.Count}";
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Pengadaan p)
            {
                var pengadaan = db.Pengadaan.Find(p.IdPengadaan);
                if (pengadaan != null)
                {
                    bindingSource1.DataSource = pengadaan;
                }
            }
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is Pengadaan p)
            {
                if (idSumberPerolehanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = p.IdSumberPerolehanNavigation?.NamaSumber;
                if (kodeGudangNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = p.KodeGudangNavigation?.NamaGudang;
                if (totalHargaDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = p.TotalHarga.HasValue ? p.TotalHarga.Value.ToString("C2") : "0";
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            loadData();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            InputPengadaanBarangForm form = new InputPengadaanBarangForm();
            if (DialogResult.OK == form.ShowDialog()) OnLoad(null);
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pengadaan p)
            {
                var pengadaan = db.Pengadaan.Find(p.IdPengadaan);
                InputPengadaanBarangForm form = new InputPengadaanBarangForm();
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
            if (bindingSource1.Current is Pengadaan k)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.IdPengadaan}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Pengadaan.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadData();
                        bindingSource1.AddNew();
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        db.Entry(k).Reload();
                        MessageBox.Show("Tidak dapat menghapus data ini karena data masih digunakan oleh data lain di dalam sistem.", "Peringatan Relasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        db.Entry(k).Reload();
                        MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ofd.Filter = "Semua File Data|*.csv;*.xlsx;*.xls|Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv";
                ofd.Title = "Pilih File Data Pengadaan";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(ofd.FileName).ToLower();
                        int berhasil = 0;

                        if (ext == ".csv")
                        {
                            berhasil = ImportCsv(ofd.FileName);
                        }
                        else if (ext == ".xlsx" || ext == ".xls")
                        {
                            berhasil = ImportExcel(ofd.FileName);
                        }
                        else
                        {
                            MessageBox.Show("Format file tidak didukung!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        loadData();
                        MessageBox.Show($"Import selesai! {berhasil} data baru berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("File sedang dibuka oleh program lain. Tutup file tersebut dan coba lagi.", "Error Akses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        int.TryParse(data[1].Trim(), out int idPemasok) &&
                        decimal.TryParse(data[2].Trim(), out decimal totalHarga))
                    {
                        var pengadaan = new Pengadaan
                        {
                            TanggalPengadaan = tgl,
                            TotalHarga = totalHarga,
                            KodeGudang = data[3].Trim(),
                            Status = data[4].Trim(),
                            Keterangan = data.Length > 5 ? data[5].Trim() : null
                        };

                        db.Pengadaan.Add(pengadaan);
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
                                int.TryParse(row[1]?.ToString().Trim(), out int idPemasok) &&
                                decimal.TryParse(row[2]?.ToString().Trim(), out decimal totalHarga))
                            {
                                var pengadaan = new Pengadaan
                                {
                                    TanggalPengadaan = tgl,
                                    TotalHarga = totalHarga,
                                    KodeGudang = row[3]?.ToString().Trim(),
                                    Status = row[4]?.ToString().Trim(),
                                    Keterangan = row.ItemArray.Length > 5 ? row[5]?.ToString().Trim() : null
                                };

                                db.Pengadaan.Add(pengadaan);
                                berhasil++;
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
            return berhasil;
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
                if (bindingSource1.Current is Pengadaan p)
                {
                    var asetList = db.Aset
                        .Where(a => a.IdDetailPengadaan != null)
                        .ToList()
                        .Where(a => db.DetailPengadaan
                            .Any(d => d.IdDetailPengadaan == a.IdDetailPengadaan && d.IdPengadaan == p.IdPengadaan))
                        .Select(a => a.KodeInventaris)
                        .ToList();

                    if (asetList.Count == 0)
                    {
                        MessageBox.Show("Pengadaan ini belum diproses menjadi aset (status belum 'dibelanjakan').\nBarcode hanya bisa dicetak untuk barang yang sudah memiliki kode inventaris.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    daftarKode.AddRange(asetList);
                }
                else
                {
                    MessageBox.Show("Silakan pilih satu baris data dari tabel terlebih dahulu.");
                    return;
                }
            }
            else
            {
                var pengadaanIds = dg.Rows.Cast<DataGridViewRow>()
                    .Where(r => r.DataBoundItem is Pengadaan)
                    .Select(r => ((Pengadaan)r.DataBoundItem).IdPengadaan)
                    .ToList();

                var asetList = db.Aset
                    .Where(a => a.IdDetailPengadaan != null)
                    .ToList()
                    .Where(a => db.DetailPengadaan
                        .Any(d => d.IdDetailPengadaan == a.IdDetailPengadaan && pengadaanIds.Contains(d.IdPengadaan)))
                    .Select(a => a.KodeInventaris)
                    .ToList();

                if (asetList.Count == 0)
                {
                    MessageBox.Show("Tidak ada aset dengan kode inventaris yang bisa dicetak.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                daftarKode.AddRange(asetList);
            }

            if (daftarKode.Count == 0) return;

            BarcodeFormat formatBarcode = BarcodeFormat.CODE_128;
            int lebar = 300, tinggi = 100;

            string jenisPilihan = cmbJenis.SelectedItem.ToString();
            if (jenisPilihan == "Code 39")
            {
                formatBarcode = BarcodeFormat.CODE_39;
            }
            else if (jenisPilihan == "QR Code")
            {
                formatBarcode = BarcodeFormat.QR_CODE;
                lebar = 200;
                tinggi = 200;
            }

            var writer = new BarcodeWriter
            {
                Format = formatBarcode,
                Options = new EncodingOptions
                {
                    Height = tinggi,
                    Width = lebar,
                    Margin = 1
                }
            };

            Form previewForm = new Form
            {
                Text = "Preview Cetak Barcode",
                Size = new Size(850, 600),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White
            };

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };
            previewForm.Controls.Add(panel);

            foreach (var kode in daftarKode)
            {
                try
                {
                    Panel pnlBox = new Panel
                    {
                        Width = lebar + 20,
                        Height = tinggi + 40,
                        Margin = new Padding(10)
                    };

                    PictureBox pb = new PictureBox
                    {
                        Image = writer.Write(kode),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Top,
                        Height = tinggi
                    };

                    Label lbl = new Label
                    {
                        Text = kode,
                        Dock = DockStyle.Bottom,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };

                    pnlBox.Controls.Add(pb);
                    pnlBox.Controls.Add(lbl);
                    panel.Controls.Add(pnlBox);
                }
                catch (Exception)
                {
                }
            }

            previewForm.ShowDialog();
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Pengadaan p)
            {
                if (DetailColumn.Index == e.RowIndex)
                {
                    DetailPengadaanForm form = new DetailPengadaanForm(p.IdPengadaan);
                    form.ShowDialog();
                }
            }
        }
    }
}
