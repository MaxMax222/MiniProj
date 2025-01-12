using System;

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

        // Private constructor to prevent direct instantiation
        private UserInfo() { }

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
    }
}
