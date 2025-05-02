using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers_Project.Domain.Models
{
    public class User
    {
        public User() { }

        public User(int id, string name, string email, string password, string? phoneNumber = null, string? address = null, bool isActive = false, bool isAdmin = false)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
            IsActive = isActive;
            IsAdmin = isAdmin;
        }

        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] 
        [MaxLength(100)] 
        public required string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress] 
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Phone] 
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = false; 

        public bool IsAdmin { get; set; } = false; 
    }
}
