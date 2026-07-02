using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VLaporanKerusakan
    {
        public int IdKerusakan { get; set; }
        public DateTime TanggalLapor { get; set; }
        public int KodeBarang { get; set; }
        public string KodeInventaris { get; set; }
        public string NamaBarang { get; set; }
        public string NamaPelapor { get; set; }
        public string DeskripsiKerusakan { get; set; }
        public string TingkatKerusakan { get; set; }
        public string StatusKerusakan { get; set; }
    }
}
