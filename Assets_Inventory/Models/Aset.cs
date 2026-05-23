using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Aset
    {
        public Aset()
        {
            BarangNonAktif = new HashSet<BarangNonAktif>();
            DetailPeminjaman = new HashSet<DetailPeminjaman>();
            Kerusakan = new HashSet<Kerusakan>();
            Mutasi = new HashSet<Mutasi>();
            OpnameAset = new HashSet<OpnameAset>();
            PenghapusanAset = new HashSet<PenghapusanAset>();
        }

        public string KodeBarang { get; set; }
        public int? IdDetailPengadaan { get; set; }
        public int IdMasterBarang { get; set; }
        public int? IdJurusan { get; set; }
        public int? IdRuang { get; set; }
        public int? IdLokasi { get; set; }
        public string NoSeri { get; set; }
        public decimal? HargaSatuan { get; set; }
        public decimal? NilaiResidu { get; set; }
        public int? UmurEkonomi { get; set; }
        public string KodeInventaris { get; set; }
        public string Status { get; set; }
        public int? IdKondisi { get; set; }
        public DateTime TanggalRegistrasi { get; set; }
        public string Gambar { get; set; }
        public string Keterangan { get; set; }

        public virtual DetailPengadaan IdDetailPengadaanNavigation { get; set; }
        public virtual Jurusan IdJurusanNavigation { get; set; }
        public virtual Kondisi IdKondisiNavigation { get; set; }
        public virtual Lokasi IdLokasiNavigation { get; set; }
        public virtual MasterBarang IdMasterBarangNavigation { get; set; }
        public virtual Ruang IdRuangNavigation { get; set; }
        public virtual ICollection<BarangNonAktif> BarangNonAktif { get; set; }
        public virtual ICollection<DetailPeminjaman> DetailPeminjaman { get; set; }
        public virtual ICollection<Kerusakan> Kerusakan { get; set; }
        public virtual ICollection<Mutasi> Mutasi { get; set; }
        public virtual ICollection<OpnameAset> OpnameAset { get; set; }
        public virtual ICollection<PenghapusanAset> PenghapusanAset { get; set; }
    }
}
