using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using PrintIt_Desktop_4.Model.Core;
using PrintIt_Desktop_4.Model.Core.Printing;
using PrintIt_Desktop_4.Other;
using PrintIt_Desktop_4.UserControls;
using PrintIt_Desktop_4.ViewModels.PrinterListInternal;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    public class PrinterListViewModel:ViewModelBase
    {
        public PrinterListViewModel()
        {
            PrinterChangedCommand = new DelegateCommand(ChangeDefaultPrinter);
            _Printers = new List<Printer>();
            Printers = new ObservableCollection<PrinterViewModel>();
            var count = PrinterManager.GetPrintersCount();
            for (int i = 0; i < count; i++)
            {
                _Printers.Add(PrinterManager.GetPrinter(i));
                Printers.Add(Map(PrinterManager.GetPrinter(i)));
            }
            SelectedPrinter = Printers.First(x => x.Name == PrinterManager.GetDefaultSystemPrinter());
            CurrentState.CurrentTicker.OnTick += Update;
        }

        public PrinterList CurrentControl { get; set; }

        public ObservableCollection<PrinterViewModel> Printers { get; set; } 
        public List<Printer> _Printers { get; set; }

        public ICommand PrinterChangedCommand { get; set; }
        public PrinterViewModel SelectedPrinter { get; set; }

        private void ChangeDefaultPrinter()
        {
            if(SelectedPrinter!=null)
            CurrentState.DefaultPrinterName = SelectedPrinter.Name;
            //MessageBox.Show(SelectedPrinter.Name);
        }
        
        private void Update()
        {
            CurrentControl.Dispatcher.BeginInvoke(new Action(() =>
            {

                _Printers = new List<Printer>();
                var count = PrinterManager.GetPrintersCount();
                for (int i = 0; i < count; i++)
                {
                    var printer = PrinterManager.GetPrinter(i);
                    _Printers.Add(printer);
                    if (Printers.Count(x => x.Name == printer.Name) > 0)
                    {
                        Map(Printers.First(x => x.Name == printer.Name), printer);
                    }
                    else
                    {
                        Printers.Add(Map(PrinterManager.GetPrinter(i)));
                    }
                }
                if (Printers.Count > _Printers.Count)
                {
                    foreach (var printer in Printers)
                    {
                        if (_Printers.Count(x => x.Name == printer.Name) == 0)
                        {
                            Printers.Remove(printer);
                        }
                    }
                }
            }));
        }


        private void Map(PrinterViewModel viewModel, Printer model)
        {
            viewModel.Name = model.Name;
            viewModel.Connected = model.WorkOffline;
            viewModel.Error = model.Queue.IsInError;
            viewModel.Queue = new QueueViewModel() {NumberOfJobs = model.Queue.GetPrintJobInfoCollection().Count(x => x.IsPrinted == false && x.IsCompleted == false && x.IsDeleted == false) };
        }

        private PrinterViewModel Map(Printer model)
        {
            var viewModel = new PrinterViewModel();
            viewModel.Name = model.Name;
            viewModel.Connected = model.WorkOffline;
            viewModel.Error = model.Queue.IsInError;
            viewModel.Queue = new QueueViewModel(){NumberOfJobs = model.Queue.GetPrintJobInfoCollection().Count(x=>x.IsPrinted==false && x.IsCompleted==false && x.IsDeleted == false)};
            return viewModel;
        }
    }
}
