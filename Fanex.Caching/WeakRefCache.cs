// <copyright file="WeakRefCache.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Implement ICache by using weak references concept
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.23">Created</change>
//  <change who="Edward" date="2011.12.27">Implement</change>
// </history>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Fanex.Caching
{
    /// <summary>
    /// <see cref="http://en.wikipedia.org/wiki/Weak_reference"/>
    /// </summary>
    internal class WeakRefCache : ICache
    {
        private static volatile Dictionary<string, WeakReference> _cache;
        private static object syncRoot = new object();

        private static Dictionary<string, WeakReference> WeakCache
        {
            get
            {
                if (_cache == null)
                {
                    lock (syncRoot)
                    {
                        if (_cache == null)
                        {
                            _cache = new Dictionary<string, WeakReference>();
                        }
                    }
                }
                return _cache;
            }
        }

        #region ICache Members

        public int Count
        {
            get
            {
                return WeakCache.Count;
            }
        }

        public bool Contains(string key)
        {
            return WeakCache.ContainsKey(key);
        }

        public T Get<T>(string key)
        {
            return (T)WeakCache[key].Target;
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

        public void Set<T>(string key, T value)
        {
            if (Contains(key))
            {
                WeakCache[key] = new WeakReference(value, false);
            }
            else
            {
                WeakCache.Add(key, new WeakReference(value, false));
            }
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {

            Set<T>(key, value);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {

            Set<T>(key, value);
        }

        public void Remove(string key)
        {

            if (Contains(key))
            {
                WeakCache.Remove(key);
            }
        }

        public void Clear()
        {

            if (WeakCache.Count > 0)
            {
                WeakCache.Clear();
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {

            _cache = null;
        }

        #endregion

        #region ICache Members


        public IList<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public void Set<T>(string key, T value, int secondTimeOut = 120)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
