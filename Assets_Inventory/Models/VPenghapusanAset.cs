using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VPenghapusanAset
    {
        public int IdPenghapusan { get; set; }
        public DateTime TanggalHapus { get; set; }
        public int KodeBarang { get; set; }
        public string KodeInventaris { get; set; }
        public string NamaBarang { get; set; }
        public string AlasanHapus { get; set; }
        public string NamaPenyetuju { get; set; }
    }
}
