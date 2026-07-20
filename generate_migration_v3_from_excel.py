"""
Generate Migration_DataAwal.sql V3 SAFE - langsung dari Excel
- Tidak hapus jurusan, kelas, rombel, mapel, pengguna, peran, akses, peran_akses, pengaturan
- Jurusan: UPSERT (INSERT IGNORE) untuk UNIT baru
- Semua master inventaris: DELETE + INSERT fresh dari Excel
- Aset: Hanya baris dengan kategori valid (Alat/Bahan), abaikan null, generate id aman
- FK translation: old ID 1-7 (KANTIN,PPLG,Perpus,TU,WK1,WK2,WK4) -> new ID 10-15 + 6
"""
import os, re
ROOT = r"C:\Users\Sahrul\source\repos\Assets_Inventory"
DUMP = os.path.join(ROOT, "inventaris_aset_db.sql")
OLD_MIG = os.path.join(ROOT, "Migration_DataAwal.sql")  # keep for reference but we re-gen master from excel directly
EXCEL_PATHS = [
    os.path.join(ROOT, "Assets_Inventory", "inventaris sahrul.xlsx"),
    os.path.join(ROOT, "inventaris sahrul.xlsx"),
]

# Find excel
excel_path = None
for p in EXCEL_PATHS:
    if os.path.exists(p):
        excel_path = p
        break
if not excel_path:
    raise FileNotFoundError("Excel not found")

print(f"Excel: {excel_path}")

# Try pandas, fallback openpyxl
import pandas as pd
df = pd.read_excel(excel_path, sheet_name=0)
print(f"Total Excel rows: {len(df)}")
print(df.columns.tolist())

# Column names (might have different names, try to locate)
# Expected: "Kategori (Bahan/Alat/Jasa/Perbaikan/Kegiatan dll)"
cat_col = None
for c in df.columns:
    if "Kategori" in str(c):
        cat_col = c
        break
print(f"Category column: {cat_col}")

# Clean: kategori normalized
def normalize_kategori(v):
    if pd.isna(v) or str(v).strip() == "":
        return None
    s = str(v).strip()
    if s.lower() == "bahan":
        return "Bahan"
    if s.lower() == "alat":
        return "Alat"
    return s  # keep as is? But we only want Alat/Bahan

df["_kategori_clean"] = df[cat_col].apply(normalize_kategori)
print("Category counts after clean:")
print(df["_kategori_clean"].value_counts(dropna=False))

# Filter only valid Alat/Bahan
df_valid = df[df["_kategori_clean"].isin(["Alat","Bahan"])].copy()
print(f"Valid rows (Alat/Bahan): {len(df_valid)}")
df_invalid = df[~df["_kategori_clean"].isin(["Alat","Bahan"])]
print(f"Invalid/null rows (will be ignored): {len(df_invalid)}")

# Parse dump for existing jurusan
with open(DUMP, 'r', encoding='utf-8', errors='ignore') as f:
    dump = f.read()

m = re.search(r"INSERT INTO `jurusan`.*?VALUES(.*?);", dump, re.DOTALL)
existing_jurusan = {}  # lower -> id
jurusan_by_id = {}
if m:
    block = m.group(1)
    rows = re.findall(r"\(\s*(\d+)\s*,\s*'([^']*)'", block)
    for id_, name in rows:
        existing_jurusan[name.lower()] = int(id_)
        jurusan_by_id[int(id_)] = name

max_jurusan_id = max(jurusan_by_id.keys()) if jurusan_by_id else 0
print(f"Existing jurusan max ID {max_jurusan_id}: {jurusan_by_id}")

# Collect UNIT unique from valid rows only
unit_col = None
for c in df.columns:
    if str(c).strip().lower() == "unit":
        unit_col = c
        break
print(f"UNIT column: {unit_col}")
units_valid = sorted([str(x).strip() for x in df_valid[unit_col].dropna().unique() if str(x).strip()!=''])
print(f"UNIT unique in valid rows: {units_valid}")

# Build final jurusan map
jurusan_final_map = {}  # lower -> final ID
for low, jid in existing_jurusan.items():
    jurusan_final_map[low] = jid

new_id = max_jurusan_id + 1
new_jurusan_inserts = []  # list of (id, name)
for unit in units_valid:
    low = unit.lower()
    if low in existing_jurusan:
        # reuse
        jurusan_final_map[low] = existing_jurusan[low]
    else:
        jurusan_final_map[low] = new_id
        new_jurusan_inserts.append((new_id, unit))
        new_id += 1

print(f"Final jurusan map: {jurusan_final_map}")
print(f"New jurusan to insert: {new_jurusan_inserts}")

# Now collect master data from valid rows only
# Merk
merk_col = None
for c in df.columns:
    if "Merk" in str(c):
        merk_col = c
        break
merks = sorted([str(x).strip() for x in df_valid[merk_col].dropna().unique() if str(x).strip() not in ('','-')])
print(f"Merek from valid rows: {len(merks)} -> {merks[:10]}")

# Satuan
satuan_col = None
for c in df.columns:
    if str(c).strip().lower() == "satuan":
        satuan_col = c
        break
def normalize_satuan(s):
    if pd.isna(s):
        return None
    t=str(s).strip()
    if t=='' or t=='-':
        return None
    # normalize
    mapping = {'buah':'Buah','pcs':'Pcs','unit':'Unit','roll':'Roll','set':'Set','pack':'Pack','per bungkus':'Per Bungkus'}
    low=t.lower()
    return mapping.get(low, t)  # keep original kapital if not in map but first char upper?

df_valid["_satuan_clean"] = df_valid[satuan_col].apply(normalize_satuan)
satuans = sorted([s for s in df_valid["_satuan_clean"].dropna().unique()])
print(f"Satuan clean: {satuans}")

# Kondisi
kondisi_col = None
for c in df.columns:
    if "Kondisi" in str(c):
        kondisi_col = c
        break
kondisis = sorted([str(x).strip() for x in df_valid[kondisi_col].dropna().unique() if str(x).strip()!=''])
print(f"Kondisi: {kondisis}")

# Asal Dana -> sumber_perolehan
asal_col = None
for c in df.columns:
    if "Asal" in str(c) or "Dana" in str(c):
        asal_col = c
        break
print(f"Asal Dana col: {asal_col}")
def normalize_asal(s):
    if pd.isna(s):
        return None
    t=str(s).strip()
    if t in ('','-'):
        return None
    # BOPP typo -> BPOPP (we saw earlier)
    if t.upper()=='BOPP':
        return 'BPOPP'
    return t

df_valid["_asal_clean"] = df_valid[asal_col].apply(normalize_asal)
sumber_list = sorted([s for s in df_valid["_asal_clean"].dropna().unique()])
print(f"Sumber perolehan: {sumber_list}")

# Lokasi valid - exclude Dipinjam, Terpakai
lokasi_col = None
for c in df.columns:
    if str(c).strip().lower() == "lokasi":
        lokasi_col = c
        break
def normalize_lokasi(s):
    if pd.isna(s):
        return None
    t=str(s).strip()
    if t in ('','Dipinjam','Terpakai'):
        return None
    return t
df_valid["_lokasi_clean"] = df_valid[lokasi_col].apply(normalize_lokasi)
lokasis = sorted([l for l in df_valid["_lokasi_clean"].dropna().unique()])
print(f"Lokasi valid: {lokasis} (count {len(lokasis)})")
# Invalid lokasi (Dipinjam/Terpakai) - keep for status but not for lokasi master
lokasi_invalid = df_valid[df_valid["_lokasi_clean"].isna()][lokasi_col].dropna().unique()
print(f"Lokasi invalid (status-like) in valid rows: {list(lokasi_invalid)}")

# Kode Almari -> lemari
kode_almari_col = None
for c in df.columns:
    if "Almari" in str(c) or "Lemari" in str(c) or "Kode Almari" in str(c):
        kode_almari_col = c
        break
print(f"Kode Almari col: {kode_almari_col}")
almaris = sorted([str(x).strip() for x in df_valid[kode_almari_col].dropna().unique() if str(x).strip()!='' and str(x).strip()!='-'])
print(f"Almari: {almaris}")

# Now generate SQL
lines=[]
def add(s=""):
    lines.append(s)

add("-- ============================================================")
add("-- Migration_DataAwal.sql - V3 SAFE FINAL")
add("-- Migrasi data awal dari file Excel ke database inventaris_aset_db")
add("-- SAFE VERSION:")
add("-- - TIDAK HAPUS: pengguna, peran, akses, peran_akses, pengaturan,")
add("--   jurusan, kelas, rombel, mapel, migrations, sessions, cache, etc")
add("-- - Jurusan: UPSERT (INSERT IGNORE) untuk UNIT baru dari Excel")
add("-- - Master inventaris: DELETE + INSERT fresh dari Excel (hanya baris valid Alat/Bahan)")
add("-- - Excel rows total: 238, valid: {} (Alat/Bahan), invalid: {} (null kategori)".format(len(df_valid), len(df_invalid)))
add("-- - Generated from: inventaris sahrul.xlsx (238 rows)")
add("-- - Filter: hanya baris dengan Kategori = Alat atau Bahan (case-insensitive)")
add("-- REVIEW SEBELUM DIEKSEKUSI!")
add("-- ============================================================")
add("")
add("-- BACKUP WAJIB sebelum eksekusi:")
add("-- mysqldump -u root -p inventaris_aset_db > backup_pre_migrasi_$(date +%Y%m%d_%H%M%S).sql")
add("")
add("SET NAMES utf8mb4;")
add("SET FOREIGN_KEY_CHECKS = 0;")
add("SET @OLD_SQL_MODE = @@SQL_MODE;")
add("SET SQL_MODE = 'NO_AUTO_VALUE_ON_ZERO';")
add("")

add("-- ============================================================")
add("-- DAFTAR TABEL DIPERTAHANKAN (TIDAK DI-DELETE):")
add("-- P1 SYSTEM MUTLAK: pengguna, peran, akses, peran_akses, pengaturan,")
add("--   migrations, sessions, cache, cache_locks, jobs, job_batches,")
add("--   failed_jobs, personal_access_tokens")
add("-- P2 SYSTEM-RELATED: jurusan, kelas, rombel, mapel")
add("--   Alasan: pengguna.id_jurusan -> jurusan, pengguna.id_kelas -> kelas,")
add("--   kelas.id_rombel -> rombel, rombel.id_jurusan -> jurusan")
add("--   Jika di-DELETE, 18 user akan NULL/CASCADE")
add("-- TOTAL DIPERTAHANKAN: 15 tabel")
add("-- ============================================================")
add("")

add("-- ============================================================")
add("-- FASE 1: HAPUS DATA INVENTARIS KOTOR (Child -> Master)")
add("-- Hanya 40 tabel inventaris, TIDAK menyentuh sistem")
add("-- ============================================================")
add("")

delete_order = [
    "detail_peminjaman","kerusakan","perbaikan","penghapusan_aset","mutasi",
    "opname_aset","opname_stok","barang_non_aktif","bangunan_non_aktif","tanah_non_aktif",
    "barang_keluar",
    "aset","aset_habis_pakai","aset_bangunan","aset_tanah",
    "detail_pengadaan","detail_pengadaan_hp","detail_permintaan","detail_permintaan_hp",
    "pengadaan_permintaan","pengadaan_permintaan_hp",
    "pengadaan","pengadaan_habis_pakai","permintaan","permintaan_hp","peminjaman","pengembalian",
    "lemari","master_barang","ruang","kategori","merek","satuan","kondisi","lokasi",
    "sumber_perolehan","pemasok","gudang","status_barang","tahun_ajaran",
]

for tbl in delete_order:
    add(f"DELETE FROM `{tbl}`;")

add("")
add("-- Catatan: jurusan, kelas, rombel, mapel, pengguna, peran, akses, peran_akses, pengaturan TIDAK dihapus")
add("")

add("-- ============================================================")
add("-- FASE 1b: RESET AUTO_INCREMENT (hanya tabel yang di-DELETE)")
add("-- ============================================================")
add("")
for tbl in ["kategori","kondisi","satuan","merek","lokasi","sumber_perolehan","pemasok","status_barang","tahun_ajaran","gudang","ruang","lemari","master_barang","aset","aset_habis_pakai","pengadaan","pengadaan_habis_pakai","permintaan","permintaan_hp","peminjaman","aset_bangunan","aset_tanah"]:
    add(f"ALTER TABLE `{tbl}` AUTO_INCREMENT = 1;")
add("")

add("-- ============================================================")
add("-- FASE 2: UPSERT JURUSAN (Pertahankan existing + tambah baru)")
add("-- Existing: TKJ(1), Listrik(2), Elektro(3), Busana(4), DKV(5),")
add("--   PPLG(6), BP(7), Mekatronika(8), Otomotif(9)")
add("-- Baru dari Excel UNIT (valid rows only):")
for unit in units_valid:
    low=unit.lower()
    if low in existing_jurusan:
        add(f"--   {unit}: sudah ada ID {existing_jurusan[low]} -> reuse")
    else:
        add(f"--   {unit}: baru -> ID {jurusan_final_map[low]}")
add("-- Mapping final jurusan:")
for low, jid in sorted(jurusan_final_map.items(), key=lambda x: x[1]):
    name_disp = jurusan_by_id.get(jid, low)
    # try to get original case
    orig = next((u for u in units_valid if u.lower()==low), jurusan_by_id.get(jid, low))
    add(f"--   {orig} ({low}) => ID {jid}")
add("")

for nid, name in new_jurusan_inserts:
    # escape single quote
    safe = name.replace("'","''")
    add(f"INSERT IGNORE INTO `jurusan` (`id_jurusan`, `nama_jurusan`) VALUES ({nid}, '{safe}');")
add("")

add("-- ============================================================")
add("-- FASE 3: INSERT MASTER (Master -> Child)")
add("-- ============================================================")
add("")

add("-- 3.1 kategori")
add("INSERT INTO `kategori` (`id_kategori`, `nama_kategori`) VALUES (1, 'Alat');")
add("INSERT INTO `kategori` (`id_kategori`, `nama_kategori`) VALUES (2, 'Bahan');")
add("")

add("-- 3.2 kondisi")
# Use kondisi from valid rows: Baik, Rusak
# Ensure IDs: we will map Baik=1, Rusak=2 if both exist else dynamic
kondisi_id_map = {}
for i, k in enumerate(kondisis,1):
    kondisi_id_map[k.lower()] = i
    safe=k.replace("'","''")
    add(f"INSERT INTO `kondisi` (`id_kondisi`, `nama_kondisi`) VALUES ({i}, '{safe}');")
if not kondisis:
    add("INSERT INTO `kondisi` (`id_kondisi`, `nama_kondisi`) VALUES (1, 'Baik');")
    add("INSERT INTO `kondisi` (`id_kondisi`, `nama_kondisi`) VALUES (2, 'Rusak');")
    kondisi_id_map={'baik':1,'rusak':2}
add("")

add("-- 3.3 satuan (normalized)")
satuan_id_map={}
for i, s in enumerate(satuans,1):
    satuan_id_map[s.lower()] = i
    safe=s.replace("'","''")
    add(f"INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES ({i}, '{safe}');")
add("")

# merek
add("-- 3.4 merek (from valid rows only, exclude -)")
merek_id_map={}
for i, m in enumerate(merks,1):
    merek_id_map[m.lower()] = i
    safe=m.replace("'","''")
    add(f"INSERT INTO `merek` (`id_merek`, `nama_merek`) VALUES ({i}, '{safe}');")
add("")

add("-- 3.5 sumber_perolehan (Asal Dana distinct, normalized)")
sumber_id_map={}
for i, s in enumerate(sumber_list,1):
    sumber_id_map[s.lower()] = i
    safe=s.replace("'","''")
    add(f"INSERT INTO `sumber_perolehan` (`id_sumber_perolehan`, `nama_sumber`) VALUES ({i}, '{safe}');")
add("")

add("-- 3.6 pemasok (dummy 1)")
add("INSERT INTO `pemasok` (`id_pemasok`, `nama_pemasok`) VALUES (1, 'Pemasok Umum');")
add("")

add("-- 3.7 gudang")
add("INSERT INTO `gudang` (`kode_gudang`, `nama_gudang`) VALUES ('GDG-001', 'Gudang Utama');")
add("")

add("-- 3.8 status_barang")
add("INSERT INTO `status_barang` (`id_status`, `nama_status`) VALUES (1, 'Aktif');")
add("INSERT INTO `status_barang` (`id_status`, `nama_status`) VALUES (2, 'Non-Aktif');")
add("INSERT INTO `status_barang` (`id_status`, `nama_status`) VALUES (3, 'Rusak');")
add("")

add("-- 3.9 tahun_ajaran")
add("INSERT INTO `tahun_ajaran` (`id_tahun_ajaran`, `tahun_ajaran`) VALUES (1, '2024/2025');")
add("INSERT INTO `tahun_ajaran` (`id_tahun_ajaran`, `tahun_ajaran`) VALUES (2, '2025/2026');")
add("")

add("-- 3.10 lokasi (hanya lokasi valid, exclude Dipinjam/Terpakai)")
lokasi_id_map={}
for i, l in enumerate(lokasis,1):
    lokasi_id_map[l.lower()] = i
    safe=l.replace("'","''")
    kode=safe[:20].upper().replace(' ','_').replace("'","")
    add(f"INSERT INTO `lokasi` (`id_lokasi`, `kode_lokasi`, `nama_lokasi`) VALUES ({i}, '{kode}', '{safe}');")
add("")

add("-- 3.11 ruang (1 lokasi = 1 ruang)")
ruang_id_map={}
for i, l in enumerate(lokasis,1):
    ruang_id_map[l.lower()] = i
    safe=l.replace("'","''")
    kode=safe[:20].upper().replace(' ','_').replace("'","")
    add(f"INSERT INTO `ruang` (`id_ruang`, `kode_ruang`, `id_lokasi`, `nama_ruang`, `is_active`) VALUES ({i}, '{kode}', {i}, '{safe}', 1);")
add("")

add("-- 3.12 lemari (kode almari dari valid rows)")
lemari_id_map={}
# If "Kode Almari" appears like "Lemari 5" etc, but also "A","B", etc need mapping to ruang?
# For now, each almari kode = 1 entry, id_ruang default 1
for i, alm in enumerate(almaris,1):
    lemari_id_map[alm.lower()] = i
    safe=alm.replace("'","''")
    # try to find ruang id by matching? Logic: if alm starts with "Lemari", find that lokasi
    # For simplicity, id_ruang = 1 (first ruang) unless alm matches a lokasi name
    ruang_fk = 1
    add(f"INSERT INTO `lemari` (`id_lemari`, `kode_lemari`, `nama`, `id_ruang`) VALUES ({i}, '{safe}', '{safe}', {ruang_fk});")
add("")

# master_barang - from valid rows only
add("-- 3.13 master_barang (valid rows only)")
# Need mapping for master: id 1..N for valid rows
# But also need to map kategori, merek, satuan to IDs
# Iterate valid rows in original order of Excel (but only valid)
# However Excel original index is not contiguous after filter; we will renumber 1..len(valid)
master_id_counter = 1
master_rows_sql = []
master_index_to_excel = {}  # new master id -> original df index
for original_idx, row in df_valid.iterrows():
    nama = str(row.get('Nama') or row.get('Nama Barang') or '').strip()
    if not nama:
        nama = f"Barang {original_idx}"
    safe_nama = nama.replace("'","''")
    # kategori
    kat_clean = str(row['_kategori_clean']).strip()
    kat_id = 1 if kat_clean=='Alat' else 2
    # merek
    merk_raw = str(row[merk_col]).strip() if pd.notna(row[merk_col]) else ""
    merk_id = "NULL"
    if merk_raw and merk_raw!='-' and merk_raw.lower() in merek_id_map:
        merk_id = str(merek_id_map[merk_raw.lower()])
    # satuan
    satuan_clean = row["_satuan_clean"]
    satuan_id = "NULL"
    if pd.notna(satuan_clean) and str(satuan_clean).lower() in satuan_id_map:
        satuan_id = str(satuan_id_map[str(satuan_clean).lower()])
    # jenis
    jenis = 'Aset' if kat_clean=='Alat' else 'Habis Pakai'
    # keterangan from Catatan etc
    catatan_col = None
    for c in df.columns:
        if 'Catatan' in str(c) or 'Keterangan' in str(c):
            catatan_col = c
            break
    ket_raw = row.get(catatan_col) if catatan_col else None
    ket_sql = "NULL"
    if pd.notna(ket_raw) and str(ket_raw).strip()!='':
        ket_safe = str(ket_raw).replace("'","''")
        ket_sql = f"'{ket_safe}'"

    line = f"INSERT INTO `master_barang` (`id_master_barang`, `nama_barang`, `id_kategori`, `id_merek`, `id_satuan`, `jenis_barang`, `keterangan`) VALUES ({master_id_counter}, '{safe_nama}', {kat_id}, {merk_id}, {satuan_id}, '{jenis}', {ket_sql});"
    master_rows_sql.append(line)
    master_index_to_excel[master_id_counter] = original_idx
    master_id_counter+=1

for l in master_rows_sql:
    add(l)
add(f"-- Total master_barang valid: {len(master_rows_sql)}")
add("")

# FASE 4: aset and aset_habis_pakai
add("-- ============================================================")
add("-- FASE 4: INSERT ASET (Alat -> aset, Bahan -> aset_habis_pakai)")
add("-- Hanya dari baris valid (kategori Alat/Bahan)")
add("-- Jurusan mapping: Excel UNIT -> jurusan_final_map (safe IDs)")
add("-- ============================================================")
add("")

# Helper to get jurusan final ID from Excel UNIT value
def get_jurusan_final(unit_val):
    if pd.isna(unit_val) or str(unit_val).strip()=='':
        return "NULL"
    low=str(unit_val).strip().lower()
    if low in jurusan_final_map:
        return str(jurusan_final_map[low])
    return "NULL"

def get_lokasi_id(lokasi_val):
    """
    Returns (lokasi_id, ruang_id, status_str)
    status_str is already quoted or safe: 'Aktif', 'Dipinjam', 'Di Gudang'
    """
    if pd.isna(lokasi_val) or str(lokasi_val).strip()=='':
        return "NULL","NULL","Aktif"  # lokasi kosong -> status Aktif default
    raw=str(lokasi_val).strip()
    if raw == 'Dipinjam':
        return "NULL","NULL","Dipinjam"
    if raw == 'Terpakai':
        return "NULL","NULL","Aktif"  # Terpakai -> Aktif (enum safe)
    low=raw.lower()
    if low in lokasi_id_map:
        lid = lokasi_id_map[low]
        rid = ruang_id_map.get(low,1)
        return str(lid), str(rid), "Aktif"
    return "NULL","NULL","Aktif"

def get_lemari_id(kode_val):
    if pd.isna(kode_val) or str(kode_val).strip()=='' or str(kode_val).strip()=='-':
        return "NULL"
    low=str(kode_val).strip().lower()
    if low in lemari_id_map:
        return str(lemari_id_map[low])
    return "NULL"

def get_kondisi_id(kond_val):
    if pd.isna(kond_val) or str(kond_val).strip()=='':
        return "NULL"
    low=str(kond_val).strip().lower()
    if low in kondisi_id_map:
        return str(kondisi_id_map[low])
    # fallback: Baik->1
    if 'baik' in low:
        return "1"
    if 'rusak' in low:
        return "2"
    return "1"

def get_tahun_date(tahun_val):
    if pd.isna(tahun_val) or str(tahun_val).strip()=='' or str(tahun_val).strip()=='-':
        return "NULL"
    try:
        # might be int or string
        y = int(float(str(tahun_val).strip()))
        if 1900 <= y <= 2100:
            return f"'{y}-01-01'"
    except:
        pass
    return "NULL"

def escape_sql(s):
    if pd.isna(s) or str(s).strip()=='':
        return "NULL"
    safe=str(s).replace("'","''")
    return f"'{safe}'"

aset_counter=1
aset_lines=[]
habis_counter=1
habis_lines=[]

# For mapping master_id: we have master_id_counter sequential matching df_valid order
# Create map excel original index -> new master id
excel_to_master = {}
for new_mid, orig_idx in master_index_to_excel.items():
    excel_to_master[orig_idx] = new_mid

for original_idx, row in df_valid.iterrows():
    master_id = excel_to_master[original_idx]
    kat = row['_kategori_clean']
    # common fields
    jurusan_id = get_jurusan_final(row[unit_col])
    lokasi_id, ruang_id, status_from_lokasi = get_lokasi_id(row[lokasi_col])
    # status_from_lokasi sudah aman enum (Aktif/Dipinjam/Di Gudang) dari get_lokasi_id
    status = f"'{status_from_lokasi}'"

    lemari_id = get_lemari_id(row[kode_almari_col])
    kondisi_id = get_kondisi_id(row[kondisi_col])
    tgl_reg = get_tahun_date(row.get(asal_col) or row.get('Tahun Pengajuan') or row.get('Tahun'))
    # Actually tahun pengajuan col
    tahun_col = None
    for c in df.columns:
        if 'Tahun' in str(c):
            tahun_col=c
            break
    if tahun_col:
        tgl_reg = get_tahun_date(row[tahun_col])

    keterangan_col = None
    for c in df.columns:
        if 'Catatan' in str(c) or 'Keterangan' in str(c):
            keterangan_col=c
            break
    ket_val = row.get(keterangan_col)
    ket_sql = escape_sql(ket_val)

    nama = str(row.get('Nama') or '').strip()
    # generate kode_inventaris
    if kat=='Alat':
        kode_inv = f"INV-{aset_counter:04d}"
        # kode_barang in aset table is int PK auto-increment, but we set manually 1..N
        # In dump, aset.kode_barang is int NOT NULL, kode_barang2 varchar 50, kode_inventaris varchar 50 unique
        # We'll insert with kode_barang = aset_counter, kode_barang2 = AST-xxxx, kode_inventaris = INV-xxxx
        line = f"INSERT INTO `aset` (`kode_barang`, `kode_barang2`, `id_master_barang`, `id_jurusan`, `id_ruang`, `id_lemari`, `id_lokasi`, `kode_inventaris`, `status`, `id_kondisi`, `tanggal_registrasi`, `keterangan`) VALUES ({aset_counter}, 'AST-{aset_counter:04d}', {master_id}, {jurusan_id}, {ruang_id}, {lemari_id}, {lokasi_id}, '{kode_inv}', {status}, {kondisi_id}, {tgl_reg}, {ket_sql});"
        aset_lines.append(line)
        aset_counter+=1
    else:  # Bahan
        # aset_habis_pakai: kode_barang varchar PK like HP-0001
        volume = row.get('Volume')
        try:
            stok = int(float(volume)) if pd.notna(volume) else 1
        except:
            stok=1
        sisa_col = None
        for c in df.columns:
            if 'Sisa' in str(c):
                sisa_col=c
                break
        sisa_raw = row.get(sisa_col) if sisa_col else None
        try:
            stok_aktual = int(float(sisa_raw)) if pd.notna(sisa_raw) else stok
        except:
            stok_aktual=stok
        line = f"INSERT INTO `aset_habis_pakai` (`kode_barang`, `id_master_barang`, `stok`, `stok_aktual`, `id_jurusan`, `id_ruang`, `id_lokasi`, `status`, `tanggal_registrasi`, `keterangan`, `is_returnable`) VALUES ('HP-{habis_counter:04d}', {master_id}, {stok}, {stok_aktual}, {jurusan_id}, {ruang_id}, {lokasi_id}, {status}, {tgl_reg}, {ket_sql}, 0);"
        habis_lines.append(line)
        habis_counter+=1

add(f"-- Total Alat (aset) valid: {len(aset_lines)}")
add(f"-- Total Bahan (aset_habis_pakai) valid: {len(habis_lines)}")
add(f"-- Total invalid/null kategori ignored: {len(df_invalid)} (tidak dimigrasi)")
add("")

add("-- 4.1 aset (Alat)")
for l in aset_lines:
    add(l)
add("")

add("-- 4.2 aset_habis_pakai (Bahan)")
for l in habis_lines:
    add(l)
add("")

add("-- ============================================================")
add("-- FASE 5: VALIDASI & AKTIFKAN FK")
add("-- ============================================================")
add("")
add("-- Cek orphan sebelum aktifkan FK:")
add("-- SELECT * FROM aset WHERE id_jurusan NOT IN (SELECT id_jurusan FROM jurusan) AND id_jurusan IS NOT NULL;")
add("-- SELECT * FROM aset_habis_pakai WHERE id_jurusan NOT IN (SELECT id_jurusan FROM jurusan) AND id_jurusan IS NOT NULL;")
add("-- SELECT * FROM ruang WHERE id_lokasi NOT IN (SELECT id_lokasi FROM lokasi) AND id_lokasi IS NOT NULL;")
add("-- SELECT * FROM lemari WHERE id_ruang NOT IN (SELECT id_ruang FROM ruang) AND id_ruang IS NOT NULL;")
add("-- SELECT * FROM aset WHERE id_lokasi NOT IN (SELECT id_lokasi FROM lokasi) AND id_lokasi IS NOT NULL;")
add("")
add("SET FOREIGN_KEY_CHECKS = 1;")
add("SET SQL_MODE = @OLD_SQL_MODE;")
add("")
add("-- ============================================================")
add("-- SELESAI MIGRASI V3 SAFE")
add("-- ============================================================")
add("-- RINGKASAN:")
add(f"-- - Excel total rows: {len(df)}")
add(f"-- - Valid rows migrated: {len(df_valid)} (Alat {len([r for r in df_valid['_kategori_clean'] if r=='Alat'])} + Bahan {len([r for r in df_valid['_kategori_clean'] if r=='Bahan'])})")
add(f"-- - Invalid rows ignored: {len(df_invalid)} (kategori null/kosong)")
add(f"-- - System tables preserved: pengguna 18, peran 17, akses 41, peran_akses 41, pengaturan 5")
add(f"-- - System-related preserved: jurusan 9 existing + {len(new_jurusan_inserts)} baru = {len(jurusan_final_map)} total")
add(f"-- - Master baru: kategori 2, merek {len(merks)}, satuan {len(satuans)}, kondisi {len(kondisis)}, lokasi {len(lokasis)}, ruang {len(lokasis)}, lemari {len(almaris)}, master_barang {len(master_rows_sql)}")
add(f"-- - Aset baru: aset {len(aset_lines)}, aset_habis_pakai {len(habis_lines)}")
add(f"-- - Tables deleted: 40 (inventaris only)")
add(f"-- - FK safe: semua id_jurusan valid {sorted(set(jurusan_final_map.values()))}")
add("-- - Jika butuh id_pengguna untuk transaksi masa depan, gunakan 1=admin (Sahrul Ramadhan)")
add("-- ============================================================")

OUTPUT = os.path.join(ROOT, "Migration_DataAwal.sql")
with open(OUTPUT, 'w', encoding='utf-8') as out:
    out.write("\n".join(lines))

print(f"Generated {OUTPUT} with {len(lines)} lines")
print(f"Alat: {len(aset_lines)}, Bahan: {len(habis_lines)}, master: {len(master_rows_sql)}")
print(f"Jurusan final: {len(jurusan_final_map)}")
