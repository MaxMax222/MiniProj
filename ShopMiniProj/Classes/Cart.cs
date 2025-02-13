using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopMiniProj.Classes
{
    public class Cart
    {
        // Private static field to hold the single instance
        private static Cart _instance;

        // Private dictionary to hold cart items
        private Dictionary<Product, int> _items;

        // Private constructor to prevent direct instantiation
        private Cart()
        {
            _items = new Dictionary<Product, int>();
        }

        // Public method to provide global access to the instance
        public static Cart GetInstance()
        {
            // Lazy initialization: create the instance only when needed
            _instance ??= new Cart();
            return _instance;
        }

        // Method to add a product to the cart
        public void AddToCart(Product product)
        {
            if (_items.ContainsKey(product))
            {
                _items[product]++;
            }
            else
            {
                _items.Add(product, 1);
            }
        }

        // Method to remove a single item of a product from the cart
        public void RemoveAnItemFromCart(Product product)
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

        // Method to remove all quantities of a product from the cart
        public void RemoveAllFromCart(Product product)
        {
            if (_items.ContainsKey(product))
            {
                _items.Remove(product);
            }
        }

        // Method to get all items in the cart
        public Dictionary<Product, int> GetCartItems()
        {
            return new Dictionary<Product, int>(_items);
        }

        // Method to calculate the total cost of items in the cart
        public double CalculateTotal()
        {
            return _items.Sum(item => item.Key.Price * item.Value);
        }

        public int GetProductAmount(Product product)
        {
            if (_items.ContainsKey(product))
            {
                return _items[product];
            }
            else
            {
                return -999;
            }
        }

        public List<Product> GetProducts()
        {
            return _items.Keys.ToList();
        }

        public void ClearCart()
        {
            _items = new Dictionary<Product, int>();
        }

        public void AddProducts(Dictionary<Product,int> products)
        {
            foreach (KeyValuePair<Product,int> product in products)
            {
                if (_items.ContainsKey(product.Key))
                {
                    _items[product.Key]+=product.Value;
                }
                else
                {
                    _items.Add(product.Key, product.Value);
                }
            }
        }
    }
}
