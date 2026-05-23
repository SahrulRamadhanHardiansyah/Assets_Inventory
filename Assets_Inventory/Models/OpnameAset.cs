using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class OpnameAset
    {
        public int IdOpnameAset { get; set; }
        public string KodeInventaris { get; set; }
        public DateTime TanggalOpname { get; set; }
        public string Keterangan { get; set; }
        public int? IdKondisi { get; set; }

        public virtual Kondisi IdKondisiNavigation { get; set; }
        public virtual Aset KodeInventarisNavigation { get; set; }
    }
}
