using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopMiniProj.Classes
{

    public class Order
    {
        private static int ID = 0;
        private int orderId;
        private UserInfo User;
        private Dictionary<Product, int> CartItems;
        private double TotalCost;

        public Order(UserInfo user, Cart cart)
        {
            orderId = ++ID;
            User = user;
            CartItems = cart.GetCartItems();
            TotalCost = cart.CalculateTotal();
        }

        // Method to serialize the object to JSON
        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        // Static method to deserialize from JSON to CombinedUserCart
        public Order DeserializeFromJson(string json)
        {
            return JsonConvert.DeserializeObject<Order>(json);
        }
        public override string ToString()
        {
            return $"User info: {User}, order id: {ID}";
        }
    }
}

