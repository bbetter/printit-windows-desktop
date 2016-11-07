using System;
using System.Collections.Generic;
using System.Windows.Documents;
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
            AddressAutoComplete = new List<string>() { "place 1", "place 2", "place 3"};
        }

        public SignUpScreenViewModel(SignUpScreen view) : this()
        {
            _view = view;
        }

        private SignUpScreen _view;
        private String _name;
        private String _login;
        private String _address;

        public String Address
        {
            get { return _address; }
            set
            {
                _address = value;
                _view.AddressValue = value;
            }
        }

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

        public List<String> AddressAutoComplete { get; set; }
    }
}
