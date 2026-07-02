using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Lemari
    {
        public Lemari()
        {
            Aset = new HashSet<Aset>();
        }

        public int IdLemari { get; set; }
        public string KodeLemari { get; set; }
        public string Nama { get; set; }
        public int? IdRuang { get; set; }

        public virtual Ruang IdRuangNavigation { get; set; }
        public virtual ICollection<Aset> Aset { get; set; }
    }
}
