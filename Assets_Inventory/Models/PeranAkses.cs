using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class PeranAkses
    {
        public int IdPeranAkses { get; set; }
        public int IdPeran { get; set; }
        public int IdAkses { get; set; }
        public bool? HakBuat { get; set; }
        public bool? HakBaca { get; set; }
        public bool? HakUbah { get; set; }
        public bool? HakHapus { get; set; }

        public virtual Akses IdAksesNavigation { get; set; }
        public virtual Peran IdPeranNavigation { get; set; }
    }
}
