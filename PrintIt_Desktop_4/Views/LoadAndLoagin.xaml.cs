using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.Views
{
    /// <summary>
    /// Interaction logic for LoadAndLoagin.xaml
    /// </summary>

    [Magic]
    public partial class LoadAndLoagin : MetroWindow
    {
        public double ToLeft { get; set; }
        public double ToTop { get; set; }
        public double LoginHeight { get; set; }
        public double LoginWidth { get; set; }

        public bool RingActive { get; set; }

        DispatcherTimer timer = new DispatcherTimer(){Interval = new TimeSpan(0,0,0,3), IsEnabled = false};
        private Storyboard transformStoryboard;
        private Storyboard showLoginStoryboard;
        private Storyboard hideSplashStoryboard;
        private Storyboard moveStoryboard;
        public LoadAndLoagin()
        {
            InitializeComponent();
            DataContext = this;
            LoginWidth = 800;
            LoginHeight = 470;
            ToTop = System.Windows.SystemParameters.WorkArea.Height-LoginHeight;
            ToLeft = System.Windows.SystemParameters.WorkArea.Width-LoginWidth;
            ToTop /= 2;
            ToLeft /= 2;
            timer.Tick += ((sender, args) =>
            {
                
                hideSplashStoryboard.Begin();
                this.SizeToContent = SizeToContent.WidthAndHeight;
                
                //moveStoryboard.Begin();
                timer.Stop();
            });
            moveStoryboard = ((Storyboard) FindResource("StoryboardMove"));
            moveStoryboard.Completed += (sender, args) => { transformStoryboard.Begin(); showLoginStoryboard.Begin(); AnimateTitle();};
            transformStoryboard = (Storyboard)FindResource("StoryboardResize");
            transformStoryboard.Completed += (sender, args) =>  SplashToLoginAnimationEnd();
            showLoginStoryboard = (Storyboard)FindResource("StoryboardShowLogin");
            hideSplashStoryboard = (Storyboard)FindResource("StoryboardHideSplash");
            hideSplashStoryboard.Completed += (sender, args) => { SplashScreen.Visibility = Visibility.Collapsed;
                                                                    SplashScreen.ProgressRingActive = false;
                                                                    //RingActive = false;
                                                                //SplashScreen.StopAnimation();
                                                                LoginScreen.Visibility = Visibility.Visible;
                                                                moveStoryboard.Begin();
            };
            

        }

        private void AnimateTitle()
        {
            Int32Animation a = new Int32Animation();
            a.From = 0;
            a.To = 30;
            a.Duration = new TimeSpan(0, 0, 0, 1);
            this.BeginAnimation(TitlebarHeightProperty,a);
        }


        private void LoadAndLoagin_OnLoaded(object sender, RoutedEventArgs e)
        {
            SplashScreen.ProgressRingActive = true;
            //RingActive = true;
            //SplashScreen.StartAnimation();
            timer.IsEnabled = true;
            timer.Start();
        }

        private void SplashToLoginAnimationEnd()
        {
            //TitlebarHeight = 30;
            Height = LoginHeight+1;
            Width = LoginWidth+1;
            Top++;
            Left++;
            UpdateLayout();
            ShowMinButton = true;
            ShowCloseButton = true;
            ResizeMode = ResizeMode.CanMinimize;
            WindowCloseButtonStyle = Application.Current.FindResource("DarkCloseWindowButtonStyle") as Style;
            WindowMinButtonStyle = Application.Current.FindResource("DarkMinMaxWindowButtonStyle") as Style;

            //SendTestRequest();
            //SendTestRequest2();
        }

        private void SendTestRequest()
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["email"] = "test@test.com";
                values["password"] = "12345678";

                var response = client.UploadValues("http://ec2-52-57-154-202.eu-central-1.compute.amazonaws.com/api/v1/sign_in", values);

                var responseString = Encoding.Default.GetString(response);
                MessageBox.Show(responseString);
            }
        }

        private void SendTestRequest2()
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["email"] = "fail@test.com";
                values["password"] = "123456";

                var response = client.UploadValues("http://ec2-52-57-154-202.eu-central-1.compute.amazonaws.com/api/v1/sign_in", values);

                var responseString = Encoding.Default.GetString(response);
                MessageBox.Show(responseString);
            }
        }
    }
}
