using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace E_Commers_Project.Domain.Models
{
    public class Cart
    {
        // Attributes
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<CartItem?>? CartItems { get; set; } = new List<CartItem?>();
        // Navigation Properties
        public virtual User User { get; set; }

    }
}
