// --------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.23">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// <see cref="http://extensionmethod.net/Result.aspx?text=type%3aSystem.Collections.Generic.ICollection%3CT%3E"/>
    /// </summary>
    public static class CollectionExtensions
    {
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return (collection == null) || (collection.Count == 0);
        }

        /// <summary>
        /// Removes items from a list based on a provided condition.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="list">The collection.</param>
        /// <param name="predicate">The predicate.</param>
        [DebuggerStepThrough]
        public static void Remove<T>(this ICollection<T> list, Func<T, bool> predicate)
        {
            var items = list.Where(predicate).ToList();

            foreach (var item in items)
            {
                list.Remove(item);
            }
        }

        /// <summary>
        /// Removes items from a collection based on the condition you provide.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="list">The collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The collection after remove.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> predicate)
        {
            var dict = new Dictionary<int, T>();

            foreach (var item in list)
            {
                if (!dict.ContainsKey(predicate(item)))
                {
                    dict.Add(predicate(item), item);
                }
            }

            return dict.Values.AsEnumerable();
        }

        /// <summary>
        /// Updates items from the collection using a modified version of this collection.
        /// Useful in MVVM scenarios needing cancellable edition and delayed persistence.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="originalCollection">The original collection.</param>
        /// <param name="updatedCollection">The updated collection.</param>
        /// <exception cref="System.InvalidOperationException">Cannot update a read-only collection.</exception>
        [DebuggerStepThrough]
        public static void UpdateCollection<T>(this ICollection<T> originalCollection, ICollection<T> updatedCollection)
        {
            if (originalCollection.IsReadOnly)
            {
                throw new InvalidOperationException("Cannot update a read-only collection.");
            }

            Collection<T> oldItems = new Collection<T>();
            Collection<T> newItems = new Collection<T>();

            foreach (var originalItem in originalCollection)
            {
                if (!updatedCollection.Contains(originalItem))
                {
                    oldItems.Add(originalItem);
                }
            }

            foreach (var updatedItem in updatedCollection)
            {
                if (!originalCollection.Contains(updatedItem))
                {
                    newItems.Add(updatedItem);
                }
            }

            foreach (var oldItem in oldItems)
            {
                originalCollection.Remove(oldItem);
            }

            foreach (var newItem in newItems)
            {
                originalCollection.Add(newItem);
            }
        }
    }
}