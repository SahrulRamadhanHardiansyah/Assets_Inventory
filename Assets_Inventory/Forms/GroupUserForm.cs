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
        AppDbContext db = new AppDbContext();

        public GroupUserForm()
        {
            InitializeComponent();
        }

        private void GroupUserForm_Load(object sender, EventArgs e)
        {
            var hakAkses = AuthManager.GetAkses("Group User");

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

            RenderDynamicAkses();
            loadDgv();
            SetMode("View");
        }

        private void RenderDynamicAkses()
        {
            try
            {
                treeAkses.Nodes.Clear();
                var semuaModul = db.Akses.ToList();

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

                        pNode.Nodes.Add(cNode);
                    }

                    if (pNode.Nodes.Count > 0) treeAkses.Nodes.Add(pNode);
                }

                treeAkses.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar modul: " + ex.Message);
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

            txtNamaGroup.Enabled = isEdit;
            txtId.Enabled = isEdit;
            treeAkses.Enabled = isEdit;
            btnTambah.Enabled = !isEdit;
            btnUbah.Enabled = !isEdit;
            btnHapus.Enabled = !isEdit;
            btnSimpan.Enabled = isEdit;
            btnBatal.Enabled = isEdit;
        }

        private void loadDgv()
        {
            try
            {
                bindingSource1.DataSource = db.Peran
                                              .Include(p => p.PeranAkses)
                                              .ToList();
                dgGroup.DataSource = bindingSource1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void dgGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgGroup.Rows[e.RowIndex].DataBoundItem is Peran p)
            {
                var peran = db.Peran
                              .Include(pr => pr.PeranAkses)
                              .FirstOrDefault(pr => pr.IdPeran == p.IdPeran);

                if (peran != null)
                {
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
                                        cNode.Nodes[0].Checked = aksesPivot.HakBuat ?? false;
                                        cNode.Nodes[1].Checked = aksesPivot.HakBaca ?? false;
                                        cNode.Nodes[2].Checked = aksesPivot.HakUbah ?? false;
                                        cNode.Nodes[3].Checked = aksesPivot.HakHapus ?? false;
                                    }
                                }
                            }
                        }
                    }
                    treeAkses.AfterCheck += treeAkses_AfterCheck;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrEmpty(txtNamaGroup.Text))
            {
                MessageBox.Show("Nama Group harus diisi!");
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
                        if (cNode.Checked || isCreate || isRead || isUpdate || isDelete)
                        {
                            aksesBaru.Add(new PeranAkses
                            {
                                IdAkses = (int)cNode.Tag,
                                HakBuat = isCreate,
                                HakBaca = isRead,
                                HakUbah = isUpdate,
                                HakHapus = isDelete
                            });
                        }
                    }
                }

                try
                {
                    Peran peranDatabase;

                    if (k.IdPeran == 0)
                    {
                        peranDatabase = new Peran { NamaPeran = txtNamaGroup.Text };
                        db.Peran.Add(peranDatabase);
                    }
                    else
                    {
                        peranDatabase = db.Peran
                                          .Include(p => p.PeranAkses)
                                          .FirstOrDefault(p => p.IdPeran == k.IdPeran);

                        if (peranDatabase != null) peranDatabase.NamaPeran = txtNamaGroup.Text;
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
                    MessageBox.Show("Error Sistem: " + (ex.InnerException?.Message ?? ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is Peran peran && peran.IdPeran != 0)
            {
                if (MessageBox.Show($"Hapus group {peran.NamaPeran}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        var peranUntukDihapus = db.Peran.Include(p => p.PeranAkses).FirstOrDefault(p => p.IdPeran == peran.IdPeran);

                        if (peranUntukDihapus != null)
                        {
                            db.PeranAkses.RemoveRange(peranUntukDihapus.PeranAkses);
                            db.Peran.Remove(peranUntukDihapus);
                            db.SaveChanges();

                            MessageBox.Show("Berhasil dihapus!");
                            loadDgv();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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