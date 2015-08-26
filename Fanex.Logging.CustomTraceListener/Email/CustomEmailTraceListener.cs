namespace Fanex.Logging.CustomTraceListener.Email
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
    using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
    [ConfigurationElementType(typeof(CustomEmailTraceListenerData))]
    public class CustomEmailTraceListener : CustomFormattedTraceListenerBase
    {
        private string _toAddress = string.Empty;
        private string _fromAddress = string.Empty;
        private string _subjectLineStarter = string.Empty;
        private string _subjectLineEnder = string.Empty;
        private string _smtpServer = string.Empty;
        private int _smtpPort = 25;
        private EmailAuthenticationMode _authenticationMode = EmailAuthenticationMode.None;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private bool _useSSL = false;
        private string _umAddress = string.Empty;
        private bool _isCheckingDowntimeEnabled = true;

        /// <summary>
        /// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress,
        /// subjectlinestarter, subjectlinender, smtpserver, and a formatter
        /// a <see cref="ILogFormatter"/>.
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="formatter">The Formatter <see cref="ILogFormatter"/> which determines how the
        /// email message should be formatted</param>
        public CustomEmailTraceListener(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, ILogFormatter formatter, string umAddress, bool isCheckingDowntimeEnabled)
            : this(toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, 25, formatter, umAddress, isCheckingDowntimeEnabled)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress,
        /// subjectlinestarter, subjectlinender, smtpserver, smtpport, and a formatter
        /// a <see cref="ILogFormatter"/>.
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="formatter">The Formatter <see cref="ILogFormatter"/> which determines how the
        /// email message should be formatted</param>
        public CustomEmailTraceListener(
            string toAddress,
            string fromAddress,
            string subjectLineStarter,
            string subjectLineEnder,
            string smtpServer,
            int smtpPort,
            ILogFormatter formatter,
            string umAddress,
            bool isCheckingDowntimeEnabled)
            : base(formatter)
        {
            this._toAddress = toAddress;
            this._fromAddress = fromAddress;
            this._subjectLineStarter = subjectLineStarter;
            this._subjectLineEnder = subjectLineEnder;
            this._smtpServer = smtpServer;
            this._smtpPort = smtpPort;
            this._umAddress = umAddress;
            this._isCheckingDowntimeEnabled = isCheckingDowntimeEnabled;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress,
        /// subjectlinestarter, subjectlinender, smtpserver, smtpport, and a formatter
        /// a <see cref="ILogFormatter"/>.
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="formatter">The Formatter <see cref="ILogFormatter"/> which determines how the
        /// email message should be formatted</param>
        /// <param name="authenticationMode">Authentication style to use when connecting to SMTP server.</param>
        /// <param name="userName">User name to use for authentication if using username/password authentication.</param>
        /// <param name="password">Password to use for authentication if using username/password authentication.</param>
        /// <param name="useSSL">Use SSL to connect to mail server if true, regular socket if false.</param>
        public CustomEmailTraceListener(
            string toAddress,
            string fromAddress,
            string subjectLineStarter,
            string subjectLineEnder,
            string smtpServer,
            int smtpPort,
            ILogFormatter formatter,
            EmailAuthenticationMode authenticationMode,
            string userName,
            string password,
            bool useSSL,
            string umAddress,
            bool isCheckingDowntimeEnabled)
            : base(formatter)
        {
            this._toAddress = toAddress;
            this._fromAddress = fromAddress;
            this._subjectLineStarter = subjectLineStarter;
            this._subjectLineEnder = subjectLineEnder;
            this._smtpServer = smtpServer;
            this._smtpPort = smtpPort;
            this._authenticationMode = authenticationMode;
            this._userName = userName;
            this._password = password;
            this._useSSL = useSSL;
            this._umAddress = umAddress;
            this._isCheckingDowntimeEnabled = isCheckingDowntimeEnabled;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress,
        /// subjectlinestarter, subjectlinender, smtpserver, smtpport, and a formatter
        /// a <see cref="ILogFormatter"/>.
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        public CustomEmailTraceListener(
            string toAddress,
            string fromAddress,
            string subjectLineStarter,
            string subjectLineEnder,
            string smtpServer,
            string umAddress,
            bool isCheckingDowntimeEnabled)
            : this(toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, 25, umAddress, isCheckingDowntimeEnabled)
        { 
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress,
        /// subjectlinestarter, subjectlinender, smtpserver, smtpport.
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        public CustomEmailTraceListener(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, string umAddress, bool isCheckingDowntimeEnabled)
            : this(toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, smtpPort, EmailAuthenticationMode.None, null, null, false, umAddress, isCheckingDowntimeEnabled)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress,
        /// subjectlinestarter, subjectlinender, smtpserver, smtpport and authentication information.
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="authenticationMode">Authentication style to use when connecting to SMTP server.</param>
        /// <param name="userName">User name to use for authentication if using username/password authentication.</param>
        /// <param name="password">Password to use for authentication if using username/password authentication.</param>
        /// <param name="useSSL">Use SSL to connect to mail server if true, regular socket if false.</param>
        public CustomEmailTraceListener(
            string toAddress,
            string fromAddress,
            string subjectLineStarter,
            string subjectLineEnder,
            string smtpServer,
            int smtpPort,
            EmailAuthenticationMode authenticationMode,
            string userName,
            string password,
            bool useSSL,
            string umAddress,
            bool isCheckingDowntimeEnabled)
        {
            this._toAddress = toAddress;
            this._fromAddress = fromAddress;
            this._subjectLineStarter = subjectLineStarter;
            this._subjectLineEnder = subjectLineEnder;
            this._smtpServer = smtpServer;
            this._smtpPort = smtpPort;
            this._authenticationMode = authenticationMode;
            this._userName = userName;
            this._password = password;
            this._useSSL = useSSL;
            this._umAddress = umAddress;
            this._isCheckingDowntimeEnabled = isCheckingDowntimeEnabled;
        }

        /// <summary>
        /// Sends an email message given a predefined string
        /// </summary>
        /// <param name="message">The string to write as the email message</param>
        public override void Write(string message)
        {
            DowntimeCheckingEmailMessage mailMessage =
                new DowntimeCheckingEmailMessage(
                    _toAddress,
                    _fromAddress,
                    _subjectLineStarter,
                    _subjectLineEnder,
                    _smtpServer,
                    _smtpPort,
                    message,
                    this.Formatter,
                    _authenticationMode,
                    _userName,
                    _password,
                    _useSSL);

            mailMessage.Send(_umAddress, _isCheckingDowntimeEnabled);
        }

        /// <summary>
        /// Sends an email message given a predefined string
        /// </summary>
        /// <param name="message">The string to write as the email message</param>
        public override void WriteLine(string message)
        {
            Write(message);
        }

        /// <summary>
        /// Delivers the trace data as an email message.
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
        /// <param name="source">The name of the trace source that delivered the trace data.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="id">The id of the event.</param>
        /// <param name="data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    DowntimeCheckingEmailMessage message = new DowntimeCheckingEmailMessage(
                        _toAddress,
                        _fromAddress,
                        _subjectLineStarter,
                        _subjectLineEnder,
                        _smtpServer,
                        _smtpPort,
                        data as LogEntry,
                        this.Formatter,
                        _authenticationMode,
                        _userName,
                        _password,
                        _useSSL);

                    message.Send(_umAddress, _isCheckingDowntimeEnabled);
                }
                else if (data is string)
                {
                    Write(data);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        /// <summary>
        /// Declare the supported attributes for <see cref="EmailTraceListener"/>
        /// </summary>
        protected override string[] GetSupportedAttributes()
        {
            return new string[9] { "formatter", "toAddress", "fromAddress", "subjectLineStarter", "subjectLineEnder", "smtpServer", "smtpPort", "umAddress", "isCheckingDowntimeEnabled" };
        }
    }
}