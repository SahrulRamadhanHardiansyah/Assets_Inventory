using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class DetailPengadaan
    {
        public DetailPengadaan()
        {
            Aset = new HashSet<Aset>();
        }

        public int IdDetailPengadaan { get; set; }
        public int IdPengadaan { get; set; }
        public int? IdPemasok { get; set; }
        public int IdMasterBarang { get; set; }
        public int JumlahMasuk { get; set; }
        public decimal HargaSatuan { get; set; }
        public bool? Status { get; set; }

        public virtual MasterBarang IdMasterBarangNavigation { get; set; }
        public virtual Pemasok IdPemasokNavigation { get; set; }
        public virtual Pengadaan IdPengadaanNavigation { get; set; }
        public virtual ICollection<Aset> Aset { get; set; }
    }
}
