using System;

namespace Assets_Inventory.Models
{
    public partial class ApprovalWorkflowConfig
    {
        public int Id { get; set; }
        public string WorkflowType { get; set; }
        public int Level { get; set; }
        public int IdPeranApprover { get; set; }
        public bool IsRequired { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Peran IdPeranApproverNavigation { get; set; }
    }
}
