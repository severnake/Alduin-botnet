using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models.UserSettings
{
    public class RoleChangeModel
    {
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
