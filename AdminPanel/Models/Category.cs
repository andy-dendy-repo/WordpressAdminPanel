using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Models
{
    public class Category
    {
        public ulong TermId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool IsSelected { get; set; }
    }
}
