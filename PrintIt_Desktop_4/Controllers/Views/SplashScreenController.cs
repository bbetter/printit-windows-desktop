using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Views;

namespace PrintIt_Desktop_4.Controllers.Views
{
    public class SplashScreenController:ViewController
    {
        public override void Init()
        {
            Target = new SplashScreen();
            Target.DataContext = this;
            Progress = 0;
            //ProgressRingActive = true;
        }

        public bool ProgressRingActive { get; set; }
        public int Progress { get; set; }
    }
}
