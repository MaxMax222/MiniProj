
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
	[Activity (Label = "RegisterActivity", Theme ="@style/AppTheme")]			
	public class RegisterActivity : AppCompatActivity
    {
		Button cencel_btn;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.register);

			Init();
		}

        private void Init()
        {
			cencel_btn = FindViewById<Button>(Resource.Id.button_cencel);
			cencel_btn.Click += (sender, e) => Finish();
        }
    }
}

