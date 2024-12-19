using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using ShopMiniProj.Classes;

namespace ShopMiniProj.Adapters
{
	public class ProductInCartAdapter : BaseAdapter<CartItem>
	{
        private readonly Context _context;
        private readonly List<CartItem> _items;
        private Dialog dialog;
        private Cart cart;
        public ProductInCartAdapter(Context context, List<CartItem> items)
        {
            _context = context;
            _items = items;
            cart = Cart.GetInstance();
		}

        public override CartItem this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var product = _items[position];
            var view = convertView;

            view ??= LayoutInflater.From(_context).Inflate(Resource.Layout.CartItemView, parent, false);

            var icon_imageView = view.FindViewById<ImageView>(Resource.Id.item_image);
            var price_textView = view.FindViewById<TextView>(Resource.Id.price_txt);
            var amount_textView = view.FindViewById<TextView>(Resource.Id.amount_txt);
            var display_button = view.FindViewById<ImageView>(Resource.Id.display_button_top);
            var add_button = view.FindViewById<ImageView>(Resource.Id.plus_button);
            var remove_button = view.FindViewById<ImageView>(Resource.Id.minus_button);

            icon_imageView.SetBackgroundResource(product.ProductId);

            price_textView.Text += product.TotalPrice;
            amount_textView.Text += product.Quantity;

            display_button.Tag = position;
            display_button.Click += Display_button_Click;

            add_button.Click += (sender, e) => cart.AddToCart(product);
            remove_button.Click += (sender, e) => cart.RemoveAnItemFromCart(product);
            
            return view;
        }

      
        private void Display_button_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int pos = (int)btn.Tag;
            showProduct(pos);
        }

        private void showProduct(int pos)
        {
            dialog = new Dialog(_context);
            dialog.SetCanceledOnTouchOutside(true);
            dialog.SetContentView(Resource.Layout.ProductDialog);

            var product = _items[pos];
            var product_imageView = dialog.FindViewById<ImageView>(Resource.Id.product_image);
            product_imageView.SetBackgroundResource(product.ProductId);

            var description_text = dialog.FindViewById<TextView>(Resource.Id.product_description);
            description_text.Text = product.Description;

            dialog.Show();

        }
    }
}

