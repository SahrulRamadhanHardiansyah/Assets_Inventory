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
            BarangKeluar = new HashSet<BarangKeluar>();
        }

        public int IdPengadaanHp { get; set; }
        public string KodeInventaris { get; set; }
        public DateTime TanggalPengadaan { get; set; }
        public string KodeBarang { get; set; }
        public int Jumlah { get; set; }
        public decimal? HargaSatuan { get; set; }
        public int? IdPemasok { get; set; }
        public string KodeGudang { get; set; }
        public string Keterangan { get; set; }

        public virtual Pemasok IdPemasokNavigation { get; set; }
        public virtual Gudang KodeGudangNavigation { get; set; }
        public virtual ICollection<BarangKeluar> BarangKeluar { get; set; }
    }
}
