using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared;
 
    public class CartItem
    {
    public int CartItemId { get; set; } // Primary Key
    public Product Product { get; set; }
    public int Quantity { get; set; }
  public decimal Discount { get; set; }
     public decimal TotalPrice { get; set; }

    }
 
