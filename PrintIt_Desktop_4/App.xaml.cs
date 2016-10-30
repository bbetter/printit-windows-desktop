using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using PrintIt_Desktop_4.Model.Core;

namespace PrintIt_Desktop_4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            WindowManager.ShowLoginWindow();
        }
    }
}
