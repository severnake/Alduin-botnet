using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Commands
{
    public class GetSourceFileCommand
    {
        public GetFileVariables newVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class GetFileVariables
    {
        public string filePath { get; set; }
    }
}
