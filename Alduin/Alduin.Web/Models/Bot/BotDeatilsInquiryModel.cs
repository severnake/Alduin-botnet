using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class BotDeatilsInquiryModel
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string LastIPAddress { get; set; }
        public DateTime? LastLoggedInUTC { get; set; }
        public string Status { get; set; }
    }
}
