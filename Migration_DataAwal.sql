-- ============================================================
-- Migration_DataAwal.sql - V3 SAFE FINAL
-- Migrasi data awal dari file Excel ke database inventaris_aset_db
-- SAFE VERSION:
-- - TIDAK HAPUS: pengguna, peran, akses, peran_akses, pengaturan,
--   jurusan, kelas, rombel, mapel, migrations, sessions, cache, etc
-- - Jurusan: UPSERT (INSERT IGNORE) untuk UNIT baru dari Excel
-- - Master inventaris: DELETE + INSERT fresh dari Excel (hanya baris valid Alat/Bahan)
-- - Excel rows total: 238, valid: 220 (Alat/Bahan), invalid: 18 (null kategori)
-- - Generated from: inventaris sahrul.xlsx (238 rows)
-- - Filter: hanya baris dengan Kategori = Alat atau Bahan (case-insensitive)
-- REVIEW SEBELUM DIEKSEKUSI!
-- ============================================================

-- BACKUP WAJIB sebelum eksekusi:
-- mysqldump -u root -p inventaris_aset_db > backup_pre_migrasi_$(date +%Y%m%d_%H%M%S).sql

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
SET @OLD_SQL_MODE = @@SQL_MODE;
SET SQL_MODE = 'NO_AUTO_VALUE_ON_ZERO';

-- ============================================================
-- DAFTAR TABEL DIPERTAHANKAN (TIDAK DI-DELETE):
-- P1 SYSTEM MUTLAK: pengguna, peran, akses, peran_akses, pengaturan,
--   migrations, sessions, cache, cache_locks, jobs, job_batches,
--   failed_jobs, personal_access_tokens
-- P2 SYSTEM-RELATED: jurusan, kelas, rombel, mapel
--   Alasan: pengguna.id_jurusan -> jurusan, pengguna.id_kelas -> kelas,
--   kelas.id_rombel -> rombel, rombel.id_jurusan -> jurusan
--   Jika di-DELETE, 18 user akan NULL/CASCADE
-- TOTAL DIPERTAHANKAN: 15 tabel
-- ============================================================

-- ============================================================
-- FASE 1: HAPUS DATA INVENTARIS KOTOR (Child -> Master)
-- Hanya 40 tabel inventaris, TIDAK menyentuh sistem
-- ============================================================

DELETE FROM `detail_peminjaman`;
DELETE FROM `kerusakan`;
DELETE FROM `perbaikan`;
DELETE FROM `penghapusan_aset`;
DELETE FROM `mutasi`;
DELETE FROM `opname_aset`;
DELETE FROM `opname_stok`;
DELETE FROM `barang_non_aktif`;
DELETE FROM `bangunan_non_aktif`;
DELETE FROM `tanah_non_aktif`;
DELETE FROM `barang_keluar`;
DELETE FROM `aset`;
DELETE FROM `aset_habis_pakai`;
DELETE FROM `aset_bangunan`;
DELETE FROM `aset_tanah`;
DELETE FROM `detail_pengadaan`;
DELETE FROM `detail_pengadaan_hp`;
DELETE FROM `detail_permintaan`;
DELETE FROM `detail_permintaan_hp`;
DELETE FROM `pengadaan_permintaan`;
DELETE FROM `pengadaan_permintaan_hp`;
DELETE FROM `pengadaan`;
DELETE FROM `pengadaan_habis_pakai`;
DELETE FROM `permintaan`;
DELETE FROM `permintaan_hp`;
DELETE FROM `peminjaman`;
DELETE FROM `pengembalian`;
DELETE FROM `lemari`;
DELETE FROM `master_barang`;
DELETE FROM `ruang`;
DELETE FROM `kategori`;
DELETE FROM `merek`;
DELETE FROM `satuan`;
DELETE FROM `kondisi`;
DELETE FROM `lokasi`;
DELETE FROM `sumber_perolehan`;
DELETE FROM `pemasok`;
DELETE FROM `gudang`;
DELETE FROM `status_barang`;
DELETE FROM `tahun_ajaran`;

-- Catatan: jurusan, kelas, rombel, mapel, pengguna, peran, akses, peran_akses, pengaturan TIDAK dihapus

-- ============================================================
-- FASE 1b: RESET AUTO_INCREMENT (hanya tabel yang di-DELETE)
-- ============================================================

ALTER TABLE `kategori` AUTO_INCREMENT = 1;
ALTER TABLE `kondisi` AUTO_INCREMENT = 1;
ALTER TABLE `satuan` AUTO_INCREMENT = 1;
ALTER TABLE `merek` AUTO_INCREMENT = 1;
ALTER TABLE `lokasi` AUTO_INCREMENT = 1;
ALTER TABLE `sumber_perolehan` AUTO_INCREMENT = 1;
ALTER TABLE `pemasok` AUTO_INCREMENT = 1;
ALTER TABLE `status_barang` AUTO_INCREMENT = 1;
ALTER TABLE `tahun_ajaran` AUTO_INCREMENT = 1;
ALTER TABLE `gudang` AUTO_INCREMENT = 1;
ALTER TABLE `ruang` AUTO_INCREMENT = 1;
ALTER TABLE `lemari` AUTO_INCREMENT = 1;
ALTER TABLE `master_barang` AUTO_INCREMENT = 1;
ALTER TABLE `aset` AUTO_INCREMENT = 1;
ALTER TABLE `aset_habis_pakai` AUTO_INCREMENT = 1;
ALTER TABLE `pengadaan` AUTO_INCREMENT = 1;
ALTER TABLE `pengadaan_habis_pakai` AUTO_INCREMENT = 1;
ALTER TABLE `permintaan` AUTO_INCREMENT = 1;
ALTER TABLE `permintaan_hp` AUTO_INCREMENT = 1;
ALTER TABLE `peminjaman` AUTO_INCREMENT = 1;
ALTER TABLE `aset_bangunan` AUTO_INCREMENT = 1;
ALTER TABLE `aset_tanah` AUTO_INCREMENT = 1;

-- ============================================================
-- FASE 2: UPSERT JURUSAN (Pertahankan existing + tambah baru)
-- Existing: TKJ(1), Listrik(2), Elektro(3), Busana(4), DKV(5),
--   PPLG(6), BP(7), Mekatronika(8), Otomotif(9)
-- Baru dari Excel UNIT (valid rows only):
--   KANTIN: baru -> ID 10
--   PPLG: sudah ada ID 6 -> reuse
--   Perpus: baru -> ID 11
--   TU: baru -> ID 12
--   WK1: baru -> ID 13
--   WK2: baru -> ID 14
--   WK4: baru -> ID 15
-- Mapping final jurusan:
--   TKJ (tkj) => ID 1
--   Listrik (listrik) => ID 2
--   Elektro (elektro) => ID 3
--   Busana (busana) => ID 4
--   DKV (dkv) => ID 5
--   PPLG (pplg) => ID 6
--   BP (bp) => ID 7
--   Mekatronika (mekatronika) => ID 8
--   Otomotif (otomotif) => ID 9
--   KANTIN (kantin) => ID 10
--   Perpus (perpus) => ID 11
--   TU (tu) => ID 12
--   WK1 (wk1) => ID 13
--   WK2 (wk2) => ID 14
--   WK4 (wk4) => ID 15

INSERT IGNORE INTO `jurusan` (`id_jurusan`, `nama_jurusan`) VALUES (10, 'KANTIN');
INSERT IGNORE INTO `jurusan` (`id_jurusan`, `nama_jurusan`) VALUES (11, 'Perpus');
INSERT IGNORE INTO `jurusan` (`id_jurusan`, `nama_jurusan`) VALUES (12, 'TU');
INSERT IGNORE INTO `jurusan` (`id_jurusan`, `nama_jurusan`) VALUES (13, 'WK1');
INSERT IGNORE INTO `jurusan` (`id_jurusan`, `nama_jurusan`) VALUES (14, 'WK2');
INSERT IGNORE INTO `jurusan` (`id_jurusan`, `nama_jurusan`) VALUES (15, 'WK4');

-- ============================================================
-- FASE 3: INSERT MASTER (Master -> Child)
-- ============================================================

-- 3.1 kategori
INSERT INTO `kategori` (`id_kategori`, `nama_kategori`) VALUES (1, 'Alat');
INSERT INTO `kategori` (`id_kategori`, `nama_kategori`) VALUES (2, 'Bahan');

-- 3.2 kondisi
INSERT INTO `kondisi` (`id_kondisi`, `nama_kondisi`) VALUES (1, 'Baik');
INSERT INTO `kondisi` (`id_kondisi`, `nama_kondisi`) VALUES (2, 'Rusak');

-- 3.3 satuan (normalized)
INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES (1, 'Buah');
INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES (2, 'Pack');
INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES (3, 'Pcs');
INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES (4, 'Roll');
INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES (5, 'Set');
INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES (6, 'Unit');

-- 3.4 merek (from valid rows only, exclude -)
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (1, 'AMP');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (2, 'Acer');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (3, 'Activ Furnitur');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (4, 'Aero Cool');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (5, 'Alcatros');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (6, 'AudioBank');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (7, 'Bardi');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (8, 'Bio Finger');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (9, 'D-Link');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (10, 'Epson');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (11, 'Force');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (12, 'Gigabit');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (13, 'Google');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (14, 'HP');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (15, 'IWARE');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (16, 'Lecoo');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (17, 'Lenovo');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (18, 'Logitech');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (19, 'MIKROTIK');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (20, 'Masterindo');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (21, 'Matsuka');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (22, 'Midas Force');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (23, 'Mikrotik');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (24, 'NF');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (25, 'NYK');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (26, 'Netlink');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (27, 'Orico');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (28, 'Panasonic');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (29, 'Panda');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (30, 'Philips');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (31, 'Plustek');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (32, 'RX7');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (33, 'Raspberry');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (34, 'Razer');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (35, 'Rexus');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (36, 'Samsung');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (37, 'Sandisk');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (38, 'Sharp');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (39, 'TOPAS');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (40, 'TP-LINK');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (41, 'TP-Link');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (42, 'TPLik');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (43, 'Tixx');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (44, 'Ubiquiti');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (45, 'Unicorn');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (46, 'VIVO');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (47, 'Vention');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (48, 'Wacom');
INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES (49, 'XPG');

-- 3.5 sumber_perolehan (Asal Dana distinct, normalized)
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (1, 'APBN');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (2, 'BOS');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (3, 'BOSP');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (4, 'BPOPP');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (5, 'BPOPP-WK4');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (6, 'KANTIN');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (7, 'Komite');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (8, 'LKS');
INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES (9, 'LSP');

-- 3.6 pemasok (dummy 1)
INSERT INTO `pemasok` (`id_pemasok`, `nama_pemasok`) VALUES (1, 'Pemasok Umum');

-- 3.7 gudang
INSERT INTO `gudang` (`kode_gudang`, `nama_gudang`) VALUES ('GDG-001', 'Gudang Utama');

-- 3.8 status_barang
INSERT INTO `status_barang` (`id_status`, `nama_status`) VALUES (1, 'Aktif');
INSERT INTO `status_barang` (`id_status`, `nama_status`) VALUES (2, 'Non-Aktif');
INSERT INTO `status_barang` (`id_status`, `nama_status`) VALUES (3, 'Rusak');

-- 3.9 tahun_ajaran
INSERT INTO `tahun_ajaran` (`id_tahun_ajaran`, `tahun_ajaran`) VALUES (1, '2024/2025');
INSERT INTO `tahun_ajaran` (`id_tahun_ajaran`, `tahun_ajaran`) VALUES (2, '2025/2026');

-- 3.10 lokasi (hanya lokasi valid, exclude Dipinjam/Terpakai)
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (1, 'KANTOR_PPLG', 'Kantor PPLG');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (2, 'KELAS_X', 'Kelas X');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (3, 'KELAS_XI', 'Kelas XI');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (4, 'LAB_PPLG', 'Lab PPLG');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (5, 'LEMARI_1', 'Lemari 1');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (6, 'LEMARI_2', 'Lemari 2');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (7, 'LEMARI_4', 'Lemari 4');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (8, 'LEMARI_5', 'Lemari 5');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (9, 'NESABASTORE', 'Nesabastore');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (10, 'RUANG_PPLG_1', 'Ruang PPLG 1');
INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES (11, 'RUANG_PERPUS', 'Ruang Perpus');

-- 3.11 ruang (1 lokasi = 1 ruang)
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (1, 'KANTOR_PPLG', 1, 'Kantor PPLG', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (2, 'KELAS_X', 2, 'Kelas X', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (3, 'KELAS_XI', 3, 'Kelas XI', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (4, 'LAB_PPLG', 4, 'Lab PPLG', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (5, 'LEMARI_1', 5, 'Lemari 1', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (6, 'LEMARI_2', 6, 'Lemari 2', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (7, 'LEMARI_4', 7, 'Lemari 4', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (8, 'LEMARI_5', 8, 'Lemari 5', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (9, 'NESABASTORE', 9, 'Nesabastore', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (10, 'RUANG_PPLG_1', 10, 'Ruang PPLG 1', 1);
INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES (11, 'RUANG_PERPUS', 11, 'Ruang Perpus', 1);

-- 3.12 lemari (kode almari dari valid rows)
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (1, '1B', '1B', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (2, 'A', 'A', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (3, 'A-1', 'A-1', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (4, 'B', 'B', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (5, 'C', 'C', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (6, 'D', 'D', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (7, 'E', 'E', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (8, 'Lemari 5B', 'Lemari 5B', 1);
INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES (9, 'Lemari 5E', 'Lemari 5E', 1);

-- 3.13 master_barang (valid rows only)
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (1, 'Meja Guru', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (2, 'Meja Siswa', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (3, 'PC Core i5', 1, 14, 5, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (4, 'DVD Disk', 2, NULL, 2, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (5, 'Mini Thermal Printer', 1, 15, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (6, 'Meja Komputer', 1, 3, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (7, 'Vivo Y91C', 1, 46, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (8, 'Almari Pintu Ayun Brother B-204', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (9, 'BARCODE SCANNER', 2, NULL, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (10, 'Micro SD', 2, 37, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (11, 'Google Home Assistant Smart', 1, 13, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (12, 'Bardi IPCAM Indoor PTZ', 1, 7, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (13, 'Bardi IPCAM Indoor PTZ', 1, 7, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (14, 'Bio Finger AC-300', 1, NULL, 6, 'Aset', 'Bio Finger AC-300');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (15, 'Touchscreen 19 inch + Floor Stand', 1, 6, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (16, 'Touchscreen 19 inch + Floor Stand', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (17, 'Bracket TV', 2, NULL, 1, 'Habis Pakai', 'Untuk Monitor CCTV');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (18, 'Arduino Uno', 2, NULL, 5, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (19, 'Switch Gigabit 24 Port', 1, 12, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (20, 'Keyboard Logitech', 2, 18, 6, 'Habis Pakai', 'Terpakai di Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (21, 'Keyboard Alcatros', 2, 5, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (22, 'Keyboard Alcatros', 2, 5, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (23, 'Switch Hub', 2, 41, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (24, 'Switch Hub', 2, 41, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (25, 'Bio Finger TE-500', 1, 8, 1, 'Aset', 'milik PPLG');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (26, 'AC Panasonic', 1, 28, 6, 'Aset', 'Diganti Merk Panasonic');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (27, 'Baterai Laptop HP', 2, NULL, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (28, 'RAM Laptop', 2, 36, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (29, 'Proyektor', 1, 10, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (30, 'Bardi IR Remote', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (31, 'Bardi Portable Plug', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (32, 'Bardi Portable Plug', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (33, 'Bardi Portable Plug', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (34, 'Bardi Smart Breaker On Off', 2, 7, 1, 'Habis Pakai', 'Stok 1');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (35, 'Bardi Light WallSwitch 3 Gang EU', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (36, 'BARDI Smart Home WIFI Window & Door Sensor', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (37, 'BARDI Smart Home WIFI Siren Loud Alarm', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (38, 'Bardi Smart Light Bulb', 2, 7, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (39, 'Logitech M170 Mouse Wireless', 2, 18, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (40, 'MOUSE LENOVO M20 WIRED MOUSE USB', 2, 17, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (41, 'Bio Finger TE-600', 1, 8, 1, 'Aset', 'Salah Spek, TE-600');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (42, 'SSD Midasforce 512 Gb', 2, 22, 1, 'Habis Pakai', 'di PC Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (43, 'HDD Case 2.5"', 2, 27, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (44, 'Diganti Epson L121', 2, 10, 1, 'Habis Pakai', 'Diganti Epson L121');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (45, 'Kabel Kolor', 2, 45, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (46, 'Anycast', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (47, 'Kabel HDMI to HDMI', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (48, 'Converter HDMI to VGA', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (49, 'Chrome Cast', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (50, 'Soundcard V8', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (51, 'Laptop', 1, NULL, 6, 'Aset', 'Dipinjam Pak Wildam');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (52, 'Smartphone', 1, NULL, 3, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (53, 'Modem Mifi', 1, NULL, 3, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (54, 'Cermin', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (55, 'Bardi Wallswitch 2 gang', 1, NULL, 1, 'Aset', 'Turun 1');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (56, 'Lampu LED Strip Bardi', 1, NULL, 1, 'Aset', 'Adaptor 1pcs');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (57, 'Bardi Wallsocket', 1, NULL, 1, 'Aset', 'Untuk IOT Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (58, 'Pengkabelan', 2, NULL, 4, 'Habis Pakai', 'Untuk Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (59, 'Cable Duct', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (60, 'Air Conditioner', 2, NULL, 6, 'Habis Pakai', 'Pengajuan Asli Daikin FTC15NV14 / FTC 15 1/2 PK');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (61, 'Roller Blind', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (62, 'PC Unit', 1, NULL, 5, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (63, 'PC Rakitan', 1, NULL, 5, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (64, 'Access Point', 1, 44, 5, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (65, 'Laser Pointer Logitech', 2, NULL, 1, 'Habis Pakai', 'Dipinjam Bu Sa''diyah');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (66, 'Printer Epson', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (67, 'Projector Epson', 1, NULL, 1, 'Aset', 'Salah Spek, EBE500');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (68, 'Screen projector', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (69, 'Screen projector', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (70, 'Tripot + Layar Projector', 1, NULL, 1, 'Aset', 'Turun 2');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (71, 'Tripot + Layar Projector', 1, NULL, 1, 'Aset', 'Turun 2');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (72, 'Speaker Aktif Advance', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (73, 'Advance Portable Speaker', 1, NULL, 1, 'Aset', 'Salah Spek');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (74, 'Tinta Refill Original', 2, NULL, 1, 'Habis Pakai', 'Habis');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (75, 'Tinta Refill Original', 2, NULL, 1, 'Habis Pakai', 'Habis');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (76, 'Tinta Refill Original', 2, NULL, 1, 'Habis Pakai', 'Habis');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (77, 'Tinta Refill Original', 2, NULL, 1, 'Habis Pakai', 'Habis');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (78, 'Jakemy Obeng Set', 2, NULL, 5, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (79, 'Dispenser', 1, NULL, 5, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (80, 'Sofa Bulat Puff', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (81, 'Sewa Hosting', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (82, 'RAM PC DDR4 8Gb', 2, NULL, 1, 'Habis Pakai', 'Dipasang di PC Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (83, 'Meja bulat/coffee table', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (84, 'Kompresor Listrik Angin', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (85, 'Micro SDXC Extreme Pro', 2, NULL, 1, 'Habis Pakai', 'Dipasang di CCTV');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (86, 'Air Blow Gun', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (87, 'Mesin Bor Tembok', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (88, 'Monitor', 1, 36, 6, 'Aset', 'Samsung');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (89, 'Monitor', 1, 36, 6, 'Aset', 'Samsung');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (90, 'Keyboard Mouse Wireless', 2, 16, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (91, 'Keyboard Mouse Wireless', 2, 16, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (92, 'Stavolt', 1, NULL, 1, 'Aset', 'Rusak 10');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (93, 'Kotak Obat Lokal', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (94, 'AC Panasonic', 1, 28, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (95, 'Switch Gigabit 24 Port', 1, 42, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (96, 'Switch Gigabit 24 Port', 1, 42, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (97, 'Network Cable Tester', 1, 24, NULL, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (98, 'RAM PC DDR4 8Gb', 2, NULL, 6, 'Habis Pakai', 'Dipasang di PC Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (99, 'RAM PC DDR3 8Gb', 2, NULL, 1, 'Habis Pakai', 'Dipasang di PC Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (100, 'RAM Laptop DDR 3 8Gb', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (101, 'RAM Laptop DDR 3 8Gb', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (102, 'SSD M2 NVME', 2, 32, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (103, 'CCTV', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (104, 'CCTV', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (105, 'Baterai Laptop', 2, NULL, 1, 'Habis Pakai', 'Dipasang di Labtop');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (106, 'Adaptor Laptop', 2, NULL, 1, 'Habis Pakai', 'Dipasang di Labtop');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (107, 'Adaptor Laptop', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (108, 'Power Supply', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (109, 'Kipas Angin Dinding 16 inch', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (110, 'Kipas Angin Dinding 16 inch', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (111, 'Kipas Angin Dinding 16 inch', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (112, 'Kipas Angin Maspion 20"', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (113, 'Kipas Angin Maspion 20"', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (114, 'Switch Gigabit 24 Port', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (115, 'Switch Gigabit 24 Port', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (116, 'HDMI Splitter', 2, 25, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (117, 'USB HUB 3.0', 2, 47, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (118, 'Data Switch Printer Usb 1-4', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (119, 'Exhaust Fan 16 Inch', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (120, 'Tas Arctic Hunter Pria Ransel', 1, NULL, 1, 'Aset', 'Dipakai P Wildam');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (121, 'PC Rakitan i9 Gen12', 1, 4, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (122, 'Avo Meter Analog', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (123, 'Avo Multimeter Digital', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (124, 'Microphone Rapat (Kenwood)', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (125, 'AC Split', 1, 38, 6, 'Aset', 'Dipasang di Lab');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (126, 'AC Split', 1, 38, 6, 'Aset', 'Dipasang di Kantor');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (127, 'Webcam Razer HD', 1, 34, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (128, 'Lemari Besi', 1, 39, 3, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (129, 'Lemari Besi', 1, 39, 3, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (130, 'Lemari Besi', 1, 39, 3, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (131, 'Access Point', 1, 44, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (132, 'Access Point', 1, 44, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (133, 'Access Point', 1, 44, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (134, 'Stavolt', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (135, 'Stavolt', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (136, 'Stavolt', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (137, 'Vacum Cleaner', 1, 43, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (138, 'Meta Quest 3', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (139, 'One Pen Tablet', 1, 48, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (140, 'Gamepad Joystick Stik Gaming', 1, 35, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (141, 'Logitech M275', 2, 18, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (142, 'Bardi Smart WiFi Touch Wallswitch', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (143, 'BARDI Smart IR Remote Control 12M', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (144, 'BARDI Smart IR Remote Control 12M', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (145, 'BARDI Smart IR Remote Control 12M', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (146, 'BARDI Smart IR Remote Control 12M', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (147, 'BARDI Smart Wall Socket EU Stop Kontak', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (148, 'BARDI Smart Wall Socket EU Stop Kontak', 2, NULL, 1, 'Habis Pakai', 'Dipasang P Wildam');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (149, 'HDMI Splitter 4 Port', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (150, 'SSD Sata', 2, 11, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (151, 'SSD Sata', 2, 11, 1, 'Habis Pakai', 'Laptop HP Hitam');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (152, 'Kabel VGA', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (153, 'Kabel VGA', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (154, 'Power Supplay', 2, 49, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (155, 'Converter Hdmi Male to Vga', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (156, 'Paket Arduino Firefighter', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (157, 'Motor Gearbox Vending', 2, NULL, 5, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (158, 'ESP32 NODEMCU TIPE C', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (159, 'Kabel Jumper Male to Female', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (160, 'Kabel Jumper Male to Male', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (161, 'Kabel Jumper Female to Female', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (162, 'Push Button', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (163, 'RAM Laptop DDR3L 8Gb', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (164, 'VGA Card', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (165, 'MC KIT Arduino Nano Sensor Basic', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (166, 'Stop Kontak Isi 6', 2, NULL, 1, 'Habis Pakai', 'Untuk Kabel Koloran di Kelas');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (167, 'Kabel listrik 2x1.5', 2, NULL, 4, 'Habis Pakai', 'Untuk Kabel Koloran di Kelas');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (168, 'Steker Kepala Colokan Listrik', 2, NULL, 1, 'Habis Pakai', 'Untuk Kabel Koloran di Kelas');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (169, 'Adaptor Multi output Digital', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (170, 'Iron Tool Kit 220V 60W', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (171, 'Timah Solder', 2, NULL, 1, 'Habis Pakai', 'Dipakai Praktik');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (172, 'Loadcell Capasitas 180Kg', 2, NULL, 1, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (173, 'Televisi', 1, 36, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (174, 'Scanner Plustek', 1, 31, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (175, 'SFP Mikrotik 1.2 Gb', 2, 23, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (176, 'Meja Kayu', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (177, 'Kursi Guru', 1, 20, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (178, 'Adapter SC', 2, NULL, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (179, 'Printer Thermal', 1, NULL, 6, 'Aset', 'Dipakai Nesabastore 1');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (180, 'Printer Thermal', 1, NULL, 6, 'Aset', 'Dipakai Nesabastore 1');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (181, 'Media Converter FO', 2, 41, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (182, 'Media Converter FO - B', 2, 26, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (183, 'Raspberry', 2, 33, 6, 'Habis Pakai', 'TouchScreen');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (184, 'Flashdisk', 2, 37, 6, 'Habis Pakai', 'Stok 20');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (185, 'Paper roll', 2, 29, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (186, 'Paper roll', 2, 29, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (187, 'Paper roll', 2, NULL, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (188, 'Crimping RJ45', 2, 1, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (189, 'Raspberry', 2, 33, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (190, 'Direct Sfp+', 2, NULL, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (191, 'Rfid card', 2, NULL, 3, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (192, 'Rfid reader', 2, NULL, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (193, 'Crimping RJ45', 2, NULL, 6, 'Habis Pakai', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (194, 'Switch Mikrotik', 1, 23, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (195, 'ONT Ufiber', 1, NULL, 6, 'Aset', 'milik Aula');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (196, 'Unifi U-6 Lite', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (197, 'Unifi AC Lite', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (198, 'Lampu', 2, 21, 6, 'Habis Pakai', 'Stok 3');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (199, 'Lampu', 2, 30, 6, 'Habis Pakai', '-');
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (200, 'Kursi SIswa', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (201, 'Meja Guru', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (202, 'Kursi Guru', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (203, 'Meja Siswa', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (204, 'Kursi SIswa', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (205, 'Papan Tulis', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (206, 'Papan Tulis', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (207, 'Papan Tulis', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (208, 'Kipas Angin Maspion', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (209, 'CCTV', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (210, 'Kipas Angin Maspion', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (211, 'CCTV', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (212, 'Meja Guru', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (213, 'Kursi Guru', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (214, 'Meja Siswa', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (215, 'Kursi SIswa', 1, NULL, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (216, 'Stavolt', 1, NULL, 1, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (217, 'PC UNIT Monitor', 1, 2, NULL, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (218, 'Modem', 1, 9, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (219, 'Laptop', 1, 2, 6, 'Aset', NULL);
INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES (220, 'Router Board', 1, 23, 6, 'Aset', NULL);
-- Total master_barang valid: 220

-- ============================================================
-- FASE 4: INSERT ASET (Alat -> aset, Bahan -> aset_habis_pakai)
-- Hanya dari baris valid (kategori Alat/Bahan)
-- Jurusan mapping: Excel UNIT -> jurusan_final_map (safe IDs)
-- ============================================================

-- Total Alat (aset) valid: 105
-- Total Bahan (aset_habis_pakai) valid: 115
-- Total invalid/null kategori ignored: 18 (tidak dimigrasi)

-- 4.1 aset (Alat)
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (1, 'AST-0001', 1, NULL, 2, NULL, 2, 'INV-0001', 'Aktif', 1, '1996-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (2, 'AST-0002', 2, NULL, 2, NULL, 2, 'INV-0002', 'Aktif', 1, '2007-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (3, 'AST-0003', 3, 13, 1, NULL, 1, 'INV-0003', 'Aktif', 1, '2016-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (4, 'AST-0004', 5, NULL, 8, 5, 8, 'INV-0004', 'Aktif', 1, '2018-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (5, 'AST-0005', 6, 11, 1, NULL, 1, 'INV-0005', 'Aktif', 1, '2018-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (6, 'AST-0006', 7, 6, 1, NULL, 1, 'INV-0006', 'Aktif', 1, '2019-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (7, 'AST-0007', 8, NULL, 11, NULL, 11, 'INV-0007', 'Aktif', 1, '2021-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (8, 'AST-0008', 11, NULL, 5, 2, 5, 'INV-0008', 'Aktif', 1, '2021-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (9, 'AST-0009', 12, NULL, 2, NULL, 2, 'INV-0009', 'Aktif', 1, '2021-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (10, 'AST-0010', 13, NULL, 3, NULL, 3, 'INV-0010', 'Aktif', 1, '2021-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (11, 'AST-0011', 14, NULL, 5, 6, 5, 'INV-0011', 'Aktif', 1, '2021-01-01', 'Bio Finger AC-300');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (12, 'AST-0012', 15, 6, 4, NULL, 4, 'INV-0012', 'Aktif', 1, '2021-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (13, 'AST-0013', 16, 15, 4, NULL, 4, 'INV-0013', 'Aktif', 1, '2021-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (14, 'AST-0014', 19, 15, 4, NULL, 4, 'INV-0014', 'Aktif', 1, '2021-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (15, 'AST-0015', 25, NULL, 5, 6, 5, 'INV-0015', 'Aktif', 1, '2022-01-01', 'milik PPLG');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (16, 'AST-0016', 26, 6, 2, NULL, 2, 'INV-0016', 'Aktif', 1, '2022-01-01', 'Diganti Merk Panasonic');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (17, 'AST-0017', 29, NULL, 6, 4, 6, 'INV-0017', 'Aktif', 1, '2022-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (18, 'AST-0018', 41, NULL, 5, 6, 5, 'INV-0018', 'Aktif', 1, '2022-01-01', 'Salah Spek, TE-600');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (19, 'AST-0019', 51, NULL, NULL, NULL, NULL, 'INV-0019', 'Dipinjam', 1, '2022-01-01', 'Dipinjam Pak Wildam');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (20, 'AST-0020', 52, NULL, 1, NULL, 1, 'INV-0020', 'Aktif', 1, '2022-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (21, 'AST-0021', 53, NULL, 1, NULL, 1, 'INV-0021', 'Aktif', 1, '2022-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (22, 'AST-0022', 55, NULL, 4, 3, 4, 'INV-0022', 'Aktif', 1, '2022-01-01', 'Turun 1');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (23, 'AST-0023', 56, NULL, 8, 2, 8, 'INV-0023', 'Aktif', 1, '2022-01-01', 'Adaptor 1pcs');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (24, 'AST-0024', 57, NULL, NULL, NULL, NULL, 'INV-0024', 'Aktif', 1, '2022-01-01', 'Untuk IOT Lab');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (25, 'AST-0025', 62, 6, 4, NULL, 4, 'INV-0025', 'Aktif', 1, '2022-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (26, 'AST-0026', 63, 6, 4, NULL, 4, 'INV-0026', 'Aktif', 1, '2022-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (27, 'AST-0027', 64, 15, 2, 3, 2, 'INV-0027', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (28, 'AST-0028', 66, NULL, 1, NULL, 1, 'INV-0028', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (29, 'AST-0029', 67, NULL, 6, 4, 6, 'INV-0029', 'Aktif', 1, '2023-01-01', 'Salah Spek, EBE500');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (30, 'AST-0030', 68, NULL, 2, NULL, 2, 'INV-0030', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (31, 'AST-0031', 69, NULL, 3, NULL, 3, 'INV-0031', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (32, 'AST-0032', 70, NULL, 3, NULL, 3, 'INV-0032', 'Aktif', 1, '2023-01-01', 'Turun 2');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (33, 'AST-0033', 71, NULL, 4, NULL, 4, 'INV-0033', 'Aktif', 1, '2023-01-01', 'Turun 2');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (34, 'AST-0034', 73, NULL, 1, NULL, 1, 'INV-0034', 'Aktif', 1, '2023-01-01', 'Salah Spek');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (35, 'AST-0035', 79, 6, 1, NULL, 1, 'INV-0035', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (36, 'AST-0036', 84, NULL, 4, NULL, 4, 'INV-0036', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (37, 'AST-0037', 87, NULL, 7, NULL, 7, 'INV-0037', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (38, 'AST-0038', 88, NULL, 4, NULL, 4, 'INV-0038', 'Aktif', 1, '2023-01-01', 'Samsung');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (39, 'AST-0039', 89, NULL, 5, 6, 5, 'INV-0039', 'Aktif', 1, '2023-01-01', 'Samsung');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (40, 'AST-0040', 92, NULL, 4, NULL, 4, 'INV-0040', 'Aktif', 1, '2023-01-01', 'Rusak 10');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (41, 'AST-0041', 93, 14, 1, NULL, 1, 'INV-0041', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (42, 'AST-0042', 94, 15, 4, NULL, 4, 'INV-0042', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (43, 'AST-0043', 95, 15, 4, NULL, 4, 'INV-0043', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (44, 'AST-0044', 96, 15, 5, NULL, 5, 'INV-0044', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (45, 'AST-0045', 97, 15, 8, 5, 8, 'INV-0045', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (46, 'AST-0046', 103, 6, 1, NULL, 1, 'INV-0046', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (47, 'AST-0047', 104, 6, 11, NULL, 11, 'INV-0047', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (48, 'AST-0048', 109, 6, 2, NULL, 2, 'INV-0048', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (49, 'AST-0049', 110, 6, 3, NULL, 3, 'INV-0049', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (50, 'AST-0050', 111, 6, 11, NULL, 11, 'INV-0050', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (51, 'AST-0051', 112, 6, 4, NULL, 4, 'INV-0051', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (52, 'AST-0052', 113, 6, 2, NULL, 2, 'INV-0052', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (53, 'AST-0053', 114, 6, 5, 1, 5, 'INV-0053', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (54, 'AST-0054', 115, 6, 4, NULL, 4, 'INV-0054', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (55, 'AST-0055', 119, NULL, 3, NULL, 3, 'INV-0055', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (56, 'AST-0056', 120, NULL, NULL, NULL, NULL, 'INV-0056', 'Aktif', 1, '2024-01-01', 'Dipakai P Wildam');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (57, 'AST-0057', 121, 6, 1, NULL, 1, 'INV-0057', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (58, 'AST-0058', 124, NULL, 8, 6, 8, 'INV-0058', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (59, 'AST-0059', 125, NULL, 4, NULL, 4, 'INV-0059', 'Aktif', 1, '2024-01-01', 'Dipasang di Lab');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (60, 'AST-0060', 126, NULL, 1, NULL, 1, 'INV-0060', 'Aktif', 1, '2024-01-01', 'Dipasang di Kantor');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (61, 'AST-0061', 127, 6, 1, NULL, 1, 'INV-0061', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (62, 'AST-0062', 128, 15, 2, NULL, 2, 'INV-0062', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (63, 'AST-0063', 129, 15, 3, NULL, 3, 'INV-0063', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (64, 'AST-0064', 130, 15, 4, NULL, 4, 'INV-0064', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (65, 'AST-0065', 131, 15, 3, NULL, 3, 'INV-0065', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (66, 'AST-0066', 132, 15, 4, NULL, 4, 'INV-0066', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (67, 'AST-0067', 133, 15, 5, 2, 5, 'INV-0067', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (68, 'AST-0068', 134, 15, 4, NULL, 4, 'INV-0068', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (69, 'AST-0069', 135, 15, 11, NULL, 11, 'INV-0069', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (70, 'AST-0070', 136, 15, 5, 6, 5, 'INV-0070', 'Aktif', 1, '2024-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (71, 'AST-0071', 137, 6, 1, NULL, 1, 'INV-0071', 'Aktif', 1, '2025-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (72, 'AST-0072', 138, NULL, 1, NULL, 1, 'INV-0072', 'Aktif', 1, '2025-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (73, 'AST-0073', 139, NULL, 8, 4, 8, 'INV-0073', 'Aktif', 1, '2025-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (74, 'AST-0074', 140, NULL, 1, NULL, 1, 'INV-0074', 'Aktif', 1, '2025-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (75, 'AST-0075', 173, 6, 1, NULL, 1, 'INV-0075', 'Aktif', 1, '2025-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (76, 'AST-0076', 174, 15, 1, NULL, 1, 'INV-0076', 'Aktif', 1, '2025-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (77, 'AST-0077', 176, 12, 1, NULL, 1, 'INV-0077', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (78, 'AST-0078', 177, NULL, 2, NULL, 2, 'INV-0078', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (79, 'AST-0079', 179, 10, 5, 5, 5, 'INV-0079', 'Aktif', 1, NULL, 'Dipakai Nesabastore 1');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (80, 'AST-0080', 180, 10, 9, 5, 9, 'INV-0080', 'Aktif', 1, NULL, 'Dipakai Nesabastore 1');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (81, 'AST-0081', 194, NULL, 11, NULL, 11, 'INV-0081', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (82, 'AST-0082', 195, NULL, 5, 2, 5, 'INV-0082', 'Aktif', 1, NULL, 'milik Aula');
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (83, 'AST-0083', 196, NULL, 5, 2, 5, 'INV-0083', 'Aktif', 1, '2022-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (84, 'AST-0084', 197, NULL, 5, 2, 5, 'INV-0084', 'Aktif', 1, '2023-01-01', NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (85, 'AST-0085', 200, NULL, 2, NULL, 2, 'INV-0085', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (86, 'AST-0086', 201, NULL, 3, NULL, 3, 'INV-0086', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (87, 'AST-0087', 202, NULL, 3, NULL, 3, 'INV-0087', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (88, 'AST-0088', 203, NULL, 3, NULL, 3, 'INV-0088', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (89, 'AST-0089', 204, NULL, 3, NULL, 3, 'INV-0089', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (90, 'AST-0090', 205, NULL, 2, NULL, 2, 'INV-0090', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (91, 'AST-0091', 206, NULL, 3, NULL, 3, 'INV-0091', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (92, 'AST-0092', 207, NULL, 4, NULL, 4, 'INV-0092', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (93, 'AST-0093', 208, NULL, 3, NULL, 3, 'INV-0093', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (94, 'AST-0094', 209, 15, 3, NULL, 3, 'INV-0094', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (95, 'AST-0095', 210, NULL, 4, NULL, 4, 'INV-0095', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (96, 'AST-0096', 211, 15, 4, NULL, 4, 'INV-0096', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (97, 'AST-0097', 212, NULL, 4, NULL, 4, 'INV-0097', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (98, 'AST-0098', 213, NULL, 4, NULL, 4, 'INV-0098', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (99, 'AST-0099', 214, NULL, 4, NULL, 4, 'INV-0099', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (100, 'AST-0100', 215, NULL, 4, NULL, 4, 'INV-0100', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (101, 'AST-0101', 216, 15, 4, NULL, 4, 'INV-0101', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (102, 'AST-0102', 217, NULL, 5, NULL, 5, 'INV-0102', 'Aktif', NULL, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (103, 'AST-0103', 218, 6, 5, 2, 5, 'INV-0103', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (104, 'AST-0104', 219, 13, 6, 5, 6, 'INV-0104', 'Aktif', 1, NULL, NULL);
INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES (105, 'AST-0105', 220, 15, 5, 2, 5, 'INV-0105', 'Aktif', 1, '2023-01-01', NULL);

-- 4.2 aset_habis_pakai (Bahan)
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0001', 4, 1, 47, NULL, 8, 8, 'Aktif', '2017-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0002', 9, 2, 2, NULL, 8, 8, 'Aktif', '2021-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0003', 10, 1, 1, NULL, 5, 5, 'Aktif', '2021-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0004', 17, 1, 1, NULL, 1, 1, 'Aktif', '2021-01-01', 'Untuk Monitor CCTV', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0005', 18, 1, 1, NULL, NULL, NULL, 'Aktif', '2021-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0006', 20, 5, 5, NULL, NULL, NULL, 'Aktif', '2022-01-01', 'Terpakai di Lab', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0007', 21, 8, 8, NULL, 5, 5, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0008', 22, 4, 4, NULL, 4, 4, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0009', 23, 1, 1, 15, 5, 5, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0010', 24, 1, 1, 15, 1, 1, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0011', 27, 1, 1, NULL, 5, 5, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0012', 28, 8, 8, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0013', 30, 2, 2, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0014', 31, 1, 1, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0015', 32, 1, 1, NULL, NULL, NULL, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0016', 33, 1, 1, NULL, NULL, NULL, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0017', 34, 1, 1, NULL, 8, 8, 'Aktif', '2022-01-01', 'Stok 1', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0018', 35, 1, 1, NULL, 4, 4, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0019', 36, 1, 1, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0020', 37, 1, 1, NULL, NULL, NULL, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0021', 38, 4, 4, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0022', 39, 4, 4, NULL, NULL, NULL, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0023', 40, 10, 10, NULL, NULL, NULL, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0024', 42, 1, 1, NULL, NULL, NULL, 'Aktif', '2022-01-01', 'di PC Lab', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0025', 43, 2, 2, NULL, NULL, NULL, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0026', 44, 1, 1, NULL, 4, 4, 'Aktif', '2022-01-01', 'Diganti Epson L121', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0027', 45, 10, 10, NULL, NULL, NULL, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0028', 46, 2, 2, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0029', 47, 1, 1, NULL, 4, 4, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0030', 48, 1, 1, NULL, 4, 4, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0031', 49, 1, 1, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0032', 50, 1, 1, NULL, 8, 8, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0033', 54, 2, 2, NULL, 4, 4, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0034', 58, 1, 1, NULL, NULL, NULL, 'Aktif', '2022-01-01', 'Untuk Lab', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0035', 59, 8, 8, NULL, 4, 4, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0036', 60, 2, 2, NULL, 10, 10, 'Aktif', '2022-01-01', 'Pengajuan Asli Daikin FTC15NV14 / FTC 15 1/2 PK', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0037', 61, 2, 2, NULL, 1, 1, 'Aktif', '2022-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0038', 65, 1, 1, NULL, 1, 1, 'Aktif', '2023-01-01', 'Dipinjam Bu Sa''diyah', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0039', 72, 1, 1, NULL, 1, 1, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0040', 74, 3, 3, NULL, NULL, NULL, 'Aktif', '2023-01-01', 'Habis', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0041', 75, 1, 1, NULL, NULL, NULL, 'Aktif', '2023-01-01', 'Habis', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0042', 76, 1, 1, NULL, NULL, NULL, 'Aktif', '2023-01-01', 'Habis', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0043', 77, 1, 1, NULL, NULL, NULL, 'Aktif', '2023-01-01', 'Habis', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0044', 78, 2, 2, 6, 1, 1, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0045', 80, 4, 1, 6, 1, 1, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0046', 81, 1, 1, NULL, NULL, NULL, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0047', 82, 11, 11, NULL, NULL, NULL, 'Aktif', '2023-01-01', 'Dipasang di PC Lab', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0048', 83, 1, 1, 6, 1, 1, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0049', 85, 1, 1, NULL, NULL, NULL, 'Aktif', '2023-01-01', 'Dipasang di CCTV', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0050', 86, 1, 1, NULL, NULL, NULL, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0051', 90, 1, 3, NULL, 1, 1, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0052', 91, 3, 3, NULL, 5, 5, 'Aktif', '2023-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0053', 98, 5, 0, NULL, NULL, NULL, 'Aktif', '2024-01-01', 'Dipasang di PC Lab', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0054', 99, 6, 0, NULL, NULL, NULL, 'Aktif', '2024-01-01', 'Dipasang di PC Lab', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0055', 100, 1, 1, NULL, NULL, NULL, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0056', 101, 4, 5, NULL, 8, 8, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0057', 102, 5, 5, NULL, 8, 8, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0058', 105, 10, 0, NULL, NULL, NULL, 'Aktif', '2024-01-01', 'Dipasang di Labtop', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0059', 106, 5, 5, NULL, NULL, NULL, 'Aktif', '2024-01-01', 'Dipasang di Labtop', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0060', 107, 5, 5, NULL, 8, 8, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0061', 108, 5, 5, NULL, 1, 1, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0062', 116, 1, 1, NULL, 8, 8, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0063', 117, 1, 1, NULL, 8, 8, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0064', 118, 1, 0, NULL, 1, 1, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0065', 122, 1, 1, NULL, 5, 5, 'Aktif', '2024-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0066', 123, 1, 1, NULL, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0067', 141, 1, 1, NULL, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0068', 142, 4, 4, NULL, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0069', 143, 1, 1, 6, 1, 1, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0070', 144, 1, 1, 6, 4, 4, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0071', 145, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0072', 146, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0073', 147, 3, 3, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0074', 148, 1, 1, NULL, NULL, NULL, 'Aktif', '2025-01-01', 'Dipasang P Wildam', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0075', 149, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0076', 150, 8, 8, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0077', 151, 1, 1, 6, NULL, NULL, 'Aktif', '2025-01-01', 'Laptop HP Hitam', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0078', 152, 9, 9, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0079', 153, 1, 1, 6, NULL, NULL, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0080', 154, 4, 4, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0081', 155, 5, 5, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0082', 156, 1, 1, 6, NULL, NULL, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0083', 157, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0084', 158, 2, 2, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0085', 159, 2, 2, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0086', 160, 2, 2, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0087', 161, 2, 2, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0088', 162, 2, 2, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0089', 163, 5, 5, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0090', 164, 5, 5, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0091', 165, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0092', 166, 5, 5, 6, NULL, NULL, 'Aktif', '2025-01-01', 'Untuk Kabel Koloran di Kelas', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0093', 167, 1, 1, 6, NULL, NULL, 'Aktif', '2025-01-01', 'Untuk Kabel Koloran di Kelas', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0094', 168, 5, 5, 6, NULL, NULL, 'Aktif', '2025-01-01', 'Untuk Kabel Koloran di Kelas', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0095', 169, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0096', 170, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0097', 171, 1, 1, 6, NULL, NULL, 'Aktif', '2025-01-01', 'Dipakai Praktik', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0098', 172, 1, 1, 6, 8, 8, 'Aktif', '2025-01-01', NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0099', 175, 4, 4, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0100', 178, 10, 10, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0101', 181, 2, 2, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0102', 182, 4, 4, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0103', 183, 3, 3, NULL, NULL, NULL, 'Aktif', NULL, 'TouchScreen', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0104', 184, 40, 18, NULL, NULL, NULL, 'Aktif', NULL, 'Stok 20', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0105', 185, 30, 15, NULL, 8, 8, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0106', 186, 10, 15, NULL, NULL, NULL, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0107', 187, 1, 1, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0108', 188, 1, 1, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0109', 189, 4, 4, NULL, 8, 8, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0110', 190, 1, 4, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0111', 191, 20, 20, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0112', 192, 2, 2, NULL, 8, 8, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0113', 193, 1, 1, NULL, 5, 5, 'Aktif', NULL, NULL, 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0114', 198, 3, 3, NULL, 8, 8, 'Aktif', NULL, 'Stok 3', 0);
INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-0115', 199, 3, 3, NULL, NULL, NULL, 'Aktif', NULL, '-', 0);

-- ============================================================
-- FASE 5: VALIDASI & AKTIFKAN FK
-- ============================================================

-- Cek orphan sebelum aktifkan FK:
-- SELECT * FROM aset WHERE id_jurusan NOT IN (SELECT id_jurusan FROM jurusan) AND id_jurusan IS NOT NULL;
-- SELECT * FROM aset_habis_pakai WHERE id_jurusan NOT IN (SELECT id_jurusan FROM jurusan) AND id_jurusan IS NOT NULL;
-- SELECT * FROM ruang WHERE id_lokasi NOT IN (SELECT id_lokasi FROM lokasi) AND id_lokasi IS NOT NULL;
-- SELECT * FROM lemari WHERE id_ruang NOT IN (SELECT id_ruang FROM ruang) AND id_ruang IS NOT NULL;
-- SELECT * FROM aset WHERE id_lokasi NOT IN (SELECT id_lokasi FROM lokasi) AND id_lokasi IS NOT NULL;

SET FOREIGN_KEY_CHECKS = 1;
SET SQL_MODE = @OLD_SQL_MODE;

-- ============================================================
-- SELESAI MIGRASI V3 SAFE
-- ============================================================
-- RINGKASAN:
-- - Excel total rows: 238
-- - Valid rows migrated: 220 (Alat 105 + Bahan 115)
-- - Invalid rows ignored: 18 (kategori null/kosong)
-- - System tables preserved: pengguna 18, peran 17, akses 41, peran_akses 41, pengaturan 5
-- - System-related preserved: jurusan 9 existing + 6 baru = 15 total
-- - Master baru: kategori 2, merek 49, satuan 6, kondisi 2, lokasi 11, ruang 11, lemari 9, master_barang 220
-- - Aset baru: aset 105, aset_habis_pakai 115
-- - Tables deleted: 40 (inventaris only)
-- - FK safe: semua id_jurusan valid [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]
-- - Jika butuh id_pengguna untuk transaksi masa depan, gunakan 1=admin (Sahrul Ramadhan)
-- ============================================================