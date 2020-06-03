using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Commands
{
    public class MiningCommand
    {
        public MinerVariables newMinerVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }
    public class MinerVariables
    {
        public string Link { get; set; }
        public string Config { get; set; }
    }
}
