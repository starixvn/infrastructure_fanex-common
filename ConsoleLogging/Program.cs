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
               // Timer timer = new Timer();
                
                
                int length = 5;
                //timer.Start();
                Console.WriteLine(DateTime.Now.ToLongTimeString());
                for (int i = 0; i < length; i++)
                {

                    LogService.Error("I am quanglewin " + i.ToString());
                    
                }
               // timer.Stop();
               // Console.WriteLine(DateTime.Now.ToLongTimeString());
                //timer.
                //Console.WriteLine("Time to log " + length.ToString()
                //    + " :" + timer.du);

                //watcher = new FileSystemWatcher(@"D:\Deploy\")
                //              {
                //                  Filter = "*.ini",
                //                  EnableRaisingEvents = true,
                //                  NotifyFilter = NotifyFilters.FileName |
                //                                 NotifyFilters.Attributes |
                //                                 NotifyFilters.LastAccess |
                //                                 NotifyFilters.LastWrite |
                //                                 NotifyFilters.Security |
                //                                 NotifyFilters.Size
                //              };

                //watcher.Changed += new FileSystemEventHandler(watcher_Changed);


                //    LoggerConfiguration.Run();

                //    LoggerService logService = new LoggerService();
                //    logService.Error("hahahhahahaha");


                //    Logger.Write("Quanglewin says Hello MSMQ " + DateTime.Now.ToString());
                //    Console.WriteLine("Send msg to queue OK" + DateTime.Now);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            Console.ReadKey();

        }

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File Changes");
        }
    }
}
