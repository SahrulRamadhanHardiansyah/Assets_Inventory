using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class PenghapusanAset
    {
        public int IdPenghapusan { get; set; }
        public int KodeBarang { get; set; }
        public DateTime TanggalHapus { get; set; }
        public string AlasanHapus { get; set; }
        public int IdPenyetuju { get; set; }

        public virtual Pengguna IdPenyetujuNavigation { get; set; }
        public virtual Aset KodeBarangNavigation { get; set; }
    }
}
