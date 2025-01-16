using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIMonitor_MySQL_API.Models
{
    public class Thumbnail
    {
        public int ThumbnailID { get; set; }
        public string JSONProject { get; set; }
        public string Image { get; set; }
        public string LastModified { get; set; }
        public string Version { get; set; }
        public int ModelID { get; set; }

    }
}