using System;

namespace ShopMiniProj.Classes
{
    public class UserInfoForOrder
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public CardInfo Card { get; private set; }
        public string ShippingAddress { get; private set; }
        public string ZipCode { get; private set; }

        public UserInfoForOrder(string name, string lastName, string username, string email, CardInfo card, string shippingAddress, string zipCode)
        {
            Name = name;
            LastName = lastName;
            Username = username;
            Email = email;
            Card = card;
            ShippingAddress = shippingAddress;
            ZipCode = zipCode;
        }

        public void UpdateShippingDetails(string shippingAddress, string zipCode)
        {
            ShippingAddress = shippingAddress;
            ZipCode = zipCode;
        }

        public void UpdateCard(CardInfo card)
        {
            Card = card;
        }

        public string GetOrderInfo()
        {
            return $"Name: {Name}, LastName: {LastName}, Username: {Username}, Email: {Email}, Card: {Card}, ShippingAddress: {ShippingAddress}, ZipCode: {ZipCode}";
        }
    }
}
