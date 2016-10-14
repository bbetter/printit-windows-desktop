using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    class LoginScreenViewModel:ViewModelBase
    {
        public LoginScreenViewModel()
        {
            PasswordVisibility = Visibility.Visible;
            ShowPasswordVisibility = Visibility.Collapsed;
            ShowPasswordCommand = new DelegateCommand<PasswordBox>((o) =>
            {
                PasswordText = (o as PasswordBox).Password;
                PasswordVisibility = Visibility.Collapsed;
                ShowPasswordVisibility = Visibility.Visible;
            });
            StopShowPasswordCommand = new DelegateCommand(() =>
            {
                PasswordText = "";
                PasswordVisibility = Visibility.Visible;
                ShowPasswordVisibility = Visibility.Collapsed;
            });
        }

        public String Login { get; set; }
        
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand StopShowPasswordCommand { get; set; }

        public String PasswordText { get; set; }

        public Visibility PasswordVisibility { get; set; }
        public Visibility ShowPasswordVisibility { get; set; }
    }
}
