using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintIt_Desktop_4.Model.Core
{
    public class PrintSpotInfo
    {
        public String PrintSpotName { get; set; }
        public String PrintSpotAddress { get; set; }
        public String OwnerName { get; set; }
        public String OwnerSoname { get; set; }
        public String ImageURI { get; set; }
        public String Status { get; set; }
        public bool Available { get; set; }


        public String Description { get; set; }
        public String AdditionalInfo { get; set; }
    }
}
