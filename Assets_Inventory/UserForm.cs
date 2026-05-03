using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory
{
    public partial class UserForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private Client apiClient;
        public UserForm()
        {
            InitializeComponent();
            if (!ApiClientHelper.TrySetToken()) return;
            apiClient = new Client(ApiClientHelper.SharedHttpClient);
        }

        private async void UserForm_Load(object sender, EventArgs e)
        {
            cmbLevel.DataSource = (await apiClient.IndexPeranAsync()).Data;
            cmbLevel.SelectedIndex = 0;

            loadDgv();
            SetMode("View");
        }

        private void SetMode(string mode)
        {
            if (mode == "View")
            {
                txtPassword.Enabled = false;
                txtPassword2.Enabled = false;
                txtUsername.Enabled = false;
                btnTambah.Enabled = true;
                btnUbah.Enabled = true;
                btnHapus.Enabled = true;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                txtPassword.Enabled = true;
                txtPassword2.Enabled = true;
                txtUsername.Enabled = true;
                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;
            }
        }

        private async void loadDgv()
        {
            var cari = txtCari.Text.ToLower();
            dg.DataSource = (await apiClient.IndexPenggunaAsync()).Data.Where(d => d.Username.ToLower().Contains(cari))
                .Select(d => new
                {
                    d.Username,
                    d.Peran.Nama_peran,
                    d.Mapel.Nama_mapel,
                    d.Unit.Nama_unit
                }).ToList();
        }

        private void uncheckAllRadioButtonInputs()
        {
            lblUnit.Visible = false;
            lblUnit.Enabled = false;
            cmbUnit.Visible = false;
            cmbUnit.Enabled = false;
            lblMapel.Visible = false;
            lblMapel.Enabled = false;
            cmbMapel.Visible = false;
            cmbMapel.Enabled = false;
            lblKelas.Visible = false;
            lblKelas.Enabled = false;
            cmbKelas.Visible = false;
            cmbKelas.Enabled = false;
        }

        private void rbStaff_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStaff.Checked)
            {
                uncheckAllRadioButtonInputs();
                lblUnit.Visible = true;
                lblUnit.Enabled = true;
                cmbUnit.Visible = true;
                cmbUnit.Enabled = true;
            }
        }

        private void rbGuru_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGuru.Checked)
            {
                uncheckAllRadioButtonInputs();
                lblMapel.Visible = true;
                lblMapel.Enabled = true;
                cmbMapel.Visible = true;
                cmbMapel.Enabled = true;
            }
        }

        private void rbMurid_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMurid.Checked)
            {
                uncheckAllRadioButtonInputs();
                lblKelas.Visible = true;
                lblKelas.Enabled = true;
                cmbKelas.Visible = true;
                cmbKelas.Enabled = true;
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadDgv();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is PenggunaResource pengguna)
            {
                bindingSource1.DataSource = pengguna;
                cmbLevel.SelectedItem = pengguna.Peran;
                
                if (cmbLevel.SelectedText == "Staff")
                {
                    rbStaff.Checked = true;
                    cmbUnit.SelectedItem = pengguna.Unit;
                }
                else if (cmbLevel.SelectedText == "Guru")
                {
                    rbGuru.Checked = true;
                    cmbMapel.SelectedItem = pengguna.Mapel;
                }
                else if (cmbLevel.SelectedText == "Murid")
                {
                    rbMurid.Checked = true;
                    cmbKelas.SelectedItem = pengguna.Kelas;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PenggunaResource k)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private async void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || cmbLevel.SelectedIndex == -1)
            {
                MessageBox.Show("Nama pengguna dan level group harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtPassword2.Text)
            {
                MessageBox.Show("Password dan konfirmasi password tidak sama.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bindingSource1.Current is PenggunaResource k)
            {
                bindingSource1.EndEdit();

                if (k.Id_pengguna == 0 && string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Password wajib diisi untuk pengguna baru.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                k.Username = txtUsername.Text;
                k.Id_peran = (int)cmbLevel.SelectedValue;
                k.Id_kelas = rbMurid.Checked ? (cmbKelas.SelectedItem as KelasResource)?.Id_kelas : null;
                k.Id_mapel = rbGuru.Checked ? (cmbMapel.SelectedItem as MapelResource)?.Id_mapel : null;
                k.Id_unit = rbStaff.Checked ? (cmbUnit.SelectedItem as UnitResource)?.Id_unit : null;
                string passwordKirim = string.IsNullOrEmpty(txtPassword.Text) ? null : txtPassword.Text;

                try
                {
                    if (k.Id_pengguna == 0)
                    {
                        await apiClient.StorePenggunaAsync(new StorePenggunaRequest
                        {
                            Username = k.Username,
                            Password = passwordKirim, 
                            Id_peran = k.Id_peran ?? 1,
                            Id_kelas = k.Id_kelas,
                            Id_mapel = k.Id_mapel,
                            Id_unit = k.Id_unit,
                        });
                    }
                    else
                    {
                        await apiClient.UpdatePenggunaAsync(k.Id_pengguna.ToString(), new UpdatePenggunaRequest
                        {
                            Username = k.Username,
                            Password = passwordKirim,
                            Id_peran = k.Id_peran ?? 1,
                            Id_kelas = k.Id_kelas,
                            Id_mapel = k.Id_mapel,
                            Id_unit = k.Id_unit,
                        });
                    }

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgv();
                    SetMode("View");
                }
                catch (Assets_Inventory.ApiException apiEx)
                {
                    MessageBox.Show("Gagal menyimpan data: " + apiEx.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is PenggunaResource k)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.Username}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        await apiClient.DestroyPenggunaAsync(k.Id_pengguna.ToString());
                    }
                    catch (Assets_Inventory.ApiException apiEx)
                    {
                        MessageBox.Show("Gagal menghapus data: " + apiEx.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Berhasil dihapus!");
                    loadDgv();
                    bindingSource1.AddNew();
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            bindingSource1.AddNew();
            loadDgv();
            SetMode("View");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
