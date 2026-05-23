using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class SumberPerolehan
    {
        public SumberPerolehan()
        {
            Pengadaan = new HashSet<Pengadaan>();
        }

        public int IdSumberPerolehan { get; set; }
        public string KodeSumber { get; set; }
        public string NamaSumber { get; set; }
        public string Keterangan { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Pengadaan> Pengadaan { get; set; }
    }
}
