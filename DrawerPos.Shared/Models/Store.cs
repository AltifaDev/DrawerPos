using System;
using System.Collections.Generic;

namespace DrawerPos.Shared
{
    public partial class Store
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; } = null!;

        public string? Location { get; set; }

        public string? Phone { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}