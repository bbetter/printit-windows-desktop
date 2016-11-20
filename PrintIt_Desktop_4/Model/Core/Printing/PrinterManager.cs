using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Printing;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows;

namespace PrintIt_Desktop_4.Model.Core.Printing
{
    public static class PrinterManager
    {
        static PrinterManager()
        {
            //setup printer lists
            int id = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                _printerNames.Add(id,printer);
                _printers.Add(ParsePrinter(printer));
                id++;
            }
            var settings = new PrinterSettings();
            _defaultPrinter=settings.PrinterName;

        }
        private static LocalPrintServer _printServer = new LocalPrintServer();
        private static Dictionary<int,String> _printerNames = new Dictionary<int,string>();
        private static List<Printer> _printers = new List<Printer>();
        private static String _defaultPrinter;
        public static int GetPrintersCount()
        {
            return _printers.Count;
        }

        public static Printer GetPrinter(int id)
        {
            return _printers[id];
        }

        public static String GetDefaultSystemPrinter()
        {
            return _defaultPrinter;
        }

        public static void UpdateInformation()
        {
            int id = 0;
            foreach (var printerName in _printerNames)
            {
                bool workOffline = false;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
                foreach (ManagementObject printer in searcher.Get())
                {
                    if((string)printer["Name"]==printerName.Value)
                    workOffline = (bool)printer["WorkOffline"];
                }
                _printers[id].Queue = ParsePrinter(printerName.Value).Queue;
                _printers[id].Settings = new PrinterSettings() {PrinterName = printerName.Value,};
                _printers[id].WorkOffline = workOffline;
                id++;
            }
        }

        private static Printer ParsePrinter(String name)
        {
            var printQueue = _printServer.GetPrintQueue(name);
            bool workOffline = false;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            foreach (ManagementObject printer in searcher.Get())
            {
                if ((string)printer["Name"] == name)
                workOffline = (bool)printer["WorkOffline"];
            }
            //MessageBox.Show(name + " " + printQueue.IsOffline);
            return new Printer(){Name = name,Queue = printQueue, Settings = new PrinterSettings(){PrinterName = name},WorkOffline = workOffline};
        }

        public static void PrintDocument(String documentName, String location, String printerName)
        {
            try
            {
                //_printers.First(x => x.Name == printerName).Queue.AddJob(documentName, location, false);

                //Process p = new Process();
                //p.StartInfo = new ProcessStartInfo()
                //{
                //    CreateNoWindow = true,
                //    Verb = "print",
                //    FileName = location
                //};
                //p.Start();

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    Verb = "print",
                    //FileName = @"D:\testdoc.doc"
                    FileName = location
                };
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

    }
}
