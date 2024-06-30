using System;
using System.Collections.Generic;

namespace DrawerPos.Shared;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal? Price { get; set; }
    public decimal? Discount { get; set; }
    public virtual Order? Order { get; set; } // Navigation property to Order
    public virtual Product? Product { get; set; } // Navigation property to Product

}
