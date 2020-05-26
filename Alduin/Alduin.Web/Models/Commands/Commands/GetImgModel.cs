using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models.Commands.Commands
{
    public class GetImgModel
    {
        [Required]
        public string Url { get; set; }
    }
}
