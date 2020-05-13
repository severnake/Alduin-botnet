using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Floods
{
    public class IcmpCommand
    {
        public IcmpVariables newIcmpVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
        public BaseFloodModel newBaseFloodModel { get; set; }
    }

    public class IcmpVariables
    {
        public int Length { get; set; }
        public int Timeout { get; set; }
    }
}
