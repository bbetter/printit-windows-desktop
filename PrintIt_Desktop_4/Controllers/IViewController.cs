using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PrintIt_Desktop_4.Controllers
{
    public interface IViewController
    {
        void Init();
        void Show();
        void Hide();
        void Close();

        Window Target { get; set; }

    }
}
