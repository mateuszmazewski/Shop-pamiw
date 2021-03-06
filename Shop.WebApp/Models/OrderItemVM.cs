using Shop.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class OrderItemVM
    {
        public int Id { get; set; }
        public Order Order { get; set; }

        [Display(Name = "ID Zamówienia")]
        public int OrderId { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Produkt")]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public int ProductId { get; set; }

        [Display(Name = "Ilość")]
        [Required(ErrorMessage = "Ilość jest wymagana")]
        public double Quantity { get; set; }
    }
}
