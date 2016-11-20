using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PrintIt_Desktop_4.Model.Core;
using PrintIt_Desktop_4.Model.Core.Printing;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    public class PrinterListViewModel:ViewModelBase
    {
        public PrinterListViewModel()
        {
            PrinterChangedCommand = new DelegateCommand(ChangeDefaultPrinter);
            Printers = new ObservableCollection<Printer>();
            var count = PrinterManager.GetPrintersCount();
            for (int i = 0; i < count; i++)
            {
                Printers.Add(PrinterManager.GetPrinter(i));
            }
            SelectedPrinter = Printers.First(x => x.Name == PrinterManager.GetDefaultSystemPrinter());
        }

        public ObservableCollection<Printer> Printers { get; set; }

        public ICommand PrinterChangedCommand { get; set; }
        public Printer SelectedPrinter { get; set; }

        private void ChangeDefaultPrinter()
        {
            CurrentState.DefaultPrinterName = SelectedPrinter.Name;
            //MessageBox.Show(SelectedPrinter.Name);
        }
    }
}
