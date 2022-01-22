using Shop.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class PaymentVM
    {
        [Display(Name = "ID Płatności")]
        public int Id { get; set; }

        [Display(Name = "Zamówienie")]
        [Required]
        public int OrderId { get; set; }

        [Display(Name = "Kwota")]
        [Required]
        public double Amount { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Data modyfikacji")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Met. płatności")]
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
