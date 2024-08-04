using DrawerPos.Shared;
using System.ComponentModel.DataAnnotations;

public partial class Order
{
    [Key]
    public string BillNo { get; set; } // Primary Key
    public DateTime OrderDate { get; set; }
    public int? CustomerId { get; set; }
    public int? StoreId { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? TotalDiscount { get; set; }
    public decimal? Discount { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? Total { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}