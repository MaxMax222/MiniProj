using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using ShopMiniProj.Classes;

namespace ShopMiniProj.Adapters
{
	public class ProductInCartAdapter : BaseAdapter<Product>
	{
        private readonly Context _context;
        private readonly List<Product> _items;
        private Dialog dialog;
        private Cart cart;
        private TypeOfAdapter type;
        public ProductInCartAdapter(Context context, List<Product> items, TypeOfAdapter type)
        {
            _context = context;
            _items = items;
            cart = Cart.GetInstance();
            this.type = type;
		}

        public override Product this[int position]
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
            var display_button = view.FindViewById<Button>(Resource.Id.display_button_top);
            var add_button = view.FindViewById<Button>(Resource.Id.plus_button);
            var remove_button = view.FindViewById<Button>(Resource.Id.minus_button);

            icon_imageView.SetBackgroundResource(product.ProductId);


            if (type == TypeOfAdapter.ForCart)
            {
                int amount = cart.GetProductAmount(product);
                price_textView.Text += $"{product.Price*amount}";
                amount_textView.Text += amount;
            }
            else
            {
                amount_textView.Visibility = ViewStates.Gone;
                price_textView.Text = $"{product.Price}";
            }
            display_button.Tag = position;
            display_button.Click += Display_button_Click;

            add_button.Click += (sender, e) =>
            {
                cart.AddToCart(product);
                NotifyDataSetChanged(); // Refresh the UI to show updated amount and price
            }; remove_button.Tag = position;
            remove_button.Click += Remove_button_Click;
            
            return view;
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int pos = (int)btn.Tag;
            RemoveItem(pos);
        }

        private void RemoveItem(int pos)
        {
            var item = _items[pos];

            if (cart.GetProductAmount(item) > 1 && cart.GetProductAmount(item) != -999)
            {
                cart.RemoveAnItemFromCart(item);
            }
            else
            {
                cart.RemoveAnItemFromCart(item); // Ensure item is removed from the cart
                if (type == TypeOfAdapter.ForCart)
                {
                    _items.RemoveAt(pos); // Remove the item from the list
                } }

            NotifyDataSetChanged(); // Notify the adapter to refresh the UI
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

