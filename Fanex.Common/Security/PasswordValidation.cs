// --------------------------------------------------------------------------
// <copyright file="PasswordValidation.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.4.17">Created</change>
// </history>

namespace Fanex.Security
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The password validation class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed. Suppression is OK here.")]
    public class PasswordValidation
    {
        private static Regex passwordRule1 = new Regex(@"^(?=.*\d).{8,100}$");
        private static Regex passwordRule2 = new Regex(@"^(?=.*[a-z]).{8,100}$");
        private static Regex passwordRule3 = new Regex(@"^(?=.*[A-Z]).{8,100}$");
        private static Regex passwordRule4 = new Regex(@"^(?=.*[\[\]\^\$\.\|\?\*\+\(\)\\~`\!@#%&\-_+={}'""<>:;,]).{8,100}$");
        private static Regex passwordMemberRule = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$");
        private static Regex notWhitespace = new Regex(@"^\S*$");

        /// <summary>
        /// Password must be at least 8 characters without white space and contain at least 3 of the following.
        ///  • Uppercase letter [A-Z].
        ///  • Lowercase leader [a-z].
        ///  • Numeric [0-9].
        ///  • Special character (!,@,#,etc..).
        /// For example: 59D7!4$h.
        /// </summary>
        /// <param name="password">The password string.</param>
        /// <returns>The result.</returns>
        public static bool CheckValidPassword(string password)
        {
            var r1 = passwordRule1.Match(password).Success ? 1 : 0;
            var r2 = passwordRule2.Match(password).Success ? 1 : 0;
            var r3 = passwordRule3.Match(password).Success ? 1 : 0;
            var r4 = passwordRule4.Match(password).Success ? 1 : 0;

            return (r1 + r2 + r3 + r4 >= 3) && notWhitespace.Match(password).Success;
        }

        /// <summary>
        /// Password must be 6-10 characters without white-space and contain at least 1 uppercase letter, 1 lowercase letter, 1 numeric digit. (e.g. 493abcDE, 59D7!4$h).
        /// Notes: your password is case sensitive.
        /// </summary>
        /// <param name="password">The password string.</param>
        /// <returns>The result.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static bool CheckValidMemberPassword(string password)
        {
            return passwordMemberRule.Match(password).Success && notWhitespace.Match(password).Success;
        }
    }
}
