using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commers_Project.Domain.Models
{
    public class ProductPhoto
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public string Url { get; set; } = string.Empty;
        // Navigation Properties
        public required Product product { get; set; }

    }
}
