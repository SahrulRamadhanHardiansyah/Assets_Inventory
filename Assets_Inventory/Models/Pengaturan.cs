using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Pengaturan
    {
        public int IdPengaturan { get; set; }
        public string NamaInstansi { get; set; }
        public string AlamatInstansi { get; set; }
        public string LogoInstansi { get; set; }
        public string WallpaperAplikasi { get; set; }
        public int? Telpon { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Kota { get; set; }
        public string KepalaSekolah { get; set; }
        public string Nip { get; set; }
        public string BagianInventaris { get; set; }
        public string PaketGolongan { get; set; }
    }
}
