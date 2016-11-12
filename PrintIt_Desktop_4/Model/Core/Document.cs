using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Model.Enums;
using PrintIt_Desktop_4.Other;
using PrintIt_Desktop_4.ViewModels;

namespace PrintIt_Desktop_4.Model.Core
{
    [Magic]
    public class Document:ViewModelBase
    {
        public bool Selected { get; set; }
        public int Progress { get; set; }
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
