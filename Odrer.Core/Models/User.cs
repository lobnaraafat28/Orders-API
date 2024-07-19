using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;


namespace Orders.Core.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
