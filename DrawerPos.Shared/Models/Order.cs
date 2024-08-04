using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DrawerPos.Shared
{
    public partial class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public string BillNo { get; set; } = null!;
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
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}