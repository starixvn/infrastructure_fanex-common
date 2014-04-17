// --------------------------------------------------------------------------
// <copyright file="Program.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.4.17">Created</change>
// </history>

namespace ConsoleLogging
{
    using System;
    using Fanex.Logging;

    /// <summary>
    /// The testing application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < 1; i++)
                {
                    LogService.Critical("Test logging.");

                    LogService.Warn("Test logging");

                    LogService.Critical("Test logging");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
