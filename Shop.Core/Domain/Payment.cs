using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Order Order { get; set; } // Navigation property
        public int OrderId { get; set; } // Foreign key
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public static String PaymentMethodDisplayName(PaymentMethod method)
        {
            switch (method)
            {
                case PaymentMethod.BankTransfer: return "Przelew";
                case PaymentMethod.Blik: return "Blik";
                case PaymentMethod.Cash: return "Gotówka";
                case PaymentMethod.CreditCard: return "Karta płatnicza";
                default: return "";
            }
        }

        public static String PaymentStatusDisplayName(PaymentStatus status)
        {
            switch (status)
            {
                case PaymentStatus.InProgress: return "W trakcie realizacji";
                case PaymentStatus.Interrupted: return "Przerwana";
                case PaymentStatus.NotPaid: return "Niezrealizowana";
                case PaymentStatus.Paid: return "Zrealizowana";
                default: return "";
            }
        }
    }
}
