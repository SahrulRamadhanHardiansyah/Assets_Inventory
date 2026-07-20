using System;

namespace Assets_Inventory.Models
{
    public partial class AsetLampiran
    {
        public int Id { get; set; }
        public string TipeAset { get; set; }
        public int RefId { get; set; }
        public string RefKode { get; set; }
        public string FilePath { get; set; }
        public string OriginalFileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public string Keterangan { get; set; }
    }
}
