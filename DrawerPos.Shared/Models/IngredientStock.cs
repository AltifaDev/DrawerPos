using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class IngredientStock
    {
        [Key]
        public int IngredientStockId { get; set; }
        public int IngredientId { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Notes { get; set; }
        public int ReorderPoint { get; set; }

     
        public virtual Ingredient Ingredient { get; set; } // Make it virtual for lazy loading

    }

}
