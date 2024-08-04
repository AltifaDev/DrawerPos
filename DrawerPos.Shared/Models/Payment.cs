using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrawerPos.Shared
{
    public partial class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public string BillNo { get; set; } = null!; // Foreign Key

        public DateTime? PaymentDate { get; set; }

        public decimal? Amount { get; set; }

        public string? PaymentMethod { get; set; }

        // Navigation property to Order
        public virtual Order BillNoNavigation { get; set; } = null!;
    }
}
