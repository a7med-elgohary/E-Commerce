using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace E_Commers.Domain.Entities
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
        // Navigation Properties
        public virtual User User { get; set; } 

    }
}
