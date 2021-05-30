using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Models
{
    public class OrderedProduct
    {
        public OrderedProduct(ulong id, string title, int count)
        {
            ID = id;
            Title = title;
            Count = count;
        }

        public ulong ID { get; set; }
        public string Title { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}; Title: {Title}; Amount ordered: {Count}";
        }
    }
}
