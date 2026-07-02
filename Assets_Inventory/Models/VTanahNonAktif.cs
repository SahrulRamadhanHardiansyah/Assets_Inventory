using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VTanahNonAktif
    {
        public int IdTanahNonAktif { get; set; }
        public int? KodeTanah { get; set; }
        public string NomorSertifikat { get; set; }
        public string NamaPemilik { get; set; }
        public int? LuasTanah { get; set; }
        public DateTime TanggalNonaktif { get; set; }
        public string Penyebab { get; set; }
        public string Keterangan { get; set; }
    }
}
