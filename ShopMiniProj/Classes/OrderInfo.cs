using System;
using System.Collections.Generic;
using System.Linq;
using static Android.Content.ClipData;

namespace ShopMiniProj.Classes
{
	public class OrderInfo
	{
		public Dictionary<Product, int> products { get; private set; }
		public double TotalPrice { get; private set; }
		public string OrderID { get; private set; }
		public DateTime DateOfOrder { get; private set; }
		public OrderInfo(Dictionary<Product,int> products, double totalPrice, string orderID, DateTime date)
		{
			this.products = products;
			TotalPrice = totalPrice;
			OrderID = orderID;
			DateOfOrder = date;
		}
        public List<Product> GetProducts()
        {
            return products.Keys.ToList();
        }
    }
}

