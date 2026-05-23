using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Pengembalian
    {
        public int IdPengembalian { get; set; }
        public string NomorPeminjaman { get; set; }
        public DateTime TanggalKembali { get; set; }

        public virtual Peminjaman NomorPeminjamanNavigation { get; set; }
    }
}
