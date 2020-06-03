
using System.ComponentModel.DataAnnotations;

namespace Alduin.Web.Models
{
    public class MiningModel
    {
        [Required]
        [Display(Name = "GitHub Realese link (.zip)")]
        public string Link { get; set; }
        [Required]
        [Display(Name = "Config arguments")]
        public string Config { get; set; }
    }
}
