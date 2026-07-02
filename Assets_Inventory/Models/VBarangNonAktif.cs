using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VBarangNonAktif
    {
        public int IdBarangNonAktif { get; set; }
        public DateTime TanggalNonaktif { get; set; }
        public string KodeInventaris { get; set; }
        public string NamaBarang { get; set; }
        public string NamaKategori { get; set; }
        public string NamaLokasi { get; set; }
        public string NamaRuang { get; set; }
        public int? Jumlah { get; set; }
        public string Penyebab { get; set; }
        public string Keterangan { get; set; }
    }
}
