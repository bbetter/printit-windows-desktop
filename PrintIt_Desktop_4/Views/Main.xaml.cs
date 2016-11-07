using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Core.Networking;
using WebSocketSharp.Net;

namespace PrintIt_Desktop_4.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public Main()
        {
            InitializeComponent();
            DataContext = this;
            PrintSpotName = "PrintZ - PRINTSPOT NAME GOES HERE";
            InfoCommand = new DelegateCommand(() => { FlyoutInfo.IsOpen = true;});
            StateCommand = new DelegateCommand(() => { FlyoutState.IsOpen = true; });
            
            //var wsw = new WebSocketWrapper(Config.GetWebSocketAddress(), new Cookie("user_id", "1"));
            var wsw = new WebSocketWrapper(Config.GetWebSocketAddress(), new Cookie("token", NetworkManager.GetAccessToken()));

            wsw.SetErrorHandler((o,e)=>MessageBox.Show(e.Message));
            wsw.SetMessageHandler((o,e)=>
            {
                var resJson = JObject.Parse(e.Data);

                if (resJson.HasValues)
                {
                    try
                    {
                        if ((string) resJson["type"] == "ping") return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Json error");
                        return;
                    }
                }
                MessageBox.Show("Message: " + e.Data); });

            Closed += (s, e) => wsw.SendMessage(Config.GetStopMessage());

            wsw.Start();
            wsw.SendMessage("{\"command\":\"subscribe\",\"identifier\":\"{\\\"channel\\\":\\\"DocsChannel\\\"}\"}");
            //Thread.Sleep(1000);
            //wsw.SendMessage(@"{""command"":""message"", ""identifier"": ""{\""channel\"":\""DocsChannel\""}"", ""data"":""{\""a\"":\""b\""}""}");
        }

        public String PrintSpotName { get; set; }
        public ICommand InfoCommand { get; set; }
        public ICommand StateCommand { get; set; }
    }
}
