﻿using System;

namespace Shop.Core.Domain
{
    public enum PaymentMethod
    {
        BankTransfer,
        Blik,
        Cash,
        CreditCard
    }

    public enum PaymentStatus
    {
        NotPaid,
        InProgress,
        Paid,
        Interrupted
    }

    public class Payment
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}