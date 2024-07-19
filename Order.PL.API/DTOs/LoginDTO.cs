using System.ComponentModel.DataAnnotations;

namespace Order.PL.API.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }

    }
}
