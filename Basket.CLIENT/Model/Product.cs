namespace Basket.CLIENT.Model
{
    /// <summary>
    /// The class represents the model of a product which is present in the database
    /// </summary>
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}