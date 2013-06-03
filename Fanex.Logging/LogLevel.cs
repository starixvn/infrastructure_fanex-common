// <copyright file="ILogger.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  LogLevel enumeration
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
// </history>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.Logging
{
    internal enum LogLevel
    {
        /// <summary>
        /// Debug enum
        /// </summary>
        Debug,
        /// <summary>
        /// Info enum
        /// </summary>
        Info,
        /// <summary>
        /// Warn enum
        /// </summary>
        Warn,
        /// <summary>
        /// Error enum
        /// </summary>
        Error,
        /// <summary>
        /// Critical enum
        /// </summary>
        Critical
    }
}
