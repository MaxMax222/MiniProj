using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace ShopMiniProj.Activities
{
    [Activity(Label = "RegisterActivity", Theme = "@style/AppTheme")]
    public class RegisterActivity : AppCompatActivity
    {
        // UI elements
        private EditText _firstNameEditText;
        private EditText _lastNameEditText;
        private EditText _usernameEditText;
        private EditText _emailEditText;
        private EditText _passwordEditText;
        private EditText _confirmPasswordEditText;
        private Button _registerButton;
        private Button _cancelButton;

        // SharedPreferences key
        private const string SharedPreferencesFile = "UserPreferences";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the layout
            SetContentView(Resource.Layout.register);

            // Initialize UI elements
            Init();
        }

        private void Init()
        {
            // Find views
            _firstNameEditText = FindViewById<EditText>(Resource.Id.edittext_firstname);
            _lastNameEditText = FindViewById<EditText>(Resource.Id.edittext_lastname);
            _usernameEditText = FindViewById<EditText>(Resource.Id.edittext_username);
            _emailEditText = FindViewById<EditText>(Resource.Id.edittext_email);
            _passwordEditText = FindViewById<EditText>(Resource.Id.edittext_password);
            _confirmPasswordEditText = FindViewById<EditText>(Resource.Id.edittext_confirmpassword);
            _registerButton = FindViewById<Button>(Resource.Id.button_register);
            _cancelButton = FindViewById<Button>(Resource.Id.button_cencel);

            // Set event handlers
            _registerButton.Click += RegisterButton_Click;
            _cancelButton.Click += (sender, e) => Finish(); // Close the activity
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            // Get input values
            string firstName = _firstNameEditText.Text.Trim();
            string lastName = _lastNameEditText.Text.Trim();
            string username = _usernameEditText.Text.Trim();
            string email = _emailEditText.Text.Trim();
            string password = _passwordEditText.Text.Trim();
            string confirmPassword = _confirmPasswordEditText.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                Toast.MakeText(this, "Please fill in all fields.", ToastLength.Short).Show();
                return;
            }

            if (!IsValidEmail(email))
            {
                Toast.MakeText(this, "Please enter a valid email address.", ToastLength.Short).Show();
                return;
            }

            if (password != confirmPassword)
            {
                Toast.MakeText(this, "Passwords do not match.", ToastLength.Short).Show();
                return;
            }

            // Save data to SharedPreferences
            SaveToSharedPreferences(firstName, lastName, username, email, password);

            // Display success message
            Toast.MakeText(this, "Registration successful!", ToastLength.Short).Show();

            // Close the activity
            Finish();
        }

        private void SaveToSharedPreferences(string firstName, string lastName, string username, string email, string password)
        {
            var sharedPreferences = GetSharedPreferences(SharedPreferencesFile, FileCreationMode.Private);
            var editor = sharedPreferences.Edit();

            // Save user data
            editor.PutString("FirstName", firstName);
            editor.PutString("LastName", lastName);
            editor.PutString("Username", username);
            editor.PutString("Email", email);
            editor.PutString("Password", password); // Note: Storing passwords in plain text is not secure. Use encryption or secure methods in production.

            // Commit changes
            editor.Apply();
        }

        private bool IsValidEmail(string email)
        {
            // Basic email validation
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }
    }
}
