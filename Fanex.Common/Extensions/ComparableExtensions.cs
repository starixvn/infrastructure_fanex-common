//<copyright file="ComparableExtensions.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.24">Created</change>
//</history>



namespace Fanex.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// Works on Comparables to check whether the checked value is between two other values
        /// . Check relaifciotn.net -> extension methods for more details.
        /// <example>if (someNumber.Between(3,7)) // Do stuff...</example>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool Between<T>(this T me, T lower, T upper) where T : IComparable<T>
        {
            return me.CompareTo(lower) >= 0 && me.CompareTo(upper) < 0;
        }

        /// <summary>
        /// Limits a value to a maximum
        /// <example>
        /// int testValue = 150;
        /// int limitedResult = testValue.Limit(100);
        /// </example>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static T Limit<T>(this T value, T maximum) where T : IComparable<T>
        {
            return value.CompareTo(maximum) < 1 ? value : maximum;
        }

        /// <summary>
        /// Returns a boolean value of true if the item being compared is less than the value of the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparable"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool LessThan<T>(this IComparable<T> comparable, T other)
        {
            return comparable.CompareTo(other) < 0;
        }

    }
}
