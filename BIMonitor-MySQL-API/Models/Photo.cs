using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIMonitor_MySQL_API.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public string Comment { get; set; }
        public string Location { get; set; }
        public string Path { get; set; }
        public int ModelID { get; set; }

    }
}
