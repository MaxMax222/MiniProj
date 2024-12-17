
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace ShopMiniProj.Activities
{
    [Activity(Label = "AboutUsActivity", Theme = "@style/AppTheme")]
    public class AboutUsActivity : AppCompatActivity
    {
        Button contact_phone_button, contact_email_button, contact_sms_button;
        TextView about_txt;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.about_us);
            Init();
        }

        private void Init()
        {
            contact_phone_button = FindViewById<Button>(Resource.Id.button_contact_phone);
            contact_phone_button.Click += Contact_phone_button_Click;

            contact_email_button = FindViewById<Button>(Resource.Id.button_contact_email);
            contact_email_button.Click += Contact_email_button_Click;

            contact_sms_button = FindViewById<Button>(Resource.Id.button_contact_sms);
            contact_sms_button.Click += Contact_sms_button_Click;

            about_txt = FindViewById<TextView>(Resource.Id.aboutTxt);
            about_txt.Text = "Welcome to \"My Diabetes Supply\", your trusted partner in managing Type 1 diabetes. We understand that living with Type 1 diabetes can be challenging, which is why we're committed to providing high-quality, reliable, and affordable supplies to support your journey to better health and well-being. \n\n Our online store offers a wide range of essential diabetes management products, including insulin pumps, glucose monitors, test strips, syringes, infusion sets, and more. We carefully select products from top brands to ensure you have access to the best tools to monitor and manage your condition with confidence. \n\n At \"My Diabetes Supply\", we prioritize your needs and convenience. Our easy-to-navigate app allows you to browse, order, and receive your diabetes supplies directly at your doorstep, making managing your health simpler and more accessible than ever. We also offer fast, discreet shipping to ensure that you receive your products when you need them most. \n\n We are passionate about helping individuals with Type 1 diabetes lead healthy, fulfilling lives. Whether you're newly diagnosed or have been living with diabetes for years, we're here to provide the products, resources, and support you need every step of the way.  Thank you for choosing \"My Diabetes Supply\" —we're honored to be a part of your diabetes management journey.";
        }

        private void Contact_sms_button_Click(object sender, EventArgs e)
        {
            var smsIntent = new Intent(Intent.ActionView);
            smsIntent.SetData(Android.Net.Uri.Parse("sms:+1234567890"));  
            smsIntent.PutExtra("sms_body", "Hello, this is a test message.");

            StartActivity(smsIntent);
        }

        private void Contact_email_button_Click(object sender, EventArgs e)
        {
            var emailIntent = new Intent(Intent.ActionSend);
            emailIntent.SetType("message/rfc822");  // Specifies that it's an email intent
            emailIntent.PutExtra(Intent.ExtraEmail, new string[] { "example@example.com" });  // Recipient email
            emailIntent.PutExtra(Intent.ExtraSubject, "Test Email Subject");  // Email subject
            emailIntent.PutExtra(Intent.ExtraText, "Hello, this is a test email.");  // Email body

            StartActivity(Intent.CreateChooser(emailIntent, "Send Email"));
        }

        private void Contact_phone_button_Click(object sender, EventArgs e)
        {
            var dialIntent = new Intent(Intent.ActionDial, Android.Net.Uri.Parse("tel:+1234567890"));

            StartActivity(dialIntent);
        }
    }
}

