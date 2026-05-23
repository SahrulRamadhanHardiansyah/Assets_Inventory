using Assets_Inventory.Models;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class ResetDbForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public ResetDbForm()
        {
            InitializeComponent();
        }

        private void ResetDbForm_Load(object sender, EventArgs e)
        {
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void JalankanReset(Type[] tabelYangDiReset, string namaKategori)
        {
            if (MessageBox.Show($"Apakah Anda yakin ingin MENGHAPUS SEMUA DATA {namaKategori}?\n\nTindakan ini tidak bisa dibatalkan!", "Peringatan Fatal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var db = new AppDbContext())
                    {
                        db.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 0;");

                        foreach (var tipeTabel in tabelYangDiReset)
                        {
                            var namaTabelAsli = db.Model.FindEntityType(tipeTabel).GetTableName();

                            if (!string.IsNullOrEmpty(namaTabelAsli))
                            {
                                db.Database.ExecuteSqlRaw($"TRUNCATE TABLE `{namaTabelAsli}`;");
                            }
                        }

                        db.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 1;");
                    }

                    MessageBox.Show($"Seluruh data {namaKategori} berhasil dibersihkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mereset data: " + (ex.InnerException?.Message ?? ex.Message), "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            Type[] tabelMaster = new Type[]
            {
                typeof(Kategori),
                typeof(Merek),
                typeof(Lokasi),
                typeof(Ruang),
                typeof(Satuan),
                typeof(Kondisi),
                typeof(Gudang),
                typeof(Pemasok),
                typeof(StatusBarang),
                typeof(Jurusan),
                typeof(Rombel),
                typeof(Kelas),
                typeof(Unit),
                typeof(Mapel),
            };

            JalankanReset(tabelMaster, "Data Master");
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            Type[] tabelBarang = new Type[]
            {
                typeof(MasterBarang)
            };

            JalankanReset(tabelBarang, "Master Barang");
        }

        private void btnTanah_Click(object sender, EventArgs e)
        {
            Type[] tabelTanah = new Type[] { typeof(AsetTanah) };
            JalankanReset(tabelTanah, "Aset Tanah");
        }

        private void btnBangunan_Click(object sender, EventArgs e)
        {
            Type[] tabelBangunan = new Type[] { typeof(AsetBangunan) };
            JalankanReset(tabelBangunan, "Aset Bangunan");
        }

        private void btnInventarisNonAktif_Click(object sender, EventArgs e)
        {
            Type[] tabelNonAktif = new Type[]
            {
                typeof(BarangNonAktif),
                typeof(TanahNonAktif),
                typeof(BangunanNonAktif)
            };

            JalankanReset(tabelNonAktif, "Inventaris Non Aktif");
        }

        private void btnBarangHabisPakai_Click(object sender, EventArgs e)
        {
            Type[] tabelHabisPakai = new Type[]
            {
                typeof(AsetHabisPakai),
                typeof(PengadaanHabisPakai),
            };

            JalankanReset(tabelHabisPakai, "Barang Habis Pakai");
        }
    }
}