// <copyright file="Extensions.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <history>
//  <change who="anders.le" date="2012.4.3">Created</change>
// </history>

namespace Fanex.Caching
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Add an item to list, perform check duplicated item before adding.
        /// </summary>
        /// <param name="list">The collection.</param>
        /// <param name="key">The string key.</param>
        public static void Set(this IList<string> list, string key)
        {
            if (!ReferenceEquals(list, null) && !string.IsNullOrWhiteSpace(key))
            {
                if (!list.Contains(key))
                {
                    list.Add(key);                    
                }
            }
        }
    }
}
