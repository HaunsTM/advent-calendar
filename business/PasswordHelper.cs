using System;
using System.Security.Cryptography;

namespace business
{
    public class PasswordHelper
    {
        private string _password ="";
        public PasswordHelper(string password)
        {
            _password = password;
        }

        private string PasswordHash(string password)
        {
            // Generate the hash, with an automatic 32 byte salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32);
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            //Return the salt and the hash
            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        public string PasswordHash()
        {
            return PasswordHash(_password);
        }

        private bool IsValidPassword(string password, string hashedPassword)
        {
            bool passwordRepresentsHashedPassword = false;

            string calculatedPasswordHash = this.PasswordHash(password);

            passwordRepresentsHashedPassword = string.Equals(calculatedPasswordHash, hashedPassword);

            return passwordRepresentsHashedPassword;
        }

        public bool IsValidPassword(string hashedPassword)
        {
            return IsValidPassword(_password, hashedPassword);
        }

    }
}
