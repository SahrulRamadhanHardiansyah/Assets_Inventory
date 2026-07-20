using System;

namespace Assets_Inventory.Models
{
    public partial class ApprovalStep
    {
        public int Id { get; set; }
        public string WorkflowType { get; set; }
        public string RefId { get; set; }
        public int Level { get; set; }
        public int? IdApprover { get; set; }
        public int IdPeranApprover { get; set; }
        public string Status { get; set; }
        public string Catatan { get; set; }
        public DateTime? TanggalKeputusan { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Pengguna IdApproverNavigation { get; set; }
        public virtual Peran IdPeranApproverNavigation { get; set; }
    }
}
