// --------------------------------------------------------------------------
// <copyright file="DataReaderExtension.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.28">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Data;
#if NETSTANDARD
    using System.Data.SqlClient;
#else
    using Microsoft.Data.SqlClient;
#endif
    using System.Diagnostics;

    /// <summary>
    /// The DataReader extension.
    /// </summary>
    public static class DataReaderExtension
    {
        [DebuggerStepThrough]
        public static T GetValue<T>(this IDataReader reader, string name)
        {
            return (T)Convert.ChangeType(reader[name], typeof(T));
        }

        [DebuggerStepThrough]
        public static T GetValue<T>(this IDataReader reader, int index)
        {
            return (T)Convert.ChangeType(reader[index], typeof(T));
        }

        [DebuggerStepThrough]
        public static bool IsDBNull(this IDataReader reader, string name)
        {
            return reader.IsDBNull(reader.GetOrdinal(name));
        }

        /// <summary>
        /// Try get value.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <param name="output">The output.</param>
        /// <returns>Indicate whether the parse success.</returns>
        [DebuggerStepThrough]
        public static bool TryGetValue<T>(this IDataReader reader, string name, out T output)
        {
            try
            {
                output = reader.GetValue<T>(name);

                return true;
            }
            catch (Exception ex)
            {
                if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
                {
                    output = default(T);

                    return false;
                }

                throw;
            }
        }

        [DebuggerStepThrough]
        public static T To<T>(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
            {
                return GetValue<T>(reader, name);
            }

            return default(T);
        }

        [DebuggerStepThrough]
        public static string ToString(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
            {
                return reader[name].ToString();
            }

            return string.Empty;
        }

        [DebuggerStepThrough]
        public static bool ToBool(this IDataReader reader, string name)
        {
            return ToBool(reader, name, false);
        }

        /// <summary>
        /// To the Boolean.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <param name="defaultValue">If set to <c>true</c> [default value].</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static bool ToBool(this IDataReader reader, string name, bool defaultValue)
        {
            bool result = false;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = bool.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// To the Boolean null able.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static bool? ToBool2(this IDataReader reader, string name)
        {
            bool result = false;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = bool.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// To the integer.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static int ToInt(this IDataReader reader, string name)
        {
            int result;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = int.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <summary>
        /// To the short.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static short ToShort(this IDataReader reader, string name)
        {
            short result;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = short.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static byte ToByte(this IDataReader reader, string name)
        {
            byte result;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = byte.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static decimal ToDecimal(this IDataReader reader, string name)
        {
            decimal result;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = decimal.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <summary>
        /// To the long.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static long ToLong(this IDataReader reader, string name)
        {
            long result;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = long.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static double ToDouble(this IDataReader reader, string name)
        {
            double result;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = double.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <summary>
        /// To the float.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static float ToFloat(this IDataReader reader, string name)
        {
            float result;
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = float.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <summary>
        /// To the unique identifier.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static Guid ToGuid(this IDataReader reader, string name)
        {
            Guid result = new Guid();
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = Guid.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static DateTime ToDateTime(this IDataReader reader, string name)
        {
            DateTime result = new DateTime();
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = DateTime.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// To the date time null able.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static DateTime? ToDateTime2(this IDataReader reader, string name)
        {
            DateTime result = new DateTime();
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = DateTime.TryParse(reader[name].ToString(), out result);
                if (check)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static char ToChar(this IDataReader reader, string name)
        {
            char result = ' ';
            var check = false;

            if (reader[name] != DBNull.Value)
            {
                check = char.TryParse(reader[name].ToString(), out result);
            }

            return check ? result : ' ';
        }

        /// <summary>
        /// To the byte array.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="name">The column name.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static byte[] ToByteArray(this IDataReader reader, string name)
        {
            byte[] blob = null;
            if (reader[name] != DBNull.Value)
            {
                //// return Convert.ToInt32(reader[name]);
                //// reader.GetBytes(0,
                //// to do wire up the tobyeArray method
                try
                {
                    blob = new byte[reader.GetBytes(0, 0, null, 0, int.MaxValue)];
                    reader.GetBytes(0, 0, blob, 0, blob.Length);
                    //// reader.GetBytes[0, 0, blob, 0, blob.Length);
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Exception: " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }

            return blob;
        }
    }
}