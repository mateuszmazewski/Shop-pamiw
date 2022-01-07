namespace Shop.Core.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
    }
}
