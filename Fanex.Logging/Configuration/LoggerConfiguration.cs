// <copyright file="LoggerConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  run fluently configuration information 
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
// </history>

namespace Fanex.Logging.Configuration
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

    /// <summary>
    /// The logger configuration class.
    /// </summary>
    internal static class LoggerConfiguration
    {
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed. Suppression is OK here.")]
        private const string Formater = "Timestamp: {timestamp}{newline}Message: {message}{newline}Category: {category}{newline}Priority: {priority}{newline}EventId: {eventid}{newline}Severity: {severity}{newline}Title:{title}{newline}Machine: {localMachine}{newline}App Domain: {localAppDomain}{newline}ProcessId: {localProcessId}{newline}Process Name: {localProcessName}{newline}Thread Name: {threadName}{newline}Win32 ThreadId:{win32ThreadId}{newline}Extended Properties: {dictionary({key} - {value}{newline})}";
        private static volatile LoggingSettings _loggingSetting;
        private static volatile bool _hasRunConfig = false;

        static LoggerConfiguration()
        {
            if (_loggingSetting == null)
            {
                _loggingSetting = LoggingSettings.GetSettings(ConfigurationSourceFactory.Create());
            }
        }

        public static LoggingSettings Setting
        {
            get { return _loggingSetting; }
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            if (_hasRunConfig)
            {
                return;
            }

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

            var configSource = new DictionaryConfigurationSource();
            builder.UpdateConfigurationWithReplace(configSource);

            EnterpriseLibraryContainer.Current = EnterpriseLibraryContainer.CreateDefaultContainer(configSource);
            _hasRunConfig = true;
        }
    }
}
