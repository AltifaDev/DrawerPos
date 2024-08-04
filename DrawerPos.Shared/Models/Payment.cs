using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrawerPos.Shared
{
    public partial class Payment
    {
        public int PaymentId { get; set; }

        public string BillNo { get; set; } = null!;

        public DateTime? PaymentDate { get; set; }

        public decimal? Amount { get; set; }

        public string? PaymentMethod { get; set; }

        public virtual Order BillNoNavigation { get; set; } = null!;

       
    }
}