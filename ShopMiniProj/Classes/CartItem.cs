using System;
namespace ShopMiniProj.Classes
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalPrice { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
    }
}

