using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class MethodPayment
    {
        [Key]
        public int QrId { get; set; }

        public string? MethodName { get; set; }
        public string? MethodType { get; set; }
        public string? MethodNumber { get; set; }
        public string? MethodStatus { get; set; }
    }
}
