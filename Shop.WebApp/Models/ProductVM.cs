using Shop.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        [Required]
        public double Price { get; set; }

        [Display(Name = "Zapas")]
        [Required]
        public double Stock { get; set; }

        [Display(Name = "J.m.")]
        [Required]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} -- {Price} zł";
        }
    }
}
