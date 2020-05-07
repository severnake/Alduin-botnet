using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands
{

    public class WebsiteOpenModel
    {
        public WebsiteVariables newWebsiteModel { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class WebsiteVariables
    {
        public string Url { get; set; }
        public bool Hidde { get; set; }
        public bool Closed { get; set; }
    }
}
