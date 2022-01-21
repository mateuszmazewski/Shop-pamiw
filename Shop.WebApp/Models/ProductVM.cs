using Shop.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Display(Name = "Zapas")]
        public double Stock { get; set; }

        [Display(Name = "J.m.")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} -- {Price} zł";
        }
    }
}
