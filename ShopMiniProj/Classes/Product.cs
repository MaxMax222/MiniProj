namespace ShopMiniProj.Classes
{
    public class Product
    {
        public int ProductId { get; protected set; } // set to resource.drawable.<name_of_image> for dialog and listview convinience
        public double Price { get; protected set; }
        public string Description { get; protected set; }
        public string Manufacturer { get; protected set; }
        public string Name { get; protected set; }

        public Product(int productId, double price, string description, string manufacturer, string name)
        {
            ProductId = productId;
            Price = price;
            Description = description;
            Manufacturer = manufacturer;
            Name = name;
        }
        // Override Equals
        public override bool Equals(object obj)
        {
            if (obj is Product otherProduct)
            {
                // Compare by unique properties, e.g., ProductId
                return ProductId == otherProduct.ProductId;
            }
            return false;
        }

        // Override GetHashCode
        public override int GetHashCode()
        {
            // Use ProductId as the basis for hash code
            return ProductId.GetHashCode();
        }
    }
}

