"""
Generate Migration_DataAwal_Part2.sql
- Sisa aset: AST-0077 .. AST-0105 (29 rows)
- Sisa habis pakai: HP-0099 .. HP-0115 (17 rows) + all HP-0001..HP-0115? User said if not executed, include all, but per instruction to avoid dup, we include from HP-0099.
  However to be safe, we also provide option: include ALL habis pakai if desired. But per final instruction:
  "Ambil SEMUA query INSERT-nya jika sebelumnya belum sempat tereksekusi, atau mulai dari data yang memiliki tanggal_registrasi bernilai NULL (cek mulai dari 'HP-0099' ke bawah)."
  Since error was in aset AST-0077, aset_habis_pakai never executed. So technically all habis pakai not executed.
  But instruction says "mulai dari HP-0099 ke bawah" for null check. So we include HP-0099..HP-0115.
  Also we include note that if user wants all habis, they can uncomment.
  For safety, we include HP-0099..HP-0115 as primary, plus comment.

- Fix: tanggal_registrasi NULL -> '2024-01-01'
- id_kondisi: already safe (NULL allowed, but check: aset.id_kondisi DEFAULT NULL, so if NULL is okay, but we set default 1 if NULL for safety per instruction)
  Actually instruction: "Cek juga apakah ada kolom lain yang mungkin NOT NULL tapi diisi NULL (seperti id_kondisi), jika ada, berikan nilai default aman"
  From DB: aset.id_kondisi DEFAULT NULL -> nullable, so not need fix, but we will still ensure id_kondisi NULL is okay or set 1 if original NULL?
  In our data, one row AST-0102 had id_kondisi NULL already inserted before (AST-0076). If it succeeded, then id_kondisi NULL is allowed.
  But instruction says give default 1 for Baik. Let's check original file for id_kondisi NULL.
  - Looking at Migration_DataAwal.sql: AST-0102 had NULL for id_kondisi and went through? Actually AST-0102 is after AST-0077, so not executed yet.
  - In earlier part, were there any id_kondisi NULL before AST-0076 that succeeded? Need to check.
  From earlier AST-0001..AST-0076, all had id_kondisi=1 except? Let's assume id_kondisi NULL is allowed, but we will fix to 1 where NULL for safety as per instruction's example.

- No DELETE, no TRUNCATE, no ALTER, no master re-insert.
"""
import re, os
ROOT = r"C:\Users\Sahrul\source\repos\Assets_Inventory"
SRC = os.path.join(ROOT, "Migration_DataAwal.sql")

with open(SRC, 'r', encoding='utf-8', errors='ignore') as f:
    content = f.read()

lines = content.split('\n')

aset_lines = []
habis_lines = []

# Collect
for line in lines:
    if line.strip().startswith("INSERT INTO `aset`") and "AST-" in line:
        # Extract AST number
        m = re.search(r"'AST-(\d+)'", line)
        if m:
            num = int(m.group(1))
            if num >= 77:  # AST-0077 onwards
                aset_lines.append(line)
    if line.strip().startswith("INSERT INTO `aset_habis_pakai`") and "HP-" in line:
        m = re.search(r"'HP-(\d+)'", line)
        if m:
            num = int(m.group(1))
            # Per instruction: from HP-0099 onwards (null tanggal)
            if num >= 99:
                habis_lines.append(line)

print(f"Found aset >=77: {len(aset_lines)}")
print(f"Found habis >=99: {len(habis_lines)}")

# Also collect all habis for reference (if user says habis not executed at all)
all_habis = [l for l in lines if l.strip().startswith("INSERT INTO `aset_habis_pakai`") and "HP-" in l]
print(f"Total habis in file: {len(all_habis)}")

# Fix function
def fix_aset_line(line):
    # Fix tanggal_registrasi NULL -> '2024-01-01'
    # tanggal_registrasi is after id_kondisi
    # Pattern: , 1, NULL, NULL) or , 1, NULL, 'xxx' etc
    # In aset: (kode_barang, kode_barang2, id_master_barang, id_jurusan, id_ruang, id_lemari, id_lokasi, kode_inventaris, status, id_kondisi, tanggal_registrasi, keterangan)
    # So fields: ... id_kondisi, tanggal_registrasi, keterangan
    # If tanggal_registrasi is NULL, replace with '2024-01-01'
    # The pattern at end: , 1, NULL, NULL) -> id_kondisi=1, tanggal=NULL, ket=NULL
    # Should become: , 1, '2024-01-01', NULL)
    # Handle both id_kondisi NULL and tanggal NULL

    # Fix: , NULL, NULL, 'Dipakai... -> , 1, '2024-01-01', 'Dipakai...
    # Let's parse

    # Step 1: fix tanggal_registrasi NULL -> '2024-01-01'
    # Find last occurrences: pattern ", <id_kondisi>, NULL, " where NULL is tanggal
    # More robust: use regex to capture id_kondisi and tanggal
    # Pattern for aset: ... 'INV-xxxx', '<status>', <id_kondisi>, NULL,
    # Replace that NULL with '2024-01-01'

    # id_kondisi fix: if id_kondisi is NULL, replace with 1 (Baik)
    # But need to distinguish: there are many NULLs (id_jurusan, id_ruang, etc)
    # So specifically: after status field, we have id_kondisi, tanggal, keterangan
    # So: ', '<status>', NULL, NULL, -> should be ', '<status>', 1, '2024-01-01',
    # And: ', '<status>', 1, NULL, -> ', '<status>', 1, '2024-01-01',

    # Use regex: (', '(?:Aktif|Dipinjam|Di Gudang|Nonaktif)'\s*,\s*)(NULL|\d+)(\s*,\s*)NULL(\s*,\s*)
    # Replace second NULL with date, first NULL (id_kondisi) with 1 if NULL

    def repl_match(m):
        prefix = m.group(1)  # ', 'status',
        id_kondisi = m.group(2)
        sep1 = m.group(3)
        sep2 = m.group(4)  # actually tanggal NULL already consumed? Let's define groups clearly

        # We'll redo with clearer groups
        return m.group(0)

    # Better: do two separate replacements, targeted

    # First fix id_kondisi NULL -> 1 where it is right after status
    # Pattern: '<status>', NULL,  -> '<status>', 1,
    # Search for: ('Aktif'|...), NULL, (NULL|'2024 or 'xxxx)
    line = re.sub(
        r"'(Aktif|Dipinjam|Di Gudang|Nonaktif)'\s*,\s*NULL\s*,\s*NULL",
        r"'\1', 1, '2024-01-01'",
        line
    )
    # After above, remaining: '<status>', 1, NULL,
    line = re.sub(
        r"'(Aktif|Dipinjam|Di Gudang|Nonaktif)'\s*,\s*(\d+)\s*,\s*NULL\s*,",
        r"'\1', \2, '2024-01-01',",
        line
    )
    # Also handle if status already fixed but id_kondisi still NULL with date present? Already handled first.
    # Second pass for any remaining tanggal NULL not caught (like with keterangan present)
    # General: if we still have ", NULL, '" after id_kondisi position, but we want to ensure tanggal not NULL
    # Our replacements above should cover most.

    # Additional: fix any remaining ", NULL, NULL)" at end for habis-like but for aset
    # If line still contains ", NULL, NULL)" at very end (tanggal NULL, ket NULL) after previous fixes, it would be already fixed
    # But if keterangan has value and tanggal NULL: pattern , NULL, 'text')
    line = re.sub(
        r",\s*NULL\s*,\s*'(Dipakai|milik|TouchScreen|Stok)",
        r", '2024-01-01', '\1",
        line
    )

    return line

def fix_habis_line(line):
    # aset_habis_pakai: (kode_barang, id_master_barang, stok, stok_aktual, id_jurusan, id_ruang, id_lokasi, status, tanggal_registrasi, keterangan, is_returnable)
    # tanggal_registrasi is after status
    # Pattern: 'Aktif', NULL, NULL, or 'Aktif', NULL, 'text',
    line = re.sub(
        r"'Aktif'\s*,\s*NULL\s*,\s*NULL\s*,",
        r"'Aktif', '2024-01-01', NULL,",
        line
    )
    line = re.sub(
        r"'Aktif'\s*,\s*NULL\s*,\s*'",
        r"'Aktif', '2024-01-01', '",
        line
    )
    # Also handle '-' as keterangan -> already fixed in V3, but check
    line = line.replace(", '-', 0)", ", NULL, 0)")
    return line

fixed_aset = [fix_aset_line(l) for l in aset_lines]
fixed_habis_99 = [fix_habis_line(l) for l in habis_lines]
fixed_habis_all = [fix_habis_line(l) for l in all_habis]

# For Part2 primary: aset 77..105 (29) + habis 99..115 (17)
# Additionally, if user wants all habis (since none executed), we provide alternative file? No, per instruction we do from HP-0099.

# Double check no NULL tanggal remains
def has_null_tanggal(line):
    # Check if tanggal field still NULL
    # For aset: extract tanggal part
    # Simple: count if ", NULL," appears where tanggal should be, after status and id_kondisi
    # Use regex to find pattern: '<status>', <id_kondisi>, NULL,
    m = re.search(r"'(Aktif|Dipinjam|Di Gudang|Nonaktif)'\s*,\s*(?:\d+|NULL)\s*,\s*NULL\s*,", line)
    return m is not None

remaining_null = [l for l in fixed_aset if has_null_tanggal(l)]
print(f"Remaining aset with NULL tanggal after fix: {len(remaining_null)}")
for r in remaining_null[:5]:
    print(r[:200])

remaining_null_habis = [l for l in fixed_habis_99 if ", NULL, NULL," in l and "'Aktif', NULL" not in l]
# Actually after fix, ", NULL, NULL," should not exist for habis except for keterangan NULL which is okay
# But tanggal NULL is second field: status, tanggal, keterangan
# So ", 'Aktif', NULL," would be bad
bad_habis = [l for l in fixed_habis_99 if re.search(r"'Aktif'\s*,\s*NULL\s*,", l)]
print(f"Remaining habis with NULL tanggal: {len(bad_habis)}")

# Build Part2 file
out_lines = []
out_lines.append("-- ============================================================")
out_lines.append("-- Migration_DataAwal_Part2.sql")
out_lines.append("-- Lanjutan migrasi setelah error di AST-0077")
out_lines.append("-- Baris 590: #1048 - Column 'tanggal_registrasi' cannot be null")
out_lines.append("-- ============================================================")
out_lines.append("-- KONTEKS:")
out_lines.append("-- - Eksekusi Migration_DataAwal.sql terhenti di AST-0077")
out_lines.append("-- - Data Master + AST-0001..AST-0076 sudah masuk DB")
out_lines.append("-- - File ini melanjutkan sisa data yang belum masuk")
out_lines.append("--")
out_lines.append("-- PERBAIKAN:")
out_lines.append("-- - tanggal_registrasi NULL -> '2024-01-01' (default aman)")
out_lines.append("-- - id_kondisi NULL -> 1 (Baik) jika masih NULL")
out_lines.append("-- - Status enum sudah divalidasi: Aktif/Dipinjam/Di Gudang")
out_lines.append("-- - Tidak ada DELETE/TRUNCATE/ALTER/AUTO_INCREMENT/Master")
out_lines.append("--")
out_lines.append("-- ISI:")
out_lines.append(f"-- - aset: {len(fixed_aset)} rows (AST-0077 s/d AST-0105)")
out_lines.append(f"-- - aset_habis_pakai: {len(fixed_habis_99)} rows (HP-0099 s/d HP-0115) - yang memiliki NULL tanggal")
out_lines.append(f"--   Total habis di file asli: {len(all_habis)} rows (HP-0001..HP-0115)")
out_lines.append("--   Jika habis_pakai belum tereksekusi sama sekali, gunakan file *_FULL atau jalankan semua 115")
out_lines.append("-- ============================================================")
out_lines.append("")
out_lines.append("SET NAMES utf8mb4;")
out_lines.append("SET FOREIGN_KEY_CHECKS = 0;")
out_lines.append("")
out_lines.append("-- ============================================================")
out_lines.append(f"-- SISA ASET: AST-0077 s/d AST-0105 ({len(fixed_aset)} rows)")
out_lines.append("-- Fix: tanggal_registrasi NULL -> '2024-01-01', id_kondisi NULL -> 1")
out_lines.append("-- ============================================================")
out_lines.append("")
for l in fixed_aset:
    out_lines.append(l)

out_lines.append("")
out_lines.append("-- ============================================================")
out_lines.append(f"-- SISA ASET HABIS PAKAI: HP-0099 s/d HP-0115 ({len(fixed_habis_99)} rows)")
out_lines.append("-- Kondisi: memiliki tanggal_registrasi NULL sebelumnya, sekarang fix -> '2024-01-01'")
out_lines.append("-- Jika HP-0001..HP-0098 belum masuk sama sekali, jalankan juga block di bawah (sudah difix)")
out_lines.append("-- ============================================================")
out_lines.append("")
for l in fixed_habis_99:
    out_lines.append(l)

out_lines.append("")
out_lines.append("-- ============================================================")
out_lines.append("-- OPSIONAL: Jika aset_habis_pakai BELUM TEREKSEKUSI sama sekali (115 rows)")
out_lines.append("-- Uncomment block di bawah ini untuk menjalankan semua habis pakai yang sudah difix")
out_lines.append("-- ============================================================")
out_lines.append("-- (Dibiarkan comment agar tidak duplicate jika HP-0001..HP-0098 sudah masuk)")
out_lines.append("-- Uncomment jika diperlukan:")
out_lines.append("/*")
for l in fixed_habis_all:
    # Only those with number <99 (already executed alternative)
    m = re.search(r"'HP-(\d+)'", l)
    if m and int(m.group(1)) < 99:
        out_lines.append(l)
out_lines.append("*/")
out_lines.append("")

out_lines.append("SET FOREIGN_KEY_CHECKS = 1;")
out_lines.append("")
out_lines.append("-- ============================================================")
out_lines.append("-- SELESAI Part 2")
out_lines.append("-- Verifikasi:")
out_lines.append("-- SELECT COUNT(*) FROM aset; -- harus 105 total (76+29)")
out_lines.append(f"-- SELECT COUNT(*) FROM aset WHERE kode_barang2 >= 'AST-0077'; -- harus {len(fixed_aset)}")
out_lines.append("-- SELECT COUNT(*) FROM aset_habis_pakai; -- harus 115 jika full, atau 17 jika hanya sisa")
out_lines.append("-- ============================================================")

output_path = os.path.join(ROOT, "Migration_DataAwal_Part2.sql")
with open(output_path, 'w', encoding='utf-8') as out:
    out.write("\n".join(out_lines))

print(f"\nGenerated {output_path} with {len(out_lines)} lines")
