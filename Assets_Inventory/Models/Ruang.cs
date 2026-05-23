using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Ruang
    {
        public Ruang()
        {
            Aset = new HashSet<Aset>();
            AsetHabisPakai = new HashSet<AsetHabisPakai>();
            BarangKeluar = new HashSet<BarangKeluar>();
            Lemari = new HashSet<Lemari>();
        }

        public int IdRuang { get; set; }
        public string KodeRuang { get; set; }
        public int? IdLokasi { get; set; }
        public string NamaRuang { get; set; }
        public string Lantai { get; set; }
        public string Keterangan { get; set; }
        public bool? IsActive { get; set; }

        public virtual Lokasi IdLokasiNavigation { get; set; }
        public virtual ICollection<Aset> Aset { get; set; }
        public virtual ICollection<AsetHabisPakai> AsetHabisPakai { get; set; }
        public virtual ICollection<BarangKeluar> BarangKeluar { get; set; }
        public virtual ICollection<Lemari> Lemari { get; set; }
    }
}
