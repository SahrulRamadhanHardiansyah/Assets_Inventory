using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Lokasi
    {
        public Lokasi()
        {
            Aset = new HashSet<Aset>();
            AsetHabisPakai = new HashSet<AsetHabisPakai>();
            AsetTanah = new HashSet<AsetTanah>();
            Ruang = new HashSet<Ruang>();
        }

        public int IdLokasi { get; set; }
        public string KodeLokasi { get; set; }
        public string NamaLokasi { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<Aset> Aset { get; set; }
        public virtual ICollection<AsetHabisPakai> AsetHabisPakai { get; set; }
        public virtual ICollection<AsetTanah> AsetTanah { get; set; }
        public virtual ICollection<Ruang> Ruang { get; set; }
    }
}
