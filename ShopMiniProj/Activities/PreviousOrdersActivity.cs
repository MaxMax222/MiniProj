
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
using ShopMiniProj.Classes;
using ShopMiniProj.Adapters;
namespace ShopMiniProj.Activities
{
	[Activity (Label = "PreviousOrdersActivity")]			
	public class PreviousOrdersActivity : MenuActivity
	{
		private List<OrderInfo> orders;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.previous_orders);

			orders = UserInfo.GetInstance().prev_orders;
			var list = FindViewById<ListView>(Resource.Id.orders_listview);
			list.Adapter = new oredersAdapter(orders, this);
		}
	}
}

