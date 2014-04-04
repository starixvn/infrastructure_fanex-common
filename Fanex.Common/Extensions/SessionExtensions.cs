//<copyright file="SessionExtensions.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.22">Created</change>
//</history>


namespace Fanex.Common.Extensions
{
    using System.Web.SessionState;

    public static class SessionExtensions
    {
        public delegate object UponCreate();


        public static T GetObject<T>(this HttpSessionState session,
           string key, UponCreate uponCreate = null) where T : class
        {
            if (null == session[key])
            {
                var item = uponCreate() as T;
                session[key] = item;
                return item;
            }
            return session[key] as T;
        }

        public static T GetValue<T>(this HttpSessionState session, string key,
            UponCreate uponCreate = null) where T : struct
        {
            if (null == session[key])
            {
                var item = uponCreate();
                session[key] = item;
                return (T)item;
            }
            return (T)session[key];
        }

        public static object GetSession(this HttpSessionState sessionState, string key)
        {

            return sessionState[key];

        }

        public static void AddSession(this HttpSessionState sessionState, string key, object value)
        {
            sessionState[key] = value;
        }

    }
}
