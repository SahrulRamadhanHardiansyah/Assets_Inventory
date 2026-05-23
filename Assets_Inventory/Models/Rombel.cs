using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Rombel
    {
        public Rombel()
        {
            Kelas = new HashSet<Kelas>();
        }

        public int IdRombel { get; set; }
        public string KodeRombel { get; set; }
        public int IdJurusan { get; set; }
        public string NamaRombel { get; set; }
        public string Tingkat { get; set; }
        public bool? IsActive { get; set; }

        public virtual Jurusan IdJurusanNavigation { get; set; }
        public virtual ICollection<Kelas> Kelas { get; set; }
    }
}
