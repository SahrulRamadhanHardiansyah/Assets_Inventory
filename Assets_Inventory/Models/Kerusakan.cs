using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Kerusakan
    {
        public Kerusakan()
        {
            Perbaikan = new HashSet<Perbaikan>();
        }

        public int IdKerusakan { get; set; }
        public string KodeBarang { get; set; }
        public DateTime TanggalLapor { get; set; }
        public int IdPelapor { get; set; }
        public string DeskripsiKerusakan { get; set; }
        public string TingkatKerusakan { get; set; }
        public string StatusKerusakan { get; set; }

        public virtual Pengguna IdPelaporNavigation { get; set; }
        public virtual Aset KodeBarangNavigation { get; set; }
        public virtual ICollection<Perbaikan> Perbaikan { get; set; }
    }
}
