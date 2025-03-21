using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers_Project.Domain.Models
{
    public class Rating
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        public double? Value { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
        // Navigation Properties
        public User? User { get; set; }
        public Product? Product { get; set; }



    }
}
