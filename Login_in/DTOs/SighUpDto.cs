using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Login_in.DTOs
{
    public class SighUpDto
    {
        
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
    }
}
