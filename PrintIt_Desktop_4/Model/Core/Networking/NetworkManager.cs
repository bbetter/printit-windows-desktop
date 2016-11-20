using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using PrintIt_Desktop_4.Model.Configuration;

namespace PrintIt_Desktop_4.Model.Core.Networking
{
    public static class NetworkManager
    {
        private static WebClient _client;
        private static string _token;
        static NetworkManager()
        {
            //_client = new WebClient();
            //_client.Headers.Add(@"Accept: */*");
            //WebClient c = new WebClient();
            //var socet = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.IP);
            //socet.Bind(new DnsEndPoint("localhost",3000));
            //socet.Listen(128);
            //socet.BeginAccept(null, 0, OnAccept, null);
            //    //c.OpenReadAsync();
        }

        public static void SetAccessToken(string token)
        {
            _token = token;
        }

        public static string GetAccessToken()
        {
            return _token;
        }

        private static void OnAccept(IAsyncResult res)
        {

        }

        public static bool CanStartSession(string serverAddressOrName)
        {
            var pingable = true;
            var pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(serverAddressOrName);
                if(reply!=null)
                pingable = reply.Status == IPStatus.Success;
                else pingable = false;
            }
            catch (Exception)
            {
                pingable = false;
            }
            return pingable;
        }

        public static string SendGetRequest(NameValueCollection data, string page)
        {
            using (_client = new WebClient())
            {
                _client.Headers.Add(@"Accept: */*");
                if(data.Count>0)
                    _client.QueryString.Add(data);
                    _client.Headers.Add("Authorization", "Token token=" + _token);
                var response = _client.DownloadData(Config.Networking.GetServerAddress() + page);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }

        public static string SendPutRequest(NameValueCollection data, string page)
        {
            using (_client = new WebClient())
            {
                _client.Headers.Add(@"Accept: */*");
                _client.Headers.Add("Authorization", "Token token=" + _token);
                var response = _client.UploadValues(Config.Networking.GetServerAddress() + page, "PUT", data);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }

        public static string SendPostRequest(NameValueCollection data, string page)
        {
            using (_client = new WebClient())
            {
                _client.Headers.Add(@"Accept: */*");
                var response = _client.UploadValues(Config.Networking.GetServerAddress() + page, "POST", data);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }

        public static string SendPostRequest(NameValueCollection data, string page, string login, string password)
        {
            using (_client = new WebClient())
            {
                _client.Headers.Add(@"Accept: */*");
                string credentials = Convert.ToBase64String(
                Encoding.ASCII.GetBytes(login + ":" + password));
                _client.Headers[HttpRequestHeader.Authorization] = string.Format(
                    "Basic {0}", credentials);
                var response = _client.UploadValues(Config.Networking.GetServerAddress() + page, "POST", data);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }

        public static void DownloadFile(string address, string fileName, DownloadProgressChangedEventHandler handler)
        {
            _client = new WebClient();
            _client.Headers.Add("Authorization", "Token token=" + _token);
            _client.DownloadProgressChanged += handler;
            _client.DownloadFileCompleted += (o, e) => _client = null;
            _client.DownloadFileAsync(new Uri(address), fileName);
            //_client.DownloadFile(new Uri(address),fileName);
           
        }

    }
}
