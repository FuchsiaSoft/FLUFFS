using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    partial class User
    {
        /// <summary>
        /// Checks the provided password against that stored for the user
        /// and returns true if it matches.
        /// </summary>
        /// <param name="password">The plaintext password for the user</param>
        /// <returns>true if password is correct</returns>
        public bool Authenticate (string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes(this.Salt);

            string hashedPassword = HashPasswordAndSalt(password, salt);

            return this.Hash == hashedPassword;
        }

        /// <summary>
        /// Generates a new crypto random salt of 32bytes.
        /// </summary>
        /// <returns>32 crypto random bytes</returns>
        private static byte[] GetNewSalt()
        {
            RandomNumberGenerator rng = RNGCryptoServiceProvider.Create();
            byte[] saltBuffer = new byte[32];

            rng.GetBytes(saltBuffer);

            return saltBuffer;
        }

        /// <summary>
        /// Returns a SHA256 of the provided plaintext password and salt data,
        /// in the format of hex numbers.
        /// </summary>
        /// <param name="password">the plaintext password to hash</param>
        /// <param name="salt">the salt to combined with the password</param>
        /// <returns></returns>
        private static string HashPasswordAndSalt(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] saltedPassword = passwordBytes.Concat(salt).ToArray();

            SHA256 sha256 = SHA256.Create();

            byte[] hashedData = sha256.ComputeHash(saltedPassword);

            StringBuilder builder = new StringBuilder();

            foreach (byte element in hashedData)
            {
                builder.Append(element.ToString("X2"));
            }

            return builder.ToString();
        }
    }
}
