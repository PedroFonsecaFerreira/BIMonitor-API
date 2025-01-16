using System;

namespace BIMonitor_MySQL_API.Models
{
    public class RevitProject
    {
        public int RevitProjectID { get; set; }
        public string Name { get; set; }
        public string FolderName { get; set; }
        public string Location { get; set; }
        public string SelectedQuality { get; set; }
        public bool IsAllProjectViews { get; set; }
        public bool IsAllParameters { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime LastEdited { get; set; }
        public string LastVerified { get; set; }
        public int RevitProjectGroupID { get; set; }
        public string TaskElementsParametersCsvLocalPath { get; set; }
        public string TaskTimelinersCsvLocalPath { get; set; }

    }
}
