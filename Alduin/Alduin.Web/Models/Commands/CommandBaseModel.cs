using System.ComponentModel.DataAnnotations;

namespace Alduin.Web.Models.Commands
{
    public class CommandBaseModel
    {
        [Display(Name = "Force command execute to address")]
        public bool Force { get; set; }
    }
}
