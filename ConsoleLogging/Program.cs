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
    using System.Threading;
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
                for (int i = 0; i < 2; i++)
                {
                    LogService.Error("Test logging.", new Exception("Demo Exception"));
                    LogService.Warn("Test warning");
                    LogService.Info("Test info");
                }

                LogService.Error("Test logging1.", new Exception("Demo Exception"));
                LogService.Warn("Test warning1");
                LogService.Info("Test info1");

                Thread.Sleep(130 * 1000);

                Console.WriteLine("Second time");

                LogService.Error("Test logging.", new Exception("Demo Exception"));
                LogService.Warn("Test warning");
                LogService.Info("Test info");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}