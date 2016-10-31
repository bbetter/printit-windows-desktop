using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
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

            
            var wsw = new WebSocketWrapper(Config.GetWebSocketAddress(), new Cookie("user_id", "1"));

            wsw.SetErrorHandler((o,e)=>MessageBox.Show(e.Message));
            wsw.SetMessageHandler((o,e)=>MessageBox.Show("Message: "+e.Data));

            Closed += (s, e) => wsw.SendMessage(Config.GetStopMessage());

            wsw.Start();
            wsw.SendMessage("{\"command\":\"subscribe\",\"identifier\":\"{\\\"channel\\\":\\\"DocsChannel\\\"}\"}");
        }

        public String PrintSpotName { get; set; }
        public ICommand InfoCommand { get; set; }
    }
}
