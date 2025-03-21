using System.ComponentModel.DataAnnotations;

namespace E_Commers_Project.Domain.Models
{
    public class SignUpModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public required string UserName { get; set; }
    }
}
