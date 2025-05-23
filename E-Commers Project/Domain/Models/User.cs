using E_Commers_Project.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
[Index(nameof(Email), IsUnique = true)]
public class User
{
    public User()
    {
        Cart = new Cart();
    }

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
        Cart = new Cart { UserId = id };
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
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    public Cart Cart { get; set; }

    [Phone]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(250)]
    public string? Address { get; set; }

    public bool IsActive { get; set; } = false;

    public bool IsAdmin { get; set; } = false;
}
