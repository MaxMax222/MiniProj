
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
	[Activity (Label = "@string/loginGreeting", Theme = "@style/AppTheme", MainLauncher =true)]			
	public class LoginActivity : AppCompatActivity
    {
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.login);
			var register = FindViewById<Button>(Resource.Id.button_register);
			var intent = new Intent(this, typeof(RegisterActivity));
			register.Click += (sender, e) => StartActivity(intent);
		}
	}
}

