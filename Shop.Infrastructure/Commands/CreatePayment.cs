using Shop.Core.Domain;

namespace Shop.Infrastructure.Commands
{
    public class CreatePayment
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
