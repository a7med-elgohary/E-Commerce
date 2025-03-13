using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers.Domain.Entities
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        //Navigation Properties
        public User User { get; set; }

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