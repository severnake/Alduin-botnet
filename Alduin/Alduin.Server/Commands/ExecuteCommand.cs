using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands
{
    public class ExecuteCommand : BaseCommands
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public bool Run { get; set; }
        public bool Proxy { get; set; }
    }
}
