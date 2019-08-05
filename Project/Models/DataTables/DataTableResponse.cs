using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class DataTableResponse
    {
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object[] data { get; set; }
        //public string error { get; set; }
    }
}