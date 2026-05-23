using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Merek
    {
        public Merek()
        {
            MasterBarang = new HashSet<MasterBarang>();
        }

        public int IdMerek { get; set; }
        public string NamaMerek { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<MasterBarang> MasterBarang { get; set; }
    }
}
