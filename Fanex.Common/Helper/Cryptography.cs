

namespace  Fanex.Common.Helper
{
    using System;
    using System.Security.Cryptography;
    using System.Web.Security;
    public static class Cryptography
    {
        private const int DefaultBufferSaltLenght = 13;

        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            //what the hell i did
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Create salt with lengh = 10
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            return CreateSalt(DefaultBufferSaltLenght);
        }

        public static string CreateHash(string content, string salt)
        {
            string saltAndPwd = String.Concat(content, salt);
            string hashedPwd =
             FormsAuthentication.HashPasswordForStoringInConfigFile(
             saltAndPwd, "sha1");

            return hashedPwd;
        }

        /// <summary>
        /// Create hash string with default salt length = 13
        /// Hash medthod SHA1
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CreateHash(string content)
        {
            string salt = CreateSalt();
            return CreateHash(content, salt);
        }
    }
}
