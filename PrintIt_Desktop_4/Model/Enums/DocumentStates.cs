using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintIt_Desktop_4.Model.Enums
{
    public enum DocumentState
    {
        Pending,
        Queued,
        Printing,
        Done,
        Canceled
    }
}
