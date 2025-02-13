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
using Java.Util;
using static Android.Content.ClipData;

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
            orderId = FirebaseAuth.CurrentUser.Uid + "/UserOrders/ " + DateTime.Now.ToString().Replace("/","-");
            User = UserInfo.GetInstance();
            CartItems = cart.GetCartItems();
            
        }

        // Method to convert cart items to data for firebase
        private HashMap CreateDataOfProducts()
        {
            HashMap data = new HashMap();
            foreach (KeyValuePair<Product, int> product in CartItems)
            {
                // Create a map for each product
                HashMap item = new HashMap();

                item.Put("price", product.Key.Price);
                item.Put( "quantity", product.Value);
                item.Put( "manufacturer", product.Key.Manufacturer);
                item.Put("description", product.Key.Description);
                item.Put("productID", product.Key.ProductId);
                item.Put( "totalPrice", product.Value * product.Key.Price);
                
                data.Put(product.Key.Name, item);
            }
            return data;
        }

        public async Task<bool> PlaceOrder()
        {
            HashMap data = new HashMap();
               data.Put("firstName", User.Name);
                data.Put( "lastName", User.LastName);
                data.Put( "email", User.Email);
                data.Put( "userName", User.Username);
                data.Put( "orderId" , orderId.Replace("/UserOrders/",""));
                data.Put( "cardInfo", GetCardInfo());
                data.Put( "shippingAdress", User.ShippingAddress);
                data.Put( "zipCode", User.ZipCode);
                data.Put( "totalPrice", Cart.GetInstance().CalculateTotal());
                data.Put( "timeOfOrder", DateTime.Now.ToString());
            try
            {
                
                var orderReference = database.Collection(COLLECTION_NAME).Document(orderId);
                await orderReference.Set(data);
                var products = CreateDataOfProducts();
                foreach (var product in products.KeySet())
                {
                    var productsReference = database.Collection(COLLECTION_NAME)
                        .Document(orderId)
                        .Collection("products")
                        .Document(product.ToString());
                    await productsReference.Set(products.Get((Java.Lang.Object)product));
                    
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return false;
            }
            return true;

        }

        private HashMap GetCardInfo()
        {
            HashMap cardData = new HashMap();
            cardData.Put("hashedNumber", User.Card.HashedCardNumber);
            cardData.Put("hashedExeperationDate", User.Card.HashedExpirationDate);
            cardData.Put("hashedCVV", User.Card.HashedCVV);
            return cardData;
        }
    }
}

