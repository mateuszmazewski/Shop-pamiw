using Shop.Core.Domain;
using System;

namespace Shop.Infrastructure.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
