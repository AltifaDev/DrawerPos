using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{ 
        public class IngredientCreateDto
        {
            [Required]
            public int IngredientId { get; set; }

            [Required]
            [StringLength(100)]
            public string IngredientName { get; set; }

            [Required]
            public int UnitId { get; set; }
        }

     
}
