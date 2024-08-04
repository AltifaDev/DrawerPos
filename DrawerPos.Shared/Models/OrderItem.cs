using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrawerPos.Shared
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public string BillNo { get; set; } // Foreign Key
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }

        public virtual Order BillNoNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}