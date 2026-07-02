using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VLaporanPerbaikan
    {
        public int IdPerbaikan { get; set; }
        public DateTime TanggalPerbaikan { get; set; }
        public int KodeBarang { get; set; }
        public string KodeInventaris { get; set; }
        public string NamaBarang { get; set; }
        public string DeskripsiKerusakan { get; set; }
        public string Teknisi { get; set; }
        public string TindakanPerbaikan { get; set; }
        public decimal? BiayaPerbaikan { get; set; }
    }
}
