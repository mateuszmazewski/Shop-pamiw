using Shop.Core.Domain;

namespace Shop.Infrastructure.Commands
{
    public class CreateOrderItem
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
    }
}
