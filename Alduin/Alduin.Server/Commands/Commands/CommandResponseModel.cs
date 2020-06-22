using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Server.Commands
{
    public class CommandResponseModel
    {
        public string Message { get; set; }
        public string KeyUnique { get; set; }
        public string Type { get; set; }
    }
}
