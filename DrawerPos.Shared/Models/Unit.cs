using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }

        [Required]
        public string UnitName { get; set; }

        // Navigation property to link Unit to Ingredients
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
