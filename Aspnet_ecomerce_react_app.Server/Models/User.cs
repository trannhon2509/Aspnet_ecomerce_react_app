using System.ComponentModel.DataAnnotations;

namespace Aspnet_ecomerce_react_app.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        public string? Email { get; set; }
        [Required]
        public ICollection<Role>? Roles { get; set; }
    }
}
