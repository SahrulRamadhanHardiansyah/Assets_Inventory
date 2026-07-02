using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class BarangKeluar
    {
        public int NoTransaksi { get; set; }
        public DateTime TanggalKeluar { get; set; }
        public string KodeBarang { get; set; }
        public int JumlahKeluar { get; set; }
        public string Keterangan { get; set; }
        public int? IdRuang { get; set; }
        public int NamaPenerima { get; set; }
        public int Petugas { get; set; }
        public string KodeGudang { get; set; }

        public virtual Ruang IdRuangNavigation { get; set; }
        public virtual AsetHabisPakai KodeBarangNavigation { get; set; }
        public virtual Gudang KodeGudangNavigation { get; set; }
        public virtual Pengguna NamaPenerimaNavigation { get; set; }
        public virtual Pengguna PetugasNavigation { get; set; }
    }
}
