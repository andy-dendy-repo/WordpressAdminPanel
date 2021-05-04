using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Models
{
    public class Statistics
    {
        public int AllOrdersCount { get; set; }

        public int DeletedOrdersCount { get; set; }

        public int AllProductsCount { get; set; }

        public int DeletedProductsCount { get; set; }

        public DateTime LastOrderMadeDate { get; set; }

        public DateTime LastProductMadeDate { get; set; }
    }
}
