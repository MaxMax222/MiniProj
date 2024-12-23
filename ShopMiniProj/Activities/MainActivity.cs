using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;
using Android.Content;

namespace ShopMiniProj.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Button products_btn, cart_btn, aboutUs_btn, prev_orders_btn;
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
            Intent products_intent = new Intent(this, typeof(ProductsActivity));
            products_btn = FindViewById<Button>(Resource.Id.button_products);
            products_btn.Click += (sender, e) => StartActivity(products_intent);

            //intent = new Intent(this, typeof(CartActivity));
            cart_btn = FindViewById<Button>(Resource.Id.button_cart);
            //products_btn.Click += (sender, e) => StartActivity(intent);

            Intent about_us_intent = new Intent(this, typeof(AboutUsActivity));
            aboutUs_btn = FindViewById<Button>(Resource.Id.button_about_us);
            aboutUs_btn.Click += (sender, e) => StartActivity(about_us_intent);

            prev_orders_btn = FindViewById<Button>(Resource.Id.button_previous_orders);
            //products_btn.Click += (sender, e) => StartActivity(intent);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
