// <copyright file="EntlibCache.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Implement ICache by using Enterprise Library Caching Application Block
//  Use fluent configuration API
//  <![CDATA[http://msdn.microsoft.com/en-us/library/ff664363%28v=pandp.50%29.aspx]]>
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.23">Created</change>
//  <change who="Edward" date="2011.12.26">Implement</change>
//   <change who="Anders.Le" date="2012.9.13">Fix bug - thread safe of HastSet.Add</change>
// </history>

namespace Fanex.Caching
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

    /// <summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/ff664753%28v=PandP.50%29.aspx"/>    
    /// Enterprise library cache adaptor
    /// </summary>
    internal class EntlibCache : ICache
    {
        private static volatile ICacheManager cacheManager;
        private static object syncRoot = new object();

        /// <summary>
        /// contains a list of keys of current cache instance
        /// </summary>
        private static volatile HashSet<string> listKey;

        /// <summary>
        /// Gets all keys in current cache instance
        /// </summary>
        public HashSet<string> Keys
        {
            get
            {
                if (listKey == null)
                {
                    lock (syncRoot)
                    {
                        if (listKey == null)
                        {
                            listKey = new HashSet<string>();
                        }
                    }
                }

                return listKey;
            }
        }

        /// <summary>
        /// Gets the cache manager.
        /// </summary>
        private static ICacheManager CacheManager
        {
            get
            {
                if (cacheManager == null)
                {
                    lock (syncRoot)
                    {
                        if (cacheManager == null)
                        {
                            cacheManager = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
                        }
                    }
                }

                return cacheManager;
            }
        }

        #region ICache Members

        /// <summary>
        /// Gets the number of item in current cache instance
        /// </summary>
        public int Count
        {
            get
            {
                return CacheManager.Count;
            }
        }

        /// <summary>
        /// Determines whether cache contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            return CacheManager.Contains(key);
        }

        /// <summary>
        /// Gets cached value by the specified key.
        /// </summary>
        /// <typeparam name="T">any .Net type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T instance</returns>
        public T Get<T>(string key)
        {
            return (T)CacheManager.GetData(key);
        }

        public bool TryGet<T>(string key, out T value)
        {
            bool contains = this.Contains(key);
            value = (T)CacheManager.GetData(key);
            return contains;
        }

        public void Set<T>(string key, T value, params ICacheItemExpiration[] expiration)
        {
            CacheManager.Add(key, value, CacheItemPriority.Normal, null, expiration);

            lock (syncRoot)
            {
                this.Keys.Add(key);
            }
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <typeparam name="T">any .Net type</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="timeOut">default is 120s</param>
        public void Set<T>(string key, T value, int timeOut = 120)
        {
            DateTime absoluteExpiration = DateTime.Now.AddSeconds(timeOut);
            Set<T>(key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            AbsoluteTime ansoluteTime = new AbsoluteTime(absoluteExpiration);
            Set<T>(key, value, new ICacheItemExpiration[] { ansoluteTime });
        }

        /// <summary>
        /// Set item type T to cache with sliding expiration
        /// </summary>
        /// <typeparam name="T">any .Net Type</typeparam>
        /// <param name="key">valid key</param>
        /// <param name="value">the value</param>
        /// <param name="slidingExpiration">sliding expiration</param>
        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            SlidingTime slidingTime = new SlidingTime(slidingExpiration);
            Set<T>(key, value, slidingTime);
        }

        public void SetFileDependency<T>(string key, T value, string filePath)
        {
            FileDependency fileDependency = new FileDependency(filePath);
            CacheManager.Add(key, value, CacheItemPriority.High, null, new ICacheItemExpiration[] { fileDependency });
        }

        /// <summary>
        /// Removes cache item by the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            if (this.Contains(key))
            {
                CacheManager.Remove(key);
                this.Keys.Remove(key);
            }
        }

        public void Clear()
        {
            CacheManager.Flush();
            this.Keys.Clear();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            cacheManager = null;
            listKey = null;
        }

        #endregion
    }
}
