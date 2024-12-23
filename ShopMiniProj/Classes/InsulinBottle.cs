namespace ShopMiniProj.Classes
{
    public class InsulinBottle : Product
    {
        public  double Volume { get; } // e.g., in ml
        public  string Type { get; }// e.g., "Rapid-Acting," "Long-Acting"

        public InsulinBottle(int productId, double price, string description, string manufacturer, string name, double volume, string type)
            : base(productId, price, description, manufacturer, name, "Insulin Bottle")
        {
            Volume = volume;
            Type = type;
        }
    }
}

