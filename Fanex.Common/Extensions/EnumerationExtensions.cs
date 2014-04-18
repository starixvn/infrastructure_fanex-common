// --------------------------------------------------------------------------
// <copyright file="EnumerationExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="anders.le" date="2012.2.22">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The enumeration extension.
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// The try parse.<see cref="http://stackoverflow.com/questions/276585/enumeration-extension-methods"/>
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="theEnum">The enumeration.</param>
        /// <param name="strType">Type of the string.</param>
        /// <param name="result">The result.</param>
        /// <returns>The result.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public static bool TryParse<T>(this Enum theEnum, string strType, out T result)
        {
            string strTypeFixed = strType.Replace(' ', '_');
            if (Enum.IsDefined(typeof(T), strTypeFixed))
            {
                result = (T)Enum.Parse(typeof(T), strTypeFixed, true);

                return true;
            }
            else
            {
                foreach (string value in Enum.GetNames(typeof(T)))
                {
                    if (value.Equals(strTypeFixed, StringComparison.OrdinalIgnoreCase))
                    {
                        result = (T)Enum.Parse(typeof(T), value);

                        return true;
                    }
                }

                result = default(T);

                return false;
            }
        }
    }
}