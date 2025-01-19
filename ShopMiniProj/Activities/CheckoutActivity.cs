
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
            first_name.Text = UserInfo.GetInstance().Name;

            last_name = FindViewById<EditText>(Resource.Id.edittext_lastname);
            last_name.Text = UserInfo.GetInstance().LastName;

            email = FindViewById<EditText>(Resource.Id.edittext_email);
            email.Text = UserInfo.GetInstance().Email;

            shipping_addres = FindViewById<EditText>(Resource.Id.edittext_shipping_adress);
            zip_code = FindViewById<EditText>(Resource.Id.edittext_zip_code);

            

            dialog_card = new Dialog(this);
            dialog_card.SetCanceledOnTouchOutside(true);
            dialog_card.SetContentView(Resource.Layout.add_card_dialog);

            CreateUserForOrder();

            cart = Cart.GetInstance();
            add_card = FindViewById<Button>(Resource.Id.button_add_card);
            add_card.Click += Add_card_Click;
            place_order = FindViewById<Button>(Resource.Id.button_place_order);
            place_order.Click += Place_order_Click;
        }

        private void Place_order_Click(object sender, EventArgs e)
        {
            
            if (ValidateUser())
            {
                order = new Order(user, cart);
                Toast.MakeText(this, $"thank for ordering! your order id is {order.orderId}", ToastLength.Long).Show();
                cart.ClearCart();
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                StartActivity(intent);
            }
        }
        private bool ValidateUser()
        {
            if (string.IsNullOrWhiteSpace(first_name.Text))
            {
                Toast.MakeText(this, "First name is required.", ToastLength.Long).Show();
                return false;
            }

            if (string.IsNullOrWhiteSpace(last_name.Text))
            {
                Toast.MakeText(this, "Last name is required.", ToastLength.Long).Show();
                return false;
            }

            if (string.IsNullOrWhiteSpace(email.Text))
            {
                Toast.MakeText(this, "Email is required.", ToastLength.Long).Show();
                return false;
            }

            if (string.IsNullOrWhiteSpace(shipping_addres.Text))
            {
                Toast.MakeText(this, "Shipping address is required.", ToastLength.Long).Show();
                return false;
            }

            if (string.IsNullOrWhiteSpace(zip_code.Text))
            {
                Toast.MakeText(this, "Zip code is required.", ToastLength.Long).Show();
                return false;
            }

            if (card == null)
            {
                Toast.MakeText(this, "Pauyment method is required.", ToastLength.Long).Show();
                return false;
            }

            return true;
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
                string cardNumberText = card_number.Text;
                string expirationDateText = expiration_date.Text;
                string CVVText = CVV.Text;

                if (ValidateCardInfo(cardNumberText, expirationDateText, CVVText))
                {
                    card = new CardInfo(cardNumberText, expirationDateText, CVVText);
                    user.UpdateCard(card);
                    dialog_card.Dismiss();
                    Toast.MakeText(this, "Card added successfully!", ToastLength.Long).Show();
                }
            };
            dialog_card.Show();
        }

        private bool ValidateCardInfo(string cardNumber, string expirationDate, string CVV)
        {
            // Validate card number (should be numeric and 13-19 digits long)
            if (string.IsNullOrWhiteSpace(cardNumber) || !cardNumber.All(char.IsDigit) || cardNumber.Length < 13 || cardNumber.Length > 19)
            {
                Toast.MakeText(this, "Invalid card number. Must be 13-19 digits.", ToastLength.Long).Show();
                return false;
            }

            // Validate expiration date (MM/YY format and not expired)
            if (string.IsNullOrWhiteSpace(expirationDate) || !Regex.IsMatch(expirationDate, @"^(0[1-9]|1[0-2])\/\d{2}$") || IsExpired(expirationDate))
            {
                Toast.MakeText(this, "Invalid or expired expiration date. Use MM/YY format.", ToastLength.Long).Show();
                return false;
            }

            // Validate CVV (should be numeric and 3-4 digits)
            if (string.IsNullOrWhiteSpace(CVV) || !CVV.All(char.IsDigit) || (CVV.Length != 3 && CVV.Length != 4))
            {
                Toast.MakeText(this, "Invalid CVV. Must be 3 or 4 digits.", ToastLength.Long).Show();
                return false;
            }

            return true;
        }

        private bool IsExpired(string expirationDate)
        {
            try
            {
                var parts = expirationDate.Split('/');
                int month = int.Parse(parts[0]);
                int year = int.Parse(parts[1]) + 2000;

                DateTime expiration = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                return DateTime.Now > expiration;
            }
            catch
            {
                return true;
            }
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

