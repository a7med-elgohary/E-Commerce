using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers.Domain.Entities
{
    public class Category
    {
        //Attributes
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Navigation Property (One-to-Many with Product)
        public virtual ICollection<Product> Product { get; set; } = new List<Product>();

    }
}
