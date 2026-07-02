using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VOpnameAset
    {
        public int IdOpnameAset { get; set; }
        public DateTime TanggalOpname { get; set; }
        public string KodeInventaris { get; set; }
        public string NamaBarang { get; set; }
        public string NamaRuang { get; set; }
        public string KondisiTerkini { get; set; }
        public string Keterangan { get; set; }
    }
}
