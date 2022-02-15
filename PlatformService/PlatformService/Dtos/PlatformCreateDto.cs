using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos
{
    public class PlatformCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Publisher { get; set; }

        [Required]
        [MaxLength(20)]
        public string Cost { get; set; }
    }
}
