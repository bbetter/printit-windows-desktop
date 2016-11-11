using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using PrintIt_Desktop_4.Model.Core.Networking;
using PrintIt_Desktop_4.Model.LocalStorage;

namespace PrintIt_Desktop_4.Model.Core
{
    public static class CurrentState
    {
        static CurrentState()
        {
            WebSocketWrappers = new List<WebSocketWrapper>();
        }
        public static UserConfiguration Configuration { get; set; }
        public static List<WebSocketWrapper> WebSocketWrappers { get; set; }
        public static ResourceDictionary LanguageResourceDictionary { get; set; }
    }
}
