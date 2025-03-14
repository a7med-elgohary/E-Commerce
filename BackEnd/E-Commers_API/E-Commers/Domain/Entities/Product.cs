using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace E_Commers.Domain.Entities
{
    public class Product
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsFavorite { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Sale")]
        public int SaleId { get; set; }
        // Navigation Properties
        public Category Category { get; set; }
        public Sale Sale { get; set; }

    }
}
