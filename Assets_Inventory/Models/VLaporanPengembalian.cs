using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VLaporanPengembalian
    {
        public int IdPengembalian { get; set; }
        public string NomorPeminjaman { get; set; }
        public string NamaPeminjam { get; set; }
        public DateTime TanggalPinjam { get; set; }
        public DateTime? BatasWaktu { get; set; }
        public DateTime TanggalKembali { get; set; }
        public int? TelatHari { get; set; }
    }
}
