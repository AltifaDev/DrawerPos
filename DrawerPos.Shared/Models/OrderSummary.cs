using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class OrderSummary
    {
        public DateTime OrderDate { get; set; }
        public string? ProductName { get; set; }
        public string? NameDisplay { get; set; }
        public string? Image { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
