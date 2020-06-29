using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Commands
{
    public class SeedTorrentCommand
    {
        public SeedTorrentVariables newVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class SeedTorrentVariables
    {
        public string path { get; set; }
    }
}
