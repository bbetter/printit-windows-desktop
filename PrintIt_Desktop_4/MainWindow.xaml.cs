using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace PrintIt_Desktop_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MapBrowser.Navigate("http://maps.google.com.ua");
            //MapBrowser.Navigate("http://Google.com/maps");
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            dynamic doc = MapBrowser.Document;
            dynamic htmlText = doc.documentElement.InnerHtml;
            string htmlstring = htmlText;
            Regex pattern = new Regex(@"<meta content=""(.*?)"" itemprop=""description"" property=""og:description"">");
            Match match = pattern.Match(htmlstring);
            string location = match.Groups[1].Value;
            MessageBox.Show(location);
            MessageBox.Show(pattern.ToString());
        }
    }
}
