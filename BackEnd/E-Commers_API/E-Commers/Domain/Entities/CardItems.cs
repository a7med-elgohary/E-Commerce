using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers.Domain.Entities
{
    public class CardItems
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        [ForeignKey("Card")]
        public int CardId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        // Navigation Properties
        public Card Card { get; set; }
        public Product Product { get; set; }
    }
}
