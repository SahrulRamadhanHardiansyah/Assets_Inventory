using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assets_Inventory.Models
{
    public partial class Cache
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public long Expiration { get; set; }
    }
}
