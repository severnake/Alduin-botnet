using System;
using System.ComponentModel.DataAnnotations;

namespace Alduin.Web.Models.Bot
{
    public class BotDeatilsModel
    {
        public string KeyUnique { get; set; }
        public string HardwareName { get; set; }
        public string HardwareType { get; set; }
        public string Performance { get; set; }
        public string OtherInformation { get; set; }
    }
}
