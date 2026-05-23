using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Mapel
    {
        public Mapel()
        {
            Pengguna = new HashSet<Pengguna>();
        }

        public int IdMapel { get; set; }
        public string NamaMapel { get; set; }

        public virtual ICollection<Pengguna> Pengguna { get; set; }
    }
}
