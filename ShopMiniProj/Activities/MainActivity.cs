using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;
using Android.Content;
using ShopMiniProj.Classes;
using Android.Views;

namespace ShopMiniProj.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : MenuActivity
    {
        Button products_btn, cart_btn, aboutUs_btn, prev_orders_btn;
        Cart cart;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Init();
        }

        private void Init()
        {
            cart = Cart.GetInstance();
            Intent products_intent = new Intent(this, typeof(ProductsActivity));
            products_btn = FindViewById<Button>(Resource.Id.button_products);
            products_btn.Click += (sender, e) => StartActivity(products_intent);

            Intent cart_intent = new Intent(this, typeof(CartActivity));
            cart_btn = FindViewById<Button>(Resource.Id.button_cart);
            cart_btn.Click += (sender, e) => StartActivity(cart_intent);

            Intent about_us_intent = new Intent(this, typeof(AboutUsActivity));
            aboutUs_btn = FindViewById<Button>(Resource.Id.button_about_us);
            aboutUs_btn.Click += (sender, e) => StartActivity(about_us_intent);


            Intent previous_orders_intent = new Intent(this, typeof(PreviousOrdersActivity));
            prev_orders_btn = FindViewById<Button>(Resource.Id.button_previous_orders);
            prev_orders_btn.Click += (sender, e) => StartActivity(previous_orders_intent);

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            // Remove the "Back" menu item for specific activities
            if (this is MainActivity) 
            {
                menu.FindItem(Resource.Id.menu_back)?.SetVisible(false);
            }

            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
