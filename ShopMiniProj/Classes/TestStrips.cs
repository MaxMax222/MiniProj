using System;
namespace ShopMiniProj.Classes
{
    public class TestStrip : Product
    {
        public int StripsPerBox { get; }

        public TestStrip(int productId, double price, string description, string manufacturer, string name, int stripsPerBox)
            : base(productId, price, description, manufacturer, name)
        {
            StripsPerBox = stripsPerBox;
        }
    }
}

