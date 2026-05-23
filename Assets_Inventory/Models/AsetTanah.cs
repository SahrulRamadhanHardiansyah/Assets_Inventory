using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class AsetTanah
    {
        public AsetTanah()
        {
            TanahNonAktif = new HashSet<TanahNonAktif>();
        }

        public int KodeTanah { get; set; }
        public string NamaPemilik { get; set; }
        public int? IdLokasi { get; set; }
        public int LuasTanah { get; set; }
        public string LetakTanah { get; set; }
        public string NomorSertifikat { get; set; }
        public string StatusHak { get; set; }
        public decimal? NilaiAset { get; set; }
        public string Penggunaan { get; set; }
        public DateTime? TanggalPerolehan { get; set; }
        public string SumberPerolehan { get; set; }

        public virtual Lokasi IdLokasiNavigation { get; set; }
        public virtual ICollection<TanahNonAktif> TanahNonAktif { get; set; }
    }
}
