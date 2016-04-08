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

    public enum DateTimeFormat
    {
        /// <summary>
        /// The unknown specific.
        /// </summary>
        [StringValue("unknown")]
        UnknownSpecific = 0,

        /// <summary>
        /// The short date.
        /// </summary>
        [StringValue("d")]
        ShortDate = 1,

        /// <summary>
        /// The long date.
        /// </summary>
        [StringValue("D")]
        LongDate = 2,

        /// <summary>
        /// The short time.
        /// </summary>
        [StringValue("t")]
        ShortTime = 3,

        /// <summary>
        /// The long time.
        /// </summary>
        [StringValue("T")]
        LongTime = 4,

        /// <summary>
        /// The full short date time.
        /// </summary>
        [StringValue("f")]
        FullShortDateTime = 5,

        /// <summary>
        /// The full long date time.
        /// </summary>
        [StringValue("F")]
        FullLongDateTime = 6,

        /// <summary>
        /// The general short date time.
        /// </summary>
        [StringValue("g")]
        GeneralShortDateTime = 7,

        /// <summary>
        /// The general long date time.
        /// </summary>
        [StringValue("G")]
        GeneralLongDateTime = 8,

        /// <summary>
        /// The month day lower case.
        /// </summary>
        [StringValue("m")]
        MonthDayLowerCase = 9,

        /// <summary>
        /// The month day upper case.
        /// </summary>
        [StringValue("M")]
        MonthDayUpperCase = 10,

        /// <summary>
        /// The RFC1123 lower case.
        /// </summary>
        [StringValue("r")]
        Rfc1123LowerCase = 11,

        /// <summary>
        /// The RFC1123 upper case.
        /// </summary>
        [StringValue("R")]
        Rfc1123UpperCase = 12,

        /// <summary>
        /// The sortable date time ISO8601.
        /// </summary>
        [StringValue("s")]
        SortableDateTimeIso8601 = 13,

        /// <summary>
        /// The universal sortable date time.
        /// </summary>
        [StringValue("U")]
        UniversalSortableDateTime = 14,

        /// <summary>
        /// The year month lower case.
        /// </summary>
        [StringValue("y")]
        YearMonthLowerCase = 15,

        /// <summary>
        /// The year month upper case.
        /// </summary>
        [StringValue("Y")]
        YearMonthUpperCase = 16
    }

    /// <summary>
    /// The date and time extension class.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        public const string ShortDateFormat = "MM/dd/yyyy";
        public const string FullDateFormat = "MM/dd/yyyy hh:mm:ss tt";

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
        /// To the short date equal length string./// <see cref="http://geekswithblogs.net/thibbard/archive/2008/06/26/c-datetime-extension-method.aspx"/>
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static string ToShortDateEqualLengthString(this DateTime dateTime)
        {
            var rv = new StringBuilder();
            if (dateTime.Month.ToString().Length == 1)
            {
                rv.Append("0");
            }

            rv.Append(dateTime.Month.ToString());
            rv.Append("/");
            if (dateTime.Day.ToString().Length == 1)
            {
                rv.Append("0");
            }

            rv.Append(dateTime.Day.ToString());
            rv.Append("/");
            rv.Append(dateTime.Year.ToString());

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
        /// This is extension method for format the data time into the string with pattern specific and current culture.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="dateTimeFormat">The date time format.</param>
        /// <returns>The result.</returns>
        [DebuggerStepThrough]
        public static string ToStringFormat(this DateTime source, Expression<Func<DateTimeFormat>> dateTimeFormat)
        {
            var dateTimeFormatCompiled = dateTimeFormat.Compile().Invoke();

            var dateTimeStringFormat = StringEnum.GetStringValue(dateTimeFormatCompiled);

            var currentCulture = Thread.CurrentThread.CurrentCulture;

            return source.ToString(dateTimeStringFormat, currentCulture);
        }

        /// <summary>
        /// Get the elapsed time since the input DateTime.
        /// </summary>
        /// <param name="input">Input DateTime.</param>
        /// <returns>Returns a TimeSpan value with the elapsed time since the input DateTime.</returns>
        /// <seealso cref="ElapsedSeconds()"/>
        [DebuggerStepThrough]
        public static TimeSpan Elapsed(this DateTime input)
        {
            return DateTime.Now.Subtract(input);
        }

        /// <summary>
        /// Length the of time.
        /// </summary>
        /// <param name="date">The date and time.</param>
        /// <returns>The result.</returns>
        public static string LengthOfTime(this DateTime date)
        {
            TimeSpan lengthOfTime = DateTime.Now.Subtract(date);
            if (lengthOfTime.Minutes == 0)
            {
                return lengthOfTime.Seconds.ToString() + "s";
            }
            else if (lengthOfTime.Hours == 0)
            {
                return lengthOfTime.Minutes.ToString() + "m";
            }
            else if (lengthOfTime.Days == 0)
            {
                return lengthOfTime.Hours.ToString() + "h";
            }
            else
            {
                return lengthOfTime.Days.ToString() + "d";
            }
        }

        /// <summary>
        /// Format a DateTime value to short date format, separated by '-'.
        /// </summary>
        /// <param name="date">DateTime type that the method needs to be attached to.</param>
        /// <returns>Formatted date such as 2013-10-10.</returns>
        public static string ToShortFormatedDate(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToShortFormatedDate();
            }

            return string.Empty;
        }

        /// <summary>
        /// Format a DateTime value to short date format, separated by '-'.
        /// </summary>
        /// <param name="date">DateTime type that the method needs to be attached to.</param>
        /// <returns>Formatted date such as 2013-10-10.</returns>
        public static string ToShortFormatedDate(this DateTime date)
        {
            return date.ToString(ShortDateFormat);
        }

        public static string ToFullFormatedDate(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToFullFormatedDate();
            }
                
            return string.Empty;
        }

        public static string ToFullFormatedDate(this DateTime date)
        {
            return date.ToString(FullDateFormat);
        }
    }
}