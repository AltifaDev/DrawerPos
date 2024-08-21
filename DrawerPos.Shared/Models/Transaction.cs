using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
