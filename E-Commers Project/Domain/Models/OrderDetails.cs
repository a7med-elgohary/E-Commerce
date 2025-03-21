using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers_Project.Domain.Models
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
        public required Order Order { get; set; }
        public  required Product Product { get; set; }
    }
}
