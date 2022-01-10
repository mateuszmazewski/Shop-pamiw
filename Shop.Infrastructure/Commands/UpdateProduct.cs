using Shop.Core.Domain;

namespace Shop.Infrastructure.Commands
{
    public class UpdateProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
    }
}
