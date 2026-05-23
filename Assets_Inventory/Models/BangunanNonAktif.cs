using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class BangunanNonAktif
    {
        public int IdBangunanNonAktif { get; set; }
        public int KodeBangunan { get; set; }
        public int? IdStatus { get; set; }
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; }

        public virtual StatusBarang IdStatusNavigation { get; set; }
        public virtual AsetBangunan KodeBangunanNavigation { get; set; }
    }
}
