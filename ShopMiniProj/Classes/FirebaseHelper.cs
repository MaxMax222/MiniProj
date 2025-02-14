using Android.App;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Xamarin.Essentials;
using static Java.Text.Normalizer;

namespace ShopMiniProj.Classes
{
    public static class FirebaseHelper
    {
        //Set project_id and api_key from firebase json file(downlowded from firebase console);
        const string PROJECT_ID = "miniprojdb";
        const string API_KEY = "AIzaSyBz4zRgTIYXK1zgb4coezzshB-9fxN42Qk";

        static FirebaseFirestore database;
        public static FirebaseFirestore GetFirestore() => database ?? FirebaseFirestore.GetInstance(RetriveApp());

        public static FirebaseAuth GetFirebaseAuthentication() => FirebaseAuth.Instance;

        private static FirebaseApp RetriveApp()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId(PROJECT_ID)
                    .SetApplicationId(PROJECT_ID)
                    .SetApiKey(API_KEY)
                    .SetDatabaseUrl($"https://{PROJECT_ID}.firebaseio.com")
                    .SetStorageBucket($"{PROJECT_ID}.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
            }
            return app;
        }
    }
}