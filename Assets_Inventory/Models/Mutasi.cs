using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Mutasi
    {
        public int IdMutasi { get; set; }
        public string KodeInventaris { get; set; }
        public int? IdJurusanAsal { get; set; }
        public int IdJurusanTujuan { get; set; }
        public DateTime TanggalMutasi { get; set; }
        public string AlasanMutasi { get; set; }
        public bool? IsApproved { get; set; }

        public virtual Jurusan IdJurusanAsalNavigation { get; set; }
        public virtual Jurusan IdJurusanTujuanNavigation { get; set; }
        public virtual Aset KodeInventarisNavigation { get; set; }
    }
}
