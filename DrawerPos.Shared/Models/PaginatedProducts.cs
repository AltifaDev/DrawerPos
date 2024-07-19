using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class PaginatedProducts
    {
        public IEnumerable<Product> Products { get; set; }
        public int TotalProducts { get; set; }
    }

}
