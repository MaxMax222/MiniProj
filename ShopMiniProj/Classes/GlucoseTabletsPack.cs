using System;
namespace ShopMiniProj.Classes
{
    public class GlucoseTabletPack : Product
    {
        public int TabletsPerPack { get; }
        public double TabletStrength { get; }// e.g., mg of glucose per tablet

        public GlucoseTabletPack(int productId, double price, string description, string manufacturer, string name, int tabletsPerPack, double tabletStrength)
            : base(productId, price, description, manufacturer, name, "Glucose Tablet")
        {
            TabletsPerPack = tabletsPerPack;
            TabletStrength = tabletStrength;
        }
    }
}

