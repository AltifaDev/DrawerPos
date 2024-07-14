using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Add using directive if Payment is in a different namespace
using DrawerPos.Shared.Models; // Or the correct namespace for Payment

namespace DrawerPos.Shared
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public string BillNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentMethod { get; set; }
    }
}