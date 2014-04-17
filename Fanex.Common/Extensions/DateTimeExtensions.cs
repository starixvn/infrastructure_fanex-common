// --------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.4.17">Created</change>
// </history>

namespace Fanex.Common.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;

    public static class DateTimeExtensions
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        [DebuggerStepThrough]
        public static bool IsInRange(this DateTime currentDate, DateTime fromDate, DateTime toDate)
        {
            return (currentDate >= fromDate) && (currentDate <= toDate);
        }

        /// <summary>
        /// <see cref="http://geekswithblogs.net/thibbard/archive/2008/06/26/c-datetime-extension-method.aspx"/>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ToShortDateEqualLengthString(this DateTime dt)
        {
            var rv = new StringBuilder();
            if (dt.Month.ToString().Length == 1)
            {
                rv.Append("0");
            }
            rv.Append(dt.Month.ToString());
            rv.Append("/");
            if (dt.Day.ToString().Length == 1)
            {
                rv.Append("0");
            }
            rv.Append(dt.Day.ToString());
            rv.Append("/");
            rv.Append(dt.Year.ToString());

            return rv.ToString();
        }

        [DebuggerStepThrough]
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        [DebuggerStepThrough]
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// This is extension method for format the data time into the string with pattern specific and current culture
        /// <example>
        /// var shortDateResult = DateTime.Now.ToStringFormat(() => DateTimeFormat.ShortDate);
        /// var fullLongDateTimeResult = DateTime.Now.ToStringFormat(() => DateTimeFormat.FullLongDateTime);
        /// var rfc1123Result = DateTime.Now.ToStringFormat(() => DateTimeFormat.Rfc1123LowerCase);
        /// </example>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ToStringFormat(this DateTime source, Expression<Func<DateTimeFormat>> dateTimeFormat)
        {
            var dateTimeFormatCompiled = dateTimeFormat.Compile().Invoke();

            var dateTimeStringFormat = StringEnum.GetStringValue(dateTimeFormatCompiled);

            var currentCulture = Thread.CurrentThread.CurrentCulture;

            return source.ToString(dateTimeStringFormat, currentCulture);
        }

        /// <summary>
        /// Get the elapsed time since the input DateTime
        /// </summary>
        /// <param name="input">Input DateTime</param>
        /// <returns>Returns a TimeSpan value with the elapsed time since the input DateTime</returns>
        /// <example>
        /// TimeSpan elapsed = dtStart.Elapsed();
        /// </example>
        /// <seealso cref="ElapsedSeconds()"/>
        [DebuggerStepThrough]
        public static TimeSpan Elapsed(this DateTime input)
        {
            return DateTime.Now.Subtract(input);
        }

        public static string LengthOfTime(this DateTime date)
        {
            TimeSpan lengthOfTime = DateTime.Now.Subtract(date);
            if (lengthOfTime.Minutes == 0)
                return lengthOfTime.Seconds.ToString() + "s";
            else if (lengthOfTime.Hours == 0)
                return lengthOfTime.Minutes.ToString() + "m";
            else if (lengthOfTime.Days == 0)
                return lengthOfTime.Hours.ToString() + "h";
            else
                return lengthOfTime.Days.ToString() + "d";
        }
    }


    public enum DateTimeFormat
    {
        [StringValue("unknown")]
        UnknownSpecific = 0,
        [StringValue("d")]
        ShortDate = 1,
        [StringValue("D")]
        LongDate = 2,
        [StringValue("t")]
        ShortTime = 3,
        [StringValue("T")]
        LongTime = 4,
        [StringValue("f")]
        FullShortDateTime = 5,
        [StringValue("F")]
        FullLongDateTime = 6,
        [StringValue("g")]
        GeneralShortDateTime = 7,
        [StringValue("G")]
        GeneralLongDateTime = 8,
        [StringValue("m")]
        MonthDayLowerCase = 9,
        [StringValue("M")]
        MonthDayUpperCase = 10,
        [StringValue("r")]
        Rfc1123LowerCase = 11,
        [StringValue("R")]
        Rfc1123UpperCase = 12,
        [StringValue("s")]
        SortableDateTimeIso8601 = 13,
        [StringValue("U")]
        UniversalSortableDateTime = 14,
        [StringValue("y")]
        YearMonthLowerCase = 15,
        [StringValue("Y")]
        YearMonthUpperCase = 16
    }
}