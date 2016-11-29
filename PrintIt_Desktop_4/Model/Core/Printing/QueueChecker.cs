using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using PrintIt_Desktop_4.Model.Enums;

namespace PrintIt_Desktop_4.Model.Core.Printing
{
    public class QueueChecker
    {
        public delegate void StateChagedEventHandler(String task, DocumentState state, int count, int done);

        public event StateChagedEventHandler OnQueueJobChanged;


        public String PrinterName { get; set; }

        public void Check()
        {
            var count = CurrentState.CurrentPrintQueue.Count;
            var docs = new List<String>();
            for (int i = 0; i < count; i++)
            {
                
                docs.Add(CurrentState.CurrentPrintQueue[i]);
            }
            for (int i = 0; i < count; i++)
            {
                Check(docs[i]);
            }
        }

        private void Check(String taskName)
        {
            var printers = new List<Printer>();
            var c = PrinterManager.GetPrintersCount();
            for (int i = 0; i < c; i++)
            {
                printers.Add(PrinterManager.GetPrinter(i));
            }
            foreach (var printer in printers)
            {
                if (printer.Name == PrinterName || String.IsNullOrEmpty(PrinterName))
                {
                    var printQueue = new PrintServer().GetPrintQueue(printer.Name);
                    var jobs = printQueue.GetPrintJobInfoCollection();
                    foreach (var job in jobs)
                    {
                        if (job.Name.Contains(taskName.Replace(".docx",String.Empty).Replace(".doc",String.Empty).Replace(".pdf",String.Empty))) //todo change expression
                        {
                            if (job.IsPrinting)
                            {
                                GenerateUpdate(taskName, DocumentState.Printing, job.NumberOfPages,
                                    job.NumberOfPagesPrinted);
                            }
                            else
                            {
                                GenerateUpdate(taskName, DocumentState.Queued);
                            }
                            return;
                        }
                    }
                }
            }
            if (CurrentState.CurrentPrintQueue.Contains(taskName))
            {
                GenerateUpdate(taskName, DocumentState.Done);
                //CurrentState.CurrentPrintQueue.Remove(taskName);
            }
            else
            {
                GenerateUpdate(taskName, DocumentState.Pending);
            }
            
        }

        private void GenerateUpdate(String task, DocumentState state, int pages=0, int pagesDone=0)
        {
            if(OnQueueJobChanged!=null)
            OnQueueJobChanged(task, state, pages, pagesDone);
        }
    }
}
