using System;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using Android.Widget;
using Android.Content;
using Android.App;

namespace ShopMiniProj.Classes
{
    public class UserInfo
    {
        // Private static field to hold the single instance
        private static UserInfo _instance;

        // Public properties with private setters
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public CardInfo Card { get; private set; }

        //Firebase handlers
        public static FirebaseAuth FirebaseAuth { get; private set; }
        public static FirebaseFirestore database { get; private set; }
        public const string COLLECTION_NAME = "users";

        // Private constructor to prevent direct instantiation
        private UserInfo()
        {
            database = FirebaseHelper.GetFirestore();
            FirebaseAuth = FirebaseHelper.GetFirebaseAuthentication();
        }

        // Public method to provide global access to the instance
        public static UserInfo GetInstance()
        {
            // Lazy initialization: create the instance only when needed
            _instance ??= new UserInfo();

            return _instance;
        }

        // Method to initialize user information
        public void Initialize(string name, string lastName, string username, string email, CardInfo card)
        {
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Email) && Card == null)
            {
                Name = name;
                LastName = lastName;
                Username = username;
                Email = email;
                Card = card;
            }
        }

        // Method to retrieve user information
        public string GetUserInfo()
        {
            return $"Name: {Name}, LastName: {LastName}, Username: {Username}, Email: {Email}, Card: {Card}";
        }

        public void SetCard(CardInfo card)
        {
            Card = card;
        }

        public static async Task<bool> Login(string Email, string Password)
        {
            try
            {
                await FirebaseAuth.SignInWithEmailAndPassword(Email, Password);

            }
            catch (Exception Ex)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> Register(string firstName, string lastName, string username, string email, string password)
        {
            try
            {
                await FirebaseAuth.CreateUserWithEmailAndPassword(email, password);
            }
            catch (Exception Ex)
            {
                Toast.MakeText(Application.Context, Ex.Message, ToastLength.Long);
                return false;
            }
            try
            {
                HashMap newUser = new HashMap();
                newUser.Put("firstName", firstName);
                newUser.Put("lastName", lastName);
                newUser.Put("username", username);
                newUser.Put("email", email);

                var userReference = database.Collection(COLLECTION_NAME).Document(FirebaseAuth.CurrentUser.Uid);
                await userReference.Set(newUser);
            }
            catch (Exception Ex)
            {
                Toast.MakeText(Application.Context, Ex.Message, ToastLength.Long);
                return false;
            }
            return true;
        }
    }
}