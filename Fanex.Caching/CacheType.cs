// <copyright file="CacheType.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <history>
// <change who="anders.le" date="2012.4.3">Created</change>
// </history>

namespace Fanex.Caching
{
    /// <summary>
    /// ENTLIBCACHE = 1, MEMCACHE = 2, MEMORYCACHE = 3, SHAREDCACHE = 4, WEAKREFCACHE = 5.
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// The ENTLIB cache.
        /// </summary>
        EntlibCache = 1, 

        /// <summary>
        /// This is MEMCACHED type.
        /// </summary>
        MemCache = 2,

        /// <summary>
        /// The MEMORY cache.
        /// </summary>
        MemoryCache = 3        
    }
}
