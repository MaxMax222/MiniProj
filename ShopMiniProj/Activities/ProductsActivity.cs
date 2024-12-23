
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
using ShopMiniProj.Adapters;
using ShopMiniProj.Classes;

namespace ShopMiniProj.Activities
{
	[Activity (Label = "ProductsActivity", Theme ="@style/AppTheme", MainLauncher =true)]			
	public class ProductsActivity : Activity
	{
        Button return_button;
        ListView products_listview;
        Spinner filter_spinner;
        List<Product> products;
        Cart cart;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pruducts);
            Init();
        }

        private void Init()
        {
            cart = Cart.GetInstance();
            return_button = FindViewById<Button>(Resource.Id.return_button);
            return_button.Click += (sender, e) => Finish();

            GenerateList();
            products_listview = FindViewById<ListView>(Resource.Id.products_listView);
            products_listview.Adapter = new ProductInCartAdapter(this, products, TypeOfAdapter.ForProducts);
        }

        private void GenerateList()
        {
            products = new List<Product>
            {
                new InsulinPump(Resource.Drawable.pump780g, 3600, "The Medtronic MiniMed 780G insulin pump is a hybrid closed-loop system that adjusts insulin delivery based on CGM data, offering automatic corrections and customizable glucose targets for improved diabetes management.", "Medtronic", "780G", "Minimed 780G", "AA"),
                new InsulinPump(Resource.Drawable.omnipod, 1400, "The Omnipod is a tubeless insulin pump offering discreet, continuous insulin delivery. It features a wearable pod controlled via a handheld device or app, simplifying diabetes management with flexibility and convenience.", "Insulet", "omnipod","DASH pod","built-in")
            };
        }
    }
}

