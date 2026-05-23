using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class TanahNonAktif
    {
        public int IdTanahNonAktif { get; set; }
        public int KodeTanah { get; set; }
        public int? IdStatus { get; set; }
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; }

        public virtual StatusBarang IdStatusNavigation { get; set; }
        public virtual AsetTanah KodeTanahNavigation { get; set; }
    }
}
