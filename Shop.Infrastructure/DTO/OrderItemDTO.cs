using Shop.Core.Domain;

namespace Shop.Infrastructure.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
    }
}
