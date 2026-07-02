using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VPengadaanAset
    {
        public int IdPengadaan { get; set; }
        public DateTime TanggalPengadaan { get; set; }
        public string TahunAjaran { get; set; }
        public string NamaSumber { get; set; }
        public string NamaGudang { get; set; }
        public string NamaBarang { get; set; }
        public string NamaKategori { get; set; }
        public int JumlahMasuk { get; set; }
        public decimal HargaSatuan { get; set; }
        public decimal SubTotal { get; set; }
        public string NamaPemasok { get; set; }
        public string Status { get; set; }
        public string Keterangan { get; set; }
    }
}
