using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Core.Networking;
using PrintIt_Desktop_4.Model.LocalStorage;

namespace PrintIt_Desktop_4.Model.Core
{
    public static class Finisher
    {
        public static void FinishApplication()
        {
            StopWrappers();
            StopTimers();
            SaveConfig();
        }

        private static void SaveConfig()
        {
            var serializer = new XmlSerializer(typeof(UserConfiguration));
            using (var sr = new StreamWriter(Config.Storage.GetConfigLocation()))
            {
                serializer.Serialize(sr, CurrentState.Configuration);
            }
        }

        private static void StopWrappers()
        {
            foreach (var webSocketWrapper in CurrentState.WebSocketWrappers)
            {
                webSocketWrapper.SendMessage(Config.Wrappers.GetStopMessage());
            }
        }

        private static void StopTimers()
        {
            if (CurrentState.CurrentTicker !=null)
           CurrentState.CurrentTicker.Stop();
        }
    }
}
