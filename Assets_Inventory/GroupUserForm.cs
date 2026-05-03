using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory
{
    public partial class GroupUserForm : KryptonForm
    {
        private Client apiClient;
        private Dictionary<int, CheckBox> mapAkses = new Dictionary<int, CheckBox>();

        public GroupUserForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
            SetupAksesMapping();
        }

        private void SetupAksesMapping()
        {
            mapAkses.Add(1, cbInventaris);
            mapAkses.Add(2, cbPengadaanBarangInv);
            mapAkses.Add(3, cbInputTanah);
            mapAkses.Add(4, cbInputBangunan);

            mapAkses.Add(5, cbProses);
            mapAkses.Add(6, cbMutasiBarang);
            mapAkses.Add(7, cbOpname);
            mapAkses.Add(8, cbNonAktifBarang);
            mapAkses.Add(9, cbPeminjaman);
            mapAkses.Add(10, cbPengembalian);

            mapAkses.Add(11, cbBrgHabisPakai);
            mapAkses.Add(12, cbMasterData);
            mapAkses.Add(13, cbDataBarang);
            mapAkses.Add(14, cbPengadaanBarangHabisPakai);
            mapAkses.Add(15, cbBarangKeluar);
            mapAkses.Add(16, cbLapStokBarang);

            mapAkses.Add(17, cbAdmin);
            mapAkses.Add(18, cbDataMaster);
            mapAkses.Add(19, cbSetLembaga);
            mapAkses.Add(20, cbUser);
            mapAkses.Add(21, cbWallpaper);

            mapAkses.Add(22, cbLaporan);
            mapAkses.Add(23, cbSubLaporan);

            mapAkses.Add(24, cbTools);
            mapAkses.Add(25, cbKoneksi);
            mapAkses.Add(26, cbBackup);

            mapAkses.Add(27, cbHelp);
            mapAkses.Add(28, cbAbout);
            mapAkses.Add(29, cbTutorial);
        }

        private void GroupUserForm_Load(object sender, EventArgs e)
        {
            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            bool isEdit = mode != "View";

            txtNamaGroup.Enabled = isEdit;
            txtKeterangan.Enabled = isEdit;

            foreach (var cb in mapAkses.Values)
            {
                cb.Enabled = isEdit;
            }

            btnTambah.Enabled = !isEdit;
            btnUbah.Enabled = !isEdit;
            btnHapus.Enabled = !isEdit;

            btnSimpan.Enabled = isEdit;
            btnBatal.Enabled = isEdit;
        }

        private async void loadDgv()
        {
            try
            {
                bindingSource1.DataSource = (await apiClient.IndexPeranAsync()).Data.ToList();
                dgGroup.DataSource = bindingSource1;

                foreach (DataGridViewColumn col in dgGroup.Columns) col.Visible = false;

                dgGroup.Columns["Nama_peran"].Visible = true;
                dgGroup.Columns["Nama_peran"].HeaderText = "Nama Group";
                dgGroup.Columns["Keterangan"].Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void dgGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && bindingSource1.Current is PeranResource peran)
            {
                foreach (var cb in mapAkses.Values) cb.Checked = false;

                if (peran.Akses_list != null)
                {
                    foreach (var akses in peran.Akses_list)
                    {
                        if (mapAkses.ContainsKey(akses.Id_akses))
                        {
                            mapAkses[akses.Id_akses].Checked = true;
                        }
                    }
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            foreach (var cb in mapAkses.Values) cb.Checked = false;
            SetMode("Insert");
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PeranResource) SetMode("Update");
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            loadDgv();
            SetMode("View");
        }

        private async void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamaGroup.Text))
            {
                MessageBox.Show("Nama Group harus diisi!");
                return;
            }

            if (bindingSource1.Current is PeranResource peran)
            {
                bindingSource1.EndEdit();

                List<int> aksesYangDipilih = new List<int>();
                foreach (var item in mapAkses)
                {
                    if (item.Value.Checked)
                    {
                        aksesYangDipilih.Add(item.Key);
                    }
                }

                try
                {
                    int idPeranSekarang = peran.Id_peran;

                    if (idPeranSekarang == 0)
                    {
                        var response = await apiClient.StorePeranAsync(new StorePeranRequest
                        {
                            Nama_peran = txtNamaGroup.Text,
                            Keterangan = txtKeterangan.Text
                        });

                        idPeranSekarang = response.Data.Id_peran;
                    }
                    else
                    {
                        await apiClient.UpdatePeranAsync(idPeranSekarang.ToString(), new UpdatePeranRequest
                        {
                            Nama_peran = txtNamaGroup.Text,
                            Keterangan = txtKeterangan.Text
                        });
                    }

                    await apiClient.SyncAksesPeranAsync(idPeranSekarang.ToString(), new SyncAksesRequest
                    {
                        Id_akses = aksesYangDipilih
                    });

                    MessageBox.Show("Data dan Hak Akses berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgv();
                    SetMode("View");
                }
                catch (Assets_Inventory.ApiException apiEx)
                {
                    MessageBox.Show("Gagal menyimpan: " + apiEx.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PeranResource peran && peran.Id_peran != 0)
            {
                if (MessageBox.Show($"Hapus group {peran.Nama_peran}?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        await apiClient.DestroyPeranAsync(peran.Id_peran.ToString());
                        MessageBox.Show("Berhasil dihapus!");
                        loadDgv();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus: " + ex.Message);
                    }
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}