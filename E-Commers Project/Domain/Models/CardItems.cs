using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers_Project.Domain.Models
{
    // This table is created as a result of a many-to-many relationship between Product and Cart.

    public class CartItem
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        // Navigation Properties
        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
