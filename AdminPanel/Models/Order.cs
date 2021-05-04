using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Models
{
    public class Order
    {
        public ulong Id { get; set; }

        public DateTime PostDate { get; set; }

        public OrderMeta OrderMeta { get; set; }

        public string PostStatus { get; set; }
    }
}
