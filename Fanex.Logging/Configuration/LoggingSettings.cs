// <copyright file="LoggingSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Logging Settings and configuration info
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
// </history>

namespace Fanex.Logging.Configuration
{
    using System;
    using System.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

    /// <summary>
    /// Represents a configuration section that can be serialized and desterilized to XML.
    /// </summary>
    public class LoggingSettings : ConfigurationSection
    {
        private const string QueuePath = "msmqPath";
        private const string RollingFilePath = "filePath";
        private const string DefaultCategoryName = "defaultCategory";

        public static string SectionName
        {
            get { return "Fanex.Logging"; }
        }

        [ConfigurationProperty(QueuePath, IsRequired = false)]
        public string MsmqPath
        {
            get { return (string)base[QueuePath]; }
            set { base[QueuePath] = value; }
        }

        [ConfigurationProperty(RollingFilePath, IsRequired = false)]
        public string FilePath
        {
            get { return (string)base[RollingFilePath]; }
            set { base[RollingFilePath] = value; }
        }

        [ConfigurationProperty(DefaultCategoryName, IsRequired = true)]
        public string DefaultCategory
        {
            get { return (string)base[DefaultCategoryName]; }
            set { base[DefaultCategoryName] = value; }
        }

        public static LoggingSettings GetSettings(IConfigurationSource configurationSource)
        {
            if (configurationSource == null)
            {
                throw new ArgumentNullException("configurationSource");
            }

            return configurationSource.GetSection(SectionName) as LoggingSettings;
        }
    }
}
