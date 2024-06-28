using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared;

public class BillNumber
{
    public int Id { get; set; }
    public string? BillNo { get; set; }
    public virtual Order? Order { get; set; }
}