using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Kategori
    {
        public Kategori()
        {
            MasterBarang = new HashSet<MasterBarang>();
        }

        public int IdKategori { get; set; }
        public string NamaKategori { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<MasterBarang> MasterBarang { get; set; }
    }
}
