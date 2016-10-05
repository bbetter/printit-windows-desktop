using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PrintIt_Desktop_4.UserControls
{
    /// <summary>
    /// Interaction logic for LoadingRing.xaml
    /// </summary>
    public partial class LoadingRing : UserControl
    {
        public LoadingRing()
        {
            InitializeComponent();
        }

        public void StartAnimation()
        {
            Ring.RenderTransform = new RotateTransform(0, Ring.ActualHeight / 2, Ring.ActualHeight / 2);
            var timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, 50)};
            timer.Tick += ((sender, args) =>
            {
                if (Ring.RenderTransform is RotateTransform) (Ring.RenderTransform as RotateTransform).Angle += 10;
                else timer.Stop();

            });
            timer.Start();
        }

        public void StopAnimation()
        {
            Ring.RenderTransform = null;
        }
    }
}
