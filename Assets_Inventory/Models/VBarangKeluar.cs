using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VBarangKeluar
    {
        public int NoTransaksi { get; set; }
        public DateTime TanggalKeluar { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public int JumlahKeluar { get; set; }
        public string NamaGudang { get; set; }
        public string NamaRuang { get; set; }
        public string NamaPenerima { get; set; }
        public string NamaPetugas { get; set; }
        public string Keterangan { get; set; }
    }
}
