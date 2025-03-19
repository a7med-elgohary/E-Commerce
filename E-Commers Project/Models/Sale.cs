using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace E_Commers_Project.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        //Attributes
        [ForeignKey("Product")] 
        public int ProductId { get; set; }
        public decimal SaleValue { get; set; }
        public DateTime SaleDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        //Navigation Properties
        public Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
