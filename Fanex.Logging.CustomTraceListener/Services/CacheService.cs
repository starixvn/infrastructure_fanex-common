namespace Fanex.Logging.CustomTraceListener.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

    internal class CacheService : ICacheService
    {
        private static volatile ICacheManager cacheManager;
        private static object syncRoot = new object();

        /// <summary>
        /// Contains a list of keys of current cache instance.
        /// </summary>
        private static volatile HashSet<string> listKey;

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

        #region ICache Members

        /// <summary>
        /// Gets the number of item in current cache instance.
        /// </summary>
        public int Count
        {
            get
            {
                return CacheManager.Count;
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
                            try
                            {
                                cacheManager = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
                            }
                            catch (Exception ex)
                            {
                                LogToWindowsEvent(ex.Message);
                            }
                        }
                    }
                }

                return cacheManager;
            }
        }

        private static void LogToWindowsEvent(string message)
        {
            var logSource = "Fanex Custom Email Trace Listener";
            var logName = "Application";

            if (!EventLog.SourceExists(logSource))
            {
                EventLog.CreateEventSource(logSource, logName);
            }

            EventLog.WriteEntry(logSource, message, EventLogEntryType.Warning, 234);
        }

        /// <summary>
        /// Determines whether cache contains the specified key.
        /// </summary>
        /// <param name="key">The string key.</param>
        /// <returns>
        ///   <c>True</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            if (CacheManager != null)
            {
                return CacheManager.Contains(key);
            }

            return false;
        }

        /// <summary>
        /// Gets cached value by the specified key.
        /// </summary>
        /// <typeparam name="T">Any .Net type.</typeparam>
        /// <param name="key">The string key.</param>
        /// <returns>T instance.</returns>
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
        /// <typeparam name="T">Any .Net type.</typeparam>
        /// <param name="key">The string key.</param>
        /// <param name="value">The value.</param>
        /// <param name="timeOut">Default is 120s.</param>
        public void Set<T>(string key, T value, int timeOut = 120)
        {
            if (CacheManager != null)
            {
                DateTime absoluteExpiration = DateTime.Now.AddSeconds(timeOut);
                Set<T>(key, value, absoluteExpiration);
            }
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            AbsoluteTime ansoluteTime = new AbsoluteTime(absoluteExpiration);
            Set<T>(key, value, new ICacheItemExpiration[] { ansoluteTime });
        }

        /// <summary>
        /// Set item type T to cache with sliding expiration.
        /// </summary>
        /// <typeparam name="T">Any .Net type.</typeparam>
        /// <param name="key">Valid key.</param>
        /// <param name="value">The value.</param>
        /// <param name="slidingExpiration">Sliding expiration.</param>
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
        /// <param name="key">The string key.</param>
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

        #endregion ICache Members

        #region IDisposable Members

        public void Dispose()
        {
            cacheManager = null;
            listKey = null;
        }

        #endregion IDisposable Members
    }
}