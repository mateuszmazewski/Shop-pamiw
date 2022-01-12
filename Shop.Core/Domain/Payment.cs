﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Domain
{
    public enum PaymentMethod
    {
        [Display(Name = "Przelew")]
        BankTransfer,

        Blik,

        [Display(Name = "Gotówka")]
        Cash,

        [Display(Name = "Karta płatnicza")]
        CreditCard
    }

    public enum PaymentStatus
    {
        [Display(Name = "Niezrealizowana")]
        NotPaid,

        [Display(Name = "W trakcie realizacji")]
        InProgress,

        [Display(Name = "Zrealizowana")]
        Paid,

        [Display(Name = "Przerwana")]
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
