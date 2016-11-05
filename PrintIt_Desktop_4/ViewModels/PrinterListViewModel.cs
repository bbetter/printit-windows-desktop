using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Model.Core.Printing;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    public class PrinterListViewModel:ViewModelBase
    {
        public PrinterListViewModel()
        {
            Printers = new ObservableCollection<Printer>();
            var count = PrinterManager.GetPrintersCount();
            for (int i = 0; i < count; i++)
            {
                Printers.Add(PrinterManager.GetPrinter(i));
            }
        }

        public ObservableCollection<Printer> Printers { get; set; }
    }
}
