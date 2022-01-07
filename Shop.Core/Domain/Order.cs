using System;

namespace Shop.Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
