using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VLaporanPermintaan
    {
        public string KodePermintaan { get; set; }
        public DateTime TanggalPermintaan { get; set; }
        public string NamaJurusan { get; set; }
        public string NamaPeminta { get; set; }
        public string NamaBarang { get; set; }
        public int JumlahDiminta { get; set; }
        public string AlasanKebutuhan { get; set; }
        public string StatusPersetujuan { get; set; }
        public DateTime? TanggalPersetujuan { get; set; }
        public string NamaPenyetuju { get; set; }
        public string AlasanDisetujui { get; set; }
    }
}
