using System;

namespace Assets_Inventory.Models
{
    public partial class Notifikasi
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string RefTable { get; set; }
        public string RefId { get; set; }
        public bool IsRead { get; set; }
        public int? IdPenerima { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public string Title { get; set; }

        public virtual Pengguna IdPenerimaNavigation { get; set; }
    }
}
