using System;
using System.Windows;
using System.Windows.Forms;
using PrintIt_Desktop_4.Model.Core;
using Application = System.Windows.Application;

namespace PrintIt_Desktop_4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool tray = true;
        private NotifyIcon ni;
        void App_Startup(object sender, StartupEventArgs e)
        {
            PreLoader.PerformPreLoad();
            WindowManager.ShowLoginWindow();        
        }

        public void App_Exit(object sender, ExitEventArgs e)
        {
            if (tray)
            {
                ni = new NotifyIcon();
                ni.Visible = true;
                ni.Icon = ni.Icon = System.Drawing.Icon.ExtractAssociatedIcon(
                    System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
                ni.Click +=
                    delegate(object s, EventArgs args)
                    {
                        WindowManager.ShowMainWindowTray();
                    };
                tray = false;
            }
            else
            {
                Finisher.FinishApplication();
                ni.Dispose();
                App.Current.Shutdown();
            }
        }
    }
}
