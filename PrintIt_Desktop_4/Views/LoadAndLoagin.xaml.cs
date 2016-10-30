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
using Microsoft.Expression.Prototyping.Data;
using PrintIt_Desktop_4.Model.Core;
using PrintIt_Desktop_4.Other;
using PrintIt_Desktop_4.ViewModels;

namespace PrintIt_Desktop_4.Views
{
    /// <summary>
    /// Interaction logic for LoadAndLoagin.xaml
    /// </summary>

    [Magic]
    public partial class LoadAndLoagin : MetroWindow
    {
        public LoadAndLoagin()
        {
            InitializeComponent();
        }

      /*
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
        */
    }
}
