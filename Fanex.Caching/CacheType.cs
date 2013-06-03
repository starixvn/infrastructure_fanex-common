// <copyright file="CacheType.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <history>
// <change who="anders.le" date="2012.4.3">Created</change>
// </history>
namespace Fanex.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// EntlibCache=1, Memcache=2, MemoryCache=3,SharedCache=4, WeakRefCache=5
    /// </summary>
    public enum CacheType
    {
        EntlibCache = 1, 
        /// <summary>
        /// this is MemCached type
        /// </summary>
        MemCache = 2, 

        MemoryCache = 3        
    }
}
