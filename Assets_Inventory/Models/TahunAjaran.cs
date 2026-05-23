using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class TahunAjaran
    {
        public int IdTahunAjaran { get; set; }
        public string TahunAjaran1 { get; set; }
        public string Semester { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalSelesai { get; set; }
        public bool? IsActive { get; set; }
    }
}
