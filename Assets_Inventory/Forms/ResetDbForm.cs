using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
            var hakAkses = AuthManager.GetAkses("Reset");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void JalankanReset(Type[] tabelYangDiReset, string namaKategori)
        {
            // Double confirmation with typed input
            string confirmText = $"HAPUS {namaKategori}";
            using (var inputForm = new Form())
            {
                inputForm.Text = "Konfirmasi Hapus";
                inputForm.Width = 450;
                inputForm.Height = 220;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.MaximizeBox = false;

                var lbl = new Label { Text = $"Tindakan ini akan MENGHAPUS SEMUA DATA {namaKategori}!\n\nKetik '{confirmText}' untuk konfirmasi:", AutoSize = false, Width = 400, Height = 60, Left = 15, Top = 15 };
                var txt = new TextBox { Left = 15, Top = 85, Width = 400 };
                var btnOk = new Button { Text = "Hapus", Left = 230, Top = 125, Width = 90, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Batal", Left = 325, Top = 125, Width = 90, DialogResult = DialogResult.Cancel };
                inputForm.Controls.Add(lbl);
                inputForm.Controls.Add(txt);
                inputForm.Controls.Add(btnOk);
                inputForm.Controls.Add(btnCancel);
                inputForm.AcceptButton = btnOk;
                inputForm.CancelButton = btnCancel;

                if (inputForm.ShowDialog() != DialogResult.OK) return;
                if (txt.Text.Trim() != confirmText)
                {
                    MessageBox.Show($"Konfirmasi tidak sesuai. Harus ketik persis: {confirmText}", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Second yes/no
            if (MessageBox.Show($"Apakah Anda yakin ingin MENGHAPUS SEMUA DATA {namaKategori}?\nTindakan ini tidak bisa dibatalkan!", "Peringatan Fatal",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new AppDbContext())
                {
                    // Validate table names whitelist: only known entity types + safe identifier
                    foreach (var tipe in tabelYangDiReset)
                    {
                        var tableName = db.Model.FindEntityType(tipe)?.GetTableName();
                        if (!string.IsNullOrEmpty(tableName))
                        {
                            if (!Regex.IsMatch(tableName, @"^[a-zA-Z0-9_]+$"))
                                throw new ArgumentException($"Nama tabel tidak valid: {tableName}");
                        }
                    }

                    // Use transaction to keep FK checks consistent
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 0;");

                            foreach (var tipeTabel in tabelYangDiReset)
                            {
                                var namaTabelAsli = db.Model.FindEntityType(tipeTabel).GetTableName();
                                if (!string.IsNullOrEmpty(namaTabelAsli))
                                {
                                    // ponytail: identifier tidak bisa diparameterize, whitelist Type[] + regex adalah ceiling
                                    db.Database.ExecuteSqlRaw($"TRUNCATE TABLE `{namaTabelAsli}`;");
                                }
                            }

                            db.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 1;");
                            transaction.Commit();
                        }
                        catch
                        {
                            try
                            {
                                db.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 1;");
                                transaction.Rollback();
                            }
                            catch { }
                            throw;
                        }
                    }
                }

                MessageBox.Show($"Seluruh data {namaKategori} berhasil dibersihkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Reset error: " + ex.Message);
                MessageBox.Show("Gagal mereset data. Silakan hubungi administrator.", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
