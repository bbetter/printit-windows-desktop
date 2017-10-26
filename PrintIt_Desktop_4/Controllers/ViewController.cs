using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using PrintIt_Desktop_4.ViewModels;

namespace PrintIt_Desktop_4.Controllers
{
    public abstract class ViewController:ViewModelBase,IViewController
    {
        public virtual void Init()
        {
            Target = new Window();
        }

        public virtual void Show()
        {
            if(Target!=null) Target.Show();
        }

        public virtual void Hide()
        {
            if (Target != null) Target.Hide();
        }

        public virtual void Close()
        {
            if (Target != null) Target.Close();
        }

        public Window Target { get; set; }
    }
}
