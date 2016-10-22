using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Other;
using PrintIt_Desktop_4.UserControls;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    public class SignUpScreenViewModel:ViewModelBase
    {
        public SignUpScreenViewModel()
        {
        }

        public SignUpScreenViewModel(SignUpScreen view) : this()
        {
            _view = view;
        }

        private SignUpScreen _view;
        private String _name;
        private String _login;

        public String Login
        {
            get { return _login; }
            set
            {
                _login = value;
                _view.LoginValue = value;
            }
        }

        public String Name
        {
            get { return _name; }
            set
            { 
                _name = value;
                _view.NameValue = value;
            }
        }
        public ICommand CancelCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        public int PasswordLength { get { return Constants.MaxPasswordLength; } }
    }
}
