
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

namespace ShopMiniProj.Activities
{
	[Activity (Label = "CheckoutActivity")]			
	public class CheckoutActivity : MenuActivity
	{
        EditText first_name, last_name, email, shipping_addres, zip_code;
        Button add_card, place_order;
        CardInfo card;
        Dialog dialog_card;
        Cart cart;
        UserInfoForOrder user;
        Order order;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.checkout_screen);
			Init();
		}

        private void Init()
        {
            first_name = FindViewById<EditText>(Resource.Id.edittext_firstname);
            first_name.Text = user.Name;
            last_name = FindViewById<EditText>(Resource.Id.edittext_lastname);
            last_name.Text = user.LastName;
            email = FindViewById<EditText>(Resource.Id.edittext_email);
            email.Text = user.Email;
            shipping_addres = FindViewById<EditText>(Resource.Id.edittext_shipping_adress);
            zip_code = FindViewById<EditText>(Resource.Id.edittext_zip_code);

            

            dialog_card = new Dialog(this);
            dialog_card.SetCanceledOnTouchOutside(true);
            dialog_card.SetContentView(Resource.Layout.add_card_dialog);

            
            cart = Cart.GetInstance();
            add_card = FindViewById<Button>(Resource.Id.button_add_card);
            add_card.Click += Add_card_Click;
            place_order = FindViewById<Button>(Resource.Id.button_place_order);
            place_order.Click += Place_order_Click;
        }

        private void Place_order_Click(object sender, EventArgs e)
        {
            CreateUserForOrder();
            order = new Order(user, cart);
            Toast.MakeText(this, $"thank for ordering! your order id is {order.orderId}", ToastLength.Long).Show();
            cart.ClearCart();
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void CreateUserForOrder()
        {
            string firstNameText = first_name.Text;
            string lastNameText = last_name.Text;
            string emailText = email.Text;
            string shippingAddressText = shipping_addres.Text;
            string zipCodeText = zip_code.Text;

            user = new UserInfoForOrder(firstNameText, lastNameText, UserInfo.GetInstance().Username, emailText, null, shippingAddressText, zipCodeText);

        }
            private void Add_card_Click(object sender, EventArgs e)
        {
            EditText card_number, expiration_date, CVV;
            Button add_card;
            card_number = dialog_card.FindViewById<EditText>(Resource.Id.edittxt_card_num);
            expiration_date = dialog_card.FindViewById<EditText>(Resource.Id.edittext_date);
            CVV = dialog_card.FindViewById<EditText>(Resource.Id.edittext_CVV);
            add_card = dialog_card.FindViewById<Button>(Resource.Id.button_add_card);
            add_card.Click += (sender, e) =>
            {
                CardInfo cardInfo = new CardInfo(card_number.Text, expiration_date.Text, CVV.Text);
                user.UpdateCard(card);
                dialog_card.Dismiss();

            };
            dialog_card.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            // Remove the "Cart" menu item for specific activities
            if (this is CheckoutActivity)
            {
                menu.FindItem(Resource.Id.menu_cart)?.SetVisible(false);
            }

            return true;
        }
    }
}

