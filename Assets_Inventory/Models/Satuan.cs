using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Satuan
    {
        public Satuan()
        {
            MasterBarang = new HashSet<MasterBarang>();
        }

        public int IdSatuan { get; set; }
        public string KodeSatuan { get; set; }
        public string NamaSatuan { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<MasterBarang> MasterBarang { get; set; }
    }
}
