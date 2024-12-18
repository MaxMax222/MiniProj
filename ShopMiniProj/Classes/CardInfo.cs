using System;
using System.Security.Cryptography;
using System.Text;

namespace ShopMiniProj.Classes
{
    public class CardInfo
    {
        // The original values will not be stored, only the hashed versions
        public string HashedCardNumber { get; private set; }
        public string HashedExpirationDate { get; private set; }
        public string HashedCVV { get; private set; }

        // Constructor takes raw card information and hashes it
        public CardInfo(string cardNumber, string expirationDate, string cvv)
        {
            HashedCardNumber = HashString(cardNumber);
            HashedExpirationDate = HashString(expirationDate);
            HashedCVV = HashString(cvv);
        }

        // Utility method to hash a string using SHA-256
        private string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}

