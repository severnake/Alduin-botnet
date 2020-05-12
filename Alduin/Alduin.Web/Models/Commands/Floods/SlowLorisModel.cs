using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class SlowLorisModel : BaseFloodModel
    {
        [Required]
        [Display(Name = "Port")]
        public int Port { get; set; }
        [Required]
        [Display(Name = "Post Data")]
        public string PostDATA { get; set; }
        [Display(Name = "Random file")]
        public bool RandomFile { get; set; }
    }
}
