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

        [Key] // 🔹 تحديد أن Id هو المفتاح الأساسي
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 🔹 جعله Auto-Increment
        public int Id { get; set; }

        [Required] // 🔹 لا يمكن أن يكون فارغًا
        [MaxLength(100)] // 🔹 تحديد الحد الأقصى للطول
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress] // 🔹 التأكد من أن التنسيق هو بريد إلكتروني
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Phone] // 🔹 التأكد من أن الإدخال رقم هاتف صحيح
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = false; // 🔹 افتراضيًا غير نشط

        public bool IsAdmin { get; set; } = false; // 🔹 افتراضيًا ليس أدمن
    }
}
