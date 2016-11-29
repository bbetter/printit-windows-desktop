using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels.PrinterListInternal
{
    [Magic] 
    public class PrinterViewModel:ViewModelBase
    {
        public String Name { get; set; }
        public bool Connected { get; set; }
        public bool Error { get; set; }
        public String ErrorMessage { get; set; }
        public QueueViewModel Queue { get; set; }
    }
}
