using System;
using System.Collections.Generic;

namespace DrawerPos.Shared
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }

        public int? StoreId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime? LastUpdated { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Store? Store { get; set; }
    }
}