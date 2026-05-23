using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Akses
    {
        public Akses()
        {
            InverseIdParentNavigation = new HashSet<Akses>();
            PeranAkses = new HashSet<PeranAkses>();
        }

        public int IdAkses { get; set; }
        public int? IdParent { get; set; }
        public string NamaModul { get; set; }

        public virtual Akses IdParentNavigation { get; set; }
        public virtual ICollection<Akses> InverseIdParentNavigation { get; set; }
        public virtual ICollection<PeranAkses> PeranAkses { get; set; }
    }
}
