using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Jobs
    {
        public ulong Id { get; set; }
        public string Queue { get; set; }
        public string Payload { get; set; }
        public byte Attempts { get; set; }
        public uint? ReservedAt { get; set; }
        public uint AvailableAt { get; set; }
        public uint CreatedAt { get; set; }
    }
}
