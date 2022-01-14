namespace Shop.Core.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; } // Navigation property
        public int OrderId { get; set; } // Foreign key
        public Product Product { get; set; } // Navigation property
        public int ProductId { get; set; } // Foreign key
        public double Quantity { get; set; }
    }
}
