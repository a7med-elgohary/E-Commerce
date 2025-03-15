using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers.Domain.Entities
{
    public class Payments
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int Quantity { get; set; } 
        public decimal SubTotal { get; set; }
        //Navigation Properties
        public Order Order { get; set; }
    }
}
