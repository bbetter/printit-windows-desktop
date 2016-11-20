using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Views;

namespace PrintIt_Desktop_4.Model.Core
{
    public static class WindowManager
    {
        private static LoadAndLoagin LoginWindow { get; set; }
        private static Main MainWindow { get; set; }

        private static void SwitchWindows()
        {
            if(LoginWindow==null || MainWindow == null) return;
            LoginWindow.Close();
            MainWindow.Show();
        }

        private static void CreateMainWindow()
        {
            MainWindow = new Main();
        }

        private static void CreateLoginWindow()
        {
            LoginWindow = new LoadAndLoagin();
        }

        public static void ShowMainWindow()
        {
            CreateMainWindow();
            SwitchWindows();
        }

        public static void ShowLoginWindow()
        {
            CreateLoginWindow();
            LoginWindow.Show();
        }

        public static void ShowMainWindowTray()
        {
            MainWindow.Show();
        }

        public static void HideMainWindowTray()
        {
            MainWindow.Hide();
        }
    }
}
