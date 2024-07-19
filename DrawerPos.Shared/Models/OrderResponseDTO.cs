﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public string? Status { get; set; }
        public List<string> ErrorMessages { get; set; }  // Optional
    }
}