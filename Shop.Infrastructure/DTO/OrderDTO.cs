using Shop.Core.Domain;
using System;

namespace Shop.Infrastructure.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
