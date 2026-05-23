using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Perbaikan
    {
        public int IdPerbaikan { get; set; }
        public int IdKerusakan { get; set; }
        public DateTime TanggalPerbaikan { get; set; }
        public string Teknisi { get; set; }
        public decimal? BiayaPerbaikan { get; set; }
        public string TindakanPerbaikan { get; set; }

        public virtual Kerusakan IdKerusakanNavigation { get; set; }
    }
}
