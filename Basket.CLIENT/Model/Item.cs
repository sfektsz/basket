namespace Basket.CLIENT.Model
{
    /// <summary>
    /// This class represents a model of an item which is present in the cart
    /// </summary>
    public class Item
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}