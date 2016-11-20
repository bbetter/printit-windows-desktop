using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;

namespace PrintIt_Desktop_4.Other
{
    public class CancelCloseWindowBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty CancelCloseProperty =
          DependencyProperty.Register("CancelClose", typeof(bool),
            typeof(CancelCloseWindowBehavior), new FrameworkPropertyMetadata(false));

        public bool CancelClose
        {
            get { return (bool)GetValue(CancelCloseProperty); }
            set { SetValue(CancelCloseProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Closing += (sender, args) => args.Cancel = CancelClose;
        }
    }
}
