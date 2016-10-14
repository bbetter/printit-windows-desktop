using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{

    [Magic]
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected virtual void RaisePropertyChanged(string propName)
        {
            var e = PropertyChanged;
            if (e != null)
                e(this, new PropertyChangedEventArgs(propName));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected static void Raise() { }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
