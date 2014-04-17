// --------------------------------------------------------------------------
// <copyright file="ComparableExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.24">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The comparable extension class.
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// Works on Comparable to check whether the checked value is between two other values
        /// . Check reflection.net -> extension methods for more details.
        /// <example>if (someNumber.Between(3,7)) // Do stuff...</example>
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="me">The value.</param>
        /// <param name="lower">The lower.</param>
        /// <param name="upper">The upper.</param>
        /// <returns>The Boolean value.</returns>
        [DebuggerStepThrough]
        public static bool Between<T>(this T me, T lower, T upper) where T : IComparable<T>
        {
            return me.CompareTo(lower) >= 0 && me.CompareTo(upper) < 0;
        }

        /// <summary>
        /// Limits a value to a maximum.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static T Limit<T>(this T value, T maximum) where T : IComparable<T>
        {
            return value.CompareTo(maximum) < 1 ? value : maximum;
        }

        /// <summary>
        /// Returns a Boolean value of true if the item being compared is less than the value of the parameter.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="comparable">The comparable.</param>
        /// <param name="other">The other.</param>
        /// <returns>The Boolean value.</returns>
        [DebuggerStepThrough]
        public static bool LessThan<T>(this IComparable<T> comparable, T other)
        {
            return comparable.CompareTo(other) < 0;
        }
    }
}
