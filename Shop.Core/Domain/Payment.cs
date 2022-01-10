using System;

namespace Shop.Core.Domain
{
    public enum PaymentMethod
    {
        BankTransfer,
        Blik,
        Cash,
        CreditCard
    }

    public class Payment
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
