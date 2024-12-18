using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopMiniProj.Classes
{
    public static class Cart
    {
        private static Dictionary<Product, int> _items = new Dictionary<Product, int>();

        public static void AddToCart(Product product)
        {
            if (_items.ContainsKey(product))
            {
                _items[product] ++;
            }
            else
            {
                _items.Add(product,1);
            }
        }

        public static void RemoveAnItemFromCart(Product product)
        {
            if (_items.ContainsKey(product))
            {
                _items[product]--;
                if (_items[product] <= 0)
                {
                    _items.Remove(product);
                }
            }
        }

        public static void RemoveAllFromCart(Product product)
        {
            if (_items.ContainsKey(product))
            {
                _items.Remove(product);
            }
        }

        public static Dictionary<Product, int> GetCartItems()
        {
            return new Dictionary<Product, int>(_items);
        }

        public static double CalculateTotal()
        {
            return _items.Sum(item => item.Key.Price * item.Value);
        }
    }
}
