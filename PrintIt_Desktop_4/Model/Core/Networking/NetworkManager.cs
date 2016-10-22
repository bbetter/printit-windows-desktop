using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using PrintIt_Desktop_4.Model.Configuration;

namespace PrintIt_Desktop_4.Model.Core.Networking
{
    public static class NetworkManager
    {
        private static WebClient _client;

        static NetworkManager()
        {
            _client = new WebClient();
            //_client.Headers.Add(@"Accept: application/json");     
        }

        public static bool CanStartSession(string serverAddressOrName)
        {
            bool pingable;
            var pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(serverAddressOrName);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {
                pingable = false;
            }
            return pingable;
        }

        public static string SendPostRequest(NameValueCollection data, string page)
        {
            var response = _client.UploadValues(Config.GetServerAddress()+page,"POST", data);
            var responseString = Encoding.Default.GetString(response);
            return responseString;
        }
    }
}
