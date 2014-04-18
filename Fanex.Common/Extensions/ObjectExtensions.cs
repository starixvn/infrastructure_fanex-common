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
    /// See performance test of Convert.ChangeType and convert to specific type.
    /// <see cref="http://stackoverflow.com/questions/4756375/what-is-the-major-differences-between-convert-changetype-or-convert-toint32"/>
    /// </summary>
    public static class ObjectExtensions
    {
        [DebuggerStepThrough]
        public static bool Is<T>(this object me) where T : class
        {
            return me is T;
        }

        [DebuggerStepThrough]
        public static bool IsNot<T>(this object item) where T : class
        {
            return !item.Is<T>();
        }

        [DebuggerStepThrough]
        public static T As<T>(this object item) where T : class
        {
            return item as T;
        }

        /// <summary>
        /// Checks if object is any of provided values separated by comma.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="s">The value.</param>
        /// <param name="values">The values.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static bool In<T>(this T s, params T[] values)
        {
            return values.Any(x => x.Equals(s));
        }

        [DebuggerStepThrough]
        public static bool IsNullOrDBNull(this object item)
        {
            return item == null || item.GetType() == typeof(DBNull);
        }

        /// <summary>
        /// If the object this method is called on is not null, runs the given function and returns the value.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="getValue">The get value.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static TResult IfNotNull<T, TResult>(this T target, Func<T, TResult> getValue)
        {
            if (target != null)
            {
                return getValue(target);
            }

            return default(TResult);
        }

        /// <summary>
        /// Map any object T to a delimited string and control how that string is formatted.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="func">The function.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static string ToDelimitedString<T>(this T obj, char delimiter, Func<T, System.Reflection.PropertyInfo, string> func)
        {
            if (obj == null || func == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            var props = obj.GetType().GetProperties();
            for (int p = 0; p < props.Length; p++)
            {
                builder.Append(func(obj, props[p]) + delimiter.ToString());
            }

            // Remove the last character, the last delimiter
            if (builder.Length > 0)
            {
                return builder.ToString().Remove(builder.ToString().Length - 1);
            }

            return null;
        }

        /// <summary>
        /// Equals method that lets you specify on what property or field value you want to compare an object on. Also compares the object types.
        /// Useful to use in an overridden Equals method of an object.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="obj1">The object 1.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static bool Equals<T, TResult>(this T obj, object obj1, Func<T, TResult> selector)
        {
            return obj1 is T && selector(obj).Equals(selector((T)obj1));
        }
    }
}