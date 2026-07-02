using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VBangunanNonAktif
    {
        public int IdBangunanNonAktif { get; set; }
        public int? KodeBangunan { get; set; }
        public string NamaBangunan { get; set; }
        public int? LuasBangunan { get; set; }
        public DateTime TanggalNonaktif { get; set; }
        public string Penyebab { get; set; }
        public string Keterangan { get; set; }
    }
}
