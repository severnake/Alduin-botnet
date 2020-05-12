using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands
{
    public class ExecuteCommand
    {
        public ExecuteVariables newExecute { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class ExecuteVariables
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public bool Run { get; set; }
        public bool Proxy { get; set; }
    }
}
