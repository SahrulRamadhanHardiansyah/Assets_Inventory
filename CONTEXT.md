# 📋 Dokumen Konteks Aplikasi — Assets Inventory

> **Versi dokumen:** 1.0  
> **Terakhir diperbarui:** 20 Mei 2026  
> **Tujuan:** Memori konteks untuk handoff ke AI Agent baru

---

## 1. Executive Summary

| Item | Detail |
|---|---|
| **Nama Aplikasi** | Assets Inventory (Sistem Inventaris Aset) |
| **Tujuan Utama** | Aplikasi desktop untuk manajemen inventaris aset milik instansi pendidikan (SMKN 1 Bangil), mencakup pencatatan barang, tanah, bangunan, pengadaan, peminjaman, mutasi, opname, hingga pelaporan |
| **Target Pengguna** | Staf inventaris/sarana-prasarana sekolah, kepala sekolah, dan guru/karyawan yang membutuhkan barang |
| **Jenis Aplikasi** | Windows Forms Desktop Application (WinForms) |
| **Namespace Root** | `Assets_Inventory` |

---

## 2. Tech Stack

### 2.1 Core

| Komponen | Teknologi | Versi |
|---|---|---|
| **Bahasa** | C# | — |
| **Framework** | .NET Framework | **4.8** |
| **UI Framework** | Windows Forms (WinForms) | — |
| **UI Library** | Krypton Toolkit (ComponentFactory) | `100.26.1.19` |
| **ORM** | Entity Framework Core | `3.1.32` |
| **Database** | MySQL | via `MySqlConnector 0.69.9` |
| **EF Provider** | Pomelo.EntityFrameworkCore.MySql | `3.2.4` |

### 2.2 Library Pendukung

| Library | Versi | Fungsi |
|---|---|---|
| `EPPlus` | `7.7.3` | Export data ke Excel (`.xlsx`) |
| `ExcelDataReader` | `3.8.0` | Import data dari Excel (`.xls`/`.xlsx`) |
| `Newtonsoft.Json` | `13.0.1` | Serialisasi/deserialisasi JSON |
| `ZXing.Net` | `0.16.11` | Generate/baca barcode/QR code |
| `BouncyCastle.Cryptography` | `2.6.2` | Kriptografi (dependensi) |
| `Google.Protobuf` | `3.32.0` | Protocol Buffers (dependensi MySQL) |

### 2.3 Database Connection

Connection string dikonfigurasi di `App.config`:

```xml
<connectionStrings>
    <add name="KoneksiServer" 
         connectionString="Server=localhost;Port=3307;Database=inventaris_aset_db;Uid=root;Pwd=root;" 
         providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

- **Database Name:** `inventaris_aset_db`
- **Default Port:** `3307`
- Connection string bisa diubah runtime melalui `KoneksiDbForm`

### 2.4 User Settings (Properties.Settings)

Disimpan di `Properties/Settings.settings`, diakses via `Properties.Settings.Default`:

| Setting | Tipe | Deskripsi |
|---|---|---|
| `userId` | `int` | ID pengguna yang sedang login |
| `BackupPath` | `string` | Path folder untuk auto-backup |
| `BackupInterval` | `int` | Interval auto-backup dalam menit (0 = nonaktif) |

---

## 3. Core Features & Business Logic

### 3.1 Autentikasi & Otorisasi

- **Login** (`LoginForm`): Validasi `username` + `password` langsung ke tabel `pengguna` (plaintext, tanpa hashing)
- **Role-Based Access Control**: Sistem peran (`Peran`) + hak akses modular (`PeranAkses`) dengan granularitas CRUD per modul
- **User ID** disimpan di `Properties.Settings.Default.userId` setelah login berhasil

### 3.2 Manajemen Aset Tetap

- **Pengadaan Barang** (`PengadaanBarangUC`, `InputPengadaanBarangForm`):  
  Pencatatan pengadaan barang masuk → otomatis generate kode inventaris per barang → insert ke tabel `aset`
- **Input Tanah** (`InputTanahForm`): Pencatatan aset tanah dengan detail sertifikat, luas, status hak
- **Input Bangunan** (`InputBangunanForm`): Pencatatan aset bangunan dengan dimensi, konstruksi, kondisi
- **Kelengkapan Aset** (`KelengkapanAsetForm`): Melengkapi data aset yang belum lengkap (no seri, gambar, lokasi, ruang)
- **Notifikasi Aset Belum Lengkap**: Badge di `MainForm` menampilkan jumlah aset tanpa NoSeri/IdRuang/IdLokasi/Gambar

### 3.3 Manajemen Aset Habis Pakai

- **Data Barang Habis Pakai** (`DataBarangHabisPakaiUC`): Master data ATK/bahan habis pakai
- **Pengadaan Habis Pakai** (`DataPengadaaanBarangHabisPakaiUC`): Pencatatan barang masuk (stok bertambah)
- **Barang Keluar** (`DataBarangHabisPakaiKeluarUC`): Pencatatan distribusi barang keluar ke ruang/pengguna

### 3.4 Permintaan Barang

- **Permintaan** (`PermintaanBarangUC`, `InputPermintaanBarangForm`):  
  Alur: User buat permintaan → detail barang diminta → approval oleh penyetuju  
  Status: `Menunggu` → `Disetujui`/`Ditolak`
- **Pengadaan dari Permintaan**: Permintaan yang disetujui bisa langsung dijadikan pengadaan via tabel pivot `pengadaan_permintaan`

### 3.5 Peminjaman & Pengembalian

- **Peminjaman** (`PeminjamanBarangUC`): Catat peminjaman dengan detail barang, durasi, nama peminjam
- **Pengembalian** (`PengembalianBarangUC`): Proses pengembalian barang pinjaman
- Status aset berubah: `Di Gudang` → `Dipinjam` → `Di Gudang`

### 3.6 Mutasi Barang

- **Mutasi** (`MutasiBarangForm`): Pindah aset dari jurusan/unit asal ke jurusan tujuan
- Riwayat mutasi tercatat di tabel `mutasi`

### 3.7 Opname & Non-Aktif

- **Opname** (`OpnameBarangForm`): Stock opname untuk verifikasi kondisi fisik aset
- **Non-Aktif** (`ProsesNonAktifForm`): Penonaktifan aset (barang/bangunan/tanah) dengan alasan
  - Status aset berubah ke `Nonaktif`

### 3.8 Master Data

Diakses melalui `MasterDataForm` (form induk), masing-masing memiliki form CRUD sendiri:

| Form | Tabel | Keterangan |
|---|---|---|
| `MasterBarangForm` | `master_barang` | Daftar jenis barang (dengan import/export Excel) |
| `MasterKategoriForm` | `kategori` | Kategori barang |
| `MasterMerekForm` | `merek` | Merek barang |
| `MasterSatuanForm` | `satuan` | Satuan barang (pcs, unit, dll) |
| `MasterKondisiForm` | `kondisi` | Kondisi fisik (Baik, Rusak Ringan, dll) |
| `MasterLokasiForm` | `lokasi` | Lokasi penempatan |
| `MasterRuangForm` | `ruang` | Ruangan |
| `MasterNonAktifForm` | `status_barang` | Alasan non-aktif (Rusak Berat, Hilang, dll) |
| `MasterGudangForm` | `gudang` | Data gudang penyimpanan |
| `MasterSupplierForm` | `pemasok` | Data pemasok/supplier |

### 3.9 Laporan

Diakses melalui `LaporanForm` (form induk), menampilkan berbagai `UserControl`:

- Inventaris Barang, Tanah, Bangunan (aktif & non-aktif)
- Pengadaan Barang
- Peminjaman & Pengembalian
- Mutasi Barang
- Penyusutan Nilai Barang (metode garis lurus)
- Proses Opname
- Stok Barang Habis Pakai & Stok Minimal
- Jatuh Tempo Peminjaman
- Kartu Inventaris Ruangan

### 3.10 Pengaturan Sistem

- **Profil Lembaga** (`ProfilLembagaForm`): Data instansi (nama, alamat, logo, kepala sekolah, NIP)
- **Koneksi Database** (`KoneksiDbForm`): Ubah connection string MySQL runtime
- **Backup** (`BackupDbForm`): Backup manual + konfigurasi auto-backup (via `mysqldump.exe`)
- **Restore** (`RestoreDbForm`): Restore database dari file `.sql`
- **Reset** (`ResetDbForm`): Reset seluruh data database
- **Wallpaper Aplikasi**: Ganti background panel utama `MainForm`
- **User Management** (`UserForm`): CRUD pengguna + assign peran
- **Group User** (`GroupUserForm`): CRUD peran/role + assign hak akses CRUD per modul

---

## 4. Database Schema & Data Model

### 4.1 Entity-Relationship Overview

Database `inventaris_aset_db` menggunakan **53 tabel** yang di-scaffold via EF Core `dotnet ef dbcontext scaffold`. Semua model berada di namespace `Assets_Inventory.Models`.

### 4.2 Tabel Utama

#### Aset & Inventaris

```
┌──────────────────┐       ┌─────────────────┐       ┌──────────────────┐
│   master_barang   │◄──────│  detail_pengadaan │──────►│    pengadaan      │
│ (id_master_barang)│       │(id_detail_pengad.)│       │  (id_pengadaan)   │
│ nama_barang       │       │ jumlah_masuk      │       │ tanggal_pengadaan │
│ id_kategori (FK)  │       │ harga_satuan      │       │ id_pemasok (FK)   │
│ id_merek (FK)     │       │                   │       │ kode_gudang (FK)  │
│ id_satuan (FK)    │       │                   │       │ sumber_perolehan  │
│ jenis_barang      │       │                   │       │ status            │
└──────────────────┘       └────────┬──────────┘       └──────────────────┘
                                    │
                                    ▼
                           ┌──────────────────┐
                           │       aset        │
                           │  (kode_barang) PK │
                           │ kode_inventaris UK│
                           │ id_master_barang  │
                           │ id_detail_pengad. │
                           │ no_seri            │
                           │ harga_satuan       │
                           │ nilai_residu       │
                           │ umur_ekonomi       │
                           │ status (ENUM)      │
                           │ id_ruang (FK)      │
                           │ id_lokasi (FK)     │
                           │ id_jurusan (FK)    │
                           │ gambar             │
                           └──────────────────┘
```

**Status Aset (ENUM):** `'Di Gudang'`, `'Aktif'`, `'Dipinjam'`, `'Nonaktif'`

#### Aset Tanah & Bangunan

| Tabel | PK | Field Utama |
|---|---|---|
| `aset_tanah` | `kode_tanah` (int) | `letak_tanah`, `luas_tanah`, `nama_pemilik`, `nomor_sertifikat`, `status_hak` (ENUM), `nilai_aset`, `sumber_perolehan` (ENUM) |
| `aset_bangunan` | `kode_bangunan` (int) | `nama_bangunan`, `ukuran_p`, `ukuran_l`, `luas_bangunan`, `konstruksi`, `status_bangunan` (ENUM), `nilai_aset`, `id_kondisi` (FK) |

#### Aset Habis Pakai

| Tabel | PK | Field Utama |
|---|---|---|
| `aset_habis_pakai` | `id_aset_habis_pakai` | `kode_barang` (UK), `nama_barang`, `id_satuan` (FK), `stok_minimal`, `gambar` |

#### Transaksi

| Tabel | PK | Deskripsi |
|---|---|---|
| `permintaan` | `kode_permintaan` (varchar) | Header permintaan barang |
| `detail_permintaan` | `id_detail_permintaan` | Detail barang per permintaan |
| `pengadaan_permintaan` | — | Pivot: link pengadaan ↔ permintaan |
| `peminjaman` | `nomor_peminjaman` (varchar) | Header peminjaman |
| `detail_peminjaman` | `id_detail_pinjam` | Detail barang per peminjaman |
| `pengembalian` | — | Pencatatan pengembalian |
| `mutasi` | `id_mutasi` | Mutasi barang antar jurusan |
| `barang_keluar` | `no_transaksi` | Distribusi barang habis pakai |

#### Non-Aktif

| Tabel | Deskripsi |
|---|---|
| `barang_non_aktif` | Pencatatan aset barang yang dinonaktifkan |
| `bangunan_non_aktif` | Pencatatan bangunan yang dinonaktifkan |
| `tanah_non_aktif` | Pencatatan tanah yang dinonaktifkan |

#### Master Data Referensi

| Tabel | PK | Field Utama |
|---|---|---|
| `kategori` | `id_kategori` | `nama_kategori`, `keterangan` |
| `merek` | `id_merek` | `nama_merek` |
| `satuan` | `id_satuan` | `nama_satuan` |
| `kondisi` | `id_kondisi` | `nama_kondisi`, `keterangan` |
| `lokasi` | `id_lokasi` | `nama_lokasi` |
| `ruang` | `id_ruang` | `nama_ruang` |
| `gudang` | `kode_gudang` (varchar) | `nama_gudang`, `keterangan` |
| `pemasok` | `id_pemasok` | `nama_pemasok`, `alamat`, `telepon` |
| `status_barang` | `id_status` | `nama_status` (alasan non-aktif) |
| `jurusan` | `id_jurusan` | `nama_jurusan` |
| `kelas` | `id_kelas` | — |
| `mapel` | `id_mapel` | — |
| `unit` | `id_unit` | — |
| `sumber_perolehan` | `id_sumber` | `nama_sumber` |

#### User & Access Control

```
┌───────────┐       ┌───────────┐       ┌─────────────┐       ┌──────────┐
│  pengguna  │──────►│   peran    │◄──────│  peran_akses │──────►│  akses   │
│id_pengguna │       │  id_peran  │       │id_peran_akses│       │ id_akses │
│ username   │       │ nama_peran │       │ id_peran(FK) │       │id_parent │
│ password   │       │            │       │ id_akses(FK) │       │nama_modul│
│id_peran(FK)│       │            │       │ hak_buat     │       │          │
│id_kelas(FK)│       │            │       │ hak_baca     │       │          │
│id_mapel(FK)│       │            │       │ hak_ubah     │       │          │
│id_unit(FK) │       │            │       │ hak_hapus    │       │          │
└───────────┘       └───────────┘       └─────────────┘       └──────────┘
```

#### Pengaturan Sistem

| Tabel | Field Utama |
|---|---|
| `pengaturan` | `nama_instansi`, `alamat_instansi`, `logo_instansi`, `wallpaper_aplikasi`, `telpon`, `website`, `email`, `kota`, `kepala_sekolah`, `nip`, `bagian_inventaris` |

#### Opname

| Tabel | Deskripsi |
|---|---|
| `opname_aset` | Opname per aset tetap (cek kondisi fisik) |
| `opname_stok` | Opname stok barang habis pakai |

#### Tabel Lainnya (Heritage dari Laravel)

Tabel-tabel berikut di-scaffold dari database yang awalnya dibuat dengan Laravel dan **tidak digunakan aktif** di aplikasi WinForms:

`cache`, `cache_locks`, `failed_jobs`, `job_batches`, `jobs`, `migrations`, `personal_access_tokens`, `sessions`

---

## 5. Architecture & Project Structure

### 5.1 Solution Structure

```
Assets_Inventory.slnx
└── Assets_Inventory/                    # Single project (WinForms App)
    ├── Models/                          # 📂 EF Core entity models (53 files)
    │   ├── AppDbContext.cs              #    DbContext + Fluent API configuration
    │   ├── Aset.cs                      #    Model per tabel database
    │   ├── Pengguna.cs
    │   ├── Pengadaan.cs
    │   ├── ...                          #    (Total: 53 model classes)
    │   └── Unit.cs
    │
    ├── Properties/                      # 📂 Assembly info & Settings
    │   ├── AssemblyInfo.cs
    │   ├── Settings.Designer.cs         #    Auto-generated user settings
    │   ├── Settings.settings
    │   └── DataSources/
    │
    ├── Resources/                       # 📂 Embedded resources & tools
    │   ├── mysql.exe                    #    MySQL client binary
    │   ├── mysqldump.exe               #    Backup utility binary
    │   ├── panduan.pdf                  #    User manual PDF
    │   └── smkn1bangil-logo.png        #    Logo instansi
    │
    ├── Program.cs                       # 🚀 Entry point → LoginForm
    ├── App.config                       # ⚙️ Connection string + settings
    ├── packages.config                  # 📦 NuGet package references
    │
    ├── LoginForm.cs/.Designer.cs        # 🔐 Form login
    ├── MainForm.cs/.Designer.cs         # 🏠 Form utama (MDI-like, menu bar)
    │
    │── [Form-form CRUD]                 # 📝 Form dialog untuk input data
    │   ├── InputPengadaanBarangForm.cs
    │   ├── InputTanahForm.cs
    │   ├── InputBangunanForm.cs
    │   ├── InputPermintaanBarangForm.cs
    │   ├── MutasiBarangForm.cs
    │   ├── OpnameBarangForm.cs
    │   └── ...
    │
    │── [Form-form Master Data]          # 📋 Form CRUD master data
    │   ├── MasterBarangForm.cs
    │   ├── MasterKategoriForm.cs
    │   ├── MasterKondisiForm.cs
    │   ├── MasterLokasiForm.cs
    │   ├── MasterRuangForm.cs
    │   ├── MasterSatuanForm.cs
    │   ├── MasterMerekForm.cs
    │   ├── MasterGudangForm.cs
    │   ├── MasterSupplierForm.cs
    │   └── MasterNonAktifForm.cs
    │
    │── [Form-form Pengaturan]           # ⚙️ Form pengaturan sistem
    │   ├── UserForm.cs
    │   ├── GroupUserForm.cs
    │   ├── ProfilLembagaForm.cs
    │   ├── KoneksiDbForm.cs
    │   ├── BackupDbForm.cs
    │   ├── RestoreDbForm.cs
    │   └── ResetDbForm.cs
    │
    │── [UserControl Laporan]            # 📊 UC ditampilkan di panel MainForm
    │   ├── LaporanBarangInventarisUC.cs
    │   ├── LaporanTanahInventarisUC.cs
    │   ├── LaporanBangunanInventarisUC.cs
    │   ├── LaporanPengadaanBarangUC.cs
    │   ├── LaporanPeminjamanBarangUC.cs
    │   ├── LaporanPengembalianBarangUC.cs
    │   ├── LaporanMutasiBarangUC.cs
    │   ├── LaporanPenyusutanNilaiBarangUC.cs
    │   ├── LaporanProsesOpnameUC.cs
    │   ├── LaporanStokBarangHabisPakaiUC.cs
    │   ├── LaporanStokMinimalBarangHabisPakaiUC.cs
    │   ├── LaporanJatuhTempoPeminjamanUC.cs
    │   └── KartuInventarisRuanganUC.cs
    │
    │── [UserControl Transaksi]          # 🔄 UC embedded di panel MainForm
    │   ├── PengadaanBarangUC.cs
    │   ├── PermintaanBarangUC.cs
    │   ├── PeminjamanBarangUC.cs
    │   ├── PengembalianBarangUC.cs
    │   ├── DataBarangHabisPakaiUC.cs
    │   ├── DataBarangHabisPakaiKeluarUC.cs
    │   ├── DataPengadaaanBarangHabisPakaiUC.cs
    │   └── AsetPerluDilengkapiUC.cs
    │
    │── [Form Lainnya]
    │   ├── MasterDataForm.cs            # Induk navigasi master data
    │   ├── LaporanForm.cs               # Induk navigasi laporan
    │   ├── BarangForm.cs
    │   ├── AboutForm.cs
    │   ├── PersetujuanForm.cs
    │   ├── ProsesNonAktifForm.cs
    │   ├── KelengkapanAsetForm.cs
    │   ├── NonAktifBarangForm.cs
    │   ├── NonAktifBangunanForm.cs
    │   └── NonAktifTanahForm.cs
    │
    │── [Helper/Utility]
    │   ├── DatabaseHelper.cs            # Static helper: backup via mysqldump
    │   └── SortableBindingList.cs       # Generic BindingList dgn sort support
    │
    └── packages.config
```

### 5.2 Arsitektur Aplikasi

**Pola: Monolithic WinForms (No Layered Architecture)**

```
┌─────────────────────────────────────────────────┐
│                    UI Layer                      │
│  (Forms + UserControls + Designer files)         │
│                                                  │
│  LoginForm → MainForm                           │
│  MainForm mengelola:                            │
│    - MenuStrip (navigasi)                       │
│    - Panel pnlContent (host UserControl)        │
│    - ShowDialog() untuk Form CRUD               │
│                                                  │
│  ChangeView(UserControl uc) → swap panel content│
├─────────────────────────────────────────────────┤
│                  Data Layer                      │
│  AppDbContext (EF Core 3.1 + Fluent API)        │
│  - Direct DbContext instance per Form/UC         │
│  - `AppDbContext db = new AppDbContext();`       │
│  - LINQ to Entities untuk query                 │
│  - db.SaveChanges() untuk persist               │
└─────────────────────────────────────────────────┘
```

### 5.3 Navigasi UI

- **`LoginForm`** → Entry point (via `Program.cs`)
- **`MainForm`** → Tampil setelah login sukses
  - **MenuStrip** navigasi ke semua modul
  - **Transaksi UserControl** ditampilkan di `pnlContent` via `ChangeView(uc)`
  - **Form CRUD / Dialog** dibuka via `form.ShowDialog()`
  - **`MasterDataForm`** → Sub-dialog navigasi master data
  - **`LaporanForm`** → Sub-dialog navigasi laporan

---

## 6. Code Conventions & Rules

### 6.1 Penamaan (Naming)

| Elemen | Konvensi | Contoh |
|---|---|---|
| **Form** | `PascalCase` + suffix `Form` | `MasterBarangForm`, `LoginForm` |
| **UserControl** | `PascalCase` + suffix `UC` | `PengadaanBarangUC`, `LaporanStokBarangHabisPakaiUC` |
| **Model (Entity)** | `PascalCase` (Bahasa Indonesia) | `Aset`, `Pengadaan`, `Pengguna`, `MasterBarang` |
| **Property Model** | `PascalCase` (C#) → mapping ke `snake_case` (DB) | `IdMasterBarang` → `id_master_barang` |
| **Navigation Property** | `Id[Entity]Navigation` | `IdKategoriNavigation`, `KodeGudangNavigation` |
| **Collection Navigation** | Nama entity (plural implicit) | `DetailPengadaan`, `PeranAkses` |
| **Control prefix** | Hungarian notation | `txt`, `cmb`, `btn`, `dg`, `lbl`, `pnl` |
| **BindingSource** | `bindingSource1` | — |
| **Tabel Database** | `snake_case` (lowercase) | `master_barang`, `detail_pengadaan` |
| **Kolom Database** | `snake_case` (lowercase) | `kode_barang`, `tanggal_pengadaan` |

### 6.2 Pola Akses Data

```csharp
// Inisialisasi DbContext: langsung di field class, 1 instance per form
AppDbContext db = new AppDbContext();

// Query: LINQ to Entities (synchronous)
var data = db.MasterBarang
    .Where(mb => mb.NamaBarang.ToLower().Contains(cari))
    .ToList();

// Load dengan relasi
var data = db.Peran.Include(p => p.PeranAkses).ToList();

// Simpan (Create)
db.MasterBarang.Add(baru);
db.SaveChanges();

// Simpan (Update): langsung ubah property tracked entity
k.NamaBarang = txtNama.Text;
db.SaveChanges();

// Hapus
db.MasterBarang.Remove(k);
db.SaveChanges();
```

> **CATATAN PENTING:** Tidak ada service layer, repository pattern, atau Unit of Work terpisah. Semua akses data dilakukan langsung dari code-behind Form/UC.

### 6.3 Pola UI Form (SetMode Pattern)

Semua form CRUD menggunakan pola `SetMode()` untuk toggle antara mode View dan Edit:

```csharp
private void SetMode(string mode)
{
    if (mode == "View")
    {
        txtNama.Enabled = false;
        btnTambah.Enabled = true;
        btnUbah.Enabled = true;
        btnHapus.Enabled = true;
        btnSimpan.Enabled = false;
        btnBatal.Enabled = false;
    }
    else // "Insert" atau "Update"
    {
        txtNama.Enabled = true;
        btnTambah.Enabled = false;
        btnUbah.Enabled = false;
        btnHapus.Enabled = false;
        btnSimpan.Enabled = true;
        btnBatal.Enabled = true;
    }
}
```

### 6.4 Error Handling

```csharp
// Pola standar: try-catch dengan MessageBox
try
{
    // operasi database
    db.SaveChanges();
    MessageBox.Show("Data berhasil disimpan!", "Sukses", 
        MessageBoxButtons.OK, MessageBoxIcon.Information);
}
catch (DbUpdateException)
{
    db.Entry(k).Reload(); // rollback entity state
    MessageBox.Show("Tidak dapat menghapus data ini karena masih digunakan...", 
        "Peringatan Relasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
catch (Exception ex)
{
    MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, 
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

### 6.5 DataGridView Binding

```csharp
// Menggunakan SortableBindingList<T> custom untuk enable column sorting
dg.DataSource = new SortableBindingList<MasterBarang>(dataList);

// Cell formatting untuk navigation property
private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
    if (dg.Rows[e.RowIndex].DataBoundItem is MasterBarang mb)
    {
        if (KategoriNavigation.Index == e.ColumnIndex) 
            e.Value = mb.IdKategoriNavigation?.NamaKategori;
    }
}
```

### 6.6 Import / Export Excel

- **Import**: Menggunakan `ExcelDataReader` → parse `DataTable` → loop insert
- **Export**: Menggunakan `EPPlus` (NonCommercial license) → format header + auto-fit columns
- Selalu wrap dengan `try/catch/finally` + disable form selama proses

### 6.7 Bahasa

- **UI**: Bahasa Indonesia (label, pesan error, konfirmasi)
- **Code**: Campuran Indonesia (nama model, property) dan English (C# keywords, standard patterns)
- **Database**: Bahasa Indonesia (nama tabel dan kolom dalam `snake_case`)

### 6.8 UI Library

- Semua Form inherit dari `ComponentFactory.Krypton.Toolkit.KryptonForm` (bukan `System.Windows.Forms.Form`)
- Import dua namespace:
  ```csharp
  using ComponentFactory.Krypton.Toolkit;
  using Krypton.Toolkit;
  ```

---

## 7. Current Progress & Next Steps

### 7.1 Fitur yang Sudah Selesai ✅

- [x] Login & autentikasi user
- [x] Manajemen user & group (role-based access dengan CRUD granularity)
- [x] Master data lengkap (barang, kategori, merek, satuan, kondisi, lokasi, ruang, gudang, supplier, alasan non-aktif)
- [x] Pengadaan barang (input detail + auto-generate kode inventaris per unit barang)
- [x] Input aset tanah & bangunan
- [x] Permintaan barang (create + approval workflow)
- [x] Peminjaman & pengembalian barang (UI created, logic in progress)
- [x] Mutasi barang antar jurusan
- [x] Opname barang (UI created)
- [x] Proses non-aktif aset (barang, bangunan, tanah)
- [x] Manajemen barang habis pakai (data, pengadaan, distribusi keluar)
- [x] Profil lembaga + wallpaper aplikasi
- [x] Koneksi database runtime
- [x] Backup (manual + auto), restore, reset database
- [x] Notifikasi aset belum lengkap
- [x] Import/export Excel untuk master barang
- [x] Kelengkapan aset form
- [x] Tutorial/panduan (buka PDF)
- [x] About form
- [x] Laporan-laporan (UI UserControl sudah dibuat)

### 7.2 Fitur yang Masih Perlu Dikembangkan / Diperbaiki 🔧

- [ ] **Hashing password**: Saat ini password disimpan plaintext di database
- [ ] **Implementasi hak akses di UI**: Sistem `PeranAkses` sudah ada di database tapi belum di-enforce ke UI (menu/tombol belum di-disable berdasarkan hak akses user)
- [ ] **Logic laporan**: Beberapa `UserControl` laporan hanya stub (hanya `InitializeComponent()` tanpa logic)
- [ ] **Peminjaman & Pengembalian**: Form sudah dibuat tapi beberapa logic masih kosong/stub
- [ ] **Penyusutan nilai barang**: Logic perhitungan penyusutan metode garis lurus perlu di-review
- [ ] **Validasi input lebih ketat**: Beberapa form belum memiliki validasi yang memadai
- [ ] **Barcode/QR Code**: Library `ZXing.Net` sudah di-install tapi belum diimplementasikan
- [ ] **Print laporan**: Belum ada fitur cetak/print untuk laporan
- [ ] **Audit trail/log aktivitas**: Belum ada pencatatan siapa melakukan apa kapan

### 7.3 Known Issues ⚠️

1. **Duplicate `using`** di `MainForm.cs`: `using System.Net.Http.Headers;` diulang 2x (line 12-13)
2. **Async tanpa await**: Beberapa method ditandai `async` tapi tidak menggunakan `await` (warning CS1998)
3. **DbContext lifetime**: Satu instance `AppDbContext` per form tanpa `Dispose()` — potential memory leak pada penggunaan lama
4. **Password plaintext**: Tidak ada hashing/salting untuk password user
5. **Error handling auto-backup**: Silent catch (empty catch block) di `timerAutoBackup_Tick`

---

## Appendix: Quick Reference

### Cara Menjalankan Aplikasi

1. Pastikan MySQL Server berjalan di `localhost:3307`
2. Database `inventaris_aset_db` sudah di-create dan di-migrate
3. Buka solution di Visual Studio
4. Build & Run (F5)
5. Login default: `admin` / `admin`

### File Penting untuk Dibaca Pertama Kali

| File | Alasan |
|---|---|
| `Models/AppDbContext.cs` | Memahami seluruh skema database (2344 baris Fluent API) |
| `MainForm.cs` | Memahami navigasi dan struktur aplikasi |
| `LoginForm.cs` | Alur autentikasi |
| `MasterBarangForm.cs` | Contoh terbaik pola CRUD yang digunakan |
| `GroupUserForm.cs` | Contoh implementasi hak akses TreeView |
| `App.config` | Connection string dan konfigurasi |

### Konvensi Pesan MessageBox

| Tipe | Pola |
|---|---|
| Sukses | `"Data berhasil disimpan!"` / `"Berhasil dihapus!"` |
| Validasi | `"[Field] harus diisi."` |
| Konfirmasi hapus | `"Apakah anda yakin ingin menghapus data [nama]?"` |
| Error relasi | `"Tidak dapat menghapus data ini karena data masih digunakan..."` |
| Error sistem | `"Terjadi kesalahan sistem: " + ex.Message` / `"Error Sistem: " + ex.Message` |
