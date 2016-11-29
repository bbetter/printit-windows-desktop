using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels.PrinterListInternal
{
    [Magic]
    public class QueueViewModel:ViewModelBase
    {
        public int NumberOfJobs { get; set; }
    }
}
