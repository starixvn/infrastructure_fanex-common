// --------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.22">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// see perfomance test of Convert.ChangeType and convert to specific type
    /// http://stackoverflow.com/questions/4756375/what-is-the-major-differences-between-convert-changetype-or-convert-toint32
    /// </summary>
    public static class ObjectExtensions
    {
        [DebuggerStepThrough()]
        public static bool Is<T>(this object me) where T : class
        {
            return me is T;
        }

        [DebuggerStepThrough()]
        public static bool IsNot<T>(this object item) where T : class
        {
            return !(item.Is<T>());
        }

        [DebuggerStepThrough()]
        public static T As<T>(this object item) where T : class
        {
            return item as T;
        }

        /// <summary>
        /// Checks if object is any of provided values separated by comma
        /// <example>
        /// myGender.In("m", "f", "male", "female");
        /// </example>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        [DebuggerStepThrough()]
        public static bool In<T>(this T s, params T[] values)
        {
            return values.Any(x => x.Equals(s));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerStepThrough()]
        public static bool IsNullOrDBNull(this object item)
        {
            return (item == null || item.GetType() == typeof(DBNull));
        }

        /// <summary>
        /// if the object this method is called on is not null, runs the given function and returns the value.
        /// if the object is null, returns default(TResult)
        /// <example>
        /// var username = HttpContext.Current.IfNotNull( ctx => ctx.User.IfNotNull( user => user.Identity.IfNotNull( iden => iden.Name )
        /// </example>
        /// </summary>
        [DebuggerStepThrough()]
        public static TResult IfNotNull<T, TResult>(this T target, Func<T, TResult> getValue)
        {
            if (target != null)
                return getValue(target);

            return default(TResult);
        }

        /// <summary>
        /// Map any object T to a delimited string and control how that string is formatted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="delimiter"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        [DebuggerStepThrough()]
        public static string ToDelimitedString<T>(this T obj, char delimiter, Func<T, System.Reflection.PropertyInfo, string> func)
        {
            if (obj == null || func == null)
                return null;

            var builder = new StringBuilder();
            var props = obj.GetType().GetProperties();
            for (int p = 0; p < props.Length; p++)
                builder.Append(func(obj, props[p]) + delimiter.ToString());

            // Remove the last character, the last delimiter
            if (builder.Length > 0)
                return builder.ToString().Remove(builder.ToString().Length - 1);

            return null;
        }

        /// <summary>
        /// Equals method that lets you specify on what property
        /// or field value you want to compare an object on. Also compares the object types.
        /// Useful to use in an overridden Equals method of an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <param name="obj1"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        [DebuggerStepThrough()]
        public static bool Equals<T, TResult>(this T obj, object obj1, Func<T, TResult> selector)
        {
            return obj1 is T && selector(obj).Equals(selector((T)obj1));
        }
    }
}