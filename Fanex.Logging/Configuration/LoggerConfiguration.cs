// <copyright file="LoggerConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  run fluently config information 
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
// </history>


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Fanex.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Fanex.Logging.Configuration
{
    internal static class LoggerConfiguration
    {
        private const string Formater = "Timestamp: {timestamp}{newline}Message: {message}{newline}Category: {category}{newline}Priority: {priority}{newline}EventId: {eventid}{newline}Severity: {severity}{newline}Title:{title}{newline}Machine: {localMachine}{newline}App Domain: {localAppDomain}{newline}ProcessId: {localProcessId}{newline}Process Name: {localProcessName}{newline}Thread Name: {threadName}{newline}Win32 ThreadId:{win32ThreadId}{newline}Extended Properties: {dictionary({key} - {value}{newline})}";
        private static volatile LoggingSettings LoggingSetting;
        private static volatile bool _hasRunConfig = false;

        static LoggerConfiguration()
        {
            if (LoggingSetting == null)
            {
                LoggingSetting = LoggingSettings.GetSettings(ConfigurationSourceFactory.Create());
            }
        }

        public static void Run()
        {
            if (_hasRunConfig) return;

            var builder = new ConfigurationSourceBuilder();


            if (!string.IsNullOrWhiteSpace(Setting.MsmqPath))
            {
                builder.ConfigureLogging()
                    .SpecialSources.UnprocessedCategory
                    .SpecialSources.LoggingErrorsAndWarningsCategory
                    .SpecialSources.AllEventsCategory
                    .SendTo.Msmq("MsmqListener").UseQueue(Setting.MsmqPath)
                    .WithTraceOptions(TraceOptions.Callstack
                    | TraceOptions.DateTime
                    | TraceOptions.LogicalOperationStack
                    | TraceOptions.ProcessId
                    | TraceOptions.ThreadId
                    | TraceOptions.Timestamp)
                    .FormatWith(new FormatterBuilder().BinaryFormatterNamed("BinaryFormatter"));
            }

            //if (!string.IsNullOrWhiteSpace(Setting.FilePath))
            //{
               

            //    builder.ConfigureLogging()
            //        .SpecialSources.UnprocessedCategory
            //        .SpecialSources.LoggingErrorsAndWarningsCategory
            //        .SpecialSources.AllEventsCategory
            //    .SendTo.RollingFile("Rolling Log files")
            //    .RollEvery(RollInterval.Day)
            //    .ToFile(Setting.FilePath)
            //   .WithTraceOptions(TraceOptions.Callstack
            //    | TraceOptions.DateTime
            //    | TraceOptions.LogicalOperationStack
            //    | TraceOptions.ProcessId
            //    | TraceOptions.ThreadId
            //    | TraceOptions.Timestamp)
            //    .FormatWith(new FormatterBuilder().TextFormatterNamed("TextFormatter")
            //    .UsingTemplate(Formater));
            //}





            //if (!string.IsNullOrEmpty(Setting.FilePath))
            //{
            //    builder.ConfigureLogging()
            //         .SpecialSources.UnprocessedCategory
            //        .SpecialSources.LoggingErrorsAndWarningsCategory
            //        .SpecialSources.AllEventsCategory
            //        .SendTo
            //        .RollingFile(Setting.FilePath)
            //        .RollEvery(RollInterval.Day)
            //        .WithTraceOptions(TraceOptions.Callstack)
            //        .FormatWith(new FormatterBuilder().TextFormatterNamed("TextFormatter"));
            //}

            var configSource = new DictionaryConfigurationSource();
            builder.UpdateConfigurationWithReplace(configSource);

            EnterpriseLibraryContainer.Current
                = EnterpriseLibraryContainer.CreateDefaultContainer(configSource);

            _hasRunConfig = true;
        }

        public static LoggingSettings Setting
        {
            get { return LoggingSetting; }
        }
    }
}
