using Alduin.Shared.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Shared.DTOs
{
    public class BotLogDTO : DTOBase
    {
        public int? BotId { get; set; }
        public BotDTO Bot { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
