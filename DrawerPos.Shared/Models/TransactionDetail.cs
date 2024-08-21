using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class TransactionDetail
    {
        [Key]
        public int TransactionDetailId { get; set; }
        public int TransactionId { get; set; }
        public int IngredientId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PricePerUnit { get; set; }

        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }

        [ForeignKey("ProductId")]
        public virtual Ingredient Ingredient { get; set; }
    }
}
