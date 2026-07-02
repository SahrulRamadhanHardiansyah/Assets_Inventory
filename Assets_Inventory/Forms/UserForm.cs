using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class UserForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        AppDbContext db = new AppDbContext();

        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("User");

            if (!hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            btnTambah.Enabled = hakAkses.HakBuat;
            btnUbah.Enabled = hakAkses.HakUbah;
            btnSimpan.Enabled = hakAkses.HakBuat || hakAkses.HakUbah;
            btnHapus.Enabled = hakAkses.HakHapus;

            cmbPeran.DataSource = db.Peran.ToList();
            cmbJurusan.DataSource = db.Jurusan.ToList();
            cmbMapel.DataSource = db.Mapel.ToList();
            cmbKelas.DataSource = db.Kelas.ToList();

            cmbPeran.DisplayMember = "NamaPeran";
            cmbPeran.ValueMember = "IdPeran";
            cmbJurusan.DisplayMember = "NamaJurusan";
            cmbJurusan.ValueMember = "IdJurusan";
            cmbMapel.DisplayMember = "NamaMapel";
            cmbMapel.ValueMember = "IdMapel";
            cmbKelas.DisplayMember = "NamaKelas";
            cmbKelas.ValueMember = "IdKelas";

            cmbPeran.SelectedIndex = 0;
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
                txtFullname.Enabled = false;
                cmbJurusan.Enabled = false;
                cmbMapel.Enabled = false;
                cmbKelas.Enabled = false;
                cmbPeran.Enabled = false;

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
                txtFullname.Enabled = true;
                cmbJurusan.Enabled = true;
                cmbMapel.Enabled = true;
                cmbKelas.Enabled = true;
                cmbPeran.Enabled = true;

                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = true;
                btnBatal.Enabled = true;

                TriggerUIState();
            }
        }

        private void loadDgv()
        {
            var cari = txtCari.Text.ToLower();
            var query = db.Pengguna.AsQueryable();

            if (!string.IsNullOrEmpty(cari))
            {
                query = query.Where(d => d.Username.ToLower().Contains(cari) ||
                                         (d.FullName != null && d.FullName.ToLower().Contains(cari)));
            }

            dg.DataSource = new SortableBindingList<Pengguna>(query.ToList());
            if (dg.Columns["FullName"] != null) dg.Columns["FullName"].HeaderText = "Nama Lengkap";
        }

        private void TriggerUIState()
        {
            lblMapel.Visible = false;
            cmbMapel.Visible = false;
            lblKelas.Visible = false;
            cmbKelas.Visible = false;

            if (cmbPeran.SelectedItem is Peran peranTerpilih)
            {
                if (peranTerpilih.NamaPeran == "Guru")
                {
                    lblMapel.Visible = true;
                    cmbMapel.Visible = true;
                }
                else if (peranTerpilih.NamaPeran == "Murid")
                {
                    lblKelas.Visible = true;
                    cmbKelas.Visible = true;
                }
            }
        }

        private void cmbPeran_SelectedIndexChanged(object sender, EventArgs e)
        {
            TriggerUIState();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadDgv();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dg.Rows[e.RowIndex].DataBoundItem is Pengguna p)
            {
                var pengguna = db.Pengguna.Find(p.IdPengguna);

                if (pengguna != null)
                {
                    bindingSource1.DataSource = pengguna;
                    txtFullname.Text = pengguna.FullName;

                    cmbPeran.SelectedValue = pengguna.IdPeran;

                    if (pengguna.IdMapel != null) cmbMapel.SelectedValue = pengguna.IdMapel;
                    else cmbMapel.SelectedIndex = -1;

                    if (pengguna.IdKelas != null) cmbKelas.SelectedValue = pengguna.IdKelas;
                    else cmbKelas.SelectedIndex = -1;

                    if (pengguna.IdJurusan != null) cmbJurusan.SelectedValue = pengguna.IdJurusan;
                    else cmbJurusan.SelectedIndex = -1;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SetMode("Insert");
            bindingSource1.AddNew();
            txtUsername.Clear();
            txtFullname.Clear();
            txtPassword.Clear();
            txtPassword2.Clear();
            cmbMapel.SelectedIndex = -1;
            cmbKelas.SelectedIndex = -1;
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pengguna)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            var peranObj = cmbPeran.SelectedItem as Peran;
            string namaPeran = peranObj != null ? peranObj.NamaPeran : "";

            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtFullname.Text) || cmbPeran.SelectedIndex == -1)
            {
                MessageBox.Show("Nama pengguna, nama lengkap, dan peran harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbJurusan.SelectedIndex == -1)
            {
                MessageBox.Show("Jurusan harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (namaPeran == "Guru" && cmbMapel.SelectedIndex == -1)
            {
                MessageBox.Show("Mata pelajaran harus dipilih untuk guru.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (namaPeran == "Murid" && cmbKelas.SelectedIndex == -1)
            {
                MessageBox.Show("Kelas harus dipilih untuk murid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtPassword2.Text)
            {
                MessageBox.Show("Password dan konfirmasi password tidak sama.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bindingSource1.Current is Pengguna k)
            {
                bindingSource1.EndEdit();

                if (k.IdPengguna == 0 && string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Password wajib diisi untuk pengguna baru.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string hashedPassword = "";
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, 10);
                }

                try
                {
                    int? targetIdKelas = (namaPeran == "Murid" && cmbKelas.SelectedValue != null) ? (int?)cmbKelas.SelectedValue : null;
                    int? targetIdMapel = (namaPeran == "Guru" && cmbMapel.SelectedValue != null) ? (int?)cmbMapel.SelectedValue : null;

                    if (k.IdPengguna == 0)
                    {
                        var baru = new Pengguna
                        {
                            Username = txtUsername.Text,
                            FullName = txtFullname.Text,
                            IdPeran = (int)cmbPeran.SelectedValue,
                            IdKelas = targetIdKelas,
                            IdMapel = targetIdMapel,
                            IdJurusan = cmbJurusan.SelectedValue != null ? (int?)cmbJurusan.SelectedValue : null,
                            Password = hashedPassword
                        };
                        db.Pengguna.Add(baru);
                    }
                    else
                    {
                        k.Username = txtUsername.Text;
                        k.FullName = txtFullname.Text;
                        k.IdPeran = (int)cmbPeran.SelectedValue;
                        k.IdKelas = targetIdKelas;
                        k.IdMapel = targetIdMapel;
                        k.IdJurusan = cmbJurusan.SelectedValue != null ? (int?)cmbJurusan.SelectedValue : null;

                        if (!string.IsNullOrEmpty(txtPassword.Text))
                        {
                            k.Password = hashedPassword;
                        }
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtPassword.Clear();
                    txtPassword2.Clear();
                    loadDgv();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Pengguna k && k.IdPengguna != 0)
            {
                if (MessageBox.Show($"Apakah anda yakin ingin menghapus data {k.Username}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.Pengguna.Remove(k);
                        db.SaveChanges();

                        MessageBox.Show("Berhasil dihapus!");
                        loadDgv();
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

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            bindingSource1.AddNew();
            txtUsername.Clear();
            txtFullname.Clear();
            txtPassword.Clear();
            txtPassword2.Clear();
            loadDgv();
            SetMode("View");
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dg.Rows[e.RowIndex].DataBoundItem is Pengguna pengguna)
            {
                if (idKelasNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = pengguna.IdKelasNavigation != null ? pengguna.IdKelasNavigation.NamaKelas : "-";
                if (idMapelNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = pengguna.IdMapelNavigation != null ? pengguna.IdMapelNavigation.NamaMapel : "-";
                if (idPeranNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = pengguna.IdPeranNavigation != null ? pengguna.IdPeranNavigation.NamaPeran : "-";
                if (idJurusanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex) e.Value = pengguna.IdJurusanNavigation != null ? pengguna.IdJurusanNavigation.NamaJurusan : "-";
            }
        }
    }
}