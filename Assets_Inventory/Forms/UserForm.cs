using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class UserForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private AppDbContext db = new AppDbContext();
        private ModulAkses _hakAkses; // store for SetMode enforcement
        private const int BCRYPT_WORK_FACTOR = 12;

        // DTO without password exposure
        private class PenggunaDto
        {
            public int IdPengguna { get; set; }
            public string Username { get; set; }
            public string FullName { get; set; }
            public string NamaPeran { get; set; }
            public string NamaJurusan { get; set; }
            public string NamaKelas { get; set; }
            public string NamaMapel { get; set; }
        }

        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            _hakAkses = AuthManager.GetAkses("User");

            if (!_hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            try
            {
                cmbPeran.DataSource = db.Peran.AsNoTracking().ToList();
                cmbJurusan.DataSource = db.Jurusan.AsNoTracking().ToList();
                cmbMapel.DataSource = db.Mapel.AsNoTracking().ToList();
                cmbKelas.DataSource = db.Kelas.AsNoTracking().ToList();

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
            catch
            {
                MessageBox.Show("Gagal memuat data pengguna.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetMode(string mode)
        {
            bool isEdit = mode != "View";
            var hak = _hakAkses ?? new ModulAkses();

            if (!isEdit)
            {
                txtPassword.Enabled = false;
                txtPassword2.Enabled = false;
                txtUsername.Enabled = false;
                txtFullname.Enabled = false;
                cmbJurusan.Enabled = false;
                cmbMapel.Enabled = false;
                cmbKelas.Enabled = false;
                cmbPeran.Enabled = false;

                // FIX: respect RBAC instead of hard true
                btnTambah.Enabled = hak.HakBuat;
                btnUbah.Enabled = hak.HakUbah;
                btnHapus.Enabled = hak.HakHapus;
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
                // Allow save if either create or update permission
                btnSimpan.Enabled = hak.HakBuat || hak.HakUbah;
                btnBatal.Enabled = true;

                TriggerUIState();
            }
        }

        private void loadDgv()
        {
            try
            {
                var cari = txtCari.Text?.ToLower()?.Trim() ?? "";
                // Projection without password - prevents hash exposure
                var query = db.Pengguna
                    .Include(p => p.IdPeranNavigation)
                    .Include(p => p.IdJurusanNavigation)
                    .Include(p => p.IdKelasNavigation)
                    .Include(p => p.IdMapelNavigation)
                    .AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(cari))
                {
                    query = query.Where(d => d.Username.ToLower().Contains(cari) ||
                                             (d.FullName != null && d.FullName.ToLower().Contains(cari)));
                }

                var list = query.Select(p => new PenggunaDto
                {
                    IdPengguna = p.IdPengguna,
                    Username = p.Username,
                    FullName = p.FullName,
                    NamaPeran = p.IdPeranNavigation != null ? p.IdPeranNavigation.NamaPeran : "-",
                    NamaJurusan = p.IdJurusanNavigation != null ? p.IdJurusanNavigation.NamaJurusan : "-",
                    NamaKelas = p.IdKelasNavigation != null ? p.IdKelasNavigation.NamaKelas : "-",
                    NamaMapel = p.IdMapelNavigation != null ? p.IdMapelNavigation.NamaMapel : "-"
                }).ToList();

                dg.DataSource = new SortableBindingList<PenggunaDto>(list);
                if (dg.Columns["FullName"] != null) dg.Columns["FullName"].HeaderText = "Nama Lengkap";
                if (dg.Columns["NamaPeran"] != null) dg.Columns["NamaPeran"].HeaderText = "Peran";
            }
            catch
            {
                MessageBox.Show("Gagal memuat data pengguna.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (e.RowIndex < 0) return;
            try
            {
                int id = 0;
                if (dg.Rows[e.RowIndex].DataBoundItem is PenggunaDto dto)
                    id = dto.IdPengguna;
                else if (dg.Rows[e.RowIndex].DataBoundItem is Pengguna p)
                    id = p.IdPengguna;
                else return;

                var pengguna = db.Pengguna.Find(id);
                if (pengguna != null)
                {
                    bindingSource1.DataSource = pengguna;
                    txtFullname.Text = pengguna.FullName;
                    txtUsername.Text = pengguna.Username;

                    cmbPeran.SelectedValue = pengguna.IdPeran;

                    if (pengguna.IdMapel != null) cmbMapel.SelectedValue = pengguna.IdMapel;
                    else cmbMapel.SelectedIndex = -1;

                    if (pengguna.IdKelas != null) cmbKelas.SelectedValue = pengguna.IdKelas;
                    else cmbKelas.SelectedIndex = -1;

                    if (pengguna.IdJurusan != null) cmbJurusan.SelectedValue = pengguna.IdJurusan;
                    else cmbJurusan.SelectedIndex = -1;
                }
            }
            catch
            {
                MessageBox.Show("Gagal memuat detail pengguna.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Guard RBAC
            if (_hakAkses != null && !_hakAkses.HakBuat)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk menambah pengguna.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            if (_hakAkses != null && !_hakAkses.HakUbah)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk mengubah pengguna.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bindingSource1.Current is Pengguna)
            {
                SetMode("Update");
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah.");
            }
        }

        private bool ValidatePasswordComplexity(string pwd, string username, out string err)
        {
            err = null;
            if (pwd.Length < 8)
            {
                err = "Password minimal 8 karakter.";
                return false;
            }
            if (!string.IsNullOrEmpty(username) && pwd.ToLower().Contains(username.ToLower()))
            {
                err = "Password tidak boleh mengandung username.";
                return false;
            }
            // Require at least 1 letter and 1 digit for new passwords
            bool hasLetter = pwd.Any(char.IsLetter);
            bool hasDigit = pwd.Any(char.IsDigit);
            if (!hasLetter || !hasDigit)
            {
                err = "Password harus mengandung huruf dan angka.";
                return false;
            }
            return true;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            var peranObj = cmbPeran.SelectedItem as Peran;
            string namaPeran = peranObj != null ? peranObj.NamaPeran : "";

            // RBAC guard before save
            bool isNew = (bindingSource1.Current is Pengguna cur && cur.IdPengguna == 0) || !(bindingSource1.Current is Pengguna);
            if (isNew && _hakAkses != null && !_hakAkses.HakBuat)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk menambah pengguna.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isNew && _hakAkses != null && !_hakAkses.HakUbah)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk mengubah pengguna.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            // Password complexity for new or changed password
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                if (!ValidatePasswordComplexity(txtPassword.Text, txtUsername.Text, out string pwdErr))
                {
                    MessageBox.Show(pwdErr, "Validasi Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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
                    hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, BCRYPT_WORK_FACTOR);
                }

                try
                {
                    int? targetIdKelas = (namaPeran == "Murid" && cmbKelas.SelectedValue != null) ? (int?)cmbKelas.SelectedValue : null;
                    int? targetIdMapel = (namaPeran == "Guru" && cmbMapel.SelectedValue != null) ? (int?)cmbMapel.SelectedValue : null;

                    if (k.IdPengguna == 0)
                    {
                        var baru = new Pengguna
                        {
                            Username = txtUsername.Text.Trim(),
                            FullName = txtFullname.Text.Trim(),
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
                        k.Username = txtUsername.Text.Trim();
                        k.FullName = txtFullname.Text.Trim();
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
                    System.Diagnostics.Debug.WriteLine("Save user error: " + ex.Message);
                    MessageBox.Show("Terjadi kesalahan sistem saat menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_hakAkses != null && !_hakAkses.HakHapus)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk menghapus pengguna.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bindingSource1.Current is Pengguna k && k.IdPengguna != 0)
            {
                // Prevent self-deletion
                if (k.IdPengguna == AuthManager.CurrentUserId)
                {
                    MessageBox.Show("Tidak dapat menghapus akun sendiri.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                        try { db.Entry(k).Reload(); } catch { }
                        MessageBox.Show("Tidak dapat menghapus data ini karena data masih digunakan oleh data lain di dalam sistem.", "Peringatan Relasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        try { db.Entry(k).Reload(); } catch { }
                        System.Diagnostics.Debug.WriteLine("Delete user error: " + ex.Message);
                        MessageBox.Show("Terjadi kesalahan sistem saat menghapus.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // DTO no longer needs nav property resolution; keep for backward compat if Pengguna still bound
            if (dg.Rows[e.RowIndex].DataBoundItem is Pengguna pengguna)
            {
                try
                {
                    if (idKelasNavigationDataGridViewTextBoxColumn != null && idKelasNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                        e.Value = pengguna.IdKelasNavigation != null ? pengguna.IdKelasNavigation.NamaKelas : "-";
                    if (idMapelNavigationDataGridViewTextBoxColumn != null && idMapelNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                        e.Value = pengguna.IdMapelNavigation != null ? pengguna.IdMapelNavigation.NamaMapel : "-";
                    if (idPeranNavigationDataGridViewTextBoxColumn != null && idPeranNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                        e.Value = pengguna.IdPeranNavigation != null ? pengguna.IdPeranNavigation.NamaPeran : "-";
                    if (idJurusanNavigationDataGridViewTextBoxColumn != null && idJurusanNavigationDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                        e.Value = pengguna.IdJurusanNavigation != null ? pengguna.IdJurusanNavigation.NamaJurusan : "-";
                }
                catch { }
            }
        }

    }
}
