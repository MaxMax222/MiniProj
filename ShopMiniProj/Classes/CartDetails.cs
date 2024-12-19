using System;
using System.Collections.Generic;

namespace ShopMiniProj.Classes
{
    public class CartDetails
    {
        public List<CartItem> Items { get; }
        public double Total { get; set; }

        public CartDetails(Dictionary<Product, int> cartItems, double total)
        {
            Items = new List<CartItem>();
            foreach (var item in cartItems)
            {
                Items.Add(new CartItem
                {
                    ProductId = item.Key.ProductId,
                    ProductName = item.Key.Name,
                    Quantity = item.Value,
                    PricePerUnit = item.Key.Price,
                    TotalPrice = item.Key.Price * item.Value,
                    Description = item.Key.Description,
                    Manufacturer = item.Key.Manufacturer
                });
            }
            Total = total;
        }
    }
}

