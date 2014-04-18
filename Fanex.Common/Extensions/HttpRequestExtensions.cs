// --------------------------------------------------------------------------
// <copyright file="HttpRequestExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.22">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

    /// <summary>
    /// The HttpRequest extension.
    /// </summary>
    public static class HttpRequestExtensions
    {
        public static string GetIpAddress(this HttpRequest request)
        {
            return request.ServerVariables["remote_addr"];
        }

        public static string GetDomain(this HttpRequest request)
        {
            return request.ServerVariables["server_name"];
        }

        public static string GetBrowser(this HttpRequest request)
        {
            return request.ServerVariables["http_user_agent"];
        }

        public static string GetDnsLookUp(HttpRequest request)
        {
            return request.ServerVariables["remote_host"];
        }

        /// <summary>
        /// Values the specified parameter name.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The result.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot call Request when HttpContext.Current is null.
        /// </exception>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed. Suppression is OK here.")]
        public static T Value<T>(string parameterName, T defaultValue) where T : IConvertible
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("Cannot call Request<T> when HttpContext.Current is null");
            }

            HttpRequest request = HttpContext.Current.Request;
            string input;

            if (request[parameterName] != null && !string.IsNullOrEmpty(request[parameterName]))
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
                throw new InvalidOperationException(string.Format("Unable to convert query string parameter named '{0}' with value of '{1}' to type {2}", parameterName, input, typeof(T).FullName), ex);
            }

            return value;
        }
    }
}