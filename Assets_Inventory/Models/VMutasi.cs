using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VMutasi
    {
        public int IdMutasi { get; set; }
        public DateTime TanggalMutasi { get; set; }
        public string KodeInventaris { get; set; }
        public string NamaBarang { get; set; }
        public string JurusanAsal { get; set; }
        public string JurusanTujuan { get; set; }
        public string AlasanMutasi { get; set; }
    }
}
