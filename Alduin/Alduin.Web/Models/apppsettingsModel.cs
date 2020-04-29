using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class ConnectionStrings
    {
        public string ConnectDataBase { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }
    public class Stump
    {
        public string KeyCertified { get; set; }
    }
    public class Tor
    {
        public bool RunItStart { get; set; }
    }

    public class appsettingsModel
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Tor Tor { get; set; }
        public Stump Stump { get; set; }
    }
    
}
