// <copyright file="EntlibCache.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.27">Created</change>
//  <change who="Edward" date="2011.12.27">Implement</change>
// </history>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Fanex.Caching
{
    internal class HttpRuntimeCache : ICache
    {
        //        private static volatile HttpRuntime _httpCache;

        private static object syncRoot = new object();

      
        #region ICache Members

        public int Count
        {
            get
            {
                return HttpRuntime.Cache.Count;
            }
        }

        public bool Contains(string key)
        {
            if (ReferenceEquals(HttpRuntime.Cache.Get(key), null))
            {
                return false;
            }
            return true;
        }

        public T Get<T>(string key)
        {
            if (Contains(key))
            {
                return (T)HttpRuntime.Cache.Get(key);
            }
            return default(T);
        }

        public bool TryGet<T>(string key, out T value)
        {
            bool contains = Contains(key);
            if (contains)
            {
                value = Get<T>(key);
            }
            else
            {
                value = default(T);
            }
            return contains;
        }

        private void ReplaceCache(string key)
        {
            if (Contains(key))
            {
                HttpRuntime.Cache.Remove(key);
            }
        }


        /// <summary>
        /// Only expiration when use Remove Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>       
        public void Set<T>(string key, T value)
        {
            ReplaceCache(key);
            HttpRuntime.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            ReplaceCache(key);
            HttpRuntime.Cache.Add(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            ReplaceCache(key);
            HttpRuntime.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, CacheItemPriority.High, null);
        }

        public void Remove(string key)
        {

            if (Contains(key))
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        public void Clear()
        {

            if (Count > 0)
            {
                HttpRuntime.Close();
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}
