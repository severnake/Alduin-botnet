using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class EditTorchFileModel
    {
        [Required]
        [Display(Name = "Torch file")]
        public string Torch { get; set; }
    }
}
