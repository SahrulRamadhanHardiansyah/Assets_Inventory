using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Permintaan
    {
        public Permintaan()
        {
            DetailPermintaan = new HashSet<DetailPermintaan>();
            PengadaanPermintaan = new HashSet<PengadaanPermintaan>();
        }

        public string KodePermintaan { get; set; }
        public int? IdPengguna { get; set; }
        public int? IdJurusan { get; set; }
        public DateTime TanggalPermintaan { get; set; }
        public int? IdTahunAjaran { get; set; }
        public string KeteranganKeperluan { get; set; }
        public string StatusPersetujuan { get; set; }
        public DateTime? TanggalPersetujuan { get; set; }
        public int? IdPenyetuju { get; set; }
        public string AlasanDisetujui { get; set; }

        public virtual Jurusan IdJurusanNavigation { get; set; }
        public virtual Pengguna IdPenggunaNavigation { get; set; }
        public virtual Pengguna IdPenyetujuNavigation { get; set; }
        public virtual TahunAjaran IdTahunAjaranNavigation { get; set; }
        public virtual ICollection<DetailPermintaan> DetailPermintaan { get; set; }
        public virtual ICollection<PengadaanPermintaan> PengadaanPermintaan { get; set; }
    }
}
