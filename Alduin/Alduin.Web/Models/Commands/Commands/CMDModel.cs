using Alduin.Web.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class CMDModel : CommandBaseModel
    {
        [Required]
        [Display(Name = "Command")]
        public string command { get; set; }
    }
}
