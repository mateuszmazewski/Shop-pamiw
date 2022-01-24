using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class OrderVM
    {
        [Display(Name = "ID Zamówienia")]
        public int Id { get; set; }

        public CustomerVM Customer { get; set; }

        [Display(Name = "Klient")]
        [Required(ErrorMessage = "Klient jest wymagany")]
        public int CustomerId { get; set; }

        public PaymentVM Payment { get; set; }

        [Display(Name = "Płatność")]
        public int PaymentId { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Data modyfikacji")]
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"[{Id}] złożone: {CreatedAt.ToString("dd/MM/yyyy HH:mm")}, klient: {Customer.Name} {Customer.Surname}";
        }
    }
}
