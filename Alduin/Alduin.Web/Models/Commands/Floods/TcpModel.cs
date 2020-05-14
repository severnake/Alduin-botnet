using System.ComponentModel.DataAnnotations;

namespace Alduin.Web.Models
{
    public class TcpModel : BaseFloodModel
    {
        [Required]
        [Display(Name = "Port")]
        public int Port { get; set; }
        [Required]
        [Display(Name = "Length")]
        public int Length { get; set; }
    }
}
