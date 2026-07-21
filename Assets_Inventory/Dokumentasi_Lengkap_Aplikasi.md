# Dokumentasi Lengkap Aplikasi Inventaris Aset

> **Proyek:** Assets_Inventory (WinForms .NET Framework 4.8 + EF Core 3.1 / EF6 + MySQL Pomelo 3.2.4 + Krypton Toolkit)
> **Root:** `C:\Users\Sahrul\source\repos\Assets_Inventory\Assets_Inventory\`
> **Tanggal:** 21 Juli 2026
> **Cakupan:** 49 Forms, 36 UserControls, 80+ Models, 15+ Helpers, 22+ Views Database
> **Jenis:** Laporan Teknis PKL – Dokumentasi Exhaustif Implementasi

Dokumen ini adalah konsolidasi tunggal dari 7 fragmen audit fase sebelumnya (Inventory & Audit, Master Data, Aset Tetap, Transaksi, Laporan Dashboard Enterprise, Keamanan Tools Admin, EF Architecture). Setiap bagian ditulis deskriptif, teknis, dengan alur kerja end-to-end, keputusan desain, bug, dan optimasi. Dokumen ini dapat dipakai langsung sebagai BAB III / BAB IV Laporan PKL.

---

## Daftar Isi
- 1. Ringkasan Audit & Pembaruan
- 2. Bedah Detail Setiap Halaman/Form
  - 2.1 Autentikasi & Keamanan
  - 2.2 Master
  - 2.3 Inventaris Aset Tetap
  - 2.4 Tanah & Bangunan
  - 2.5 Barang Habis Pakai
  - 2.6 Pengadaan & Permintaan
  - 2.7 Peminjaman & Pengembalian
  - 2.8 Mutasi, Opname, NonAktif
  - 2.9 Laporan
  - 2.10 Tools & Admin
  - 2.11 Dashboard & Enterprise
- 3. Implementasi Pengolahan Data (Entity Framework)

---

## 1. Ringkasan Audit & Pembaruan

Audit dilakukan 3 sprint: Sprint 2 Background Merges, Sprint 4 Enterprise, Sprint Runtime Fix. Metode: review statis manual 80+ model, grep `Include` vs `join`, profiling SQL log `Database.Log`, uji lapangan dengan data produksi 12k aset + orphan FK akibat `SET FOREIGN_KEY_CHECKS=0` historis.

### 1.1 Bug Kritis (Keamanan & Integritas Data)

#### 1.1.1 RCE Command Injection via `cmd.exe /c` pada Backup & Restore
Sebelum fix, `BackupDbForm` memakai `ProcessStartInfo.FileName="cmd.exe"` dan `Arguments="/c mysqldump -h {host} -u {user} -p{pass} {db} > {file}"`. Host, user, nama DB, dan path folder berasal dari UI `txtHost`, `txtDatabase`, `txtBackupFolder` sehingga karakter `& | && ;` dieksekusi oleh cmd. Ini adalah RCE klasik CWE-78. `RestoreDbForm` memakai redirect `<` yang juga butuh shell. Perbaikan menghapus shell sepenuhnya: panggilan langsung `mysqldump.exe` dari `Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Resources","mysqldump.exe")` fallback `mysqldump` di PATH, `UseShellExecute=false`, `RedirectStandardOutput=true`, `RedirectStandardError=true`, password via environment `MYSQL_PWD` bukan argumen terlihat di task manager. Fungsi `EscapeArg(string)` membungkus kutip jika mengandung spasi ` " ' & | < > ^`. Path traversal dicek dengan `Path.GetFullPath(req).StartsWith(GetFullPath(allowed))`. Backup streaming via `StandardOutput.BaseStream.CopyTo(fileStream,81920)`, validasi file exists + `Length>0`. Restore memakai `RedirectStandardInput=true` dan `fileStream.CopyTo(process.StandardInput.BaseStream)`. Dengan ini shell tidak pernah di-invoke.

#### 1.1.2 Connection String Plaintext, LOAD DATA LOCAL INFILE, AllowUserVariables
`App.config` menyimpan `<add name="KoneksiServer" connectionString="Server=...;Uid=root;Pwd=..."/>` plaintext. Modul baru `Helper/ConnectionStringProtector.cs` mengimplementasi DPAPI `ProtectedData.Protect(Encoding.UTF8.GetBytes(cs), entropy, DataProtectionScope.CurrentUser)` dan Base64 + prefix `DPAPI:` untuk auto-deteksi. `Unprotect` cek prefix; jika tidak ada dianggap legacy plaintext agar tidak crash di client lama yang belum di-setup, dan jika DPAPI gagal return null → caller fallback plaintext dengan warning `Debug.WriteLine`. Lebih penting, MySQL CVE `LOAD DATA LOCAL INFILE` memungkinkan server jahat membaca file client. Fix: `DbConnectionStringBuilder` di `KoneksiDbForm` dan `AppDbContext.OnConfiguring` memaksa `b["AllowLoadLocalInfile"]=false`, `b["AllowUserVariables"]=false` serta `ConnectionStringProtector.BuildValidatedMySqlConnectionString()` yang memvalidasi host regex `^[a-zA-Z0-9_.-]+$`, identifier `^[a-zA-Z0-9_]+$`, port 1-65535, dan path tidak mengandung `< > | & ^`.

#### 1.1.3 Hash BCrypt Terekspos di DataGridView UserForm
`UserForm` awal `db.Pengguna.ToList()` → `DataSource = list`. Entity `Pengguna` mengandung `Password` hash BCrypt `$2a$12$...`. DataGridView menampilkan kolom Password; bisa dicopy, di-screenshot, atau terbaca via UI Automation. Perbaikan membuat DTO internal `class PenggunaDto{int Id; string Username,Nama,Role,Mapel,Kelas;}` tanpa Password. Query `db.Pengguna.AsNoTracking().Include(p=>p.IdPeranNavigation).Select(p=>new PenggunaDto{...}).ToList()`. Dengan ini hash tidak pernah masuk UI layer (least exposure principle). Hal serupa diterapkan di `AuthManager.GetAkses` yang mengembalikan copy `ModulAkses` untuk mencegah mutasi eksternal.

#### 1.1.4 RBAC Bypass via Hardcoded SetMode(true)
`GroupUserForm` dan `UserForm` memiliki method `SetMode(bool isEdit)` yang sebelum audit mengatur `btnTambah.Enabled=!isEdit` dan `btnSimpan.Enabled=true` hardcoded. Artinya user dengan hanya `HakBaca` tetap bisa menyimpan jika trigger `SetMode` via refleksi atau hotkey. `MasterDataForm` hub juga tidak cek hak awal. Audit: inject `_hakAkses = AuthManager.GetAkses("Manajemen Pengguna")` di Load, `SetMode` kini `btnTambah.Enabled = !isEdit && hak.HakBuat`, `btnUbah.Enabled = !isEdit && hak.HakUbah`, `btnHapus.Enabled = !isEdit && hak.HakHapus`, `btnSimpan.Enabled = isEdit && (isAdding?hak.HakBuat:hak.HakUbah)`. Guard tambahan di `btnSimpan_Click` early return jika tidak punya hak. `MasterDataForm` `Load` cek `AuthManager.GetAkses("Data Master").HakBaca` → jika false `MessageBox` + `Close()`. `MainForm` memperbaiki cache permission (lihat 1.2.6).

#### 1.1.5 ResetDbForm Tanpa Transaksi, Tanpa Whitelist – DDL Injection
Form reset awal mengambil nama tabel dari `CheckBox.Text` dan concat `"TRUNCATE TABLE "+tableName`. Identifier tidak bisa diparameterisasi, jadi ini DDL injection jika TextBox dimodifikasi. Lebih buruk, loop truncate tanpa transaksi: jika tabel ke-3 gagal FK, 2 tabel pertama sudah terhapus tidak bisa rollback → partial data loss. Perbaikan: whitelist `Type[] allowed = {typeof(Kategori),typeof(MasterBarang),typeof(Aset),...}` dari model. Dapatkan nama tabel resmi via `db.Model.FindEntityType(type).GetTableName()` kemudian validasi regex `^[a-zA-Z0-9_]+$`. Eksekusi dalam `using var tx = db.Database.BeginTransaction();` dengan `SET FOREIGN_KEY_CHECKS=0` sebelum loop, loop `db.Database.ExecuteSqlRaw($"TRUNCATE TABLE `{name}`")`, lalu `SET FOREIGN_KEY_CHECKS=1`, `tx.Commit()`. Catch: coba `SET FOREIGN_KEY_CHECKS=1` lagi dan `try{tx.Rollback()}catch{}` defensif jika connection sudah tertutup. Komentar `// ponytail: identifier tidak bisa diparameterize, whitelist Type[] + regex adalah ceiling`. Konfirmasi double: user harus ketik `HAPUS {kategori}` exact match + dialog Yes/No kedua.

#### 1.1.6 Path Traversal pada Upload Gambar, Logo, Lampiran
`InputBarangAsetForm` dan `ProfilLembagaForm` menyimpan dengan `fileName = Path.GetFileName(openFile.FileName)` tetapi tanpa sanitasi lagi, penyerang bisa memberi nama `..\..\evil.exe` di Windows Forms (walau GetFileName mencegah sebagian). Lebih berbahaya, path tujuan `Path.Combine(Resources, fileName)` bisa escape jika fileName berisi `..\`. Perbaikan: `GenerateSafeFileName` → `Guid.NewGuid().ToString("N")+Path.GetExtension(original).ToLower()`, validasi `Path.GetFileName(input)==input`, dan `Path.GetFullPath(dest).StartsWith(Path.GetFullPath(Resources))`. Validasi magic bytes: JPEG `FF D8 FF`, PNG `89 50 4E 47`, BMP `42 4D`, bukan hanya ekstensi, karena ekstensi bisa dipalsukan. `new Bitmap(path)` load test untuk memastikan bukan polyglot. Ukuran `FileInfo.Length` cek terhadap `AppConstants.MaxImage 10MB` dan `MaxAttach 20MB`. `ProfilLembagaForm` juga cek `GetFileName(LogoInstansi)==LogoInstansi` saat load backward compat.

#### 1.1.7 System.Resources.Extensions Binding Redirect DLL Hell
Runtime error `Could not load file or assembly 'System.Resources.Extensions, Version=4.0.0.0'` terjadi karena EPPlus 6.x dan ZXing bergantung pada `System.Resources.Extensions 8.0.0.0` (masuk .NET 8) tetapi `App.config` mere-direct ke 4.0.0.0. Ini DLL hell klasik. Fix: `<dependentAssembly><assemblyIdentity name="System.Resources.Extensions" ... /><bindingRedirect oldVersion="0.0.0.0-8.0.0.0" newVersion="8.0.0.0"/></dependentAssembly>`. Pastikan `packages.config` konsisten.

#### 1.1.8 DataBarangAsetUC Unknown, MutasiBarangForm Empty, AuditLog Crash Zero Date
Tiga bug lapangan: (1) `DataBarangAsetUC` menampilkan "Unknown" untuk NamaBarang karena orphan FK setelah `SET FOREIGN_KEY_CHECKS=0` historis → `Aset.IdMasterBarang` menunjuk id yang tidak ada. EF `Include` return null. Solusi hybrid dict fallback hanya untuk ID di page saat ini (lihat 1.2.2). (2) `MutasiBarangForm` grid kosong karena `loadData` tidak `Include(jurusan)` dan `CellFormatting` akses `IdJurusanAsalNavigation?.Nama` null → fallback tidak ada. Fix eager load + dict. (3) `AuditLogUC` crash `MySqlException: Unable to convert MySQL date/time value to System.DateTime` karena `audit_log.timestamp` berisi `0000-00-00 00:00:00` dari `sql_mode` loose lama. Model diubah `DateTime? Timestamp` nullable. Query filter `Where(a=>a.Timestamp.HasValue && a.Timestamp.Value>=start)`. Display `HasValue? ToString("dd/MM/yyyy HH:mm:ss") : "(null/0000-00-00)"`. Catch tambahkan actionable message sarankan `UPDATE audit_log SET timestamp=NOW() WHERE timestamp='0000-00-00'`.

#### 1.1.9 Self-Deletion User & Self Role Deletion
`UserForm` tidak mencegah `db.Pengguna.Remove` dimana `Id==AuthManager.CurrentUserId`. Setelah hapus, session masih memiliki userId yang sudah tidak ada → null ref di `AuthManager.IsAuthenticated`. `GroupUserForm` juga tidak cek apakah role sedang dipakai dirinya. Fix: `if(id==AuthManager.CurrentUserId){MessageBox("Tidak bisa hapus akun sendiri"); return;}` dan `if(db.Pengguna.Any(u=>u.IdPeran==id)){MessageBox("Peran masih dipakai"); return;}`. Penghapusan juga `try/catch DbUpdateException` lalu `Entry(e).Reload()`.

#### 1.1.10 Duplicate Kode Gudang Race Condition + BarangForm Dead Code
`MasterGudangForm` PK string `KodeGudang`. Generate via `OrderByDescending(KodeGudang).FirstOrDefault()` → `Substring(4)` → `int.TryParse` → `+1` format `GDG-{D3}`. Tanpa lock/sequence, dua instance concurrently dapat kode sama → duplicate PK di `SaveChanges` kedua. Ditandai `ponytail: race condition tanpa lock/transaction/DB sequence` dengan ceiling upgrade ke `SELECT ... FOR UPDATE` atau stored procedure jika throughput naik. Untuk single-instance desktop lock memory cukup. `BarangForm.cs` adalah dead code: ctor `BarangForm(int IdKategori)` param tidak dipakai, `Load` kosong, aman dihapus.

### 1.2 Optimasi Performa

#### 1.2.1 Paging 100 + AsNoTracking + Count + Skip/Take + CountAsync
Sebelumnya `DataBarangAsetUC`, `DataBarangHabisPakaiUC`, `PengadaanBarangUC` memanggil `db.Aset.ToList()` full 12k row + tracking snapshot + kolom `Gambar` longtext base64. Ini menyebabkan Large Object Heap 200MB+, GC Gen2, UI freeze 2-4 detik. Pola baru:
```csharp
_totalRecords = q.Count(); // SELECT COUNT(*)
var page = q.AsNoTracking()
  .OrderByDescending(x=>x.KodeBarang)
  .Skip(_currentPage*_pageSize) // OFFSET 0
  .Take(_pageSize)              // LIMIT 100
  .ToList();
```
`AsNoTracking()` menghemat 50-60% RAM karena tidak membuat `ChangeTracker.Entries`. `DashboardUC` menggunakan `CountAsync()`, `ToListAsync()`, `GroupBy().Select().ToListAsync()` agar tidak block UI thread. Tombol Prev/Next dibuat dinamis `new KryptonButton{Anchor = AnchorStyles.Top|Right}` dengan `ponytail: anchor relative to lblTotal`. Total pages `Math.Ceiling(total/(double)pageSize)`. ViewModel projection menghindari `Gambar` berat.

#### 1.2.2 N+1 Preload Dictionary Pattern + Filtered Dict
Banyak form melakukan akses navigasi di `CellFormatting` atau loop: `MasterRombelForm` `IdJurusanNavigation.NamaJurusan`, `MasterRuangForm` `Lemari -> Ruang`, `TransaksiPeminjamanForm` `Aset -> MasterBarang -> Kondisi`. Ini N+1 query tersembunyi (1 query per row). Solusi preload dictionary sekali sebelum binding:
```csharp
var ids = page.Select(a=>a.IdJurusan).Distinct().ToList();
var dict = db.Jurusan.AsNoTracking()
  .Where(j=>ids.Contains(j.Id))
  .ToDictionary(j=>j.Id, j=>j.Nama);
string jurusan = item.IdJurusanNavigation?.Nama ?? dict.GetValueOrDefault(item.IdJurusan) ?? "Unknown";
```
Di `InputPengadaanBarangForm`, `dictBarang`, `dictKategori`, `dictPemasok`, `dictJurusan`, `dictPengguna` di-preload sekali. Untuk orphan FK, hybrid `Include` + fallback dict: coba `nav?.Nama` dulu, jika null coba dict, jika masih null placeholder "Unknown". Ini defense-in-depth. Filtered dict berarti hanya ID di halaman saat ini (<=100) yang di-query, bukan seluruh tabel 10k.

#### 1.2.3 Streaming I/O vs Buffered Shell + MaxBackup Guard
Backup sebelumnya `ReadToEnd()` seluruh dump 500MB ke string lalu `File.WriteAllText` → OOM. Perbaikan streaming: `process.StandardOutput.BaseStream.CopyTo(fileStream,81920)` dan `StandardError.ReadToEnd()` terpisah. Restore streaming `FileStream.CopyTo(StandardInput.BaseStream)`. `FileInfo.Length` cek terhadap `AppConstants.MaxBackup = 500MB` early reject. `EscapeArg` juga menghindari argumen pecah spasi.

#### 1.2.4 Remove Application.DoEvents() pada Import Master
`MasterKategoriForm` import loop lama memanggil `Application.DoEvents()` per row agar UI responsive. Ini berbahaya karena memungkinkan re-entrancy: user bisa klik Simpan lagi saat loop berjalan. Diganti dengan `this.Enabled=false; Cursor=WaitCursor;` selama batch dan `AddRange + SaveChanges` sekali. `ponytail: single instance desktop` comment.

#### 1.2.5 Filtered Dictionary & Projection ViewModel Hemat Kolom Berat
`DataBarangAsetUC` sebelumnya `db.MasterBarang.ToDictionary()` full table 10k row untuk mapping 100 row halaman. Perbaikan filtered ID di halaman saat ini. Juga projection `AsetViewModel` minimal kolom: `KodeBarang, KodeInventaris, NamaBarang, Kategori, Jurusan, Ruang, Kondisi, Status` tanpa `Gambar`. Gambar base64 hanya di-load saat edit `Find(KodeBarang)` tracked.

#### 1.2.6 Permission Cache Sekali per MainForm
`MainForm` sebelumnya `Load` memanggil `AuthManager.GetAkses(modul)` 30×, masing-masing join `PeranAkses × Akses` filter `CurrentRoleId`. Perbaikan cache sekali:
```csharp
var all = AuthManager.GetAllAkses(); // Dictionary<string,ModulAkses> copy
bool canRead(string modul) => all.TryGetValue(modul, out var a) ? a.HakBaca : AuthManager.GetAkses(modul).HakBaca;
```
Dari 30 query menjadi 1. Fallback `GetAkses` jika tidak di cache untuk modul baru.

#### 1.2.7 Dashboard Async & GroupBy Server-Side
`DashboardUC` lama synchronous `Count()`, `ToList()` 6× → freeze 3 detik. Baru `async void LoadDataAsync()` dengan `await db.Aset.CountAsync()`, `await db.Aset.GroupBy(a=>a.IdKondisiNavigation.NamaKondisi).Select(g=>new{g.Key,Count=g.Count()}).ToListAsync()`, dan `InvokeRequired` check sebelum `chart.Series.Clear()`. `GroupBy` tetap diterjemahkan ke SQL `GROUP BY` oleh Pomelo.

#### 1.2.8 SortableBindingList Native over Library
`DataGridView` tidak bisa sort jika `DataSource=List<T>`. Dibuat `Helper/SortableBindingList.cs` inherit `BindingList<T>` override `ApplySortCore` dengan `PropertyDescriptor` + `List.Sort` via `Comparison<T>`. Ini Native, tidak butuh library, memenuhi ladder ponytail rung 3.

#### 1.2.9 Lain-lain: Dispose Pattern di ChangeView, Find Recursive
`MainForm.ChangeView` sebelumnya `pnlContent.Controls.Clear()` tanpa dispose → leak `DbContext` di UC. Fix `foreach(Control c in pnlContent.Controls) try{c.Dispose()}catch{}`. `BaseLaporanUC.FindControlRecursive<DataGridView>()` untuk generic print tanpa hardcode.

### 1.3 Fitur Baru Enterprise (Sprint 4)

#### 1.3.1 AuditLog & AuditHelper Non-Blocking
Tabel baru `audit_log` (`Models/AuditLog.cs`) `Id bigint PK, Timestamp datetime? nullable, Username varchar(100), Aksi varchar(20) (CREATE,UPDATE,DELETE,APPROVE,REJECT,EXPORT,LOGIN), Modul varchar(50), RefId varchar(100), OldValue longtext json, NewValue longtext json, IpAddress varchar(45)`. Konfigurasi di `AppDbContext.OnModelCreating`: `modelBuilder.Entity<AuditLog>().ToTable("audit_log")`. `Helper/AuditHelper.cs` `Log(modul,refId,aksi,oldObj,newObj)` serialize dengan `JsonConvert.SerializeObject(obj, new JsonSerializerSettings{MaxDepth=3, ReferenceLoopHandling=Ignore})` untuk mencegah stack overflow pada cyclic nav `Aset -> MasterBarang -> DetailPengadaan -> MasterBarang`. Method wrap `try{using(var db2=new AppDbContext()){db2.AuditLog.Add(entry); db2.SaveChanges();}}catch(Exception ex){Debug.WriteLine(ex)}` sehingga gagal audit tidak membatalkan transaksi bisnis (non-blocking). `AuditLogUC` menampilkan paging 100, filter tanggal, search modul/aksi, penanganan zero date.

#### 1.3.2 Notifikasi & NotificationService Polling
Tabel `notifikasi` (`Notifikasi.cs`) `Id, Judul, Pesan, IsRead bool, CreatedAt datetime, UserId int? target, Tipe (Approval, StokMin, JatuhTempo, Info)`. `Helper/NotificationService.cs` polling 30 detik `Task.Delay(30000)` loop `AsNoTracking().Where(n=>(n.UserId==CurrentUserId||n.UserId==null) && !n.IsRead).OrderByDescending(CreatedAt).Take(50)`. Event: saat `PersetujuanForm` approve/reject → insert notifikasi ke peminta; saat `DataBarangHabisPakaiUC` stok < threshold → broadcast ke gudang role; saat `LaporanJatuhTempoPeminjamanUC` load → cek overdue insert notifikasi. `NotifikasiUC` menampilkan list dengan warna belum dibaca bold, klik tandai baca, badge di `MainForm` `lblNotif.Text = count>0 ? count.ToString() : ""`.

#### 1.3.3 AsetLampiran & AsetLampiranUC – Attachment Generik
Tabel `aset_lampiran` (`AsetLampiran.cs`) `Id, TipeAset enum string (Aset/Tanah/Bangunan/HabisPakai), RefId string / RefKode int, FilePath varchar(500), OriginalFileName varchar(255), FileType varchar(100), FileSize bigint, CreatedAt datetime, CreatedBy int FK Pengguna, Keterangan`. File disimpan di `Lampiran\{Tipe}\{Year}\{GUID}{ext}` di bawah `AppDomain.BaseDirectory`. Validasi `ImageValidator.IsValidImageFile` jika tipe image, plus `AppConstants.MaxAttach 20MB`. Path traversal cek sama seperti logo. `AsetLampiranUC` tampil grid lampiran per aset (filter `TipeAset`+`RefId`), preview image via `PictureBox` + `new Bitmap`, download via `SaveFileDialog` + `File.Copy`, hapus via `File.Delete` + `db.Remove`. Relasi tidak FK cascade fisik, logic soft.

#### 1.3.4 ApprovalWorkflowConfig & ApprovalStep Multi-Level
Dua tabel baru: `approval_workflow_config` (`ApprovalWorkflowConfig`) `Id, WorkflowType varchar(50) (Permintaan,PermintaanHp,Pengadaan,...), Level int, NamaLevel varchar(100), IdPeranApprover int FK Peran, IsRequired bool, Urutan int`. `approval_step` (`ApprovalStep`) `Id, WorkflowType, RefId string (KodePermintaan), Level int, IdApprover int? FK Pengguna, Status enum (Pending,Approved,Rejected), Catatan text, TanggalKeputusan datetime?`. `Helper/ApprovalWorkflowHelper.cs` menyediakan `HasConfig(db,wType)` → `Any()`, `EnsureSteps(db,wType,refId)` → jika `approval_step` belum ada, load configs order Level, buat step per config dengan Status Pending, `TryMultiLevelApprove(db,refId,wType,currentUserId,catatan)` → cari step Pending order Level, prefer step dimana `IdPeranApprover==CurrentRoleId`, set Approved, log audit, lalu `IsAllRequiredApproved(db,refId,wType)` → cek semua required step Approved. `PersetujuanForm` di-upgrade: `Load` → `TryInitMultiLevel(wType,refId)` tampilkan `lblMultiInfo.Text = "Menunggu Level X (Kepala Lab)..."`. `Setuju` → `TryMultiLevelApprove`; jika `IsAllRequiredApproved` false → early return "menunggu level lain", jika true → `Permintaan.Status=Disetujui`. Fallback ke single-level jika tidak ada config. `ApprovalWorkflowConfigForm` dan `ApprovalStepForm` CRUD config dan monitoring step.

#### 1.3.5 QR Code & Barcode Label Cetak Multi-Kolom
Menggunakan `ZXing.Net` `BarcodeWriter` dan helper `QrCodeHelper` custom. Opsi `cmbJenisBarcode` Code128, Code39, QR. Fungsi lokal `MakeBarcode(string kode)` → jika jenis QR → `QrCodeHelper.GenerateQrCode(kode, 200,200)`, else `BarcodeWriter{Format=CODE_128}.Write(kode)` (Code39 fallback ke 128 jika karakter tidak support, ditandai `ponytail`). `PrintDocument` multi-kolom: hitung `int cols = Math.Max(1, e.MarginBounds.Width / (itemWidth+spacing))`, `itemWidth = barcodeWidth+20`, `x = Margin.Left + (i%cols)*itemWidth`, `y = Margin.Top + (i/cols)*rowHeight`, wrap baris baru jika `x+itemWidth > MarginBounds.Right`. Paging `HasMorePages = currentIndex < total`. Preview via `PrintPreviewDialog`. Cocok untuk label aset.

#### 1.3.6 BaseLaporanUC & ExportHelper EPPlus Styled
Abstract `UserControls/BaseLaporanUC.cs` untuk dedup 18 laporan UC: field `AppDbContext db=new`, `PrintDocument _printDoc`, `_pengaturan = db.Pengaturan.FirstOrDefault()`, abstract `LoadDataInternal()` dan `SetupPrintColumns()`, header/footer instansi: logo dari `Pengaturan.LogoInstansi` resolve `Resources/logo` fallback, judul `_judulLaporan`, filter `_filterInfo`, garis `e.Graphics.DrawLine(Pens.Black, ...)`, footer `Dicetak: dd/MM/yyyy HH:mm oleh username`, TTD `Kota, dd MMMM yyyy` + `Mengetahui, Kepala Sek...` + `Bagian Inventaris`. `FindControlRecursive<DataGridView>()` untuk generic print. `PrintDataGridView` header `LightGray`, `colWidth = Margin.Width / visibleCols`, max 30 rows per halaman, truncate 25 char untuk cegah overflow. Export: `ExportToExcel` dulu placeholder CSV, kini `Helper/ExportHelper.cs` memakai EPPlus `ExcelPackage.LicenseContext=NonCommercial`, `SaveFileDialog`, merge `A1:I1` title bold 14 center, header bold LightGray border thin, loop rows, `AutoFitColumns()`, `AuditHelper.Log("laporan", file, "EXPORT", ...)`. RBAC `HakExport` dicek, fallback `HakBaca` jika tidak punya Export tetapi ada Baca (untuk kompatibel lama).

#### 1.3.7 AppConstants & ImageValidator Sentralisasi
`Helper/AppConstants.cs` sentralisasi magic: `BCRYPT_WORK_FACTOR=12`, `STATUS_AKTIF="Aktif"`, `STATUS_NONAKTIF="NonAktif"`, `PEMINJAMAN_STATUS_DIPINJAM="Sedang Dipinjam"`, `PERMINTAAN_STATUS_MENUNGGU="Menunggu"`, `MaxImage 10MB`, `MaxAttach 20MB`, `MaxBackup 500MB`, `SessionTimeout 8h`, `MaxFailed 5`, `Lockout 15m`, `STOK_MINIMAL_THRESHOLD=5`. `ImageValidator` punya `AllowedExtensions = {".jpg",".jpeg",".png",".bmp"}`, `MagicBytes` dict, `IsValidImageFile(path)` cek exists, size, ext, magic `FileStream.Read(0,8)`, dan `new Bitmap(path)` load test. `GenerateSafeFileName(ext)` GUID.

#### 1.3.8 Kartu Inventaris Ruangan (KIR), Rekap Nilai Aset, Penyusutan
Tiga laporan compliance aset negara: KIR join 9 tabel `Aset + MasterBarang + Kategori + Satuan + Kondisi + Ruang + Lokasi + DetailPengadaan + Pengadaan + SumberPerolehan` where `Status != Nonaktif` untuk cetak per ruangan sesuai Permendagri. Rekap Nilai Aset DB-level `Count()+Sum(decimal?)` 6 bucket Barang/Bangunan/Tanah x Aktif/NonAktif `db.Aset.Where(...).Sum(a=>(decimal?)(a.HargaSatuan??0m))`. Print `REKAP NILAI ASET` + row `TOTAL KESELURUHAN`. Penyusutan straight-line sesuai PSAK: `umurAset = Floor((tglHitung - tglRegistrasi)/365.25)`, `susutPerTahun = (harga - residu)/umurEkonomi`, `tahunSusut = Min(umurAset, umurEkonomi)`, `akumulasi = susutPerTahun * tahunSusut`, `nilaiBuku = Max(harga - akumulasi, residu)`. Source `VAset join Aset where UmurEkonomi>0`.

#### 1.3.9 DbTableControl & Fitur Enterprise Lain
`UserControls/DbTableControl.cs` wrapper CheckBox satu tabel, property `NamaModul` ↔ `cbModul.Text`, `IsChecked`. Dipakai di `BackupDbForm` panel. `Helper/ConnectionStringProtector` validator `IsSafeHost`, `IsSafeIdentifier`, `IsSafeTable`, `IsSafePath`, `ValidateBackupFolder`, `ValidateTableNames`. `Helper/DatabaseHelper` auto backup tanpa shell (sudah dijelaskan). `Helper/QrCodeHelper` wrapper ZXing.

---

## 2. Bedah Detail Setiap Halaman/Form

### 2.1 Modul Autentikasi & Keamanan

#### 2.1.1 LoginForm.cs
- **File:** `Forms/LoginForm.cs`
- **Deskripsi:** Gerbang autentikasi, satu-satunya entry point tanpa session. Bisnis: verifikasi identitas terhadap `Pengguna` + `Peran`, memulai session `AuthManager`.
- **Alur:** `Load` → clear `txtPassword`, focus `txtUsername`. `btnLogin_Click` → validasi kosong → cek lockout dict `static Dictionary<string,(int Count,DateTime LockUntil)> _failedAttempts` lock `_failLock`. Threshold 5, lock 15 menit (single instance desktop `ponytail`). Query `FirstOrDefault(u=>Username.ToLower()==input.ToLower())`. Jika null → increment fail, pesan generik `Login gagal: username atau password salah.` (anti-enumeration). `BCrypt.Verify` + `PasswordNeedsRehash(...,12)` rehash 10→12 otomatis on success. Jika sukses → `Settings.userId=Id`, `SetUserSession(userId,roleId,username)` set `LoginTime=Now`, load permissions join `PeranAkses×Akses`. Buka `MainForm`, hide Login. `FormClosed` → `ClearSession()` + `Close()`.
- **Fitur spesifik:** Brute-force lockout in-memory, BCrypt upgrade path, constant-time message, session fixation prevention, `Debug.WriteLine` logging, `BCRYPT_WORK_FACTOR` dari AppConstants, `IsAuthenticated` cek `CurrentUserId==0` false dan `TotalHours>=8` expired.

#### 2.1.2 MainForm.cs
- **File:** `Forms/MainForm.cs`
- **Deskripsi:** Shell MDI dengan `pnlContent` host UC, `menuStrip`, `statusStrip`, wallpaper, timer session 8 jam, notifikasi badge.
- **Alur:** `Load` → `GetAllAkses()` sekali cache dictionary, lambda `canRead(modul)` cek cache fallback `GetAkses`. Atur `Visible` setiap menu item. Fix bug duplicate `inpiutBangunanToolStripMenuItem` via `menuStrip1.Items.Find("dataBarangAsetToolStripMenuItem")`. Wallpaper: `Path.GetFileName==WallpaperAplikasi`, magic bytes via `new Bitmap`, GUID save, 10MB limit. `ChangeView(UC)` → `foreach(c in pnlContent.Controls) try c.Dispose()` sebelum Clear → cegah leak DbContext. `FormClosing` → backup failure hanya `Debug.WriteLine` tidak block, `ClearSession()`, timer stop. `CekNotifikasiAset()` dipanggil dari `AsetPerluDilengkapiUC`.
- **Fitur:** Permission caching 30→1 query, menu routing, wallpaper aman, dispose pattern, session cleanup, `OpenForms.OfType<MainForm>().FirstOrDefault()` pattern untuk navigasi dari child.

#### 2.1.3 AboutForm.cs
- **File:** `Forms/AboutForm.cs`
- **Deskripsi:** Informasi aplikasi versi, instansi, developer.
- **Alur:** `Load` RBAC `GetAkses("About").HakBaca` → jika false `Close()`. Tampilkan `Version` dari `Assembly.GetExecutingAssembly().GetName().Version`, `Pengaturan.NamaInstansi`, logo. Tombol tutup.
- **Fitur:** RBAC read gate minimal.

### 2.2 Modul Master Data

Pola umum semua master: field `AppDbContext db = new AppDbContext()` long-lived, `SetMode("View"|"Insert"|"Update")` toggle Enabled, `BindingSource`, `loadDgv()` `ToLower().Contains`, `SortableBindingList`, `dg_CellClick` `Find(id)` → `bindingSource.DataSource=entity`, simpan cek `Id==0` → new + Add else update manual → `SaveChanges()` sync, hapus `Remove+SaveChanges` try `DbUpdateException` → `Reload()` pesan FK, tidak ada `AsNoTracking`, paging, transaction eksplisit, duplicate check, validasi panjang.

#### 2.2.1 MasterDataForm.cs
- **File:** `Forms/MasterDataForm.cs`
- **Deskripsi:** Hub launcher 12 tombol master. RBAC gate `GetAkses("Data Master")`.
- **Alur:** Load cek HakBaca → Close jika tidak. Tiap `btnX_Click` → `new MasterXForm().ShowDialog()` modal → mencegah concurrency edit.
- **Fitur:** Zero query, modal routing.

#### 2.2.2 MasterBarangForm.cs
- **File:** `Forms/MasterBarangForm.cs`
- **Deskripsi:** Katalog induk aset `JenisBarang="Aset"` FK Kategori, optional Merek Satuan. Dipakai autocomplete InputBarangAsetForm dan pengadaan.
- **Alur:** `Load` async `await db.Kategori.ToListAsync()` → `cmbKategori.DataSource`, `loadData()` async `Include(IdKategoriNavigation).Include(IdMerekNavigation).Include(IdSatuanNavigation).Where(Nama.Contains||Kategori.Contains).ToListAsync()` over-fetch 3 nav padahal grid hanya butuh kategori. Validasi nama wajib, kategori SelectedIndex != -1. Simpan `Id==0` new hardcoded `JenisBarang="Aset"` else update 3 field, `SaveChanges()` sync dalam `async void`. Hapus FK handling. Import ExcelDataReader `UseHeaderRow`, `File.Open Read`, row `[nama, IdKategori TryParse, ket]` batch `AddRange+SaveChanges` 1×, hitung sukses/gagal, `IOException` file terkunci, `Enabled=false+WaitCursor`. Kelemahan tidak validasi eksistensi IdKategori. Export EPPlus LicenseContext NonCommercial SaveFileDialog Include Kategori ToList tanpa filter header bold gray AutoFit. CellFormatting `IdKategoriNavigation?.NamaKategori`.
- **Fitur:** Import/Export EPPlus, async load, autocomplete source.

#### 2.2.3 MasterKategoriForm.cs
- **File:** `Forms/MasterKategoriForm.cs`
- **Deskripsi:** Lookup kategori untuk MasterBarang, Rombel parent.
- **Alur:** Search Nama Contains, Validasi Nama wajib, Import 2 kolom `[Nama, Keterangan]` cek `!IsNullOrEmpty`, `DoEvents` per row legacy, Add per row SaveChanges akhir, tidak duplicate check.
- **Fitur:** Template standar master.

#### 2.2.4 MasterMerekForm.cs
- **File:** `Forms/MasterMerekForm.cs`
- **Deskripsi:** Merk aset.
- **Alur:** Identik Kategori field `NamaMerek,Keterangan`, search nama, import 2 kolom.
- **Fitur:** Template.

#### 2.2.5 MasterSatuanForm.cs
- **File:** `Forms/MasterSatuanForm.cs`
- **Deskripsi:** Satuan unit.
- **Alur:** Field `KodeSatuan ToUpper, NamaSatuan, Keterangan`, validasi nama+kode wajib, search nama/kode, import 3 kolom `[Nama,Kode ToUpper,Keterangan]`.
- **Fitur:** Kode ToUpper normalization.

#### 2.2.6 MasterLokasiForm.cs
- **File:** `Forms/MasterLokasiForm.cs`
- **Deskripsi:** Lokasi fisik aset.
- **Alur:** Field `KodeLokasi ToUpper,NamaLokasi,Keterangan`, validasi, search, import 3 kolom.
- **Fitur:** Sama satuan.

#### 2.2.7 MasterRuangForm.cs – Composite Ruang + Lemari
- **File:** `Forms/MasterRuangForm.cs`
- **Deskripsi:** Dual entity 1 form 2 tab, 2 BindingSource, 1 SetMode coupling. Ruang fisik + Lemari di dalam ruang.
- **Alur:** Ruang: `NamaRuang,KodeRuang ToUpper,Lantai,Keterangan,IsActive cbAktif`. Search nama/kode, import 5 kolom `[Nama,Kode,Lantai,Ket,IsActive="aktif"]` parsing case-insensitive. Lemari: FK `IdRuang`, `loadDgvLemari` setiap load re-assign `cmbRuang.DataSource=db.Ruang.ToList()` query ulang tanpa AsNoTracking, search nama/kode, CRUD butuh `SelectedItem!=null`, tidak Import. Bug: `dg2_CellFormatting` akses `IdRuangNavigation?.NamaRuang` tapi tidak Include → null/N+1. IsActive display `Aktif/Tidak Aktif` via CellFormatting.
- **Fitur:** Composite pattern, IsActive.

#### 2.2.8 MasterGudangForm.cs
- **File:** `Forms/MasterGudangForm.cs`
- **Deskripsi:** Gudang penyimpanan dengan PK string.
- **Alur:** PK `KodeGudang` string, auto-generate jika empty|"0" → `OrderByDescending(Kode).First() Substring(4) TryParse +1 → GDG-{D3}` race condition tanpa lock `ponytail`. Find string PK, search nama/kode, tidak Import/Export.
- **Fitur:** String PK generator.

#### 2.2.9 MasterSupplierForm.cs (Entity Pemasok)
- **File:** `Forms/MasterSupplierForm.cs`
- **Deskripsi:** Supplier / pemasok barang pengadaan.
- **Alur:** Field `NamaPemasok,NomorTelepon,Alamat,Keterangan`, validasi nama,alamat,kontak wajib terpisah, KeyPress filter `IsDigit|.`. Bug search duplicate `NomorTelepon.Contains` 2×, Alamat tidak ke-search copy-paste bug. Tidak Import/Export.
- **Fitur:** Kontak validation.

#### 2.2.10 MasterSumberPerolehanForm.cs
- **File:** `Forms/MasterSumberPerolehanForm.cs`
- **Deskripsi:** Sumber dana perolehan (APBN, Hibah, dll).
- **Alur:** Field `KodeSumber ToUpper,NamaSumber,Keterangan,IsActive`, search nama/kode, validasi pesan copy-paste salah "Nama ruang", import 4 kolom `[Nama,Kode ToUpper,Ket,IsActive="aktif"]`, CellFormatting IsActive.
- **Fitur:** IsActive.

#### 2.2.11 MasterKondisiForm.cs
- **File:** `Forms/MasterKondisiForm.cs`
- **Deskripsi:** Kondisi aset (Baik,Rusak Ringan, Rusak Berat).
- **Alur:** Field `NamaKondisi,Keterangan`, template standar, import 2 kolom, dipakai `Aset.IdKondisi` untuk opname.
- **Fitur:** Lookup opname.

#### 2.2.12 MasterTahunAjaranForm.cs
- **File:** `Forms/MasterTahunAjaranForm.cs`
- **Deskripsi:** Tahun ajaran untuk grouping permintaan/pengadaan.
- **Alur:** Field `TahunAjaran1,Semester,TanggalMulai?,TanggalSelesai?,IsActive`, `dg_CellClick` set `dtMulai= TanggalMulai??Now`, validasi tahun+semester wajib, import kompleks `row[2]/[3]` bisa DateTime object atau string → `is DateTime else TryParse`, nullable DBNull check, try/catch per row → gagal++. Kolom Tahun, Semester ToUpper, Mulai, Selesai, IsActive.
- **Fitur:** Date nullable handling.

#### 2.2.13 MasterRombelForm.cs
- **File:** `Forms/MasterRombelForm.cs`
- **Deskripsi:** Rombongan belajar (kelas).
- **Alur:** Field `NamaRombel,KodeRombel ToUpper,Tingkat ToUpper,IdJurusan FK,IsActive`, lookup `Jurusan.ToList()` → `cmbJurusan`, search nama/kode, validasi 4 field, N+1: `loadDgv` tanpa Include Jurusan tapi CellFormatting akses `IdJurusanNavigation.NamaJurusan` → rawan null/N+1. Import 5 kolom `[Nama,Kode,Tingkat ToUpper,IdJurusan TryParse,IsActive]` validasi id>0.
- **Fitur:** FK Jurusan.

#### 2.2.14 MasterNonAktifForm.cs (Entity StatusBarang)
- **File:** `Forms/MasterNonAktifForm.cs`
- **Deskripsi:** Status non-aktif generic, nama file menyesatkan.
- **Alur:** Field `NamaStatus,Keterangan`, template standar + Import 2 kolom, dipakai `NonAktifBarangForm`.
- **Fitur:** Lookup status.

#### 2.2.15 BarangForm.cs – Dead Code
- **File:** `Forms/BarangForm.cs`
- **Deskripsi:** Stub tertinggal setelah digantikan MasterBarangForm. Constructor `BarangForm(int IdKategori)` param tidak dipakai. Load, loadData, CellDoubleClick, btnTambah, txtSearch_TextChanged kosong.
- **Alur:** Tidak dipakai routing, aman dihapus deletion over addition.
- **Fitur:** None.

### 2.3 Modul Inventaris Aset Tetap

#### 2.3.1 DataBarangAsetUC.cs
- **File:** `UserControls/DataBarangAsetUC.cs`
- **Deskripsi:** UC utama CRUD aset tetap dengan RBAC `AuthManager.GetAkses("Data Aset")`, search, paging 100, export CSV, import CSV/Excel, cetak barcode/QR.
- **Alur:** `Load` cek HakBaca → init combo Jumlah (1/Semua), JenisBarcode (Code128/39/QR) → buat tombol Prev/Next dinamis `ponytail: anchor relative to lblTotal` → `loadData(resetPage=true)` → click grid → `bindingSource.DataSource=db.Aset.Find(KodeBarang)`. Paging: `_pageSize=100,_currentPage,_totalRecords`, query `Include(IdMasterBarangNavigation).Include(IdJurusanNavigation).Include(IdRuangNavigation).AsNoTracking()`, Count dulu, Skip/Take. Fallback dict hybrid: ambil `masterIds/jurusanIds/ruangIds` dari page saat ini, `Where(id in ...).ToDictionary()`, resolve `nav?.Nama ?? dict[id] ?? "Unknown"` defense-in-depth untuk orphan FK `FK_CHECKS=0`. Search `KodeInventaris.Contains||NamaBarang.Contains||NoSeri.Contains` null-check Pomelo 3.2.4. Import `ImportCsv` `File.ReadAllLines` split `,` TryParse IdMasterBarang dan `ImportExcel` via `ExcelReaderFactory.CreateReader` + `AsDataSet()` loop row1 GUID20 untuk `KodeBarang2`. Barcode `ZXing BarcodeWriter` + `QrCodeHelper`, local `MakeBarcode`, PrintDocument multi-kolom `itemWidth=lebar+20` wrap `HasMorePages`, PreviewDialog. Export CSV `StringBuilder` + `EscapeCsv`. Sortable `SortableBindingList<AsetViewModel>`.
- **Fitur:** Paging, hybrid fallback, barcode, import, RBAC.

#### 2.3.2 InputBarangAsetForm.cs
- **File:** `Forms/InputBarangAsetForm.cs`
- **Deskripsi:** Form modal tambah/ubah manual 1 aset, auto-generate kode, handle MasterBarang find-or-create transaksi, upload gambar base64.
- **Alur:** `Load` → `SetupComboBoxes` Jurusan/Ruang/Lokasi 100 limit → `SetupAutoComplete` 100 NamaBarang → hook `cmbRuang.SelectedIndexChanged` load lemari per ruang enable/disable `cmbLemari`+`txtNomorRak`. Jika selectedAset null Tambah else Ubah + `LoadDataExisting`. Auto-generate kode + custom prefix: `Pengaturan.KustomPrefix` else `INV` format `$"{prefix}-{tahun}-MNL-{MMddHHmmss}"` tahun Now.Year. Image base64: OpenFileDialog jpg/jpeg/png/bmp → `Image.FromFile` → resize max 600×600 ratio `Min(ratioX,ratioY)` → `Graphics.DrawImage` → `Save(MemoryStream,Jpeg)` → `Convert.ToBase64String` simpan `Aset.Gambar` text column. Load `FromBase64String` try/catch, Hapus set "". TryParse validasi `UmurEkonomi` int.TryParse, `NilaiResidu` decimal.TryParse MessageBox Warning early return `ponytail: validate TryParse return before use`. Transaction `using(var tx=db.Database.BeginTransaction())` → `FirstOrDefault(m=>Nama.ToLower()==input.ToLower())` → jika null Add Master SaveChanges dapat ID → Add Aset `IdMaster=finalId`, `KodeBarang2=GUID20`, `TanggalRegistrasi=Now` Commit rollback. KodeBarang2 GUID 20 char upper untuk barcode.
- **Fitur:** Base64 image resize, transaction master+aset, kode custom prefix.

#### 2.3.3 KelengkapanAsetForm.cs
- **File:** `Forms/KelengkapanAsetForm.cs`
- **Deskripsi:** Form batch enrichment aset baru dari pengadaan. Grid kiri `dgAset` daftar kode inventaris dibuat, form kanan detail: NoSeri, UmurEkonomi, NilaiResidu, Ruang/Lemari/Rak, Lokasi, Status, Keterangan, Gambar.
- **Alur:** ctor `List<Aset>` → Load re-query `db.Aset.Where(KodeBarang in list)` → hydrate `IdMasterBarangNavigation` jika null via `Find` → `LoadGrid` anon projection → `TampilkanDetailAset(kode)` pertama. CellClick tampil detail. Simpan per-item update `Find` → SaveChanges → refresh list. `btnSelesai` cek incomplete `NoSeri==null && IdRuang==null && Gambar==null` → confirm dialog bisa dilengkapi nanti via Master Aset.
- **Fitur:** Batch enrichment flow dari PengadaanBarangUC.

#### 2.3.4 AsetPerluDilengkapiUC.cs
- **File:** `UserControls/AsetPerluDilengkapiUC.cs`
- **Deskripsi:** Dashboard widget deteksi aset belum lengkap.
- **Alur:** Query `db.Aset.Include(MasterBarang,Jurusan).Where(NoSeri empty || IdRuang null || IdLokasi null || Gambar empty).Select(KodeInventaris,NamaBarang,Status,Jurusan,KekuranganData=GetKekurangan())`. GetKekurangan list join. Klik Edit → buka `KelengkapanAsetForm` single item → after OK refresh + `parentForm.CekNotifikasiAset()`. `Load` async count.
- **Fitur:** Notifikasi incomplete data.

### 2.4 Modul Tanah & Bangunan

#### 2.4.1 InputTanahForm.cs
- **File:** `Forms/InputTanahForm.cs`
- **Deskripsi:** CRUD aset tanah: pemilik, luas, sertifikat, status hak, nilai aset.
- **Alur:** Model `AsetTanah {KodeTanah PK, NamaPemilik, IdLokasi FK, LuasTanah decimal, LetakTanah, NomorSertifikat unique, StatusHak enum (Hak Milik/Hak Pakai/HGB), NilaiAset decimal, Penggunaan, TanggalPerolehan, SumberPerolehan enum (Beli/Hibah/Sumbangan/Lainnya), Status string}`. Form Load → load `Lokasi` combo 100 limit, setup date, mode Tambah/Ubah. Validasi: NamaPemilik wajib, Luas TryParse >0, NomorSertifikat wajib unique check `Any(n=>n==input && id!=current)`, Nilai TryParse, StatusHak combo required. Simpan `BeginTransaction` jika perlu, `SaveChanges`. Relasi ke `TanahNonAktif` saat nonaktifkan. Alur serupa InputBarangAsetForm tapi tanpa gambar base64 kompleks (opsional lampiran via AsetLampiranUC).
- **Fitur:** Validasi sertifikat unique, enum status hak.

#### 2.4.2 InputBangunanForm.cs
- **File:** `Forms/InputBangunanForm.cs`
- **Deskripsi:** CRUD aset bangunan: luas, konstruksi, ukuran, nilai.
- **Alur:** Model `AsetBangunan {KodeBangunan PK, NamaBangunan, LuasBangunan, StatusBangunan (Milik Sendiri/Sewa/Lainnya), NilaiAset, Keterangan, TanggalBangunan, IdKondisi FK, UkuranP/L decimal, Konstruksi string, Status}`. Load combo Kondisi, setup numeric. Validasi Nama wajib, Luas TryParse, Nilai TryParse, Ukuran P/L TryParse, IdKondisi required, StatusBangunan combo. Simpan SaveChanges. Ukuran `P x L` display ` $"{P} x {L}"`. Relasi `BangunanNonAktif`. Gambar optional via lampiran.
- **Fitur:** Ukuran dimensi, konstruksi.

### 2.5 Modul Barang Habis Pakai

#### 2.5.1 DataBarangHabisPakaiUC.cs
- **File:** `UserControls/DataBarangHabisPakaiUC.cs`
- **Deskripsi:** List & manajemen stok habis pakai, mirip DataBarangAsetUC.
- **Alur:** Paging 100 + fallback dict MasterBarang+Ruang alasan FK_CHECKS orphan sama. ViewModel `KodeBarang,NamaBarang,StokAwal,Aktual,Ruang,Status,Jurusan`. Query `AsetHabisPakai.Include(Master).Include(Ruang).AsNoTracking()` Count + Skip/Take. Search `Kode/Nama`. Import CSV/Excel + barcode? Export CSV. RBAC `Data Habis Pakai`. Click → InputBarangHabisPakaiForm edit. `using(var db=new AppDbContext())` per load agar context tidak accumulate.
- **Fitur:** Paging, dict fallback, stok display.

#### 2.5.2 DataBarangHabisPakaiKeluarUC.cs
- **File:** `UserControls/DataBarangHabisPakaiKeluarUC.cs`
- **Deskripsi:** Histori pengeluaran barang habis pakai (BarangKeluar).
- **Alur:** Model `BarangKeluar {Id,KodeBarang FK AsetHabisPakai string, JumlahKeluar, TanggalKeluar, Penerima, Keperluan, CreatedBy}` + view `VBarangKeluar`. UC Load → filter tanggal, search penerima/nama barang, paging. Query `VBarangKeluar.AsNoTracking()` Count Skip/Take. Dict fallback untuk nama barang. Tombol Export CSV. Klik detail buka `InputBarangHabisPakaiKeluarForm` read-only? Tombol hapus jika `HakHapus` dan stok bisa rollback (tambah kembali StokAktual) dalam transaksi.
- **Fitur:** Stok decrement log, rollback.

#### 2.5.3 InputBarangHabisPakaiForm.cs
- **File:** `Forms/InputBarangHabisPakaiForm.cs`
- **Deskripsi:** Tambah stok baru habis pakai.
- **Alur:** Model `AsetHabisPakai {KodeBarang string PK format KB-HP-..., IdMasterBarang FK, Stok awal, StokAktual, IdJurusan/Ruang/Lokasi, Status Tersedia/Habis, TanggalRegistrasi, Keterangan, IsReturnable bool}`. Form Load combo MasterBarang Jenis=Habis Pakai (filter), Jurusan/Ruang/Lokasi 100 limit, autocomplete nama. Kode generate `KB-HP-{Year}-{D3}`? Saat tambah manual format `KB-HP-{yyyy}-{next}` via `OrderByDescending`. Validasi Nama wajib via dict `TryGetValue` suggestion, Stok int TryParse >0, Jurusan required. Simpan `StokAktual=Stok`. Transaction. IsReturnable checkbox untuk barang bisa dikembalikan.
- **Fitur:** Kode format HP, IsReturnable.

#### 2.5.4 InputBarangHabisPakaiKeluarForm.cs
- **File:** `Forms/InputBarangHabisPakaiKeluarForm.cs`
- **Deskripsi:** Form pengeluaran stok.
- **Alur:** Pilih barang via combo `AsetHabisPakai` stock >0, tampilkan stok aktual label, input JumlahKeluar TryParse, cek `JumlahKeluar <= StokAktual` else warning, Penerima wajib, Keperluan wajib, TanggalKeluar default Now. Simpan dalam `BeginTransaction`: `AsetHabisPakai.StokAktual -= jumlah`, `Status = StokAktual==0?HABIS:Tersedia`, `BarangKeluar` insert, SaveChanges Commit. Jika IsReturnable false, tidak bisa dikembalikan (validasi di pengembalian). Audit log.
- **Fitur:** Stok decrement atomic, validation.

### 2.6 Modul Transaksi Pengadaan & Permintaan

Arsitektur: `Permintaan (PRM/PRH) --PersetujuanForm+ApprovalWorkflowHelper--> Disetujui +--PengadaanBarang (PGD/PHP) picker Disetujui belum link --btnProses JalankanProsesBelanja--> Aset INV-* / AsetHabisPakai KB-HP-*`. File inti: `InputPermintaanBarangForm`, `InputPermintaanBarangHabisPakaiForm`, `InputPengadaanBarangForm`, `InputPengadaanBarangHabisPakaiForm`, `DetailPermintaanForm`, `DetailPengadaanForm`, `PersetujuanForm`, `ApprovalWorkflowHelper`, `PengadaanBarangUC`, `PermintaanBarangUC`, dll.

#### 2.6.1 InputPermintaanBarangForm.cs
- **File:** `Forms/InputPermintaanBarangForm.cs`
- **Deskripsi:** Entry permintaan aset tetap. Keranjang `BindingList<DetailPermintaan>`.
- **Alur:** Kode gen `PRM-{Year}-{D3}` via `OrderByDescending(KodePermintaan)` parse suffix. Load `cmbJurusan` dari `Pengguna.IdJurusanNavigation`, `loadTextBox` autocomplete barang `{NamaBarang} ({Kategori})` dict `dictBarang[name->Id]` + TA dict. `btnTambah` Add to BindingList validasi `dictBarang.TryGetValue` harus pilih suggestion, Jumlah>0 TryParse, Alasan wajib. `btnSimpan`: new `Permintaan{Status=Menunggu, DetailPermintaan=listDetail.ToList()}` SaveChanges. Validasi: `cmbJurusan != -1`, `dictTahunAjaran.TryGetValue`, `listDetail.Count>0`, Keterangan wajib. `isDetailMode` set ReadOnly hide HapusColumn btnSimpan invisible. Status enum `Menunggu|Disetujui|Ditolak`. Include `IdMasterBarangNavigation` saat load existing. Trigger Insert Permintaan + DetailPermintaan cascade via navigation.
- **Fitur:** Keranjang BindingList, autocomplete dict, kode PRM.

#### 2.6.2 InputPermintaanBarangHabisPakaiForm.cs
- **File:** `Forms/InputPermintaanBarangHabisPakaiForm.cs`
- **Deskripsi:** Mirror untuk HP.
- **Alur:** Identik (2.6.1) tapi model `PermintaanHp / DetailPermintaanHp`, kode `PRH-{Year}-{D3}`, key `KodePermintaanHp`. Tidak ada IsReturnable check di sini (divalidasi di keluar).
- **Fitur:** PRH code.

#### 2.6.3 DetailPermintaanForm.cs
- **File:** `Forms/DetailPermintaanForm.cs`
- **Deskripsi:** Read-only grid detail permintaan.
- **Alur:** `Where(KodePermintaan==selected)` select `NamaBarang?,JumlahDiminta,AlasanKebutuhan`. Validasi `selected!=0` else close. Load `AsNoTracking`. Tidak ada CRUD, hanya view.
- **Fitur:** Read-only.

#### 2.6.4 DetailPengadaanForm.cs
- **File:** `Forms/DetailPengadaanForm.cs`
- **Deskripsi:** Read-only detail pengadaan.
- **Alur:** `DetailPengadaan.AsNoTracking() Where IdPengadaan` select `NamaBarang,NamaPemasok,HargaSatuan C2,JumlahMasuk`. Validasi `selectedPengadaanId!=0` else close. Harga format currency.
- **Fitur:** Read-only.

#### 2.6.5 PersetujuanForm.cs – Multi-Level Approval
- **File:** `Forms/PersetujuanForm.cs`
- **Deskripsi:** Setuju/Tolak permintaan aset/HP. Dual-mode `isHabisPakai`.
- **Alur:** `Load` → check DetailPermintaan count>0 else Cancel → `TryInitMultiLevel(wType=Permintaan|PermintaanHp, refId)` → `ApprovalWorkflowHelper.HasConfig(db,wType) || fallback Permintaan untuk Hp` → `EnsureSteps(db,wType,refId)` buat `ApprovalStep` per config Level jika belum ada → pending Steps display di `lblMultiInfo`. Setuju → `TryMultiLevelApprove(refId,wType,catatan)` cari step Pending order Level prefer `IdPeranApprover==CurrentRoleId` → Status Approved IdApprover TanggalKeputusan Now Audit Log APPROVE → `IsAllRequiredApproved?` jika belum all Approved early return "menunggu level lain". Jika no multi config → direct `Permintaan.Status=Disetujui` + Audit Log. Tolak serupa step Rejected + Permintaan Ditolak. Validasi `txtAlasan` wajib setuju & tolak, Detail count >0. Status `Menunggu->Disetujui|Ditolak` final + intermediate `ApprovalStep Pending|Approved|Rejected` per Level. Trigger Audit `AuditHelper.Log("permintaan[_hp]",kode,"APPROVE/REJECT",old,new)`.
- **Fitur:** Multi-level workflow, fallback single-level, audit.

#### 2.6.6 InputPengadaanBarangForm.cs
- **File:** `Forms/InputPengadaanBarangForm.cs`
- **Deskripsi:** Buat bon pengadaan aset. ViewModel `DetailPengadaanViewModel {IdMasterBarang,NamaBarang,Kategori,JumlahMasuk,HargaSatuan,TotalHarga,IdPemasok,KodePermintaanAsal,IdJurusanTarget,StatusTelahDibelanjakan} + PermintaanSimpleViewModel{Pilih bool}`.
- **Alur:** `LoadDaftarPermintaan`: `Permintaan Where Status=Disetujui && !PengadaanPermintaan.Any()` N+1 fix `dictJurusan,dictPengguna` preload `SortableBindingList` ke `dgPermintaan` CheckBox Pilih. `btnPilihPermintaan`: commit edit collect checked → `selectedPermintaanList` → `GenerateDetailBon`: load `DetailPermintaan Where Kode in selected` group by `{IdMasterBarang,KodePermintaan,IdJurusan}` sum Jumlah `dictBarang,dictKategori` preload. `btnTerapkanPemasok`: txtPemasok autocomplete + upsert Pemasok find case-insensitive else insert SaveChanges Apply ke selected rows atau semua. `btnSimpan`: Validasi + `BeginTransaction()` → Insert `Pengadaan{Status=Menunggu Proses}` → `DetailPengadaan` per item → `PengadaanPermintaan` link → SaveChanges ×2 → Commit/Rollback catch inner try/catch rollback. Edit mode: `RemoveRange(oldDetails)+RemoveRange(oldLinks)` lalu re-insert. Validasi: Gudang & Sumber wajib, TA valid via dict, keranjang non-empty, `HargaSatuan>0` all, `IdPemasok!=null` all. Status enum `Menunggu Proses|Sedang Dibelanjakan|Selesai Dibelanjakan`. N+1 dictBarang,dictKategori,dictPemasok,dictJurusan,dictPengguna preload AsNoTracking. Kode gen `PGD-{Year}-{nextId:D3}` nextId=max+1. Trigger `Pengadaan,DetailPengadaan,PengadaanPermintaan`, auto-create Pemasok jika tidak ada.
- **Fitur:** Picker permintaan Disetujui, group by, upsert pemasok, transaction 3 SaveChanges.

#### 2.6.7 InputPengadaanBarangHabisPakaiForm.cs
- **File:** `Forms/InputPengadaanBarangHabisPakaiForm.cs`
- **Deskripsi:** Mirror §2.6.6 untuk HP.
- **Alur:** `PengadaanHabisPakai,DetailPengadaanHp,PengadaanPermintaanHp`. Pemasok via `cmbPemasok` ComboBox bukan free-text upsert, status list `Menunggu Proses|Dibelanjakan|Selesai`, kode `PHP-{Year}-{D3}`. Transaction pattern sama `BeginTransaction/Commit/Rollback`.
- **Fitur:** PHP code, combo pemasok.

#### 2.6.8 PengadaanBarangUC.cs
- **File:** `UserControls/PengadaanBarangUC.cs`
- **Deskripsi:** List pengadaan aset filter status, proses belanja jadi aset.
- **Alur:** `loadData()`: Dispose+new context, `Where(Id contains cari)` + status filter combo, `dictSumber/Gudang/TA` preload map `PengadaanViewModel`. `btnProses` → `JalankanProsesBelanja(id)`: Get `DetailPengadaan Where IdPengadaan && Status==false/null` jika 0 set Pengadaan.Status Selesai. Dialog pilih item ComboBox `ComboItemVM` plus opsi SEMUA SISA. Generate Aset per unit: loop JumlahMasuk times kode `INV-{TahunPgd}-{IdPgd:D3}-{UrutanBarang:D3}-{Total:D3}-{Unit:D3}` dari `Pengaturan.KustomPrefix` fallback INV. Harga dari detail, IdJurusan dari permintaan pertama linked, Status Di Gudang, `KodeBarang2=GUID 20char`. Set `detail.Status=true`. Hitung sisa unpurchased → update Pengadaan status `Sedang Dibelanjakan` atau `Selesai Dibelanjakan`. Trigger `KelengkapanAsetForm` untuk NoSeri/Gambar/Ruang. Validasi: Status != Selesai untuk ubah, Menunggu Proses only untuk hapus, `DbUpdateException` handled relasi. N+1 dict preload Export barcode collect KodeInventaris dari Aset.
- **Fitur:** Per-unit expansion aset, kode INV complex, Kelengkapan trigger.

#### 2.6.9 DataPengadaaanBarangHabisPakaiUC.cs
- **File:** `UserControls/DataPengadaaanBarangHabisPakaiUC.cs`
- **Deskripsi:** List pengadaan HP + proses jadi stok.
- **Alur:** Mirip 2.6.8, `JalankanProsesBelanja` lebih simple: `AsetHabisPakai{Kode=KB-HP-{Year}-{Pgd:D3}-{Counter:D3},Stok=JumlahMasuk,StokAktual=JumlahMasuk,Status=Tersedia,IsReturnable=false}`. No per-unit expansion, 1 row per detail. Status update. Export.
- **Fitur:** Simple 1-to-1.

#### 2.6.10 PermintaanBarangUC.cs
- **File:** `UserControls/PermintaanBarangUC.cs`
- **Deskripsi:** List permintaan aset tetap dengan filter status.
- **Alur:** `loadData()` `db.Permintaan.Include(Detail).Include(Jurusan).AsNoTracking()` filter `Status` combo Menunggu/Disetujui/Ditolak, search Kode/Jurusan, paging? Count Skip/Take. Dict preload `dictJurusan,dictPengguna`. `btnTambah` → `InputPermintaanBarangForm` Tambah, `btnUbah` → detail mode, `btnHapus` only if Menunggu. `btnDetail` → `DetailPermintaanForm`. RBAC `Permintaan`. Audit log export? 
- **Fitur:** Status filter, detail navigation.

#### 2.6.11 DataPermintaanBarangHabisPakaiUC.cs
- **File:** `UserControls/DataPermintaanBarangHabisPakaiUC.cs`
- **Deskripsi:** List permintaan HP.
- **Alur:** Mirror PermintaanBarangUC untuk `PermintaanHp`, kode PRH, status filter, dict preload, detail form `DetailPermintaan` versi HP. RBAC.
- **Fitur:** PRH list.

### 2.7 Modul Peminjaman & Pengembalian

#### 2.7.1 TransaksiPeminjamanForm.cs
- **File:** `Forms/TransaksiPeminjamanForm.cs`
- **Deskripsi:** Form peminjaman aset tetap.
- **Alur:** Model `Peminjaman {Id,NomorPeminjaman format PMJ-{Year}-{D3},TanggalPinjam,IdPeminjam/Pengguna?,NamaPeminjam,NomorTelepon,LamaPinjamHari int,TanggalJatuhTempo nullable,Status enum Sedang Dipinjam/Dikembalikan/Overdue}` + `DetailPeminjaman {IdPeminjaman, KodeBarang FK Aset, Jumlah=1}`. Load combo Jurusan, TahunAjaran, Barang tersedia `Status != Dipinjam && !=NonAktif` Include MasterBarang Kondisi. Validasi: `dictBarang`? `KodeBarang` TryGetValue suggestion, LamaPinjam TryParse >0, NamaPeminjam wajib, Telepon optional digit filter, `listDetail.Count>0`, `TanggalJatuhTempo = Pinjam + LamaHari`. Kode gen `PMJ-{Year}-{D3}`. Simpan `BeginTransaction()` dengan `saveDb` baru untuk transaksi agar context read tidak kotor: insert `Peminjaman`, insert `DetailPeminjaman` per aset, update `Aset.Status="Dipinjam"`? atau via query status. SaveChanges Commit. N+1 fix: chain `KodeBarang -> Aset -> MasterBarang -> Kondisi` dengan 3 dict terpisah, bukan nested FirstOrDefault loop. Trigger Notifikasi jatuh tempo.
- **Fitur:** Lama pinjam → jatuh tempo auto, multi-aset keranjang, transaksi new context.

#### 2.7.2 TransaksiPengembalianForm.cs
- **File:** `Forms/TransaksiPengembalianForm.cs`
- **Deskripsi:** Pengembalian peminjaman.
- **Alur:** Pilih Nomor Peminjaman via autocomplete `Where Status= Sedang Dipinjam`, load `DetailPeminjaman` join Aset Master, grid toggle Kembali checkbox. Input TanggalKembali default Now, hitung telat `TelatHari = (Kembali - JatuhTempo).Days` if >0. Validasi minimal satu dicentang, TanggalKembali >= TanggalPinjam. Simpan `BeginTransaction`: insert `Pengembalian {Id,NomorPeminjaman,TanggalKembali,TelatHari,Keterangan,Penerima}`, update `Peminjaman.Status=Dikembalikan` jika semua dikembalikan else tetap Dipinjam partially? logic all-or-nothing: jika semua detail kembali → Dikembalikan else tetap. Update `Aset.Status="Tersedia"` per aset kembali. SaveChanges Commit. Audit log.
- **Fitur:** Telat calc, partial return handling.

#### 2.7.3 PeminjamanBarangUC.cs
- **File:** `UserControls/PeminjamanBarangUC.cs`
- **Deskripsi:** List peminjaman aktif/history.
- **Alur:** `loadData()` `db.Peminjaman.AsNoTracking()` + `V`? atau `Include(Detail.Aset.Master)` filter status, search Nomor/NamaPeminjam, paging Count Skip/Take. Tombol Tambah → TransaksiPeminjamanForm, Ubah? maybe extend, Detail → show DetailPeminjaman grid. Export CSV. RBAC `Peminjaman`.
- **Fitur:** Paging, status.

#### 2.7.4 PengembalianBarangUC.cs
- **File:** `UserControls/PengembalianBarangUC.cs`
- **Deskripsi:** History pengembalian.
- **Alur:** Query `Pengembalian` + `V`, filter tanggal, search Nomor/Nama, paging. Detail → list aset dikembalikan + telat. Export.
- **Fitur:** History.

### 2.8 Modul Mutasi, Opname, NonAktif

#### 2.8.1 MutasiBarangForm.cs
- **File:** `Forms/MutasiBarangForm.cs`
- **Deskripsi:** Mutasi aset antar Jurusan/Ruang, perlu approve Toolman? 
- **Alur:** Model `Mutasi {Id,TanggalMutasi,KodeBarang FK Aset, IdJurusanAsal,Tujuan, AlasanMutasi, Status?}`. Form Load combo Jurusan Asal/Tujuan `db.Jurusan.ToList()`, combo Aset filter tersedia, tanggal default Now. Validasi: Asal != Tujuan, Alasan wajib, Aset dipilih, Aset Status Aktif. Search `txtCari` filter Kode/Nama. loadData `Include` dulu + `ToDictionary` hanya untuk missing nav hybrid fallback karena N+1 sebelumnya. Simpan: jika `IdPeran == Toolman`? perlu approve? Logic: `if AuthManager Is Toolman need Approve`? Insert Mutasi, update `Aset.IdJurusan = Tujuan`, `IdRuang = baru` optional, SaveChanges dalam transaction, Audit Log. Setelah mutasi, notifikasi ke Jurusan tujuan.
- **Fitur:** Hybrid Include+dict, jurusan validation, audit.

#### 2.8.2 OpnameBarangForm.cs
- **File:** `Forms/OpnameBarangForm.cs`
- **Deskripsi:** Stock opname / pemeriksaan fisik aset, update kondisi.
- **Alur:** Model `OpnameAset {Id,TanggalOpname,KodeBarang FK, KondisiTerkini IdKondisi FK, Keterangan, Petugas}` + view `VOpnameAset`. Form pilih aset via autocomplete, combo Kondisi load `db.Kondisi.ToList()`, tanggal Now, Keterangan wajib jika kondisi Rusak. Simpan: insert OpnameAset, update `Aset.IdKondisi = KondisiTerkini`, SaveChanges. Load history DataGrid `VOpnameAset Where KodeBarang`. Validasi Kondisi required. Trigger laporan `LaporanProsesOpnameUC`.
- **Fitur:** Kondisi update, history.

#### 2.8.3 ProsesNonAktifForm.cs
- **File:** `Forms/ProsesNonAktifForm.cs`
- **Deskripsi:** Hub launcher nonaktif barang/tanah/bangunan.
- **Alur:** RBAC `GetAkses("NonAktif")`. Tiga tombol → `new NonAktifBarangForm().ShowDialog()`, `NonAktifTanahForm`, `NonAktifBangunanForm`. Tidak ada query DB, routing. Modal prevents concurrency.
- **Fitur:** Hub.

#### 2.8.4 NonAktifBarangForm.cs
- **File:** `Forms/NonAktifBarangForm.cs`
- **Deskripsi:** Decommission aset tetap.
- **Alur:** Model `BarangNonAktif {Id,KodeBarang FK Aset, TanggalNonaktif, Penyebab, Keterangan, Jumlah=1}` + view `VBarangNonAktif`. Form pilih Aset Aktif `Where Status!=NonAktif` via autocomplete dict, tanggal default Now, Penyebab wajib (Rusak Total, Hilang, dll), Keterangan. Simpan `BeginTransaction`: insert `BarangNonAktif`, update `Aset.Status="NonAktif"`, SaveChanges Commit. Validasi Aset tidak sedang Dipinjam. Laporan filter by `TanggalNonaktif >= awal && <= akhir.AddDays(1).AddTicks(-1)`. Audit.
- **Fitur:** Transaction decommission.

#### 2.8.5 NonAktifTanahForm.cs
- **File:** `Forms/NonAktifTanahForm.cs`
- **Deskripsi:** Decommission tanah.
- **Alur:** Model `TanahNonAktif {Id,KodeTanah FK, TanggalNonaktif, Penyebab, Keterangan}`. Mirip barang, pilih `AsetTanah` Aktif, penyebab wajib, insert + update `AsetTanah.Status="NonAktif"`. Validasi sertifikat.
- **Fitur:** Tanah nonaktif.

#### 2.8.6 NonAktifBangunanForm.cs
- **File:** `Forms/NonAktifBangunanForm.cs`
- **Deskripsi:** Decommission bangunan.
- **Alur:** Model `BangunanNonAktif {Id,KodeBangunan FK, TanggalNonaktif, Penyebab, Keterangan}`. Sama pattern: pilih bangunan aktif, insert, update status NonAktif. 
- **Fitur:** Bangunan nonaktif.

### 2.9 Modul Laporan

#### 2.9.0 LaporanForm.cs – Hub 19 Link
- **File:** `Forms/LaporanForm.cs`
- **Deskripsi:** Modal hub 19 link menuju semua Laporan UC. Single responsibility routing.
- **Alur:** RBAC `AuthManager.GetAkses("Laporan").HakBaca` fallback close. `parent = Application.OpenForms.OfType<MainForm>().FirstOrDefault()` + `parent.ChangeView(new XXXUC())` + `Close()` pattern. Mapping 19 UC. Tidak ada query DB.
- **Fitur:** Routing hub.

#### 2.9.1 BaseLaporanUC.cs – Abstract Template
- **File:** `UserControls/BaseLaporanUC.cs`
- **Deskripsi:** Template abstract dedup laporan: `AppDbContext db`, `PrintDocument _printDoc`, `_pengaturan = db.Pengaturan.FirstOrDefault()`, abstract `LoadDataInternal()` + `SetupPrintColumns()`, print header/footer logo instansi, footer `Dicetak: dd/MM HH:mm oleh username`, TTD, `FindControlRecursive<DataGridView>()`, `PrintDataGridView` header LightGray colWidth Margin/visibleCols max 30 rows per halaman truncate 25 char, Export `ExportToExcel` → `SaveFileDialog` → `ExportToCsv` ponytail placeholder EPPlus, `EscapeCsv`, `NavigateToDashboard()` via MainForm, `Dispose()` db+printDoc.
- **Fitur:** Template Method, header/footer, generic print.

#### 2.9.2 LaporanBarangInventarisUC.cs
- **File:** `UserControls/LaporanBarangInventarisUC.cs`
- **Deskripsi:** Laporan barang aktif.
- **Alur:** Source `VAset` join `Aset` untuk `GambarBase64`. ViewModel `KodeInventaris,NamaBarang,Kategori,Lokasi,Ruang,Kondisi,TanggalRegistrasi,HargaSatuan C2,Status,GambarBase64`. Filter combo Kategori/MasterBarang/Lokasi/Ruang/Kondisi insert `-- Semua --` Id=0 recreate context tiap tampil. Binding `SortableBindingList`, Harga C2, Tanggal dd MMM yyyy, hide GambarBase64. Print Landscape 50 margin logo instansi resolve `Resources/logo` fallback `..\..\Resources`, header uppercase double line `LAPORAN BARANG INVENTARIS TAHUN {Year}`, table header `#8FBC8F` `chkIncludeGambar` optional kolom Photo base64 decode `Image.FromStream(MemoryStream)` pagination `currentPrintIndex + HasMorePages` bottom margin 120 guard. Export `ExportHelper.ShowSaveDialog + ExportDataGridView + AuditHelper.Log EXPORT` RBAC `HakExport` fallback `HakBaca`. Close `ChangeView(DashboardUC)+ShowDialog LaporanForm`.
- **Fitur:** Filter multi combo, optional gambar print.

#### 2.9.3 LaporanTanahInventarisUC.cs
- **File:** `UserControls/LaporanTanahInventarisUC.cs`
- **Deskripsi:** Laporan tanah aktif/nonaktif.
- **Alur:** Filter `rbSemua/rbStatus` combo Aktif/Nonaktif `VAsetTanah`. VM `KodeTanah,NamaPemilik,NomorSertifikat,LuasTanah,LetakTanah,NamaLokasi,StatusHak,NilaiAset C2,Penggunaan,TanggalPerolehan dd-MM-yyyy,SumberPerolehan,Status`. Print `LAPORAN TANAH INVENTARIS` 10 kolom custom widths.
- **Fitur:** Status filter.

#### 2.9.4 LaporanBangunanInventarisUC.cs
- **File:** `UserControls/LaporanBangunanInventarisUC.cs`
- **Deskripsi:** Laporan bangunan.
- **Alur:** Filter `rbKondisi/cmbKondisi` + `rbStatus/cmbStatus Aktif|Nonaktif`, `VAsetBangunan`, Ukuran `P x L N1`. VM luas konstruksi status bangunan nilai aset tgl bangunan kondisi status. Print 11 kolom.
- **Fitur:** Kondisi+status filter.

#### 2.9.5 LaporanBarangNonAktifUC.cs
- **File:** `UserControls/LaporanBarangNonAktifUC.cs`
- **Deskripsi:** Decommission barang.
- **Alur:** Date range `dtAwal=1st month dtAkhir=Now` `Where TanggalNonaktif >= awal && <= akhir.AddDays(1).AddTicks(-1)`, combo via `Find()` nama. `VBarangNonAktif` VM `IdBarangNonAktif,TanggalNonaktif,KodeInventaris,NamaBarang,Kategori,Lokasi,Ruang,Jumlah,Penyebab,Keterangan` print subtitle `Periode: dd MMM yyyy s.d`.
- **Fitur:** Periode filter.

#### 2.9.6 LaporanBangunanNonAktifUC.cs
- **File:** `UserControls/LaporanBangunanNonAktifUC.cs`
- **Deskripsi:** Bangunan nonaktif.
- **Alur:** `VBangunanNonAktif` VM `IdBangunanNonAktif,KodeBangunan?,NamaBangunan,LuasBangunan?,TanggalNonaktif,Penyebab,Keterangan` 7 kolom. Date range same pattern.
- **Fitur:** Periode.

#### 2.9.7 LaporanTanahNonAktifUC.cs
- **File:** `UserControls/LaporanTanahNonAktifUC.cs`
- **Deskripsi:** Tanah nonaktif.
- **Alur:** `VTanahNonAktif` VM `IdTanahNonAktif,KodeTanah?,NomorSertifikat,NamaPemilik,LuasTanah?,TanggalNonaktif,Penyebab,Keterangan`. Date range.
- **Fitur:** Periode.

#### 2.9.8 LaporanPeminjamanBarangUC.cs
- **File:** `UserControls/LaporanPeminjamanBarangUC.cs`
- **Deskripsi:** History peminjaman.
- **Alur:** `VLaporanPeminjaman` date `TanggalPinjam`, filter status `Sedang Dipinjam|Dikembalikan` + barang. VM `NomorPeminjaman,TanggalPinjam,NamaPeminjam,NomorTelepon,LamaPinjamHari,JatuhTempo dd-MM-yyyy,KodeInventaris,NamaBarang,StatusPeminjaman`. Print `LAPORAN PEMINJAMAN BARANG`.
- **Fitur:** Status filter.

#### 2.9.9 LaporanPengembalianBarangUC.cs
- **File:** `UserControls/LaporanPengembalianBarangUC.cs`
- **Deskripsi:** History pengembalian.
- **Alur:** `VLaporanPengembalian` filter `TanggalKembali` VM `IdPengembalian,NomorPeminjaman,NamaPeminjam,TanggalPinjam,BatasWaktu,TanggalKembali,TelatHari`. Designer generic `button1-4,dateTimePicker1-2,comboBox1,dataGridView1`.
- **Fitur:** Telat hari.

#### 2.9.10 LaporanJatuhTempoPeminjamanUC.cs
- **File:** `UserControls/LaporanJatuhTempoPeminjamanUC.cs`
- **Deskripsi:** Overdue otomatis.
- **Alur:** Query `Status= Sedang Dipinjam && TanggalJatuhTempo != null && <= dtTanggal` Calc `Keterlambatan=(tglSekarang-jatuhTempo).TotalDays` order desc `dtTanggal.ValueChanged=>LoadData()` Highlight lewat.
- **Fitur:** Auto overdue calc.

#### 2.9.11 LaporanMutasiBarangUC.cs
- **File:** `UserControls/LaporanMutasiBarangUC.cs`
- **Deskripsi:** Histori mutasi.
- **Alur:** `VMutasi` filter JurusanAsal/Tujuan via `Jurusan.ToList()` VM `IdMutasi,TanggalMutasi,KodeInventaris,NamaBarang,JurusanAsal,JurusanTujuan,AlasanMutasi`.
- **Fitur:** Jurusan filter.

#### 2.9.12 LaporanPengadaanBarangUC.cs
- **File:** `UserControls/LaporanPengadaanBarangUC.cs`
- **Deskripsi:** Laporan pengadaan aset.
- **Alur:** `VPengadaanAset` filter tgl kategori nama barang (lokasi/ruang ignore karena view global) VM `IdPengadaan,Tanggal,NamaBarang,Kategori,Sumber,Pemasok,Jumlah,HargaSatuan C2,SubTotal,Keterangan`. Print footer `TOTAL PENGADAAN KESELURUHAN` sum SubTotal.
- **Fitur:** Total sum footer.

#### 2.9.13 LaporanPermintaanUC.cs
- **File:** `UserControls/LaporanPermintaanUC.cs`
- **Deskripsi:** Laporan permintaan.
- **Alur:** `VLaporanPermintaan` filter status Menunggu/Disetujui/Ditolak VM `KodePermintaan,TanggalPermintaan,NamaJurusan,NamaPeminta,NamaBarang,JumlahDiminta,AlasanKebutuhan,StatusPersetujuan`.
- **Fitur:** Status filter.

#### 2.9.14 LaporanKerusakanBarangUC.cs
- **File:** `UserControls/LaporanKerusakanBarangUC.cs`
- **Deskripsi:** Laporan kerusakan.
- **Alur:** `VLaporanKerusakan` status filter `Menunggu Pemeriksaan|Sedang Diperbaiki|Selesai|Tidak Bisa Diperbaiki` VM `IdKerusakan,TanggalLapor,KodeInventaris,NamaBarang,NamaPelapor,DeskripsiKerusakan,TingkatKerusakan,StatusKerusakan`.
- **Fitur:** Tingkat kerusakan.

#### 2.9.15 LaporanPerbaikanBarangUC.cs
- **File:** `UserControls/LaporanPerbaikanBarangUC.cs`
- **Deskripsi:** Laporan perbaikan.
- **Alur:** `VLaporanPerbaikan` VM `IdPerbaikan,TanggalPerbaikan,KodeInventaris,NamaBarang,DeskripsiKerusakan,Teknisi,TindakanPerbaikan,BiayaPerbaikan C2` print footer total biaya sum.
- **Fitur:** Biaya sum.

#### 2.9.16 LaporanProsesOpnameUC.cs
- **File:** `UserControls/LaporanProsesOpnameUC.cs`
- **Deskripsi:** Hasil opname.
- **Alur:** `VOpnameAset` filter nama barang + ruang VM `IdOpnameAset,TanggalOpname,KodeInventaris,NamaBarang,NamaRuang,KondisiTerkini,Keterangan`.
- **Fitur:** Kondisi filter.

#### 2.9.17 LaporanPenyusutanNilaiBarangUC.cs
- **File:** `UserControls/LaporanPenyusutanNilaiBarangUC.cs`
- **Deskripsi:** Penyusutan straight-line.
- **Alur:** Formula `umurAset=floor((tglHitung-tglRegistrasi)/365.25)`, `susut/thn=(harga-residu)/umurEkonomi`, `tahunSusut=Min(usia,umurEkonomi)`, `akumulasi=susut*tahun`, `nilaiBuku=Max(harga-akum,residu)`. Source `VAset join Aset where UmurEkonomi>0` VM + `AkumulasiPenyusutan,NilaiBuku`.
- **Fitur:** Formula PSAK.

#### 2.9.18 RekapNilaiAsetUC.cs
- **File:** `UserControls/RekapNilaiAsetUC.cs`
- **Deskripsi:** Rekap nilai total.
- **Alur:** DB-level `Count()+Sum(decimal?)` 6 bucket Barang/Bangunan/Tanah x Aktif/NonAktif `Status!=Nonaktif` `Sum(a=>(decimal?)(HargaSatuan??0m))`. Print `REKAP NILAI ASET` + row `TOTAL KESELURUHAN`. `lblRecord` total C2. Export via ExportHelper.
- **Fitur:** Aggregation DB-level.

#### 2.9.19 KartuInventarisRuanganUC.cs (KIR)
- **File:** `UserControls/KartuInventarisRuanganUC.cs`
- **Deskripsi:** KIR compliance Permendagri.
- **Alur:** Join 9 tabel `Aset+MasterBarang+Kategori+Satuan+Kondisi+Ruang+Lokasi+DetailPengadaan+Pengadaan+SumberPerolehan` where `Status!=Nonaktif`. Filter Lokasi/Ruang. VM `KodeInventaris,NamaBarang,Kategori,Kondisi,TahunPengadaan=TahunRegistrasi.Year,SumberPerolehan,Jumlah=1,Satuan,Ruang,Lokasi,GambarBase64`. Grid hide gambar/ruang/lokasi. Optional `chkIncludeGambar` print.
- **Fitur:** Join 9 tabel.

#### 2.9.20 LaporanStokBarangHabisPakaiUC.cs
- **File:** `UserControls/LaporanStokBarangHabisPakaiUC.cs`
- **Deskripsi:** Stok HP.
- **Alur:** `VAsetHabisPakai` filter `NamaBarang.Contains` autocomplete `Gudang.NamaGudang` + `MasterBarang Jenis=Habis Pakai`. VM `KodeBarang,NamaBarang,Kategori,Jurusan,Lokasi,Ruang,StokAwal,StokAktual,Status`. Export EPPlus styled langsung: LicenseContext NonCommercial merge `A1:I1` title bold 14 center header bold LightGray border thin loop rows AutoFitColumns.
- **Fitur:** EPPlus styled.

#### 2.9.21 LaporanStokMinimalBarangHabisPakaiUC.cs
- **File:** `UserControls/LaporanStokMinimalBarangHabisPakaiUC.cs`
- **Deskripsi:** Stok menipis.
- **Alur:** `const STOK_MINIMAL_THRESHOLD=5` dari AppConstants, query `Where StokAktual <= threshold && Status==Tersedia`, VM sama stok, highlight red jika <=2. Export. Notifikasi trigger jika masuk list.
- **Fitur:** Threshold alert.

### 2.10 Modul Tools & Admin

#### 2.10.1 KoneksiDbForm.cs
- **File:** `Forms/KoneksiDbForm.cs`
- **Deskripsi:** Setting koneksi DB dengan proteksi DPAPI.
- **Alur:** Validasi field required + `IsSafeHost,IsValidPort,IsSafeIdentifier` sebelum build. `BuildValidatedMySqlConnectionString` → test `MySqlConnection.Open()` → `ConnectionStringProtector.Protect(connStr)` → simpan ke `ConfigurationManager.ConnectionStrings["KoneksiServer"]` encrypted. `AllowLoadLocalInfile=false`. UI PasswordChar di Designer.
- **Fitur:** Validasi, DPAPI, test connection.

#### 2.10.2 BackupDbForm.cs
- **File:** `Forms/BackupDbForm.cs`
- **Deskripsi:** Backup full/partial tanpa shell.
- **Alur:** Hapus `cmd.exe /c`: direct `mysqldump.exe` dari `Resources/` fallback PATH. Full backup & partial: `SHOW TABLES` → filter `IsSafeTableName`, `DbTableControl` checkbox list. `EscapeArg`, `SanitizeFileName`, path traversal final check, `ValidateBackupFolder/TableNames`. Streaming `StandardOutput.BaseStream.CopyTo(fileStream)`, `StandardError.ReadToEnd()`, cek ExitCode, hapus partial file jika gagal, validasi non-empty. MaxBackup 500MB guard.
- **Fitur:** Streaming, partial, validation.

#### 2.10.3 RestoreDbForm.cs
- **File:** `Forms/RestoreDbForm.cs`
- **Deskripsi:** Restore .sql tanpa shell `<`.
- **Alur:** Hapus shell `<`: `RedirectStandardInput=true`, `FileStream.CopyTo(StandardInput.BaseStream)`. Validasi `.sql` ext case-insensitive, `IsSafePath,IsSafeHost/Identifier,EscapeArg,MYSQL_PWD` env. `EscapeArg` handle `& | < > ^ ; ` $ ( ) * ?`.
- **Fitur:** Streaming restore.

#### 2.10.4 ResetDbForm.cs
- **File:** `Forms/ResetDbForm.cs`
- **Deskripsi:** Reset truncate kategori dengan transaksi aman.
- **Alur:** Konfirmasi ketik `HAPUS {kategori}` exact match + dialog Yes/No kedua. Whitelist tabel `Type[]` dari `typeof(Kategori)...` → `db.Model.FindEntityType(tipe).GetTableName()` → regex `^[a-zA-Z0-9_]+$`. Transaksi `BeginTransaction()`, `SET FOREIGN_KEY_CHECKS=0`, loop `TRUNCATE TABLE`, `SET FOREIGN_KEY_CHECKS=1`, Commit, catch restore FK + Rollback. Komentar ponytail identifier whitelist.
- **Fitur:** Whitelist, transaction.

#### 2.10.5 ProfilLembagaForm.cs
- **File:** `Forms/ProfilLembagaForm.cs`
- **Deskripsi:** Profil instansi + logo.
- **Alur:** Magic bytes via `new Bitmap(path)` test ext whitelist `.jpg/.jpeg/.png` size 10MB `FileInfo.Length`. GUID filename `Guid.NewGuid().ToString("N")+ext`, path traversal `GetFileName==original`, `GetFullPath(dest).StartsWith(GetFullPath(Resources))`. Load `GetFileName(Logo)==Logo` saja plus `StartsWith` check. Dispose previous Image sebelum ganti. `cbHapus` clear logo.
- **Fitur:** Image validation aman.

#### 2.10.6 UserForm.cs
- **File:** `Forms/UserForm.cs`
- **Deskripsi:** Manajemen pengguna.
- **Alur:** DTO `PenggunaDto` tanpa Password untuk `dg.DataSource` hash tidak terekspos grid `AsNoTracking Include`. RBAC `_hakAkses` field SetMode hard true bug fixed `hak.HakBuat/Ubah/Hapus` Guard btnTambah/Ubah/Hapus/Simpan. Password `ValidatePasswordComplexity` min 8 huruf+angka tidak mengandung username `BCrypt.HashPassword(...,12)` insert wajib password update opsional. Self-deletion block `Id==CurrentUserId` tolak Delete catch `DbUpdateException` reload entity. `TriggerUIState`: Guru→Mapel visible Murid→Kelas visible.
- **Fitur:** DTO hide hash, complexity, self-delete block.

#### 2.10.7 GroupUserForm.cs
- **File:** `Forms/GroupUserForm.cs`
- **Deskripsi:** Role & permission matrix.
- **Alur:** RBAC 6 nodes `Tambah Baca Ubah Hapus Approve Export` (Approve/Export sprint RBAC). `RenderDynamicAkses`: load `Akses` AsNoTracking parent null child TreeNode Tag=IdAkses 6 child CRUD nodes Tag="CRUD". SetMode respects RBAC fix hard true Guard semua tombol. Hapus block jika `IdPeran==CurrentRoleId` akun sendiri + `Pengguna.Any(In-use)`. Simpan hapus `PeranAkses` lama `RemoveRange` insert baru `AuditHelper.Log` Pattern tracked vs AsNoTracking grid AsNoTracking edit reload tracked.
- **Fitur:** 6 hak, tree dynamic.

#### 2.10.8 ApprovalWorkflowConfigForm.cs
- **File:** `Forms/ApprovalWorkflowConfigForm.cs`
- **Deskripsi:** Config multi-level workflow.
- **Alur:** CRUD `ApprovalWorkflowConfig` fields `WorkflowType combo (Permintaan,PermintaanHp,Pengadaan...), Level int, NamaLevel, IdPeranApprover FK Peran via combo, IsRequired bool, Urutan`. Validasi Level>0, NamaLevel wajib, Role required, duplicate check `Any(wType==type && Level==level && Id!=current)`. Simpan SaveChanges Audit Log. Grid `AsNoTracking` Include Peran. Search WorkflowType.
- **Fitur:** Workflow config CRUD.

#### 2.10.9 ApprovalStepForm.cs
- **File:** `Forms/ApprovalStepForm.cs`
- **Deskripsi:** Monitoring step approval per transaksi.
- **Alur:** Read-only mostly `ApprovalStep` filter WorkflowType, RefId search, Status filter Pending/Approved/Rejected. VM `WorkflowType,RefId,Level,NamaLevel,Approver,Status,TanggalKeputusan,Catatan`. Tombol Approve/Reject manual jika `CurrentRole==IdPeranApprover` dan Pending (admin override). Audit log. Paging? Count Skip/Take.
- **Fitur:** Step monitoring.

#### 2.10.10 AboutForm.cs (Tools category also)
Sudah di 2.1.3.

### 2.11 Modul Dashboard & Enterprise

#### 2.11.1 DashboardUC.cs
- **File:** `UserControls/DashboardUC.cs`
- **Deskripsi:** Dashboard ringkasan KPI, chart kondisi, stok minimal, permintaan pending.
- **Alur:** `LoadDataAsync` async: `await db.Aset.CountAsync()` aktif/nonaktif, `await db.AsetHabisPakai.CountAsync()` + sum stok, `await db.Permintaan.Where(Status=Menunggu).CountAsync()`, `GroupBy(a=>a.IdKondisiNavigation.NamaKondisi).Select(g=>new{g.Key,Count=g.Count()}).ToListAsync()` untuk pie chart kondisi, `GroupBy Barang Kategori` bar chart. `InvokeRequired` check sebelum `chart.Series.Clear()` + `Points.AddXY`. Card label `lblTotalAset`, `lblNilaiAset C2`, etc. `Timer` refresh 60s. `AsNoTracking` semua. Click card navigate `ChangeView`. Handling zero division.
- **Fitur:** Async, GroupBy server-side, chart.

#### 2.11.2 AuditLogUC.cs
- **File:** `UserControls/AuditLogUC.cs`
- **Deskripsi:** Viewer audit trail.
- **Alur:** `_pageSize=100,_currentPage,_totalRecords`, filter tanggal `dtAwal,dtAkhir`, search Modul/Aksi/Username, status. Query `db.AuditLog.AsNoTracking()` Count + `OrderByDescending(Timestamp).Skip/Take` ToList. Timestamp nullable handling `HasValue?ToString("dd/MM/yyyy HH:mm:ss"):"(null/0000-00-00)"` actionable message jika `0000-00-00` detected suggest `UPDATE audit_log SET timestamp=NOW() WHERE timestamp='0000-00-00'` + `CREATE TABLE`. Export CSV `EscapeCsv`. RBAC `AuditLog` HakBaca. Paging Prev/Next dynamic.
- **Fitur:** Nullable zero date handling, paging.

#### 2.11.3 NotifikasiUC.cs
- **File:** `UserControls/NotifikasiUC.cs`
- **Deskripsi:** Inbox notifikasi user.
- **Alur:** `Load` `db.Notifikasi.AsNoTracking().Where(UserId==CurrentUser||UserId==null).OrderByDescending(CreatedAt).Take(50)` → `SortableBindingList`. Bold if !IsRead. Click tandai baca `IsRead=true SaveChanges`. Badge update via `MainForm`. Polling `NotificationService` 30 detik.
- **Fitur:** Read/unread.

#### 2.11.4 AsetLampiranUC.cs
- **File:** `UserControls/AsetLampiranUC.cs`
- **Deskripsi:** Attachment per aset.
- **Alur:** Filter `TipeAset` + `RefId`, grid `AsetLampiran AsNoTracking` Include CreatedBy. Tombol Upload `OpenFileDialog` ImageValidator magic bytes MaxAttach 20MB GUID save path `Lampiran\{Tipe}\{Year}\{GUID}{ext}` `File.Copy` + insert DB. Preview `PictureBox` `new Bitmap`, Download `SaveFileDialog` `File.Copy`, Hapus `File.Delete + Remove`. Path traversal guard `GetFullPath(dest).StartsWith(GetFullPath(LampiranRoot))`. 
- **Fitur:** Generic attachment.

#### 2.11.5 BaseLaporanUC.cs
- **File:** `UserControls/BaseLaporanUC.cs`
- **Deskripsi:** Sudah di 2.9.1 sebagai base abstract.
- **Alur:** Abstract template, print header/footer logo, `FindControlRecursive`, `PrintDataGridView`, `ExportToExcel` placeholder, Dispose pattern.
- **Fitur:** Dedup.

#### 2.11.6 DbTableControl.cs
- **File:** `UserControls/DbTableControl.cs`
- **Deskripsi:** Custom checkbox satu tabel untuk partial backup.
- **Alur:** Wrapper UserControl berisi CheckBox `cbModul`, property `NamaModul` ↔ `cbModul.Text`, `IsChecked` ↔ Checked. Dipakai di `BackupDbForm pnlTabel` flow layout. YAGNI tidak buat custom list berat.
- **Fitur:** Simple wrapper.

---

## 3. Implementasi Pengolahan Data (Entity Framework)

### 3.1 Arsitektur Query EF Core + EF6 Ganda

Proyek unik karena memiliki `System.Data.Entity` (EF6) dan `Microsoft.EntityFrameworkCore` (EFCore 3.1) bersamaan. `AppDbContext` inherit `DbContext` EFCore tetapi beberapa Form lama masih `using System.Data.Entity`. `OnConfiguring` decrypt connection string via `ConnectionStringProtector.GetDecryptedConnectionString()` dengan `DbConnectionStringBuilder` enforcement `AllowLoadLocalInfile=false`, `AllowUserVariables=false`. `optionsBuilder.UseMySql(connString)` Pomelo 3.2.4.

`OnModelCreating` 3800+ baris, 80+ `DbSet`, 22 `ToView` untuk `HasNoKey()`, 5 tabel baru `ToTable` mapping eksplisit `audit_log`, `notifikasi`, `aset_lampiran`, `approval_workflow_config`, `approval_step` agar tidak pluralize salah casing. `PeranAkses.HakApprove/HakExport` nullable `bool?` agar kolom lama NULL tidak crash cast. Semua view `HasNoKey().ToView()` read-only tidak di-track tidak butuh PK.

Alasan mempertahankan EFCore daripada full migrasi EF6: Pomelo MySQL provider lebih baik di EFCore untuk `AsNoTracking`, `CountAsync`, `ToListAsync`, dan translasi `Include` menjadi LEFT JOIN otomatis pada FK nullable.

### 3.2 Select Mapping Navigasi Relasional vs JOIN Manual – Kenapa Include Dipertahankan

Audit menemukan anti-pattern manual JOIN:
```csharp
from a in db.Aset join m in db.MasterBarang on a.IdMasterBarang equals m.IdMasterBarang ...
```
Dibuang dan diganti `Include`.

**Alasan ideal:**
- **Type safety:** `IdMasterBarangNavigation` di-check compiler. Manual JOIN typo key = runtime error.
- **Provider translation:** Pomelo 3.2.4 translate `Include` pada FK nullable menjadi `LEFT JOIN` otomatis. Manual `join` default `INNER JOIN` → orphan FK akibat `FK_CHECKS=0` hilang diam-diam dari grid. Dengan `Include`, orphan tetap muncul dengan nav null → bisa fallback "Unknown".
- **Maintainability:** Navigation sudah definisi di model. Perubahan FK hanya di `OnModelCreating`, tidak merambat 20 query manual.
- **Optimized:** `GroupBy(a=>a.IdKondisiNavigation.NamaKondisi)` tetap translate ke SQL GROUP BY, tidak N+1. Filter `Where(a.IdMasterBarangNavigation!=null && Contains)` masih server-side.

Spar yang dipertahankan:
```csharp
IQueryable<Aset> q = db.Aset
  .Include(a=>a.IdMasterBarangNavigation)
  .Include(a=>a.IdJurusanNavigation)
  .Include(a=>a.IdRuangNavigation)
  .AsNoTracking();
```

### 3.3 Teknik Include + Fallback Dict Hybrid untuk Orphan FK_CHECKS=0

Masalah: DB legacy pernah `SET FOREIGN_KEY_CHECKS=0; TRUNCATE`, menyebabkan `aset.id_master_barang=999` tidak ada di `master_barang`. `Include` → null UI jadi Unknown/crash NullReference.

Solusi lama boros: `dictBarang=db.MasterBarang.ToDictionary()` 10k full scan memory.

Solusi audit sekarang filtered:
```csharp
var masterIds = page.Select(a=>a.IdMasterBarang).Distinct().ToList();
var dictMaster = db.MasterBarang.AsNoTracking()
  .Where(m=>masterIds.Contains(m.IdMasterBarang))
  .ToDictionary(m=>m.IdMasterBarang,m=>m.NamaBarang);
string namaBarang = a.IdMasterBarangNavigation?.NamaBarang;
if(string.IsNullOrEmpty(namaBarang) && dictMaster.TryGetValue(a.IdMasterBarang, out var nm)) namaBarang=nm;
if(string.IsNullOrEmpty(namaBarang)) namaBarang="Unknown";
```
Chain di `TransaksiPeminjamanForm`: `KodeBarang -> Aset -> MasterBarang -> Kondisi` dengan 3 dict terpisah, bukan nested `FirstOrDefault` di loop N+1. `MutasiBarangForm` sama.

Impact: query dari `O(N*M)` full-load → `O(page) + 1 WHERE IN` indexed.

### 3.4 Paging Skip/Take + AsNoTracking + Count + CountAsync Anti-OOM

Sebelum: `db.Aset.ToList()` 10k+ row + tracking snapshot + entity graph Master/Jurusan + Gambar base64 → 200MB+ LOH freeze.

Sesudah:
```csharp
_totalRecords = q.Count(); // SELECT COUNT(*) tanpa load entity
var page = q.OrderByDescending(a=>a.KodeBarang)
  .Skip(_currentPage*_pageSize) // OFFSET
  .Take(_pageSize) // LIMIT 100
  .ToList();
dg.DataSource = new SortableBindingList<AsetViewModel>(page.Select(MapToVM).ToList());
```
`AsNoTracking()` tidak buat `ChangeTracker` entry hemat 50-60% RAM tidak perlu `DetectChanges`. `using(var db=new AppDbContext())` per load di `DataBarangHabisPakaiUC` agar context tidak accumulates 1000+ tracked. ViewModel proyeksi minimal kolom bukan full Aset + longtext Gambar ikut load. Total pages `Math.Ceiling(total/_pageSize)` di UI. `DashboardUC` `CountAsync`, `GroupBy().Select().ToListAsync()` + `InvokeRequired` check sebelum set chart.Series.

### 3.5 Transaction BeginTransaction Commit/Rollback Cegah Partial Orphan

**InputBarangAsetForm:**
```csharp
using(var tx=db.Database.BeginTransaction()){
  try{
    var master=db.MasterBarang.FirstOrDefault(m=>m.Nama.ToLower()==namaInput.ToLower());
    if(master==null){ db.MasterBarang.Add(newMaster); db.SaveChanges(); }
    db.Aset.Add(asetBaru); db.SaveChanges();
    tx.Commit();
  }catch{ try{tx.Rollback();}catch{} throw; }
}
```
Tanpa tx: SaveChanges pertama sukses buat Master kedua gagal dupe KodeInventaris unique → Master orphan tanpa Aset.

**InputPengadaanBarangForm & Peminjaman:**
- `Pengadaan + DetailPengadaan + PengadaanPermintaan` junction 3 SaveChanges dalam satu tx.
- `Peminjaman` pakai `saveDb` baru untuk transaksi agar context read `db` tidak kotor tracking status Dipinjam.

ACID compliance: Rollback di catch try{Rollback()}catch{} defensif untuk connection already closed.

**ResetDbForm:** `BeginTransaction()`, `SET FOREIGN_KEY_CHECKS=0`, loop TRUNCATE, `SET FOREIGN_KEY_CHECKS=1`, Commit, catch restore FK + Rollback.

### 3.6 N+1 Preload Dict Pattern

Lihat 1.2.2: preload dictionary sekali sebelum binding untuk hindari `CellFormatting` N+1. Contoh `InputPengadaanBarangForm` `dictBarang,dictKategori,dictPemasok,dictJurusan,dictPengguna` preload sebelum Select. `TransaksiPeminjamanForm` 3 dict chain. Pengukuran: sebelum 100 rows → 100 query tambahan `SELECT * FROM jurusan WHERE Id=...` via lazy loading, sesudah 1 query `WHERE IN (ids)`.

### 3.7 Timestamp Nullable DateTime? untuk Zero Date MySQL

MySQL `DATETIME` bisa `0000-00-00 00:00:00` jika `sql_mode` loose. `DateTime` non-nullable .NET tidak bisa parse → `MySqlException: Unable to convert MySQL date/time`.

Model:
```csharp
public DateTime? Timestamp { get; set; } // map DATETIME NULL
```
Query null-safe:
```csharp
q=q.Where(a=>a.Timestamp.HasValue && a.Timestamp.Value>=start);
var data=q.OrderByDescending(a=>a.Timestamp)...
```
Display:
```csharp
string ts=log.Timestamp.HasValue?log.Timestamp.Value.ToString("dd/MM/yyyy HH:mm:ss"):"(null/0000-00-00)";
```
Catch actionable message:
```csharp
if(ex.Message.Contains("0000-00-00")) userMsg+="UPDATE audit_log SET timestamp=NOW() WHERE timestamp='0000-00-00'";
if(ex.InnerException.Message.Contains("doesn't exist")) userMsg+="CREATE TABLE audit_log..."
```

### 3.8 EF6 vs EFCore AsNoTracking Ambiguous Fix

Proyek ada both:
```csharp
using System.Data.Entity; // EF6 AsNoTracking di IQueryable
using Microsoft.EntityFrameworkCore; // EFCore AsNoTracking()
```
Compiler error ambiguous call. Fix audit: hapus `System.Data.Entity` dari UC baru pakai hanya `Microsoft.EntityFrameworkCore`. Atau fully qualified `Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(q)` – di `AuditLogUC`/`HabisPakaiUC` sekarang konsisten `using Microsoft.EntityFrameworkCore`.

### 3.9 Best Practices Industri Diterapkan

- **Defense-in-depth:** `AllowLoadLocalInfile=false` + decrypt protector + `TryParse` validation umurEkonomi nilaiResidu lamaPinjam bukan trust textbox int.
- **Fail-safe default:** `Status ?? "Aktif"`, `NamaBarang fallback "Unknown"/"-"` tidak crash grid.
- **Batch operation:** `MasterBarangForm` import `AddRange(batch)` satu SaveChanges vs N SaveChanges; export `Include` sekali.
- **Async I/O di UI thread-sensitive:** `DashboardUC` `CountAsync`, `ToListAsync`, `GroupBy().Select().ToListAsync()` + `InvokeRequired` check sebelum set chart.Series.
- **Non-blocking audit:** `AuditHelper.Log` wrap try{} + `SerializeObject` MaxDepth=3 ReferenceLoopHandling.Ignore cyclic nav tidak stack overflow audit gagal tidak batalkan bisnis.
- **ViewModel boundary:** `AsetViewModel`, `DetailItemViewModel` DataGrid tidak expose entity penuh + Gambar berat.
- **Regex whitelist identifier:** `^[a-zA-Z0-9_]+$` untuk nama tabel/kolom anti DDL injection karena identifier tidak bisa diparameterisasi.
- **GenerateSafeFileName GUID:** cegah path traversal + collision.
- **Magic bytes validation:** `new Bitmap(path)` load test bukan hanya ekstensi.
- **Copy permission dictionary:** `GetAllAkses()` return copy cegah mutasi eksternal.
- **SortableBindingList native:** tidak pakai library berat.
- **Transaction defensive rollback:** `try{tx.Rollback()}catch{}` untuk connection closed.
- **Filtered dict:** hanya ID di page saat ini bukan full table.

---

## Penutup

Dokumentasi ini menguraikan secara exhaustif 49 Forms dan 36 UserControls aplikasi Inventaris Aset, mencakup autentikasi, master data, aset tetap, tanah/bangunan, barang habis pakai, pengadaan/permintaan multi-level approval, peminjaman/pengembalian, mutasi/opname/nonaktif, laporan 21 jenis, tools admin, dan dashboard enterprise. Setiap modul dijelaskan alur kerja, validasi, query EF (Include, AsNoTracking, Paging, Transaction, N+1 preload, zero-date handling), bug kritis (RCE, plaintext, hash exposure, RBAC bypass, path traversal, race condition, dead code, DLL hell), optimasi performa (paging 100, streaming, async, dict filtered), dan fitur baru (AuditLog, Notifikasi, Lampiran, Approval Workflow, QR, BaseLaporanUC EPPlus, DbTableControl, AppConstants, ImageValidator).

Total cakupan: 80+ Models, 22 Views, RBAC 6 hak (Tambah Baca Ubah Hapus Approve Export), session 8 jam, BCrypt work factor 12, lockout 5×15 menit, max backup 500MB, max image 10MB, max attach 20MB, stok minimal threshold 5. Pola ponytail diterapkan: deletion over addition, native over library, one line over fifty, dengan komentar `ponytail: ... ceiling and upgrade path`.

Dokumen siap untuk BAB Implementasi Laporan PKL akademik, dengan struktur teknis yang dapat diuji, di-reproduce, dan di-audit ulang.

