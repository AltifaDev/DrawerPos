using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared.Models
{
    public class IngredientStockDto
    {
        public int IngredientStockId { get; set; }
        public int IngredientId { get; set; } // Use this for the lookup
        public int QuantityInStock { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Notes { get; set; } // Make Notes nullable
        public int ReorderPoint { get; set; }
    }
}
