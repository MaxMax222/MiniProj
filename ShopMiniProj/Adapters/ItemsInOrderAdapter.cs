using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using ShopMiniProj.Classes;

namespace ShopMiniProj.Adapters
{
	public class ItemsInOrderAdapter:BaseAdapter<Product>
	{
        private readonly Context _context;
        private readonly List<Product> _items;
        private readonly OrderInfo _order;
        public ItemsInOrderAdapter(Context context, List<Product> items, OrderInfo order)
		{
            _context = context;
            _items = items;
            _order = order;
		}

        public override Product this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count { get { return _items.Count; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var product = _items[position];
            var view = convertView;
            view ??= LayoutInflater.From(_context).Inflate(Resource.Layout.order_dialog_item, parent, false);

            var productImg = view.FindViewById<ImageView>(Resource.Id.item_image);
            productImg.SetBackgroundResource(product.ProductId);

            var priceTxt = view.FindViewById<TextView>(Resource.Id.price_txt);
            priceTxt.Text = $"Price: {product.Price}";

            var amountTxt = view.FindViewById<TextView>(Resource.Id.amount_txt);
            amountTxt.Text = $"Amount: {_order.products[product]}";

            return view;
        }
    }
}

