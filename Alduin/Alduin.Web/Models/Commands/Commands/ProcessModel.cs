using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models.Commands.Commands
{
    public class ProcessModel
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public string description { get; set; }
    }
}
