using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class MasterBarang
    {
        public MasterBarang()
        {
            Aset = new HashSet<Aset>();
            AsetHabisPakai = new HashSet<AsetHabisPakai>();
            DetailPengadaan = new HashSet<DetailPengadaan>();
            DetailPermintaan = new HashSet<DetailPermintaan>();
            OpnameStok = new HashSet<OpnameStok>();
        }

        public int IdMasterBarang { get; set; }
        public string NamaBarang { get; set; }
        public int? IdKategori { get; set; }
        public int? IdMerek { get; set; }
        public int? IdSatuan { get; set; }
        public string JenisBarang { get; set; }
        public string Keterangan { get; set; }

        public virtual Kategori IdKategoriNavigation { get; set; }
        public virtual Merek IdMerekNavigation { get; set; }
        public virtual Satuan IdSatuanNavigation { get; set; }
        public virtual ICollection<Aset> Aset { get; set; }
        public virtual ICollection<AsetHabisPakai> AsetHabisPakai { get; set; }
        public virtual ICollection<DetailPengadaan> DetailPengadaan { get; set; }
        public virtual ICollection<DetailPermintaan> DetailPermintaan { get; set; }
        public virtual ICollection<OpnameStok> OpnameStok { get; set; }
    }
}
