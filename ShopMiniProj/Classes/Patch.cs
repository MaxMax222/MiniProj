using System;
namespace ShopMiniProj.Classes
{
    public class Patch : Product
    {
        public readonly string Material;

        public Patch(int productId, double price, string description, string manufacturer, string name, string material)
            : base(productId, price, description, manufacturer, name, "Patch")
        {
            Material = material;
        }
    }
}

