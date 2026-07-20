using Assets_Inventory.Helper;
using Assets_Inventory.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.UserControls
{
    public partial class AsetLampiranUC : UserControl
    {
        private int _refId;
        private string _tipeAset;
        private string _refKode;

        public AsetLampiranUC(int refId, string tipeAset, string refKode = null)
        {
            _refId = refId;
            _tipeAset = tipeAset ?? "Aset";
            _refKode = refKode ?? refId.ToString();
            InitializeComponent();
        }

        public AsetLampiranUC() : this(0, "Aset") { }

        private void AsetLampiranUC_Load(object sender, EventArgs e)
        {
            lblInfo.Text = $"Lampiran untuk {_tipeAset} - {_refKode}";
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var list = db.AsetLampiran
                        .Where(a => a.RefId == _refId && a.TipeAset == _tipeAset)
                        .OrderByDescending(a => a.CreatedAt)
                        .ToList();

                    dgLampiran.DataSource = list.Select(l => new
                    {
                        l.Id,
                        l.TipeAset,
                        l.OriginalFileName,
                        l.FileType,
                        FileSizeKB = $"{l.FileSize / 1024} KB",
                        l.CreatedAt,
                        l.Keterangan
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load lampiran error: " + ex.Message);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Semua File|*.*|Gambar|*.jpg;*.jpeg;*.png;*.bmp|Dokumen|*.pdf;*.docx;*.xlsx";
                ofd.Title = "Pilih File Lampiran";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var fi = new FileInfo(ofd.FileName);
                        if (fi.Length > AppConstants.MaxAttachmentFileSizeBytes)
                        {
                            MessageBox.Show($"File terlalu besar (max {AppConstants.MaxAttachmentFileSizeBytes / 1024 / 1024}MB).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string ext = Path.GetExtension(ofd.FileName).ToLower();
                        string fileType = ".jpg .jpeg .png .bmp".Contains(ext) ? "Foto" : "Dokumen";

                        // Save to Attachments/{kode}/
                        string baseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Attachments", _refKode);
                        if (!Directory.Exists(baseFolder)) Directory.CreateDirectory(baseFolder);

                        string safeName = Guid.NewGuid().ToString("N") + ext;
                        string destPath = Path.Combine(baseFolder, safeName);
                        File.Copy(ofd.FileName, destPath, true);

                        using (var db = new AppDbContext())
                        {
                            db.AsetLampiran.Add(new AsetLampiran
                            {
                                TipeAset = _tipeAset,
                                RefId = _refId,
                                RefKode = _refKode,
                                FilePath = destPath,
                                OriginalFileName = Path.GetFileName(ofd.FileName),
                                FileType = fileType,
                                FileSize = fi.Length,
                                CreatedAt = DateTime.Now,
                                CreatedBy = AuthManager.CurrentUserId != 0 ? (int?)AuthManager.CurrentUserId : null,
                                Keterangan = txtKeterangan.Text
                            });
                            db.SaveChanges();
                        }

                        AuditHelper.Log("aset_lampiran", _refId.ToString(), "INSERT", (object)null, (object)new { file = safeName }, "Lampiran");
                        LoadData();
                        MessageBox.Show("Lampiran berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Add lampiran error: " + ex.Message);
                        MessageBox.Show("Gagal menambahkan lampiran.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLihat_Click(object sender, EventArgs e)
        {
            if (dgLampiran.CurrentRow == null) return;
            try
            {
                var row = dgLampiran.CurrentRow;
                // Get full entity
                using (var db = new AppDbContext())
                {
                    var idProp = row.DataBoundItem?.GetType().GetProperty("Id");
                    if (idProp == null) return;
                    int id = (int)idProp.GetValue(row.DataBoundItem);
                    var lamp = db.AsetLampiran.Find(id);
                    if (lamp != null && File.Exists(lamp.FilePath))
                    {
                        var psi = new System.Diagnostics.ProcessStartInfo { FileName = lamp.FilePath, UseShellExecute = true };
                        System.Diagnostics.Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("File tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch { MessageBox.Show("Gagal membuka file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgLampiran.CurrentRow == null) return;
            if (MessageBox.Show("Hapus lampiran ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var row = dgLampiran.CurrentRow;
                var idProp = row.DataBoundItem?.GetType().GetProperty("Id");
                if (idProp == null) return;
                int id = (int)idProp.GetValue(row.DataBoundItem);

                using (var db = new AppDbContext())
                {
                    var lamp = db.AsetLampiran.Find(id);
                    if (lamp != null)
                    {
                        try { if (File.Exists(lamp.FilePath)) File.Delete(lamp.FilePath); } catch { }
                        db.AsetLampiran.Remove(lamp);
                        db.SaveChanges();
                    }
                }

                AuditHelper.Log("aset_lampiran", id.ToString(), "DELETE", (object)null, (object)null, "Lampiran");
                LoadData();
            }
            catch { MessageBox.Show("Gagal menghapus.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgLampiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnLihat_Click(sender, e);
        }
    }
}
