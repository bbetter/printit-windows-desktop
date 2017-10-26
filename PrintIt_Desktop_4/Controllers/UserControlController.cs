using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using PrintIt_Desktop_4.ViewModels;

namespace PrintIt_Desktop_4.Controllers
{
    public abstract class UserControlController : ViewModelBase, IUserControlController
    {
        public virtual void Init()
        {
            Target.DataContext = Target;
        }
        
        public UserControl Target { get; set; }
    }
}
