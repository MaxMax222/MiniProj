using System;
namespace ShopMiniProj.Classes
{
    public class UserInfo
    {
        public string Name { get; }
        public string LastName { get; }
        public string Username { get; }
        public string Email { get; }
        public CardInfo Card { get; }

        public UserInfo(string name, string lastName, string username, string email, CardInfo card)
        {
            Name = name;
            LastName = lastName;
            Username = username;
            Email = email;
            Card = card;
        }

        public override string ToString()
        {
            return $"Name: {Name}, LastName: {LastName}, Username: {Username}, Email: {Email}, Card: {Card}";
        }
    }
}

