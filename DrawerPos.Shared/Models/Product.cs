using System;
using System.Collections.Generic;

namespace DrawerPos.Shared
{
    public partial class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string NameDisplay { get; set; } = null!;

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public decimal CostPrice { get; set; }

        public int Quantity { get; set; }

        public string? Unit { get; set; }

        public string? Image { get; set; }

        public string? ProductBarcode { get; set; }

        public string? ProductSize { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
      
    }
}