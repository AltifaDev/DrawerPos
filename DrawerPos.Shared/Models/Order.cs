using System;
using System.Collections.Generic;

namespace DrawerPos.Shared;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int? CustomerId { get; set; }

    public int? StoreId { get; set; }
    public string? BillNo { get; set; }

    public decimal? TotalDiscount { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Store? Store { get; set; }
}
