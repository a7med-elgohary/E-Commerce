using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace E_Commers_Project.Domain.Models
{
    public class Product
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required, Range(0, double.MaxValue, ErrorMessage = "price must be a positive value")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public bool IsFavorite { get; set; } = false;
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Sale")]
        public int? SaleId { get; set; }
        // Navigation Properties
        public Category? Category { get; set; }
        public Sale? Sale { get; set; }

        public ICollection<ProductPhoto>? photos { get; set; } = new List<ProductPhoto>();

    }
}
