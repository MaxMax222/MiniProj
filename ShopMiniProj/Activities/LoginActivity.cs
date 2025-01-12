using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using ShopMiniProj.Classes;
namespace ShopMiniProj.Activities
{
    [Activity(Label = "@string/loginGreeting", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        // UI elements
        private Button _loginButton, _registerButton;
        private EditText _usernameEditText, _passwordEditText;
        private CheckBox _rememberCheckBox;

        // SharedPreferences
        private ISharedPreferences _preferences;
        private ISharedPreferencesEditor _editor;

        private UserInfo user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the layout
            SetContentView(Resource.Layout.login);

            // Initialize UI elements
            Init();
        }

        private void Init()
        {
            _usernameEditText = FindViewById<EditText>(Resource.Id.edittext_username);
            _passwordEditText = FindViewById<EditText>(Resource.Id.edittext_password);
            _rememberCheckBox = FindViewById<CheckBox>(Resource.Id.checkRemeber);

            _loginButton = FindViewById<Button>(Resource.Id.button_login);
            _registerButton = FindViewById<Button>(Resource.Id.button_register);

            // Set up SharedPreferences
            _preferences = GetSharedPreferences("UserPreferences", FileCreationMode.Private);
            _editor = _preferences.Edit();

            // Load saved preferences if "Remember Me" is checked
            if (_preferences.GetBoolean("Remember", false))
            {
                _usernameEditText.Text = _preferences.GetString("Username", "");
                _passwordEditText.Text = _preferences.GetString("Password", "");
                _rememberCheckBox.Checked = true;
            }

            // Set button click events
            _loginButton.Click += LoginButton_Click;
            _registerButton.Click += (sender, e) =>
            {
                var registerIntent = new Intent(this, typeof(RegisterActivity));
                StartActivity(registerIntent);
            };
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Validate input fields
            string enteredUsername = _usernameEditText.Text.Trim();
            string enteredPassword = _passwordEditText.Text.Trim();

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                Toast.MakeText(this, "Please fill in both fields.", ToastLength.Long).Show();
                return;
            }

            // Check if the user exists
            if (CheckUserExists(enteredUsername, enteredPassword))
            {
                // Save preferences if "Remember Me" is checked
                if (_rememberCheckBox.Checked)
                {
                    SaveUserPreferences(enteredUsername, enteredPassword);
                }
                else
                {
                    _editor.PutBoolean("Remember", false);
                    _editor.Apply();
                }

                // Initialize the UserInfo singleton with the logged-in user's details
                var userInfo = UserInfo.GetInstance();
                string email = _preferences.GetString("Email", ""); // Assuming email was saved during registration
                userInfo.Initialize(
                    name: _preferences.GetString("FirstName", ""),
                    lastName: _preferences.GetString("LastName", ""),
                    username: enteredUsername,
                    email: email,
                    card: null
                );


                var mainIntent = new Intent(this, typeof(MainActivity));
                mainIntent.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                StartActivity(mainIntent);
            }
            else
            {
                Toast.MakeText(this, "Invalid username or password. Please try again.", ToastLength.Long).Show();
            }
        }

        private bool CheckUserExists(string enteredUsername, string enteredPassword)
        {
            // Retrieve stored username and password from SharedPreferences
            string storedUsername = _preferences.GetString("Username", null);
            string storedPassword = _preferences.GetString("Password", null);

            // Check if the entered credentials match the stored credentials
            return enteredUsername == storedUsername && enteredPassword == storedPassword;
        }

        private void SaveUserPreferences(string username, string password)
        {
            // Save user credentials to SharedPreferences
            _editor.PutString("Username", username);
            _editor.PutString("Password", password);
            _editor.PutBoolean("Remember", _rememberCheckBox.Checked);
            _editor.Apply();
        }
    }
}
