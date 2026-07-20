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
        private int _kodeBarang;
        private string _tipeAset;
        private readonly string[] _imgExts = { ".jpg", ".jpeg", ".png", ".bmp" };
        private readonly string[] _allowedExts = { ".jpg", ".jpeg", ".png", ".bmp", ".pdf" };

        public AsetLampiranUC(int kodeBarang, string tipeAset)
        {
            _kodeBarang = kodeBarang;
            _tipeAset = tipeAset ?? "Aset";
            InitializeComponent();
        }

        // Designer & legacy compatibility
        public AsetLampiranUC() : this(0, "Aset") { }
        public AsetLampiranUC(int refId, string tipeAset, string refKode) : this(refId, tipeAset) { }

        private void AsetLampiranUC_Load(object sender, EventArgs e)
        {
            if (lblInfo != null)
                lblInfo.Text = $"Lampiran [{_tipeAset}] Kode: {_kodeBarang}";
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (_kodeBarang <= 0)
                {
                    dgLampiran.DataSource = null;
                    ClearPreview();
                    return;
                }

                using (var db = new AppDbContext())
                {
                    var list = db.AsetLampiran
                        .Where(a => a.RefId == _kodeBarang && a.TipeAset == _tipeAset)
                        .OrderByDescending(a => a.CreatedAt)
                        .ToList();

                    // ponytail: anonymous bind keeps diff minimal; typed view when paging/sorting needed
                    var bind = list.Select(l => new
                    {
                        l.Id,
                        l.OriginalFileName,
                        l.FileType,
                        FileSizeKB = $"{l.FileSize / 1024} KB",
                        l.CreatedAt,
                        l.Keterangan
                    }).ToList();

                    dgLampiran.DataSource = bind;
                    if (dgLampiran.Columns["Id"] != null) dgLampiran.Columns["Id"].HeaderText = "ID";
                    if (dgLampiran.Columns["OriginalFileName"] != null) dgLampiran.Columns["OriginalFileName"].HeaderText = "Nama File";
                    if (dgLampiran.Columns["FileType"] != null) dgLampiran.Columns["FileType"].HeaderText = "Tipe";
                    if (dgLampiran.Columns["CreatedAt"] != null) dgLampiran.Columns["CreatedAt"].HeaderText = "Tgl";
                }

                if (dgLampiran.Rows.Count == 0) ClearPreview();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load lampiran error: " + ex.Message);
            }
        }

        private int GetSelectedId()
        {
            if (dgLampiran.CurrentRow == null) return 0;
            try
            {
                var r = dgLampiran.CurrentRow;
                if (r.Cells["Id"] != null && r.Cells["Id"].Value != null)
                    return Convert.ToInt32(r.Cells["Id"].Value);

                var item = r.DataBoundItem;
                var prop = item?.GetType().GetProperty("Id");
                if (prop != null) return (int)prop.GetValue(item);
            }
            catch { }
            return 0;
        }

        private void dgLampiran_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            try
            {
                int id = GetSelectedId();
                if (id == 0) { ClearPreview(); return; }

                using (var db = new AppDbContext())
                {
                    var lamp = db.AsetLampiran.Find(id);
                    if (lamp == null || !File.Exists(lamp.FilePath))
                    {
                        ClearPreview();
                        return;
                    }

                    string ext = Path.GetExtension(lamp.FilePath).ToLowerInvariant();
                    if (_imgExts.Contains(ext))
                        SetPreviewImage(lamp.FilePath);
                    else
                        ClearPreview();
                }
            }
            catch { ClearPreview(); }
        }

        private void SetPreviewImage(string path)
        {
            try
            {
                if (picPreview == null) return;
                picPreview.Image?.Dispose();
                picPreview.Image = null;

                byte[] bytes = File.ReadAllBytes(path);
                using (var ms = new MemoryStream(bytes))
                using (var img = Image.FromStream(ms))
                {
                    picPreview.Image = new Bitmap(img);
                }
            }
            catch
            {
                ClearPreview();
            }
        }

        private void ClearPreview()
        {
            if (picPreview == null) return;
            try { picPreview.Image?.Dispose(); } catch { }
            picPreview.Image = null;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (_kodeBarang <= 0)
            {
                MessageBox.Show("Kode barang belum diatur.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih File Lampiran (image/pdf)";
                ofd.Filter = "Image & PDF|*.jpg;*.jpeg;*.png;*.bmp;*.pdf|Gambar|*.jpg;*.jpeg;*.png;*.bmp|PDF|*.pdf";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var fi = new FileInfo(ofd.FileName);
                    if (fi.Length > AppConstants.MaxAttachmentFileSizeBytes)
                    {
                        MessageBox.Show($"File terlalu besar (max {AppConstants.MaxAttachmentFileSizeBytes / 1024 / 1024}MB).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string ext = Path.GetExtension(ofd.FileName).ToLowerInvariant();
                    if (!_allowedExts.Contains(ext))
                    {
                        MessageBox.Show("Tipe file tidak diizinkan. Hanya gambar (jpg/png/bmp) dan pdf.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool isImg = _imgExts.Contains(ext);
                    if (isImg)
                    {
                        // ImageValidator validates magic bytes + structure
                        if (!ImageValidator.IsValidImageFile(ofd.FileName, AppConstants.MaxAttachmentFileSizeBytes))
                        {
                            MessageBox.Show("File gambar tidak valid atau rusak.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string baseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Attachments", _kodeBarang.ToString());
                    Directory.CreateDirectory(baseFolder);

                    string safeName = Guid.NewGuid().ToString("N") + ext;
                    string destPath = Path.Combine(baseFolder, safeName);
                    File.Copy(ofd.FileName, destPath, false);

                    string fileType = isImg ? "Foto" : "Dokumen";
                    string keterangan = null;
                    if (txtKeterangan != null) keterangan = txtKeterangan.Text?.Trim();

                    using (var db = new AppDbContext())
                    {
                        db.AsetLampiran.Add(new AsetLampiran
                        {
                            TipeAset = _tipeAset,
                            RefId = _kodeBarang,
                            RefKode = _kodeBarang.ToString(),
                            FilePath = destPath,
                            OriginalFileName = Path.GetFileName(ofd.FileName),
                            FileType = fileType,
                            FileSize = fi.Length,
                            CreatedAt = DateTime.Now,
                            CreatedBy = AuthManager.CurrentUserId != 0 ? (int?)AuthManager.CurrentUserId : null,
                            Keterangan = keterangan
                        });
                        db.SaveChanges();
                    }

                    try { AuditHelper.Log("aset_lampiran", _kodeBarang.ToString(), "INSERT", (object)null, (object)new { file = safeName }, "Lampiran"); } catch { }

                    if (txtKeterangan != null) txtKeterangan.Clear();
                    LoadData();
                    MessageBox.Show("Lampiran berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Add lampiran error: " + ex.Message);
                    MessageBox.Show("Gagal menambahkan lampiran: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLihat_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();
            if (id == 0) return;
            try
            {
                using (var db = new AppDbContext())
                {
                    var lamp = db.AsetLampiran.Find(id);
                    if (lamp != null && File.Exists(lamp.FilePath))
                    {
                        var psi = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = lamp.FilePath,
                            UseShellExecute = true
                        };
                        System.Diagnostics.Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("File tidak ditemukan di disk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();
            if (id == 0) return;

            if (MessageBox.Show("Hapus lampiran ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
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

                try { AuditHelper.Log("aset_lampiran", id.ToString(), "DELETE", (object)null, (object)null, "Lampiran"); } catch { }
                ClearPreview();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgLampiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnLihat_Click(sender, e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ClearPreview();
            base.OnHandleDestroyed(e);
        }
    }
}
