using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Core;
using PrintIt_Desktop_4.Model.Core.Networking;
using PrintIt_Desktop_4.Model.Core.Printing;
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
            CurrentState.CurrentQueueChecker = new QueueChecker() { PrinterName = null };
            CurrentState.CurrentTicker = new Ticker();
            InfoFlowDocument2 = File.ReadAllText(@"..\Docs\info.rtf");

            //InitializeComponent();
            
            DataContext = this;
            //InfoFlowDocument = new FlowDocument();
            //TextRange range = new TextRange(InfoFlowDocument.ContentStart,InfoFlowDocument.ContentEnd);
            //using (var doc =File.Open(@"..\Docs\info.rtf",FileMode.Open,FileAccess.Read))
            //{
            //    range.Load(doc,DataFormats.Rtf);
            //}

            


            PrintSpotName = "PrintZ - PRINTSPOT NAME GOES HERE";
            InfoCommand = new DelegateCommand(() => { FlyoutInfo.IsOpen = true;});
            StateCommand = new DelegateCommand(() => { FlyoutState.IsOpen = true; });
            CancelClose = false;
            var wsw = new WebSocketWrapper(Config.Networking.GetWebSocketAddress(), new Cookie("token", NetworkManager.GetAccessToken()));
            wsw.SetErrorHandler(MessageHandler.HandleWebsocketError);
            wsw.SetMessageHandler(MessageHandler.HandleWebSocketMessage);

            CurrentState.WebSocketWrappers.Add(wsw);
            WhoAmI();
            //InitializeComponent();

            wsw.Start();
            wsw.SendMessage("{\"command\":\"subscribe\",\"identifier\":\"{\\\"channel\\\":\\\"DocsChannel\\\"}\"}");

            
            
            PrinterError = true;
        }

        public String PrintSpotName { get; set; }
        public ICommand InfoCommand { get; set; }
        public ICommand StateCommand { get; set; }
        public ICommand ClosingCommand { get; set; }
        public bool PrinterError { get; set; }
        public bool CancelClose { get; set; }

        public FlowDocument InfoFlowDocument { get; set; }
        public String InfoFlowDocument2 { get; set; }
        private void WhoAmI()
        {
            var values = new NameValueCollection();
            values.Add("token",NetworkManager.GetAccessToken());
            try
            {
                var res = NetworkManager.SendGetRequest(values, @"/api/v1/users/me");
                //MessageBox.Show(res);
                var json = JObject.Parse(res);
                var id = (string)json["print_spots"][0]["id"];
                PrintSpotName = "PrintZ " + (string)json["print_spots"][0]["name"];
                //MessageBox.Show(id);

                //todo refactor
                
                var info = new PrintSpotInfo();
                info.AdditionalInfo = (string)json["print_spots"][0]["additional_info"];
                info.Description = (string)json["print_spots"][0]["description"];
                info.PrintSpotAddress = (string)json["print_spots"][0]["address"];
                info.PrintSpotName = (string)json["print_spots"][0]["name"];
                info.Status = (string)json["print_spots"][0]["status"];
                info.ImageURI = (string)json["print_spots"][0]["photo_url"];
                info.OwnerName = (string) json["first_name"];
                info.OwnerSoname = (string)json["last_name"];
                info.Id = id;
                CurrentState.PrintSpotParameters = info;
                InitializeComponent();


                values = new NameValueCollection();
                values.Add("print_spot_id",id);
                var res2 = NetworkManager.SendGetRequest(values, @"/api/v1/docs");
                MessageHandler.HandleDocArray(res2);
                //MessageBox.Show(res2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.InnerException.Message);
            }
        }

        private void testlocale(object sender, RoutedEventArgs e)
        {
            if (CurrentState.Configuration.Locale == "uk-UA")
            {
                Localizer.ChangeLocalization("en-US");
            }
            else
            {
                Localizer.ChangeLocalization("uk-UA");
            }
        }

        private void Main_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel=true;
            Hide();
            (Application.Current as App).App_Exit(this, null);
        }
    }
}
