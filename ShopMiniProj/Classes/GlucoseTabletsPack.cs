using System;
namespace ShopMiniProj.Classes
{
    public class GlucoseTabletPack : Product
    {
        public readonly int TabletsPerPack;
        public readonly double TabletStrength; // e.g., mg of glucose per tablet

        public GlucoseTabletPack(int productId, double price, string description, string manufacturer, string name, int tabletsPerPack, double tabletStrength)
            : base(productId, price, description, manufacturer, name, "Glucose Tablet")
        {
            TabletsPerPack = tabletsPerPack;
            TabletStrength = tabletStrength;
        }
    }
}

