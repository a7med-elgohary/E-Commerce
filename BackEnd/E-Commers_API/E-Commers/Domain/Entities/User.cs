using System.ComponentModel.DataAnnotations;

namespace E_Commers.Domain.Entities
{
    public class User
    {
        public User()
        {
            
        }

        public User(int id, string name, string email, string password, string? phoneNumber = null, string? address = null, bool isActive = false)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = false; 
        
    }
}
