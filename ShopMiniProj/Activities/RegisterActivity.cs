using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using ShopMiniProj.Classes;

namespace ShopMiniProj.Activities
{
    [Activity(Label = "Register", Theme = "@style/AppTheme")]
    public class RegisterActivity : AppCompatActivity
    {
        private EditText _firstNameEditText, _lastNameEditText, _usernameEditText, _emailEditText, _passwordEditText, _confirmPasswordEditText;
        private Button _registerButton, _cancelButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);
            Init();
        }

        private void Init()
        {
            _firstNameEditText = FindViewById<EditText>(Resource.Id.edittext_firstname);
            _lastNameEditText = FindViewById<EditText>(Resource.Id.edittext_lastname);
            _usernameEditText = FindViewById<EditText>(Resource.Id.edittext_username);
            _emailEditText = FindViewById<EditText>(Resource.Id.edittext_email);
            _passwordEditText = FindViewById<EditText>(Resource.Id.edittext_password);
            _confirmPasswordEditText = FindViewById<EditText>(Resource.Id.edittext_confirmpassword);
            _registerButton = FindViewById<Button>(Resource.Id.button_register);
            _cancelButton = FindViewById<Button>(Resource.Id.button_cencel);

            _registerButton.Click += async (sender, e) => await RegisterUser();
            _cancelButton.Click += (sender, e) => Finish();
        }

        private async Task RegisterUser()
        {
            string firstName = _firstNameEditText.Text.Trim();
            string lastName = _lastNameEditText.Text.Trim();
            string username = _usernameEditText.Text.Trim();
            string email = _emailEditText.Text.Trim();
            string password = _passwordEditText.Text.Trim();
            string confirmPassword = _confirmPasswordEditText.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                Toast.MakeText(this, "Please fill in all fields.", ToastLength.Short).Show();
                return;
            }

            if (!Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
            {
                Toast.MakeText(this, "Please enter a valid email address.", ToastLength.Short).Show();
                return;
            }

            if (password != confirmPassword)
            {
                Toast.MakeText(this, "Passwords do not match.", ToastLength.Short).Show();
                return;
            }

            if (password.Length < 6)
            {
                Toast.MakeText(this, "Password must be at least 6 characters long.", ToastLength.Short).Show();
                return;
            }

            try
            {
                bool success = await UserInfo.Register(firstName, lastName, username, email, password);
                if (success)
                {
                    Toast.MakeText(this, "Registration successful!", ToastLength.Short).Show();
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Registration failed. Try again.", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
    }
}
