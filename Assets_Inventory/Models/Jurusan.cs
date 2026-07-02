using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Jurusan
    {
        public Jurusan()
        {
            Aset = new HashSet<Aset>();
            AsetHabisPakai = new HashSet<AsetHabisPakai>();
            MutasiIdJurusanAsalNavigation = new HashSet<Mutasi>();
            MutasiIdJurusanTujuanNavigation = new HashSet<Mutasi>();
            Pengguna = new HashSet<Pengguna>();
            Permintaan = new HashSet<Permintaan>();
            PermintaanHp = new HashSet<PermintaanHp>();
            Rombel = new HashSet<Rombel>();
        }

        public int IdJurusan { get; set; }
        public string NamaJurusan { get; set; }

        public virtual ICollection<Aset> Aset { get; set; }
        public virtual ICollection<AsetHabisPakai> AsetHabisPakai { get; set; }
        public virtual ICollection<Mutasi> MutasiIdJurusanAsalNavigation { get; set; }
        public virtual ICollection<Mutasi> MutasiIdJurusanTujuanNavigation { get; set; }
        public virtual ICollection<Pengguna> Pengguna { get; set; }
        public virtual ICollection<Permintaan> Permintaan { get; set; }
        public virtual ICollection<PermintaanHp> PermintaanHp { get; set; }
        public virtual ICollection<Rombel> Rombel { get; set; }
    }
}
