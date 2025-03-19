using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers_Project.Models
{
    public class OrderDetails
    {
        //Attributes
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        //Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
