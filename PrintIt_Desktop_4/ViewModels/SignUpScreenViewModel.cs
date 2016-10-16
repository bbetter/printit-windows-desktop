using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    public class SignUpScreenViewModel:ViewModelBase
    {
        public String Login { get; set; }
        public String Name { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
    }
}
