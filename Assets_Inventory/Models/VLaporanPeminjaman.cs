using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VLaporanPeminjaman
    {
        public string NomorPeminjaman { get; set; }
        public DateTime TanggalPinjam { get; set; }
        public string NamaPeminjam { get; set; }
        public string NomorTelepon { get; set; }
        public int LamaPinjamHari { get; set; }
        public DateTime? TanggalJatuhTempo { get; set; }
        public int KodeBarang { get; set; }
        public string KodeInventaris { get; set; }
        public string NamaBarang { get; set; }
        public string StatusPeminjaman { get; set; }
        public string Keterangan { get; set; }
    }
}
