using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class JobBatches
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalJobs { get; set; }
        public int PendingJobs { get; set; }
        public int FailedJobs { get; set; }
        public string FailedJobIds { get; set; }
        public string Options { get; set; }
        public int? CancelledAt { get; set; }
        public int CreatedAt { get; set; }
        public int? FinishedAt { get; set; }
    }
}
