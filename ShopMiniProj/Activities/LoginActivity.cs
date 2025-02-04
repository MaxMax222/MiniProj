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
    [Activity(Label = "@string/loginGreeting", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        private Button _loginButton, _registerButton;
        private EditText _emailEditText, _passwordEditText;
        private CheckBox _rememberCheckBox;
        private UserInfo user;

        private ISharedPreferences _preferences;
        private ISharedPreferencesEditor _editor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
            Init();
        }

        private void Init()
        {
            _emailEditText = FindViewById<EditText>(Resource.Id.edittext_username);
            _passwordEditText = FindViewById<EditText>(Resource.Id.edittext_password);
            _rememberCheckBox = FindViewById<CheckBox>(Resource.Id.checkRemeber);
            _loginButton = FindViewById<Button>(Resource.Id.button_login);
            _registerButton = FindViewById<Button>(Resource.Id.button_register);

            _preferences = GetSharedPreferences("UserPreferences", FileCreationMode.Private);
            _editor = _preferences.Edit();

            LoadUserPreferences();

            user = UserInfo.GetInstance();

            _loginButton.Click += LoginButton_Click;
            _registerButton.Click += (sender, e) =>
            {
                StartActivity(new Intent(this, typeof(RegisterActivity)));
            };
        }

        private void LoadUserPreferences()
        {
            if (_preferences.GetBoolean("Remember", false))
            {
                _emailEditText.Text = _preferences.GetString("Email", "");
                _passwordEditText.Text = _preferences.GetString("Password", "");
                _rememberCheckBox.Checked = true;
            }
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            string enteredEmail = _emailEditText.Text.Trim();
            string enteredPassword = _passwordEditText.Text.Trim();

            if (string.IsNullOrEmpty(enteredEmail) || string.IsNullOrEmpty(enteredPassword))
            {
                Toast.MakeText(this, "Please fill in both fields.", ToastLength.Long).Show();
                return;
            }
            if (enteredPassword.Length < 6)
            {
                Toast.MakeText(this, "Password must contain at least 6 characters", ToastLength.Long).Show();
                return;
            }

            try
            {
                if (await UserInfo.Login(enteredEmail, enteredPassword))
                {
                    SaveUserPreferences(enteredEmail, enteredPassword);
                    NavigateToMain();
                }
                else
                {
                    Toast.MakeText(this, "Invalid username or password. Please try again.", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }

        private void SaveUserPreferences(string email, string password)
        {
            if (_rememberCheckBox.Checked)
            {
                _editor.PutString("Email", email);
                _editor.PutString("Password", password);
                _editor.PutBoolean("Remember", true);
            }
            else
            {
                _editor.Remove("Email");
                _editor.Remove("Password");
                _editor.PutBoolean("Remember", false);
            }
            _editor.Apply();
        }

        private void NavigateToMain()
        {
            var mainIntent = new Intent(this, typeof(MainActivity));
            mainIntent.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
            StartActivity(mainIntent);
        }
    }
}