// --------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.22">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The enumerable extension.
    /// </summary>
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Gets the first or default.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static T GetFirstOrDefault<T>(this IEnumerable<T> value)
        {
            if (!ReferenceEquals(value, null))
            {
                if (value.GetEnumerator().MoveNext())
                {
                    foreach (T item in value)
                    {
                        return item;
                    }
                }
            }

            return default(T);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> Concat<T>(IEnumerable<T> target, T element)
        {
            foreach (T e in target)
            {
                yield return e;
            }

            yield return element;
        }
    }
}