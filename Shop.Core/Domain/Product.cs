using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Domain
{
    public enum UnitOfMeasurement
    {
        [Display(Name = "szt.")]
        Pieces,

        [Display(Name = "kg")]
        Kilogram,

        [Display(Name = "m")]
        Meter
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
    }
}
