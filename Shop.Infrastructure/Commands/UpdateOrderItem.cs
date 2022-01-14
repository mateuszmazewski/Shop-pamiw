using Shop.Core.Domain;

namespace Shop.Infrastructure.Commands
{
    public class UpdateOrderItem
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
