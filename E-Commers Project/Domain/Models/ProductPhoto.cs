using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commers_Project.Domain.Models
{
    public class ProductPhoto
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("product")]
        public int ProuductId { get; set; }
        public string Url { get; set; } = string.Empty;
        // Navigation Properties
        public required User User { get; set; }
        public required Product product { get; set; }

    }
}
