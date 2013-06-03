// <copyright file="MemoryCache.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Implement ICache by using ObjectCache of .Net Framework 4.0
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.23">Created</change>
//  <change who="Edward" date="2011.12.23">Implement</change>
//  <change who="Anders.le" date="2012.4.3">Fix bug of Set function add duplicated key</change>
//   <change who="Anders.Le" date="2012.9.13">Fix bug hashset is not threadsafe</change>
// </history>

namespace Fanex.Caching
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Runtime.Caching;
    using SRC = System.Runtime.Caching;

    /// <summary>
    /// Memory cache
    /// <see cref="http://msdn.microsoft.com/en-us/library/dd997357.aspx"/>
    /// <see cref="http://deanhume.com/Home/BlogPost/object-caching----net-4/37"/>
    /// </summary>
    internal class MemoryCache : ICache
    {
        #region ICache Members

        /// <summary>
        /// Gets the number of item in current cache instance
        /// </summary>
        public int Count
        {
            get
            {
                return (int)CacheInstance.GetCount();
            }
        }

        

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
        /// Determines whether cache contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            return CacheInstance.Contains(key);
        }

        /// <summary>
        /// Gets cache item by the specified key.
        /// </summary>
        /// <typeparam name="T">Type of cache item</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// cache item
        /// </returns>
        public T Get<T>(string key)
        {
            return this.Contains(key) ? (T)CacheInstance[key] : default(T);
        }

        /// <summary>
        /// Get cache item by specified key, and cast to type T
        /// if cast failed return out T
        /// </summary>
        /// <typeparam name="T">Type of cache item</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// true/false to indicate whether getting cache item and casting successfull or not
        /// </returns>
        public bool TryGet<T>(string key, out T value)
        {
            bool checkTryGet = this.Contains(key);
            if (checkTryGet)
            {
                value = (T)Get<T>(key);
            }
            else
            {
                value = default(T);
            }

            return checkTryGet;
        }

        /// <summary>
        /// Adds item with a specified key to cache, if the key already exist then update 
        /// </summary>
        /// <typeparam name="T"> Type of cache item</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheItemPolicy">The cache item policy.</param>
        public void Set<T>(string key, T value, CacheItemPolicy cacheItemPolicy)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }

            CacheItem cacheItem = new CacheItem(key, value);
            CacheInstance.Set(cacheItem, cacheItemPolicy);

            //because HashSet is not thread safe
            lock (syncRoot)
            {
                this.Keys.Add(key);
            }
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <typeparam name="T">Type of cache item</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="timeOut">The second time out, default is 120s</param>
        public void Set<T>(string key, T value, int timeOut = 120)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            this.Set<T>(key, value, DateTime.Now.AddSeconds(timeOut));
        }

        /// <summary>
        ///   <para>Let’s say, the cache item is created at 8:05:00 </para>
        ///   <para>and the lifetime is given to 5 seconds. </para>
        ///   <para>Then the lifetime of the cache item expires at 8:05:05.</para>
        /// </summary>
        /// <typeparam name="T">Type of cache item</typeparam>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="absoluteExpiration">DateTime AbsoluteExpiration</param>
        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = absoluteExpiration;
            this.Set<T>(key, value, cacheItemPolicy);
        }

        /// <summary>
        ///   <para>Let’s say, the cache item is created at 8:05:00 </para>
        ///   <para>and the lifetime is given to 5 seconds. </para>
        ///   <para>If an access to the cache item happen at 8:05:04, </para>
        ///   <para>the expiring time of the cache item will be updated to 8:05:09.</para>
        /// </summary>
        /// <typeparam name="T">Type of cache item</typeparam>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="slidingExpiration">Sliding Expiration</param>
        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.SlidingExpiration = slidingExpiration;

            this.Set<T>(key, value, cacheItemPolicy);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            this.Keys.Remove(key);            
            CacheInstance.Remove(key);           
        }

        /// <summary>
        /// Clears all
        /// </summary>
        public void Clear()
        {
            var keys = this.Keys.ToList<string>();

            while (this.Keys.Count > 0)
            {
                this.Remove(this.Keys.First());
            }
        }

        #endregion
        #region private functions
        /// <summary>
        /// Singleton object cache instance
        /// </summary>
        private static volatile ObjectCache objCache;

        /// <summary>
        /// use to double check create singleton instance
        /// </summary>
        private static object syncRoot = new object();             

        /// <summary>
        /// Gets an ObjectCache cache instance.
        /// </summary>
        private static ObjectCache CacheInstance
        {
            get
            {
                if (objCache == null)
                {
                    lock (syncRoot)
                    {
                        if (objCache == null)
                        {
                            objCache = SRC.MemoryCache.Default;
                        }
                    }
                }

                return objCache;
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            objCache = null;
            listKey = null;
        }

        #endregion
    }
}
