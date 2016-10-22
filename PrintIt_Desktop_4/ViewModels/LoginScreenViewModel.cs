using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Other;
using PrintIt_Desktop_4.UserControls;

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
        public LoginScreenViewModel(LoginScreen view):this()
        {
            _view = view;
        }

        private LoginScreen _view;

        private String _login;
        public String Login {
            get { return _login; }
            set {
                    _login = value;
                    _view.Login = value;
                }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand ShowSignUpCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand StopShowPasswordCommand { get; set; }

        public String PasswordText { get; set; }

        public Visibility PasswordVisibility { get; set; }
        public Visibility ShowPasswordVisibility { get; set; }
        public int PasswordLength { get { return Constants.MaxPasswordLength; } }
    }
}
