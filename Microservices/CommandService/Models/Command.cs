using System.ComponentModel.DataAnnotations;

namespace CommandService.Models
{
    public class Command
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Commandline { get; set; }

        [Required]
        public int PlatformId { get; set; }

        public Platform Platform { get; set; }
    }
}
