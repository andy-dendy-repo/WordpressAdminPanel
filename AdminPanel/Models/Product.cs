using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Models
{
    public class Product
    {
        public ulong Id { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime PostDateGmt { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public string PostExcerpt { get; set; }
        public string PostStatus { get; set; }
        public string PostName { get; set; }
        public DateTime PostModified { get; set; }

        public ProductMeta ProductMeta { get; set; }
    }
}
