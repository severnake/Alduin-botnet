using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands
{
    public class MessageCommand
    {
        public MessageVariables newMessageVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }
    public class MessageVariables
    {
        public string Msg { get; set; }
        public bool Closed { get; set; }
    }
}
