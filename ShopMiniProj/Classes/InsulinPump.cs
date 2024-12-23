namespace ShopMiniProj.Classes
{
    public class InsulinPump : Product
    {
        public string Model { get; }
        public string BatteryType { get; }

        public InsulinPump(int productId, double price, string description, string manufacturer, string name, string model, string batteryType)
            : base(productId, price, description, manufacturer, name, "Insulin Pump")
        {
            Model = model;
            BatteryType = batteryType;
        }
    }
}

