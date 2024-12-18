using System;
using System.Text.Json;

namespace ShopMiniProj.Classes
{

    public class Order
    {
        public readonly UserInfo User;
        public readonly CartDetails Cart;
        public readonly string Address;
        public readonly string ZipCode;

        public Order(UserInfo user, CartDetails cart, string address, string zipCode)
        {
            User = user;
            Cart = cart;
            Address = address;
            ZipCode = zipCode;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}

