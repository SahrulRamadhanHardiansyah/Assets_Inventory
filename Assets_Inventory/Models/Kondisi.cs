using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Kondisi
    {
        public Kondisi()
        {
            Aset = new HashSet<Aset>();
            AsetBangunan = new HashSet<AsetBangunan>();
            OpnameAset = new HashSet<OpnameAset>();
        }

        public int IdKondisi { get; set; }
        public string NamaKondisi { get; set; }
        public string Keterangan { get; set; }

        public virtual ICollection<Aset> Aset { get; set; }
        public virtual ICollection<AsetBangunan> AsetBangunan { get; set; }
        public virtual ICollection<OpnameAset> OpnameAset { get; set; }
    }
}
