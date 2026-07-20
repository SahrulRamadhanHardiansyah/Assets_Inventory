using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using Assets_Inventory.UserControls;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class GroupUserForm : KryptonForm
    {
        private AppDbContext db = new AppDbContext();
        private ModulAkses _hakAkses; // stored for RBAC enforcement

        public GroupUserForm()
        {
            InitializeComponent();
        }

        private void GroupUserForm_Load(object sender, EventArgs e)
        {
            _hakAkses = AuthManager.GetAkses("Group User");

            if (!_hakAkses.HakBaca)
            {
                MessageBox.Show("Anda tidak memiliki akses untuk membuka halaman ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            RenderDynamicAkses();
            loadDgv();
            SetMode("View");
        }

        private void RenderDynamicAkses()
        {
            try
            {
                treeAkses.Nodes.Clear();
                var semuaModul = db.Akses.AsNoTracking().ToList();

                var parents = semuaModul.Where(x => x.IdParent == null).ToList();

                foreach (var p in parents)
                {
                    TreeNode pNode = new TreeNode(p.NamaModul) { Tag = "PARENT" };

                    var children = semuaModul.Where(x => x.IdParent == p.IdAkses).ToList();
                    foreach (var c in children)
                    {
                        TreeNode cNode = new TreeNode(c.NamaModul) { Tag = c.IdAkses };

                        cNode.Nodes.Add(new TreeNode("Tambah Data (Create)") { Tag = "CRUD" });
                        cNode.Nodes.Add(new TreeNode("Baca Data (Read)") { Tag = "CRUD" });
                        cNode.Nodes.Add(new TreeNode("Ubah Data (Update)") { Tag = "CRUD" });
                        cNode.Nodes.Add(new TreeNode("Hapus Data (Delete)") { Tag = "CRUD" });
                        cNode.Nodes.Add(new TreeNode("Approve Data") { Tag = "CRUD" });
                        cNode.Nodes.Add(new TreeNode("Export Data") { Tag = "CRUD" });

                        pNode.Nodes.Add(cNode);
                    }

                    if (pNode.Nodes.Count > 0) treeAkses.Nodes.Add(pNode);
                }

                treeAkses.ExpandAll();
            }
            catch
            {
                MessageBox.Show("Gagal memuat daftar modul.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeAkses_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                treeAkses.AfterCheck -= treeAkses_AfterCheck;
                CheckAllChildNodes(e.Node, e.Node.Checked);

                if (e.Node.Checked && e.Node.Parent != null)
                {
                    e.Node.Parent.Checked = true;
                    if (e.Node.Parent.Parent != null)
                    {
                        e.Node.Parent.Parent.Checked = true;
                    }
                }

                treeAkses.AfterCheck += treeAkses_AfterCheck;
            }
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0) CheckAllChildNodes(node, nodeChecked);
            }
        }

        private void SetMode(string mode)
        {
            bool isEdit = mode != "View";
            var hak = _hakAkses ?? new ModulAkses();

            txtNamaGroup.Enabled = isEdit;
            txtId.Enabled = isEdit;
            treeAkses.Enabled = isEdit;

            if (!isEdit)
            {
                // FIX: respect RBAC instead of unconditional !isEdit
                btnTambah.Enabled = hak.HakBuat;
                btnUbah.Enabled = hak.HakUbah;
                btnHapus.Enabled = hak.HakHapus;
                btnSimpan.Enabled = false;
                btnBatal.Enabled = false;
            }
            else
            {
                btnTambah.Enabled = false;
                btnUbah.Enabled = false;
                btnHapus.Enabled = false;
                btnSimpan.Enabled = hak.HakBuat || hak.HakUbah;
                btnBatal.Enabled = true;
            }
        }

        private void loadDgv()
        {
            try
            {
                bindingSource1.DataSource = db.Peran
                                              .Include(p => p.PeranAkses)
                                              .AsNoTracking()
                                              .ToList();
                dgGroup.DataSource = bindingSource1;
            }
            catch
            {
                MessageBox.Show("Gagal memuat data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgGroup.Rows[e.RowIndex].DataBoundItem is Peran p)
            {
                try
                {
                    var peran = db.Peran
                                  .Include(pr => pr.PeranAkses)
                                  .AsNoTracking()
                                  .FirstOrDefault(pr => pr.IdPeran == p.IdPeran);

                    if (peran != null)
                    {
                        // Need tracked entity for editing
                        var tracked = db.Peran.Include(pr => pr.PeranAkses).FirstOrDefault(pr => pr.IdPeran == p.IdPeran);
                        if (tracked != null)
                            bindingSource1.DataSource = tracked;
                        else
                            bindingSource1.DataSource = peran;

                        treeAkses.AfterCheck -= treeAkses_AfterCheck;
                        foreach (TreeNode pNode in treeAkses.Nodes)
                        {
                            pNode.Checked = false;
                            foreach (TreeNode cNode in pNode.Nodes)
                            {
                                cNode.Checked = false;
                                foreach (TreeNode crudNode in cNode.Nodes) crudNode.Checked = false;
                            }
                        }

                        if (peran.PeranAkses != null)
                        {
                            foreach (var aksesPivot in peran.PeranAkses)
                            {
                                foreach (TreeNode pNode in treeAkses.Nodes)
                                {
                                    foreach (TreeNode cNode in pNode.Nodes)
                                    {
                                        if (cNode.Tag != null && cNode.Tag.ToString() != "PARENT" && (int)cNode.Tag == aksesPivot.IdAkses)
                                        {
                                            pNode.Checked = true;
                                            cNode.Checked = true;
                                            cNode.Nodes[0].Checked = aksesPivot.HakBuat == true;
                                            cNode.Nodes[1].Checked = aksesPivot.HakBaca == true;
                                            cNode.Nodes[2].Checked = aksesPivot.HakUbah == true;
                                            cNode.Nodes[3].Checked = aksesPivot.HakHapus == true;
                                            if (cNode.Nodes.Count > 4) cNode.Nodes[4].Checked = aksesPivot.HakApprove == true;
                                            if (cNode.Nodes.Count > 5) cNode.Nodes[5].Checked = aksesPivot.HakExport == true;
                                        }
                                    }
                                }
                            }
                        }
                        treeAkses.AfterCheck += treeAkses_AfterCheck;
                    }
                }
                catch
                {
                    MessageBox.Show("Gagal memuat detail peran.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (_hakAkses != null && !_hakAkses.HakBuat)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk menambah group.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bindingSource1.AddNew();

            treeAkses.AfterCheck -= treeAkses_AfterCheck;
            foreach (TreeNode pNode in treeAkses.Nodes)
            {
                pNode.Checked = false;
                foreach (TreeNode cNode in pNode.Nodes)
                {
                    cNode.Checked = false;
                    foreach (TreeNode crudNode in cNode.Nodes) crudNode.Checked = false;
                }
            }
            treeAkses.AfterCheck += treeAkses_AfterCheck;

            SetMode("Insert");
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (_hakAkses != null && !_hakAkses.HakUbah)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk mengubah group.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bindingSource1.Current is Peran) SetMode("Update");
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            loadDgv();
            SetMode("View");
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaGroup.Text))
            {
                MessageBox.Show("Nama Group harus diisi!");
                return;
            }

            bool isNew = bindingSource1.Current is Peran cur && cur.IdPeran == 0;
            if (isNew && _hakAkses != null && !_hakAkses.HakBuat)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk menambah group.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isNew && _hakAkses != null && !_hakAkses.HakUbah)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk mengubah group.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bindingSource1.Current is Peran k)
            {
                bindingSource1.EndEdit();
                List<PeranAkses> aksesBaru = new List<PeranAkses>();

                foreach (TreeNode pNode in treeAkses.Nodes)
                {
                    foreach (TreeNode cNode in pNode.Nodes)
                    {
                        bool isCreate = cNode.Nodes[0].Checked;
                        bool isRead = cNode.Nodes[1].Checked;
                        bool isUpdate = cNode.Nodes[2].Checked;
                        bool isDelete = cNode.Nodes[3].Checked;
                        bool isApprove = cNode.Nodes.Count > 4 ? cNode.Nodes[4].Checked : false;
                        bool isExport = cNode.Nodes.Count > 5 ? cNode.Nodes[5].Checked : false;
                        if (cNode.Checked || isCreate || isRead || isUpdate || isDelete)
                        {
                            aksesBaru.Add(new PeranAkses
                            {
                                IdAkses = (int)cNode.Tag,
                                HakBuat = isCreate,
                                HakBaca = isRead,
                                HakUbah = isUpdate,
                                HakHapus = isDelete,
                                HakApprove = isApprove,
                                HakExport = isExport
                            });
                        }
                    }
                }

                try
                {
                    Peran peranDatabase;

                    if (k.IdPeran == 0)
                    {
                        peranDatabase = new Peran { NamaPeran = txtNamaGroup.Text.Trim() };
                        db.Peran.Add(peranDatabase);
                        db.SaveChanges(); // need Id for FK
                    }
                    else
                    {
                        peranDatabase = db.Peran
                                          .Include(p => p.PeranAkses)
                                          .FirstOrDefault(p => p.IdPeran == k.IdPeran);

                        if (peranDatabase != null)
                            peranDatabase.NamaPeran = txtNamaGroup.Text.Trim();
                    }

                    if (peranDatabase.IdPeran != 0 && peranDatabase.PeranAkses != null)
                    {
                        db.PeranAkses.RemoveRange(peranDatabase.PeranAkses);
                    }

                    foreach (var item in aksesBaru)
                    {
                        item.IdPeran = peranDatabase.IdPeran;
                        db.PeranAkses.Add(item);
                    }

                    db.SaveChanges();

                    MessageBox.Show("Data dan Hak Akses berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgv();
                    SetMode("View");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Save peran error: " + ex.Message);
                    MessageBox.Show("Terjadi kesalahan saat menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_hakAkses != null && !_hakAkses.HakHapus)
            {
                MessageBox.Show("Anda tidak memiliki hak untuk menghapus group.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevent deleting role assigned to current user
            if (bindingSource1.Current is Peran peran && peran.IdPeran != 0)
            {
                if (peran.IdPeran == AuthManager.CurrentRoleId)
                {
                    MessageBox.Show("Tidak dapat menghapus peran yang sedang digunakan oleh akun Anda sendiri.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Hapus group {peran.NamaPeran}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        var peranUntukDihapus = db.Peran.Include(p => p.PeranAkses).FirstOrDefault(p => p.IdPeran == peran.IdPeran);

                        if (peranUntukDihapus != null)
                        {
                            // Check if role in use
                            bool inUse = db.Pengguna.Any(u => u.IdPeran == peranUntukDihapus.IdPeran);
                            if (inUse)
                            {
                                MessageBox.Show("Tidak dapat menghapus peran yang masih digunakan oleh pengguna.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            db.PeranAkses.RemoveRange(peranUntukDihapus.PeranAkses);
                            db.Peran.Remove(peranUntukDihapus);
                            db.SaveChanges();

                            MessageBox.Show("Berhasil dihapus!");
                            loadDgv();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Delete peran error: " + ex.Message);
                        MessageBox.Show("Gagal menghapus peran.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
