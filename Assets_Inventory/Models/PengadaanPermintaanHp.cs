using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class PengadaanPermintaanHp
    {
        public int IdPengadaanHp { get; set; }
        public string KodePermintaanHp { get; set; }

        public virtual PengadaanHabisPakai IdPengadaanHpNavigation { get; set; }
        public virtual PermintaanHp KodePermintaanHpNavigation { get; set; }
    }
}
