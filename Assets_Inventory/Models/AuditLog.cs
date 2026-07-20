using System;

namespace Assets_Inventory.Models
{
    public partial class AuditLog
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string RecordPK { get; set; }
        public string Action { get; set; }
        public string OldJson { get; set; }
        public string NewJson { get; set; }
        public int? IdPengguna { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
        public string Modul { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
    }
}
