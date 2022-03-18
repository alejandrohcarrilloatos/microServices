using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos
{
    public class CommandCreateDto
    {

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        
        [Required]
        [MaxLength(100)] 
        public string CommandLine { get; set; }

    }
}
