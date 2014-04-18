// --------------------------------------------------------------------------
// <copyright file="QueryExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.22">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Web;

    public static class QueryExtensions
    {
        /// <summary>
        /// Generic function get value from query string
        /// and automatically convert to default value of typeof(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static T Value<T>(string parameterName) where T : IConvertible
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("Cannot call Query<T> when HttpContext.Current is null");

            return Value(parameterName, default(T));
        }

        /// <summary>
        /// get value from
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Value<T>(string parameterName, T defaultValue) where T : IConvertible
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("Cannot call Query<T> when HttpContext.Current is null");

            HttpRequest request = HttpContext.Current.Request;

            string input;
            if (!string.IsNullOrEmpty(parameterName)
                && request[parameterName] != null
                && !string.IsNullOrEmpty(request[parameterName]))
            {
                input = request[parameterName];
            }
            else
            {
                return defaultValue;
            }

            T value;
            try
            {
                value = (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Unable to convert query parameter named '{0}' with value of '{1}' to type {2}", parameterName, input, typeof(T).FullName), ex);
            }
            return value;
        }
    }
}