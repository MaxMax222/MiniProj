using System;
namespace ShopMiniProj.Classes
{
    public class InsulinSyringe : Product
    {
        public string NeedleGauge { get; }
        public double Capacity { get; } // e.g., in ml

        public InsulinSyringe(int productId, double price, string description, string manufacturer, string name, string needleGauge, double capacity)
            : base(productId, price, description, manufacturer, name, "Insulin Syringe")
        {
            NeedleGauge = needleGauge;
            Capacity = capacity;
        }
    }
}