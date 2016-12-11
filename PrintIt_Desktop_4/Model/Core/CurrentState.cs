using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using PrintIt_Desktop_4.Model.Core.Networking;
using PrintIt_Desktop_4.Model.Core.Printing;
using PrintIt_Desktop_4.Model.LocalStorage;

namespace PrintIt_Desktop_4.Model.Core
{
    public static class CurrentState
    {
        static CurrentState()
        {
            WebSocketWrappers = new List<WebSocketWrapper>();
            CurrentPrintQueue = new List<string>();
        }
        public static UserConfiguration Configuration { get; set; }
        public static List<WebSocketWrapper> WebSocketWrappers { get; private set; }
        public static ResourceDictionary LanguageResourceDictionary { get; set; }
        public static String DefaultPrinterName { get; set; }
        public static List<String> CurrentPrintQueue { get; private set; }
        public static QueueChecker CurrentQueueChecker { get; set; }
        public static Ticker CurrentTicker { get; set; }
        public static PrintSpotInfo PrintSpotParameters { get; set; }
    }
}
