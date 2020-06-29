using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Commands
{
    public class AdsCommand
    {
        public AdsVariables newVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class AdsVariables
    {
        public string Url { get; set; }
    }
}
