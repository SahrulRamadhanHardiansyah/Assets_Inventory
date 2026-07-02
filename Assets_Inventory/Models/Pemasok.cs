using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Pemasok
    {
        public Pemasok()
        {
            DetailPengadaan = new HashSet<DetailPengadaan>();
            DetailPengadaanHp = new HashSet<DetailPengadaanHp>();
        }

        public int IdPemasok { get; set; }
        public string NamaPemasok { get; set; }
        public string NomorTelepon { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<DetailPengadaan> DetailPengadaan { get; set; }
        public virtual ICollection<DetailPengadaanHp> DetailPengadaanHp { get; set; }
    }
}
