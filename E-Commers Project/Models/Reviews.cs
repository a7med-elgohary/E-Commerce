using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace E_Commers_Project.Models
{
    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public double Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreateDate { get; set; }
        // Navigation Properties
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
