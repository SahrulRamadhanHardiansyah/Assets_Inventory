using Assets_Inventory.Models;
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
    public partial class PermintaanBarangUC : UserControl
    {
        AppDbContext db = new AppDbContext();

        public PermintaanBarangUC()
        {
            InitializeComponent();
        }

        private void PermintaanBarangUC_Load(object sender, EventArgs e)
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
            var data = db.Permintaan
                         .Include(p => p.IdJurusanNavigation)
                         .Include(p => p.IdPenggunaNavigation)
                         .Include(p => p.IdPenyetujuNavigation)
                         .Where(x => x.KodePermintaan.ToLower().Contains(cari))
                         .ToList();

            dg.DataSource = new SortableBindingList<Permintaan>(data);
            lblTotal.Text = $"Total Record: {data.Count}";
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
            bindingSource1.AddNew();
            InputPermintaanBarangForm form = new InputPermintaanBarangForm();
            if (form.ShowDialog() == DialogResult.OK) loadData();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Permintaan p)
            {
                var permintaan = db.Permintaan.Find(p.KodePermintaan);
                InputPermintaanBarangForm form = new InputPermintaanBarangForm();
                form.selectedPermintaan = permintaan;
                if (form.ShowDialog() == DialogResult.OK) loadData();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Permintaan k)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.KodePermintaan}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Permintaan.Remove(k);
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
                ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                ofd.Title = "Pilih File Data Permintaan (CSV)";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = File.ReadAllLines(ofd.FileName);
                        int berhasil = 0;

                        for (int i = 1; i < lines.Length; i++)
                        {
                            var data = lines[i].Split(',');

                            if (data.Length >= 3)
                            {
                                string kode = data[0].Trim();
                                if (!db.Permintaan.Any(p => p.KodePermintaan == kode))
                                {
                                    db.Permintaan.Add(new Permintaan
                                    {
                                        KodePermintaan = kode,
                                        IdJurusan = int.TryParse(data[1], out int idJ) ? idJ : 1,
                                        KeteranganKeperluan = data[2].Trim(),
                                        StatusPersetujuan = "Menunggu",
                                        TanggalPermintaan = DateTime.Now
                                    });
                                    berhasil++;
                                }
                            }
                        }

                        db.SaveChanges();
                        MessageBox.Show($"Import selesai! {berhasil} data baru berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengimpor file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Permintaan p)
            {
                var permintaan = db.Permintaan.Find(p.KodePermintaan);
                if (permintaan != null)
                {
                    bindingSource1.DataSource = permintaan;
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
                if (bindingSource1.Current is Permintaan p)
                {
                    daftarKode.Add(p.KodePermintaan);
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
                    if (row.DataBoundItem is Permintaan p)
                    {
                        daftarKode.Add(p.KodePermintaan);
                    }
                }
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
                lebar = 200; tinggi = 200; 
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
                    Panel pnlBox = new Panel { Width = lebar + 20, Height = tinggi + 40, Margin = new Padding(10) };

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

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is Permintaan p)
            {
                if (idJurusanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = p.IdJurusanNavigation?.NamaJurusan;
                }
                if (idPenggunaNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = p.IdPenggunaNavigation?.Username;
                }
                if (idPenyetujuNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                {
                    e.Value = p.IdPenyetujuNavigation?.Username;
                }

                if (p.StatusPersetujuan == "Disetujui")
                {
                    SetujuColumn.HeaderText = "Disetujui";
                    SetujuColumn.ReadOnly = true;
                }
                
            }
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Permintaan p)
            {
                if (SetujuColumn.Index == e.ColumnIndex)
                {
                    var permintaan = db.Permintaan.Find(p.KodePermintaan);
                    PersetujuanForm form = new PersetujuanForm();
                    form.selectedPermintaan = permintaan;
                    if (form.ShowDialog() == DialogResult.OK) loadData();
                }
                if (DetailColumn.Index == e.ColumnIndex)
                {
                    DetailPermintaanForm form = new DetailPermintaanForm(p.KodePermintaan);
                    form.ShowDialog();
                }
            }
        }
    }
}
