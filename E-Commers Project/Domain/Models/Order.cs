using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers_Project.Domain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        //Navigation Properties

        public required User User { get; set; }

    }

    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Canceled
    }

    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        Cash
    }
}