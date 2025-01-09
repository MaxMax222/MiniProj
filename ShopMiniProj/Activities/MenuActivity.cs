
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

namespace ShopMiniProj.Activities
{
	public abstract class MenuActivity : AppCompatActivity
	{
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_cart:
                    // Navigate to CartActivity
                    Intent intentCart = new Intent(this, typeof(CartActivity));
                    StartActivity(intentCart);
                    return true;

                case Resource.Id.menu_back:
                    // Return to the previous page
                    Finish();
                        return true;
                case Resource.Id.menu_logout:
                    var intentLogOut = new Intent(this, typeof(LoginActivity));
                    intentLogOut.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                    StartActivity(intentLogOut);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}

