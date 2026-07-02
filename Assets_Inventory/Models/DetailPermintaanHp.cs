using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class DetailPermintaanHp
    {
        public int IdDetailPermintaanHp { get; set; }
        public string KodePermintaanHp { get; set; }
        public int IdMasterBarang { get; set; }
        public int JumlahDiminta { get; set; }
        public string AlasanKebutuhan { get; set; }

        public virtual MasterBarang IdMasterBarangNavigation { get; set; }
        public virtual PermintaanHp KodePermintaanHpNavigation { get; set; }
    }
}
