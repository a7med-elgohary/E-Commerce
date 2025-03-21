using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commers_Project.Domain.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        public string? UserName { get; set; }
        public string? genral { get; set; }

    }
}