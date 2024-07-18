using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrawerPos.Shared
{

    public class BillNumber
    {
        [Key]
        public int BillNumberId { get; set; }
        public string BillNo { get; set; }
       
    }
}