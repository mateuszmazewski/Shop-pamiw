namespace Shop.Core.Domain
{
    public enum UnitOfMeasurement
    {
        Pieces, Kilogram, Meter
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
