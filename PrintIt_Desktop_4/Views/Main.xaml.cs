using System;
using MahApps.Metro.Controls;

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
        }

        public String PrintSpotName { get; set; }
    }
}
