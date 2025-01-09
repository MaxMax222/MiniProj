
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using ShopMiniProj.Classes;
using ShopMiniProj.Adapters;

namespace ShopMiniProj.Activities
{
	[Activity (Label = "CartActivity")]			
	public class CartActivity : MenuActivity
	{
		ListView products;
		TextView total;
		Cart cart;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.cart_screen);
			Init();
		}

        private void Init()
        {
			products = FindViewById<ListView>(Resource.Id.products_listview_cart);
			total = FindViewById<TextView>(Resource.Id.total);
			cart = Cart.GetInstance();

			var adapter = new ProductInCartAdapter(this, cart.GetProducts(), TypeOfAdapter.ForCart);
			adapter.OnCartUpdated += UpdateTotal;
			products.Adapter = adapter;
			UpdateTotal();
        }

        private void UpdateTotal()
        {
            double totalAmount = cart.CalculateTotal();
            total.Text = $"Total: {totalAmount}$"; // Display total as currency
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            // Remove the "Cart" menu item for specific activities
            if (this is CartActivity) {
				
                menu.FindItem(Resource.Id.menu_cart)?.SetVisible(false);
            }

            return true;
        }
    }
}

