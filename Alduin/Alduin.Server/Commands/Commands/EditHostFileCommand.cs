using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Commands
{
    public class EditHostFileCommand
    {
        public EditHostFileVariables newVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class EditHostFileVariables
    {
        public string Line { get; set; }
    }
}
