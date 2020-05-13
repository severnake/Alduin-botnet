using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Floods
{
    public class RudyCommand
    {
        public RudyVariables newRudyVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
        public BaseFloodModel newBaseFloodModel { get; set; }
    }

    public class RudyVariables
    {
        public int Port { get; set; }
        public string PostDATA { get; set; }
    }
}
