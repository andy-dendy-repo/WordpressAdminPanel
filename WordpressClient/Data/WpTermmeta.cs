using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WordpressClient.Data
{
    public partial class WpTermmeta
    {
        public ulong MetaId { get; set; }
        public ulong TermId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}
