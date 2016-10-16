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
using PrintIt_Desktop_4.ViewModels;

namespace PrintIt_Desktop_4.UserControls
{
    /// <summary>
    /// Interaction logic for SignUpScreen.xaml
    /// </summary>
    public partial class SignUpScreen : UserControl
    {
        public SignUpScreen()
        {
            InitializeComponent();
        }



        public ICommand SignUpCommand
        {
            get { return (ICommand)GetValue(SignUpCommandProperty); }
            set { SetValue(SignUpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SignUpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SignUpCommandProperty =
            DependencyProperty.Register("SignUpCommand", typeof(ICommand), typeof(SignUpScreen), new PropertyMetadata(null));



        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CancelCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(SignUpScreen), new PropertyMetadata(null,CancelCommandChanged));

        private static void CancelCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var vm = (SignUpScreenViewModel) (o as SignUpScreen).DataContext;
            vm.CancelCommand = (ICommand) args.NewValue;
        }


        public String LoginValue
        {
            get { return (String)GetValue(LoginValueProperty); }
            set { SetValue(LoginValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoginValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginValueProperty =
            DependencyProperty.Register("LoginValue", typeof(String), typeof(SignUpScreen), new PropertyMetadata(null));



        public String NameValue
        {
            get { return (String)GetValue(NameValueProperty); }
            set { SetValue(NameValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NameValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameValueProperty =
            DependencyProperty.Register("NameValue", typeof(String), typeof(SignUpScreen), new PropertyMetadata(null));

        
        
    }
}
