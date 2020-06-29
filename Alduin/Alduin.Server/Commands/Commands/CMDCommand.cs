using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Commands
{
    public class CMDCommand
    {
        public CMDVariables newVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class CMDVariables
    {
        public string command { get; set; }
    }
}
