using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIMonitor_MySQL_API.Models
{
    [JsonObject, Serializable]
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Organization { get; set; }
        public string HashedPassword { get; set; }
        public string SaltPassword { get; set; }
        public string HashedEmail { get; set; }
        public bool Activated { get; set; }

    }
}
