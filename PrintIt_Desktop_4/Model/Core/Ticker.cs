using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using PrintIt_Desktop_4.Model.Core.Printing;

namespace PrintIt_Desktop_4.Model.Core
{
    public class Ticker
    {
        public delegate void TickEventHandler();

        public event TickEventHandler OnTick;

        public TimeSpan Interval { get; private set; }

        private Timer _timer;

        public Ticker()
        {
            Interval = new TimeSpan(0,0,0,2);
            _timer = new Timer(PerformTick, null, Interval, Interval);
        }


        private void PerformTick(object sender)
        {
            PrinterManager.UpdateInformation();
            CurrentState.CurrentQueueChecker.Check();
            if(OnTick!=null)
            OnTick();
        }

        public void Stop()
        {
            _timer.Dispose();
        }
    }
}
