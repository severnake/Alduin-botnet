using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class IcmpModel : BaseFloodModel
    {
        [Required]
        [Display(Name = "Length")]
        public int Length{ get; set; }
        [Required]
        [Display(Name = "TimeOut")]
        public int Timeout { get; set; }
    }
}
