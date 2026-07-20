namespace Assets_Inventory.Helper
{
    /// <summary>
    /// Centralized constants to avoid magic strings duplication.
    /// </summary>
    public static class AppConstants
    {
        // Status Aset
        public const string StatusAktif = "Aktif";
        public const string StatusDiGudang = "Di Gudang";
        public const string StatusDipinjam = "Dipinjam";
        public const string StatusNonaktif = "Nonaktif";
        public const string StatusKeluar = "Keluar";
        public const string StatusTersedia = "Tersedia";

        // Status Persetujuan
        public const string PersetujuanMenunggu = "Menunggu";
        public const string PersetujuanDisetujui = "Disetujui";
        public const string PersetujuanDitolak = "Ditolak";

        // Status Pengadaan
        public const string PengadaanMenungguProses = "Menunggu Proses";
        public const string PengadaanSedangDibelanjakan = "Sedang Dibelanjakan";
        public const string PengadaanSelesaiDibelanjakan = "Selesai Dibelanjakan";
        public const string PengadaanDibelanjakan = "Dibelanjakan";
        public const string PengadaanSelesai = "Selesai";

        // Status Peminjaman
        public const string PeminjamanSedangDipinjam = "Sedang Dipinjam";
        public const string PeminjamanDikembalikan = "Dikembalikan";

        // Tingkat Kerusakan
        public const string KerusakanRingan = "Ringan";
        public const string KerusakanSedang = "Sedang";
        public const string KerusakanBerat = "Berat";

        // Status Kerusakan
        public const string KerusakanMenungguPemeriksaan = "Menunggu Pemeriksaan";
        public const string KerusakanSedangDiperbaiki = "Sedang Diperbaiki";
        public const string KerusakanSelesai = "Selesai";
        public const string KerusakanTidakBisaDiperbaiki = "Tidak Bisa Diperbaiki";

        // Jenis Barang
        public const string JenisBarangAset = "Aset";
        public const string JenisBarangHabisPakai = "Habis Pakai";

        // BCrypt
        public const int BcryptWorkFactor = 12;

        // Paging
        public const int DefaultPageSize = 100;

        // File limits
        public const long MaxImageFileSizeBytes = 10 * 1024 * 1024; // 10MB
        public const long MaxAttachmentFileSizeBytes = 20 * 1024 * 1024; // 20MB
        public const int MaxBackupFileSizeBytes = 500 * 1024 * 1024; // 500MB

        // Session
        public const int SessionTimeoutHours = 8;
        public const int MaxFailedLoginAttempts = 5;
        public const int LockoutMinutes = 15;
    }
}
