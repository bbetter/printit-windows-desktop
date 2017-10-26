using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PrintIt_Desktop_4.Controllers
{
    public interface IUserControlController
    {
        void Init();
        UserControl Target { get; set; }

    }
}
