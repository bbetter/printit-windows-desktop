using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintIt_Desktop_4.UserControls
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : UserControl
    {
        public SplashScreen()
        {
            InitializeComponent();
            DataContext = this;
        }


        public bool ProgressRingActive
        {
            get { return (bool)GetValue(ProgressRingActiveProperty); }
            set { SetValue(ProgressRingActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Active.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressRingActiveProperty =
            DependencyProperty.Register("ProgressRingActive", typeof(bool), typeof(SplashScreen), new PropertyMetadata(false));
    }
}
