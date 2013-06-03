// <copyright file="Cryptography.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Cryptography class provide method to encrypt and decrypt connection string
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.16">Created</change>
//  <change who="Anders" date="2011.12.16">Add some enhancement, and hash method</change>
//   <change who="Anders.Le" date="2012.8.27">Fix bug encrypt pwd</change>
// </history>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Microsoft.VisualBasic;

namespace Fanex.Security
{
    /// <summary>
    /// test
    /// </summary>
    public static class Cryptography
    {
        #region Const Params
        private const string Chars = "qwertyuiopasdfghjklzxcvbnm0123456789";
        private const string CharsNoZero = "qwertyuipasdfghjklzxcvbnm123456789";
        private const string Numbers = "0123456789";
        private static readonly DESCryptoServiceProvider CryptoServiceProvider = new DESCryptoServiceProvider();
        private static readonly byte[] RgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
        private static readonly byte[] RgbIv = Encoding.UTF8.GetBytes("&％#@?,:*!~^.");
        #endregion

        #region Public functions

        /// <summary>
        /// One-way encryption password
        /// </summary>
        /// <param name="codeStr"></param>
        /// <returns></returns>
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

        public static string CfsEnCrypt(string plainText)
        {
            var encryptedText = string.Empty;
            try
            {
                byte[] rgbIV = new byte[0];
                byte[] rgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
                rgbIV = Encoding.UTF8.GetBytes(cfsKey);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV),
                        CryptoStreamMode.Write))
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
        /// Decrypt a non-empty encrypted string
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
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

        public static string DeCryptConnectionString(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr))
                throw new ArgumentException(connStr);

            var decryptPwd = GetDecryptPwd(connStr, "=");
            var str = CfsDeCrypt(decryptPwd[1].Trim());
            return (decryptPwd[0].Trim() + str + ";");
        }
        #endregion

        #region private functions

        private const string cfsKey = "&％#@?,:*!~^.";
        private static string CfsCode(string nWord)
        {
            string number = string.Empty;
            for (int i = 1; i <= nWord.Length; i++)
            {
                number = number + Strings.Asc(Strings.Mid(nWord, i, 1));
            }
            return Conversion.Hex(number);
        }

        /// <summary>
        /// use using keyword to dispose object asap
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static MemoryStream CreateMemoryStream(byte[] buffer)
        {
            var stream = new MemoryStream();
            using (var stream2 = new CryptoStream(stream, CryptoServiceProvider.CreateDecryptor(RgbKey, RgbIv)
                , CryptoStreamMode.Write))
            {
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
            }

            return stream;
        }



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
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="noZeroO"></param>
        /// <returns></returns>
        private static string RandomString(int length, bool noZeroO = false)
        {
            string chars = noZeroO
                               ? Chars
                               : CharsNoZero;

            string rndStr = string.Empty;
            Random rnd = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
            int len = chars.Length;

            for (int i = 0; i < length; ++i)
            {
                rndStr += chars[rnd.Next() % len];
            }
            return rndStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns>a string of a sequence number</returns>
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
                return str;
            return str.Replace("\n", "\\n").Replace("\"", "\\\"").Replace("'", "\\'");
        }

        private const int DefaultBufferSaltLenght = 13;

        public static string CreateSalt(int size)
        {
            byte[] buff = null;
            //Generate a cryptographic random number.
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                //what the hell i did
                buff = new byte[size];
                rng.GetBytes(buff);
            }
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
        #endregion

		#region Hash algorithm for security code

		private static string[] lowerHexBytes;
		private static string[] LowerHexBytes
		{
			get
			{
				return (lowerHexBytes ?? (lowerHexBytes = new[] {
                "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0a", "0b", "0c", "0d", "0e", "0f",
                "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1a", "1b", "1c", "1d", "1e", "1f",
                "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2a", "2b", "2c", "2d", "2e", "2f",
                "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3a", "3b", "3c", "3d", "3e", "3f",
                "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4a", "4b", "4c", "4d", "4e", "4f",
                "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5a", "5b", "5c", "5d", "5e", "5f",
                "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6a", "6b", "6c", "6d", "6e", "6f",
                "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7a", "7b", "7c", "7d", "7e", "7f",
                "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8a", "8b", "8c", "8d", "8e", "8f",
                "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9a", "9b", "9c", "9d", "9e", "9f",
                "a0", "a1", "a2", "a3", "a4", "a5", "a6", "a7", "a8", "a9", "aa", "ab", "ac", "ad", "ae", "af",
                "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8", "b9", "ba", "bb", "bc", "bd", "be", "bf",
                "c0", "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "ca", "cb", "cc", "cd", "ce", "cf",
                "d0", "d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8", "d9", "da", "db", "dc", "dd", "de", "df",
                "e0", "e1", "e2", "e3", "e4", "e5", "e6", "e7", "e8", "e9", "ea", "eb", "ec", "ed", "ee", "ef",
                "f0", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "fa", "fb", "fc", "fd", "fe", "ff" }));
			}
		}

		private static HashAlgorithm GetHashProvider(string hashAlgorithm)
		{
			HashAlgorithm hash;
			// Hashing algorithm
			hashAlgorithm = hashAlgorithm.ToUpper();
			switch (hashAlgorithm)
			{
				case "SHA1":
					hash = new SHA1Managed();
					break;

				case "SHA256":
					hash = new SHA256Managed();
					break;

				case "SHA384":
					hash = new SHA384Managed();
					break;

				case "SHA512":
					hash = new SHA512Managed();
					break;

				default: // MD5
					hash = new MD5CryptoServiceProvider();
					break;
			}

			return hash;
		}

		private static string ToHexString(byte[] value)
		{
			int length = value.Length;
			char[] hex = new char[length * 2];

			int j = 0;
			for (int i = 0; i < length; i++)
			{
				string s = LowerHexBytes[value[i]];
				hex[j++] = s[0];
				hex[j++] = s[1];
			}

			return new string(hex);
		}

		/// <summary>
		/// Compute hash from plain text
		/// </summary>
		/// <param name="hashAlgorithm">Can one of these values: MD5, SHA1, SHA256, SHA384, SHA512</param>
		/// <param name="plainText"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static string ComputeHash(string hashAlgorithm, string plainText, string salt = null)
		{
			if (salt != null)
			{
				plainText += salt;
			}

			// Convert plain text into a byte array
			byte[] data = Encoding.UTF8.GetBytes(plainText);

			// Calculate the hash
			// Compute hash value of our plain text with appended salt.
			byte[] encrypt = GetHashProvider(hashAlgorithm).ComputeHash(data);

			return ToHexString(encrypt);
		}

		public static string RandomString(int size)
		{
			byte[] buffer = new byte[size];
			new RNGCryptoServiceProvider().GetBytes(buffer);

			return ToHexString(buffer);
		}

		#endregion
    }
}
