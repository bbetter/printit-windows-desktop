using System;
using System.Collections.Generic;
using System.Data;
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
        //private static string msg;
        //static void PrintProps(ManagementObject o, string prop)
        //{
        //    try { msg+=(prop + "|" + o[prop]+"\n"); }
        //    catch (Exception e) { Console.Write(e.ToString()); }
        //}
        static PrinterManager()
        {
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            //foreach (ManagementObject printer in searcher.Get())
            //{
            //    string printerName = printer["Name"].ToString().ToLower();
            //    msg+=("Printer :" + printerName+"\n");

            //    PrintProps(printer, "Caption");
            //    PrintProps(printer, "ExtendedPrinterStatus");
            //    PrintProps(printer, "Availability");
            //    PrintProps(printer, "Default");
            //    PrintProps(printer, "DetectedErrorState");
            //    PrintProps(printer, "ExtendedDetectedErrorState");
            //    PrintProps(printer, "ExtendedPrinterStatus");
            //    PrintProps(printer, "LastErrorCode");
            //    PrintProps(printer, "PrinterState");
            //    PrintProps(printer, "PrinterStatus");
            //    PrintProps(printer, "Status");
            //    PrintProps(printer, "WorkOffline");
            //    PrintProps(printer, "Local");
            //}
            //MessageBox.Show(msg);
            //setup printer lists
            int id = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                _printerNames.Add(id,printer);
                _printers.Add(ParsePrinter(printer));
                id++;
            }

        }
        private static LocalPrintServer _printServer = new LocalPrintServer();
        private static Dictionary<int,String> _printerNames = new Dictionary<int,string>();
        private static List<Printer> _printers = new List<Printer>(); 
        public static int GetPrintersCount()
        {
            return _printers.Count;
        }

        public static Printer GetPrinter(int id)
        {
            return _printers[id];
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


    }
}
