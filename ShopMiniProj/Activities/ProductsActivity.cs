
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
using ShopMiniProj.Adapters;
using ShopMiniProj.Classes;

namespace ShopMiniProj.Activities
{
	[Activity (Label = "ProductsActivity", Theme ="@style/AppTheme")]			
	public class ProductsActivity : AppCompatActivity
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
            filter_spinner = FindViewById<Spinner>(Resource.Id.products_filter_spinner);
            SetUpFilter();
        }

        private void SetUpFilter()
        {
            var filterCategories = new List<string>{ "Show All", "Sort by price","Insulin pump", "Insulin Syringes", "Insulin Bottles", "Patches", "Glucose tablet", "Test strip"};
            var adapter = new ArrayAdapter<string>(this, Resource.Layout.spinner_item, filterCategories);
            filter_spinner.Adapter = adapter;

            filter_spinner.ItemSelected += Filter_spinner_ItemSelected;

        }

        private void Filter_spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var filteredProducts = GetFilteredProducts(e.Position);
            products_listview.Adapter = new ProductInCartAdapter(this, filteredProducts, TypeOfAdapter.ForProducts);

        }

        private List<Product> GetFilteredProducts(int position)
        {
            return position switch
            {
                0 => products,
                1 => products.OrderBy(p => p.Price).ToList(),
                2 => products.OfType<InsulinPump>().Cast<Product>().ToList(),
                3 => products.OfType<InsulinSyringe>().Cast<Product>().ToList(),
                4 => products.OfType<InsulinBottle>().Cast<Product>().ToList(),
                5 => products.OfType<Patch>().Cast<Product>().ToList(),
                6 => products.OfType<GlucoseTabletPack>().Cast<Product>().ToList(),
                7 => products.OfType<TestStrip>().Cast<Product>().ToList(),
                _ => new List<Product>(), // Default empty list for unhandled cases
            };
        }

        private void GenerateList()
        {
            products = new List<Product>
            {
                new InsulinPump(Resource.Drawable.pump780g, 3600, "The Medtronic MiniMed 780G insulin pump is a hybrid closed-loop system that adjusts insulin delivery based on CGM data, offering automatic corrections and customizable glucose targets for improved diabetes management.", "Medtronic", "780G", "Minimed 780G", "AA"),
                new InsulinPump(Resource.Drawable.omnipod, 1400, "The Omnipod is a tubeless insulin pump offering discreet, continuous insulin delivery. It features a wearable pod controlled via a handheld device or app, simplifying diabetes management with flexibility and convenience.", "Insulet", "omnipod","DASH pod","built-in"),
                new InsulinSyringe(Resource.Drawable.syringe30g3ml, 0.9, "3ml 30 gauge 1/2 inch syrigne, sealed packaging for easy access.\n 30G 3ml 1/2 \"precision matching\" \n Not for professional medical use , only for industrial, or as home tools","Health inc", "3ml syringe", "30g",0.03),

            };
        }
    }
}

