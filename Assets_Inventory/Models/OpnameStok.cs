using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class OpnameStok
    {
        public int IdOpnameStok { get; set; }
        public int IdMasterBarang { get; set; }
        public DateTime TanggalOpname { get; set; }
        public int StokSistem { get; set; }
        public int StokFisik { get; set; }
        public int Selisih { get; set; }
        public string Keterangan { get; set; }
        public int IdPemeriksa { get; set; }

        public virtual MasterBarang IdMasterBarangNavigation { get; set; }
        public virtual Pengguna IdPemeriksaNavigation { get; set; }
    }
}
