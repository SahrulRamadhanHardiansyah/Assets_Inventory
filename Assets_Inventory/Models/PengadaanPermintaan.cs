using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class PengadaanPermintaan
    {
        public int IdPengadaan { get; set; }
        public string KodePermintaan { get; set; }

        public virtual Pengadaan IdPengadaanNavigation { get; set; }
        public virtual Permintaan KodePermintaanNavigation { get; set; }
    }
}
