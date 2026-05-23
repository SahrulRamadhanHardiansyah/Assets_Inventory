using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Gudang
    {
        public Gudang()
        {
            BarangKeluar = new HashSet<BarangKeluar>();
            Pengadaan = new HashSet<Pengadaan>();
            PengadaanHabisPakai = new HashSet<PengadaanHabisPakai>();
        }

        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<BarangKeluar> BarangKeluar { get; set; }
        public virtual ICollection<Pengadaan> Pengadaan { get; set; }
        public virtual ICollection<PengadaanHabisPakai> PengadaanHabisPakai { get; set; }
    }
}
