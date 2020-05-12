using Alduin.Web.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class MessageModel : CommandBaseModel 
    {
        [Required]
        [Display(Name = "Message")]
        public string Msg { get; set; }
        [Display(Name = "Cannot be Closed")]
        public bool Closed { get; set; }
    }
}
