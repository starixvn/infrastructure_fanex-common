using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fanex.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Timers;

namespace ConsoleLogging
{
    class Program
    {
        static void Main(string[] args)
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
