using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrawerPos.Shared
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public string BillNo { get; set; } = null!;
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public virtual Order BillNoNavigation { get; set; } = null!;
        public virtual Product Product { get; set; }
    }

}