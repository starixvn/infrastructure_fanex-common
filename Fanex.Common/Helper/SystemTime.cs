// --------------------------------------------------------------------------
// <copyright file="SystemTime.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.4.17">Created</change>
// </history>

namespace Fanex.Common.Helper
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The system class.
    /// </summary>
    public static class SystemTime
    {
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}