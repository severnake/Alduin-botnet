using System;

namespace Alduin.Web.Models
{
    public class BotDeatilsModel
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public DateTime? LastLoggedInUTC { get; set; }
        public string LastIPAddress { get; set; }
        public string Status { get; set; }
    }
}
