// <copyright file="SharedCache.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Implement ICache by using Shared Cache <see cref="http://www.sharedcache.com/cms/"/>
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.23">Created</change>
//  <change who="Edward" date="2011.12.26">Implement</change>
// </history>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedCache;
using SHARE = SharedCache.WinServiceCommon.Provider.Cache.IndexusDistributionCache;
namespace Fanex.Caching
{
    /// <summary>
    /// <see cref="http://www.sharedcache.com/cms/"/>
    /// </summary>
    [Obsolete]
    internal class SharedCache : ICache
    {
        private static object syncRoot = new object();

        #region ICache Members

        public int Count
        {
            get
            {
                return (int)SHARE.SharedCache.Count;
            }
        }

        public bool Contains(string key)
        {
            if (ReferenceEquals(null, SHARE.SharedCache.Get(key)))
            {
                return false;
            }
            return true;
        }

        public T Get<T>(string key)
        {
            try
            {
                return SHARE.SharedCache.Get<T>(key);
            }
            catch (Exception e)
            {
                throw e;
            }
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

        /// <summary>
        /// Only expiration when use Remove method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value, int secondTimeOut = 120)
        {
            DateTime absoluteExpiration = DateTime.Now.AddSeconds(secondTimeOut);
            Set<T>(key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            SHARE.SharedCache.Add(key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            SHARE.SharedCache.Add(key, value, new DateTime(slidingExpiration.Ticks));
        }

        public void Remove(string key)
        {
            if (Contains(key))
            {
                SHARE.SharedCache.Remove(key);
            }
        }

        public void Clear()
        {
            if (Count > 0)
            {
                SHARE.SharedCache.Clear();
            }
        }

        public HashSet<string> Keys
        {
            get { return SHARE.SharedCache.GetAllKeys(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion


    }
}
