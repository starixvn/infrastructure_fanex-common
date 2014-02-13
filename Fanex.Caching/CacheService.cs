// <copyright file="CacheService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// </summary>
//
// <history>
//  <change who="Edward" date="2011.12.27">Create</change>
//  <change who="Anders" date="2011.12.27">Add default function create instance</change>
//  <change who="Anders" date="2011.12.27">Correct create instance cache by type</change>
// <change who="Anders.le" date="2012.3.28">Refactor, and check correct source code follow StyleCop</change>
// </history>

namespace Fanex.Caching
{
    using System;

    /// <summary>
    /// Factory service to create cache instance
    /// which every type of cache, cache service just create only one instance.
    /// <see cref="http://msdn.microsoft.com/en-us/library/ff650316.aspx"/>
    /// </summary>
    public static class CacheService
    {
        /// <summary>
        /// Create cache instance by cache type.
        /// </summary>
        /// <param name="cacheType"><see cref="CacheType"/>The cache type.</param>
        /// <returns><see cref="ICache"/>The ICache.</returns>
        public static ICache CreateInstance(CacheType cacheType)
        {           
            switch (cacheType)
            {
                case CacheType.EntlibCache:
                    return new EntlibCache();                    
                case CacheType.MemCache:                    
                    return new MemCache();
                case CacheType.MemoryCache:           
                default:
                    return new MemoryCache();
            }           
        }

        /// <summary>
        /// Create memory cache instance.
        /// </summary>
        /// <returns><see cref="ICache"/>The ICache.</returns>
        public static ICache CreateInstance()
        {
            return CreateInstance(CacheType.MemoryCache);
        }

        /// <summary>
        /// Create cache instance by string ENUM type.
        /// Create MemoryCache as default.
        /// </summary>
        /// <param name="type"><see cref="CacheType"/>The cache type.</param>
        /// <returns><see cref="ICache"/>The ICache.</returns>
        public static ICache CreateInstance(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return CreateInstance();
            }

            CacheType cacheType = CacheType.MemoryCache;
            Enum.TryParse<CacheType>(type, out cacheType);
            return CreateInstance(cacheType);
        }

        /// <summary>
        /// Clear the cache instance, and all item in the cache.
        /// </summary>
        /// <param name="type"><see cref="CacheType"/>The cache type.</param>
        public static void Dispose(CacheType type)
        {
            ICache cachInstance = CreateInstance(type);
            cachInstance.Clear();
            cachInstance.Dispose();
        }
    }
}
