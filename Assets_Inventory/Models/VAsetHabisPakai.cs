using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class VAsetHabisPakai
    {
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string NamaKategori { get; set; }
        public int StokAwal { get; set; }
        public int StokAktual { get; set; }
        public string NamaJurusan { get; set; }
        public string NamaRuang { get; set; }
        public string NamaLokasi { get; set; }
        public bool? IsReturnable { get; set; }
        public DateTime TanggalRegistrasi { get; set; }
        public string Status { get; set; }
        public string Keterangan { get; set; }
    }
}
