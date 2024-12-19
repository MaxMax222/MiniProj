namespace ShopMiniProj.Classes
{
    public class Product
    {
        public int ProductId { get; protected set; } // set to resource.drawable.<name_of_image> for dialog and listview convinience
        public double Price { get; protected set; }
        public string Description { get; protected set; }
        public string Manufacturer { get; protected set; }
        public string Name { get; protected set; }

        public Product(int productId, double price, string description, string manufacturer, string name, string category)
        {
            ProductId = productId;
            Price = price;
            Description = description;
            Manufacturer = manufacturer;
            Name = name;
        }
    }
}

