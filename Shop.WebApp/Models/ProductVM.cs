using Shop.Core.Domain;

namespace Shop.WebApp.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
    }
}
