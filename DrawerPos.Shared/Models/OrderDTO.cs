using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public string? BillNo { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? Total { get; set; }
        public decimal? Discount { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
        public int? CustomerId { get; set; }
        public int? StoreId { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal? SubTotal { get; set; }
        public List<PaymentDTO>? Payments { get; set; }
    }
}
