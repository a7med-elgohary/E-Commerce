using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers.Domain.Entities
{
    public class Card
    {
        // Attributes
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        // Navigation Properties
        public User User { get; set; }

    }
}
