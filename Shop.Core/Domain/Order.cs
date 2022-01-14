using System;

namespace Shop.Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; } // Navigation property
        public int CustomerId { get; set; } // Foreign Key
        public Payment? Payment { get; set; } // Navigation property
        public int PaymentId { get; set; } // Foreign Key
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
