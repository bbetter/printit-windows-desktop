using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    class LoadAndLoginViewModel:ViewModelBase
    {
        public LoadAndLoginViewModel()
        {
            SignUpShowCommand = new DelegateCommand(ShowSignUp);
            SignUpCancelCommand = new DelegateCommand(HideSignUp);
            LoginVisibility = Visibility.Collapsed;
            SignUpVisibility = Visibility.Collapsed;
        }

        public ICommand LoginCommand { get; set; }
        public ICommand SignUpShowCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SignUpCancelCommand { get; set; }

        public Visibility LoginVisibility { get; set; }
        public Visibility SignUpVisibility { get; set; }

        public double ToLeft { get; set; }
        public double ToTop { get; set; }
        public double LoginHeight { get; set; }
        public double LoginWidth { get; set; }

        private void ShowSignUp()
        {
            LoginVisibility = Visibility.Collapsed;
            SignUpVisibility = Visibility.Visible;
        }

        private void HideSignUp()
        {
            SignUpVisibility = Visibility.Collapsed;
            LoginVisibility = Visibility.Visible;
        }
    }
}
