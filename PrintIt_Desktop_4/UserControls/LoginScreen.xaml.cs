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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        public String Login
        {
            get { return (String)GetValue(LoginPropertyProperty); }
            set { SetValue(LoginPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoginProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginPropertyProperty =
            DependencyProperty.Register("Login", typeof(String), typeof(LoginScreen), new PropertyMetadata(null,LoginChangedCallback));

        private static void LoginChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var vm = (LoginScreenViewModel)((o as LoginScreen).DataContext);
            vm.Login = (String)e.NewValue;
        }

        public ICommand ForgotPasswordCommand
        {
            get { return (ICommand)GetValue(ForgotPasswordCommandProperty); }
            set { SetValue(ForgotPasswordCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPasswordCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForgotPasswordCommandProperty =
            DependencyProperty.Register("ForgotPasswordCommand", typeof(ICommand), typeof(LoginScreen), new PropertyMetadata(null));

        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPasswordCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginCommandProperty =
            DependencyProperty.Register(" LoginCommand", typeof(ICommand), typeof(LoginScreen), new PropertyMetadata(null));


        public ICommand SignUpCommand
        {
            get { return (ICommand)GetValue(SignUpCommandProperty); }
            set { SetValue(SignUpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPasswordCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SignUpCommandProperty =
            DependencyProperty.Register(" SignUpCommand", typeof(ICommand), typeof(SignUpScreen), new PropertyMetadata(null));
    }
}
