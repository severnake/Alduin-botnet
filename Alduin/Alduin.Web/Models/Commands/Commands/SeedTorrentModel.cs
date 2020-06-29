using Alduin.Web.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Models
{
    public class SeedTorrentModel : CommandBaseModel
    {
        [Required]
        [Display(Name = "Url")]
        public string Url { get; set; }
    }
}
