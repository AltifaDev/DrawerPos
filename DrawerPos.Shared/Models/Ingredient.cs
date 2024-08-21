using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "IngredientName is required")]
        public string IngredientName { get; set; }

        [Required]
        public int UnitId { get; set; }

        public virtual Unit Unit { get; set; }
    }

}
