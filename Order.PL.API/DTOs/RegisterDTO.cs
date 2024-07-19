using System.ComponentModel.DataAnnotations;

namespace Order.PL.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
}
