using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class PrinterSetting
    {
        public int Id { get; set; }

        public string? PrinterName { get; set; }

        public string? PrintQuality { get; set; }

        public string? ColorMode { get; set; }

        public string? PaperSize { get; set; }

        public string? PaperReprint { get; set; }
    }
}
