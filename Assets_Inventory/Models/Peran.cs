using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Peran
    {
        public Peran()
        {
            Pengguna = new HashSet<Pengguna>();
            PeranAkses = new HashSet<PeranAkses>();
        }

        public int IdPeran { get; set; }
        public string NamaPeran { get; set; }

        public virtual ICollection<Pengguna> Pengguna { get; set; }
        public virtual ICollection<PeranAkses> PeranAkses { get; set; }
    }
}
