using Alduin.Web.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class BaseFloodModel : CommandBaseModel
    {
        [Required]
        [Display(Name = "Host")]
        public string Host { get; set; }
        [Required]
        [Display(Name = "Time to Attack")]
        public int Time { get; set; }
        [Required]
        [Display(Name = "Thread Use")]
        public int ThreadstoUse { get; set; }
    }
}
