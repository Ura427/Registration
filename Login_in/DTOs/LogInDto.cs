using System.ComponentModel.DataAnnotations;

namespace Login_in.DTOs
{
    public class LogInDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
