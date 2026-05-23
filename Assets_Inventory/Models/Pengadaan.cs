using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Pengadaan
    {
        public Pengadaan()
        {
            DetailPengadaan = new HashSet<DetailPengadaan>();
            PengadaanPermintaan = new HashSet<PengadaanPermintaan>();
        }

        public int IdPengadaan { get; set; }
        public DateTime TanggalPengadaan { get; set; }
        public decimal? TotalHarga { get; set; }
        public string Keterangan { get; set; }
        public string KodeGudang { get; set; }
        public int? IdSumberPerolehan { get; set; }
        public string Status { get; set; }

        public virtual SumberPerolehan IdSumberPerolehanNavigation { get; set; }
        public virtual Gudang KodeGudangNavigation { get; set; }
        public virtual ICollection<DetailPengadaan> DetailPengadaan { get; set; }
        public virtual ICollection<PengadaanPermintaan> PengadaanPermintaan { get; set; }
    }
}
