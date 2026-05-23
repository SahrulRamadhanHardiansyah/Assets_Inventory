using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class StatusBarang
    {
        public StatusBarang()
        {
            BangunanNonAktif = new HashSet<BangunanNonAktif>();
            BarangNonAktif = new HashSet<BarangNonAktif>();
            TanahNonAktif = new HashSet<TanahNonAktif>();
        }

        public int IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<BangunanNonAktif> BangunanNonAktif { get; set; }
        public virtual ICollection<BarangNonAktif> BarangNonAktif { get; set; }
        public virtual ICollection<TanahNonAktif> TanahNonAktif { get; set; }
    }
}
