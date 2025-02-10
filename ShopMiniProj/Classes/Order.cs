using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Gms.Extensions;
using Android.Runtime;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Newtonsoft.Json;

namespace ShopMiniProj.Classes
{

    public class Order
    {
        public string orderId { get; }
        private UserInfo User;
        private Dictionary<Product, int> CartItems;
         public static FirebaseAuth FirebaseAuth { get; private set; }
        public static FirebaseFirestore database { get; private set; }
        private const string COLLECTION_NAME = "orders";

        public Order(Cart cart)
        {
            database = FirebaseHelper.GetFirestore();
            FirebaseAuth = FirebaseHelper.GetFirebaseAuthentication();
            orderId = FirebaseAuth.CurrentUser.Uid + " " + DateTime.Now.ToString();
            User = UserInfo.GetInstance();
            CartItems = cart.GetCartItems();
            
        }

        // Method to convert cart items to data for firebase
        private JavaDictionary<string, Java.Lang.Object> CreateDataForFirebase()
        {
            JavaDictionary<string, Java.Lang.Object> data = new JavaDictionary<string, Java.Lang.Object>();
            foreach (KeyValuePair<Product, int> product in CartItems)
            {
                // Create a dictionary for each product
                JavaDictionary<string, Java.Lang.Object> item = new JavaDictionary<string, Java.Lang.Object>
                {
                    { "price", product.Key.Price },
                    { "quantity", product.Value },
                    { "totalPrice", product.Value * product.Key.Price }
                };
                data.Add(product.Key.Name, item);
            }
            return data;
        }

        public async Task<bool> PlaceOrder()
        {
            Dictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>()
            {   {"firstName", User.Name},
                {"lastName", User.LastName},
                {"email", User.Email },
                {"userName", User.Username},
                {"orderId" , orderId},
                {"orderInfo", CreateDataForFirebase()},
                {"cardInfo", GetCardInfo()},
                {"shippingAdress", User.ShippingAddress},
                {"zipCode", User.ZipCode},
                {"totalPrice", Cart.GetInstance().CalculateTotal()},
                {"timeOfOrder", DateTime.Now.ToString()}
            };

            try
            {
                var orderReference = database.Collection(COLLECTION_NAME).Document(orderId);
                await orderReference.Set(data);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long);
                return false;
            }
            return true;

        }

        private JavaDictionary<string, Java.Lang.Object> GetCardInfo()
        {
            return new JavaDictionary<string, Java.Lang.Object>()
            {
                {"cardNumber", User.Card.HashedCardNumber},
                {"experationDate", User.Card.HashedExpirationDate},
                {"cvv",User.Card.HashedCVV}

            };
        }
    }
}

