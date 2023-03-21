namespace Fanex.Common
{
    using System;

    public static class PasswordHash
    {
        const int SuggestedLength = 30;

        public static string Hash(string password)
        {
            var standardPassword = FillPasswordUpTo30Chars(password);

            var seed = ToSeedNumber(standardPassword);

            var hexString = ToHexString(seed);

            return ToHashString(hexString);
        }

        private static string FillPasswordUpTo30Chars(string password)
        {
            var remainLength = SuggestedLength - password.Length;

            for (var i = 1; i <= remainLength; i++)
            {
                password += Convert.ToChar(0x15);
            }

            return password;
        }

        private static double ToSeedNumber(string password)
        {
            var seed = 1.0;

            for (var i = 1; i <= SuggestedLength; i++)
            {
                var @char = password[i - 1];

                var temp = SuggestedLength + @char * i;
                seed *= temp;
            }

            return seed;
        }

        private static string ToHexString(double seed)
        {
            var seedString = seed.ToString("G15");
            var hexString = string.Empty;

            for (var i = 1; i <= seedString.Length; i++)
            {
                var length = seedString.Length - i < 2 
                    ? seedString.Length - i + 1 
                    : 3;
                var subString = seedString.Substring(i - 1, length);

                hexString += ToHexString(subString);
            }

            return hexString;
        }

        private static string ToHashString(string hexString)
        {
            var hash = string.Empty;
            for (var i = 20; i <= hexString.Length - 0x12; i += 2)
            {
                hash += hexString.Substring(i - 1, 1);
            }

            return hash;
        }

        private static string ToHexString(string value)
        {
            var number = string.Empty;

            for (var i = 1; i <= value.Length; i++)
            {
                number += (int)value[i - 1];
            }

            return Convert.ToInt32(number).ToString("X");
        }
    }
}