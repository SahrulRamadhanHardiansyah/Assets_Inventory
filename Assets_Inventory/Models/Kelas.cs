using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Kelas
    {
        public Kelas()
        {
            Pengguna = new HashSet<Pengguna>();
        }

        public int IdKelas { get; set; }
        public int IdRombel { get; set; }
        public string NamaKelas { get; set; }
        public string TahunAjaran { get; set; }

        public virtual Rombel IdRombelNavigation { get; set; }
        public virtual ICollection<Pengguna> Pengguna { get; set; }
    }
}
