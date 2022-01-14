using Shop.Core.Domain;
using System;

namespace Shop.Infrastructure.Commands
{
    public class UpdateOrder
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
