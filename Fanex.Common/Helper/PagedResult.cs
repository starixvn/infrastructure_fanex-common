// --------------------------------------------------------------------------
// <copyright file="PagedResult.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.4.17">Created</change>
// </history>

namespace Fanex.Common.Helper
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    /// <summary>
    /// The page result class.
    /// </summary>
    /// <typeparam name="T">The type of T.</typeparam>
    public class PagedResult<T>
    {
        private readonly ReadOnlyCollection<T> _result;
        private readonly int _total;

        public PagedResult(IEnumerable<T> result, int total)
        {
            Check.Argument.IsNotNull(result, "result");
            Check.Argument.IsNotNegative(total, "total");

            _result = new ReadOnlyCollection<T>(new List<T>(result));
            _total = total;
        }

        public PagedResult() : this(new List<T>(), 0)
        {
        }

        public ICollection<T> Result
        {
            [DebuggerStepThrough]
            get
            {
                return _result;
            }
        }

        public int Total
        {
            [DebuggerStepThrough]
            get
            {
                return _total;
            }
        }

        public bool IsEmpty
        {
            [DebuggerStepThrough]
            get
            {
                return _result.Count == 0;
            }
        }
    }
}