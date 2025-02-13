using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Org.Apache.Commons.Logging;
using ShopMiniProj.Classes;

namespace ShopMiniProj.Adapters
{
    public class oredersAdapter : BaseAdapter<OrderInfo>
    {
        List<OrderInfo> _orders;
        Context _context;
        private Dialog dialog;

        public oredersAdapter(List<OrderInfo> orders, Context context)
        {
            _context = context;
            _orders = orders;
        }

        public override OrderInfo this[int position]
        {
            get { return _orders[position]; }
        }

        public override int Count
        {
            get { return _orders.Count;}
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var order = _orders[position];
            var view = convertView;

            view ??= LayoutInflater.From(_context).Inflate(Resource.Layout.order_item, parent, false);
            var total_txt = view.FindViewById<TextView>(Resource.Id.total_txt);
            var date_txt = view.FindViewById<TextView>(Resource.Id.date_txt);
            var id_txt = view.FindViewById<TextView>(Resource.Id.orderID_txt);
            var show_order_button = view.FindViewById(Resource.Id.display_button_top);

            total_txt.Text = $"Price of Order: {order.TotalPrice}";
            date_txt.Text = $"Date of Order: {order.DateOfOrder}";
            id_txt.Text = $"Order ID: {order.OrderID}";

            show_order_button.Tag = position;
            show_order_button.Click -= Show_order_button_Click;
            show_order_button.Click += Show_order_button_Click;
            
            return view;
        }

        private void Show_order_button_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int pos = (int)btn.Tag;
            ShowOrder(pos);
          

        }

        private void ShowOrder(int pos)
        {
            dialog = new Dialog(_context);
            dialog.SetCanceledOnTouchOutside(true);
            dialog.SetContentView(Resource.Layout.order_dialog);

            var order = _orders[pos];

            var listview = dialog.FindViewById<ListView>(Resource.Id.display_order_listView);
            var add_to_cart = dialog.FindViewById<Button>(Resource.Id.add_order);
            add_to_cart.Click += (sender, e) => { Cart.GetInstance().AddProducts(order.products); };
            dialog.Show();
        }

       
    }
}

