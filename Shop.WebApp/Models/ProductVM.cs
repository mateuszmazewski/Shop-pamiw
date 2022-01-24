using Shop.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Cena jest wwymagana")]
        public double Price { get; set; }

        [Display(Name = "Zapas")]
        [Required(ErrorMessage = "Zapas jest wymagany")]
        public double Stock { get; set; }

        [Display(Name = "J.m.")]
        [Required(ErrorMessage = "Jednostka miary jest wymagana")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} -- {Price} zł";
        }
    }
}
