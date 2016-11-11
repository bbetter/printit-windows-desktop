using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Model.Enums;

namespace PrintIt_Desktop_4.Model.Core
{
    public class Document
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String OrientedDateMin { get; set; }
        public String OrientedDateMax { get; set; }
        public int PageCount { get; set; }
        public float Price { get; set; }
        public String CreatedAt { get; set; }
        public DocumentState State { get; set; }

    }
}
