using Assets_Inventory.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Akses> Akses { get; set; }
        public virtual DbSet<Aset> Aset { get; set; }
        public virtual DbSet<AsetBangunan> AsetBangunan { get; set; }
        public virtual DbSet<AsetHabisPakai> AsetHabisPakai { get; set; }
        public virtual DbSet<AsetTanah> AsetTanah { get; set; }
        public virtual DbSet<BangunanNonAktif> BangunanNonAktif { get; set; }
        public virtual DbSet<BarangKeluar> BarangKeluar { get; set; }
        public virtual DbSet<BarangNonAktif> BarangNonAktif { get; set; }
        public virtual DbSet<Cache> Cache { get; set; }
        public virtual DbSet<CacheLocks> CacheLocks { get; set; }
        public virtual DbSet<DetailPeminjaman> DetailPeminjaman { get; set; }
        public virtual DbSet<DetailPengadaan> DetailPengadaan { get; set; }
        public virtual DbSet<DetailPengadaanHp> DetailPengadaanHp { get; set; }
        public virtual DbSet<DetailPermintaan> DetailPermintaan { get; set; }
        public virtual DbSet<DetailPermintaanHp> DetailPermintaanHp { get; set; }
        public virtual DbSet<FailedJobs> FailedJobs { get; set; }
        public virtual DbSet<Gudang> Gudang { get; set; }
        public virtual DbSet<JobBatches> JobBatches { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Jurusan> Jurusan { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kelas> Kelas { get; set; }
        public virtual DbSet<Kerusakan> Kerusakan { get; set; }
        public virtual DbSet<Kondisi> Kondisi { get; set; }
        public virtual DbSet<Lemari> Lemari { get; set; }
        public virtual DbSet<Lokasi> Lokasi { get; set; }
        public virtual DbSet<Mapel> Mapel { get; set; }
        public virtual DbSet<MasterBarang> MasterBarang { get; set; }
        public virtual DbSet<Merek> Merek { get; set; }
        public virtual DbSet<Migrations> Migrations { get; set; }
        public virtual DbSet<Mutasi> Mutasi { get; set; }
        public virtual DbSet<OpnameAset> OpnameAset { get; set; }
        public virtual DbSet<OpnameStok> OpnameStok { get; set; }
        public virtual DbSet<Pemasok> Pemasok { get; set; }
        public virtual DbSet<Peminjaman> Peminjaman { get; set; }
        public virtual DbSet<Pengadaan> Pengadaan { get; set; }
        public virtual DbSet<PengadaanHabisPakai> PengadaanHabisPakai { get; set; }
        public virtual DbSet<PengadaanPermintaan> PengadaanPermintaan { get; set; }
        public virtual DbSet<PengadaanPermintaanHp> PengadaanPermintaanHp { get; set; }
        public virtual DbSet<Pengaturan> Pengaturan { get; set; }
        public virtual DbSet<Pengembalian> Pengembalian { get; set; }
        public virtual DbSet<Pengguna> Pengguna { get; set; }
        public virtual DbSet<PenghapusanAset> PenghapusanAset { get; set; }
        public virtual DbSet<Peran> Peran { get; set; }
        public virtual DbSet<PeranAkses> PeranAkses { get; set; }
        public virtual DbSet<Perbaikan> Perbaikan { get; set; }
        public virtual DbSet<Permintaan> Permintaan { get; set; }
        public virtual DbSet<PermintaanHp> PermintaanHp { get; set; }
        public virtual DbSet<PersonalAccessTokens> PersonalAccessTokens { get; set; }
        public virtual DbSet<Rombel> Rombel { get; set; }
        public virtual DbSet<Ruang> Ruang { get; set; }
        public virtual DbSet<Satuan> Satuan { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<StatusBarang> StatusBarang { get; set; }
        public virtual DbSet<SumberPerolehan> SumberPerolehan { get; set; }
        public virtual DbSet<TahunAjaran> TahunAjaran { get; set; }
        public virtual DbSet<TanahNonAktif> TanahNonAktif { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<Notifikasi> Notifikasi { get; set; }
        public virtual DbSet<AsetLampiran> AsetLampiran { get; set; }
        public virtual DbSet<ApprovalWorkflowConfig> ApprovalWorkflowConfig { get; set; }
        public virtual DbSet<ApprovalStep> ApprovalStep { get; set; }
        public virtual DbSet<VAset> VAset { get; set; }
        public virtual DbSet<VAsetBangunan> VAsetBangunan { get; set; }
        public virtual DbSet<VAsetHabisPakai> VAsetHabisPakai { get; set; }
        public virtual DbSet<VAsetTanah> VAsetTanah { get; set; }
        public virtual DbSet<VBangunanNonAktif> VBangunanNonAktif { get; set; }
        public virtual DbSet<VBarangKeluar> VBarangKeluar { get; set; }
        public virtual DbSet<VBarangNonAktif> VBarangNonAktif { get; set; }
        public virtual DbSet<VLaporanKerusakan> VLaporanKerusakan { get; set; }
        public virtual DbSet<VLaporanPeminjaman> VLaporanPeminjaman { get; set; }
        public virtual DbSet<VLaporanPengembalian> VLaporanPengembalian { get; set; }
        public virtual DbSet<VLaporanPerbaikan> VLaporanPerbaikan { get; set; }
        public virtual DbSet<VLaporanPermintaan> VLaporanPermintaan { get; set; }
        public virtual DbSet<VMutasi> VMutasi { get; set; }
        public virtual DbSet<VOpnameAset> VOpnameAset { get; set; }
        public virtual DbSet<VPengadaanAset> VPengadaanAset { get; set; }
        public virtual DbSet<VPenghapusanAset> VPenghapusanAset { get; set; }
        public virtual DbSet<VTanahNonAktif> VTanahNonAktif { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    // Decrypt via protector (supports legacy plaintext)
                    string connString = ConnectionStringProtector.GetDecryptedConnectionString();
                    if (string.IsNullOrEmpty(connString))
                    {
                        // Fallback to raw for error handling
                        connString = ConfigurationManager.ConnectionStrings["KoneksiServer"]?.ConnectionString;
                    }
                    // Validate & enforce safe defaults
                    if (!string.IsNullOrEmpty(connString))
                    {
                        // Ensure AllowLoadLocalInfile=false even if config tampered
                        var b = new System.Data.Common.DbConnectionStringBuilder { ConnectionString = connString };
                        // Do not allow dangerous options
                        if (b.TryGetValue("AllowLoadLocalInfile", out _))
                            b["AllowLoadLocalInfile"] = "false";
                        else
                            b["AllowLoadLocalInfile"] = "false";
                        connString = b.ConnectionString;
                        optionsBuilder.UseMySql(connString);
                    }
                }
                catch
                {
                    // Let EF handle connection error normally (will throw later)
                    string raw = ConfigurationManager.ConnectionStrings["KoneksiServer"]?.ConnectionString;
                    if (!string.IsNullOrEmpty(raw))
                    {
                        string maybeDecrypted = ConnectionStringProtector.Unprotect(raw);
                        if (maybeDecrypted != null)
                            raw = maybeDecrypted;
                        optionsBuilder.UseMySql(raw);
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Akses>(entity =>
            {
                entity.HasKey(e => e.IdAkses)
                    .HasName("PRIMARY");

                entity.ToTable("akses");

                entity.HasIndex(e => e.IdParent)
                    .HasName("fk_akses_parent");

                entity.Property(e => e.IdAkses).HasColumnName("id_akses");

                entity.Property(e => e.IdParent).HasColumnName("id_parent");

                entity.Property(e => e.NamaModul)
                    .IsRequired()
                    .HasColumnName("nama_modul")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany(p => p.InverseIdParentNavigation)
                    .HasForeignKey(d => d.IdParent)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_akses_parent");
            });

            modelBuilder.Entity<Aset>(entity =>
            {
                entity.HasKey(e => e.KodeBarang)
                    .HasName("PRIMARY");

                entity.ToTable("aset");

                entity.HasIndex(e => e.IdDetailPengadaan)
                    .HasName("fk_aset_detail_pengadaan");

                entity.HasIndex(e => e.IdJurusan)
                    .HasName("fk_aset_jurusan");

                entity.HasIndex(e => e.IdKondisi)
                    .HasName("fk_aset_kondisi");

                entity.HasIndex(e => e.IdLemari)
                    .HasName("fk_aset_lemari");

                entity.HasIndex(e => e.IdLokasi)
                    .HasName("fk_aset_lokasi");

                entity.HasIndex(e => e.IdMasterBarang)
                    .HasName("id_master_barang");

                entity.HasIndex(e => e.IdRuang)
                    .HasName("fk_aset_ruang");

                entity.HasIndex(e => e.KodeInventaris)
                    .HasName("uk_aset_kode_inventaris")
                    .IsUnique();

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.Gambar)
                    .HasColumnName("gambar")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.HargaSatuan)
                    .HasColumnName("harga_satuan")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.IdDetailPengadaan).HasColumnName("id_detail_pengadaan");

                entity.Property(e => e.IdJurusan).HasColumnName("id_jurusan");

                entity.Property(e => e.IdKondisi).HasColumnName("id_kondisi");

                entity.Property(e => e.IdLemari).HasColumnName("id_lemari");

                entity.Property(e => e.IdLokasi).HasColumnName("id_lokasi");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.IdRuang).HasColumnName("id_ruang");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBarang2)
                    .IsRequired()
                    .HasColumnName("kode_barang2")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NilaiResidu)
                    .HasColumnName("nilai_residu")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.NoSeri)
                    .HasColumnName("no_seri")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NomorRak)
                    .HasColumnName("nomor_rak")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif')")
                    .HasDefaultValueSql("'Di Gudang'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalRegistrasi)
                    .HasColumnName("tanggal_registrasi")
                    .HasColumnType("date");

                entity.Property(e => e.UmurEkonomi).HasColumnName("umur_ekonomi");

                entity.HasOne(d => d.IdDetailPengadaanNavigation)
                    .WithMany(p => p.Aset)
                    .HasForeignKey(d => d.IdDetailPengadaan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_aset_detail_pengadaan");

                entity.HasOne(d => d.IdJurusanNavigation)
                    .WithMany(p => p.Aset)
                    .HasForeignKey(d => d.IdJurusan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_aset_jurusan");

                entity.HasOne(d => d.IdKondisiNavigation)
                    .WithMany(p => p.Aset)
                    .HasForeignKey(d => d.IdKondisi)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_aset_kondisi");

                entity.HasOne(d => d.IdLemariNavigation)
                    .WithMany(p => p.Aset)
                    .HasForeignKey(d => d.IdLemari)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_aset_lemari");

                entity.HasOne(d => d.IdLokasiNavigation)
                    .WithMany(p => p.Aset)
                    .HasForeignKey(d => d.IdLokasi)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_aset_lokasi");

                entity.HasOne(d => d.IdMasterBarangNavigation)
                    .WithMany(p => p.Aset)
                    .HasForeignKey(d => d.IdMasterBarang)
                    .HasConstraintName("aset_ibfk_1");

                entity.HasOne(d => d.IdRuangNavigation)
                    .WithMany(p => p.Aset)
                    .HasForeignKey(d => d.IdRuang)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_aset_ruang");
            });

            modelBuilder.Entity<AsetBangunan>(entity =>
            {
                entity.HasKey(e => e.KodeBangunan)
                    .HasName("PRIMARY");

                entity.ToTable("aset_bangunan");

                entity.HasIndex(e => e.IdKondisi)
                    .HasName("fk_bangunan_kondisi");

                entity.Property(e => e.KodeBangunan).HasColumnName("kode_bangunan");

                entity.Property(e => e.IdKondisi).HasColumnName("id_kondisi");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Konstruksi)
                    .HasColumnName("konstruksi")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LuasBangunan).HasColumnName("luas_bangunan");

                entity.Property(e => e.NamaBangunan)
                    .IsRequired()
                    .HasColumnName("nama_bangunan")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NilaiAset)
                    .HasColumnName("nilai_aset")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Status)
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif')")
                    .HasDefaultValueSql("'Aktif'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusBangunan)
                    .HasColumnName("status_bangunan")
                    .HasColumnType("enum('Milik Sendiri','Sewa','Lainnya')")
                    .HasDefaultValueSql("'Milik Sendiri'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_bin");

                entity.Property(e => e.TanggalBangunan)
                    .HasColumnName("tanggal_bangunan")
                    .HasColumnType("date");

                entity.Property(e => e.UkuranL)
                    .HasColumnName("ukuran_l")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("Lebar Bangunan");

                entity.Property(e => e.UkuranP)
                    .HasColumnName("ukuran_p")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("Panjang Bangunan");

                entity.HasOne(d => d.IdKondisiNavigation)
                    .WithMany(p => p.AsetBangunan)
                    .HasForeignKey(d => d.IdKondisi)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_bangunan_kondisi");
            });

            modelBuilder.Entity<AsetHabisPakai>(entity =>
            {
                entity.HasKey(e => e.KodeBarang)
                    .HasName("PRIMARY");

                entity.ToTable("aset_habis_pakai");

                entity.HasIndex(e => e.IdJurusan)
                    .HasName("fk_ahp_jurusan");

                entity.HasIndex(e => e.IdLokasi)
                    .HasName("fk_ahp_lokasi");

                entity.HasIndex(e => e.IdMasterBarang)
                    .HasName("fk_ahp_master");

                entity.HasIndex(e => e.IdPengadaanHp)
                    .HasName("fk_ahp_pengadaanhp");

                entity.HasIndex(e => e.IdRuang)
                    .HasName("fk_ahp_ruang");

                entity.Property(e => e.KodeBarang)
                    .HasColumnName("kode_barang")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdJurusan).HasColumnName("id_jurusan");

                entity.Property(e => e.IdLokasi).HasColumnName("id_lokasi");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.IdPengadaanHp).HasColumnName("id_pengadaan_hp");

                entity.Property(e => e.IdRuang).HasColumnName("id_ruang");

                entity.Property(e => e.IsReturnable)
                    .HasColumnName("is_returnable")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif','Keluar','Tersedia')")
                    .HasDefaultValueSql("'Di Gudang'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Stok).HasColumnName("stok");

                entity.Property(e => e.StokAktual).HasColumnName("stok_aktual");

                entity.Property(e => e.TanggalRegistrasi)
                    .HasColumnName("tanggal_registrasi")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdJurusanNavigation)
                    .WithMany(p => p.AsetHabisPakai)
                    .HasForeignKey(d => d.IdJurusan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_ahp_jurusan");

                entity.HasOne(d => d.IdLokasiNavigation)
                    .WithMany(p => p.AsetHabisPakai)
                    .HasForeignKey(d => d.IdLokasi)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_ahp_lokasi");

                entity.HasOne(d => d.IdMasterBarangNavigation)
                    .WithMany(p => p.AsetHabisPakai)
                    .HasForeignKey(d => d.IdMasterBarang)
                    .HasConstraintName("fk_ahp_master");

                entity.HasOne(d => d.IdPengadaanHpNavigation)
                    .WithMany(p => p.AsetHabisPakai)
                    .HasForeignKey(d => d.IdPengadaanHp)
                    .HasConstraintName("fk_ahp_pengadaanhp");

                entity.HasOne(d => d.IdRuangNavigation)
                    .WithMany(p => p.AsetHabisPakai)
                    .HasForeignKey(d => d.IdRuang)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_ahp_ruang");
            });

            modelBuilder.Entity<AsetTanah>(entity =>
            {
                entity.HasKey(e => e.KodeTanah)
                    .HasName("PRIMARY");

                entity.ToTable("aset_tanah");

                entity.HasIndex(e => e.IdLokasi)
                    .HasName("id_lokasi");

                entity.Property(e => e.KodeTanah).HasColumnName("kode_tanah");

                entity.Property(e => e.IdLokasi).HasColumnName("id_lokasi");

                entity.Property(e => e.LetakTanah)
                    .IsRequired()
                    .HasColumnName("letak_tanah")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LuasTanah).HasColumnName("luas_tanah");

                entity.Property(e => e.NamaPemilik)
                    .IsRequired()
                    .HasColumnName("nama_pemilik")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NilaiAset)
                    .HasColumnName("nilai_aset")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.NomorSertifikat)
                    .HasColumnName("nomor_sertifikat")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Penggunaan)
                    .HasColumnName("penggunaan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif')")
                    .HasDefaultValueSql("'Aktif'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusHak)
                    .HasColumnName("status_hak")
                    .HasColumnType("enum('Hak Milik','Hak Pakai','Hak Guna Bangunan','Sewa','Lainnya')")
                    .HasDefaultValueSql("'Hak Milik'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SumberPerolehan)
                    .HasColumnName("sumber_perolehan")
                    .HasColumnType("enum('Beli','Sumbangan','Hibah','Lainnya')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPerolehan)
                    .HasColumnName("tanggal_perolehan")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdLokasiNavigation)
                    .WithMany(p => p.AsetTanah)
                    .HasForeignKey(d => d.IdLokasi)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("aset_tanah_ibfk_1");
            });

            modelBuilder.Entity<BangunanNonAktif>(entity =>
            {
                entity.HasKey(e => e.IdBangunanNonAktif)
                    .HasName("PRIMARY");

                entity.ToTable("bangunan_non_aktif");

                entity.HasIndex(e => e.IdStatus)
                    .HasName("fk_bgna_status");

                entity.HasIndex(e => e.KodeBangunan)
                    .HasName("fk_bgna_bangunan");

                entity.Property(e => e.IdBangunanNonAktif).HasColumnName("id_bangunan_non_aktif");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBangunan).HasColumnName("kode_bangunan");

                entity.Property(e => e.Tanggal)
                    .HasColumnName("tanggal")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.BangunanNonAktif)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_bgna_status");

                entity.HasOne(d => d.KodeBangunanNavigation)
                    .WithMany(p => p.BangunanNonAktif)
                    .HasForeignKey(d => d.KodeBangunan)
                    .HasConstraintName("fk_bgna_bangunan");
            });

            modelBuilder.Entity<BarangKeluar>(entity =>
            {
                entity.HasKey(e => e.NoTransaksi)
                    .HasName("PRIMARY");

                entity.ToTable("barang_keluar");

                entity.HasIndex(e => e.IdRuang)
                    .HasName("fk_barang_keluar_ruang");

                entity.HasIndex(e => e.KodeBarang)
                    .HasName("fk_bk_inventaris");

                entity.HasIndex(e => e.KodeGudang)
                    .HasName("fk_barang_keluar_gudang");

                entity.HasIndex(e => e.NamaPenerima)
                    .HasName("fk_barang_keluar_pengguna");

                entity.HasIndex(e => e.Petugas)
                    .HasName("fk_barang_keluar_petugas");

                entity.Property(e => e.NoTransaksi).HasColumnName("no_transaksi");

                entity.Property(e => e.IdRuang).HasColumnName("id_ruang");

                entity.Property(e => e.JumlahKeluar).HasColumnName("jumlah_keluar");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBarang)
                    .IsRequired()
                    .HasColumnName("kode_barang")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeGudang)
                    .IsRequired()
                    .HasColumnName("kode_gudang")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPenerima).HasColumnName("nama_penerima");

                entity.Property(e => e.Petugas).HasColumnName("petugas");

                entity.Property(e => e.TanggalKeluar)
                    .HasColumnName("tanggal_keluar")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdRuangNavigation)
                    .WithMany(p => p.BarangKeluar)
                    .HasForeignKey(d => d.IdRuang)
                    .HasConstraintName("fk_barang_keluar_ruang");

                entity.HasOne(d => d.KodeBarangNavigation)
                    .WithMany(p => p.BarangKeluar)
                    .HasForeignKey(d => d.KodeBarang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_bk_kode_barang");

                entity.HasOne(d => d.KodeGudangNavigation)
                    .WithMany(p => p.BarangKeluar)
                    .HasForeignKey(d => d.KodeGudang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_barang_keluar_gudang");

                entity.HasOne(d => d.NamaPenerimaNavigation)
                    .WithMany(p => p.BarangKeluarNamaPenerimaNavigation)
                    .HasForeignKey(d => d.NamaPenerima)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_barang_keluar_pengguna");

                entity.HasOne(d => d.PetugasNavigation)
                    .WithMany(p => p.BarangKeluarPetugasNavigation)
                    .HasForeignKey(d => d.Petugas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_barang_keluar_petugas");
            });

            modelBuilder.Entity<BarangNonAktif>(entity =>
            {
                entity.HasKey(e => e.IdBarangNonAktif)
                    .HasName("PRIMARY");

                entity.ToTable("barang_non_aktif");

                entity.HasIndex(e => e.IdStatus)
                    .HasName("fk_bna_status");

                entity.HasIndex(e => e.KodeInventaris)
                    .HasName("fk_bna_inventaris");

                entity.Property(e => e.IdBarangNonAktif).HasColumnName("id_barang_non_aktif");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.JumlahNonaktif)
                    .HasColumnName("jumlah_nonaktif")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Tanggal)
                    .HasColumnName("tanggal")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.BarangNonAktif)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_bna_status");

                entity.HasOne(d => d.KodeInventarisNavigation)
                    .WithMany(p => p.BarangNonAktif)
                    .HasPrincipalKey(p => p.KodeInventaris)
                    .HasForeignKey(d => d.KodeInventaris)
                    .HasConstraintName("fk_bna_aset");
            });

            modelBuilder.Entity<Cache>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PRIMARY");

                entity.ToTable("cache");

                entity.HasIndex(e => e.Expiration)
                    .HasName("cache_expiration_index");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Expiration).HasColumnName("expiration");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<CacheLocks>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PRIMARY");

                entity.ToTable("cache_locks");

                entity.HasIndex(e => e.Expiration)
                    .HasName("cache_locks_expiration_index");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Expiration).HasColumnName("expiration");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("owner")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<DetailPeminjaman>(entity =>
            {
                entity.HasKey(e => e.IdDetailPinjam)
                    .HasName("PRIMARY");

                entity.ToTable("detail_peminjaman");

                entity.HasIndex(e => e.KodeBarang)
                    .HasName("detail_peminjaman_ibfk_2");

                entity.HasIndex(e => e.NomorPeminjaman)
                    .HasName("nomor_peminjaman");

                entity.Property(e => e.IdDetailPinjam).HasColumnName("id_detail_pinjam");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.NomorPeminjaman)
                    .IsRequired()
                    .HasColumnName("nomor_peminjaman")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.KodeBarangNavigation)
                    .WithMany(p => p.DetailPeminjaman)
                    .HasForeignKey(d => d.KodeBarang)
                    .HasConstraintName("detail_peminjaman_ibfk_2");

                entity.HasOne(d => d.NomorPeminjamanNavigation)
                    .WithMany(p => p.DetailPeminjaman)
                    .HasForeignKey(d => d.NomorPeminjaman)
                    .HasConstraintName("detail_peminjaman_ibfk_1");
            });

            modelBuilder.Entity<DetailPengadaan>(entity =>
            {
                entity.HasKey(e => e.IdDetailPengadaan)
                    .HasName("PRIMARY");

                entity.ToTable("detail_pengadaan");

                entity.HasIndex(e => e.IdMasterBarang)
                    .HasName("idx_master");

                entity.HasIndex(e => e.IdPemasok)
                    .HasName("fk_detail_pengadaan_pemasok");

                entity.HasIndex(e => e.IdPengadaan)
                    .HasName("idx_pengadaan");

                entity.Property(e => e.IdDetailPengadaan).HasColumnName("id_detail_pengadaan");

                entity.Property(e => e.HargaSatuan)
                    .HasColumnName("harga_satuan")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.IdPemasok).HasColumnName("id_pemasok");

                entity.Property(e => e.IdPengadaan).HasColumnName("id_pengadaan");

                entity.Property(e => e.JumlahMasuk).HasColumnName("jumlah_masuk");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdMasterBarangNavigation)
                    .WithMany(p => p.DetailPengadaan)
                    .HasForeignKey(d => d.IdMasterBarang)
                    .HasConstraintName("fk_detail_pengadaan_master");

                entity.HasOne(d => d.IdPemasokNavigation)
                    .WithMany(p => p.DetailPengadaan)
                    .HasForeignKey(d => d.IdPemasok)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_detail_pengadaan_pemasok");

                entity.HasOne(d => d.IdPengadaanNavigation)
                    .WithMany(p => p.DetailPengadaan)
                    .HasForeignKey(d => d.IdPengadaan)
                    .HasConstraintName("fk_detail_pengadaan_pengadaan");
            });

            modelBuilder.Entity<DetailPengadaanHp>(entity =>
            {
                entity.HasKey(e => e.IdDetailPengadaanHp)
                    .HasName("PRIMARY");

                entity.ToTable("detail_pengadaan_hp");

                entity.HasIndex(e => e.IdMasterBarang)
                    .HasName("fk_dp_hp_master");

                entity.HasIndex(e => e.IdPemasok)
                    .HasName("fk_dp_hp_pemasok");

                entity.HasIndex(e => e.IdPengadaanHp)
                    .HasName("fk_dp_hp_pengadaan");

                entity.Property(e => e.IdDetailPengadaanHp).HasColumnName("id_detail_pengadaan_hp");

                entity.Property(e => e.HargaSatuan)
                    .HasColumnName("harga_satuan")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.IdPemasok).HasColumnName("id_pemasok");

                entity.Property(e => e.IdPengadaanHp).HasColumnName("id_pengadaan_hp");

                entity.Property(e => e.JumlahMasuk).HasColumnName("jumlah_masuk");

                entity.HasOne(d => d.IdMasterBarangNavigation)
                    .WithMany(p => p.DetailPengadaanHp)
                    .HasForeignKey(d => d.IdMasterBarang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dp_hp_master");

                entity.HasOne(d => d.IdPemasokNavigation)
                    .WithMany(p => p.DetailPengadaanHp)
                    .HasForeignKey(d => d.IdPemasok)
                    .HasConstraintName("fk_dp_hp_pemasok");

                entity.HasOne(d => d.IdPengadaanHpNavigation)
                    .WithMany(p => p.DetailPengadaanHp)
                    .HasForeignKey(d => d.IdPengadaanHp)
                    .HasConstraintName("fk_dp_hp_pengadaan");
            });

            modelBuilder.Entity<DetailPermintaan>(entity =>
            {
                entity.HasKey(e => e.IdDetailPermintaan)
                    .HasName("PRIMARY");

                entity.ToTable("detail_permintaan");

                entity.HasIndex(e => e.IdMasterBarang)
                    .HasName("id_master_barang");

                entity.HasIndex(e => e.KodePermintaan)
                    .HasName("nomor_permintaan");

                entity.Property(e => e.IdDetailPermintaan).HasColumnName("id_detail_permintaan");

                entity.Property(e => e.AlasanKebutuhan)
                    .HasColumnName("alasan_kebutuhan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.JumlahDiminta).HasColumnName("jumlah_diminta");

                entity.Property(e => e.KodePermintaan)
                    .IsRequired()
                    .HasColumnName("kode_permintaan")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdMasterBarangNavigation)
                    .WithMany(p => p.DetailPermintaan)
                    .HasForeignKey(d => d.IdMasterBarang)
                    .HasConstraintName("detail_permintaan_ibfk_2");

                entity.HasOne(d => d.KodePermintaanNavigation)
                    .WithMany(p => p.DetailPermintaan)
                    .HasForeignKey(d => d.KodePermintaan)
                    .HasConstraintName("detail_permintaan_ibfk_1");
            });

            modelBuilder.Entity<DetailPermintaanHp>(entity =>
            {
                entity.HasKey(e => e.IdDetailPermintaanHp)
                    .HasName("PRIMARY");

                entity.ToTable("detail_permintaan_hp");

                entity.HasIndex(e => e.IdMasterBarang)
                    .HasName("fk_dphp_master");

                entity.HasIndex(e => e.KodePermintaanHp)
                    .HasName("fk_dphp_kodeprm");

                entity.Property(e => e.IdDetailPermintaanHp).HasColumnName("id_detail_permintaan_hp");

                entity.Property(e => e.AlasanKebutuhan)
                    .HasColumnName("alasan_kebutuhan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.JumlahDiminta).HasColumnName("jumlah_diminta");

                entity.Property(e => e.KodePermintaanHp)
                    .IsRequired()
                    .HasColumnName("kode_permintaan_hp")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdMasterBarangNavigation)
                    .WithMany(p => p.DetailPermintaanHp)
                    .HasForeignKey(d => d.IdMasterBarang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dphp_master");

                entity.HasOne(d => d.KodePermintaanHpNavigation)
                    .WithMany(p => p.DetailPermintaanHp)
                    .HasForeignKey(d => d.KodePermintaanHp)
                    .HasConstraintName("fk_dphp_kodeprm");
            });

            modelBuilder.Entity<FailedJobs>(entity =>
            {
                entity.ToTable("failed_jobs");

                entity.HasIndex(e => e.Uuid)
                    .HasName("failed_jobs_uuid_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Connection)
                    .IsRequired()
                    .HasColumnName("connection")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Exception)
                    .IsRequired()
                    .HasColumnName("exception")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FailedAt)
                    .HasColumnName("failed_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnName("payload")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Queue)
                    .IsRequired()
                    .HasColumnName("queue")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Uuid)
                    .IsRequired()
                    .HasColumnName("uuid")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Gudang>(entity =>
            {
                entity.HasKey(e => e.KodeGudang)
                    .HasName("PRIMARY");

                entity.ToTable("gudang");

                entity.Property(e => e.KodeGudang)
                    .HasColumnName("kode_gudang")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaGudang)
                    .IsRequired()
                    .HasColumnName("nama_gudang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<JobBatches>(entity =>
            {
                entity.ToTable("job_batches");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CancelledAt).HasColumnName("cancelled_at");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.FailedJobIds)
                    .IsRequired()
                    .HasColumnName("failed_job_ids")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FailedJobs).HasColumnName("failed_jobs");

                entity.Property(e => e.FinishedAt).HasColumnName("finished_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Options)
                    .HasColumnName("options")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PendingJobs).HasColumnName("pending_jobs");

                entity.Property(e => e.TotalJobs).HasColumnName("total_jobs");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.ToTable("jobs");

                entity.HasIndex(e => e.Queue)
                    .HasName("jobs_queue_index");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Attempts).HasColumnName("attempts");

                entity.Property(e => e.AvailableAt).HasColumnName("available_at");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnName("payload")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Queue)
                    .IsRequired()
                    .HasColumnName("queue")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ReservedAt).HasColumnName("reserved_at");
            });

            modelBuilder.Entity<Jurusan>(entity =>
            {
                entity.HasKey(e => e.IdJurusan)
                    .HasName("PRIMARY");

                entity.ToTable("jurusan");

                entity.Property(e => e.IdJurusan).HasColumnName("id_jurusan");

                entity.Property(e => e.NamaJurusan)
                    .IsRequired()
                    .HasColumnName("nama_jurusan")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.HasKey(e => e.IdKategori)
                    .HasName("PRIMARY");

                entity.ToTable("kategori");

                entity.Property(e => e.IdKategori).HasColumnName("id_kategori");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKategori)
                    .IsRequired()
                    .HasColumnName("nama_kategori")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Kelas>(entity =>
            {
                entity.HasKey(e => e.IdKelas)
                    .HasName("PRIMARY");

                entity.ToTable("kelas");

                entity.HasIndex(e => e.IdRombel)
                    .HasName("id_rombel");

                entity.Property(e => e.IdKelas).HasColumnName("id_kelas");

                entity.Property(e => e.IdRombel).HasColumnName("id_rombel");

                entity.Property(e => e.NamaKelas)
                    .IsRequired()
                    .HasColumnName("nama_kelas")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TahunAjaran)
                    .IsRequired()
                    .HasColumnName("tahun_ajaran")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdRombelNavigation)
                    .WithMany(p => p.Kelas)
                    .HasForeignKey(d => d.IdRombel)
                    .HasConstraintName("kelas_ibfk_1");
            });

            modelBuilder.Entity<Kerusakan>(entity =>
            {
                entity.HasKey(e => e.IdKerusakan)
                    .HasName("PRIMARY");

                entity.ToTable("kerusakan");

                entity.HasIndex(e => e.IdPelapor)
                    .HasName("id_pelapor");

                entity.HasIndex(e => e.KodeBarang)
                    .HasName("kerusakan_ibfk_1");

                entity.Property(e => e.IdKerusakan).HasColumnName("id_kerusakan");

                entity.Property(e => e.DeskripsiKerusakan)
                    .IsRequired()
                    .HasColumnName("deskripsi_kerusakan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdPelapor).HasColumnName("id_pelapor");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.StatusKerusakan)
                    .HasColumnName("status_kerusakan")
                    .HasColumnType("enum('Menunggu Pemeriksaan','Sedang Diperbaiki','Selesai','Tidak Bisa Diperbaiki')")
                    .HasDefaultValueSql("'Menunggu Pemeriksaan'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalLapor)
                    .HasColumnName("tanggal_lapor")
                    .HasColumnType("date");

                entity.Property(e => e.TingkatKerusakan)
                    .IsRequired()
                    .HasColumnName("tingkat_kerusakan")
                    .HasColumnType("enum('Ringan','Sedang','Berat')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdPelaporNavigation)
                    .WithMany(p => p.Kerusakan)
                    .HasForeignKey(d => d.IdPelapor)
                    .HasConstraintName("kerusakan_ibfk_2");

                entity.HasOne(d => d.KodeBarangNavigation)
                    .WithMany(p => p.Kerusakan)
                    .HasForeignKey(d => d.KodeBarang)
                    .HasConstraintName("kerusakan_ibfk_1");
            });

            modelBuilder.Entity<Kondisi>(entity =>
            {
                entity.HasKey(e => e.IdKondisi)
                    .HasName("PRIMARY");

                entity.ToTable("kondisi");

                entity.Property(e => e.IdKondisi).HasColumnName("id_kondisi");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKondisi)
                    .IsRequired()
                    .HasColumnName("nama_kondisi")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Lemari>(entity =>
            {
                entity.HasKey(e => e.IdLemari)
                    .HasName("PRIMARY");

                entity.ToTable("lemari");

                entity.HasIndex(e => e.IdRuang)
                    .HasName("fk_lemari_ruang");

                entity.Property(e => e.IdLemari).HasColumnName("id_lemari");

                entity.Property(e => e.IdRuang).HasColumnName("id_ruang");

                entity.Property(e => e.KodeLemari)
                    .IsRequired()
                    .HasColumnName("kode_lemari")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasColumnName("nama")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdRuangNavigation)
                    .WithMany(p => p.Lemari)
                    .HasForeignKey(d => d.IdRuang)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_lemari_ruang");
            });

            modelBuilder.Entity<Lokasi>(entity =>
            {
                entity.HasKey(e => e.IdLokasi)
                    .HasName("PRIMARY");

                entity.ToTable("lokasi");

                entity.Property(e => e.IdLokasi).HasColumnName("id_lokasi");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeLokasi)
                    .HasColumnName("kode_lokasi")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaLokasi)
                    .IsRequired()
                    .HasColumnName("nama_lokasi")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Mapel>(entity =>
            {
                entity.HasKey(e => e.IdMapel)
                    .HasName("PRIMARY");

                entity.ToTable("mapel");

                entity.Property(e => e.IdMapel).HasColumnName("id_mapel");

                entity.Property(e => e.NamaMapel)
                    .IsRequired()
                    .HasColumnName("nama_mapel")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<MasterBarang>(entity =>
            {
                entity.HasKey(e => e.IdMasterBarang)
                    .HasName("PRIMARY");

                entity.ToTable("master_barang");

                entity.HasIndex(e => e.IdKategori)
                    .HasName("id_kategori");

                entity.HasIndex(e => e.IdMerek)
                    .HasName("id_merek");

                entity.HasIndex(e => e.IdSatuan)
                    .HasName("id_satuan");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.IdKategori).HasColumnName("id_kategori");

                entity.Property(e => e.IdMerek).HasColumnName("id_merek");

                entity.Property(e => e.IdSatuan).HasColumnName("id_satuan");

                entity.Property(e => e.JenisBarang)
                    .HasColumnName("jenis_barang")
                    .HasColumnType("enum('Aset','Habis Pakai')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .IsRequired()
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdKategoriNavigation)
                    .WithMany(p => p.MasterBarang)
                    .HasForeignKey(d => d.IdKategori)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("master_barang_ibfk_1");

                entity.HasOne(d => d.IdMerekNavigation)
                    .WithMany(p => p.MasterBarang)
                    .HasForeignKey(d => d.IdMerek)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("master_barang_ibfk_2");

                entity.HasOne(d => d.IdSatuanNavigation)
                    .WithMany(p => p.MasterBarang)
                    .HasForeignKey(d => d.IdSatuan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("master_barang_ibfk_3");
            });

            modelBuilder.Entity<Merek>(entity =>
            {
                entity.HasKey(e => e.IdMerek)
                    .HasName("PRIMARY");

                entity.ToTable("merek");

                entity.Property(e => e.IdMerek).HasColumnName("id_merek");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaMerek)
                    .IsRequired()
                    .HasColumnName("nama_merek")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Migrations>(entity =>
            {
                entity.ToTable("migrations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Batch).HasColumnName("batch");

                entity.Property(e => e.Migration)
                    .IsRequired()
                    .HasColumnName("migration")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Mutasi>(entity =>
            {
                entity.HasKey(e => e.IdMutasi)
                    .HasName("PRIMARY");

                entity.ToTable("mutasi");

                entity.HasIndex(e => e.IdJurusanAsal)
                    .HasName("fk_mutasi_jurusan_asal");

                entity.HasIndex(e => e.IdJurusanTujuan)
                    .HasName("fk_mutasi_jurusan_tujuan");

                entity.HasIndex(e => e.KodeInventaris)
                    .HasName("kode_barang");

                entity.Property(e => e.IdMutasi).HasColumnName("id_mutasi");

                entity.Property(e => e.AlasanMutasi)
                    .HasColumnName("alasan_mutasi")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdJurusanAsal).HasColumnName("id_jurusan_asal");

                entity.Property(e => e.IdJurusanTujuan).HasColumnName("id_jurusan_tujuan");

                entity.Property(e => e.IsApproved)
                    .HasColumnName("is_approved")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalMutasi)
                    .HasColumnName("tanggal_mutasi")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdJurusanAsalNavigation)
                    .WithMany(p => p.MutasiIdJurusanAsalNavigation)
                    .HasForeignKey(d => d.IdJurusanAsal)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_mutasi_jurusan_asal");

                entity.HasOne(d => d.IdJurusanTujuanNavigation)
                    .WithMany(p => p.MutasiIdJurusanTujuanNavigation)
                    .HasForeignKey(d => d.IdJurusanTujuan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mutasi_jurusan_tujuan");

                entity.HasOne(d => d.KodeInventarisNavigation)
                    .WithMany(p => p.Mutasi)
                    .HasPrincipalKey(p => p.KodeInventaris)
                    .HasForeignKey(d => d.KodeInventaris)
                    .HasConstraintName("fk_mutasi_aset");
            });

            modelBuilder.Entity<OpnameAset>(entity =>
            {
                entity.HasKey(e => e.IdOpnameAset)
                    .HasName("PRIMARY");

                entity.ToTable("opname_aset");

                entity.HasIndex(e => e.IdKondisi)
                    .HasName("fk_opname_kondisi");

                entity.HasIndex(e => e.KodeInventaris)
                    .HasName("fk_opname_inventaris");

                entity.Property(e => e.IdOpnameAset).HasColumnName("id_opname_aset");

                entity.Property(e => e.IdKondisi).HasColumnName("id_kondisi");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalOpname)
                    .HasColumnName("tanggal_opname")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdKondisiNavigation)
                    .WithMany(p => p.OpnameAset)
                    .HasForeignKey(d => d.IdKondisi)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_opname_kondisi");

                entity.HasOne(d => d.KodeInventarisNavigation)
                    .WithMany(p => p.OpnameAset)
                    .HasPrincipalKey(p => p.KodeInventaris)
                    .HasForeignKey(d => d.KodeInventaris)
                    .HasConstraintName("fk_opname_aset");
            });

            modelBuilder.Entity<OpnameStok>(entity =>
            {
                entity.HasKey(e => e.IdOpnameStok)
                    .HasName("PRIMARY");

                entity.ToTable("opname_stok");

                entity.HasIndex(e => e.IdMasterBarang)
                    .HasName("id_master_barang");

                entity.HasIndex(e => e.IdPemeriksa)
                    .HasName("id_pemeriksa");

                entity.Property(e => e.IdOpnameStok).HasColumnName("id_opname_stok");

                entity.Property(e => e.IdMasterBarang).HasColumnName("id_master_barang");

                entity.Property(e => e.IdPemeriksa).HasColumnName("id_pemeriksa");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Selisih).HasColumnName("selisih");

                entity.Property(e => e.StokFisik).HasColumnName("stok_fisik");

                entity.Property(e => e.StokSistem).HasColumnName("stok_sistem");

                entity.Property(e => e.TanggalOpname)
                    .HasColumnName("tanggal_opname")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdMasterBarangNavigation)
                    .WithMany(p => p.OpnameStok)
                    .HasForeignKey(d => d.IdMasterBarang)
                    .HasConstraintName("opname_stok_ibfk_1");

                entity.HasOne(d => d.IdPemeriksaNavigation)
                    .WithMany(p => p.OpnameStok)
                    .HasForeignKey(d => d.IdPemeriksa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("opname_stok_ibfk_2");
            });

            modelBuilder.Entity<Pemasok>(entity =>
            {
                entity.HasKey(e => e.IdPemasok)
                    .HasName("PRIMARY");

                entity.ToTable("pemasok");

                entity.Property(e => e.IdPemasok).HasColumnName("id_pemasok");

                entity.Property(e => e.Alamat)
                    .HasColumnName("alamat")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPemasok)
                    .IsRequired()
                    .HasColumnName("nama_pemasok")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NomorTelepon)
                    .HasColumnName("nomor_telepon")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Peminjaman>(entity =>
            {
                entity.HasKey(e => e.NomorPeminjaman)
                    .HasName("PRIMARY");

                entity.ToTable("peminjaman");

                entity.HasIndex(e => e.NamaPeminjam)
                    .HasName("id_peminjam");

                entity.Property(e => e.NomorPeminjaman)
                    .HasColumnName("nomor_peminjaman")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LamaPinjamHari).HasColumnName("lama_pinjam_hari");

                entity.Property(e => e.NamaPeminjam)
                    .IsRequired()
                    .HasColumnName("nama_peminjam")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NomorTelepon)
                    .HasColumnName("nomor_telepon")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusPeminjaman)
                    .HasColumnName("status_peminjaman")
                    .HasColumnType("enum('Sedang Dipinjam','Dikembalikan')")
                    .HasDefaultValueSql("'Sedang Dipinjam'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPinjam)
                    .HasColumnName("tanggal_pinjam")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Pengadaan>(entity =>
            {
                entity.HasKey(e => e.IdPengadaan)
                    .HasName("PRIMARY");

                entity.ToTable("pengadaan");

                entity.HasIndex(e => e.IdSumberPerolehan)
                    .HasName("fk_pengadaan_sumber");

                entity.HasIndex(e => e.IdTahunAjaran)
                    .HasName("fk_pengadaan_ta");

                entity.HasIndex(e => e.KodeGudang)
                    .HasName("idx_gudang");

                entity.Property(e => e.IdPengadaan).HasColumnName("id_pengadaan");

                entity.Property(e => e.IdSumberPerolehan).HasColumnName("id_sumber_perolehan");

                entity.Property(e => e.IdTahunAjaran).HasColumnName("id_tahun_ajaran");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeGudang)
                    .HasColumnName("kode_gudang")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Menunggu Proses','Sedang Dibelanjakan','Selesai Dibelanjakan')")
                    .HasDefaultValueSql("'Menunggu Proses'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPengadaan)
                    .HasColumnName("tanggal_pengadaan")
                    .HasColumnType("date");

                entity.Property(e => e.TotalHarga)
                    .HasColumnName("total_harga")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.HasOne(d => d.IdSumberPerolehanNavigation)
                    .WithMany(p => p.Pengadaan)
                    .HasForeignKey(d => d.IdSumberPerolehan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_pengadaan_sumber");

                entity.HasOne(d => d.IdTahunAjaranNavigation)
                    .WithMany(p => p.Pengadaan)
                    .HasForeignKey(d => d.IdTahunAjaran)
                    .HasConstraintName("fk_pengadaan_ta");

                entity.HasOne(d => d.KodeGudangNavigation)
                    .WithMany(p => p.Pengadaan)
                    .HasForeignKey(d => d.KodeGudang)
                    .HasConstraintName("fk_pengadaan_gudang");
            });

            modelBuilder.Entity<PengadaanHabisPakai>(entity =>
            {
                entity.HasKey(e => e.IdPengadaan)
                    .HasName("PRIMARY");

                entity.ToTable("pengadaan_habis_pakai");

                entity.HasIndex(e => e.IdTahunAjaran)
                    .HasName("fk_php_ta");

                entity.HasIndex(e => e.KodeGudang)
                    .HasName("fk_php_gudang");

                entity.Property(e => e.IdPengadaan).HasColumnName("id_pengadaan");

                entity.Property(e => e.IdSumberPerolehan).HasColumnName("id_sumber_perolehan");

                entity.Property(e => e.IdTahunAjaran).HasColumnName("id_tahun_ajaran");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeGudang)
                    .HasColumnName("kode_gudang")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Menunggu Proses','Dibelanjakan','Selesai')")
                    .HasDefaultValueSql("'Menunggu Proses'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPengadaan)
                    .HasColumnName("tanggal_pengadaan")
                    .HasColumnType("date");

                entity.Property(e => e.TotalHarga)
                    .HasColumnName("total_harga")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.HasOne(d => d.IdTahunAjaranNavigation)
                    .WithMany(p => p.PengadaanHabisPakai)
                    .HasForeignKey(d => d.IdTahunAjaran)
                    .HasConstraintName("fk_php_ta");

                entity.HasOne(d => d.KodeGudangNavigation)
                    .WithMany(p => p.PengadaanHabisPakai)
                    .HasForeignKey(d => d.KodeGudang)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_php_gudang");
            });

            modelBuilder.Entity<PengadaanPermintaan>(entity =>
            {
                entity.HasKey(e => new { e.IdPengadaan, e.KodePermintaan })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("pengadaan_permintaan");

                entity.HasIndex(e => e.KodePermintaan)
                    .HasName("fk_pp_permintaan");

                entity.Property(e => e.IdPengadaan).HasColumnName("id_pengadaan");

                entity.Property(e => e.KodePermintaan)
                    .HasColumnName("kode_permintaan")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdPengadaanNavigation)
                    .WithMany(p => p.PengadaanPermintaan)
                    .HasForeignKey(d => d.IdPengadaan)
                    .HasConstraintName("fk_pp_pengadaan");

                entity.HasOne(d => d.KodePermintaanNavigation)
                    .WithMany(p => p.PengadaanPermintaan)
                    .HasForeignKey(d => d.KodePermintaan)
                    .HasConstraintName("fk_pp_permintaan");
            });

            modelBuilder.Entity<PengadaanPermintaanHp>(entity =>
            {
                entity.HasKey(e => new { e.IdPengadaanHp, e.KodePermintaanHp })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("pengadaan_permintaan_hp");

                entity.HasIndex(e => e.KodePermintaanHp)
                    .HasName("fk_jembatan_hp_permintaan");

                entity.Property(e => e.IdPengadaanHp).HasColumnName("id_pengadaan_hp");

                entity.Property(e => e.KodePermintaanHp)
                    .HasColumnName("kode_permintaan_hp")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdPengadaanHpNavigation)
                    .WithMany(p => p.PengadaanPermintaanHp)
                    .HasForeignKey(d => d.IdPengadaanHp)
                    .HasConstraintName("fk_jembatan_hp_pengadaan");

                entity.HasOne(d => d.KodePermintaanHpNavigation)
                    .WithMany(p => p.PengadaanPermintaanHp)
                    .HasForeignKey(d => d.KodePermintaanHp)
                    .HasConstraintName("fk_jembatan_hp_permintaan");
            });

            modelBuilder.Entity<Pengaturan>(entity =>
            {
                entity.HasKey(e => e.IdPengaturan)
                    .HasName("PRIMARY");

                entity.ToTable("pengaturan");

                entity.Property(e => e.IdPengaturan).HasColumnName("id_pengaturan");

                entity.Property(e => e.AlamatInstansi)
                    .HasColumnName("alamat_instansi")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BagianInventaris)
                    .HasColumnName("bagian_inventaris")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KepalaSekolah)
                    .HasColumnName("kepala_sekolah")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Kota)
                    .HasColumnName("kota")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KustomPrefix)
                    .HasColumnName("kustom_prefix")
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("'INV'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LogoInstansi)
                    .HasColumnName("logo_instansi")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaInstansi)
                    .IsRequired()
                    .HasColumnName("nama_instansi")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Nip)
                    .HasColumnName("NIP")
                    .HasColumnType("varchar(25)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PaketGolongan)
                    .HasColumnName("paket_golongan")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Telpon).HasColumnName("telpon");

                entity.Property(e => e.WallpaperAplikasi)
                    .HasColumnName("wallpaper_aplikasi")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Pengembalian>(entity =>
            {
                entity.HasKey(e => e.IdPengembalian)
                    .HasName("PRIMARY");

                entity.ToTable("pengembalian");

                entity.HasIndex(e => e.NomorPeminjaman)
                    .HasName("nomor_peminjaman");

                entity.Property(e => e.IdPengembalian).HasColumnName("id_pengembalian");

                entity.Property(e => e.NomorPeminjaman)
                    .IsRequired()
                    .HasColumnName("nomor_peminjaman")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalKembali)
                    .HasColumnName("tanggal_kembali")
                    .HasColumnType("date");

                entity.HasOne(d => d.NomorPeminjamanNavigation)
                    .WithMany(p => p.Pengembalian)
                    .HasForeignKey(d => d.NomorPeminjaman)
                    .HasConstraintName("pengembalian_ibfk_1");
            });

            modelBuilder.Entity<Pengguna>(entity =>
            {
                entity.HasKey(e => e.IdPengguna)
                    .HasName("PRIMARY");

                entity.ToTable("pengguna");

                entity.HasIndex(e => e.IdJurusan)
                    .HasName("fk_pengguna_jurusan");

                entity.HasIndex(e => e.IdKelas)
                    .HasName("id_kelas");

                entity.HasIndex(e => e.IdMapel)
                    .HasName("id_mapel");

                entity.HasIndex(e => e.IdPeran)
                    .HasName("id_peran");

                entity.Property(e => e.IdPengguna).HasColumnName("id_pengguna");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdJurusan).HasColumnName("id_jurusan");

                entity.Property(e => e.IdKelas).HasColumnName("id_kelas");

                entity.Property(e => e.IdMapel).HasColumnName("id_mapel");

                entity.Property(e => e.IdPeran).HasColumnName("id_peran");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdJurusanNavigation)
                    .WithMany(p => p.Pengguna)
                    .HasForeignKey(d => d.IdJurusan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_pengguna_jurusan");

                entity.HasOne(d => d.IdKelasNavigation)
                    .WithMany(p => p.Pengguna)
                    .HasForeignKey(d => d.IdKelas)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("pengguna_ibfk_2");

                entity.HasOne(d => d.IdMapelNavigation)
                    .WithMany(p => p.Pengguna)
                    .HasForeignKey(d => d.IdMapel)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("pengguna_ibfk_3");

                entity.HasOne(d => d.IdPeranNavigation)
                    .WithMany(p => p.Pengguna)
                    .HasForeignKey(d => d.IdPeran)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pengguna_ibfk_1");
            });

            modelBuilder.Entity<PenghapusanAset>(entity =>
            {
                entity.HasKey(e => e.IdPenghapusan)
                    .HasName("PRIMARY");

                entity.ToTable("penghapusan_aset");

                entity.HasIndex(e => e.IdPenyetuju)
                    .HasName("id_penyetuju");

                entity.HasIndex(e => e.KodeBarang)
                    .HasName("penghapusan_aset_ibfk_1");

                entity.Property(e => e.IdPenghapusan).HasColumnName("id_penghapusan");

                entity.Property(e => e.AlasanHapus)
                    .IsRequired()
                    .HasColumnName("alasan_hapus")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdPenyetuju).HasColumnName("id_penyetuju");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.TanggalHapus)
                    .HasColumnName("tanggal_hapus")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdPenyetujuNavigation)
                    .WithMany(p => p.PenghapusanAset)
                    .HasForeignKey(d => d.IdPenyetuju)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("penghapusan_aset_ibfk_2");

                entity.HasOne(d => d.KodeBarangNavigation)
                    .WithMany(p => p.PenghapusanAset)
                    .HasForeignKey(d => d.KodeBarang)
                    .HasConstraintName("penghapusan_aset_ibfk_1");
            });

            modelBuilder.Entity<Peran>(entity =>
            {
                entity.HasKey(e => e.IdPeran)
                    .HasName("PRIMARY");

                entity.ToTable("peran");

                entity.Property(e => e.IdPeran).HasColumnName("id_peran");

                entity.Property(e => e.NamaPeran)
                    .IsRequired()
                    .HasColumnName("nama_peran")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<PeranAkses>(entity =>
            {
                entity.HasKey(e => e.IdPeranAkses)
                    .HasName("PRIMARY");

                entity.ToTable("peran_akses");

                entity.HasIndex(e => e.IdAkses)
                    .HasName("id_akses");

                entity.HasIndex(e => e.IdPeran)
                    .HasName("id_peran");

                entity.Property(e => e.IdPeranAkses).HasColumnName("id_peran_akses");

                entity.Property(e => e.HakBaca)
                    .HasColumnName("hak_baca")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HakBuat)
                    .HasColumnName("hak_buat")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HakHapus)
                    .HasColumnName("hak_hapus")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HakUbah)
                    .HasColumnName("hak_ubah")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdAkses).HasColumnName("id_akses");

                entity.Property(e => e.IdPeran).HasColumnName("id_peran");
                entity.Property(e => e.HakApprove).HasColumnName("hak_approve");
                entity.Property(e => e.HakExport).HasColumnName("hak_export");


                entity.HasOne(d => d.IdAksesNavigation)
                    .WithMany(p => p.PeranAkses)
                    .HasForeignKey(d => d.IdAkses)
                    .HasConstraintName("peran_akses_ibfk_2");

                entity.HasOne(d => d.IdPeranNavigation)
                    .WithMany(p => p.PeranAkses)
                    .HasForeignKey(d => d.IdPeran)
                    .HasConstraintName("peran_akses_ibfk_1");
            });

            modelBuilder.Entity<Perbaikan>(entity =>
            {
                entity.HasKey(e => e.IdPerbaikan)
                    .HasName("PRIMARY");

                entity.ToTable("perbaikan");

                entity.HasIndex(e => e.IdKerusakan)
                    .HasName("id_kerusakan");

                entity.Property(e => e.IdPerbaikan).HasColumnName("id_perbaikan");

                entity.Property(e => e.BiayaPerbaikan)
                    .HasColumnName("biaya_perbaikan")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.IdKerusakan).HasColumnName("id_kerusakan");

                entity.Property(e => e.TanggalPerbaikan)
                    .HasColumnName("tanggal_perbaikan")
                    .HasColumnType("date");

                entity.Property(e => e.Teknisi)
                    .HasColumnName("teknisi")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TindakanPerbaikan)
                    .IsRequired()
                    .HasColumnName("tindakan_perbaikan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdKerusakanNavigation)
                    .WithMany(p => p.Perbaikan)
                    .HasForeignKey(d => d.IdKerusakan)
                    .HasConstraintName("perbaikan_ibfk_1");
            });

            modelBuilder.Entity<Permintaan>(entity =>
            {
                entity.HasKey(e => e.KodePermintaan)
                    .HasName("PRIMARY");

                entity.ToTable("permintaan");

                entity.HasIndex(e => e.IdJurusan)
                    .HasName("fk_permintaan_jurusan");

                entity.HasIndex(e => e.IdPengguna)
                    .HasName("fk_permintaan_pengguna");

                entity.HasIndex(e => e.IdPenyetuju)
                    .HasName("id_penyetuju");

                entity.HasIndex(e => e.IdTahunAjaran)
                    .HasName("fk_permintaan_ta");

                entity.Property(e => e.KodePermintaan)
                    .HasColumnName("kode_permintaan")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AlasanDisetujui)
                    .HasColumnName("alasan_disetujui")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdJurusan).HasColumnName("id_jurusan");

                entity.Property(e => e.IdPengguna).HasColumnName("id_pengguna");

                entity.Property(e => e.IdPenyetuju).HasColumnName("id_penyetuju");

                entity.Property(e => e.IdTahunAjaran).HasColumnName("id_tahun_ajaran");

                entity.Property(e => e.KeteranganKeperluan)
                    .IsRequired()
                    .HasColumnName("keterangan_keperluan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusPersetujuan)
                    .HasColumnName("status_persetujuan")
                    .HasColumnType("enum('Menunggu','Disetujui','Ditolak')")
                    .HasDefaultValueSql("'Menunggu'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPermintaan)
                    .HasColumnName("tanggal_permintaan")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalPersetujuan)
                    .HasColumnName("tanggal_persetujuan")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdJurusanNavigation)
                    .WithMany(p => p.Permintaan)
                    .HasForeignKey(d => d.IdJurusan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_permintaan_jurusan");

                entity.HasOne(d => d.IdPenggunaNavigation)
                    .WithMany(p => p.PermintaanIdPenggunaNavigation)
                    .HasForeignKey(d => d.IdPengguna)
                    .HasConstraintName("fk_permintaan_pengguna");

                entity.HasOne(d => d.IdPenyetujuNavigation)
                    .WithMany(p => p.PermintaanIdPenyetujuNavigation)
                    .HasForeignKey(d => d.IdPenyetuju)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("permintaan_ibfk_2");

                entity.HasOne(d => d.IdTahunAjaranNavigation)
                    .WithMany(p => p.Permintaan)
                    .HasForeignKey(d => d.IdTahunAjaran)
                    .HasConstraintName("fk_permintaan_ta");
            });

            modelBuilder.Entity<PermintaanHp>(entity =>
            {
                entity.HasKey(e => e.KodePermintaanHp)
                    .HasName("PRIMARY");

                entity.ToTable("permintaan_hp");

                entity.HasIndex(e => e.IdJurusan)
                    .HasName("fk_permintaanhp_jurusan");

                entity.HasIndex(e => e.IdPengguna)
                    .HasName("fk_permintaannhp_pengguna");

                entity.HasIndex(e => e.IdPenyetuju)
                    .HasName("fk_permintaanhp_penyetuju");

                entity.HasIndex(e => e.IdTahunAjaran)
                    .HasName("fk_permintaanhp_ta");

                entity.Property(e => e.KodePermintaanHp)
                    .HasColumnName("kode_permintaan_hp")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AlasanDisetujui)
                    .HasColumnName("alasan_disetujui")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdJurusan).HasColumnName("id_jurusan");

                entity.Property(e => e.IdPengguna).HasColumnName("id_pengguna");

                entity.Property(e => e.IdPenyetuju).HasColumnName("id_penyetuju");

                entity.Property(e => e.IdTahunAjaran).HasColumnName("id_tahun_ajaran");

                entity.Property(e => e.KeteranganKeperluan)
                    .IsRequired()
                    .HasColumnName("keterangan_keperluan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusPersetujuan)
                    .HasColumnName("status_persetujuan")
                    .HasColumnType("enum('Menunggu','Disetujui','Ditolak')")
                    .HasDefaultValueSql("'Menunggu'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPermintaan)
                    .HasColumnName("tanggal_permintaan")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalPersetujuan)
                    .HasColumnName("tanggal_persetujuan")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdJurusanNavigation)
                    .WithMany(p => p.PermintaanHp)
                    .HasForeignKey(d => d.IdJurusan)
                    .HasConstraintName("fk_permintaanhp_jurusan");

                entity.HasOne(d => d.IdPenggunaNavigation)
                    .WithMany(p => p.PermintaanHpIdPenggunaNavigation)
                    .HasForeignKey(d => d.IdPengguna)
                    .HasConstraintName("fk_permintaannhp_pengguna");

                entity.HasOne(d => d.IdPenyetujuNavigation)
                    .WithMany(p => p.PermintaanHpIdPenyetujuNavigation)
                    .HasForeignKey(d => d.IdPenyetuju)
                    .HasConstraintName("fk_permintaanhp_penyetuju");

                entity.HasOne(d => d.IdTahunAjaranNavigation)
                    .WithMany(p => p.PermintaanHp)
                    .HasForeignKey(d => d.IdTahunAjaran)
                    .HasConstraintName("fk_permintaanhp_ta");
            });

            modelBuilder.Entity<PersonalAccessTokens>(entity =>
            {
                entity.ToTable("personal_access_tokens");

                entity.HasIndex(e => e.ExpiresAt)
                    .HasName("personal_access_tokens_expires_at_index");

                entity.HasIndex(e => e.Token)
                    .HasName("personal_access_tokens_token_unique")
                    .IsUnique();

                entity.HasIndex(e => new { e.TokenableType, e.TokenableId })
                    .HasName("personal_access_tokens_tokenable_type_tokenable_id_index");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abilities)
                    .HasColumnName("abilities")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.ExpiresAt)
                    .HasColumnName("expires_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.LastUsedAt)
                    .HasColumnName("last_used_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TokenableId).HasColumnName("tokenable_id");

                entity.Property(e => e.TokenableType)
                    .IsRequired()
                    .HasColumnName("tokenable_type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Rombel>(entity =>
            {
                entity.HasKey(e => e.IdRombel)
                    .HasName("PRIMARY");

                entity.ToTable("rombel");

                entity.HasIndex(e => e.IdJurusan)
                    .HasName("id_jurusan");

                entity.Property(e => e.IdRombel).HasColumnName("id_rombel");

                entity.Property(e => e.IdJurusan).HasColumnName("id_jurusan");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.KodeRombel)
                    .HasColumnName("kode_rombel")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaRombel)
                    .IsRequired()
                    .HasColumnName("nama_rombel")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Tingkat)
                    .HasColumnName("tingkat")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdJurusanNavigation)
                    .WithMany(p => p.Rombel)
                    .HasForeignKey(d => d.IdJurusan)
                    .HasConstraintName("rombel_ibfk_1");
            });

            modelBuilder.Entity<Ruang>(entity =>
            {
                entity.HasKey(e => e.IdRuang)
                    .HasName("PRIMARY");

                entity.ToTable("ruang");

                entity.HasIndex(e => e.IdLokasi)
                    .HasName("id_lokasi");

                entity.Property(e => e.IdRuang).HasColumnName("id_ruang");

                entity.Property(e => e.IdLokasi).HasColumnName("id_lokasi");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeRuang)
                    .HasColumnName("kode_ruang")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Lantai)
                    .HasColumnName("lantai")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaRuang)
                    .IsRequired()
                    .HasColumnName("nama_ruang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdLokasiNavigation)
                    .WithMany(p => p.Ruang)
                    .HasForeignKey(d => d.IdLokasi)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ruang_ibfk_1");
            });

            modelBuilder.Entity<Satuan>(entity =>
            {
                entity.HasKey(e => e.IdSatuan)
                    .HasName("PRIMARY");

                entity.ToTable("satuan");

                entity.Property(e => e.IdSatuan).HasColumnName("id_satuan");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeSatuan)
                    .HasColumnName("kode_satuan")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaSatuan)
                    .IsRequired()
                    .HasColumnName("nama_satuan")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.ToTable("sessions");

                entity.HasIndex(e => e.LastActivity)
                    .HasName("sessions_last_activity_index");

                entity.HasIndex(e => e.UserId)
                    .HasName("sessions_user_id_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ip_address")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LastActivity).HasColumnName("last_activity");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnName("payload")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserAgent)
                    .HasColumnName("user_agent")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<StatusBarang>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PRIMARY");

                entity.ToTable("status_barang");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaStatus)
                    .IsRequired()
                    .HasColumnName("nama_status")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<SumberPerolehan>(entity =>
            {
                entity.HasKey(e => e.IdSumberPerolehan)
                    .HasName("PRIMARY");

                entity.ToTable("sumber_perolehan");

                entity.Property(e => e.IdSumberPerolehan).HasColumnName("id_sumber_perolehan");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeSumber)
                    .HasColumnName("kode_sumber")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaSumber)
                    .IsRequired()
                    .HasColumnName("nama_sumber")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<TahunAjaran>(entity =>
            {
                entity.HasKey(e => e.IdTahunAjaran)
                    .HasName("PRIMARY");

                entity.ToTable("tahun_ajaran");

                entity.Property(e => e.IdTahunAjaran).HasColumnName("id_tahun_ajaran");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasColumnName("semester")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TahunAjaran1)
                    .IsRequired()
                    .HasColumnName("tahun_ajaran")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalMulai)
                    .HasColumnName("tanggal_mulai")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalSelesai)
                    .HasColumnName("tanggal_selesai")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<TanahNonAktif>(entity =>
            {
                entity.HasKey(e => e.IdTanahNonAktif)
                    .HasName("PRIMARY");

                entity.ToTable("tanah_non_aktif");

                entity.HasIndex(e => e.IdStatus)
                    .HasName("fk_tna_status");

                entity.HasIndex(e => e.KodeTanah)
                    .HasName("fk_tna_tanah");

                entity.Property(e => e.IdTanahNonAktif).HasColumnName("id_tanah_non_aktif");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeTanah).HasColumnName("kode_tanah");

                entity.Property(e => e.Tanggal)
                    .HasColumnName("tanggal")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.TanahNonAktif)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_tna_status");

                entity.HasOne(d => d.KodeTanahNavigation)
                    .WithMany(p => p.TanahNonAktif)
                    .HasForeignKey(d => d.KodeTanah)
                    .HasConstraintName("fk_tna_tanah");
            });

            modelBuilder.Entity<VAset>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_aset");

                entity.Property(e => e.HargaSatuan)
                    .HasColumnName("harga_satuan")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaJurusan)
                    .HasColumnName("nama_jurusan")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKategori)
                    .HasColumnName("nama_kategori")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKondisi)
                    .HasColumnName("nama_kondisi")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaLokasi)
                    .HasColumnName("nama_lokasi")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaMerek)
                    .HasColumnName("nama_merek")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaRuang)
                    .HasColumnName("nama_ruang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaSatuan)
                    .HasColumnName("nama_satuan")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NilaiResidu)
                    .HasColumnName("nilai_residu")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.NoSeri)
                    .HasColumnName("no_seri")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif')")
                    .HasDefaultValueSql("'Di Gudang'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalRegistrasi)
                    .HasColumnName("tanggal_registrasi")
                    .HasColumnType("date");

                entity.Property(e => e.UmurEkonomi).HasColumnName("umur_ekonomi");
            });

            modelBuilder.Entity<VAsetBangunan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_aset_bangunan");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBangunan).HasColumnName("kode_bangunan");

                entity.Property(e => e.Konstruksi)
                    .HasColumnName("konstruksi")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LuasBangunan).HasColumnName("luas_bangunan");

                entity.Property(e => e.NamaBangunan)
                    .IsRequired()
                    .HasColumnName("nama_bangunan")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKondisi)
                    .HasColumnName("nama_kondisi")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NilaiAset)
                    .HasColumnName("nilai_aset")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif')")
                    .HasDefaultValueSql("'Aktif'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusBangunan)
                    .HasColumnName("status_bangunan")
                    .HasColumnType("enum('Milik Sendiri','Sewa','Lainnya')")
                    .HasDefaultValueSql("'Milik Sendiri'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_bin");

                entity.Property(e => e.TanggalBangunan)
                    .HasColumnName("tanggal_bangunan")
                    .HasColumnType("date");

                entity.Property(e => e.UkuranL)
                    .HasColumnName("ukuran_l")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("Lebar Bangunan");

                entity.Property(e => e.UkuranP)
                    .HasColumnName("ukuran_p")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("Panjang Bangunan");
            });

            modelBuilder.Entity<VAsetHabisPakai>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_aset_habis_pakai");

                entity.Property(e => e.IsReturnable)
                    .HasColumnName("is_returnable")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBarang)
                    .IsRequired()
                    .HasColumnName("kode_barang")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaJurusan)
                    .HasColumnName("nama_jurusan")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKategori)
                    .HasColumnName("nama_kategori")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaLokasi)
                    .HasColumnName("nama_lokasi")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaRuang)
                    .HasColumnName("nama_ruang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif','Keluar','Tersedia')")
                    .HasDefaultValueSql("'Di Gudang'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StokAktual).HasColumnName("stok_aktual");

                entity.Property(e => e.StokAwal).HasColumnName("stok_awal");

                entity.Property(e => e.TanggalRegistrasi)
                    .HasColumnName("tanggal_registrasi")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VAsetTanah>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_aset_tanah");

                entity.Property(e => e.KodeTanah).HasColumnName("kode_tanah");

                entity.Property(e => e.LetakTanah)
                    .IsRequired()
                    .HasColumnName("letak_tanah")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LuasTanah).HasColumnName("luas_tanah");

                entity.Property(e => e.NamaLokasi)
                    .HasColumnName("nama_lokasi")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPemilik)
                    .IsRequired()
                    .HasColumnName("nama_pemilik")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NilaiAset)
                    .HasColumnName("nilai_aset")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.NomorSertifikat)
                    .HasColumnName("nomor_sertifikat")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Penggunaan)
                    .HasColumnName("penggunaan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Di Gudang','Aktif','Dipinjam','Nonaktif')")
                    .HasDefaultValueSql("'Aktif'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusHak)
                    .HasColumnName("status_hak")
                    .HasColumnType("enum('Hak Milik','Hak Pakai','Hak Guna Bangunan','Sewa','Lainnya')")
                    .HasDefaultValueSql("'Hak Milik'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SumberPerolehan)
                    .HasColumnName("sumber_perolehan")
                    .HasColumnType("enum('Beli','Sumbangan','Hibah','Lainnya')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPerolehan)
                    .HasColumnName("tanggal_perolehan")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VBangunanNonAktif>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_bangunan_non_aktif");

                entity.Property(e => e.IdBangunanNonAktif).HasColumnName("id_bangunan_non_aktif");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBangunan)
                    .HasColumnName("kode_bangunan")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LuasBangunan).HasColumnName("luas_bangunan");

                entity.Property(e => e.NamaBangunan)
                    .HasColumnName("nama_bangunan")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Penyebab)
                    .HasColumnName("penyebab")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalNonaktif)
                    .HasColumnName("tanggal_nonaktif")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VBarangKeluar>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_barang_keluar");

                entity.Property(e => e.JumlahKeluar).HasColumnName("jumlah_keluar");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBarang)
                    .IsRequired()
                    .HasColumnName("kode_barang")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaGudang)
                    .HasColumnName("nama_gudang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPenerima)
                    .HasColumnName("nama_penerima")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPetugas)
                    .HasColumnName("nama_petugas")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaRuang)
                    .HasColumnName("nama_ruang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NoTransaksi).HasColumnName("no_transaksi");

                entity.Property(e => e.TanggalKeluar)
                    .HasColumnName("tanggal_keluar")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VBarangNonAktif>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_barang_non_aktif");

                entity.Property(e => e.IdBarangNonAktif).HasColumnName("id_barang_non_aktif");

                entity.Property(e => e.Jumlah)
                    .HasColumnName("jumlah")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKategori)
                    .HasColumnName("nama_kategori")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaLokasi)
                    .HasColumnName("nama_lokasi")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaRuang)
                    .HasColumnName("nama_ruang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Penyebab)
                    .HasColumnName("penyebab")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalNonaktif)
                    .HasColumnName("tanggal_nonaktif")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VLaporanKerusakan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_laporan_kerusakan");

                entity.Property(e => e.DeskripsiKerusakan)
                    .IsRequired()
                    .HasColumnName("deskripsi_kerusakan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdKerusakan).HasColumnName("id_kerusakan");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.KodeInventaris)
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPelapor)
                    .HasColumnName("nama_pelapor")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusKerusakan)
                    .HasColumnName("status_kerusakan")
                    .HasColumnType("enum('Menunggu Pemeriksaan','Sedang Diperbaiki','Selesai','Tidak Bisa Diperbaiki')")
                    .HasDefaultValueSql("'Menunggu Pemeriksaan'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalLapor)
                    .HasColumnName("tanggal_lapor")
                    .HasColumnType("date");

                entity.Property(e => e.TingkatKerusakan)
                    .IsRequired()
                    .HasColumnName("tingkat_kerusakan")
                    .HasColumnType("enum('Ringan','Sedang','Berat')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<VLaporanPeminjaman>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_laporan_peminjaman");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.KodeInventaris)
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LamaPinjamHari).HasColumnName("lama_pinjam_hari");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPeminjam)
                    .IsRequired()
                    .HasColumnName("nama_peminjam")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NomorPeminjaman)
                    .IsRequired()
                    .HasColumnName("nomor_peminjaman")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NomorTelepon)
                    .HasColumnName("nomor_telepon")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusPeminjaman)
                    .HasColumnName("status_peminjaman")
                    .HasColumnType("enum('Sedang Dipinjam','Dikembalikan')")
                    .HasDefaultValueSql("'Sedang Dipinjam'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalJatuhTempo)
                    .HasColumnName("tanggal_jatuh_tempo")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalPinjam)
                    .HasColumnName("tanggal_pinjam")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VLaporanPengembalian>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_laporan_pengembalian");

                entity.Property(e => e.BatasWaktu)
                    .HasColumnName("batas_waktu")
                    .HasColumnType("date");

                entity.Property(e => e.IdPengembalian).HasColumnName("id_pengembalian");

                entity.Property(e => e.NamaPeminjam)
                    .IsRequired()
                    .HasColumnName("nama_peminjam")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NomorPeminjaman)
                    .IsRequired()
                    .HasColumnName("nomor_peminjaman")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalKembali)
                    .HasColumnName("tanggal_kembali")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalPinjam)
                    .HasColumnName("tanggal_pinjam")
                    .HasColumnType("date");

                entity.Property(e => e.TelatHari).HasColumnName("telat_hari");
            });

            modelBuilder.Entity<VLaporanPerbaikan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_laporan_perbaikan");

                entity.Property(e => e.BiayaPerbaikan)
                    .HasColumnName("biaya_perbaikan")
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.DeskripsiKerusakan)
                    .IsRequired()
                    .HasColumnName("deskripsi_kerusakan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdPerbaikan).HasColumnName("id_perbaikan");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.KodeInventaris)
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPerbaikan)
                    .HasColumnName("tanggal_perbaikan")
                    .HasColumnType("date");

                entity.Property(e => e.Teknisi)
                    .HasColumnName("teknisi")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TindakanPerbaikan)
                    .IsRequired()
                    .HasColumnName("tindakan_perbaikan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<VLaporanPermintaan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_laporan_permintaan");

                entity.Property(e => e.AlasanDisetujui)
                    .HasColumnName("alasan_disetujui")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AlasanKebutuhan)
                    .HasColumnName("alasan_kebutuhan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.JumlahDiminta).HasColumnName("jumlah_diminta");

                entity.Property(e => e.KodePermintaan)
                    .IsRequired()
                    .HasColumnName("kode_permintaan")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaJurusan)
                    .HasColumnName("nama_jurusan")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPeminta)
                    .HasColumnName("nama_peminta")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPenyetuju)
                    .HasColumnName("nama_penyetuju")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StatusPersetujuan)
                    .HasColumnName("status_persetujuan")
                    .HasColumnType("enum('Menunggu','Disetujui','Ditolak')")
                    .HasDefaultValueSql("'Menunggu'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPermintaan)
                    .HasColumnName("tanggal_permintaan")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalPersetujuan)
                    .HasColumnName("tanggal_persetujuan")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VMutasi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_mutasi");

                entity.Property(e => e.AlasanMutasi)
                    .HasColumnName("alasan_mutasi")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdMutasi).HasColumnName("id_mutasi");

                entity.Property(e => e.JurusanAsal)
                    .HasColumnName("jurusan_asal")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.JurusanTujuan)
                    .HasColumnName("jurusan_tujuan")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalMutasi)
                    .HasColumnName("tanggal_mutasi")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VOpnameAset>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_opname_aset");

                entity.Property(e => e.IdOpnameAset).HasColumnName("id_opname_aset");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeInventaris)
                    .IsRequired()
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KondisiTerkini)
                    .HasColumnName("kondisi_terkini")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaRuang)
                    .HasColumnName("nama_ruang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalOpname)
                    .HasColumnName("tanggal_opname")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VPengadaanAset>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_pengadaan_aset");

                entity.Property(e => e.HargaSatuan)
                    .HasColumnName("harga_satuan")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.IdPengadaan).HasColumnName("id_pengadaan");

                entity.Property(e => e.JumlahMasuk).HasColumnName("jumlah_masuk");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaGudang)
                    .HasColumnName("nama_gudang")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaKategori)
                    .HasColumnName("nama_kategori")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPemasok)
                    .HasColumnName("nama_pemasok")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaSumber)
                    .HasColumnName("nama_sumber")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('Menunggu Proses','Sedang Dibelanjakan','Selesai Dibelanjakan')")
                    .HasDefaultValueSql("'Menunggu Proses'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SubTotal)
                    .HasColumnName("sub_total")
                    .HasColumnType("decimal(25,2)");

                entity.Property(e => e.TahunAjaran)
                    .HasColumnName("tahun_ajaran")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalPengadaan)
                    .HasColumnName("tanggal_pengadaan")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VPenghapusanAset>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_penghapusan_aset");

                entity.Property(e => e.AlasanHapus)
                    .IsRequired()
                    .HasColumnName("alasan_hapus")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdPenghapusan).HasColumnName("id_penghapusan");

                entity.Property(e => e.KodeBarang).HasColumnName("kode_barang");

                entity.Property(e => e.KodeInventaris)
                    .HasColumnName("kode_inventaris")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaBarang)
                    .HasColumnName("nama_barang")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NamaPenyetuju)
                    .HasColumnName("nama_penyetuju")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalHapus)
                    .HasColumnName("tanggal_hapus")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VTanahNonAktif>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_tanah_non_aktif");

                entity.Property(e => e.IdTanahNonAktif).HasColumnName("id_tanah_non_aktif");

                entity.Property(e => e.Keterangan)
                    .HasColumnName("keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.KodeTanah)
                    .HasColumnName("kode_tanah")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LuasTanah).HasColumnName("luas_tanah");

                entity.Property(e => e.NamaPemilik)
                    .HasColumnName("nama_pemilik")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NomorSertifikat)
                    .HasColumnName("nomor_sertifikat")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Penyebab)
                    .HasColumnName("penyebab")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TanggalNonaktif)
                    .HasColumnName("tanggal_nonaktif")
                    .HasColumnType("date");
            });

            // Enterprise tables (Sprint 4)
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("audit_log");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.TableName).HasColumnName("table_name").HasColumnType("varchar(100)");
                entity.Property(e => e.RecordPK).HasColumnName("record_pk").HasColumnType("varchar(100)");
                entity.Property(e => e.Action).HasColumnName("action").HasColumnType("varchar(20)");
                entity.Property(e => e.OldJson).HasColumnName("old_json").HasColumnType("longtext");
                entity.Property(e => e.NewJson).HasColumnName("new_json").HasColumnType("longtext");
                entity.Property(e => e.IdPengguna).HasColumnName("id_pengguna");
                entity.Property(e => e.Username).HasColumnName("username").HasColumnType("varchar(100)");
                entity.Property(e => e.Timestamp).HasColumnName("timestamp").HasColumnType("datetime");
                entity.Property(e => e.Modul).HasColumnName("modul").HasColumnType("varchar(100)");
                entity.Property(e => e.IpAddress).HasColumnName("ip_address").HasColumnType("varchar(45)");
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
            });

            modelBuilder.Entity<Notifikasi>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("notifikasi");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Type).HasColumnName("type").HasColumnType("varchar(50)");
                entity.Property(e => e.Title).HasColumnName("title").HasColumnType("varchar(255)");
                entity.Property(e => e.Message).HasColumnName("message").HasColumnType("text");
                entity.Property(e => e.RefTable).HasColumnName("ref_table").HasColumnType("varchar(100)");
                entity.Property(e => e.RefId).HasColumnName("ref_id").HasColumnType("varchar(100)");
                entity.Property(e => e.IsRead).HasColumnName("is_read");
                entity.Property(e => e.IdPenerima).HasColumnName("id_penerima");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            });

            modelBuilder.Entity<AsetLampiran>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("aset_lampiran");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.TipeAset).HasColumnName("tipe_aset").HasColumnType("varchar(20)");
                entity.Property(e => e.RefId).HasColumnName("ref_id");
                entity.Property(e => e.RefKode).HasColumnName("ref_kode").HasColumnType("varchar(100)");
                entity.Property(e => e.FilePath).HasColumnName("file_path").HasColumnType("varchar(500)");
                entity.Property(e => e.OriginalFileName).HasColumnName("original_file_name").HasColumnType("varchar(255)");
                entity.Property(e => e.FileType).HasColumnName("file_type").HasColumnType("varchar(50)");
                entity.Property(e => e.FileSize).HasColumnName("file_size");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.Keterangan).HasColumnName("keterangan").HasColumnType("text");
            });

            modelBuilder.Entity<ApprovalWorkflowConfig>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("approval_workflow_config");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.WorkflowType).HasColumnName("workflow_type").HasColumnType("varchar(50)");
                entity.Property(e => e.Level).HasColumnName("level");
                entity.Property(e => e.IdPeranApprover).HasColumnName("id_peran_approver");
                entity.Property(e => e.IsRequired).HasColumnName("is_required");
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime");
            });

            modelBuilder.Entity<ApprovalStep>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("approval_step");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.WorkflowType).HasColumnName("workflow_type").HasColumnType("varchar(50)");
                entity.Property(e => e.RefId).HasColumnName("ref_id").HasColumnType("varchar(100)");
                entity.Property(e => e.Level).HasColumnName("level");
                entity.Property(e => e.IdApprover).HasColumnName("id_approver");
                entity.Property(e => e.IdPeranApprover).HasColumnName("id_peran_approver");
                entity.Property(e => e.Status).HasColumnName("status").HasColumnType("varchar(20)");
                entity.Property(e => e.Catatan).HasColumnName("catatan").HasColumnType("text");
                entity.Property(e => e.TanggalKeputusan).HasColumnName("tanggal_keputusan").HasColumnType("datetime");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
