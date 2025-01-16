using System;

namespace BIMonitor_MySQL_API.Models
{
    public class RevitProjectGroup
    {
        public int RevitProjectGroupID { get; set; }
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }
        public DateTime LastEdited { get; set; }
        public int UserID { get; set; }
    }
}
