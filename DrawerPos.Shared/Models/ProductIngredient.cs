using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
   
    public class ProductIngredient
    {
        [Key]
        public int ProductIngredientId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient
        { get; set; }
    }

}
