using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commers.Domain.Entities
{
    public class Favorite
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }
        //Navigation Properties 
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
