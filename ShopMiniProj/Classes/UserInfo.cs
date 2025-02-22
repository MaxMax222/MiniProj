﻿using System;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using Android.Widget;
using Android.Content;
using Android.App;
using System.Reflection.Emit;
using System.Collections.Generic;
using Android.Runtime;
using Newtonsoft.Json;

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
        public string ShippingAddress { get; private set; }
        public string ZipCode { get; private set; }
        public List<OrderInfo> prev_orders { get; private set; }
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
        public void UpdateShippingDetails(string shippingAddress, string zipCode)
        {
            ShippingAddress = shippingAddress;
            ZipCode = zipCode;
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
                Toast.MakeText(Application.Context, Ex.Message, ToastLength.Long).Show();
                return false;
            }
            return true;
        }

        public bool SignOut()
        {
            try
            {
                FirebaseAuth.SignOut();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
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
                Toast.MakeText(Application.Context, Ex.Message, ToastLength.Long).Show();
                return false;
            }
            return true;
        }


        // Method to retrieve user information
        public async Task FetchUserData()
        {
            try
            {
                var docRef = database.Collection(COLLECTION_NAME).Document(FirebaseAuth.CurrentUser.Uid);
                var snapshot = await docRef.Get().AsAsync<DocumentSnapshot>();
                if (snapshot.Exists())
                {
                    var data = snapshot.Data;
                    if (data != null)
                    {
                        Name = data.ContainsKey("firstName") ? data["firstName"].ToString() : "no name";
                        LastName = data.ContainsKey("lastName") ? data["lastName"].ToString() : "no last name";
                        Email = data.ContainsKey("email") ? data["email"].ToString() : "no email";
                        Username = data.ContainsKey("username") ? data["username"].ToString() : "no username";
                        ShippingAddress = data.ContainsKey("shippingAdress") ? data["shippingAdress"].ToString() : "no shippingAdress";
                        ZipCode = data.ContainsKey("zipCode") ? data["zipCode"].ToString() : "no zipCode";
                    }
                }

                await FetchUserOrders();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }

        private async Task FetchUserOrders()
        {
            prev_orders = new List<OrderInfo>();
            try
            {
                var ordersRef = database.Collection("orders").Document(FirebaseAuth.CurrentUser.Uid).Collection("UserOrders");
                var snapshot = await ordersRef.Get().AsAsync<QuerySnapshot>();

                foreach (var doc in snapshot.Documents)
                {
                    var orderData = doc.Data;

                    //parse basic fields
                    var orderId = orderData["orderId"].ToString();
                    var totalPrice = Convert.ToDouble(orderData["totalPrice"].ToString());
                    var timeOfOrder = DateTime.Parse(orderData["timeOfOrder"].ToString());
                    var productsDict = new Dictionary<Product, int>();

                    //parse each product
                    var productsRef = doc.Reference.Collection("products");
                    var productsSnapshot = await productsRef.Get().AsAsync<QuerySnapshot>();
                    foreach (var product in productsSnapshot.Documents)
                    {
                        var productData = product.Data;
                        var productForDict = new Product
                           (
                           productId: int.Parse(productData["productID"].ToString()),
                           name: product.Id,
                           description: productData["description"].ToString(),
                           price: double.Parse(productData["price"].ToString()),
                           manufacturer: productData["manufacturer"].ToString()
                           );
                        productsDict.Add(productForDict, int.Parse(productData["quantity"].ToString()));
                    }
                    prev_orders.Add(new OrderInfo(productsDict, totalPrice, orderId, timeOfOrder));
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }
    }
}
    