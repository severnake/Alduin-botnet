using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Commands.Commands
{
    public class GetImgCommand
    {
        public ImgVariables newImgModel { get; set; }
        public BaseCommands newBaseCommand { get; set; }
    }

    public class ImgVariables
    {
        public string ImgUrl { get; set; }
    }
}
