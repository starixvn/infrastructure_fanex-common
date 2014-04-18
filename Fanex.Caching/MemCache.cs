// <copyright file="MemCache.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <summary>
//  Implement ICache by using Memcached
//  use http://memcached.enyim.com/ as client lib for memcache
// <see cref="http://www.couchbase.com/products-and-services/memcached"/>
// </summary>
//
// <history>
// <change who="Anders" date="2011.12.23">Created</change>
// <change who="Edward" date="2011.12.26">Implement</change>
// <change who="Anders.le" date="2012.2.20">remove configuration</change>
// <change who="Anders.le" date="2012.3.28">Add Function List All Keys</change>
// <change who="Anders.le" date="2012.4.3">Fix bug Set item, not check duplicate key</change>
// </history>

namespace Fanex.Caching
{
    using System;
    using System.Collections.Generic;
    using Couchbase;
    using Enyim.Caching;
    using Enyim.Caching.Memcached;

    /// <summary>
    /// COUCHBASE - MEMCACHE implementation.<see cref="http://www.couchbase.com/develop/net/current"/>
    /// </summary>
    internal class MemCache : ICache
    {
        /// <summary>
        /// COUCHBASE cache client singleton instance.
        /// </summary>
        private static volatile CouchbaseClient couchbaseClient;

        /// <summary>
        /// Use to double check when creating singleton instance.
        /// </summary>
        private static object syncRoot = new object();

        /// <summary>
        /// Contains a list of keys of current cache instance.
        /// </summary>
        private static volatile HashSet<string> listKey;

        /// <summary>
        /// Gets the number of item in current cache instance.
        /// </summary>
        public int Count
        {
            get
            {
                return this.Keys.Count;
            }
        }

        /// <summary>
        /// Gets all keys in current cache instance.
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
        /// Gets the cache client.
        /// </summary>
        private static MemcachedClient MemCacheClient
        {
            get
            {
                if (couchbaseClient == null)
                {
                    lock (syncRoot)
                    {
                        if (couchbaseClient == null)
                        {
                            UseBucket();
                        }
                    }
                }

                return couchbaseClient;
            }
        }

        /// <summary>
        /// Create new instance COUCHBASECLIENT.
        /// <para>BUCKETNAME is null, use default bucket get from configuration.</para>
        /// </summary>
        /// <param name="bucketName">If bucket name is empty get default bucket name from configuration.</param>
        public static void UseBucket(string bucketName = "")
        {
            couchbaseClient = string.IsNullOrWhiteSpace(bucketName)
                        ? new CouchbaseClient()
                        : new CouchbaseClient(bucketName, string.Empty);
        }

        #region ICache Members

        /// <summary>
        /// Determines whether cache contains the specified key.
        /// </summary>
        /// <param name="key">The string key.</param>
        /// <returns>
        ///   <c>True</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            return !string.IsNullOrWhiteSpace(key) && MemCacheClient.Get(key) != null;
        }

        /// <summary>
        /// Gets cache item by the specified key.
        /// </summary>
        /// <typeparam name="T">Type of cache item.</typeparam>
        /// <param name="key">The string key.</param>
        /// <returns>
        /// The cache item.
        /// </returns>
        public T Get<T>(string key)
        {
            T tmp = MemCacheClient.Get<T>(key);
            return tmp;
        }

        /// <summary>
        /// Get cache item by specified key, and cast to type T
        /// if cast failed return out T.
        /// </summary>
        /// <typeparam name="T">Type of cache item.</typeparam>
        /// <param name="key">The string key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// True/false to indicate whether getting cache item and casting successful or not.
        /// </returns>
        public bool TryGet<T>(string key, out T value)
        {
            bool contains = this.Contains(key);
            value = contains ? Get<T>(key) : default(T);
            return contains;
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <typeparam name="T">Type of cache item.</typeparam>
        /// <param name="key">The string key.</param>
        /// <param name="value">The value.</param>
        /// <param name="timeOut">The second time out, default is 120s.</param>
        public void Set<T>(string key, T value, int timeOut = 120)
        {
            DateTime absoluteExpiration = DateTime.Now.AddSeconds(timeOut);
            Set<T>(key, value, absoluteExpiration);
        }

        /// <summary>
        ///   <para>Let’s say, the cache item is created at 8:05:00.</para>
        ///   <para>and the lifetime is given to 5 seconds.</para>
        ///   <para>Then the lifetime of the cache item expires at 8:05:05.</para>
        /// </summary>
        /// <typeparam name="T">Type of cache item.</typeparam>
        /// <param name="key">The string key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteExpiration">DateTime AbsoluteExpiration.</param>
        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }

            MemCacheClient.Store(StoreMode.Set, key, value, absoluteExpiration);

            lock (syncRoot)
            {
                this.Keys.Add(key);
            }
        }

        /// <summary>
        ///   <para>Let’s say, the cache item is created at 8:05:00.</para>
        ///   <para>and the lifetime is given to 5 seconds.</para>
        ///   <para>If an access to the cache item happen at 8:05:04.</para>
        ///   <para>the expiring time of the cache item will be updated to 8:05:09.</para>
        /// </summary>
        /// <typeparam name="T">Type of cache item.</typeparam>
        /// <param name="key">The string key.</param>
        /// <param name="value">The value.</param>
        /// <param name="slidingExpiration">Sliding Expiration.</param>
        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }

            MemCacheClient.Store(StoreMode.Set, key, value, slidingExpiration);

            lock (syncRoot)
            {
                this.Keys.Add(key);
            }
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The string key.</param>
        public void Remove(string key)
        {
            if (this.Keys.Count > 0)
            {
                MemCacheClient.Remove(key);
                this.Keys.Remove(key);
            }
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        public void Clear()
        {
            MemCacheClient.FlushAll();
            this.Keys.Clear();
        }

        #endregion ICache Members

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            MemCacheClient.Dispose();
            listKey = null;
        }

        #endregion IDisposable Members
    }
}