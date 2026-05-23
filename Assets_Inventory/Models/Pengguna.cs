using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Pengguna
    {
        public Pengguna()
        {
            BarangKeluarNamaPenerimaNavigation = new HashSet<BarangKeluar>();
            BarangKeluarPetugasNavigation = new HashSet<BarangKeluar>();
            Kerusakan = new HashSet<Kerusakan>();
            OpnameStok = new HashSet<OpnameStok>();
            PenghapusanAset = new HashSet<PenghapusanAset>();
            PermintaanIdPenggunaNavigation = new HashSet<Permintaan>();
            PermintaanIdPenyetujuNavigation = new HashSet<Permintaan>();
        }

        public int IdPengguna { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdPeran { get; set; }
        public int? IdKelas { get; set; }
        public int? IdMapel { get; set; }
        public int? IdJurusan { get; set; }

        public virtual Jurusan IdJurusanNavigation { get; set; }
        public virtual Kelas IdKelasNavigation { get; set; }
        public virtual Mapel IdMapelNavigation { get; set; }
        public virtual Peran IdPeranNavigation { get; set; }
        public virtual ICollection<BarangKeluar> BarangKeluarNamaPenerimaNavigation { get; set; }
        public virtual ICollection<BarangKeluar> BarangKeluarPetugasNavigation { get; set; }
        public virtual ICollection<Kerusakan> Kerusakan { get; set; }
        public virtual ICollection<OpnameStok> OpnameStok { get; set; }
        public virtual ICollection<PenghapusanAset> PenghapusanAset { get; set; }
        public virtual ICollection<Permintaan> PermintaanIdPenggunaNavigation { get; set; }
        public virtual ICollection<Permintaan> PermintaanIdPenyetujuNavigation { get; set; }
    }
}
