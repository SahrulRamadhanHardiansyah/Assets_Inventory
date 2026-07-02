using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VAsetBangunan
    {
        public int KodeBangunan { get; set; }
        public string NamaBangunan { get; set; }
        public int LuasBangunan { get; set; }
        public decimal? UkuranP { get; set; }
        public decimal? UkuranL { get; set; }
        public string Konstruksi { get; set; }
        public string StatusBangunan { get; set; }
        public decimal? NilaiAset { get; set; }
        public DateTime? TanggalBangunan { get; set; }
        public string NamaKondisi { get; set; }
        public string Status { get; set; }
        public string Keterangan { get; set; }
    }
}
