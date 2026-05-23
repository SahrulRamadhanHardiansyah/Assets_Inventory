using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Pengguna = new HashSet<Pengguna>();
        }

        public int IdUnit { get; set; }
        public string NamaUnit { get; set; }

        public virtual ICollection<Pengguna> Pengguna { get; set; }
    }
}
