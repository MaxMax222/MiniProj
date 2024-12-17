
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
		Button login_btn, register_btn;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.login);
			login_btn = FindViewById<Button>(Resource.Id.button_login);
			var intent_main = new Intent(this, typeof(MainActivity));
			login_btn.Click += (sender, e) => { StartActivity(intent_main); };

			register_btn = FindViewById<Button>(Resource.Id.button_register);
			var intent_register = new Intent(this, typeof(RegisterActivity));
			register_btn.Click += (sender, e) => { StartActivity(intent_register); };
		}
	}
}

