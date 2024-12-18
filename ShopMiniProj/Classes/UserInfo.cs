using System;
namespace ShopMiniProj.Classes
{
    public class UserInfo
    {
        public readonly string Name;
        public readonly string LastName;
        public readonly string Username;
        public readonly string Email;
        public readonly CardInfo Card;

        public UserInfo(string name, string lastName, string username, string email, CardInfo card)
        {
            Name = name;
            LastName = lastName;
            Username = username;
            Email = email;
            Card = card;
        }
    }
}

