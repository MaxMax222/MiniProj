using System;
namespace ShopMiniProj.Classes
{
    public class Patch : Product
    {
        public string Color { get; }

        public Patch(int productId, double price, string description, string manufacturer, string name, string color)
            : base(productId, price, description, manufacturer, name)
        {
            Color = color;
        }
    }
}

