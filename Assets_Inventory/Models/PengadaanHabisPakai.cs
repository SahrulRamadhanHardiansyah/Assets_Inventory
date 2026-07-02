using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class PengadaanHabisPakai
    {
        public PengadaanHabisPakai()
        {
            AsetHabisPakai = new HashSet<AsetHabisPakai>();
            DetailPengadaanHp = new HashSet<DetailPengadaanHp>();
            PengadaanPermintaanHp = new HashSet<PengadaanPermintaanHp>();
        }

        public int IdPengadaan { get; set; }
        public DateTime TanggalPengadaan { get; set; }
        public int? IdTahunAjaran { get; set; }
        public decimal? TotalHarga { get; set; }
        public string KodeGudang { get; set; }
        public int? IdSumberPerolehan { get; set; }
        public string Status { get; set; }
        public string Keterangan { get; set; }

        public virtual TahunAjaran IdTahunAjaranNavigation { get; set; }
        public virtual Gudang KodeGudangNavigation { get; set; }
        public virtual ICollection<AsetHabisPakai> AsetHabisPakai { get; set; }
        public virtual ICollection<DetailPengadaanHp> DetailPengadaanHp { get; set; }
        public virtual ICollection<PengadaanPermintaanHp> PengadaanPermintaanHp { get; set; }
    }
}
