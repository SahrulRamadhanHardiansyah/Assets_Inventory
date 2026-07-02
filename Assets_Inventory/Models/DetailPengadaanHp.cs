using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class DetailPengadaanHp
    {
        public int IdDetailPengadaanHp { get; set; }
        public int IdPengadaanHp { get; set; }
        public int? IdPemasok { get; set; }
        public int IdMasterBarang { get; set; }
        public int JumlahMasuk { get; set; }
        public decimal HargaSatuan { get; set; }

        public virtual MasterBarang IdMasterBarangNavigation { get; set; }
        public virtual Pemasok IdPemasokNavigation { get; set; }
        public virtual PengadaanHabisPakai IdPengadaanHpNavigation { get; set; }
    }
}
