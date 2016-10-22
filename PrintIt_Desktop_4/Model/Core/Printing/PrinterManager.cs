using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintIt_Desktop_4.Model.Core.Printing
{
    public static class PrinterManager
    {
        static PrinterManager()
        {
            //setup printer list
            _printerNames = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                _printerNames.Add(printer);
            }
        }

        private static List<String> _printerNames;
    }
}
