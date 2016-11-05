using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Dynamic;
using System.Linq;
using System.Printing;
using System.Text;

namespace PrintIt_Desktop_4.Model.Core.Printing
{
    public class Printer
    {
        public string Name { get;set; }
        public PrintQueue Queue { get; set; }
        public PrinterSettings Settings { get; set; }
        public bool WorkOffline { get; set; }

    }
}
