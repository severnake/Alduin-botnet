using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands
{
    public class OpenWebsiteCommand : BaseCommands
    {
        public string Url { get; set; }
        public bool Hidde { get; set; }
        public bool Closed { get; set; }
    }
}
