//<copyright file="EnumerationExtensions.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.22">Created</change>
//</history>
namespace Fanex.Common.Extensions
{
    using System;

    public static class EnumerationExtensions
    {
        /// <summary>
        /// <![CDATA[http://stackoverflow.com/questions/276585/enumeration-extension-methods]]>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEnum"></param>
        /// <param name="strType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse<T>(this Enum theEnum, string strType,  out T result)
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
                    if (value.Equals(strTypeFixed,
                                    StringComparison.OrdinalIgnoreCase))
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
