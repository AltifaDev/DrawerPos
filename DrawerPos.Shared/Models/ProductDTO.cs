using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? Unit { get; set; }
        public string? Status { get; set; }
    }
}
