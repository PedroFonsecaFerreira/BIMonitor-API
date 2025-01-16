using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIMonitor_MySQL_API.Models
{
    public class Model
    {
        public int ModelID { get; set; }
        public string Name { get; set; }
        public string OBJPath { get; set; }
        public string ThumbnailPath { get; set; }
        public string CSVPath { get; set; }
        public string JSONPath { get; set; }
        public string Location { get; set; }
        public string Owner { get; set; }
        public string ModelQuality { get; set; }
        public string ModelUnits { get; set; }
        public string RevitName { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime LastEdited { get; set; }
        public string LastVerified { get; set; }

    }
}
