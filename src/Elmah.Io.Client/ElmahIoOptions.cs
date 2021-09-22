using System;
using System.Collections.Generic;
using System.Net;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Options to use when using the elmah.io client.
    /// </summary>
    public class ElmahIoOptions
    {
        /// <summary>
        /// Zero or more form key names to obfuscate before logging messages to elmah.io.
        /// </summary>
        public List<string> FormKeysToObfuscate { get; set; }

        /// <summary>
        /// Make it possible to communicate with the elmah.io API through a proxy. Proxy settings should
        /// be provided on the options when calling the ElmahioAPI.Create method. Once the client has been
        /// initialized, setting a proxy in this property will no longer have any effect.
        /// </summary>
        public IWebProxy WebProxy { get; set; }

        /// <summary>
        /// Modify the default timeout of 5 seconds when communicating with the elmah.io API.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Append a custom user agent to requests made to the elmah.io API. If you are using an integration
        /// this will be already handled. If you are using the Elmah.Io.Client package directly, this will be
        /// a good way to identify who you are. User agents should be on the form name/version.
        /// Example: PiedPiper/1.0.0.0
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Create a new instance with default options.
        /// </summary>
        public ElmahIoOptions()
        {
            FormKeysToObfuscate = new List<string>
            {
                "password",
                "pwd"
            };
            Timeout = TimeSpan.FromSeconds(5);
        }
    }
}
