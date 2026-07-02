using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class AsetHabisPakai
    {
        public AsetHabisPakai()
        {
            BarangKeluar = new HashSet<BarangKeluar>();
        }

        public string KodeBarang { get; set; }
        public int? IdPengadaanHp { get; set; }
        public int IdMasterBarang { get; set; }
        public int Stok { get; set; }
        public int StokAktual { get; set; }
        public int? IdJurusan { get; set; }
        public int? IdRuang { get; set; }
        public int? IdLokasi { get; set; }
        public string Status { get; set; }
        public DateTime TanggalRegistrasi { get; set; }
        public string Keterangan { get; set; }
        public bool? IsReturnable { get; set; }

        public virtual Jurusan IdJurusanNavigation { get; set; }
        public virtual Lokasi IdLokasiNavigation { get; set; }
        public virtual MasterBarang IdMasterBarangNavigation { get; set; }
        public virtual PengadaanHabisPakai IdPengadaanHpNavigation { get; set; }
        public virtual Ruang IdRuangNavigation { get; set; }
        public virtual ICollection<BarangKeluar> BarangKeluar { get; set; }
    }
}
