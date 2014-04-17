// --------------------------------------------------------------------------
// <copyright file="Cryptography.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Anders" date="2011.12.16">Created</change>
//   <change who="Anders" date="2011.12.16">Add some enhancement, and hash method</change>
//   <change who="Anders.Le" date="2012.8.27">Fix bug encrypt password</change>
// </history>

namespace Fanex.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Security;
    using Microsoft.VisualBasic;

    /// <summary>
    /// The cryptography class.
    /// </summary>
    public static class Cryptography
    {
        #region Const Params

        private const string Chars = "qwertyuiopasdfghjklzxcvbnm0123456789";
        private const string CharsNoZero = "qwertyuipasdfghjklzxcvbnm123456789";
        private const string Numbers = "0123456789";
        private const string CfsKey = "&％#@?,:*!~^.";
        private const int DefaultBufferSaltLenght = 13;
        private static readonly DESCryptoServiceProvider CryptoServiceProvider = new DESCryptoServiceProvider();
        private static readonly byte[] RgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
        private static readonly byte[] RgbIv = Encoding.UTF8.GetBytes("&％#@?,:*!~^.");

        #endregion

        #region Public functions

        /// <summary>
        /// Encode string value.
        /// </summary>
        /// <param name="codeStr">The code string.</param>
        /// <returns>Return encode string.</returns>
        public static string CfsEnCode(string codeStr)
        {
            int num = 30;
            int num2 = num - codeStr.Length;
            if (num2 > 1)
            {
                for (int m = 1; m <= num2; m++)
                {
                    codeStr = codeStr + Strings.Chr(0x15);
                }
            }

            double num3 = 1.0;
            for (int i = 1; i <= num; i++)
            {
                int num5 = num + (Strings.Asc(Strings.Mid(codeStr, i, 1)) * i);
                num3 *= num5;
            }

            codeStr = num3.ToString();
            string str = string.Empty;
            for (int j = 1; j <= codeStr.Length; j++)
            {
                str = str + CfsCode(Strings.Mid(codeStr, j, 3));
            }

            string str2 = string.Empty;
            for (int k = 20; k <= (str.ToString().Length - 0x12); k += 2)
            {
                str2 = str2 + Strings.Mid(str, k, 1);
            }

            return str2;
        }

        /// <summary>
        /// CFSs the encrypt.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>The encrypt string.</returns>
        public static string CfsEnCrypt(string plainText)
        {
            var encryptedText = string.Empty;
            try
            {
                byte[] rgbIV = new byte[0];
                byte[] rgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
                rgbIV = Encoding.UTF8.GetBytes(CfsKey);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        stream2.Write(bytes, 0, bytes.Length);
                        stream2.FlushFinalBlock();
                        encryptedText = Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            catch
            {
                throw;
            }

            return encryptedText;
        }

        /// <summary>
        /// Decrypt string.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <returns>The decrypt string.</returns>
        public static string CfsDeCrypt(string encryptedText)
        {
            var decryptedText = string.Empty;

            if (!string.IsNullOrWhiteSpace(encryptedText))
            {
                var buffer = Convert.FromBase64String(encryptedText);
                using (var stream = CreateMemoryStream(buffer))
                {
                    decryptedText = Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            return decryptedText;
        }

        /// <summary>
        /// Decrypt connection string.
        /// </summary>
        /// <param name="connStr">The connection string.</param>
        /// <returns>The decrypt connection string.</returns>
        /// <exception cref="System.ArgumentException">Throw exception if string is null or empty.</exception>
        public static string DeCryptConnectionString(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ArgumentException(connStr);
            }

            var decryptPwd = GetDecryptPwd(connStr, "=");
            var str = CfsDeCrypt(decryptPwd[1].Trim());

            return decryptPwd[0].Trim() + str + ";";
        }

        #endregion

        /// <summary>
        /// Creates the salt.
        /// </summary>
        /// <param name="size">The size of string.</param>
        /// <returns>The result string.</returns>
        public static string CreateSalt(int size)
        {
            byte[] buff = null;
            //// Generate a cryptographic random number.
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                //// What the hell i did
                buff = new byte[size];
                rng.GetBytes(buff);
            }

            //// Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public static string CreateSalt()
        {
            return CreateSalt(DefaultBufferSaltLenght);
        }

        public static string CreateHash(string content, string salt)
        {
            string saltAndPwd = string.Concat(content, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");

            return hashedPwd;
        }

        public static string CreateHash(string content)
        {
            string salt = CreateSalt();
            return CreateHash(content, salt);
        }

        #region private functions

        private static string CfsCode(string value)
        {
            string number = string.Empty;
            for (int i = 1; i <= value.Length; i++)
            {
                number = number + Strings.Asc(Strings.Mid(value, i, 1));
            }

            return Conversion.Hex(number);
        }

        private static MemoryStream CreateMemoryStream(byte[] buffer)
        {
            var stream = new MemoryStream();
            using (var stream2 = new CryptoStream(stream, CryptoServiceProvider.CreateDecryptor(RgbKey, RgbIv), CryptoStreamMode.Write))
            {
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
            }

            return stream;
        }

        /// <summary>
        /// Gets the decrypt password.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns>The array results.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        private static string[] GetDecryptPwd(string sourceString, string searchString)
        {
            int length = 1;
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4;

            for (int i = sourceString.Length - 1; i >= 0; i--)
            {
                str = str + sourceString.Substring(i, 1);
            }

            for (int j = searchString.Length - 1; j >= 0; j--)
            {
                str2 = str2 + searchString.Substring(j, 1);
            }

            if (str.Substring(1, 2) != "==")
            {
                if (str.Substring(1, 1) != "=")
                {
                    for (int n = 1; n <= str.Length; n++)
                    {
                        if (str.Substring(n, 1) == "=")
                        {
                            break;
                        }

                        length++;
                    }

                    str4 = str.Substring(0, length);
                    for (int num9 = str4.Length; num9 > 1; num9--)
                    {
                        str3 = str3 + str4.Substring(num9 - 1, 1);
                    }

                    return new[] { sourceString.Substring(0, sourceString.Length - length), str3 };
                }

                length++;
                for (int m = 2; m <= str.Length; m++)
                {
                    if (str.Substring(m, 1) == "=")
                    {
                        break;
                    }

                    length++;
                }
            }
            else
            {
                length += 2;
                for (int num4 = 3; num4 <= str.Length; num4++)
                {
                    if (str.Substring(num4, 1) == "=")
                    {
                        break;
                    }

                    length++;
                }

                str4 = str.Substring(0, length);
                for (int num5 = str4.Length; num5 > 1; num5--)
                {
                    str3 = str3 + str4.Substring(num5 - 1, 1);
                }

                return new[] { sourceString.Substring(0, sourceString.Length - length), str3 };
            }

            str4 = str.Substring(0, length);
            for (int k = str4.Length; k > 1; k--)
            {
                str3 = str3 + str4.Substring(k - 1, 1);
            }

            return new[] { sourceString.Substring(0, sourceString.Length - length), str3 };
        }

        /// <summary>
        /// Random the string number.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>The result.</returns>
        private static string RandomStringNumber(int length)
        {
            string rndStr = string.Empty;
            Random rnd = new Random();
            int len = Numbers.Length;

            for (int i = 0; i < length; ++i)
            {
                rndStr += Numbers[rnd.Next() % len];
            }

            return rndStr;
        }

        private static string RemoveSpecialChars(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return str.Replace("\n", "\\n").Replace("\"", "\\\"").Replace("'", "\\'");
        }

        /// <summary>
        /// Random the string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="hasZeroNumber">If set to <c>true</c> [has zero number].</param>
        /// <returns>The result.</returns>
        private static string RandomString(int length, bool hasZeroNumber = false)
        {
            string chars = hasZeroNumber ? Chars : CharsNoZero;

            string rndStr = string.Empty;
            Random rnd = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
            int len = chars.Length;

            for (int i = 0; i < length; ++i)
            {
                rndStr += chars[rnd.Next() % len];
            }

            return rndStr;
        }
        #endregion
    }
}
